//
//  DemoRewardedVideoAdDelegate.m
//  IronSourceDemoApp
//
//  Copyright © 2024 ironSource Mobile Ltd. All rights reserved.
//

#import "DemoRewardedVideoAdDelegate.h"

@implementation DemoRewardedVideoAdDelegate

- (instancetype)initWithDelegate:(id<DemoViewControllerDelegate>)delegate {
    self = [super init];
    
    if (self) {
        _delegate = delegate;
    }
    
    return self;
}

/**
 Called after a rewarded has been loaded
 @param adInfo The info of the ad.
 */
- (void)didLoadAdWithAdInfo:(nonnull LPMAdInfo *)adInfo {
    logCallbackName(@"adInfo = %@", adInfo);
    [self.delegate setEnablementForButton:ShowRewardedVideoButtonIdentifier
                                   enable:YES];
}

/**
 Called after a rewarded has attempted to load but failed.
 @param adUnitId The ad unit id of the ad.
 @param error The reason for the error
 */
- (void)didFailToLoadAdWithAdUnitId:(nonnull NSString *)adUnitId error:(nonnull NSError *)error {
    logCallbackName(@"error = %@", error.localizedDescription);
    [self.delegate setEnablementForButton:ShowRewardedVideoButtonIdentifier
                                    enable:NO];
}

/**
 Called after the ad info is updated, Available when another rewarded ad has loaded, and includes a higher CPM/Rate
 @param adInfo The info of the ad.
 */
- (void) didChangeAdInfo:(nonnull LPMAdInfo *)adInfo {
    logCallbackName(@"adInfo =%@", adInfo)
}

/**
 Called after a rewarded ad has been opened..
 This is the indication for impression.
 @param adInfo The info of the ad.
 */
- (void)didDisplayAdWithAdInfo:(nonnull LPMAdInfo *)adInfo {
    logCallbackName(@"adInfo =%@", adInfo)
    [self.delegate setEnablementForButton:ShowRewardedVideoButtonIdentifier
                                    enable:NO];
}

/**
 Called after a rewarded has attempted to show but failed.
 @param adInfo The info of the ad.
 @param error The reason for the error.
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
 Called after a rewarded ad has been viewed completely and the user is eligible for a reward.
 @param placementInfo An object that contains the placement's reward name and amount.
 @param adInfo The info of the ad.
 */
- (void)didRewardAdWithAdInfo:(nonnull LPMAdInfo *)adInfo reward:(nonnull LPMReward *)reward {
    logCallbackName(@"adInfo = %@", adInfo);
    [self.delegate setReward:reward];
}

/**
 Called after a rewarded ad has been dismissed.
 @param adInfo The info of the ad.
 */
- (void)didCloseAdWithAdInfo:(LPMAdInfo *)adInfo {
    logCallbackName(@"adInfo = %@", adInfo);
    [self.delegate showVideoRewardMessage];
}

@end
