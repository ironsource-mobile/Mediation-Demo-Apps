package com.levelplay.levelplaysdkdemo

import android.view.View
import com.levelplay.levelplaysdkdemo.DemoActivity.Companion.logCallbackName
import com.unity3d.mediation.LevelPlayAdError
import com.unity3d.mediation.LevelPlayAdInfo
import com.unity3d.mediation.banner.LevelPlayBannerAdViewListener

class DemoBannerAdListener(private val listener: DemoActivityListener) :
    LevelPlayBannerAdViewListener {

    private val TAG = DemoBannerAdListener::class.java.name

    /**
    Called after each banner ad has been successfully loaded, either a manual load or banner refresh
    @param adInfo The info of the ad
     */
    override fun onAdLoaded(adInfo: LevelPlayAdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
        listener.setBannerViewVisibility(View.VISIBLE)
        listener.setEnablementForButton(DemoButtonIdentifiers.LOAD_BANNER_BUTTON_IDENTIFIER, false)
        listener.setEnablementForButton(DemoButtonIdentifiers.DESTROY_BANNER_BUTTON_IDENTIFIER, true)
    }

    /**
    Called after a banner has attempted to load an ad but failed
    This delegate will be sent both for manual load and refreshed banner failures
    @param error The reason for the error
     */
    override fun onAdLoadFailed(error: LevelPlayAdError) {
        logCallbackName(TAG, "error = $error")
    }

    /**
    Called after a banner was displayed and visible on screen
    @param adInfo The info of the ad
     */
    override fun onAdDisplayed(adInfo: LevelPlayAdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
    }

    /**
    Called after a banner failed to be displayed on screen
    @param adInfo The info of the ad
    @param error The reason for the error
     */
    override fun onAdDisplayFailed(adInfo: LevelPlayAdInfo, error: LevelPlayAdError) {
        logCallbackName(TAG, "error = $error | adInfo = $adInfo")
    }

    /**
    Called after a banner has been clicked
    @param adInfo The info of the ad
     */
    override fun onAdClicked(adInfo: LevelPlayAdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
    }

    /**
    Called when a banner opened on full screen
    @param adInfo The info of the ad
     */
    override fun onAdExpanded(adInfo: LevelPlayAdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
    }

    /**
    Called after a banner is restored to its original size
    @param adInfo The info of the ad
     */
    override fun onAdCollapsed(adInfo: LevelPlayAdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
    }

    /**
    Called when a user pressed on the ad and was navigated out of the app
    @param adInfo The info of the ad
     */
    override fun onAdLeftApplication(adInfo: LevelPlayAdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
    }
}