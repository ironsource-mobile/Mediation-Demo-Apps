package com.ironsource.ironsourcesdkdemo

import com.unity3d.mediation.rewarded.LevelPlayReward

interface DemoActivityListener {
    fun setEnablementForButton(buttonIdentifier: DemoButtonIdentifiers, enable: Boolean)
    fun setBannerViewVisibility(visibility: Int)
    fun setReward(reward: LevelPlayReward)
    fun showRewardDialog()
    fun createInterstitialAd()
    fun createRewardedAd()
    fun createBannerAd()
}