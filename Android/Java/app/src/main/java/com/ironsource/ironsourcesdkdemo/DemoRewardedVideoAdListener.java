package com.ironsource.ironsourcesdkdemo;

import static com.ironsource.ironsourcesdkdemo.DemoActivity.logCallbackName;

import com.ironsource.mediationsdk.adunit.adapter.utility.AdInfo;
import com.ironsource.mediationsdk.logger.IronSourceError;
import com.ironsource.mediationsdk.model.Placement;
import com.ironsource.mediationsdk.sdk.LevelPlayRewardedVideoListener;

public class DemoRewardedVideoAdListener implements LevelPlayRewardedVideoListener {
    private final String TAG = DemoRewardedVideoAdListener.class.getSimpleName();
    private final DemoActivityListener listener;

    public DemoRewardedVideoAdListener(DemoActivityListener listener) {
        this.listener = listener;
    }

    /**
     Called after a rewarded video has changed its availability to true.
     @param adInfo The info of the ad.
     */
    @Override
    public void onAdAvailable(AdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
        this.listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_REWARDED_VIDEO_BUTTON_IDENTIFIER, true);
    }

    /**
     Called after a rewarded video has changed its availability to false.
     */
    @Override
    public void onAdUnavailable() {
        logCallbackName(TAG, "");
        this.listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_REWARDED_VIDEO_BUTTON_IDENTIFIER, false);
    }

    /**
     Called after a rewarded video has been opened.
     @param adInfo The info of the ad.
     */
    @Override
    public void onAdOpened(AdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
        this.listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_REWARDED_VIDEO_BUTTON_IDENTIFIER, false);
    }

    /**
     Called after a rewarded video has attempted to show but failed.
     @param ironSourceError The reason for the error.
     @param adInfo The info of the ad.
     */
    @Override
    public void onAdShowFailed(IronSourceError ironSourceError, AdInfo adInfo) {
        logCallbackName(TAG, "error = " + ironSourceError + " | adInfo = " + adInfo);
    }

    /**
     Called after a rewarded video has been clicked.
     This callback is not supported by all networks, and we recommend using it
     only if it's supported by all networks you included in your build
     @param placement An object that contains the placement's reward name and amount.
     @param adInfo The info of the ad.
     */
    @Override
    public void onAdClicked(Placement placement, AdInfo adInfo) {
        logCallbackName(TAG, "placement = " + placement + " | adInfo = " + adInfo);
    }

    /**
     Called after a rewarded video has been viewed completely and the user is eligible for a reward.
     @param placement An object that contains the placement's reward name and amount.
     @param adInfo The info of the ad.
     */
    @Override
    public void onAdRewarded(Placement placement, AdInfo adInfo) {
        logCallbackName(TAG, "placement = " + placement + " | adInfo = " + adInfo);
        listener.setPlacementInfo(placement);
    }

    /**
     Called after a rewarded video has been dismissed.
     @param adInfo The info of the ad.
     */
    @Override
    public void onAdClosed(AdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
        this.listener.showRewardDialog();
    }
}
