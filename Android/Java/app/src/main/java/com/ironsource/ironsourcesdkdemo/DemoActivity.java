package com.ironsource.ironsourcesdkdemo;

import static android.view.ViewGroup.LayoutParams.MATCH_PARENT;

import android.app.Activity;
import android.app.AlertDialog;
import android.graphics.Color;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.FrameLayout;
import android.widget.TextView;
import androidx.multidex.BuildConfig;
import com.ironsource.mediationsdk.IronSource;
import com.ironsource.mediationsdk.integration.IntegrationHelper;
import com.ironsource.mediationsdk.model.Placement;
import com.ironsource.mediationsdk.utils.IronSourceUtils;
import com.unity3d.mediation.LevelPlay;
import com.unity3d.mediation.LevelPlayAdSize;
import com.unity3d.mediation.LevelPlayInitRequest;
import com.unity3d.mediation.banner.LevelPlayBannerAdView;
import com.unity3d.mediation.interstitial.LevelPlayInterstitialAd;

import java.util.Arrays;
import java.util.List;

public class DemoActivity extends Activity implements DemoActivityListener {

    public static final String TAG = "DemoActivity";

    // Replace with your app key as available in the LevelPlay dashboard
    public static final String APP_KEY = "85460dcd";

    // Replace with your ad unit ids as available in the LevelPlay dashboard
    public static final String INTERSTITIAL_AD_UNIT_ID = "aeyqi3vqlv6o8sh9";
    public static final String BANNER_AD_UNIT_ID = "thnfvcsog13bhn08";

    private Button rewardedVideoShowButton;
    private Button interstitialLoadButton;
    private Button interstitialShowButton;
    private Button bannerLoadButton;

    private FrameLayout bannerParentLayout;
    private Placement rewardedVideoPlacementInfo;

    private LevelPlayInterstitialAd mInterstitialAd;
    private LevelPlayBannerAdView mBannerAd;

