package com.unity3d.levelplaydemo;

import android.view.WindowManager;

import androidx.annotation.NonNull;
import androidx.test.ext.junit.rules.ActivityScenarioRule;
import androidx.test.ext.junit.runners.AndroidJUnit4;

import com.unity3d.mediation.LevelPlay;
import com.unity3d.mediation.LevelPlayAdError;
import com.unity3d.mediation.LevelPlayAdInfo;
import com.unity3d.mediation.LevelPlayConfiguration;
import com.unity3d.mediation.LevelPlayInitError;
import com.unity3d.mediation.LevelPlayInitListener;
import com.unity3d.mediation.LevelPlayInitRequest;
import com.unity3d.mediation.impression.LevelPlayImpressionData;
import com.unity3d.mediation.impression.LevelPlayImpressionDataListener;
import com.unity3d.mediation.rewarded.LevelPlayReward;
import com.unity3d.mediation.rewarded.LevelPlayRewardedAd;
import com.unity3d.mediation.rewarded.LevelPlayRewardedAdListener;

import org.junit.Rule;
import org.junit.Test;
import org.junit.runner.RunWith;

import static org.junit.Assert.assertTrue;
import static org.junit.Assert.fail;

import java.util.concurrent.CountDownLatch;
import java.util.concurrent.TimeUnit;

@RunWith(AndroidJUnit4.class)
public class RewardedIntegrationTest {

    private static final String APP_KEY = DemoActivity.APP_KEY;
    private static final String REWARDED_AD_UNIT_ID = DemoActivity.REWARDED_VIDEO_AD_UNIT_ID;

    @Rule
    public ActivityScenarioRule<DemoActivity> activityRule = new ActivityScenarioRule<>(DemoActivity.class);

    @Test
    public void testInitLoadAndImpression() throws InterruptedException {
        final CountDownLatch initLatch = new CountDownLatch(1);
        final CountDownLatch loadLatch = new CountDownLatch(1);
        final CountDownLatch impressionLatch = new CountDownLatch(1);
        final LevelPlayRewardedAd[] rewardedAdHolder = new LevelPlayRewardedAd[1];

        activityRule.getScenario().onActivity(activity ->
                activity.getWindow().addFlags(
                        WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON
                                | WindowManager.LayoutParams.FLAG_TURN_SCREEN_ON
                                | WindowManager.LayoutParams.FLAG_SHOW_WHEN_LOCKED
                                | WindowManager.LayoutParams.FLAG_DISMISS_KEYGUARD
                )
        );

        LevelPlay.addImpressionDataListener(new LevelPlayImpressionDataListener() {
            @Override
            public void onImpressionSuccess(@NonNull LevelPlayImpressionData impressionData) {
                impressionLatch.countDown();
            }
        });

        activityRule.getScenario().onActivity(activity -> {
            LevelPlayInitRequest request = new LevelPlayInitRequest.Builder(APP_KEY).build();
            LevelPlay.init(activity, request, new LevelPlayInitListener() {
                @Override
                public void onInitSuccess(@NonNull LevelPlayConfiguration configuration) {
                    initLatch.countDown();

                    rewardedAdHolder[0] = new LevelPlayRewardedAd(REWARDED_AD_UNIT_ID);
                    rewardedAdHolder[0].setListener(new LevelPlayRewardedAdListener() {
                        @Override
                        public void onAdLoaded(@NonNull LevelPlayAdInfo adInfo) {
                            loadLatch.countDown();
                        }

                        @Override
                        public void onAdLoadFailed(@NonNull LevelPlayAdError error) {
                            fail("Ad load failed: " + error);
                        }

                        @Override
                        public void onAdDisplayed(@NonNull LevelPlayAdInfo adInfo) {}

                        @Override
                        public void onAdDisplayFailed(@NonNull LevelPlayAdError error, @NonNull LevelPlayAdInfo adInfo) {}

                        @Override
                        public void onAdClicked(@NonNull LevelPlayAdInfo adInfo) {}

                        @Override
                        public void onAdClosed(@NonNull LevelPlayAdInfo adInfo) {}

                        @Override
                        public void onAdRewarded(@NonNull LevelPlayReward reward, @NonNull LevelPlayAdInfo adInfo) {}

                        @Override
                        public void onAdInfoChanged(@NonNull LevelPlayAdInfo adInfo) {}
                    });
                    rewardedAdHolder[0].loadAd();
                }

                @Override
                public void onInitFailed(@NonNull LevelPlayInitError error) {
                    fail("Init failed: " + error);
                }
            });
        });

        assertTrue("Init did not complete within 10 seconds", initLatch.await(10, TimeUnit.SECONDS));
        assertTrue("Rewarded ad did not load within 15 seconds", loadLatch.await(15, TimeUnit.SECONDS));

        activityRule.getScenario().onActivity(activity ->
                rewardedAdHolder[0].showAd(activity)
        );

        assertTrue("Impression callback not received within 20 seconds", impressionLatch.await(20, TimeUnit.SECONDS));
    }
}
