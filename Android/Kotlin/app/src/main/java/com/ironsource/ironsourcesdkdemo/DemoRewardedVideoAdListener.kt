package com.ironsource.ironsourcesdkdemo

import com.ironsource.ironsourcesdkdemo.DemoActivity.Companion.logCallbackName
import com.ironsource.mediationsdk.adunit.adapter.utility.AdInfo
import com.ironsource.mediationsdk.logger.IronSourceError
import com.ironsource.mediationsdk.model.Placement
import com.ironsource.mediationsdk.sdk.LevelPlayRewardedVideoListener

class DemoRewardedVideoAdListener(private val listener: DemoActivityListener) : LevelPlayRewardedVideoListener {

    private val TAG = DemoRewardedVideoAdListener::class.java.name

    /**
    Called after a rewarded video has changed its availability to true.
    @param adInfo The info of the ad.
     */
    override fun onAdAvailable(adInfo: AdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
        listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_REWARDED_VIDEO_BUTTON_IDENTIFIER, true)
    }

    /**
    Called after a rewarded video has changed its availability to false.
     */
    override fun onAdUnavailable() {
        logCallbackName(TAG, "")
        listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_REWARDED_VIDEO_BUTTON_IDENTIFIER, false)
    }

    /**
    Called after a rewarded video has been opened.
    @param adInfo The info of the ad.
     */
    override fun onAdOpened(adInfo: AdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
        listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_REWARDED_VIDEO_BUTTON_IDENTIFIER, false)
    }

    /**
    Called after a rewarded video has attempted to show but failed.
    @param ironSourceError The reason for the error.
    @param adInfo The info of the ad.
     */
    override fun onAdShowFailed(ironSourceError: IronSourceError, adInfo: AdInfo) {
        logCallbackName(TAG, "error = $ironSourceError | adInfo = $adInfo")
    }

    /**
    Called after a rewarded video has been clicked.
    This callback is not supported by all networks, and we recommend using it
    only if it's supported by all networks you included in your build
    @param placement An object that contains the placement's reward name and amount.
    @param adInfo The info of the ad.
     */
    override fun onAdClicked(placement: Placement?, adInfo: AdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
    }

    /**
    Called after a rewarded video has been viewed completely and the user is eligible for a reward.
    @param placement An object that contains the placement's reward name and amount.
    @param adInfo The info of the ad.
     */
    override fun onAdRewarded(placement: Placement, adInfo: AdInfo) {
        logCallbackName(TAG, "placement = $placement | adInfo = $adInfo")
        listener.setPlacementInfo(placement)
    }

    /**
    Called after a rewarded video has been dismissed.
    @param adInfo The info of the ad.
     */
    override fun onAdClosed(adInfo: AdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
        listener.showRewardDialog()
    }
}