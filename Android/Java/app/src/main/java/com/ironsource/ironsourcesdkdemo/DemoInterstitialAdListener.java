package com.ironsource.ironsourcesdkdemo;

import static com.ironsource.ironsourcesdkdemo.DemoActivity.logCallbackName;

import com.ironsource.mediationsdk.adunit.adapter.utility.AdInfo;
import com.ironsource.mediationsdk.logger.IronSourceError;
import com.ironsource.mediationsdk.sdk.LevelPlayInterstitialListener;

public class DemoInterstitialAdListener implements LevelPlayInterstitialListener {
    private final String TAG = DemoInterstitialAdListener.class.getSimpleName();

    private final DemoActivityListener listener;

    public DemoInterstitialAdListener(DemoActivityListener listener) {
        this.listener = listener;
    }

    /**
     Called after an interstitial has been loaded
     @param adInfo The info of the ad.
     */
    @Override
    public void onAdReady(AdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
        this.listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_INTERSTITIAL_BUTTON_IDENTIFIER, true);
    }

    /**
     Called after an interstitial has attempted to load but failed.
     @param ironSourceError The reason for the error
     */
    @Override
    public void onAdLoadFailed(IronSourceError ironSourceError) {
        logCallbackName(TAG, "error = " + ironSourceError);
        this.listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_INTERSTITIAL_BUTTON_IDENTIFIER, false);
    }

    /**
     Called after an interstitial has been opened.
     This is the indication for impression.
     @param adInfo The info of the ad.
     */
    @Override
    public void onAdOpened(AdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
        this.listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_INTERSTITIAL_BUTTON_IDENTIFIER, false);
    }

    /**
     Called after an interstitial has been displayed on the screen.
     This callback is not supported by all networks, and we recommend using it
     only if it's supported by all networks you included in your build.
     @param adInfo The info of the ad.
     */
    @Override
    public void onAdShowSucceeded(AdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
    }

    /**
     Called after an interstitial has attempted to show but failed.
     @param ironSourceError The reason for the error.
     @param adInfo The info of the ad.
     */
    @Override
    public void onAdShowFailed(IronSourceError ironSourceError, AdInfo adInfo) {
        logCallbackName(TAG, "error = " + ironSourceError + " | adInfo = " + adInfo);
    }

    /**
     Called after an interstitial has been clicked.
     @param adInfo The info of the ad.
     */
    @Override
    public void onAdClicked(AdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
    }

    /**
     Called after an interstitial has been dismissed.
     @param adInfo The info of the ad.
     */
    @Override
    public void onAdClosed(AdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
    }
}
