using System;
using Unity.Services.LevelPlay;
using UnityEngine;

namespace com.unity3d.mediation
{
    class AndroidInterstitialAd : IPlatformInterstitialAd, IUnityInterstitialAdListener
    {
        const string k_AndroidInterstitialClass = "com.ironsource.unity.androidbridge.InterstitialAd";
        const string k_AndroidLoadAdFunction = "loadAd";
        const string k_AndroidShowAdFunction = "showAd";
        const string k_IsAdReadyFunction = "isAdReady";
        const string k_IsPlacementCappedStaticFunction = "isPlacementCapped";

        const string k_ErrorDisposed = "Instance is disposed. Please create a new instance in order to call any method.";


        public event Action<LevelPlayAdInfo> OnAdLoaded;
        public event Action<LevelPlayAdError> OnAdLoadFailed;
        public event Action<LevelPlayAdInfo> OnAdDisplayed;
        public event Action<LevelPlayAdInfo> OnAdClosed;
        public event Action<LevelPlayAdInfo> OnAdClicked;
        public event Action<LevelPlayAdDisplayInfoError> OnAdDisplayFailed;
        public event Action<LevelPlayAdInfo> OnAdInfoChanged;

        AndroidJavaObject m_InterstitialJavaObject;
        IUnityInterstitialAdListener m_InterstitialListener;

        volatile bool m_Disposed;
        volatile bool m_IsReady;


        public string AdUnitId { get; }

        internal AndroidInterstitialAd(string adUnitId)
        {
            AdUnitId = adUnitId;
            ThreadUtil.Send(state =>
            {
                try
                {
                    m_InterstitialListener ??= new UnityInterstitialAdListener(this);
                    m_InterstitialJavaObject =
                        new AndroidJavaObject(k_AndroidInterstitialClass, adUnitId, m_InterstitialListener);
                }
                catch (Exception e)
                {
                    LevelPlayLogger.LogException(e);
                }
            });
        }

        public void LoadAd()
        {
            if (!CheckDisposedAndLogError())
            {
                ThreadUtil.Post(state =>
                {
                    try
                    {
                        m_InterstitialJavaObject.Call(k_AndroidLoadAdFunction);
                    }
                    catch (Exception e)
                    {
                        LevelPlayLogger.LogException(e);
                    }
                });
            }
        }

        public void ShowAd(string placementName)
        {
            if (!CheckDisposedAndLogError())
            {
                ThreadUtil.Post(state =>
                {
                    try
                    {
                        m_InterstitialJavaObject.Call(k_AndroidShowAdFunction, placementName);
                    }
                    catch (Exception e)
                    {
                        LevelPlayLogger.LogException(e);
                    }
                });
            }
        }

        public bool IsAdReady()
        {
            if (!CheckDisposedAndLogError())
            {
                ThreadUtil.Send(state =>
                {
                    try
                    {
                        m_IsReady = m_InterstitialJavaObject.Call<bool>(k_IsAdReadyFunction);
                    }
                    catch (Exception e)
                    {
                        LevelPlayLogger.LogException(e);
                    }
                });
            }
            return m_IsReady;
        }

        public static bool IsPlacementCapped(string placementName)
        {
            var isPlacementCapped = false;
            try
            {
                using var interstitialJavaClass = new AndroidJavaClass(k_AndroidInterstitialClass);
                isPlacementCapped = interstitialJavaClass.CallStatic<bool>(k_IsPlacementCappedStaticFunction, placementName);
            }
            catch (Exception e)
            {
                LevelPlayLogger.LogException(e);
            }
            return isPlacementCapped;
        }

        public void onAdLoaded(string adInfo)
        {
            OnAdLoaded?.Invoke(new LevelPlayAdInfo(adInfo));
        }

        public void onAdLoadFailed(string error)
        {
            OnAdLoadFailed?.Invoke(new LevelPlayAdError(error));
        }

        public void onAdDisplayed(string adInfo)
        {
            OnAdDisplayed?.Invoke(new LevelPlayAdInfo(adInfo));
        }

        public void onAdDisplayFailed(string error, string adInfo)
        {
            OnAdDisplayFailed?.Invoke(new LevelPlayAdDisplayInfoError(new LevelPlayAdInfo(adInfo), new LevelPlayAdError(error)));
        }

        public void onAdClosed(string adInfo)
        {
            OnAdClosed?.Invoke(new LevelPlayAdInfo(adInfo));
        }

        public void onAdClicked(string adInfo)
        {
            OnAdClicked?.Invoke(new LevelPlayAdInfo(adInfo));
        }

        public void onAdInfoChanged(string adInfo)
        {
            OnAdInfoChanged?.Invoke(new LevelPlayAdInfo(adInfo));
        }

        void Dispose(bool disposing)
        {
            if (m_Disposed) return;
            m_Disposed = true;
            if (disposing)
            {
                ThreadUtil.Post(state =>
                {
                    m_InterstitialJavaObject?.Dispose();
                    m_IsReady = false;
                    m_InterstitialListener = null;
                    m_InterstitialJavaObject = null;
                });
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~AndroidInterstitialAd()
        {
            Dispose(false);
        }

        bool CheckDisposedAndLogError()
        {
            if (m_Disposed)
            {
                LevelPlayLogger.LogError(k_ErrorDisposed);
            }
            return m_Disposed;
        }
    }
}
