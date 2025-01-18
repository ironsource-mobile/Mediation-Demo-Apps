using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Unity.Services.LevelPlay.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]


namespace com.unity3d.mediation
{
    internal interface IPlatformInterstitialAd : IDisposable
    {
        event Action<LevelPlayAdInfo> OnAdLoaded;
        event Action<LevelPlayAdError> OnAdLoadFailed;
        event Action<LevelPlayAdInfo> OnAdDisplayed;
        event Action<LevelPlayAdInfo> OnAdClosed;
        event Action<LevelPlayAdInfo> OnAdClicked;
        event Action<LevelPlayAdDisplayInfoError> OnAdDisplayFailed;
        event Action<LevelPlayAdInfo> OnAdInfoChanged;

        string AdUnitId { get; }

        void LoadAd();

        void ShowAd(string placementName);

        bool IsAdReady();
    }
}
