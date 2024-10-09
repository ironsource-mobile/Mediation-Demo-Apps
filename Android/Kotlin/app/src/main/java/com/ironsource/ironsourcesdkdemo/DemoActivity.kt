package com.ironsource.ironsourcesdkdemo

import android.app.Activity
import android.app.AlertDialog
import android.graphics.Color
import android.os.Bundle
import android.util.Log
import android.view.View
import android.view.ViewGroup.LayoutParams.MATCH_PARENT
import android.widget.Button
import android.widget.FrameLayout
import android.widget.TextView
import androidx.multidex.BuildConfig
import com.ironsource.mediationsdk.IronSource
import com.ironsource.mediationsdk.integration.IntegrationHelper
import com.ironsource.mediationsdk.model.Placement
import com.ironsource.mediationsdk.utils.IronSourceUtils
import com.unity3d.mediation.LevelPlay
import com.unity3d.mediation.LevelPlayAdSize
import com.unity3d.mediation.LevelPlayInitRequest
import com.unity3d.mediation.banner.LevelPlayBannerAdView
import com.unity3d.mediation.interstitial.LevelPlayInterstitialAd


private const val TAG = "DemoActivity"

// Replace with your app key as available in the LevelPlay dashboard
private const val APP_KEY = "85460dcd"

// Replace with your ad unit ids as available in the LevelPlay dashboard
private const val INTERSTITIAL_AD_UNIT_ID = "aeyqi3vqlv6o8sh9"
private const val BANNER_AD_UNIT_ID = "thnfvcsog13bhn08"

class DemoActivity : Activity(), DemoActivityListener {

    private lateinit var rewardedVideoShowButton: Button
    private lateinit var interstitialLoadButton: Button
    private lateinit var interstitialShowButton: Button
    private lateinit var bannerLoadButton: Button

    private var bannerParentLayout: FrameLayout? = null
    private var rewardedVideoPlacementInfo: Placement? = null

    private var mInterstitialAd : LevelPlayInterstitialAd? = null
    private var mBannerAd : LevelPlayBannerAdView? = null

    companion object {
        internal fun logCallbackName(tag: String, fmt: String) {
            Log.d(tag, String.format("%s $fmt", getMethodName()))
        }

        private fun getMethodName(): String {
            return Thread.currentThread().stackTrace
                .takeIf { it.size >= 5 }
                ?.let { it[4].methodName }
                ?: ""
        }
    }

