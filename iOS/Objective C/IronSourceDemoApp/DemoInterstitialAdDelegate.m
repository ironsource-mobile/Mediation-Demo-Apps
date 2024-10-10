//
//  DemoInterstitialAdDelegate.m
//  IronSourceDemoApp
//
//  Copyright © 2024 ironSource Mobile Ltd. All rights reserved.
//

#import "DemoInterstitialAdDelegate.h"

@implementation DemoInterstitialAdDelegate

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
- (void)didLoadAdWithAdInfo:(LPMAdInfo *)adInfo {
    logCallbackName(@"adInfo = %@", adInfo);
    [self.delegate setEnablementForButton:ShowInterstitialButtonIdentifier
                                   enable:YES];
}

/**
 Called after an interstitial has attempted to load but failed.
 @param adUnitId The ad unit id of the ad.
 @param error The reason for the error
 */
- (void)didFailToLoadAdWithAdUnitId:(NSString *)adUnitId error:(NSError *)error {
    logCallbackName(@"error = %@ | adUnitId = %@", error.localizedDescription, adUnitId);
    [self.delegate setEnablementForButton:ShowInterstitialButtonIdentifier
                                   enable:NO];
}

/**
 Called after an interstitial has attempted to load but failed.
 @param adUnitId The ad unit id of the ad.
 @param error The reason for the error
 */
- (void)didChangeAdInfo:(LPMAdInfo *)adInfo {
    logCallbackName(@"adInfo = %@", adInfo);
    [self.delegate setEnablementForButton:ShowInterstitialButtonIdentifier
                                   enable:NO];
}

/**
 Called after an interstitial has been displayed.
 This is the indication for impression.
 @param adInfo The info of the ad.
 */
- (void)didDisplayAdWithAdInfo:(LPMAdInfo *)adInfo {
    logCallbackName(@"adInfo = %@", adInfo);
    [self.delegate setEnablementForButton:ShowInterstitialButtonIdentifier
                                   enable:NO];
}

/**
 Called after an interstitial has attempted to show but failed.
 @param error The reason for the error.
 @param adInfo The info of the ad.
 */
- (void)didFailToDisplayAdWithAdInfo:(LPMAdInfo *)adInfo error:(NSError *)error {
    logCallbackName(@"error = %@ | adInfo = %@", error.localizedDescription, adInfo);
}

/**
 Called after an interstitial has been clicked.
 @param adInfo The info of the ad.
 */
- (void)didClickAdWithAdInfo:(LPMAdInfo *)adInfo {
    logCallbackName(@"adInfo = %@", adInfo);
}

/**
 Called after an interstitial has been dismissed.
 @param adInfo The info of the ad.
 */
- (void)didCloseAdWithAdInfo:(LPMAdInfo *)adInfo {
    logCallbackName(@"adInfo = %@", adInfo);
}




@end
