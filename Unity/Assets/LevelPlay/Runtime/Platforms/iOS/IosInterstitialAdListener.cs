#if UNITY_IOS && !UNITY_EDITOR
using System;
using System.Runtime.InteropServices;
using AOT;

namespace com.unity3d.mediation
{
    class IosInterstitialAdListener : IosNativeObject
    {
        readonly DidLoadAdWithAdInfo k_LoadSuccessCallback = Loaded;
        readonly DidFailToLoadAdWithAdUnitId k_FailedToLoadCallback = LoadFailed;
        readonly DidDisplayWithAdInfo k_DisplayedCallback = Displayed;
        readonly DidFailToDisplayWithAdInfo k_DisplayFailedCallback = DisplayFailed;
        readonly DidClickWithAdInfo k_ClickedCallback = Clicked;
        readonly DidCloseWithAdInfo k_ClosedCallback = Closed;
        readonly DidChangeAdInfo k_AdInfoChangedCallback = AdInfoChanged;

        IosInterstitialAd m_InterstitialAd;
        internal IosInterstitialAdListener(IosInterstitialAd interstitialAd) : base(true)
        {
            NativePtr = LPMInterstitialAdDelegateCreate(interstitialAd.NativePtr, k_LoadSuccessCallback, k_FailedToLoadCallback, k_DisplayedCallback, k_DisplayFailedCallback, k_ClickedCallback, k_ClosedCallback, k_AdInfoChangedCallback);
            m_InterstitialAd = interstitialAd;
        }

        [DllImport("__Internal", EntryPoint = "LPMInterstitialAdDelegateCreate")]
        static extern IntPtr LPMInterstitialAdDelegateCreate(IntPtr interstitialAd, DidLoadAdWithAdInfo loadSuccess, DidFailToLoadAdWithAdUnitId loadFailed, DidDisplayWithAdInfo displayed, DidFailToDisplayWithAdInfo displayFailed, DidClickWithAdInfo clicked, DidCloseWithAdInfo closed, DidChangeAdInfo adInfoChanged);

        [DllImport("__Internal", EntryPoint = "LPMInterstitialAdDelegateDestroy")]
        static extern void LPMInterstitialAdDelegateDestroy(IntPtr delegatePtr);

        delegate void DidLoadAdWithAdInfo(IntPtr interstitialAd, string adInfoJson);

        delegate void DidFailToLoadAdWithAdUnitId(IntPtr interstitialAd, string errorPtr);

        delegate void DidDisplayWithAdInfo(IntPtr interstitialAd, string adInfoJson);

        delegate void DidFailToDisplayWithAdInfo(IntPtr interstitialAd, string adInfoJson, String errorPtr);

        delegate void DidClickWithAdInfo(IntPtr interstitialAd, string adInfoJson);

        delegate void DidCloseWithAdInfo(IntPtr interstitialAd, string adInfoJson);

        delegate void DidChangeAdInfo(IntPtr interstitialAd, string adInfoJson);

        [MonoPInvokeCallback(typeof(DidLoadAdWithAdInfo))]
        static void Loaded(IntPtr ptr, string adInfoJson)
        {
            var interstitialAd = Get<IosInterstitialAd>(ptr);
            interstitialAd?.InvokeLoadedEvent(adInfoJson);
        }

        [MonoPInvokeCallback(typeof(DidFailToLoadAdWithAdUnitId))]
        static void LoadFailed(IntPtr ptr, string errorPtr)
        {
            var interstitialAd = Get<IosInterstitialAd>(ptr);
            interstitialAd?.InvokeFailedLoadEvent(errorPtr);
        }

        [MonoPInvokeCallback(typeof(DidClickWithAdInfo))]
        static void Clicked(IntPtr ptr, string adInfoJson)
        {
            var interstitialAd = Get<IosInterstitialAd>(ptr);
            interstitialAd?.InvokeClickedEvent(adInfoJson);
        }

        [MonoPInvokeCallback(typeof(DidDisplayWithAdInfo))]
        static void Displayed(IntPtr ptr, string adInfoJson)
        {
            var interstitialAd = Get<IosInterstitialAd>(ptr);
            interstitialAd?.InvokeDisplayedEvent(adInfoJson);
        }

        [MonoPInvokeCallback(typeof(DidFailToDisplayWithAdInfo))]
        static void DisplayFailed(IntPtr ptr, string adInfoJson, String errorPtr)
        {
            var interstitialAd = Get<IosInterstitialAd>(ptr);
            interstitialAd?.InvokeFailedDisplayEvent(adInfoJson, errorPtr);
        }

        [MonoPInvokeCallback(typeof(DidCloseWithAdInfo))]
        static void Closed(IntPtr ptr, string adInfoJson)
        {
            var interstitialAd = Get<IosInterstitialAd>(ptr);
            interstitialAd?.InvokeClosedEvent(adInfoJson);
        }

        [MonoPInvokeCallback(typeof(DidChangeAdInfo))]
        static void AdInfoChanged(IntPtr ptr, string adInfoJson)
        {
            var interstitialAd = Get<IosInterstitialAd>(ptr);
            interstitialAd?.InvokeOnAdInfoChangedEvent(adInfoJson);
        }
    }
}
#endif
