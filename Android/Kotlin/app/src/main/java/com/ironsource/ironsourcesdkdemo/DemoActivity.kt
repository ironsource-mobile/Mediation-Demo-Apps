package com.ironsource.ironsourcesdkdemo

import android.app.Activity
import android.graphics.Color
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.FrameLayout
import android.widget.Toast
import androidx.appcompat.app.AlertDialog
import com.ironsource.adapters.supersonicads.SupersonicConfig
import com.ironsource.ironsourcesdkdemo.databinding.ActivityDemoBinding
import com.ironsource.mediationsdk.*
import com.ironsource.mediationsdk.impressionData.ImpressionData
import com.ironsource.mediationsdk.impressionData.ImpressionDataListener
import com.ironsource.mediationsdk.integration.IntegrationHelper
import com.ironsource.mediationsdk.logger.IronSourceError
import com.ironsource.mediationsdk.model.Placement
import com.ironsource.mediationsdk.sdk.*
import com.ironsource.mediationsdk.utils.IronSourceUtils

class DemoActivity : Activity(), RewardedVideoListener, OfferwallListener, InterstitialListener,ImpressionDataListener {

    companion object {
        const val APP_KEY = "85460dcd"
        const val TAG = "DemoActivity"
    }

    private lateinit var binding: ActivityDemoBinding

    private var mPlacement: Placement? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityDemoBinding.inflate(layoutInflater)
        val view = binding.root
        setContentView(view)

        //The integrationHelper is used to validate the integration. Remove the integrationHelper before going live!
        IntegrationHelper.validateIntegration(this)
        initUIElements()

        val advertisingId = IronSource.getAdvertiserId(this@DemoActivity)
        // we're using an advertisingId as the 'userId'
        initIronSource(advertisingId)

