//
//  BannerLevelPlayCallbacksHandler.m
//  IronSourceDemoApp
//
//  Copyright © 2017 IronSource. All rights reserved.


#import "BannerLevelPlayCallbacksHandler.h"

@implementation BannerLevelPlayCallbacksHandler

- (instancetype)initWithDelegate:(id<BannerLevelPlayCallbacksWrapper>)delegate {
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
    [_delegate bannerLevelPlayDidLoad:bannerView
                           withAdInfo:adInfo];
}

/**
 Called after a banner has attempted to load an ad but failed.
 @param error The reason for the error
 */
- (void)didFailToLoadWithError:(NSError *)error {
    [_delegate bannerLevelPlayDidFailToLoadWithError:error];
}

/**
 Called after a banner has been clicked.
 @param adInfo The info of the ad.
 */
- (void)didClickWithAdInfo:(ISAdInfo *)adInfo {
    [_delegate bannerLevelPlayDidClickWithAdInfo:adInfo];
}

/**
 Called when a user was taken out of the application context.
 @param adInfo The info of the ad.
 */
- (void)didLeaveApplicationWithAdInfo:(ISAdInfo *)adInfo {
    [_delegate bannerLevelPlayDidLeaveApplicationWithAdInfo:adInfo];
}

/**
 Called when a banner presented a full screen content.
 @param adInfo The info of the ad.
 */
- (void)didPresentScreenWithAdInfo:(ISAdInfo *)adInfo {
    [_delegate bannerLevelPlayDidPresentScreenWithAdInfo:adInfo];
}

/**
 Called after a full screen content has been dismissed.
 @param adInfo The info of the ad.
 */
- (void)didDismissScreenWithAdInfo:(ISAdInfo *)adInfo {
    [_delegate bannerLevelPlayDidDismissScreenWithAdInfo:adInfo];
}

@end
