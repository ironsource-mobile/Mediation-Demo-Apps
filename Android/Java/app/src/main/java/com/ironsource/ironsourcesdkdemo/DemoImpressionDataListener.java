package com.ironsource.ironsourcesdkdemo;

import static com.ironsource.ironsourcesdkdemo.DemoActivity.logCallbackName;

import com.ironsource.mediationsdk.impressionData.ImpressionData;
import com.ironsource.mediationsdk.impressionData.ImpressionDataListener;

public class DemoImpressionDataListener implements ImpressionDataListener {
    private final String TAG = DemoImpressionDataListener.class.getSimpleName();

    /**
     Called when the ad was displayed successfully and the impression data was recorded
     @param impressionData The recorded impression data
     */
    @Override
    public void onImpressionSuccess(ImpressionData impressionData) {
        if (impressionData != null) {
            logCallbackName(TAG, "");
        }
    }
}
