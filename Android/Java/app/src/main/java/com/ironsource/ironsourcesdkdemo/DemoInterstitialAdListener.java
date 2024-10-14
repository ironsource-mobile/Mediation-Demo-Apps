package com.ironsource.ironsourcesdkdemo;

import static com.ironsource.ironsourcesdkdemo.DemoActivity.logCallbackName;
import androidx.annotation.NonNull;
import com.unity3d.mediation.LevelPlayAdError;
import com.unity3d.mediation.LevelPlayAdInfo;
import com.unity3d.mediation.interstitial.LevelPlayInterstitialAdListener;


public class DemoInterstitialAdListener implements LevelPlayInterstitialAdListener {
    private final String TAG = DemoInterstitialAdListener.class.getSimpleName();

    private final DemoActivityListener listener;

    public DemoInterstitialAdListener(DemoActivityListener listener) {
        this.listener = listener;
    }

    /**
     Called after an interstitial ad has been loaded
     @param adInfo The info of the ad
     */
    @Override
    public void onAdLoaded(@NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
        this.listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_INTERSTITIAL_BUTTON_IDENTIFIER, true);

    }

    /**
     Called after an interstitial ad has attempted to load but failed
     @param error The reason for the error
     */
    @Override
    public void onAdLoadFailed(@NonNull LevelPlayAdError error) {
        logCallbackName(TAG, "error = " + error);
        this.listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_INTERSTITIAL_BUTTON_IDENTIFIER, false);
    }

    /**
     Called after an interstitial ad has been displayed
     This is the indication for impression
     @param adInfo The info of the ad
     */
    @Override
    public void onAdDisplayed(@NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
        this.listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_INTERSTITIAL_BUTTON_IDENTIFIER, false);
    }

    /**
     Called after an interstitial ad has attempted to display but failed
     @param error The reason for the error
     @param adInfo The info of the ad
     */
    @Override
    public void onAdDisplayFailed(@NonNull LevelPlayAdError error, @NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "error = " + error + " | adInfo = " + adInfo);

    }

    /**
     Called after an interstitial ad has been closed
     @param adInfo The info of the ad
     */
    @Override public void onAdClosed(@NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
    }

    /**
     Called after an interstitial ad has been clicked
     @param adInfo The info of the ad
     */
    @Override public void onAdClicked(@NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);

    }

    /**
     Called after the ad info is updated. Available when another interstitial ad has loaded, and includes a higher CPM/Rate
     @param adInfo The info of the ad
     */
    @Override
    public void onAdInfoChanged(@NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
    }

}
