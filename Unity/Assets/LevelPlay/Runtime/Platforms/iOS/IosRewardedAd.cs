#if UNITY_IOS && !UNITY_EDITOR
using System;
using System.Runtime.InteropServices;

namespace com.unity3d.mediation
{
    class IosRewardedAd : IosNativeObject, IPlatformRewardedAd
    {
        public event Action<LevelPlayAdInfo> OnAdLoaded;
        public event Action<LevelPlayAdError> OnAdLoadFailed;
        public event Action<LevelPlayAdInfo> OnAdDisplayed;
        public event Action<LevelPlayAdDisplayInfoError> OnAdDisplayFailed;
        public event Action<LevelPlayAdInfo, LevelPlayReward> OnAdRewarded;
        public event Action<LevelPlayAdInfo> OnAdClicked;
        public event Action<LevelPlayAdInfo> OnAdClosed;
        public event Action<LevelPlayAdInfo> OnAdInfoChanged;
        public string AdUnitId { get; }

        IosRewardedAdListener m_RewardedAdListener;

        public IosRewardedAd(string adUnitId) : base(true)
        {
            AdUnitId = adUnitId;
            NativePtr = RewardedAdCreate(adUnitId);
            m_RewardedAdListener = new IosRewardedAdListener(this);
            RewardedAdSetDelegate(NativePtr, m_RewardedAdListener.NativePtr);
        }

        public void LoadAd()
        {
            if (CheckDisposedAndLogError("Cannot Load Rewarded Ad")) return;
            RewardedAdLoadAd(NativePtr);
        }

        public void ShowAd(string placementName)
        {
            if (CheckDisposedAndLogError("Cannot Show Rewarded Ad")) return;
            RewardedAdShowAd(NativePtr, placementName);
        }

        public bool IsAdReady()
        {
            if (CheckDisposedAndLogError("Cannot Check if Rewarded Ad is Ready")) return false;
            return RewardedAdIsAdReady(NativePtr);
        }

        public static bool IsPlacementCapped(string placementName)
        {
            return RewardedAdIsPlacementCapped(placementName);
        }

        public override void Dispose()
        {
            m_RewardedAdListener?.Dispose();
            m_RewardedAdListener = null;
            base.Dispose();
        }

        internal void InvokeLoadedEvent(string adInfo)
        {
            ThreadUtil.Post(_ => OnAdLoaded?.Invoke(new LevelPlayAdInfo(adInfo)));
        }

        internal void InvokeFailedLoadEvent(string error)
        {
            ThreadUtil.Post(_ => OnAdLoadFailed?.Invoke(new LevelPlayAdError(error)));
        }

        internal void InvokeDisplayedEvent(string adInfo)
        {
            ThreadUtil.Post(_ => OnAdDisplayed?.Invoke(new LevelPlayAdInfo(adInfo)));
        }

        internal void InvokeFailedDisplayEvent(string adInfo, string error)
        {
            var errorInfo = new LevelPlayAdDisplayInfoError(new LevelPlayAdInfo(adInfo), new LevelPlayAdError(error));
            ThreadUtil.Post(_ => OnAdDisplayFailed?.Invoke(errorInfo));
        }

        internal void InvokeRewardedEvent(string adInfo, string rewardName, int rewardAmount)
        {
            ThreadUtil.Post(_ => OnAdRewarded?.Invoke(new LevelPlayAdInfo(adInfo), new LevelPlayReward(rewardName, rewardAmount)));
        }

        internal void InvokeClickedEvent(string adInfo)
        {
            ThreadUtil.Post(_ => OnAdClicked?.Invoke(new LevelPlayAdInfo(adInfo)));
        }

        internal void InvokeClosedEvent(string adInfo)
        {
            ThreadUtil.Post(_ => OnAdClosed?.Invoke(new LevelPlayAdInfo(adInfo)));
        }

        internal void InvokeOnAdInfoChangedEvent(string adInfo)
        {
            ThreadUtil.Post(_ => OnAdInfoChanged?.Invoke(new LevelPlayAdInfo(adInfo)));
        }

        ~IosRewardedAd()
        {
            Dispose();
        }

        [DllImport("__Internal", EntryPoint = "LPMRewardedAdCreate")]
        static extern IntPtr RewardedAdCreate(string adUnitId);

        [DllImport("__Internal", EntryPoint = "LPMRewardedAdSetDelegate")]
        static extern void RewardedAdSetDelegate(IntPtr rewardedAd, IntPtr rewardedAdListener);

        [DllImport("__Internal", EntryPoint = "LPMRewardedAdLoadAd")]
        static extern void RewardedAdLoadAd(IntPtr rewardedAd);

        [DllImport("__Internal", EntryPoint = "LPMRewardedAdShowAd")]
        static extern void RewardedAdShowAd(IntPtr rewardedAd, string placementName);

        [DllImport("__Internal", EntryPoint = "LPMRewardedAdIsAdReady")]
        static extern bool RewardedAdIsAdReady(IntPtr rewardedAd);

        [DllImport("__Internal", EntryPoint = "LPMRewardedAdIsPlacementCapped")]
        static extern bool RewardedAdIsPlacementCapped(string placementName);
    }
}
#endif
