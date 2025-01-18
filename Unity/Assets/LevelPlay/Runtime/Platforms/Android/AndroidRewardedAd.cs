using System;
using Unity.Services.LevelPlay;
using UnityEngine;

namespace com.unity3d.mediation
{
    class AndroidRewardedAd : IPlatformRewardedAd, IUnityRewardedAdListener
    {
        const string k_AndroidRewardedAdClass = "com.ironsource.unity.androidbridge.RewardedAd";
        const string k_AndroidLoadAdFunction = "loadAd";
        const string k_AndroidShowAdFunction = "showAd";
        const string k_IsAdReadyFunction = "isAdReady";
        const string k_IsPlacementCappedStaticFunction = "isPlacementCapped";

        const string k_ErrorDisposed = "Instance is disposed. Please create a new instance in order to call any method.";


        public event Action<LevelPlayAdInfo> OnAdLoaded;
        public event Action<LevelPlayAdError> OnAdLoadFailed;
        public event Action<LevelPlayAdInfo> OnAdDisplayed;
        public event Action<LevelPlayAdDisplayInfoError> OnAdDisplayFailed;
        public event Action<LevelPlayAdInfo, LevelPlayReward> OnAdRewarded;
        public event Action<LevelPlayAdInfo> OnAdClicked;
        public event Action<LevelPlayAdInfo> OnAdClosed;
        public event Action<LevelPlayAdInfo> OnAdInfoChanged;

        AndroidJavaObject m_RewardedAdJavaObject;
        IUnityRewardedAdListener m_RewardedAdListener;

        volatile bool m_Disposed;
        volatile bool m_IsReady;

        public string AdUnitId { get; }

        internal AndroidRewardedAd(string adUnitId)
        {
            AdUnitId = adUnitId;
            ThreadUtil.Send(state =>
            {
                try
                {
                    m_RewardedAdListener ??= new UnityRewardedAdListener(this);
                    m_RewardedAdJavaObject =
                        new AndroidJavaObject(k_AndroidRewardedAdClass, adUnitId, m_RewardedAdListener);
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
                        m_RewardedAdJavaObject.Call(k_AndroidLoadAdFunction);
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
                        m_RewardedAdJavaObject.Call(k_AndroidShowAdFunction, placementName);
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
                        m_IsReady = m_RewardedAdJavaObject.Call<bool>(k_IsAdReadyFunction);
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
                using var rewardedAdJavaClass = new AndroidJavaClass(k_AndroidRewardedAdClass);
                isPlacementCapped = rewardedAdJavaClass.CallStatic<bool>(k_IsPlacementCappedStaticFunction, placementName);
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

        public void onAdRewarded(string adInfo, string rewardName, int rewardAmount)
        {
            OnAdRewarded?.Invoke(new LevelPlayAdInfo(adInfo), new LevelPlayReward(rewardName, rewardAmount));
        }

        public void onAdClicked(string adInfo)
        {
            OnAdClicked?.Invoke(new LevelPlayAdInfo(adInfo));
        }

        public void onAdClosed(string adInfo)
        {
            OnAdClosed?.Invoke(new LevelPlayAdInfo(adInfo));
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
                    m_RewardedAdJavaObject?.Dispose();
                    m_IsReady = false;
                    m_RewardedAdListener = null;
                    m_RewardedAdJavaObject = null;
                });
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~AndroidRewardedAd()
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
