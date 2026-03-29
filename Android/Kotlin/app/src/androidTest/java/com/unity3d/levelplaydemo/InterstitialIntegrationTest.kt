package com.unity3d.levelplaydemo

import android.view.WindowManager
import androidx.test.ext.junit.rules.ActivityScenarioRule
import androidx.test.ext.junit.runners.AndroidJUnit4
import com.unity3d.mediation.LevelPlay
import com.unity3d.mediation.LevelPlayAdError
import com.unity3d.mediation.LevelPlayAdInfo
import com.unity3d.mediation.LevelPlayConfiguration
import com.unity3d.mediation.LevelPlayInitError
import com.unity3d.mediation.LevelPlayInitListener
import com.unity3d.mediation.LevelPlayInitRequest
import com.unity3d.mediation.impression.LevelPlayImpressionData
import com.unity3d.mediation.impression.LevelPlayImpressionDataListener
import com.unity3d.mediation.interstitial.LevelPlayInterstitialAd
import com.unity3d.mediation.interstitial.LevelPlayInterstitialAdListener
import org.junit.Assert.assertTrue
import org.junit.Assert.fail
import org.junit.Rule
import org.junit.Test
import org.junit.runner.RunWith
import java.util.concurrent.CountDownLatch
import java.util.concurrent.TimeUnit

private val APP_KEY = DemoActivity.APP_KEY
private val INTERSTITIAL_AD_UNIT_ID = DemoActivity.INTERSTITIAL_AD_UNIT_ID

@RunWith(AndroidJUnit4::class)
class InterstitialIntegrationTest {

    @get:Rule
    val activityRule = ActivityScenarioRule(DemoActivity::class.java)

    @Test
    fun testInitLoadAndImpression() {
        val initLatch = CountDownLatch(1)
        val loadLatch = CountDownLatch(1)
        val impressionLatch = CountDownLatch(1)
        var interstitialAd: LevelPlayInterstitialAd? = null

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

                    interstitialAd = LevelPlayInterstitialAd(INTERSTITIAL_AD_UNIT_ID)
                    interstitialAd?.setListener(object : LevelPlayInterstitialAdListener {
                        override fun onAdLoaded(adInfo: LevelPlayAdInfo) {
                            loadLatch.countDown()
                        }

                        override fun onAdLoadFailed(error: LevelPlayAdError) {
                            fail("Ad load failed: $error")
                        }

                        override fun onAdDisplayed(adInfo: LevelPlayAdInfo) {}
                        override fun onAdDisplayFailed(error: LevelPlayAdError, adInfo: LevelPlayAdInfo) {}
                        override fun onAdClicked(adInfo: LevelPlayAdInfo) {}
                        override fun onAdClosed(adInfo: LevelPlayAdInfo) {}
                        override fun onAdInfoChanged(adInfo: LevelPlayAdInfo) {}
                    })
                    interstitialAd?.loadAd()
                }

                override fun onInitFailed(error: LevelPlayInitError) {
                    fail("Init failed: $error")
                }
            })
        }

        assertTrue("Init did not complete within 10 seconds", initLatch.await(10, TimeUnit.SECONDS))
        assertTrue("Interstitial did not load within 15 seconds", loadLatch.await(15, TimeUnit.SECONDS))

        activityRule.scenario.onActivity { activity ->
            interstitialAd?.showAd(activity)
        }

        assertTrue("Impression callback not received within 20 seconds", impressionLatch.await(20, TimeUnit.SECONDS))
    }
}
