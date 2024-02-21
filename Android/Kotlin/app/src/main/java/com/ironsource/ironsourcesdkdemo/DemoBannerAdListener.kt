package com.ironsource.ironsourcesdkdemo

import android.view.View
import com.ironsource.ironsourcesdkdemo.DemoActivity.Companion.logCallbackName
import com.ironsource.mediationsdk.adunit.adapter.utility.AdInfo
import com.ironsource.mediationsdk.logger.IronSourceError
import com.ironsource.mediationsdk.sdk.LevelPlayBannerListener

class DemoBannerAdListener(private val listener: DemoActivityListener) :
    LevelPlayBannerListener {

    private val TAG = DemoBannerAdListener::class.java.name

    /**
    Called after each banner ad has been successfully loaded, either a manual load or banner refresh
    @param adInfo The info of the ad.
     */
    override fun onAdLoaded(adInfo: AdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
        listener.setBannerViewVisibility(View.VISIBLE)
        listener.setEnablementForButton(DemoButtonIdentifiers.LOAD_BANNER_BUTTON_IDENTIFIER, false)
        listener.setEnablementForButton(DemoButtonIdentifiers.DESTROY_BANNER_BUTTON_IDENTIFIER, true)
    }

    /**
    Called after a banner has attempted to load an ad but failed.
    This delegate will be sent both for manual load and refreshed banner failures.
    @param ironSourceError The reason for the error.
     */
    override fun onAdLoadFailed(ironSourceError: IronSourceError) {
        logCallbackName(TAG, "error = $ironSourceError")
    }

    /**
    Called after a banner has been clicked.
    @param adInfo The info of the ad.
     */
    override fun onAdClicked(adInfo: AdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
    }

    /**
    Called when a user was taken out of the application context.
    @param adInfo The info of the ad.
     */
    override fun onAdLeftApplication(adInfo: AdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
    }

    /**
    Called when a banner presented a full screen content.
    @param adInfo The info of the ad.
     */
    override fun onAdScreenPresented(adInfo: AdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
    }

    /**
    Called after a full screen content has been dismissed.
    @param adInfo The info of the ad.
     */
    override fun onAdScreenDismissed(adInfo: AdInfo) {
        logCallbackName(TAG, "adInfo = $adInfo")
    }
}