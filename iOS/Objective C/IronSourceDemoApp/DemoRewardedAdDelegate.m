//
//  DemoRewardedVideoAdDelegate.m
//  IronSourceDemoApp
//
//  Copyright © 2024 ironSource Mobile Ltd. All rights reserved.
//

#import "DemoRewardedAdDelegate.h"

@implementation DemoRewardedAdDelegate

- (instancetype)initWithDelegate:(id<DemoViewControllerDelegate>)delegate {
    self = [super init];
    
    if (self) {
        _delegate = delegate;
    }
    
    return self;
}


/**
 Called after a rewarded ad has been loaded
 @param adInfo The info of the ad.
 */
- (void)didLoadAdWithAdInfo:(LPMAdInfo *)adInfo {
    logCallbackName(@"adInfo = %@", adInfo);
    [self.delegate setEnablementForButton:ShowRewardedAdButtonIdentifier
                                   enable:YES];
}

/**
 Called after a rewarded ad has attempted to load but failed.
 @param adUnitId The ad unit id of the ad.
 @param error The reason for the error
 */
- (void)didFailToLoadAdWithAdUnitId:(NSString *)adUnitId error:(NSError *)error {
    logCallbackName(@"error = %@ | adUnitId = %@", error.localizedDescription, adUnitId);
    [self.delegate setEnablementForButton:ShowRewardedAdButtonIdentifier
                                   enable:NO];
}

/**
 Called after a rewarded ad has attempted to load but failed.
 @param adUnitId The ad unit id of the ad.
 @param error The reason for the error
 */
- (void)didChangeAdInfo:(LPMAdInfo *)adInfo {
    logCallbackName(@"adInfo = %@", adInfo);
    [self.delegate setEnablementForButton:ShowRewardedAdButtonIdentifier
                                   enable:NO];
}

/**
 Called after a rewarded ad has been displayed.
 This is the indication for impression.
 @param adInfo The info of the ad.
 */
- (void)didDisplayAdWithAdInfo:(LPMAdInfo *)adInfo {
    logCallbackName(@"adInfo = %@", adInfo);
    [self.delegate setEnablementForButton:ShowRewardedAdButtonIdentifier
                                   enable:NO];
}

/**
 Called after a rewarded video has attempted to show but failed.
 @param error The reason for the error.
 @param adInfo The info of the ad.
 */
- (void)didFailToDisplayAdWithAdInfo:(LPMAdInfo *)adInfo error:(NSError *)error {
    logCallbackName(@"error = %@ | adInfo = %@", error.localizedDescription, adInfo);
}

/**
 Called after a rewarded ad has been clicked.
 @param adInfo The info of the ad.
 */
- (void)didClickAdWithAdInfo:(LPMAdInfo *)adInfo {
    logCallbackName(@"adInfo = %@", adInfo);
}

/**
 Called after a rewarded ad has been dismissed.
 @param adInfo The info of the ad.
 */
- (void)didCloseAdWithAdInfo:(LPMAdInfo *)adInfo {
    logCallbackName(@"adInfo = %@", adInfo);
    [self.delegate showVideoRewardMessage];
}

/**
 Called after a rewarded ad has been viewed completely and the user is eligible for a reward.
 @param adInfo The info of the ad
 @param reward The reward info
 */
- (void)didRewardAdWithAdInfo:(LPMAdInfo *)adInfo reward:(LPMReward *)reward {
    logCallbackName(@"adInfo = %@", adInfo);
    [self.delegate setRewardInfo:reward];
}

@end
