package com.levelplay.levelplaysdkdemo

import android.app.Activity
import android.app.AlertDialog
import android.graphics.Color
import android.os.Bundle
import android.util.Log
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.FrameLayout
import android.widget.TextView
import androidx.multidex.BuildConfig
import com.unity3d.mediation.LevelPlay
import com.unity3d.mediation.LevelPlayAdSize
import com.unity3d.mediation.LevelPlayInitRequest
import com.unity3d.mediation.banner.LevelPlayBannerAdView
import com.unity3d.mediation.interstitial.LevelPlayInterstitialAd
import com.unity3d.mediation.rewarded.LevelPlayReward
import com.unity3d.mediation.rewarded.LevelPlayRewardedAd


private const val TAG = "DemoActivity"

// Replace with your app key as available in the LevelPlay dashboard
private const val APP_KEY = "85460dcd"

// Replace with your ad unit ids as available in the LevelPlay dashboard
private const val INTERSTITIAL_AD_UNIT_ID = "aeyqi3vqlv6o8sh9"
private const val BANNER_AD_UNIT_ID = "thnfvcsog13bhn08"
private const val REWARDED_AD_UNIT_ID = "76yy3nay3ceui2a3"

class DemoActivity : Activity(), DemoActivityListener {

    private lateinit var rewardedLoadButton: Button
    private lateinit var rewardedShowButton: Button
    private var rewardedAd : LevelPlayRewardedAd? = null
    private var reward: LevelPlayReward? = null

    private lateinit var interstitialLoadButton: Button
    private lateinit var interstitialShowButton: Button
    private var interstitialAd : LevelPlayInterstitialAd? = null

