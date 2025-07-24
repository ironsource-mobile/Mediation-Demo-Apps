package com.unity3d.levelplaydemo;

import static com.unity3d.levelplaydemo.DemoActivity.logCallbackName;

import androidx.annotation.NonNull;
import com.unity3d.mediation.LevelPlayAdError;
import com.unity3d.mediation.LevelPlayAdInfo;
import com.unity3d.mediation.rewarded.LevelPlayReward;
import com.unity3d.mediation.rewarded.LevelPlayRewardedAdListener;

public class DemoRewardedVideoAdListener implements LevelPlayRewardedAdListener {
    private final String TAG = DemoRewardedVideoAdListener.class.getSimpleName();
    private final DemoActivityListener listener;

    public DemoRewardedVideoAdListener(DemoActivityListener listener) {
        this.listener = listener;
    }

    /**
     Called after an rewarded ad has been loaded
     @param adInfo The info of the ad
     */
    @Override
    public void onAdLoaded(@NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
        this.listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_REWARDED_VIDEO_BUTTON_IDENTIFIER, true);
    }

    /**
     Called after an rewarded ad has attempted to load but failed
     @param error The reason for the error
     */
    @Override
    public void onAdLoadFailed(@NonNull LevelPlayAdError error) {
        logCallbackName(TAG, "error = " + error);
        this.listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_REWARDED_VIDEO_BUTTON_IDENTIFIER, false);
    }

    /**
     Called after a rewarded video has been viewed completely and the user is eligible for a reward.
     @param levelPlayReward The reward that the user is eligible for.
     @param adInfo The info of the ad.
     */
    @Override
    public void onAdRewarded(@NonNull LevelPlayReward levelPlayReward, @NonNull LevelPlayAdInfo adInfo) {
        this.listener.setReward(levelPlayReward);
        logCallbackName(TAG, "levelPlayReward = " + levelPlayReward + " | adInfo = " + adInfo);
    }

    /**
     Called after an rewarded ad has been displayed
     This is the indication for impression
     @param adInfo The info of the ad
     **/
    @Override
    public void onAdDisplayed(@NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "levelPlayAdInfo = " + adInfo);
        this.listener.setEnablementForButton(DemoButtonIdentifiers.SHOW_REWARDED_VIDEO_BUTTON_IDENTIFIER, false);
    }

    /**
     Called after an rewarded ad has attempted to display but failed
     @param error The reason for the error
     @param adInfo The info of the ad
     */
    @Override
    public void onAdDisplayFailed(@NonNull LevelPlayAdError error, @NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "levelPlayAdError = " + error + " | adInfo = " + adInfo);
    }

    /**
     Called after an rewarded ad has been clicked
     @param adInfo The info of the ad
     */
    @Override
    public void onAdClicked(@NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
    }

    /**
     Called after an rewarded ad has been closed
     @param adInfo The info of the ad
     */
    @Override
    public void onAdClosed(@NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
        this.listener.showRewardDialog();
    }

    /**
     Called after the ad info is updated. Available when another rewarded ad has loaded, and includes a higher CPM/Rate
     @param adInfo The info of the ad
     */
    @Override
    public void onAdInfoChanged(@NonNull LevelPlayAdInfo adInfo) {
        logCallbackName(TAG, "adInfo = " + adInfo);
    }
}
