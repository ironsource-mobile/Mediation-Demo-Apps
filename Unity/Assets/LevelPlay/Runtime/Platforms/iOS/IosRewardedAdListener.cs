#if UNITY_IOS && !UNITY_EDITOR
using System;
using System.Runtime.InteropServices;
using AOT;

namespace com.unity3d.mediation
{
    class IosRewardedAdListener : IosNativeObject
    {
        readonly DidLoadAdWithAdInfo k_LoadSuccessCallback = Loaded;
        readonly DidFailToLoadAdWithAdUnitId k_FailedToLoadCallback = LoadFailed;
        readonly DidDisplayWithAdInfo k_DisplayedCallback = Displayed;
        readonly DidFailToDisplayWithAdInfo k_DisplayFailedCallback = DisplayFailed;
        readonly DidAdRewardedWithAdInfo k_RewardedCallback = Rewarded;
        readonly DidClickWithAdInfo k_ClickedCallback = Clicked;
        readonly DidCloseWithAdInfo k_ClosedCallback = Closed;
        readonly DidChangeAdInfo k_AdInfoChangedCallback = AdInfoChanged;

        IosRewardedAd _mRewardedAd;
        internal IosRewardedAdListener(IosRewardedAd rewardedAd) : base(true)
        {
            NativePtr = LPMRewardedAdDelegateCreate(rewardedAd.NativePtr, k_LoadSuccessCallback, k_FailedToLoadCallback, k_DisplayedCallback, k_DisplayFailedCallback, k_RewardedCallback, k_ClickedCallback, k_ClosedCallback, k_AdInfoChangedCallback);
            _mRewardedAd = rewardedAd;
        }

        [DllImport("__Internal", EntryPoint = "LPMRewardedAdDelegateCreate")]
        static extern IntPtr LPMRewardedAdDelegateCreate(IntPtr rewardedAd, DidLoadAdWithAdInfo loadSuccess, DidFailToLoadAdWithAdUnitId loadFailed, DidDisplayWithAdInfo displayed, DidFailToDisplayWithAdInfo displayFailed, DidAdRewardedWithAdInfo rewarded, DidClickWithAdInfo clicked, DidCloseWithAdInfo closed, DidChangeAdInfo adInfoChanged);

        [DllImport("__Internal", EntryPoint = "LPMRewardedAdDelegateDestroy")]
        static extern void LPMRewardedAdDelegateDestroy(IntPtr delegatePtr);

        delegate void DidLoadAdWithAdInfo(IntPtr rewardedAd, string adInfoJson);

        delegate void DidFailToLoadAdWithAdUnitId(IntPtr rewardedAd, string errorPtr);

        delegate void DidDisplayWithAdInfo(IntPtr rewardedAd, string adInfoJson);

        delegate void DidFailToDisplayWithAdInfo(IntPtr rewardedAd, string adInfoJson, String errorPtr);

        delegate void DidAdRewardedWithAdInfo(IntPtr rewardedAd, string adInfoJson, string rewardName, int rewardAmount);

        delegate void DidClickWithAdInfo(IntPtr rewardedAd, string adInfoJson);

        delegate void DidCloseWithAdInfo(IntPtr rewardedAd, string adInfoJson);

        delegate void DidChangeAdInfo(IntPtr rewardedAd, string adInfoJson);

        [MonoPInvokeCallback(typeof(DidLoadAdWithAdInfo))]
        static void Loaded(IntPtr ptr, string adInfoJson)
        {
            var rewardedAd = Get<IosRewardedAd>(ptr);
            rewardedAd?.InvokeLoadedEvent(adInfoJson);
        }

        [MonoPInvokeCallback(typeof(DidFailToLoadAdWithAdUnitId))]
        static void LoadFailed(IntPtr ptr, string errorPtr)
        {
            var rewardedAd = Get<IosRewardedAd>(ptr);
            rewardedAd?.InvokeFailedLoadEvent(errorPtr);
        }

        [MonoPInvokeCallback(typeof(DidDisplayWithAdInfo))]
        static void Displayed(IntPtr ptr, string adInfoJson)
        {
            var rewardedAd = Get<IosRewardedAd>(ptr);
            rewardedAd?.InvokeDisplayedEvent(adInfoJson);
        }

        [MonoPInvokeCallback(typeof(DidFailToDisplayWithAdInfo))]
        static void DisplayFailed(IntPtr ptr, string adInfoJson, String errorPtr)
        {
            var rewardedAd = Get<IosRewardedAd>(ptr);
            rewardedAd?.InvokeFailedDisplayEvent(adInfoJson, errorPtr);
        }

        [MonoPInvokeCallback(typeof(DidAdRewardedWithAdInfo))]
        static void Rewarded(IntPtr ptr, string adInfoJson, string rewardName, int rewardAmount)
        {
            var rewardedAd = Get<IosRewardedAd>(ptr);
            rewardedAd?.InvokeRewardedEvent(adInfoJson, rewardName, rewardAmount);
        }

        [MonoPInvokeCallback(typeof(DidClickWithAdInfo))]
        static void Clicked(IntPtr ptr, string adInfoJson)
        {
            var rewardedAd = Get<IosRewardedAd>(ptr);
            rewardedAd?.InvokeClickedEvent(adInfoJson);
        }

        [MonoPInvokeCallback(typeof(DidCloseWithAdInfo))]
        static void Closed(IntPtr ptr, string adInfoJson)
        {
            var rewardedAd = Get<IosRewardedAd>(ptr);
            rewardedAd?.InvokeClosedEvent(adInfoJson);
        }

        [MonoPInvokeCallback(typeof(DidChangeAdInfo))]
        static void AdInfoChanged(IntPtr ptr, string adInfoJson)
        {
            var rewardedAd = Get<IosRewardedAd>(ptr);
            rewardedAd?.InvokeOnAdInfoChangedEvent(adInfoJson);
        }
    }
}
#endif
