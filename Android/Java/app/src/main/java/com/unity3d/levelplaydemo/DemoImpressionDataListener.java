package com.unity3d.levelplaydemo;

import static com.unity3d.levelplaydemo.DemoActivity.logCallbackName;

import androidx.annotation.NonNull;

import com.unity3d.mediation.impression.LevelPlayImpressionData;
import com.unity3d.mediation.impression.LevelPlayImpressionDataListener;

public class DemoImpressionDataListener implements LevelPlayImpressionDataListener {
    private final String TAG = DemoImpressionDataListener.class.getSimpleName();

    /**
     Called when the ad was displayed successfully and the impression data was recorded
     @param levelPlayImpressionData The recorded impression data
     */
    @Override
    public void onImpressionSuccess(@NonNull LevelPlayImpressionData levelPlayImpressionData) {
        logCallbackName(TAG, "");
    }
}
