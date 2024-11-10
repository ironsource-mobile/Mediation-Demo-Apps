package com.ironsource.ironsourcesdkdemo

import com.ironsource.ironsourcesdkdemo.DemoActivity.Companion.logCallbackName
import com.unity3d.mediation.LevelPlayAdError
import com.unity3d.mediation.LevelPlayAdInfo
import com.unity3d.mediation.interstitial.LevelPlayInterstitialAdListener

class DemoInterstitialAdListener(private val listener: DemoActivityListener) :
    LevelPlayInterstitialAdListener {

    private val TAG = DemoInterstitialAdListener::class.java.name


    /**
    Called after an interstitial ad has been loaded
    @param adInfo The info of the ad
     */
    override fun onAdLoaded(adInfo: LevelPlayAdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
        listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_INTERSTITIAL_AD_BUTTON_IDENTIFIER, true)
    }

    /**
    Called after an interstitial ad has attempted to load but failed
    @param error The reason for the error
     */
    override fun onAdLoadFailed(error: LevelPlayAdError) {
        logCallbackName(TAG, "error = $error")
        listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_INTERSTITIAL_AD_BUTTON_IDENTIFIER, false)
    }


    /**
    Called after an interstitial ad has been displayed
    This is the indication for impression
    @param adInfo The info of the ad
     */
    override fun onAdDisplayed(adInfo: LevelPlayAdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
        listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_INTERSTITIAL_AD_BUTTON_IDENTIFIER, false)
    }

    /**
    Called after an interstitial ad has attempted to display but failed
    @param error The reason for the error
    @param adInfo The info of the ad
     */
    override fun onAdDisplayFailed(error: LevelPlayAdError, adInfo: LevelPlayAdInfo) {
        logCallbackName(TAG, "error = $error | adInfo = $adInfo")

    }

    /**
    Called after an interstitial ad has been clicked
    @param adInfo The info of the ad
     */
    override fun onAdClicked(adInfo: LevelPlayAdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
    }

    /**
    Called after an interstitial ad has been closed
    @param adInfo The info of the ad
     */
    override fun onAdClosed(adInfo: LevelPlayAdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
    }

    /**
    Called after the ad info is updated. Available when another interstitial ad has loaded, and includes a higher CPM/Rate
    @param adInfo The info of the ad
     */
    override fun onAdInfoChanged(adInfo: LevelPlayAdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
    }

}