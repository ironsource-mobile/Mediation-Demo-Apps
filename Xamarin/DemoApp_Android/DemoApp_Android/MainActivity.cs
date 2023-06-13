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
using Com.Ironsource.Mediationsdk.ImpressionData;
using Com.Ironsource.Mediationsdk.Adunit.Adapter.Utility;

namespace DemoApp_Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : Activity, IOfferwallListener, ILevelPlayImpressionDataListener, IInitializationListener
    {
        private const String TAG = "DemoActivity";
        private const String APP_KEY = "880456f5";
        private const string XAMARIN_FRAMEWORK_VERSION = "13.2.0.0";
        private Button mVideoButton;
        private Button mOfferwallButton;
        private Button mInterstitialLoadButton;
        private Button mInterstitialShowButton;

        private Placement mPlacement;

        private FrameLayout mBannerParentLayout;
        private IronSourceBannerLayout mIronSourceBannerLayout;


        //LevelPlay instances
        LevelPlayInterstitial mLevelPlayInterstitial;
        LevelPlayRewardedVideoListener mLevelPlayRewardedVideoListener;
        LevelPlayBannerListener mLevelPlayBannerListener;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.RequestWindowFeature(Android.Views.WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.activity_main);

            //Creating level play instances
            mLevelPlayRewardedVideoListener = new LevelPlayRewardedVideoListener(this);
            mLevelPlayInterstitial = new LevelPlayInterstitial(this);
            mLevelPlayBannerListener = new LevelPlayBannerListener(this);

            //Set all listeners
            IronSource.SetLevelPlayRewardedVideoListener(mLevelPlayRewardedVideoListener);
            IronSource.SetOfferwallListener(this);
            IronSource.SetLevelPlayInterstitialListener(mLevelPlayInterstitial);
            IronSource.AddImpressionDataListener(this);

            ConfigFile.GetConfigFile().SetPluginData("Xamarin", IronSourceUtils.SDKVersion, XAMARIN_FRAMEWORK_VERSION);

            //The integrationHelper is used to validate the integration. Remove the integrationHelper before going live!
            IntegrationHelper.ValidateIntegration(this);
            SupersonicConfig.ConfigObj.ClientSideCallbacks = true;
            initUIElements();
            IronSource.Init(this, APP_KEY, this);
            handleLoadInterstitialButtonState();
            createAndLoadBanner();
        }


        /****************************** UI ******************************/

        //This function gets a boolean parameter available
        //And changes mInterstitialShowButton's color accordingly
        public void handleInterstitialShowButtonState(bool available)
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


        /*************************** OFFERWALL ****************************/

        public bool OnOfferwallAdCredited(int credits, int totalCredits, bool totalCreditsFlag)
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

        public void OnOfferwallShowFailed(IronSourceError ironsourceError)
        {
        }

        public void OnGetOfferwallCreditsFailed(IronSourceError ironsourceError)
        {
        }

        /****************************** BANNER ******************************/

        //Creates the relevant layout and banner objects and loads the banner
        private void createAndLoadBanner()
        {
            mBannerParentLayout = FindViewById<FrameLayout>(Resource.Id.banner_footer);
            mIronSourceBannerLayout = IronSource.CreateBanner(this, ISBannerSize.Banner);
            FrameLayout.LayoutParams layoutParams = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.MatchParent, FrameLayout.LayoutParams.WrapContent);
            mBannerParentLayout.AddView(mIronSourceBannerLayout, 0, layoutParams);
            mIronSourceBannerLayout.LevelPlayBannerListener = mLevelPlayBannerListener;
            IronSource.LoadBanner(mIronSourceBannerLayout);

        }

        //Called by LevelPlayBannerListener when OnAdLoadFailed is fired
        //Removes all views from mIronSourceBannerLayout
        public void bannerRemoveAllViews()
        {
            RunOnUiThread(() =>
            {
                mIronSourceBannerLayout.RemoveAllViews();
            });
        }

        //Fired when impression is successfully shown
        public void OnImpressionSuccess(ImpressionData impressionData)
        {
           var allData = impressionData.AllData;
           double revenue = (double) impressionData.Revenue;
           string adNetwork = impressionData.AdNetwork;
           Console.Write("Xamarin-Android-Demo OnImpressionSuccess: " + impressionData.AllData);
        }

        //Fired on initialization complete
        public void OnInitializationComplete()
        {
            Console.Write("Xamarin-Android-Demo OnInitializationComplete");
        }
    }
    /****************************** LEVEL PLAY RV ******************************/

    public class LevelPlayRewardedVideoListener : Activity, ILevelPlayRewardedVideoListener
    {
        private MainActivity mainActivity;

        public LevelPlayRewardedVideoListener(MainActivity mainActivity)
        {
            this.mainActivity = mainActivity;
        }
        public void OnAdClosed(AdInfo adInfo)
        {
            Console.WriteLine("Xamarin-Android-Demo RewardedVideo OnAdClosed adinfo -  " + adInfo);
        }

        public void OnAdClicked(Placement placement, AdInfo adInfo)
        {
            Console.WriteLine("Xamarin-Android-Demo RewardedVideo OnAdClicked adInfo - " + adInfo + " placment - " + placement);
        }


        public void OnAdOpened(AdInfo adInfo)
        {
            Console.WriteLine("Xamarin-Android-Demo RewardedVideo OnAdOpened adInfo - " + adInfo);
        }

        public void OnAdRewarded(Placement placement, AdInfo adInfo)
        {
            Console.WriteLine("Xamarin-Android-Demo RewardedVideo OnAdRewarded adInfo - " + adInfo + " placment - " + placement);
        }

        public void OnAdShowFailed(IronSourceError ironSourceError, AdInfo adInfo)
        {
            Console.WriteLine("Xamarin-Android-Demo RewardedVideo OnAdShowFailed adInfo - " + adInfo + " error - " + ironSourceError);
        }

        public void OnAdAvailable(AdInfo adInfo)
        {
            mainActivity.handleVideoButtonState(true);
            Console.WriteLine("Xamarin-Android-Demo RewardedVideo OnAdAvailable adInfo - " + adInfo);
        }
        public void OnAdUnavailable()
        {
            mainActivity.handleVideoButtonState(true);
            Console.WriteLine("Xamarin-Android-Demo RewardedVideo OnAdUnavailable");
        }
    }




    /************************ LEVEL PLAY INTERSTITIAL ************************/

    public class LevelPlayInterstitial : Activity, ILevelPlayInterstitialListener
    {
        private MainActivity mainActivity;

        public LevelPlayInterstitial(MainActivity mainActivity)
        {
            this.mainActivity = mainActivity;
        }

        public void OnAdClicked(AdInfo adinfo)
        {
            Console.WriteLine("Xamarin-Android-Demo Interstitial OnAdClicked" + adinfo);
        }

        public void OnAdClosed(AdInfo adInfo)
        {
            Console.WriteLine("Xamarin-Android-Demo Interstitial OnAdClosed" + adInfo);
        }

        public void OnAdLoadFailed(IronSourceError ironSourceError)
        {
            mainActivity.handleInterstitialShowButtonState(false);
            Console.WriteLine("Xamarin-Android-Demo Interstitial OnAdLoadFailed" + ironSourceError);
        }

        public void OnAdOpened(AdInfo adinfo)
        {
            Console.WriteLine("Xamarin-Android-Demo Interstitial OnAdOpened" + adinfo);
        }

        public void OnAdReady(AdInfo adinfo)
        {
            mainActivity.handleInterstitialShowButtonState(true);
            Console.WriteLine("Xamarin-Android-Demo Interstitial OnAdReady" + adinfo);
        }

        public void OnAdShowFailed(IronSourceError ironSourceError, AdInfo adInfo)
        {
            Console.WriteLine("Xamarin-Android-Demo Interstitial OnAdShowFailed: error: " + ironSourceError + " Adinfo: " + adInfo);
        }

        public void OnAdShowSucceeded(AdInfo adInfo)
        {
            Console.WriteLine("Xamarin-Android-Demo Interstitial OnAdShowSucceeded" + adInfo);

        }
    }

    /************************** LEVEL PLAY BANNER ****************************/
    public class LevelPlayBannerListener : Activity, ILevelPlayBannerListener
    {

        private MainActivity mainActivity;

        public LevelPlayBannerListener(MainActivity mainActivity)
        {
            this.mainActivity = mainActivity;
        }

        public void OnAdClicked(AdInfo adInfo)
        {
            Console.WriteLine("Xamarin-Android-Demo Banner OnAdClicked" + adInfo);
        }

        public void OnAdLeftApplication(AdInfo adInfo)
        {
            Console.WriteLine("Xamarin-Android-Demo Banner OnAdLeftApplication" + adInfo);
        }

        public void OnAdLoadFailed(IronSourceError ironSourceError)
        {
            mainActivity.bannerRemoveAllViews();
            Console.WriteLine("Xamarin-Android-Demo Banner OnAdLoadFailed" + ironSourceError);
        }

        public void OnAdLoaded(AdInfo adInfo)
        {
            Console.WriteLine("Xamarin-Android-Demo Banner OnAdLoaded" + adInfo);
        }

        public void OnAdScreenDismissed(AdInfo adInfo)
        {
            Console.WriteLine("Xamarin-Android-Demo Banner OnAdScreenDismissed" + adInfo);
        }

        public void OnAdScreenPresented(AdInfo adInfo)
        {
            Console.WriteLine("Xamarin-Android-Demo Banner OnAdScreenPresented" + adInfo);
        }
    }
}

