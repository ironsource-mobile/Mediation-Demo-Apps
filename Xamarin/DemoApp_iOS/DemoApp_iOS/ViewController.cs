using Foundation;
using System;
using UIKit;

using IronSourceiOS;
using CoreGraphics;
using CoreAudioKit;

namespace DemoApp_iOS
{
    public partial class ViewController : UIViewController
    {
        OfferWallDelegate mOfferWallDelegate;
        RewardedVideoLevelPlayDelegate mRewardedVideoLevelPlayDelegate;
        InterstitialLevelPlayDelegate mInterstitialLevelPlayDelegate;
        BannerWrapper mBannerLevelPlayDelegate;
        ImpressionDataDelegate mImpressionDataDelegate;

        private const String APPKEY = "8545d445";

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            mRewardedVideoLevelPlayDelegate = new RewardedVideoLevelPlayDelegate(this);
            mOfferWallDelegate = new OfferWallDelegate(this);
            mInterstitialLevelPlayDelegate = new InterstitialLevelPlayDelegate(this);
            mBannerLevelPlayDelegate = new BannerWrapper(this);
            mImpressionDataDelegate = new ImpressionDataDelegate(this);

            version_lbl.Text = "SDK Version " + IronSource.SdkVersion;

            //Set delegates
            IronSource.SetOfferwallDelegate(mOfferWallDelegate);
            IronSource.setLevelPlayBannerDelegate((LevelPlayBannerDelegate)mBannerLevelPlayDelegate);
            IronSource.setLevelPlayRewardedVideoDelegate(mRewardedVideoLevelPlayDelegate);
            IronSource.setLevelPlayInterstitialDelegate(mInterstitialLevelPlayDelegate);
            IronSource.addImpressionDataDelegate(mImpressionDataDelegate);

            //Set configurations
            ISConfigurations.Configurations.Plugin = "Xamarin";
            ISConfigurations.Configurations.PluginVersion = "7.3.0";
            ISConfigurations.Configurations.PluginFrameworkVersion = "16.2.0.5";
            ISSupersonicAdsConfiguration.Configurations.UseClientSideCallbacks = true;

            ISIntegrationHelper.ValidateIntegration();
            IronSource.InitWithAppKey(APPKEY);
            var bannerSize = new ISBannerSize("BANNER", 320, 50);
            IronSource.LoadBannerWithViewController(this, bannerSize);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        // ******************* UI ******************* //
        public void SetRewardedVideoButtomState(bool available)
        {
            if (available)
            {
                ShowRV.SetTitleColor(UIColor.Green, UIControlState.Normal);
                ShowRV.Enabled = true;
            }
            else
            {
                ShowRV.SetTitleColor(UIColor.Red, UIControlState.Normal);
            }
        }

        public void EnableInterstitialButtonState()
        {
            ShowIS.SetTitleColor(UIColor.Green, UIControlState.Normal);
            ShowIS.Enabled = true;
        }

        public void DisableInterstitialButtonState(bool isNormal)
        {
            if (isNormal)
            {
                ShowIS.SetTitleColor(UIColor.Red, UIControlState.Normal);
            }
            else
            {
                ShowIS.SetTitleColor(UIColor.Red, UIControlState.Disabled);
            }
        }

        // **************** Button Actions **************** //
        partial void ShowRV_TouchUpInside(UIButton sender)
        {
            IronSource.ShowRewardedVideoWithViewController(this);
        }

        partial void ShowOW_TouchUpInside(UIButton sender)
        {
            IronSource.ShowOfferwallWithViewController(this);
        }

        partial void LoadIS_TouchUpInside(UIButton sender)
        {
            IronSource.LoadInterstitial();
        }

        partial void ShowIS_TouchUpInside(UIButton sender)
        {
            IronSource.ShowInterstitialWithViewController(this);
        }

        // **************** Offerwall **************** //
        private class OfferWallDelegate : ISOfferwallDelegate
        {
            private ViewController viewController;

            public OfferWallDelegate(ViewController viewController)
            {
                this.viewController = viewController;
            }

            public override void DidFailToReceiveOfferwallCreditsWithError(NSError error)
            {
            }

            public override bool DidReceiveOfferwallCredits(NSDictionary creditInfo)
            {
                return true;
            }

            public override void OfferwallDidClose()
            {
            }

            public override void OfferwallDidFailToShowWithError(NSError error)
            {
            }

            public override void OfferwallDidShow()
            {
            }

            public override void OfferwallHasChangedAvailability(bool available)
            {
                if (available)
                {
                    viewController.ShowOW.SetTitleColor(UIColor.Green, UIControlState.Normal);
                    viewController.ShowOW.Enabled = true;
                }
                else
                {
                    viewController.ShowOW.SetTitleColor(UIColor.Red, UIControlState.Normal);
                    //viewController.ShowOW.Enabled = false;
                }
            }
        }


    }

    #region LevelPlayRewardedVideo
    // **************** Rewarded Video **************** //
    public class RewardedVideoLevelPlayDelegate : LevelPlayRewardedVideoDelegate
    {
        ViewController viewController;

