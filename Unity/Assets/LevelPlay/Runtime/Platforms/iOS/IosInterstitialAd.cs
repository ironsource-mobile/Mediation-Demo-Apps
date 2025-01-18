#if UNITY_IOS && !UNITY_EDITOR
using System;
using System.Runtime.InteropServices;

namespace com.unity3d.mediation
{
    class IosInterstitialAd : IosNativeObject, IPlatformInterstitialAd
    {
        public event Action<LevelPlayAdInfo> OnAdLoaded;
        public event Action<LevelPlayAdError> OnAdLoadFailed;
        public event Action<LevelPlayAdInfo> OnAdDisplayed;
        public event Action<LevelPlayAdDisplayInfoError> OnAdDisplayFailed;
        public event Action<LevelPlayAdInfo> OnAdClicked;
        public event Action<LevelPlayAdInfo> OnAdClosed;
        public event Action<LevelPlayAdInfo> OnAdInfoChanged;
        public string AdUnitId { get; }

        IosInterstitialAdListener m_InterstitialListener;

        public IosInterstitialAd(string adUnitId) : base(true)
        {
            AdUnitId = adUnitId;
            NativePtr = InterstitialAdCreate(adUnitId);
            m_InterstitialListener = new IosInterstitialAdListener(this);
            InterstitialAdSetDelegate(NativePtr, m_InterstitialListener.NativePtr);
        }

        public void LoadAd()
        {
            if (CheckDisposedAndLogError("Cannot Load Interstitial Ad")) return;
            InterstitialAdLoadAd(NativePtr);
        }

        public void ShowAd(string placementName)
        {
            if (CheckDisposedAndLogError("Cannot Show Interstitial Ad")) return;
            InterstitialAdShowAd(NativePtr, placementName);
        }

        public bool IsAdReady()
        {
            if (CheckDisposedAndLogError("Cannot Check if Interstitial Ad is Ready")) return false;
            return InterstitialAdIsAdReady(NativePtr);
        }

        public static bool IsPlacementCapped(string placementName)
        {
            return InterstitialAdIsPlacementCapped(placementName);
        }

        public override void Dispose()
        {
            m_InterstitialListener?.Dispose();
            m_InterstitialListener = null;
            base.Dispose();
        }

        internal void InvokeLoadedEvent(string adInfo)
        {
            ThreadUtil.Post(state => OnAdLoaded?.Invoke(new LevelPlayAdInfo(adInfo)));
        }

        internal void InvokeFailedLoadEvent(string error)
        {
            ThreadUtil.Post(state => OnAdLoadFailed?.Invoke(new LevelPlayAdError(error)));
        }

        internal void InvokeClickedEvent(string adInfo)
        {
            ThreadUtil.Post(state => OnAdClicked?.Invoke(new LevelPlayAdInfo(adInfo)));
        }

        internal void InvokeDisplayedEvent(string adInfo)
        {
            ThreadUtil.Post(state => OnAdDisplayed?.Invoke(new LevelPlayAdInfo(adInfo)));
        }

        internal void InvokeFailedDisplayEvent(string adInfo, string error)
        {
            var errorInfo = new LevelPlayAdDisplayInfoError(new LevelPlayAdInfo(adInfo), new LevelPlayAdError(error));
            ThreadUtil.Post(state => OnAdDisplayFailed?.Invoke(errorInfo));
        }

        internal void InvokeClosedEvent(string adInfo)
        {
            ThreadUtil.Post(state => OnAdClosed?.Invoke(new LevelPlayAdInfo(adInfo)));
        }

        internal void InvokeOnAdInfoChangedEvent(string adInfo)
        {
            ThreadUtil.Post(state => OnAdInfoChanged?.Invoke(new LevelPlayAdInfo(adInfo)));
        }

        ~IosInterstitialAd()
        {
            Dispose();
        }

        [DllImport("__Internal", EntryPoint = "LPMInterstitialAdCreate")]
        static extern IntPtr InterstitialAdCreate(string adUnitId);

        [DllImport("__Internal", EntryPoint = "LPMInterstitialAdSetDelegate")]
        static extern void InterstitialAdSetDelegate(IntPtr interstitialAd, IntPtr interstitialAdListener);

        [DllImport("__Internal", EntryPoint = "LPMInterstitialAdLoadAd")]
        static extern void InterstitialAdLoadAd(IntPtr interstitialAd);

        [DllImport("__Internal", EntryPoint = "LPMInterstitialAdShowAd")]
        static extern void InterstitialAdShowAd(IntPtr interstitialAd, string placementName);

        [DllImport("__Internal", EntryPoint = "LPMInterstitialAdIsAdReady")]
        static extern bool InterstitialAdIsAdReady(IntPtr interstitialAd);

        [DllImport("__Internal", EntryPoint = "LPMInterstitialAdIsPlacementCapped")]
        static extern bool InterstitialAdIsPlacementCapped(string placementName);
    }
}
#endif
