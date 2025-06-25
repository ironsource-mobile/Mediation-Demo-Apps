package com.unity3d.levelplaydemo

import com.unity3d.levelplaydemo.DemoActivity.Companion.logCallbackName
import com.unity3d.mediation.LevelPlayAdError
import com.unity3d.mediation.LevelPlayAdInfo
import com.unity3d.mediation.rewarded.LevelPlayReward
import com.unity3d.mediation.rewarded.LevelPlayRewardedAdListener

class DemoRewardedAdListener(private val listener: DemoActivityListener) :
    LevelPlayRewardedAdListener {

    private val TAG = DemoRewardedAdListener::class.java.name


    /**
    Called after an rewarded ad has been loaded
    @param adInfo The info of the ad
     */
    override fun onAdLoaded(adInfo: LevelPlayAdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
        listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_REWARDED_VIDEO_BUTTON_IDENTIFIER, true)
    }

    override fun onAdRewarded(reward: LevelPlayReward, adInfo: LevelPlayAdInfo) {
        listener.setReward(reward)
        logCallbackName(TAG, "adInfo = $adInfo")
    }

    /**
    Called after an rewarded ad has attempted to load but failed
    @param error The reason for the error
     */
    override fun onAdLoadFailed(error: LevelPlayAdError) {
        logCallbackName(TAG, "error = $error")
        listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_REWARDED_VIDEO_BUTTON_IDENTIFIER, false)
    }


    /**
    Called after an rewarded ad has been displayed
    This is the indication for impression
    @param adInfo The info of the ad
     */
    override fun onAdDisplayed(adInfo: LevelPlayAdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
        listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_REWARDED_VIDEO_BUTTON_IDENTIFIER, false)
    }

    /**
    Called after an rewarded ad has attempted to display but failed
    @param error The reason for the error
    @param adInfo The info of the ad
     */
    override fun onAdDisplayFailed(error: LevelPlayAdError, adInfo: LevelPlayAdInfo) {
        logCallbackName(TAG, "error = $error | adInfo = $adInfo")

    }

    /**
    Called after an rewarded ad has been clicked
    @param adInfo The info of the ad
     */
    override fun onAdClicked(adInfo: LevelPlayAdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
    }

    /**
    Called after an rewarded ad has been closed
    @param adInfo The info of the ad
     */
    override fun onAdClosed(adInfo: LevelPlayAdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
        listener.showRewardDialog()

    }

    /**
    Called after the ad info is updated. Available when another rewarded ad has loaded, and includes a higher CPM/Rate
    @param adInfo The info of the ad
     */
    override fun onAdInfoChanged(adInfo: LevelPlayAdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
    }

}