    //region Lifecycle Methods
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_demo);

        setupUI();
        setupIronSourceSdk();
    }

    @Override
    protected void onResume() {
        super.onResume();
        // call the IronSource onResume method
        IronSource.onResume(this);
    }

    @Override
    protected void onPause() {
        super.onPause();
        // call the IronSource onPause method
        IronSource.onPause(this);
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        if (mBannerAd != null) {
            mBannerAd.destroy();
        }
    }

    //endregion

    //region Private Methods
    private void setupUI() {
        rewardedVideoShowButton = findViewById(R.id.rewarded_video_show_button);
        interstitialLoadButton = findViewById(R.id.interstitial_load_button);
        interstitialShowButton = findViewById(R.id.interstitial_show_button);
        bannerLoadButton = findViewById(R.id.banner_load_button);

        TextView versionTextView = findViewById(R.id.version_txt);
        versionTextView.setText(String.format("%s %s", getResources().getString(R.string.version), IronSourceUtils.getSDKVersion()));

        bannerParentLayout = findViewById(R.id.banner_frame_layout);
    }

    private void setupIronSourceSdk() {
        // The integrationHelper is used to validate the integration.
        // Remove the integrationHelper before going live!
        if (BuildConfig.DEBUG){
            IntegrationHelper.validateIntegration(this);
        }

        // Before initializing any of our legacy products (Rewarded video, Interstitial or Banner) you must set
        // their listeners. Take a look at each of these listeners method and you will see that they each implement a product
        // protocol. This is our way of letting you know what's going on, and if you don't set the listeners
        // we will not be able to communicate with you.
        // We're passing 'this' to our listeners because we want
        // to be able to enable/disable buttons to match ad availability.
        IronSource.setLevelPlayRewardedVideoListener(new DemoRewardedVideoAdListener(this));
        IronSource.addImpressionDataListener(new DemoImpressionDataListener());

        // After setting the listeners you can go ahead and initialize the SDK.
        // Once the initialization callback is returned you can start loading your ads

        // Init the SDK when implementing the Multiple Ad Units Interstitial and Banner API, and Rewarded using legacy APIs
        List<LevelPlay.AdFormat> legacyAdFormats = Arrays.asList(LevelPlay.AdFormat.REWARDED);

        LevelPlayInitRequest initRequest = new LevelPlayInitRequest.Builder(APP_KEY)
                .withLegacyAdFormats(legacyAdFormats)
                .build();

        log("init ironSource SDK with appKey: " + APP_KEY);
        LevelPlay.init(this, initRequest, new DemoInitializationListener(this));

        // Scroll down the file to find out what happens when you tap a button...
    }

    //endregion

    //region Button Handling
    public void showRewardedVideoButtonTapped(View view) {
        // It is advised to make sure there is available ad before attempting to show an ad
        if (IronSource.isRewardedVideoAvailable()) {
            // This will present the Rewarded Video.

            log("showRewardedVideo");
            IronSource.showRewardedVideo();
        } else {
            // wait for the availability of rewarded video to change to true before calling show
        }
    }

    public void createInterstitialAd() {
        mInterstitialAd = new LevelPlayInterstitialAd(INTERSTITIAL_AD_UNIT_ID);
        mInterstitialAd.setListener(new DemoInterstitialAdListener(this));

        setEnablementForButton(DemoButtonIdentifiers.LOAD_INTERSTITIAL_BUTTON_IDENTIFIER, true);
    }

    public void loadInterstitialButtonTapped(View view) {
        // This will load an Interstitial ad
        if (mInterstitialAd != null) {
            log("loadAd for interstitial");
            mInterstitialAd.loadAd();
        }
    }

    public void showInterstitialButtonTapped(View view) {
        // It is advised to make sure there is available ad that isn't capped before attempting to show it
        if (mInterstitialAd != null && mInterstitialAd.isAdReady()) {
            // This will present the Interstitial.
            // Unlike Rewarded Videos there are no placements.

            log("showAd for interstitial");
            mInterstitialAd.showAd(this);
        } else {
            // load a new ad before calling show
        }
    }

    public void createBannerAd() {
        // choose banner size

        // 1. recommended - Adaptive ad size that adjusts to the screen width
        LevelPlayAdSize adSize = LevelPlayAdSize.createAdaptiveAdSize(this);

        // 2. Adaptive ad size using fixed width ad size
//         LevelPlayAdSize adSize = LevelPlayAdSize.createAdaptiveAdSize(this, 400);

        // 3. Specific banner size - BANNER, LARGE, MEDIUM_RECTANGLE
//        LevelPlayAdSize adSize = LevelPlayAdSize.BANNER;

        // Create the banner view and set the ad unit id and ad size
        if (adSize != null) {
            mBannerAd = new LevelPlayBannerAdView(this, BANNER_AD_UNIT_ID);
            mBannerAd.setAdSize(adSize);

            // set the banner listener
            mBannerAd.setBannerListener(new DemoBannerAdListener(this));

            // add LevelPlayBannerAdView to your container
            FrameLayout.LayoutParams layoutParams = new FrameLayout.LayoutParams(MATCH_PARENT, MATCH_PARENT);
            bannerParentLayout.addView(mBannerAd, 0, layoutParams);
            setEnablementForButton(DemoButtonIdentifiers.LOAD_BANNER_BUTTON_IDENTIFIER, true);
        }
        else {
            log( "Failed to create banner ad");
        }
    }

    public void loadBannerButtonTapped(View view) {
        // Load a banner ad. If the "refresh" option is enabled in the LevelPlay dashboard settings, the banner will automatically refresh at the specified interval,
        // otherwise, the banner will remain static until manually destroyed
        if (mBannerAd != null) {
            log("loadAd for banner");
            mBannerAd.loadAd();
        }
    }

    //endregion

    //region Demo Callbacks
    @Override
    public void setEnablementForButton(DemoButtonIdentifiers identifier, boolean enable) {
        String text = null;
        final int color = enable ? Color.BLUE : Color.BLACK;
        Button buttonToModify = null;

        switch (identifier) {
            case SHOW_REWARDED_VIDEO_BUTTON_IDENTIFIER:
                text = enable ? getResources().getString(R.string.show) : getResources().getString(R.string.initializing);
                buttonToModify = rewardedVideoShowButton;
                break;
            case LOAD_INTERSTITIAL_BUTTON_IDENTIFIER:
                buttonToModify = interstitialLoadButton;
                break;
            case SHOW_INTERSTITIAL_BUTTON_IDENTIFIER:
                buttonToModify = interstitialShowButton;
                break;
            case LOAD_BANNER_BUTTON_IDENTIFIER:
                buttonToModify = bannerLoadButton;
                break;
        }

        Button finalButtonToModify = buttonToModify;
        String finalText = text;
        runOnUiThread(() -> {
            finalButtonToModify.setTextColor(color);
            finalButtonToModify.setText(finalText != null ? finalText : finalButtonToModify.getText());
            finalButtonToModify.setEnabled(enable);
        });
    }

    @Override
    public void setBannerViewVisibility(int visibility) {
        this.bannerParentLayout.setVisibility(visibility);
    }

    @Override
    public void setPlacementInfo(Placement placementInfo) {
        // Setting the rewarded video placement info, an object that contains the placement's reward name and amount
        this.rewardedVideoPlacementInfo = placementInfo;
    }

    @Override
    public void showRewardDialog() {
        // Showing a graphical indication of the reward name and amount after the user closed the rewarded video ad
        if (this.rewardedVideoPlacementInfo != null) {
            new AlertDialog.Builder(DemoActivity.this)
                    .setPositiveButton("ok", (dialog, id) -> dialog.dismiss())
                    .setTitle(getResources().getString(R.string.rewarded_dialog_header))
                    .setMessage(getResources().getString(R.string.rewarded_dialog_message) + " " + this.rewardedVideoPlacementInfo.getRewardAmount() + " " + this.rewardedVideoPlacementInfo.getRewardName())
                    .setCancelable(false)
                    .create()
                    .show();

            this.rewardedVideoPlacementInfo = null;
        }
    }

    //endregion

    //region Demo Utils
    private void log(String log) {
        Log.d(TAG, log);
    }

    protected static void logCallbackName(String tag, String fmt, Object... args) {
        Log.d(tag, String.format("%s " + fmt, getMethodName(), Arrays.toString(args)));
    }

    private static String getMethodName() {
        StackTraceElement[] stackTraceElements = Thread.currentThread().getStackTrace();
        if (stackTraceElements.length >= 5) {
            return stackTraceElements[4].getMethodName();
        }
        return "";
    }
    //endregion
}
