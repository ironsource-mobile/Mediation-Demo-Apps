using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Unity.Services.LevelPlay.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]


namespace com.unity3d.mediation
{
    internal interface IPlatformRewardedAd : IDisposable
    {
        event Action<LevelPlayAdInfo> OnAdLoaded;
        event Action<LevelPlayAdError> OnAdLoadFailed;
        event Action<LevelPlayAdInfo> OnAdDisplayed;
        event Action<LevelPlayAdDisplayInfoError> OnAdDisplayFailed;
        event Action<LevelPlayAdInfo, LevelPlayReward> OnAdRewarded;
        event Action<LevelPlayAdInfo> OnAdClicked;
        event Action<LevelPlayAdInfo> OnAdClosed;
        event Action<LevelPlayAdInfo> OnAdInfoChanged;

        string AdUnitId { get; }

        void LoadAd();

        void ShowAd(string placementName);

        bool IsAdReady();
    }
}
