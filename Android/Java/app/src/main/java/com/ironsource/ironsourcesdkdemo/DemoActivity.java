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
import com.ironsource.mediationsdk.ISBannerSize;
import com.ironsource.mediationsdk.IronSource;
import com.ironsource.mediationsdk.IronSourceBannerLayout;
import com.ironsource.mediationsdk.integration.IntegrationHelper;
import com.ironsource.mediationsdk.model.Placement;
import com.ironsource.mediationsdk.utils.IronSourceUtils;
import java.util.Arrays;

public class DemoActivity extends Activity implements DemoActivityListener {

    public static final String TAG = "DemoActivity";
    public static final String APP_KEY = "85460dcd";

    private Button rewardedVideoShowButton;
    private Button interstitialLoadButton;
    private Button interstitialShowButton;
    private Button bannerLoadButton;
    private Button bannerDestroyButton;

    private FrameLayout bannerParentLayout;
    private IronSourceBannerLayout ironSourceBannerLayout;
    private Placement rewardedVideoPlacementInfo;

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
    //endregion

    //region Private Methods
    private void setupUI() {
        rewardedVideoShowButton = findViewById(R.id.rewarded_video_show_button);
        interstitialLoadButton = findViewById(R.id.interstitial_load_button);
        interstitialShowButton = findViewById(R.id.interstitial_show_button);
        bannerLoadButton = findViewById(R.id.banner_load_button);
        bannerDestroyButton = findViewById(R.id.banner_destroy_button);

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

        // Before initializing any of our products (Rewarded video, Interstitial or Banner) you must set
        // their listeners. Take a look at each of these listeners method and you will see that they each implement a product
        // protocol. This is our way of letting you know what's going on, and if you don't set the listeners
        // we will not be able to communicate with you.
        // We're passing 'this' to our listeners because we want
        // to be able to enable/disable buttons to match ad availability.
        IronSource.setLevelPlayRewardedVideoListener(new DemoRewardedVideoAdListener(this));
        IronSource.setLevelPlayInterstitialListener(new DemoInterstitialAdListener(this));
        IronSource.addImpressionDataListener(new DemoImpressionDataListener());

        // After setting the listeners you can go ahead and initialize the SDK.
        // Once the initialization callback is returned you can start loading your ads
        log("init ironSource SDK with appKey: " + APP_KEY);
        IronSource.init(this, APP_KEY, new DemoInitializationListener(this));

        // To initialize specific ad units:
        // IronSource.init(this, APP_KEY, new InitializationListener(this), IronSource.AD_UNIT.REWARDED_VIDEO, IronSource.AD_UNIT.INTERSTITIAL, IronSource.AD_UNIT.BANNER);

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

    public void loadInterstitialButtonTapped(View view) {
        // This will load an Interstitial ad

        log("loadInterstitial");
        IronSource.loadInterstitial();
    }

    public void showInterstitialButtonTapped(View view) {
        // It is advised to make sure there is available ad before attempting to show an ad
        if (IronSource.isInterstitialReady()) {
            // This will present the Interstitial.
            // Unlike Rewarded Videos there are no placements.

            log("showInterstitial");
            IronSource.showInterstitial();
        } else {
            // load a new ad before calling show
        }
    }

    public void loadBannerButtonTapped(View view) {
        // call IronSource.destroyBanner() before loading a new banner
        if (bannerParentLayout != null) {
            this.destroyBanner();
        }

        // choose banner size
        // you can pick any banner size: ISBannerSize.BANNER, ISBannerSize.LARGE, ISBannerSize.RECTANGLE, ISBannerSize.SMART or even define a custom banner size by providing width and height
        ISBannerSize size = ISBannerSize.BANNER;

        // initialize IronSourceBanner object, using the IronSource.createBanner API
        ironSourceBannerLayout = IronSource.createBanner(this, size);

        if (ironSourceBannerLayout != null) {
            // add IronSourceBanner to your container
            FrameLayout.LayoutParams layoutParams = new FrameLayout.LayoutParams(MATCH_PARENT, MATCH_PARENT);
            bannerParentLayout.addView(ironSourceBannerLayout, 0, layoutParams);

            // set the banner listener
            ironSourceBannerLayout.setLevelPlayBannerListener(new DemoBannerAdListener(this));

            // load ad into the created banner
            log("loadBanner");
            IronSource.loadBanner(ironSourceBannerLayout);
        } else {
            log("IronSource.createBanner returned null");
        }
    }

    public void destroyBannerButtonTapped(View view) {
        destroyBanner();
    }

    private void destroyBanner() {
        if (bannerParentLayout != null) {
            log("destroyBanner");
            IronSource.destroyBanner(ironSourceBannerLayout);
            bannerParentLayout.removeView(ironSourceBannerLayout);
            this.setBannerViewVisibility(View.GONE);
        }

        setEnablementForButton(DemoButtonIdentifiers.LOAD_BANNER_BUTTON_IDENTIFIER, true);
        setEnablementForButton(DemoButtonIdentifiers.DESTROY_BANNER_BUTTON_IDENTIFIER, false);
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
            case DESTROY_BANNER_BUTTON_IDENTIFIER:
                buttonToModify = bannerDestroyButton;
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
