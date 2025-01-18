#if UNITY_ANDROID
using System;
using UnityEngine;

namespace com.unity3d.mediation
{
    class AndroidBannerAd : IPlatformBannerAd, IUnityBannerAdListener
    {
        const string k_BannerSizeClassName = "com.ironsource.unity.androidbridge.BannerUtils";
        const string k_BannerAdClassName   = "com.ironsource.unity.androidbridge.BannerAd";

        const string k_FuncGetAdSize = "getAdSize";
        const string k_FuncLoad        = "load";
        const string k_FuncDestroy     = "destroy";
        const string k_FuncShowAd      = "showAd";
        const string k_FuncHideAd      = "hideAd";
        const string k_FuncPauseAutoRefresh = "pauseAutoRefresh";
        const string k_FuncResumeAutoRefresh = "resumeAutoRefresh";

        const string k_ErrorCallingAdUnitId    = "Cannot call AdUnitId";
        const string k_ErrorCallingLoad        = "Cannot call Load()";
        const string k_ErrorFailedToLoad       = "Failed to load - ";
        const string k_ErrorCreatingBanner     = "Error while creating Banner Ad. Banner Ad will not load. Please check your build settings, and make sure LevelPlay SDK is integrated properly.";
        const string k_ErrorDisposed           = "LevelPlay SDK: {0}: Instance of type {1} is disposed. Please create a new instance in order to call any method.";

        public event EventHandler<LevelPlayAdInfo> OnAdLoaded;
        public event EventHandler<LevelPlayAdError> OnAdLoadFailed;
        public event EventHandler<LevelPlayAdInfo> OnAdClicked;
        public event EventHandler<LevelPlayAdInfo> OnAdDisplayed;
        public event EventHandler<LevelPlayAdDisplayInfoError> OnAdDisplayFailed;
        public event EventHandler<LevelPlayAdInfo> OnAdExpanded;
        public event EventHandler<LevelPlayAdInfo> OnAdCollapsed;
        public event EventHandler<LevelPlayAdInfo> OnAdLeftApplication;

        //IUnityBannerAdListener events
        public void onAdLoaded(string adInfo)
        {
            Debug.Log("onAdLoaded:" + adInfo);
            LevelPlayAdInfo info = new LevelPlayAdInfo(adInfo);
            OnAdLoaded?.Invoke(this, info);
        }

        public void onAdLoadFailed(string error)
        {
            OnAdLoadFailed?.Invoke(this, new LevelPlayAdError(error));
        }

        public void onAdClicked(string adInfo)
        {
            OnAdClicked?.Invoke(this, new LevelPlayAdInfo(adInfo));
        }

        public void onAdDisplayed(string adInfo)
        {
            OnAdDisplayed?.Invoke(this, new LevelPlayAdInfo(adInfo));
        }

        public void onAdDisplayFailed(string adInfo, string error)
        {
            OnAdDisplayFailed?.Invoke(this, new LevelPlayAdDisplayInfoError(new LevelPlayAdInfo(adInfo), new LevelPlayAdError(error)));
        }

        public void onAdExpanded(string adInfo)
        {
            OnAdExpanded?.Invoke(this, new LevelPlayAdInfo(adInfo));
        }

        public void onAdCollapsed(string adInfo)
        {
            OnAdCollapsed?.Invoke(this, new LevelPlayAdInfo(adInfo));
        }

        public void onAdLeftApplication(string adInfo)
        {
            OnAdLeftApplication?.Invoke(this, new LevelPlayAdInfo(adInfo));
        }

        public string AdUnitId { get; }
        public LevelPlayAdSize AdSize { get; }
        public LevelPlayBannerPosition Position { get; }
        public string PlacementName { get; }

        AndroidJavaObject _mBannerAd;
        IUnityBannerAdListener _mBannerAdListener;

        volatile bool _mDisposed;

        [Obsolete("This constructor will be removed in version 9.0.0. Please use ILevelPlayBannerAd instead.")]
        public AndroidBannerAd(string adUnitId, LevelPlayAdSize adSize, LevelPlayBannerPosition position, string placementName, bool displayOnLoad, bool respectSafeArea)
        {
            AdUnitId = adUnitId;
            AdSize = adSize;
            Position = position;
            PlacementName = placementName;

            ThreadUtil.Send(state =>
            {
                try
                {
                    _mBannerAdListener ??= new UnityBannerAdListener(this);
                    _mBannerAd = new AndroidJavaObject(k_BannerAdClassName, adUnitId,
                        adSize.Description, adSize.Width, adSize.Height, adSize.CustomWidth,
                        (int)position, placementName, displayOnLoad, respectSafeArea, _mBannerAdListener);
                }
                catch (Exception e)
                {
                    Debug.LogError(k_ErrorCreatingBanner);
                    Debug.LogException(e);
                }
            });
        }

        public void Load()
        {
            if (!CheckDisposedAndLogError(k_ErrorCallingLoad))
            {
                ThreadUtil.Post(state =>
                {
                    try
                    {
                        _mBannerAd.Call(k_FuncLoad);
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                        OnAdLoadFailed?.Invoke(this, new LevelPlayAdError(AdUnitId, -1, k_ErrorFailedToLoad + e.Message));
                    }
                });
            }
        }

        public void ShowAd()
        {
            if (!CheckDisposedAndLogError(k_ErrorCallingLoad))
            {
                ThreadUtil.Post(state =>
                {
                    try
                    {
                        _mBannerAd.Call(k_FuncShowAd);
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                        OnAdLoadFailed?.Invoke(this, new LevelPlayAdError(AdUnitId, -1, k_ErrorFailedToLoad + e.Message));
                    }
                });
            }
        }

        public void HideAd()
        {
            if (!CheckDisposedAndLogError(k_ErrorCallingLoad))
            {
                ThreadUtil.Post(state =>
                {
                    try
                    {
                        _mBannerAd.Call(k_FuncHideAd);
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                        OnAdLoadFailed?.Invoke(this, new LevelPlayAdError(AdUnitId, -1, k_ErrorFailedToLoad + e.Message));
                    }
                });
            }
        }

        public void PauseAutoRefresh()
        {
            ThreadUtil.Post(state =>
            {
                try
                {
                    _mBannerAd.Call(k_FuncPauseAutoRefresh);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            });
        }

        public void ResumeAutoRefresh()
        {
            ThreadUtil.Post(state =>
            {
                try
                {
                    _mBannerAd.Call(k_FuncResumeAutoRefresh);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            });
        }

        public void DestroyAd()
        {
            Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~AndroidBannerAd()
        {
            Dispose(false);
        }

        void Dispose(bool disposing)
        {
            if (!_mDisposed)
            {
                _mDisposed = true;
                if (disposing)
                {
                    //AndroidJavaObjects are created and destroyed with JNI's NewGlobalRef and DeleteGlobalRef,
                    //Therefore must be used on the same attached thread. In this case, it's Unity thread.
                    ThreadUtil.Post(state =>
                    {
                        _mBannerAd?.Call(k_FuncDestroy);
                        _mBannerAd?.Dispose();
                        _mBannerAdListener = null;
                        _mBannerAd = null;
                    });
                }
            }
        }

        bool CheckDisposedAndLogError(string message)
        {
            if (_mDisposed)
            {
                Debug.LogErrorFormat(k_ErrorDisposed, message, GetType().FullName);
            }
            return _mDisposed;
        }
    }
}
#endif
