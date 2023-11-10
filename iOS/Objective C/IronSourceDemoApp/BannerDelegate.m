//
//  BannerDelegate.m
//  IronSourceDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.


#import "BannerDelegate.h"

@implementation BannerDelegate

- (instancetype)initWithDelegate:(id<DemoViewControllerDelegate>)delegate {
    self = [super init];
    if (self) {
        _delegate = delegate;
    }
    return self;
}

/**
 Called after a banner ad has been successfully loaded
 @param adInfo The info of the ad.
 */
- (void)didLoad:(ISBannerView *)bannerView
     withAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
    
    [self.delegate setAndBindBannerView:bannerView];
    [self.delegate performActionForButton:DestroyBanner
                           withEnablement:YES];
}

/**
 Called after a banner has attempted to load an ad but failed.
 @param error The reason for the error
 */
- (void)didFailToLoadWithError:(NSError *)error {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

/**
 Called after a banner has been clicked.
 @param adInfo The info of the ad.
 */
- (void)didClickWithAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

/**
 Called when a user was taken out of the application context.
 @param adInfo The info of the ad.
 */
- (void)didLeaveApplicationWithAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

/**
 Called when a banner presented a full screen content.
 @param adInfo The info of the ad.
 */
- (void)didPresentScreenWithAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

/**
 Called after a full screen content has been dismissed.
 @param adInfo The info of the ad.
 */
- (void)didDismissScreenWithAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

@end
