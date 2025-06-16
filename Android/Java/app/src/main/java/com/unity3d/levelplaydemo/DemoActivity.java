package com.unity3d.levelplaydemo;

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
import com.unity3d.mediation.LevelPlay;
import com.unity3d.mediation.LevelPlayAdSize;
import com.unity3d.mediation.LevelPlayInitRequest;
import com.unity3d.mediation.banner.LevelPlayBannerAdView;
import com.unity3d.mediation.interstitial.LevelPlayInterstitialAd;
import com.unity3d.mediation.rewarded.LevelPlayReward;
import com.unity3d.mediation.rewarded.LevelPlayRewardedAd;

import java.util.Arrays;

public class DemoActivity extends Activity implements DemoActivityListener {

    public static final String TAG = "DemoActivity";

    // Replace with your app key as available in the LevelPlay dashboard
    public static final String APP_KEY = "85460dcd";

    // Replace with your ad unit ids as available in the LevelPlay dashboard
    public static final String INTERSTITIAL_AD_UNIT_ID = "aeyqi3vqlv6o8sh9";
    public static final String BANNER_AD_UNIT_ID = "thnfvcsog13bhn08";
    public static final String REWARDED_VIDEO_AD_UNIT_ID = "76yy3nay3ceui2a3";

    private Button rewardedVideoLoadButton;
    private Button rewardedVideoShowButton;
    private LevelPlayRewardedAd rewardedAd;
    private LevelPlayReward reward;

    private Button interstitialLoadButton;
    private Button interstitialShowButton;
    private LevelPlayInterstitialAd interstitialAd;

    private Button bannerLoadButton;
    private FrameLayout bannerParentLayout;
    private LevelPlayBannerAdView bannerAd;