        public RewardedVideoLevelPlayDelegate(ViewController viewController)
        {
            this.viewController = viewController;
            Console.WriteLine("Xamarin-iOS-Demo : LevelPlayRewardedVideoDelegate didset");

        }

        public override void didClick(ISPlacementInfo placementInfo, ISAdInfo adInfo)
        {
            Console.WriteLine("Xamarin-iOS-Demo : LevelPlayRewardedVideoDelegate didClick:  placement: " + placementInfo + " adinfo: " + adInfo);
        }

        public override void didCloseWithAdInfo(ISAdInfo adInfo)
        {
            Console.WriteLine("Xamarin-iOS-Demo : LevelPlayRewardedVideoDelegate didCloseWithAdInfo: adinfo: " + adInfo);
        }

        public override void didFailToShowWithError(NSError error, ISAdInfo adInfo)
        {
            Console.WriteLine("Xamarin-iOS-Demo : LevelPlayRewardedVideoDelegate didFailToShowWithError:  error: " + error + " adinfo: " + adInfo);
        }

        public override void didOpenWithAdInfo(ISAdInfo adInfo)
        {
            Console.WriteLine("Xamarin-iOS-Demo : LevelPlayRewardedVideoDelegate didOpenWithAdInfo: adinfo: " + adInfo);
        }

        public override void didReceiveRewardForPlacement(ISPlacementInfo placementInfo, ISAdInfo adInfo)
        {
            Console.WriteLine("Xamarin-iOS-Demo : - LevelPlayRewardedVideoDelegate didReceiveRewardForPlacement:  placement: " + placementInfo + " adinfo: " + adInfo);
        }

        public override void hasAvailableAdWithAdInfo(ISAdInfo adInfo)
        {
            Console.WriteLine("Xamarin-iOS-Demo : - LevelPlayRewardedVideoDelegate hasAvailableAdWithAdInfo: adinfo: " + adInfo);
            viewController.SetRewardedVideoButtomState(true);
        }

        public override void hasNoAvailableAd()
        {
            Console.WriteLine("Xamarin-iOS-Demo : LevelPlayRewardedVideoDelegate hasNoAvailableAd");
            viewController.SetRewardedVideoButtomState(false);

        }
    }
    #endregion


    #region LevelPlayInterstitial
    // **************** Interstitial **************** //


    public class InterstitialLevelPlayDelegate : LevelPlayInterstitialDelegate
    {
        readonly ViewController viewController;

        public InterstitialLevelPlayDelegate(ViewController viewController)
        {
            this.viewController = viewController;
        }

        public override void didClickWithAdInfo(ISAdInfo adInfo)
        {
            Console.WriteLine("Xamarin-iOS-Demo : InterstitialLevelPlayDelegate didClickWithAdInfo: adInfo: " + adInfo);
        }

        public override void didCloseWithAdInfo(ISAdInfo adInfo)
        {
            Console.WriteLine("Xamarin-iOS-Demo : InterstitialLevelPlayDelegate didCloseWithAdInfo: adInfo: " + adInfo);
            this.viewController.DisableInterstitialButtonState(true);
        }

        public override void didFailToLoadWithError(NSError error)
        {
            Console.WriteLine("Xamarin-iOS-Demo : InterstitialLevelPlayDelegate didFailToLoadWithError: error: " + error);
            this.viewController.DisableInterstitialButtonState(false);

        }

        public override void didFailToShowWithError(NSError error, ISAdInfo adInfo)
        {
            Console.WriteLine("Xamarin-iOS-Demo : InterstitialLevelPlayDelegate didFailToShowWithError: adInfo: " + adInfo + " error:" + error);
        }

        public override void didLoadWithAdInfo(ISAdInfo adInfo)
        {
            Console.WriteLine("Xamarin-iOS-Demo : InterstitialLevelPlayDelegate didLoadWithAdInfo: adInfo: " + adInfo);
            this.viewController.EnableInterstitialButtonState();
        }

        public override void didOpenWithAdInfo(ISAdInfo adInfo)
        {
            Console.WriteLine("Xamarin-iOS-Demo : InterstitialLevelPlayDelegate didOpenWithAdInfo: adInfo: " + adInfo);
        }

        public override void didShowWithAdInfo(ISAdInfo adInfo)
        {
            Console.WriteLine("Xamarin-iOS-Demo : InterstitialLevelPlayDelegate didShowWithAdInfo: adInfo: " + adInfo);
        }
    }
    #endregion

    // ********* Impression Data ********** //
    public class ImpressionDataDelegate : ISImpressionDataDelegate
    {
        readonly ViewController parent;

        public ImpressionDataDelegate(ViewController parent)
        {
            this.parent = parent;
        }

        public override void ImpressionDataDidSucceed(ISImpressionData impressionData)
        {
            NSDictionary allData = impressionData.All_data;
            int revenue = impressionData.Revenue;
            string adNetwork = impressionData.Ad_network;
        }
    }
}