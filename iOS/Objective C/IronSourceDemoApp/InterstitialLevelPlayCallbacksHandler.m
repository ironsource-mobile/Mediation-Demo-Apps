//
//  InterstitialLevelPlayCallbacksHandler.m
//  IronSourceDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.
//

#import "InterstitialLevelPlayCallbacksHandler.h"

@implementation InterstitialLevelPlayCallbacksHandler

- (instancetype)initWithDelegate:(id<InterstitialLevelPlayCallbacksWrapper>)delegate {
    self = [super init];
    
    if (self) {
        _delegate = delegate;
    }
    
    return self;
}

/**
 Called after an interstitial has been loaded
 @param adInfo The info of the ad.
 */
- (void)didLoadWithAdInfo:(ISAdInfo *)adInfo {
    [_delegate interstitialLevelPlayDidLoadWithAdInfo:adInfo];
}

/**
 Called after an interstitial has attempted to load but failed.
 @param error The reason for the error
 */
- (void)didFailToLoadWithError:(NSError *)error {
    [_delegate interstitialLevelPlayDidFailToLoadWithError:error];
}

/**
 Called after an interstitial has been opened.
 @param adInfo The info of the ad.
 */
- (void)didOpenWithAdInfo:(ISAdInfo *)adInfo {
    [_delegate interstitialLevelPlayDidOpenWithAdInfo:adInfo];
}

/**
 Called after an interstitial has been displayed on the screen.
 @param adInfo The info of the ad.
 */
- (void)didShowWithAdInfo:(ISAdInfo *)adInfo {
    [_delegate interstitialLevelPlayDidShowWithAdInfo:adInfo];
}

/**
 Called after an interstitial has attempted to show but failed.
 @param error The reason for the error
 @param adInfo The info of the ad.
 */
- (void)didFailToShowWithError:(NSError *)error
                     andAdInfo:(ISAdInfo *)adInfo {
    [_delegate interstitialLevelPlayDidFailToShowWithError:error
                                                 andAdInfo:adInfo];
}

/**
 Called after an interstitial has been clicked.
 @param adInfo The info of the ad.
 */
- (void)didClickWithAdInfo:(ISAdInfo *)adInfo {
    [_delegate interstitialLevelPlayDidClickWithAdInfo:adInfo];
}

/**
 Called after an interstitial has been dismissed.
 @param adInfo The info of the ad.
 */
- (void)didCloseWithAdInfo:(ISAdInfo *)adInfo {
    [_delegate interstitialLevelPlayDidCloseWithAdInfo:adInfo];
}

@end
