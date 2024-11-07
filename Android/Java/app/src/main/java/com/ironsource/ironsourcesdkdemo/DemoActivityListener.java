package com.ironsource.ironsourcesdkdemo;

import com.unity3d.mediation.rewarded.LevelPlayReward;

interface DemoActivityListener {
    void setEnablementForButton(DemoButtonIdentifiers buttonIdentifier, boolean enable);
    void setBannerViewVisibility(int visibility);
    void setReward(LevelPlayReward reward);
    void showRewardDialog();
    void createInterstitialAd();
    void createBannerAd();
    void createRewardedAd();
}