package com.ironsource.ironsourcesdkdemo;

import android.util.Log;

import com.ironsource.mediationsdk.IronSource;
import com.ironsource.mediationsdk.adunit.adapter.utility.AdInfo;
import com.ironsource.mediationsdk.logger.IronSourceError;
import com.ironsource.mediationsdk.sdk.LevelPlayInterstitialListener;

public class InterstitialCallbacksHandler implements LevelPlayInterstitialListener {
    private final String TAG = "InterstitialHandler";

    private final DemoActivity mDemoActivity;

    public InterstitialCallbacksHandler(DemoActivity demoActivity) {
        mDemoActivity = demoActivity;
    }

    public void setLevelPlayInterstitialListener() {
        IronSource.setLevelPlayInterstitialListener(this);
    }
    @Override
    public void onAdReady(AdInfo adInfo) {
        Log.d(TAG, "onAdReady(adInfo)");

//        if(mMainActivity.getIsLoadAndShowClicked()) {
//            mMainActivity.setIsLoadAndShowflag(false);
//            mMainActivity.showInterstitial(mMainActivity);
//            return;
//        }
//
//        mMainActivity.setIsAvailability(true);
    }

    @Override
    public void onAdLoadFailed(IronSourceError error) {
        Log.d(TAG, "onAdLoadFailed(" + error + ")");
//        mMainActivity.setIsAvailability(false);
    }

    @Override
    public void onAdOpened(AdInfo adInfo) {
        Log.d(TAG, "onAdOpened(adInfo)");
//        mMainActivity.setIsAvailability(false);
    }

    @Override
    public void onAdShowSucceeded(AdInfo adInfo) {
        Log.d(TAG, "onAdShowSucceeded(adInfo)");
    }

    @Override
    public void onAdShowFailed(IronSourceError error, AdInfo adInfo) {
        Log.d(TAG, "onAdShowFailed(adInfo) error = " + error);
//        mMainActivity.setIsAvailability(false);
    }

    @Override
    public void onAdClicked(AdInfo adInfo) {
        Log.d(TAG, "onAdClicked(adInfo)");
    }

    @Override
    public void onAdClosed(AdInfo adInfo) {
        Log.d(TAG, "onAdClosed(adInfo)");
    }
}
