package com.ironsource.ironsourcesdkdemo;

import static com.ironsource.ironsourcesdkdemo.DemoActivity.logCallbackName;

import android.view.View;
import com.ironsource.mediationsdk.adunit.adapter.utility.AdInfo;
import com.ironsource.mediationsdk.logger.IronSourceError;
import com.ironsource.mediationsdk.sdk.LevelPlayBannerListener;

public class DemoBannerAdListener implements LevelPlayBannerListener {
    private final String TAG = DemoBannerAdListener.class.getSimpleName();
    private final DemoActivityListener listener;

    public DemoBannerAdListener(DemoActivityListener listener) {
        this.listener = listener;
    }

    /**
     Called after each banner ad has been successfully loaded, either a manual load or banner refresh
     @param adInfo The info of the ad.
     */
    @Override
    public void onAdLoaded(AdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
        this.listener.setBannerViewVisibility(View.VISIBLE);
        this.listener.setEnablementForButton(DemoButtonIdentifiers.LOAD_BANNER_BUTTON_IDENTIFIER, false);
        this.listener.setEnablementForButton(DemoButtonIdentifiers.DESTROY_BANNER_BUTTON_IDENTIFIER, true);
    }

    /**
     Called after a banner has attempted to load an ad but failed.
     This delegate will be sent both for manual load and refreshed banner failures.
     @param ironSourceError The reason for the error.
     */
    @Override
    public void onAdLoadFailed(IronSourceError ironSourceError) {
        logCallbackName(TAG, "error = " + ironSourceError);
    }

    /**
     Called after a banner has been clicked.
     @param adInfo The info of the ad.
     */
    @Override
    public void onAdClicked(AdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
    }

    /**
     Called when a user was taken out of the application context.
     @param adInfo The info of the ad.
     */
    @Override
    public void onAdLeftApplication(AdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
    }

    /**
     Called when a banner presented a full screen content.
     @param adInfo The info of the ad.
     */
    @Override
    public void onAdScreenPresented(AdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
    }

    /**
     Called after a full screen content has been dismissed.
     @param adInfo The info of the ad.
     */
    @Override
    public void onAdScreenDismissed(AdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
    }
}
