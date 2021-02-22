using System;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Com.Ironsource.Adapters.Supersonicads;
using Com.Ironsource.Mediationsdk;
using Com.Ironsource.Mediationsdk.Config;
using Com.Ironsource.Mediationsdk.Integration;
using Com.Ironsource.Mediationsdk.Logger;
using Com.Ironsource.Mediationsdk.Model;
using Com.Ironsource.Mediationsdk.Sdk;
using Com.Ironsource.Mediationsdk.Utils;

namespace DemoApp_Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait) ]
    public class MainActivity : Activity, IRewardedVideoListener, IOfferwallListener, IInterstitialListener, IBannerListener
    {
        private const String TAG = "DemoActivity";
        private const String APP_KEY = "85460dcd";
        private const string XAMARIN_FRAMEWORK_VERSION = "8.2";
        private Button mVideoButton;
        private Button mOfferwallButton;
        private Button mInterstitialLoadButton;
        private Button mInterstitialShowButton;

        private Placement mPlacement;

        private FrameLayout mBannerParentLayout;
        private IronSourceBannerLayout mIronSourceBannerLayout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.RequestWindowFeature(Android.Views.WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.activity_main);

            IronSource.SetRewardedVideoListener(this);

            IronSource.SetOfferwallListener(this);

            IronSource.SetInterstitialListener(this);

            ConfigFile.GetConfigFile().SetPluginData("Xamarin", IronSourceUtils.SDKVersion, XAMARIN_FRAMEWORK_VERSION);

            //The integrationHelper is used to validate the integration. Remove the integrationHelper before going live!
            IntegrationHelper.ValidateIntegration(this);
            SupersonicConfig.ConfigObj.ClientSideCallbacks = true;
            initUIElements();
            IronSource.Init(this,APP_KEY);
            handleLoadInterstitialButtonState();
            createAndLoadBanner();
        }

        private void handleInterstitialShowButtonState(bool available)
        {
            String text = "Show Interstitial";
            Color color;
            if (available)
            {
                color = Color.Blue;
            }
            else
            {
                color = Color.Red;
            }
            this.RunOnUiThread(() =>
            {
                mInterstitialShowButton.SetTextColor(color);
                mInterstitialShowButton.Text = text;
                mInterstitialShowButton.Enabled = available;
            });
        }

        private void handleLoadInterstitialButtonState()
        {
            this.RunOnUiThread(() =>
            {
                mInterstitialLoadButton.SetTextColor(Color.Blue);
                mInterstitialLoadButton.Enabled = true;
            });
        }

        private void handleOfferwallButtonState(bool available)
        {
            String text = "Show Offerwall";
            Color color;
            if (available)
            {
                color = Color.Blue;
            }
            else
            {
                color = Color.Red;
            }
            this.RunOnUiThread(() =>
            {
                mOfferwallButton.SetTextColor(color);
                mOfferwallButton.Text = text;
                mOfferwallButton.Enabled = available;
            });
        }

        public void handleVideoButtonState(bool available)
        {
            String text = "Show Rewarded Video";
            Color color;
            if (available)
            {
                color = Color.Blue;
            }
            else
            {
                color = Color.Red;
            }
            this.RunOnUiThread(() =>
            {
                mVideoButton.SetTextColor(color);
                mVideoButton.Text = text;
                mVideoButton.Enabled = available;
            });
        }

        protected override void OnPause()
        {
            base.OnPause();
            IronSource.OnPause(this);
        }

        protected override void OnResume()
        {
            base.OnResume();
            IronSource.OnResume(this);
        }

        /******** Banner Callbacks ********/

        public void OnBannerAdClicked()
        {
        }

        public void OnBannerAdLeftApplication()
        {
        }

        public void OnBannerAdLoaded()
        {
        }

        public void OnBannerAdLoadFailed(IronSourceError p0)
        {
            RunOnUiThread(() => {
                mIronSourceBannerLayout.RemoveAllViews();
            });
        }

        public void OnBannerAdScreenDismissed()
        {
        }

        public void OnBannerAdScreenPresented()
        {
        }

        /******** Interstitial Callbacks ********/

        public void OnInterstitialAdClicked()
        {
        }

        public void OnInterstitialAdClosed()
        {
        }

        public void OnInterstitialAdLoadFailed(IronSourceError p0)
        {
            handleInterstitialShowButtonState(false);
        }

        public void OnInterstitialAdOpened()
        {
        }

        public void OnInterstitialAdReady()
        {
            handleInterstitialShowButtonState(true);
        }

        public void OnInterstitialAdShowFailed(IronSourceError p0)
        {
        }

        public void OnInterstitialAdShowSucceeded()
        {
        }

        /******** Offerwall Callbacks ********/

        public bool OnOfferwallAdCredited(int p0, int p1, bool p2)
        {
            return true;
        }

        public void OnOfferwallAvailable(bool available)
        {
            handleOfferwallButtonState(available);
        }

        public void OnOfferwallClosed()
        {
        }

        public void OnOfferwallOpened()
        {
        }

        public void OnOfferwallShowFailed(IronSourceError p0)
        {
        }

        public void OnGetOfferwallCreditsFailed(IronSourceError p0)
        {
        }

        /******** Rewarded Video Callbacks ********/

        public void OnRewardedVideoAdClicked(Placement placement)
        {
        }

        public void OnRewardedVideoAdClosed()
        {
        }

        public void OnRewardedVideoAdEnded()
        {
        }

        public void OnRewardedVideoAdOpened()
        {
        }

        public void OnRewardedVideoAdRewarded(Placement p0)
        {
        }

        public void OnRewardedVideoAdShowFailed(IronSourceError error)
        {
        }

        public void OnRewardedVideoAdStarted()
        {
        }

        public void OnRewardedVideoAvailabilityChanged(bool available)
        {
            handleVideoButtonState(available);
        }

        private void createAndLoadBanner()
        {
            mBannerParentLayout = FindViewById<FrameLayout>(Resource.Id.banner_footer);
            mIronSourceBannerLayout = IronSource.CreateBanner(this, ISBannerSize.Banner);
            FrameLayout.LayoutParams layoutParams = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.MatchParent, FrameLayout.LayoutParams.WrapContent);
            mBannerParentLayout.AddView(mIronSourceBannerLayout, 0, layoutParams);
            mIronSourceBannerLayout.BannerListener = this;
            IronSource.LoadBanner(mIronSourceBannerLayout);

        }

        private void initUIElements()
        {
            String initText = "Initializing..";
            mVideoButton = FindViewById<Button>(Resource.Id.rv_button);
            mVideoButton.Text = initText;
            mVideoButton.Enabled = false;
            mVideoButton.Click += delegate
            {
                IronSource.ShowRewardedVideo();
            };
            mOfferwallButton = FindViewById<Button>(Resource.Id.ow_button);
            mOfferwallButton.Click += delegate
            {
                IronSource.ShowOfferwall();
            };
            mOfferwallButton.Text = initText;
            mOfferwallButton.Enabled = false;
            mInterstitialLoadButton = FindViewById<Button>(Resource.Id.is_button_1);
            mInterstitialLoadButton.Click += delegate
            {
                IronSource.LoadInterstitial();
            };
            
            mInterstitialShowButton = FindViewById<Button>(Resource.Id.is_button_2);
            mInterstitialShowButton.Click += delegate
            {
                IronSource.ShowInterstitial();
            };
            mInterstitialShowButton.Enabled = false;

            TextView version = FindViewById<TextView>(Resource.Id.version_txt);
            version.Text = "SDK version " + IronSourceUtils.SDKVersion;
        }


    }

}

