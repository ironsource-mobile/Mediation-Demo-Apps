package com.ironsource.ironsourcesdkdemo

import com.ironsource.ironsourcesdkdemo.DemoActivity.Companion.logCallbackName
import com.ironsource.mediationsdk.adunit.adapter.utility.AdInfo
import com.ironsource.mediationsdk.logger.IronSourceError
import com.ironsource.mediationsdk.sdk.LevelPlayInterstitialListener

class DemoInterstitialAdListener(private val listener: DemoActivityListener) :
        LevelPlayInterstitialListener {

    private val TAG = DemoInterstitialAdListener::class.java.name


    /**
    Called after an interstitial has been loaded
    @param adInfo The info of the ad.
     */
    override fun onAdReady(adInfo: AdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
        listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_INTERSTITIAL_BUTTON_IDENTIFIER, true)
    }

    /**
    Called after an interstitial has attempted to load but failed.
    @param ironSourceError The reason for the error
     */
    override fun onAdLoadFailed(ironSourceError: IronSourceError) {
        logCallbackName(TAG, "error = $ironSourceError")
        listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_INTERSTITIAL_BUTTON_IDENTIFIER, false)
    }

    /**
    Called after an interstitial has been opened.
    This is the indication for impression.
    @param adInfo The info of the ad.
     */
    override fun onAdOpened(adInfo: AdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
        listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_INTERSTITIAL_BUTTON_IDENTIFIER, false)
    }

    /**
    Called after an interstitial has been displayed on the screen.
    This callback is not supported by all networks, and we recommend using it
    only if it's supported by all networks you included in your build.
    @param adInfo The info of the ad.
     */
    override fun onAdShowSucceeded(adInfo: AdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
    }

    /**
    Called after an interstitial has attempted to show but failed.
    @param ironSourceError The reason for the error.
    @param adInfo The info of the ad.
     */
    override fun onAdShowFailed(ironSourceError: IronSourceError, adInfo: AdInfo) {
        logCallbackName(TAG, "error = $ironSourceError | adInfo = $adInfo")
    }

    /**
    Called after an interstitial has been clicked.
    @param adInfo The info of the ad.
     */
    override fun onAdClicked(adInfo: AdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
    }

    /**
    Called after an interstitial has been dismissed.
    @param adInfo The info of the ad.
     */
    override fun onAdClosed(adInfo: AdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
    }
}