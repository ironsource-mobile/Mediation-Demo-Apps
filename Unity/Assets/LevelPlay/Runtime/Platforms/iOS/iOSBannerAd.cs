#if UNITY_IOS && !UNITY_EDITOR
using System;
using System.Runtime.InteropServices;

namespace com.unity3d.mediation
{
    [Obsolete("This class will be deprecated in version 9.0.0. Please use ILevelPlayBannerAd instead.")]
    public class iOSBannerAd : IosNativeObject, IPlatformBannerAd
    {
        public event EventHandler<LevelPlayAdInfo> OnAdLoaded;
        public event EventHandler<LevelPlayAdError> OnAdLoadFailed;
        public event EventHandler<LevelPlayAdInfo> OnAdClicked;
        public event EventHandler<LevelPlayAdInfo> OnAdDisplayed;
        public event EventHandler<LevelPlayAdDisplayInfoError> OnAdDisplayFailed;
        public event EventHandler<LevelPlayAdInfo> OnAdExpanded;
        public event EventHandler<LevelPlayAdInfo> OnAdCollapsed;
        public event EventHandler<LevelPlayAdInfo> OnAdLeftApplication;

        public string AdUnitId { get; }
        public LevelPlayAdSize AdSize { get; }
        public string PlacementName { get; }
        public LevelPlayBannerPosition Position { get; }
        private bool DisplayOnLoad { get; }

        IosBannerAdListener _mBannerAdListener;

        public iOSBannerAd(string adUnitId, LevelPlayAdSize size, LevelPlayBannerPosition bannerPosition, string placementName, bool displayOnLoad) : base(true)
        {
            AdUnitId = adUnitId;
            AdSize = size;
            Position = bannerPosition;
            PlacementName = placementName;
            DisplayOnLoad = displayOnLoad;

            NativePtr = BannerAdCreate(adUnitId, placementName, size.Description, size.Width, size.Height, size.CustomWidth);
            if (_mBannerAdListener == null)
            {
                _mBannerAdListener = new IosBannerAdListener(this);
            }
            BannerAdSetDelegate(NativePtr, _mBannerAdListener.NativePtr);
        }

        public void PauseAutoRefresh()
        {
            if (CheckDisposedAndLogError("Cannot pause auto-refresh")) return;
            BannerAdPauseAutoRefresh(NativePtr);
        }

        public void ResumeAutoRefresh()
        {
            if (CheckDisposedAndLogError("Cannot resume auto-refresh")) return;
            BannerAdResumeAutoRefresh(NativePtr);
        }

        public void Load()
        {
            if (CheckDisposedAndLogError("Cannot call Load()")) return;
            BannerAdLoad(NativePtr);
            SetPosition();
            if (DisplayOnLoad)
            {
                ShowAd();
            }
            else
            {
                HideAd();
            }
        }

        public void DestroyAd()
        {
            if (NativePtr != IntPtr.Zero)
            {
                BannerAdDestroy(NativePtr);
                NativePtr = IntPtr.Zero;
            }
            base.Dispose();
        }

        public void SetPosition()
        {
            if (CheckDisposedAndLogError("Cannot set Banner Position")) return;
            BannerAdSetPosition(NativePtr, (int)Position);
        }

        public void ShowAd()
        {
            BannerAdViewShow(NativePtr);
        }

        public void HideAd()
        {
            BannerAdViewHide(NativePtr);
        }

        //Invoke events defined in iOSBannerAdListener.cs
        internal void InvokeLoadedEvent(LevelPlayAdInfo adInfo)
        {
            ThreadUtil.Post(state => OnAdLoaded?.Invoke(this, adInfo));
        }

        internal void InvokeFailedLoadEvent(LevelPlayAdError error)
        {
            ThreadUtil.Post(state => OnAdLoadFailed?.Invoke(this, error));
        }

        internal void InvokeClickedEvent(LevelPlayAdInfo adInfo)
        {
            ThreadUtil.Post(state => OnAdClicked?.Invoke(this, adInfo));
        }

        internal void InvokeDisplayedEvent(LevelPlayAdInfo adInfo)
        {
            ThreadUtil.Post(state => OnAdDisplayed?.Invoke(this, adInfo));
        }

        internal void InvokeFailedDisplayEvent(LevelPlayAdInfo adInfo, LevelPlayAdError error)
        {
            LevelPlayAdDisplayInfoError errorInfo = new LevelPlayAdDisplayInfoError(adInfo, error);
            ThreadUtil.Post(state => OnAdDisplayFailed?.Invoke(this, errorInfo));
        }

        internal void InvokeExpandedEvent(LevelPlayAdInfo adInfo)
        {
            ThreadUtil.Post(state => OnAdExpanded?.Invoke(this, adInfo));
        }

        internal void InvokeCollapsedEvent(LevelPlayAdInfo adInfo)
        {
            ThreadUtil.Post(state => OnAdCollapsed?.Invoke(this, adInfo));
        }

        internal void InvokeLeftApplicationEvent(LevelPlayAdInfo adInfo)
        {
            ThreadUtil.Post(state => OnAdLeftApplication?.Invoke(this, adInfo));
        }

        [DllImport("__Internal", EntryPoint = "LPMBannerAdViewCreate")]
        static extern IntPtr BannerAdCreate(string adUnitId, string placementName, string description, int width, int height, int customWidth);

        [DllImport("__Internal", EntryPoint = "LPMBannerAdViewSetDelegate")]
        static extern void BannerAdSetDelegate(IntPtr bannerAdView, IntPtr bannerAdListener);

        [DllImport("__Internal", EntryPoint = "LPMBannerAdViewLoadAd")]
        static extern void BannerAdLoad(IntPtr bannerAdView);

        [DllImport("__Internal", EntryPoint = "LPMBannerAdViewDestroy")]
        static extern void BannerAdDestroy(IntPtr bannerAdView);

        [DllImport("__Internal", EntryPoint = "LPMBannerAdViewSetPosition")]
        private static extern void BannerAdSetPosition(IntPtr bannerAdView, int position);

        [DllImport("__Internal", EntryPoint = "LPMBannerAdViewShow")]
        private static extern void BannerAdViewShow(IntPtr bannerAdView);

        [DllImport("__Internal", EntryPoint = "LPMBannerAdViewHide")]
        private static extern void BannerAdViewHide(IntPtr bannerAdView);


        [DllImport("__Internal", EntryPoint = "LPMBannerAdViewPauseAutoRefresh")]
        static extern void BannerAdPauseAutoRefresh(IntPtr bannerAdView);

        [DllImport("__Internal", EntryPoint = "LPMBannerAdViewResumeAutoRefresh")]
        static extern void BannerAdResumeAutoRefresh(IntPtr bannerAdView);
    }
}
#endif
