package com.unity3d.levelplaydemo

import android.view.ViewGroup
import android.view.WindowManager
import android.widget.FrameLayout
import androidx.test.ext.junit.rules.ActivityScenarioRule
import androidx.test.ext.junit.runners.AndroidJUnit4
import com.unity3d.mediation.LevelPlay
import com.unity3d.mediation.LevelPlayAdError
import com.unity3d.mediation.LevelPlayAdInfo
import com.unity3d.mediation.LevelPlayAdSize
import com.unity3d.mediation.LevelPlayConfiguration
import com.unity3d.mediation.LevelPlayInitError
import com.unity3d.mediation.LevelPlayInitListener
import com.unity3d.mediation.LevelPlayInitRequest
import com.unity3d.mediation.banner.LevelPlayBannerAdView
import com.unity3d.mediation.banner.LevelPlayBannerAdViewListener
import com.unity3d.mediation.impression.LevelPlayImpressionData
import com.unity3d.mediation.impression.LevelPlayImpressionDataListener
import org.junit.Assert.assertTrue
import org.junit.Assert.fail
import org.junit.Rule
import org.junit.Test
import org.junit.runner.RunWith
import java.util.concurrent.CountDownLatch
import java.util.concurrent.TimeUnit

private val APP_KEY = DemoActivity.APP_KEY
private val BANNER_AD_UNIT_ID = DemoActivity.BANNER_AD_UNIT_ID

@RunWith(AndroidJUnit4::class)
class BannerIntegrationTest {

    @get:Rule
    val activityRule = ActivityScenarioRule(DemoActivity::class.java)

    @Test
    fun testInitLoadAndImpression() {
        val initLatch = CountDownLatch(1)
        val loadLatch = CountDownLatch(1)
        val impressionLatch = CountDownLatch(1)

        activityRule.scenario.onActivity { activity ->
            activity.setTurnScreenOn(true)
            activity.setShowWhenLocked(true)
            activity.window.addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON)
        }

        LevelPlay.addImpressionDataListener(object : LevelPlayImpressionDataListener {
            override fun onImpressionSuccess(impressionData: LevelPlayImpressionData) {
                impressionLatch.countDown()
            }
        })

        activityRule.scenario.onActivity { activity ->
            val request = LevelPlayInitRequest.Builder(APP_KEY).build()
            LevelPlay.init(activity, request, object : LevelPlayInitListener {
                override fun onInitSuccess(configuration: LevelPlayConfiguration) {
                    initLatch.countDown()

                    val adSize = LevelPlayAdSize.createAdaptiveAdSize(activity) ?: run {
                        fail("Failed to create banner ad size")
                        return
                    }

                    val config = LevelPlayBannerAdView.Config.Builder().setAdSize(adSize).build()
                    val bannerAd = LevelPlayBannerAdView(activity, BANNER_AD_UNIT_ID, config)

                    bannerAd.setBannerListener(object : LevelPlayBannerAdViewListener {
                        override fun onAdLoaded(adInfo: LevelPlayAdInfo) {
                            loadLatch.countDown()
                        }

                        override fun onAdLoadFailed(error: LevelPlayAdError) {
                            fail("Ad load failed: $error")
                        }

                        override fun onAdDisplayed(adInfo: LevelPlayAdInfo) {}
                        override fun onAdDisplayFailed(adInfo: LevelPlayAdInfo, error: LevelPlayAdError) {}
                        override fun onAdClicked(adInfo: LevelPlayAdInfo) {}
                        override fun onAdExpanded(adInfo: LevelPlayAdInfo) {}
                        override fun onAdCollapsed(adInfo: LevelPlayAdInfo) {}
                        override fun onAdLeftApplication(adInfo: LevelPlayAdInfo) {}
                    })

                    val bannerContainer = activity.findViewById<FrameLayout>(R.id.banner_footer)
                    val layoutParams = FrameLayout.LayoutParams(
                        ViewGroup.LayoutParams.MATCH_PARENT,
                        ViewGroup.LayoutParams.MATCH_PARENT
                    )
                    bannerContainer.addView(bannerAd, 0, layoutParams)
                    bannerAd.loadAd()
                }

                override fun onInitFailed(error: LevelPlayInitError) {
                    fail("Init failed: $error")
                }
            })
        }

        assertTrue("Init did not complete within 10 seconds", initLatch.await(10, TimeUnit.SECONDS))
        assertTrue("Banner did not load within 15 seconds", loadLatch.await(15, TimeUnit.SECONDS))
        assertTrue("Impression callback not received within 20 seconds", impressionLatch.await(20, TimeUnit.SECONDS))
    }
}