    private lateinit var bannerLoadButton: Button
    private var bannerParentLayout: FrameLayout? = null
    private var bannerAd : LevelPlayBannerAdView? = null

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
        setupLevelPlaySdk()
    }

    override fun onDestroy() {
        super.onDestroy()
        bannerAd?.destroy()
    }
    //endregion

    //region Initialization Methods
    private fun setupUI(){


        rewardedLoadButton = findViewById(R.id.rewarded_load_button)
        rewardedShowButton = findViewById(R.id.rewarded_show_button)
        interstitialLoadButton = findViewById(R.id.interstitial_load_button)
        interstitialShowButton = findViewById(R.id.interstitial_show_button)
        bannerLoadButton = findViewById(R.id.banner_load_button)

        val versionTV = findViewById<TextView>(R.id.version_txt)
        "${resources.getString(R.string.version)} ${LevelPlay.getSdkVersion()}".also { versionTV.text = it }
        bannerParentLayout = findViewById(R.id.banner_footer)
    }

    private fun setupLevelPlaySdk(){
        // The integrationHelper is used to validate the integration.
        // Remove the integrationHelper before going live!
        if (BuildConfig.DEBUG) {
            LevelPlay.validateIntegration(this)
        }

        LevelPlay.addImpressionDataListener(DemoImpressionDataListener())

        // After setting the listeners you can go ahead and initialize the SDK.
        // Once the initialization callback is returned you can start loading your ads

        val initRequest = LevelPlayInitRequest.Builder(APP_KEY)
            .build()

        log("init levelPlay SDK with appKey: $APP_KEY")
        LevelPlay.init(this, initRequest, DemoInitializationListener(this))

        // Scroll down the file to find out what happens when you tap a button...
    }
    //endregion

    //region Interstitial Methods
    override fun createInterstitialAd() {
        interstitialAd = LevelPlayInterstitialAd(INTERSTITIAL_AD_UNIT_ID)
        interstitialAd?.setListener(DemoInterstitialAdListener(this))

        setEnablementForButton(DemoButtonIdentifiers.LOAD_INTERSTITIAL_BUTTON_IDENTIFIER, true)
    }



    fun loadInterstitialButtonTapped(view: View){
        // This will load an Interstitial ad
        log("loadAd for interstitial")
        interstitialAd?.loadAd()
    }

    fun showInterstitialButtonTapped(view: View){
        // It is advised to make sure there is available ad that isn't capped before attempting to show it
        if (interstitialAd?.isAdReady() == true) {
            // This will present the Interstitial.
            // Unlike Rewarded Videos there are no placements.

            log("showAd for interstitial")
            interstitialAd?.showAd(this)
        } else {
            // load a new ad before calling show
        }
    }
    //endregion

    //region Banner Methods
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
            bannerAd = LevelPlayBannerAdView(this, BANNER_AD_UNIT_ID)
            bannerAd?.setAdSize(adSize)

            // set the banner listener
            bannerAd?.setBannerListener(DemoBannerAdListener(this))

            // add LevelPlayBannerAdView to your container
            val layoutParams = FrameLayout.LayoutParams(
                ViewGroup.LayoutParams.MATCH_PARENT,
                ViewGroup.LayoutParams.MATCH_PARENT
            )
            bannerParentLayout?.addView(bannerAd, 0, layoutParams)

            setEnablementForButton(DemoButtonIdentifiers.LOAD_BANNER_BUTTON_IDENTIFIER, true)
        }?: run {
            log("Failed to create banner ad")
        }
    }

    fun loadBannerButtonTapped(view: View) {
        // Load a banner ad. If the "refresh" option is enabled in the LevelPlay dashboard settings, the banner will automatically refresh at the specified interval,
        // otherwise, the banner will remain static until manually destroyed
        log("loadAd for banner")
        bannerAd?.loadAd()
    }

    override fun setBannerViewVisibility(visibility: Int){
        bannerParentLayout?.visibility = visibility
    }
    //endregion

    //region Rewarded Video Methods

    override fun createRewardedAd() {
        rewardedAd = LevelPlayRewardedAd(REWARDED_AD_UNIT_ID)
        rewardedAd?.setListener(DemoRewardedVideoAdListener(this))

        setEnablementForButton(DemoButtonIdentifiers.LOAD_REWARDED_VIDEO_BUTTON_IDENTIFIER, true)
    }

    fun loadRewardedButtonTapped(view: View){
        // This will load an Interstitial ad
        log("loadAd for rewarded ad")
        rewardedAd?.loadAd()
    }

    fun showRewardedButtonTapped(view: View){
        // It is advised to make sure there is available ad that isn't capped before attempting to show it
        if (rewardedAd?.isAdReady() == true) {
            // This will present the Rewarded Ad.

            log("showAd for rewarded ad")
            rewardedAd?.showAd(this)
        } else {
            // load a new ad before calling show
        }
    }

    override fun setReward(reward: LevelPlayReward) {
        // Setting the reward, containing the reward name and amount
        this.reward = reward
    }

    override fun showRewardDialog(){
        // Showing a graphical indication of the reward name and amount after the user closed the rewarded video ad
        reward?.let {
            AlertDialog.Builder(this)
                .setPositiveButton("ok") { dialog, _ -> dialog.dismiss() }
                .setTitle(resources.getString(R.string.rewarded_dialog_header))
                .setMessage("${resources.getString(R.string.rewarded_dialog_message)} ${reward!!.amount} ${reward!!.name}")
                .setCancelable(false)
                .create()
                .show()
        }

        reward = null
    }
    //endregion

    //region Utility Methods
    override fun setEnablementForButton(buttonIdentifier: DemoButtonIdentifiers, enable: Boolean) {
        var text: String? = null
        val color = if (enable) Color.BLUE else Color.BLACK
        var buttonToModify: Button? = null

        when (buttonIdentifier) {

            DemoButtonIdentifiers.LOAD_INTERSTITIAL_BUTTON_IDENTIFIER -> buttonToModify = interstitialLoadButton
            DemoButtonIdentifiers.SHOW_INTERSTITIAL_BUTTON_IDENTIFIER -> buttonToModify = interstitialShowButton
            DemoButtonIdentifiers.LOAD_REWARDED_VIDEO_BUTTON_IDENTIFIER -> buttonToModify = rewardedLoadButton
            DemoButtonIdentifiers.SHOW_REWARDED_VIDEO_BUTTON_IDENTIFIER -> buttonToModify = rewardedShowButton
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

    private fun log(log: String) {
        Log.d(TAG, log)
    }
    //endregion
}