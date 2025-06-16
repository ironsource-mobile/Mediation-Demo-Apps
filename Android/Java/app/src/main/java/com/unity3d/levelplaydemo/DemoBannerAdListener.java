package com.unity3d.levelplaydemo;


import static com.unity3d.levelplaydemo.DemoActivity.logCallbackName;
import android.view.View;
import androidx.annotation.NonNull;
import com.unity3d.mediation.LevelPlayAdError;
import com.unity3d.mediation.LevelPlayAdInfo;
import com.unity3d.mediation.banner.LevelPlayBannerAdViewListener;

public class DemoBannerAdListener implements LevelPlayBannerAdViewListener {
    private final String TAG = DemoBannerAdListener.class.getSimpleName();
    private final DemoActivityListener listener;

    public DemoBannerAdListener(DemoActivityListener listener) {
        this.listener = listener;
    }

    /**
     Called after each banner ad has been successfully loaded, either a manual load or banner refresh
     @param adInfo The info of the ad
     */
    @Override
    public void onAdLoaded(@NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
        this.listener.setBannerViewVisibility(View.VISIBLE);
        this.listener.setEnablementForButton(DemoButtonIdentifiers.LOAD_BANNER_BUTTON_IDENTIFIER, false);
    }

    /**
     Called after a banner has attempted to load an ad but failed
     This delegate will be sent both for manual load and refreshed banner failures
     @param error The reason for the error
     */
    @Override
    public void onAdLoadFailed(@NonNull LevelPlayAdError error) {
        logCallbackName(TAG, "error = " + error);
    }

    /**
     Called after a banner was displayed and visible on screen
     @param adInfo The info of the ad
     */
    @Override
    public void onAdDisplayed(@NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
    }

    /**
     Called after a banner failed to be displayed on screen
     @param adInfo The info of the ad
     @param error The reason for the error
     */

    @Override
    public void onAdDisplayFailed(@NonNull LevelPlayAdInfo adInfo, @NonNull LevelPlayAdError error) {
        logCallbackName(TAG, "error = " + error + " | adInfo = " + adInfo);

    }

    /**
     Called after a banner has been clicked
     @param adInfo The info of the ad
     */
    @Override
    public void onAdClicked(@NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
    }

    /**
     Called when a banner opened on full screen
     @param adInfo The info of the ad
     */
    @Override
    public void onAdExpanded(@NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
    }

    /**
     Called after a banner is restored to its original size
     @param adInfo The info of the ad
     */
    @Override
    public void onAdCollapsed(@NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
    }

    /**
     Called when a user pressed on the ad and was navigated out of the app
     @param adInfo The info of the ad
     */
    @Override
    public void onAdLeftApplication(@NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
    }
}
