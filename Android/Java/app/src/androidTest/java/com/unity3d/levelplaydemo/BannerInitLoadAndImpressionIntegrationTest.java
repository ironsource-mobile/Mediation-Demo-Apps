package com.unity3d.levelplaydemo;

import static android.view.ViewGroup.LayoutParams.MATCH_PARENT;

import android.view.WindowManager;
import android.widget.FrameLayout;

import androidx.annotation.NonNull;
import androidx.test.ext.junit.rules.ActivityScenarioRule;
import androidx.test.ext.junit.runners.AndroidJUnit4;

import com.unity3d.mediation.LevelPlay;
import com.unity3d.mediation.LevelPlayAdError;
import com.unity3d.mediation.LevelPlayAdInfo;
import com.unity3d.mediation.LevelPlayAdSize;
import com.unity3d.mediation.LevelPlayConfiguration;
import com.unity3d.mediation.LevelPlayInitError;
import com.unity3d.mediation.LevelPlayInitListener;
import com.unity3d.mediation.LevelPlayInitRequest;
import com.unity3d.mediation.banner.LevelPlayBannerAdView;
import com.unity3d.mediation.banner.LevelPlayBannerAdViewListener;
import com.unity3d.mediation.impression.LevelPlayImpressionData;
import com.unity3d.mediation.impression.LevelPlayImpressionDataListener;

import org.junit.Rule;
import org.junit.Test;
import org.junit.runner.RunWith;

import static org.junit.Assert.assertTrue;
import static org.junit.Assert.fail;

import java.util.concurrent.CountDownLatch;
import java.util.concurrent.TimeUnit;

@RunWith(AndroidJUnit4.class)
public class BannerInitLoadAndImpressionIntegrationTest {

    @Rule
    public ActivityScenarioRule<DemoActivity> activityRule = new ActivityScenarioRule<>(DemoActivity.class);

    @Test
    public void testShouldInitLoadBannerAndReceiveImpression() throws InterruptedException {
        // Given
        final CountDownLatch initLatch = new CountDownLatch(1);
        final CountDownLatch loadLatch = new CountDownLatch(1);
        final CountDownLatch impressionLatch = new CountDownLatch(1);

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

        // When
        activityRule.getScenario().onActivity(activity -> {
            LevelPlayInitRequest request = new LevelPlayInitRequest.Builder(DemoActivity.APP_KEY).build();
            LevelPlay.init(activity, request, new LevelPlayInitListener() {
                @Override
                public void onInitSuccess(@NonNull LevelPlayConfiguration configuration) {
                    initLatch.countDown();

                    LevelPlayAdSize adSize = LevelPlayAdSize.createAdaptiveAdSize(activity);
                    if (adSize == null) {
                        fail("Failed to create banner ad size");
                        return;
                    }

                    LevelPlayBannerAdView.Config config = new LevelPlayBannerAdView.Config.Builder().setAdSize(adSize).build();
                    LevelPlayBannerAdView bannerAd = new LevelPlayBannerAdView(activity, DemoActivity.BANNER_AD_UNIT_ID, config);

                    bannerAd.setBannerListener(new LevelPlayBannerAdViewListener() {
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
                        public void onAdDisplayFailed(@NonNull LevelPlayAdInfo adInfo, @NonNull LevelPlayAdError error) {}

                        @Override
                        public void onAdClicked(@NonNull LevelPlayAdInfo adInfo) {}

                        @Override
                        public void onAdExpanded(@NonNull LevelPlayAdInfo adInfo) {}

                        @Override
                        public void onAdCollapsed(@NonNull LevelPlayAdInfo adInfo) {}

                        @Override
                        public void onAdLeftApplication(@NonNull LevelPlayAdInfo adInfo) {}
                    });

                    FrameLayout bannerContainer = activity.findViewById(R.id.banner_frame_layout);
                    FrameLayout.LayoutParams layoutParams = new FrameLayout.LayoutParams(MATCH_PARENT, MATCH_PARENT);
                    bannerContainer.addView(bannerAd, 0, layoutParams);
                    bannerAd.loadAd();
                }

                @Override
                public void onInitFailed(@NonNull LevelPlayInitError error) {
                    fail("Init failed: " + error);
                }
            });
        });

        // Then
        assertTrue("Init did not complete within 10 seconds", initLatch.await(10, TimeUnit.SECONDS));
        assertTrue("Banner did not load within 15 seconds", loadLatch.await(15, TimeUnit.SECONDS));
        assertTrue("Impression callback not received within 20 seconds", impressionLatch.await(20, TimeUnit.SECONDS));
    }
}
