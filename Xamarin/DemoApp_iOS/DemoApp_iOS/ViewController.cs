using Foundation;
using System;
using UIKit;

using IronSourceiOS;
using CoreGraphics;

namespace DemoApp_iOS
{
    public partial class ViewController : UIViewController
    {
        RVDelegate rvDelegate;
        OWDelegate owDelegate;
        ISDelegate isDelegate;
        BannerWrapper bnDelegate;
        private const String APPKEY = "8545d445";

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            rvDelegate = new RVDelegate(this);
            owDelegate = new OWDelegate(this);
            isDelegate = new ISDelegate(this);
            bnDelegate = new BannerWrapper(this);

            version_lbl.Text = "SDK Version " + IronSource.SdkVersion;

            IronSource.SetRewardedVideoDelegate(rvDelegate);
            IronSource.SetOfferwallDelegate(owDelegate);
            IronSource.SetInterstitialDelegate(isDelegate);
            IronSource.SetBannerDelegate((ISBannerDelegate)bnDelegate);

            ISConfigurations.Configurations.Plugin = "Xamarin";
            ISConfigurations.Configurations.PluginVersion = "6.10.0";
            ISConfigurations.Configurations.PluginFrameworkVersion = "8.2.0.0";
            ISSupersonicAdsConfiguration.Configurations.UseClientSideCallbacks = true;

            ISIntegrationHelper.ValidateIntegration();
            IronSource.InitWithAppKey(APPKEY);
            var bannerSize = new ISBannerSize("BANNER");
            IronSource.LoadBannerWithViewController(this, bannerSize);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private class RVDelegate : ISRewardedVideoDelegate
        {
            private ViewController viewController;

            public RVDelegate(ViewController viewController)
            {
                this.viewController = viewController;
            }

            public override void DidClickRewardedVideo(ISPlacementInfo placementInfo)
            {
            }

            public override void DidReceiveRewardForPlacement(ISPlacementInfo placementInfo)
            {
            }

            public override void RewardedVideoDidClose()
            {
            }

            public override void RewardedVideoDidEnd()
            {
            }

            public override void RewardedVideoDidFailToShowWithError(NSError error)
            {
            }

            public override void RewardedVideoDidOpen()
            {
            }

            public override void RewardedVideoDidStart()
            {
            }

            public override void RewardedVideoHasChangedAvailability(bool available)
            {
                if (available)
                {
                    viewController.ShowRV.SetTitleColor(UIColor.Green, UIControlState.Normal);
                    viewController.ShowRV.Enabled = true;
                }
                else
                {
                    viewController.ShowRV.SetTitleColor(UIColor.Red, UIControlState.Normal);
                    //viewController.ShowRV.Enabled = false;
                }
            }
        }

        private class OWDelegate : ISOfferwallDelegate
        {
            private ViewController viewController;

            public OWDelegate(ViewController viewController)
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

        private class ISDelegate : ISInterstitialDelegate
        {
            private ViewController viewController;

            public ISDelegate(ViewController viewController)
            {
                this.viewController = viewController;
            }

            public override void InterstitialDidClick()
            {
            }

            public override void InterstitialDidClose()
            {
                viewController.ShowIS.SetTitleColor(UIColor.Red, UIControlState.Normal);
                //viewController.ShowIS.Enabled = false;
            }

            public override void InterstitialDidFailToLoadWithError(NSError error)
            {
                viewController.ShowIS.SetTitleColor(UIColor.Red, UIControlState.Disabled);
            }

            public override void InterstitialDidFailToShow(NSError error)
            {

            }

            public override void InterstitialDidLoad()
            {
                viewController.ShowIS.SetTitleColor(UIColor.Green, UIControlState.Normal);
                viewController.ShowIS.Enabled = true;
            }

            public override void InterstitialDidOpen()
            {
            }

            public override void InterstitialDidShow()
            {
            }
        }

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
    }
}