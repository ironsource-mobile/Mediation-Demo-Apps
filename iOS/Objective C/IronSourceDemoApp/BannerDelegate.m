//
//  BannerDelegate.m
//  IronSourceDemoApp
//
//  Copyright © 2024 ironSource Mobile Ltd. All rights reserved.
//

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
 Called after each banner ad has been successfully loaded, either a manual load or banner refresh
 @param adInfo The info of the ad.
 */
- (void)didLoad:(ISBannerView *)bannerView
     withAdInfo:(ISAdInfo *)adInfo {
    logCallbackName();
    
    [self.delegate setAndBindBannerView:bannerView];
    [self.delegate setEnablementForButton:LoadBannerButtonIdentifier
                                   enable:NO];
    [self.delegate setEnablementForButton:DestroyBannerButtonIdentifier
                                   enable:YES];
}

/**
 Called after a banner has attempted to load an ad but failed.
 This delegate will be sent both for manual load and refreshed banner failures.
 @param error The reason for the error.
 */
- (void)didFailToLoadWithError:(NSError *)error {
    logCallbackName(@"%@", error.localizedDescription);
}

/**
 Called after a banner has been clicked.
 @param adInfo The info of the ad.
 */
- (void)didClickWithAdInfo:(ISAdInfo *)adInfo {
    logCallbackName();
}

/**
 Called when a user was taken out of the application context.
 @param adInfo The info of the ad.
 */
- (void)didLeaveApplicationWithAdInfo:(ISAdInfo *)adInfo {
    logCallbackName();
}

/**
 Called when a banner presented a full screen content.
 @param adInfo The info of the ad.
 */
- (void)didPresentScreenWithAdInfo:(ISAdInfo *)adInfo {
    logCallbackName();
}

/**
 Called after a full screen content has been dismissed.
 @param adInfo The info of the ad.
 */
- (void)didDismissScreenWithAdInfo:(ISAdInfo *)adInfo {
    logCallbackName();
}

@end
