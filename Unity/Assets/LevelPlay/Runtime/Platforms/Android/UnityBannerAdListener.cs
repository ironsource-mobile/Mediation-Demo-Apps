#if UNITY_ANDROID
using UnityEngine;

namespace com.unity3d.mediation
{
    class UnityBannerAdListener : AndroidJavaProxy, IUnityBannerAdListener
    {
        const string k_IBannerAdListenerName = "com.ironsource.unity.androidbridge.IUnityBannerAdListener";

        IUnityBannerAdListener m_Listener;

        //verify listener bridge package path and class name
        public UnityBannerAdListener(IUnityBannerAdListener listener) : base(k_IBannerAdListenerName)
        {
            m_Listener = listener;
        }

        public void onAdLoaded(string adInfo)
        {
            //use dispatch to return to main thread
            ThreadUtil.Post(state => m_Listener.onAdLoaded(adInfo));
        }

        public void onAdLoadFailed(string error)
        {
            //use dispatch to return to main thread
            ThreadUtil.Post(state => m_Listener.onAdLoadFailed(error));
        }

        public void onAdDisplayed(string adInfo)
        {
            //use dispatch to return to main thread
            ThreadUtil.Post(state => m_Listener.onAdDisplayed(adInfo));
        }

        public void onAdDisplayFailed(string adInfo, string error)
        {
            ThreadUtil.Post(state => m_Listener.onAdDisplayFailed(adInfo, error));
        }

        public void onAdClicked(string adInfo)
        {
            ThreadUtil.Post(state => m_Listener.onAdClicked(adInfo));
        }

        public void onAdExpanded(string adInfo)
        {
            ThreadUtil.Post(state => m_Listener.onAdExpanded(adInfo));
        }

        public void onAdCollapsed(string adInfo)
        {
            ThreadUtil.Post(state => m_Listener.onAdCollapsed(adInfo));
        }

        public void onAdLeftApplication(string adInfo)
        {
            ThreadUtil.Post(state => m_Listener.onAdLeftApplication(adInfo));
        }
    }
}
#endif
