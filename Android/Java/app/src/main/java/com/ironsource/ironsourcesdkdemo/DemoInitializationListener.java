package com.ironsource.ironsourcesdkdemo;

import static com.ironsource.ironsourcesdkdemo.DemoActivity.logCallbackName;

import androidx.annotation.NonNull;
import com.unity3d.mediation.LevelPlayConfiguration;
import com.unity3d.mediation.LevelPlayInitError;
import com.unity3d.mediation.LevelPlayInitListener;

public class DemoInitializationListener implements LevelPlayInitListener {
    private final String TAG = DemoInitializationListener.class.getSimpleName();
    private final DemoActivityListener listener;

    public DemoInitializationListener(DemoActivityListener listener) {
        this.listener = listener;
    }


    /**
     triggered when the initialization is completed successfully. After you receive this indication, ads can be loaded
     @param configuration The configuration
     */
    @Override
    public void onInitSuccess(LevelPlayConfiguration configuration) {
        logCallbackName(TAG, "");
        this.listener.createInterstitialAd();
        this.listener.createBannerAd();
    }

    /**
      the configuration was not retrieved successfully and ads cannot be loaded. It is recommended to try and initialize the ironSource SDK later (when internet connection is available, or when the failure reason is resolved)
     @param error The reason for the error
     */
    @Override
    public void onInitFailed(@NonNull LevelPlayInitError error) {
        logCallbackName(TAG, "error = " + error);

    }

}
