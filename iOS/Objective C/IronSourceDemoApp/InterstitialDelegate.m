//
//  InterstitialDelegate.m
//  IronSourceDemoApp
//
//  Copyright © 2024 ironSource Mobile Ltd. All rights reserved.
//

#import "InterstitialDelegate.h"

@implementation InterstitialDelegate

- (instancetype)initWithDelegate:(id<DemoViewControllerDelegate>)delegate {
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
    logCallbackName();
    
    [self.delegate setEnablementForButton:ShowInterstitialButtonIdentifier
                                   enable:YES];
}

/**
 Called after an interstitial has attempted to load but failed.
 @param error The reason for the error
 */
- (void)didFailToLoadWithError:(NSError *)error {
    logCallbackName(@"%@", error.localizedDescription);
    
    [self.delegate setEnablementForButton:ShowInterstitialButtonIdentifier
                                   enable:NO];
}

/**
 Called after an interstitial has been opened.
 This is the indication for impression.
 @param adInfo The info of the ad.
 */
- (void)didOpenWithAdInfo:(ISAdInfo *)adInfo {
    logCallbackName();
    
    [self.delegate setEnablementForButton:ShowInterstitialButtonIdentifier
                                   enable:NO];
}

/**
 Called after an interstitial has been displayed on the screen.
 This callback is not supported by all networks, and we recommend using it
 only if it's supported by all networks you included in your build.
 @param adInfo The info of the ad.
 */
- (void)didShowWithAdInfo:(ISAdInfo *)adInfo {
    logCallbackName();
}

/**
 Called after an interstitial has attempted to show but failed.
 @param error The reason for the error.
 @param adInfo The info of the ad.
 */
- (void)didFailToShowWithError:(NSError *)error
                     andAdInfo:(ISAdInfo *)adInfo {
    logCallbackName(@"%@", error.localizedDescription);
}

/**
 Called after an interstitial has been clicked.
 @param adInfo The info of the ad.
 */
- (void)didClickWithAdInfo:(ISAdInfo *)adInfo {
    logCallbackName();
}

/**
 Called after an interstitial has been dismissed.
 @param adInfo The info of the ad.
 */
- (void)didCloseWithAdInfo:(ISAdInfo *)adInfo {
    logCallbackName();
}

@end
