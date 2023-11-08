package com.ironsource.ironsourcesdkdemo;

import android.util.Log;

import com.ironsource.mediationsdk.IronSourceBannerLayout;
import com.ironsource.mediationsdk.adunit.adapter.utility.AdInfo;
import com.ironsource.mediationsdk.logger.IronSourceError;
import com.ironsource.mediationsdk.sdk.LevelPlayBannerListener;

public class BannerCallbacksHandler implements LevelPlayBannerListener {

    private final String TAG = "BannerHandler";

    private final DemoActivity mDemoActivity;

    public BannerCallbacksHandler(DemoActivity demoActivity) {
        mDemoActivity = demoActivity;
    }

    public void setLevelPlayBannerListener(IronSourceBannerLayout banner) {
        banner.setLevelPlayBannerListener(this);
    }

    @Override
    public void onAdLoaded(AdInfo adInfo) {
        Log.d(TAG, "onAdLoaded(adInfo)");
        mDemoActivity.onBannerAdLoaded();
    }

    @Override
    public void onAdLoadFailed(IronSourceError error) {
        Log.d(TAG, "onAdLoadFailed(" + error + ")");
    }

    @Override
    public void onAdClicked(AdInfo adInfo) {
        Log.d(TAG, "onAdClicked(adInfo)");
    }

    @Override
    public void onAdLeftApplication(AdInfo adInfo) {
        Log.d(TAG, "onAdLeftApplication(adInfo)");
    }

    @Override
    public void onAdScreenPresented(AdInfo adInfo) {
        Log.d(TAG, "onAdScreenPresented(adInfo)");
    }

    @Override
    public void onAdScreenDismissed(AdInfo adInfo) {
        Log.d(TAG, "onAdScreenDismissed(adInfo)");
    }
}
