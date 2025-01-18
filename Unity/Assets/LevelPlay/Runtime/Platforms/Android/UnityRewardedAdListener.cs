using UnityEngine;

namespace com.unity3d.mediation
{
    class UnityRewardedAdListener : AndroidJavaProxy, IUnityRewardedAdListener
    {
        const string k_AndroidRewardedAdListenerName = "com.ironsource.unity.androidbridge.IUnityRewardedAdListener";

        readonly IUnityRewardedAdListener m_UnityListener;

        public UnityRewardedAdListener(IUnityRewardedAdListener listener) : base(k_AndroidRewardedAdListenerName)
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

        public void onAdRewarded(string adInfo, string rewardName, int rewardAmount)
        {
            ThreadUtil.Post(state => m_UnityListener.onAdRewarded(adInfo, rewardName, rewardAmount));
        }

        public void onAdClicked(string adInfo)
        {
            ThreadUtil.Post(state => m_UnityListener.onAdClicked(adInfo));
        }

        public void onAdClosed(string adInfo)
        {
            ThreadUtil.Post(state => m_UnityListener.onAdClosed(adInfo));
        }

        public void onAdInfoChanged(string adInfo)
        {
            ThreadUtil.Post(state => m_UnityListener.onAdInfoChanged(adInfo));
        }
    }
}
