package com.unity3d.levelplaydemo;

import com.unity3d.mediation.rewarded.LevelPlayReward;

public interface DemoActivityListener {
    void setEnablementForButton(DemoButtonIdentifiers buttonIdentifier, boolean enable);
    void setBannerViewVisibility(int visibility);
    void setReward(LevelPlayReward reward);
    void showRewardDialog();
    void createInterstitialAd();
    void createRewardedAd();
    void createBannerAd();
}