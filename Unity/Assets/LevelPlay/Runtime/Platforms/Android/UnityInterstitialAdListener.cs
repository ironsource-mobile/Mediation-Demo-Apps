using UnityEngine;

namespace com.unity3d.mediation
{
    class UnityInterstitialAdListener : AndroidJavaProxy, IUnityInterstitialAdListener
    {
        const string k_AndroidInterstitialListenerName = "com.ironsource.unity.androidbridge.IUnityInterstitialAdListener";

        readonly IUnityInterstitialAdListener m_UnityListener;

        public UnityInterstitialAdListener(IUnityInterstitialAdListener listener) : base(k_AndroidInterstitialListenerName)
        {
            m_UnityListener = listener;
        }

        public void onAdLoaded(string adInfo)
        {
            ThreadUtil.Post(state => m_UnityListener.onAdLoaded(adInfo));
        }

        public void onAdLoadFailed(string error)
        {
            ThreadUtil.Post(state => m_UnityListener.onAdLoadFailed(error));
        }

        public void onAdDisplayed(string adInfo)
        {
            ThreadUtil.Post(state => m_UnityListener.onAdDisplayed(adInfo));
        }

        public void onAdDisplayFailed(string error, string adInfo)
        {
            ThreadUtil.Post(state => m_UnityListener.onAdDisplayFailed(error, adInfo));
        }

        public void onAdClosed(string adInfo)
        {
            ThreadUtil.Post(state => m_UnityListener.onAdClosed(adInfo));
        }

        public void onAdClicked(string adInfo)
        {
            ThreadUtil.Post(state => m_UnityListener.onAdClicked(adInfo));
        }

        public void onAdInfoChanged(string adInfo)
        {
            ThreadUtil.Post(state => m_UnityListener.onAdInfoChanged(adInfo));
        }
    }
}
