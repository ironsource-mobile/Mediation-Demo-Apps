using System;
using CoreGraphics;
using Foundation;
using IronSourceiOS;
using UIKit;

namespace DemoApp_iOS
{
    public class BannerWrapper : ISBannerDelegate
    {
        readonly UIViewController parent;
        ISBannerView bannerView = null;

        public bool DestroyBanner()
        {
            if (bannerView != null)
            {
                IronSource.DestroyBanner(bannerView);
                bannerView = null;
                return true;
            }
            return false;
        }

        public BannerWrapper(UIViewController viewController)
        {
            this.parent = viewController;
        }
        public override void BannerDidClick()
        {
        }

        public override void BannerDidDismissScreen()
        {
        }

        public override void BannerDidFailToLoadWithError(NSError error)
        {
        }

        public override void BannerDidLoad(ISBannerView bnView)
        {
            InvokeOnMainThread(() =>
            {

                bannerView = bnView;

                nfloat y = this.parent.View.Frame.Size.Height - (bannerView.Frame.Size.Height / 2);
                if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                {
                    y -= this.parent.View.SafeAreaInsets.Bottom;
                }
                bannerView.Center = new CGPoint(this.parent.View.Frame.Size.Width / 2, y);

                this.parent.View.AddSubview(bannerView);
                bannerView.AccessibilityLabel = "bannerContainer";

            });

        }

        public override void BannerWillLeaveApplication()
        {
        }

        public override void BannerWillPresentScreen()
        {
        }
    }

}