    //region Lifecycle Methods
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_demo)

        setupUI()
        setupIronSourceSdk()
    }

    override fun onResume() {
        super.onResume()
        // call the IronSource onResume method
        IronSource.onResume(this)
    }

    override fun onPause() {
        super.onPause()
        // call the IronSource onPause method
        IronSource.onPause(this)
    }

    override fun onDestroy() {
        super.onDestroy()
        mBannerAd?.destroy()
    }
    //endregion

    //region Private Methods
    private fun setupUI(){
        rewardedVideoShowButton = findViewById(R.id.rv_button)
        interstitialLoadButton = findViewById(R.id.interstitial_load_button)
        interstitialShowButton = findViewById(R.id.interstitial_show_button)
        bannerLoadButton = findViewById(R.id.banner_load_button)

        val versionTV = findViewById<TextView>(R.id.version_txt)
        "${resources.getString(R.string.version)} ${IronSourceUtils.getSDKVersion()}".also { versionTV.text = it }
        bannerParentLayout = findViewById(R.id.banner_footer)
    }

    private fun setupIronSourceSdk(){
        // The integrationHelper is used to validate the integration.
        // Remove the integrationHelper before going live!
        if (BuildConfig.DEBUG) {
            IntegrationHelper.validateIntegration(this)
        }

        // Before initializing any of our legacy products (Rewarded video, Interstitial or Banner) you must set
        // their listeners. Take a look at each of these listeners method and you will see that they each implement a product
        // protocol. This is our way of letting you know what's going on, and if you don't set the listeners
        // we will not be able to communicate with you.
        // We're passing 'this' to our listeners because we want
        // to be able to enable/disable buttons to match ad availability.
        IronSource.setLevelPlayRewardedVideoListener(DemoRewardedVideoAdListener(this))
        IronSource.addImpressionDataListener(DemoImpressionDataListener())

        // After setting the listeners you can go ahead and initialize the SDK.
        // Once the initialization callback is returned you can start loading your ads

        val legacyAdFormats = listOf(LevelPlay.AdFormat.REWARDED)
        val initRequest = LevelPlayInitRequest.Builder(APP_KEY)
            .withLegacyAdFormats(legacyAdFormats)
            .build()

        log("init ironSource SDK with appKey: $APP_KEY")
        LevelPlay.init(this, initRequest, DemoInitializationListener(this))

        // Scroll down the file to find out what happens when you tap a button...
    }
    //endregion

    //region Button Handling
    fun showRewardedVideoButtonTapped(view: View){
        // It is advised to make sure there is available ad before attempting to show an ad
        if (IronSource.isRewardedVideoAvailable()) {
            // This will present the Rewarded Video.

            log("showRewardedVideo")
            IronSource.showRewardedVideo()
        } else {
            // wait for the availability of rewarded video to change to true before calling show
        }
    }

    override fun createInterstitialAd() {
        mInterstitialAd = LevelPlayInterstitialAd(INTERSTITIAL_AD_UNIT_ID)
        mInterstitialAd?.setListener(DemoInterstitialAdListener(this))

        setEnablementForButton(DemoButtonIdentifiers.LOAD_INTERSTITIAL_BUTTON_IDENTIFIER, true)
    }

    fun loadInterstitialButtonTapped(view: View){
        // This will load an Interstitial ad
        log("loadAd for interstitial")
        mInterstitialAd?.loadAd()
    }

    fun showInterstitialButtonTapped(view: View){
        // It is advised to make sure there is available ad that isn't capped before attempting to show it
        if (mInterstitialAd?.isAdReady() == true) {
            // This will present the Interstitial.
            // Unlike Rewarded Videos there are no placements.

            log("showAd for interstitial")
            mInterstitialAd?.showAd(this)
        } else {
            // load a new ad before calling show
        }
    }

    override fun createBannerAd() {
        // choose banner size

        // 1. recommended - Adaptive ad size that adjusts to the screen width
        val adSize = LevelPlayAdSize.createAdaptiveAdSize(this)

        // 2. Adaptive ad size using fixed width ad size
//        val  adSize = LevelPlayAdSize.createAdaptiveAdSize(this, 400)

        // 3. Specific banner size - BANNER, LARGE, MEDIUM_RECTANGLE
//        val adSize = LevelPlayAdSize.BANNER

        // Create the banner view and set the ad unit id and ad size
        adSize?.let {
            mBannerAd = LevelPlayBannerAdView(this, BANNER_AD_UNIT_ID)
            mBannerAd?.setAdSize(adSize)

            // set the banner listener
            mBannerAd?.setBannerListener(DemoBannerAdListener(this))

            // add LevelPlayBannerAdView to your container
            val layoutParams = FrameLayout.LayoutParams(MATCH_PARENT, MATCH_PARENT)
            bannerParentLayout?.addView(mBannerAd, 0, layoutParams)

            setEnablementForButton(DemoButtonIdentifiers.LOAD_BANNER_BUTTON_IDENTIFIER, true)
        }?: run {
            log("Failed to create banner ad")
        }
    }
    fun loadBannerButtonTapped(view: View) {
        // Load a banner ad. If the "refresh" option is enabled in the LevelPlay dashboard settings, the banner will automatically refresh at the specified interval,
        // otherwise, the banner will remain static until manually destroyed
        log("loadAd for banner")
        mBannerAd?.loadAd()
    }

    //endregion

    //region Interface Handling

    override fun setEnablementForButton(buttonIdentifier: DemoButtonIdentifiers, enable: Boolean) {
        var text: String? = null
        val color = if (enable) Color.BLUE else Color.BLACK
        var buttonToModify: Button? = null

        when (buttonIdentifier) {
            DemoButtonIdentifiers.SHOW_REWARDED_VIDEO_BUTTON_IDENTIFIER -> {
                text =
                    if (enable) resources.getString(R.string.show) else resources.getString(R.string.initializing)
                buttonToModify = rewardedVideoShowButton
            }

            DemoButtonIdentifiers.LOAD_INTERSTITIAL_BUTTON_IDENTIFIER -> buttonToModify = interstitialLoadButton
            DemoButtonIdentifiers.SHOW_INTERSTITIAL_BUTTON_IDENTIFIER -> buttonToModify = interstitialShowButton
            DemoButtonIdentifiers.LOAD_BANNER_BUTTON_IDENTIFIER -> buttonToModify = bannerLoadButton
            else -> {}
        }

        val finalButtonToModify = buttonToModify
        val finalText = text

        runOnUiThread {
            finalButtonToModify?.apply {
                setTextColor(color)
                finalText?.let {
                    setText(it)
                }
                isEnabled = enable
            }
        }
    }

    override fun setBannerViewVisibility(visibility: Int){
        bannerParentLayout?.visibility = visibility
    }

    override fun setPlacementInfo(placementInfo: Placement) {
        // Setting the rewarded video placement info, an object that contains the placement's reward name and amount
        rewardedVideoPlacementInfo = placementInfo
    }

    override fun showRewardDialog(){
        // Showing a graphical indication of the reward name and amount after the user closed the rewarded video ad
        rewardedVideoPlacementInfo?.let {
            AlertDialog.Builder(this)
                .setPositiveButton("ok") { dialog, _ -> dialog.dismiss() }
                .setTitle(resources.getString(R.string.rewarded_dialog_header))
                .setMessage("${resources.getString(R.string.rewarded_dialog_message)} ${rewardedVideoPlacementInfo!!.rewardAmount} ${rewardedVideoPlacementInfo!!.rewardName}")
                .setCancelable(false)
                .create()
                .show()
        }

        rewardedVideoPlacementInfo = null
    }
    //endregion

    //region Demo Utils
    private fun log(log: String) {
        Log.d(TAG, log)
    }

    //endregion
}
