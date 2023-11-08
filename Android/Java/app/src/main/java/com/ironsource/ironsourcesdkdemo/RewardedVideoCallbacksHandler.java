package com.ironsource.ironsourcesdkdemo;

import android.util.Log;

import com.ironsource.mediationsdk.IronSource;
import com.ironsource.mediationsdk.adunit.adapter.utility.AdInfo;
import com.ironsource.mediationsdk.logger.IronSourceError;
import com.ironsource.mediationsdk.model.Placement;
import com.ironsource.mediationsdk.sdk.LevelPlayRewardedVideoListener;

public class RewardedVideoCallbacksHandler implements LevelPlayRewardedVideoListener {

    private final String TAG = "RewardedVideoHandler";

    private final DemoActivity mDemoActivity;

    public RewardedVideoCallbacksHandler(DemoActivity demoActivity) {
            mDemoActivity = demoActivity;
        }
    public void setLevelPlayRewardedVideoListener() {
        IronSource.setLevelPlayRewardedVideoListener(this);
    }

    @Override
    public void onAdAvailable(AdInfo adInfo) {
        Log.d(TAG, "onAdAvailable(adInfo)");
       // mMainActivity.setRvAvailability(true);
    }

    @Override
    public void onAdUnavailable() {
        Log.d(TAG, "onAdUnavailable()");
        // mMainActivity.setRvAvailability(false);
    }

    @Override
    public void onAdOpened(AdInfo adInfo) {
        Log.d(TAG, "onAdOpened(adInfo)");
    }

    @Override
    public void onAdShowFailed(IronSourceError error, AdInfo adInfo) {
        Log.d(TAG, "onAdShowFailed(adInfo) error = " + error);
    }

    @Override
    public void onAdClicked(Placement placement, AdInfo adInfo) {
        Log.d(TAG, "onAdClicked(adInfo)");
    }

    @Override
    public void onAdRewarded(Placement placement, AdInfo adInfo) {
        Log.d(TAG, "onAdRewarded(adInfo)");
        // mMainActivity.notifyRewardCredits(placement);
    }

    @Override
    public void onAdClosed(AdInfo adInfo) {
        Log.d(TAG, "onAdClosed(adInfo)");
    }

}