    //region Lifecycle Methods
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_demo);

        setupUI();
        setupLevelPlaySdk();
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        if (bannerAd != null) {
            bannerAd.destroy();
        }
    }
    //endregion

    //region Initialization Methods
    private void setupUI() {
        rewardedVideoLoadButton = findViewById(R.id.rewarded_video_load_button);
        rewardedVideoShowButton = findViewById(R.id.rewarded_video_show_button);
        interstitialLoadButton = findViewById(R.id.interstitial_load_button);
        interstitialShowButton = findViewById(R.id.interstitial_show_button);
        bannerLoadButton = findViewById(R.id.banner_load_button);

        TextView versionTextView = findViewById(R.id.version_txt);
        versionTextView.setText(String.format("%s %s", getResources().getString(R.string.version), LevelPlay.getSdkVersion()));

        bannerParentLayout = findViewById(R.id.banner_frame_layout);
    }

    private void setupLevelPlaySdk() {
        // The integrationHelper is used to validate the integration.
        // Remove the integrationHelper before going live!
        if (BuildConfig.DEBUG) {
            LevelPlay.validateIntegration(this);
        }

        LevelPlay.addImpressionDataListener(new DemoImpressionDataListener());

        // After setting the listeners you can go ahead and initialize the SDK.
        // Once the initialization callback is returned you can start loading your ads

        LevelPlayInitRequest initRequest = new LevelPlayInitRequest.Builder(APP_KEY)
                .build();

        log("init levelPlay SDK with appKey: " + APP_KEY);
        LevelPlay.init(this, initRequest, new DemoInitializationListener(this));

        // Scroll down the file to find out what happens when you tap a button...
    }
    //endregion

    //region Interstitial Methods
    public void createInterstitialAd() {
        interstitialAd = new LevelPlayInterstitialAd(INTERSTITIAL_AD_UNIT_ID);
        interstitialAd.setListener(new DemoInterstitialAdListener(this));

        setEnablementForButton(DemoButtonIdentifiers.LOAD_INTERSTITIAL_BUTTON_IDENTIFIER, true);
    }

    public void loadInterstitialButtonTapped(View view) {
        // This will load an Interstitial ad
        if (interstitialAd != null) {
            log("loadAd for interstitial");
            interstitialAd.loadAd();
        }
    }

    public void showInterstitialButtonTapped(View view) {
        // It is advised to make sure there is available ad that isn't capped before attempting to show it
        if (interstitialAd != null && interstitialAd.isAdReady()) {
            // This will present the Interstitial.
            // Unlike Rewarded Videos there are no placements.

            log("showAd for interstitial");
            interstitialAd.showAd(this);
        } else {
            // load a new ad before calling show
        }
    }
    //endregion

    //region Banner Methods
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
            bannerAd = new LevelPlayBannerAdView(this, BANNER_AD_UNIT_ID);
            bannerAd.setAdSize(adSize);

            // set the banner listener
            bannerAd.setBannerListener(new DemoBannerAdListener(this));

            // add LevelPlayBannerAdView to your container
            FrameLayout.LayoutParams layoutParams = new FrameLayout.LayoutParams(MATCH_PARENT, MATCH_PARENT);
            bannerParentLayout.addView(bannerAd, 0, layoutParams);
            setEnablementForButton(DemoButtonIdentifiers.LOAD_BANNER_BUTTON_IDENTIFIER, true);
        } else {
            log("Failed to create banner ad");
        }
    }

    public void loadBannerButtonTapped(View view) {
        // Load a banner ad. If the "refresh" option is enabled in the LevelPlay dashboard settings, the banner will automatically refresh at the specified interval,
        // otherwise, the banner will remain static until manually destroyed
        if (bannerAd != null) {
            log("loadAd for banner");
            bannerAd.loadAd();
        }
    }

    @Override
    public void setBannerViewVisibility(int visibility) {
        this.bannerParentLayout.setVisibility(visibility);
    }
    //endregion

    //region Rewarded Methods

    public void createRewardedAd() {
        rewardedAd = new LevelPlayRewardedAd(REWARDED_VIDEO_AD_UNIT_ID);
        rewardedAd.setListener(new DemoRewardedVideoAdListener(this));

        setEnablementForButton(DemoButtonIdentifiers.LOAD_REWARDED_VIDEO_BUTTON_IDENTIFIER, true);
    }


    public void loadRewardedButtonTapped(View view) {
        // This will load Rewarded ad
        if (rewardedAd != null) {
            log("loadAd for rewarded ad");
            rewardedAd.loadAd();
        }
    }

    public void showRewardedVideoButtonTapped(View view) {
        // It is advised to make sure there is available ad before attempting to show an ad
        if (rewardedAd != null && rewardedAd.isAdReady()) {
            // This will present the Rewarded Video.

            log("show ad for rewarded ad");
            rewardedAd.showAd(this);
        } else {
            // load a new ad before calling show
        }
    }

    @Override
    public void setReward(LevelPlayReward reward) {
        // Setting the reward, containing the reward name and amount
        this.reward = reward;
    }

    @Override
    public void showRewardDialog() {
        // Showing a graphical indication of the reward name and amount after the user closed the rewarded video ad
        if (this.rewardedAd != null) {
            new AlertDialog.Builder(DemoActivity.this)
                    .setPositiveButton("ok", (dialog, id) -> dialog.dismiss())
                    .setTitle(getResources().getString(R.string.rewarded_dialog_header))
                    .setMessage(getResources().getString(R.string.rewarded_dialog_message) + " " + this.reward.getAmount() + " " + this.reward.getName())
                    .setCancelable(false)
                    .create()
                    .show();

            this.reward = null;
        }
    }
    //endregion

    //region Utility Methods
    @Override
    public void setEnablementForButton(DemoButtonIdentifiers identifier, boolean enable) {
        String text = null;
        final int color = enable ? Color.BLUE : Color.BLACK;
        Button buttonToModify = null;

        switch (identifier) {
            case LOAD_REWARDED_VIDEO_BUTTON_IDENTIFIER:
                text = getResources().getString(R.string.load);
                buttonToModify = rewardedVideoLoadButton;
                break;
            case SHOW_REWARDED_VIDEO_BUTTON_IDENTIFIER:
                text = getResources().getString(R.string.show);
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
