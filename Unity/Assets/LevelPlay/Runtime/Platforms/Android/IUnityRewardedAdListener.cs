namespace com.unity3d.mediation
{
    interface IUnityRewardedAdListener
    {
        void onAdLoaded(string adInfo);
        void onAdLoadFailed(string error);
        void onAdDisplayed(string adInfo);
        void onAdDisplayFailed(string error, string adInfo);
        void onAdRewarded(string adInfo, string rewardName, int rewardAmount);
        void onAdClicked(string adInfo);
        void onAdClosed(string adInfo);
        void onAdInfoChanged(string adInfo);
    }
}