        //Network Connectivity Status
        IronSource.shouldTrackNetworkState(this, true)
    }



    private fun initIronSource(userId: String?) {
        // Be sure to set a listener to each product that is being initiated
        // set the IronSource rewarded video listener
        IronSource.setRewardedVideoListener(this)
        // set the IronSource offerwall listener
        IronSource.setOfferwallListener(this)
        // set client side callbacks for the offerwall
        SupersonicConfig.getConfigObj().clientSideCallbacks = true
        // set the interstitial listener
        IronSource.setInterstitialListener(this)
        // add impression data listener
        IronSource.addImpressionDataListener(this)

        // set the IronSource user id
        IronSource.setUserId(userId)
        // init the IronSource SDK
        IronSource.init(this, APP_KEY)
        updateButtonsState()

        // In order to work with IronSourceBanners you need to add Providers who support banner ad unit and uncomment next line
         createAndloadBanner()
    }

    override fun onResume() {
        super.onResume()
        // call the IronSource onResume method
        IronSource.onResume(this)
        updateButtonsState()
    }

    override fun onPause() {
        super.onPause()
        // call the IronSource onPause method
        IronSource.onPause(this)
        updateButtonsState()
    }

    /**
     * Handle the button state according to the status of the IronSource producs
     */
    private fun updateButtonsState() {
        handleVideoButtonState(IronSource.isRewardedVideoAvailable())
        handleOfferwallButtonState(IronSource.isOfferwallAvailable())
        handleLoadInterstitialButtonState()
        handleInterstitialShowButtonState(false)
    }

    /**
     * initialize the UI elements of the activity
     */
    private fun initUIElements() {
        binding.rvButton.setOnClickListener {
            // check if video is available
            if (IronSource.isRewardedVideoAvailable()) //show rewarded video
                IronSource.showRewardedVideo()
        }
        binding.owButton.setOnClickListener { //show the offerwall
            if (IronSource.isOfferwallAvailable()) IronSource.showOfferwall()
        }
        binding.isLoadButton.setOnClickListener { IronSource.loadInterstitial() }
        binding.isShowButton.setOnClickListener {
            // check if interstitial is available
            if (IronSource.isInterstitialReady()) {
                //show the interstitial
                IronSource.showInterstitial()
            }
        }
        binding.versionTxt.text = resources.getString(R.string.version, IronSourceUtils.getSDKVersion())
    }

    /**
     * Creates and loads IronSource Banner
     *
     */
    private fun createAndloadBanner() {
        // choose banner size
        val size = ISBannerSize.BANNER

        // instantiate IronSourceBanner object, using the IronSource.createBanner API
        val mIronSourceBannerLayout = IronSource.createBanner(this, size)

        mIronSourceBannerLayout?.let {
            // add IronSourceBanner to your container
            val layoutParams = FrameLayout.LayoutParams(FrameLayout.LayoutParams.MATCH_PARENT,
                    FrameLayout.LayoutParams.MATCH_PARENT)

            binding.bannerFooter.addView(it, 0, layoutParams)

            // set the banner listener
            it.bannerListener = object : BannerListener {
                override fun onBannerAdLoaded() {
                    Log.d(TAG, "onBannerAdLoaded")
                    // since banner container was "gone" by default, we need to make it visible as soon as the banner is ready
                    binding.bannerFooter.visibility = View.VISIBLE
                }

                override fun onBannerAdLoadFailed(error: IronSourceError) {
                    Log.d(TAG, "onBannerAdLoadFailed $error")
                }

                override fun onBannerAdClicked() {
                    Log.d(TAG, "onBannerAdClicked")
                }

                override fun onBannerAdScreenPresented() {
                    Log.d(TAG, "onBannerAdScreenPresented")
                }

                override fun onBannerAdScreenDismissed() {
                    Log.d(TAG, "onBannerAdScreenDismissed")
                }

                override fun onBannerAdLeftApplication() {
                    Log.d(TAG, "onBannerAdLeftApplication")
                }
            }

            // load ad into the created banner
            IronSource.loadBanner(it)

        } ?: run {
            Toast.makeText(this@DemoActivity, "IronSource.createBanner returned null", Toast.LENGTH_LONG).show()

        }


    }

    /**
     * Set the Rewareded Video button state according to the product's state
     *
     * @param available if the video is available
     */
    private fun handleVideoButtonState(available: Boolean) {
        val text: String
        val color: Int
        if (available) {
            color = Color.BLUE
            text = resources.getString(R.string.show) + " " + resources.getString(R.string.rv)
        } else {
            color = Color.BLACK
            text = resources.getString(R.string.initializing) + " " + resources.getString(R.string.rv)
        }
        runOnUiThread {
            binding.rvButton.setTextColor(color)
            binding.rvButton.text = text
            binding.rvButton.isEnabled = available
        }
    }

    /**
     * Set the Rewareded Video button state according to the product's state
     *
     * @param available if the offerwall is available
     */
    private fun handleOfferwallButtonState(available: Boolean) {
        val text: String
        val color: Int
        if (available) {
            color = Color.BLUE
            text = resources.getString(R.string.show) + " " + resources.getString(R.string.ow)
        } else {
            color = Color.BLACK
            text = resources.getString(R.string.initializing) + " " + resources.getString(R.string.ow)
        }
        runOnUiThread {
            binding.owButton.setTextColor(color)
            binding.owButton.text = text
            binding.owButton.isEnabled = available
        }
    }

    /**
     * Set the Interstitial button state according to the product's state
     *
     */
    private fun handleLoadInterstitialButtonState() {
        Log.d(TAG, "handleInterstitialButtonState | available: true")
        val text = resources.getString(R.string.load) + " " + resources.getString(R.string.`is`)
        val color = Color.BLUE
        runOnUiThread {
            binding.isLoadButton.setTextColor(color)
            binding.isLoadButton.text = text
            binding.isLoadButton.isEnabled = true
        }
    }


    /**
     * Set the Show Interstitial button state according to the product's state
     *
     * @param available if the interstitial is available
     */
    private fun handleInterstitialShowButtonState(available: Boolean) {
        val color: Int = if (available) {
            Color.BLUE
        } else {
            Color.BLACK
        }
        runOnUiThread {
            binding.isShowButton.setTextColor(color)
            binding.isShowButton.isEnabled = available
        }
    }

    // --------- IronSource Rewarded Video Listener ---------
    override fun onRewardedVideoAdOpened() {
        // called when the video is opened
        Log.d(TAG, "onRewardedVideoAdOpened")
    }

    override fun onRewardedVideoAdClosed() {
        // called when the video is closed
        Log.d(TAG, "onRewardedVideoAdClosed")
        // here we show a dialog to the user if he was rewarded
        mPlacement?.let {
            // if the user was rewarded
            showRewardDialog(it)
            mPlacement = null
        }

    }

    override fun onRewardedVideoAvailabilityChanged(b: Boolean) {
        // called when the video availbility has changed
        Log.d(TAG, "onRewardedVideoAvailabilityChanged $b")
        handleVideoButtonState(b)
    }

    override fun onRewardedVideoAdStarted() {
        // called when the video has started
        Log.d(TAG, "onRewardedVideoAdStarted")
    }

    override fun onRewardedVideoAdEnded() {
        // called when the video has ended
        Log.d(TAG, "onRewardedVideoAdEnded")
    }

    override fun onRewardedVideoAdRewarded(placement: Placement) {
        // called when the video has been rewarded and a reward can be given to the user
        Log.d(TAG, "onRewardedVideoAdRewarded $placement")
        mPlacement = placement
    }

    override fun onRewardedVideoAdShowFailed(ironSourceError: IronSourceError) {
        // called when the video has failed to show
        // you can get the error data by accessing the IronSourceError object
        // IronSourceError.getErrorCode();
        // IronSourceError.getErrorMessage();
        Log.d(TAG, "onRewardedVideoAdShowFailed $ironSourceError")
    }

    override fun onRewardedVideoAdClicked(placement: Placement) {}

    // --------- IronSource Offerwall Listener ---------
    override fun onOfferwallAvailable(available: Boolean) {
        handleOfferwallButtonState(available)
    }

    override fun onOfferwallOpened() {
        // called when the offerwall has opened
        Log.d(TAG, "onOfferwallOpened")
    }

    override fun onOfferwallShowFailed(ironSourceError: IronSourceError) {
        // called when the offerwall failed to show
        // you can get the error data by accessing the IronSourceError object
        // IronSourceError.getErrorCode();
        // IronSourceError.getErrorMessage();
        Log.d(TAG, "onOfferwallShowFailed $ironSourceError")
    }

    override fun onOfferwallAdCredited(credits: Int, totalCredits: Int, totalCreditsFlag: Boolean): Boolean {
        Log.d(TAG, "onOfferwallAdCredited credits:$credits totalCredits:$totalCredits totalCreditsFlag:$totalCreditsFlag")
        return false
    }

    override fun onGetOfferwallCreditsFailed(ironSourceError: IronSourceError) {
        // you can get the error data by accessing the IronSourceError object
        // IronSourceError.getErrorCode();
        // IronSourceError.getErrorMessage();
        Log.d(TAG, "onGetOfferwallCreditsFailed $ironSourceError")
    }

    override fun onOfferwallClosed() {
        // called when the offerwall has closed
        Log.d(TAG, "onOfferwallClosed")
    }

    // --------- IronSource Interstitial Listener ---------
    override fun onInterstitialAdClicked() {
        // called when the interstitial has been clicked
        Log.d(TAG, "onInterstitialAdClicked")
    }

    override fun onInterstitialAdReady() {
        // called when the interstitial is ready
        Log.d(TAG, "onInterstitialAdReady")
        handleInterstitialShowButtonState(true)
    }

    override fun onInterstitialAdLoadFailed(ironSourceError: IronSourceError) {
        // called when the interstitial has failed to load
        // you can get the error data by accessing the IronSourceError object
        // IronSourceError.getErrorCode();
        // IronSourceError.getErrorMessage();
        Log.d(TAG, "onInterstitialAdLoadFailed $ironSourceError")
        handleInterstitialShowButtonState(false)
    }

    override fun onInterstitialAdOpened() {
        // called when the interstitial is shown
        Log.d(TAG, "onInterstitialAdOpened")
    }

    override fun onInterstitialAdClosed() {
        // called when the interstitial has been closed
        Log.d(TAG, "onInterstitialAdClosed")
        handleInterstitialShowButtonState(false)
    }

    override fun onInterstitialAdShowSucceeded() {
        // called when the interstitial has been successfully shown
        Log.d(TAG, "onInterstitialAdShowSucceeded")
    }

    override fun onInterstitialAdShowFailed(ironSourceError: IronSourceError) {
        // called when the interstitial has failed to show
        // you can get the error data by accessing the IronSourceError object
        // IronSourceError.getErrorCode();
        // IronSourceError.getErrorMessage();
        Log.d(TAG, "onInterstitialAdShowFailed $ironSourceError")
        handleInterstitialShowButtonState(false)
    }

    // --------- Impression Data Listener ---------

    override fun onImpressionSuccess(impressionData: ImpressionData?) {
        // The onImpressionSuccess will be reported when the rewarded video and interstitial ad is opened.
        // For banners, the impression is reported on load success.
        if (impressionData != null) {
            Log.d(TAG, "onImpressionSuccess $impressionData")
        }
    }

    private fun showRewardDialog(placement: Placement) {
        val builder = AlertDialog.Builder(this@DemoActivity)
        builder.setPositiveButton("ok") { dialog, _ -> dialog.dismiss() }
        builder.setTitle(resources.getString(R.string.rewarded_dialog_header))
        builder.setMessage(resources.getString(R.string.rewarded_dialog_message) + " " + placement.rewardAmount + " " + placement.rewardName)
        builder.setCancelable(false)
        val dialog = builder.create()
        dialog.show()
    }


}



