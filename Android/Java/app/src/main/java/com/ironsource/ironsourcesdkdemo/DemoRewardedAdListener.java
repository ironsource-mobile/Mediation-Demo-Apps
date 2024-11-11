package com.ironsource.ironsourcesdkdemo;

import static com.ironsource.ironsourcesdkdemo.DemoActivity.logCallbackName;

import androidx.annotation.NonNull;
import com.unity3d.mediation.LevelPlayAdError;
import com.unity3d.mediation.LevelPlayAdInfo;
import com.unity3d.mediation.rewarded.LevelPlayReward;
import com.unity3d.mediation.rewarded.LevelPlayRewardedAdListener;

public class DemoRewardedAdListener implements LevelPlayRewardedAdListener {
    private final String TAG = DemoRewardedAdListener.class.getSimpleName();
    private final DemoActivityListener listener;

    public DemoRewardedAdListener(DemoActivityListener listener) {
        this.listener = listener;
    }

    /**
     Called after a rewarded ad has been loaded
     @param adInfo The info of the ad
     */
    @Override
    public void onAdLoaded(@NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
        this.listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_REWARDED_AD_BUTTON_IDENTIFIER, true);
    }

    /**
     Called after a rewarded ad has attempted to load but failed
     @param error The reason for the error
     */
    @Override
    public void onAdLoadFailed(@NonNull LevelPlayAdError error) {
        logCallbackName(TAG, "error = " + error);
        this.listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_REWARDED_AD_BUTTON_IDENTIFIER, false);
    }

    /**
     Called after the ad info is updated. Available when another rewarded ad has loaded, and includes a higher CPM/Rate
     @param adInfo The info of the ad
     */
    @Override
    public void onAdInfoChanged(@NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
    }

    /**
     Called after a rewarded ad has been displayed
     This is the indication for impression
     @param adInfo The info of the ad
     */
    @Override
    public void onAdDisplayed(@NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
        this.listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_REWARDED_AD_BUTTON_IDENTIFIER, false);
    }

    /**
     Called after a rewarded ad has attempted to display but failed.
     @param error The reason for the error.
     @param adInfo The info of the ad.
     */
    @Override
    public void onAdDisplayFailed(@NonNull LevelPlayAdError error, @NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "error = " + error + " | adInfo = " + adInfo);
    }

    /**
     Called after a rewarded video has been viewed completely and the user is eligible for a reward.
     @param reward The reward info.
     @param adInfo The info of the ad.
     */
    @Override
    public void onAdRewarded(@NonNull LevelPlayReward reward, @NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "reward = " + reward + " | adInfo = " + adInfo);
        this.listener.setReward(reward);
    }

    /**
     Called after a rewarded ad has been clicked.
     @param adInfo The info of the ad.
     */
    @Override
    public void onAdClicked(@NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
    }

    /**
     Called after a rewarded ad has been closed.
     @param adInfo The info of the ad.
     */
    @Override
    public void onAdClosed(@NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
        this.listener.showRewardDialog();
    }
}
