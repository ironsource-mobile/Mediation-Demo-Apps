//
//  RewardedVideoLevelPlayCallbacksHandler.m
//  IronSourceDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.
//

#import "RewardedVideoLevelPlayCallbacksHandler.h"

@implementation RewardedVideoLevelPlayCallbacksHandler

- (instancetype)initWithDelegate:(id<RewardedVideoLevelPlayCallbacksWrapper>)delegate {
    self = [super init];
    
    if (self) {
        _delegate = delegate;
    }
    
    return self;
}

/**
 Called after a rewarded video has changed its availability to true.
 @param adInfo The info of the ad.
 */
- (void)hasAvailableAdWithAdInfo:(ISAdInfo *)adInfo {
    [_delegate rewardedVideoLevelPlayHasAvailableAdWithAdInfo:adInfo];
}

/**
 Called after a rewarded video has changed its availability to false.
 */
- (void)hasNoAvailableAd {
    [_delegate rewardedVideoLevelPlayHasNoAvailableAd];
}

/**
 Called after a rewarded video has been opened.
 @param adInfo The info of the ad.
 */
- (void)didOpenWithAdInfo:(ISAdInfo *)adInfo {
    [_delegate rewardedVideoLevelPlayDidOpenWithAdInfo:adInfo];
}

/**
 Called after a rewarded video has attempted to show but failed.
 @param error The reason for the error
 @param adInfo The info of the ad.
 */
- (void)didFailToShowWithError:(NSError *)error
                     andAdInfo:(ISAdInfo *)adInfo {
    [_delegate rewardedVideoLevelPlayDidFailToShowWithError:error
                                                  andAdInfo:adInfo];
}

/**
 Called after a rewarded video has been clicked.
 @param placementInfo An object that contains the placement's reward name and amount.
 @param adInfo The info of the ad.
 */
- (void)didClick:(ISPlacementInfo *)placementInfo
      withAdInfo:(ISAdInfo *)adInfo {
    [_delegate rewardedVideoLevelPlayDidClick:placementInfo
                                   withAdInfo:adInfo];
}

/**
 Called after a rewarded video has been viewed completely and the user is eligible for reward.
 @param placementInfo An object that contains the placement's reward name and amount.
 @param adInfo The info of the ad.
 */
- (void)didReceiveRewardForPlacement:(ISPlacementInfo *)placementInfo
                          withAdInfo:(ISAdInfo *)adInfo {
    [_delegate rewardedVideoLevelPlayDidReceiveRewardForPlacement:placementInfo
                                                       withAdInfo:adInfo];
}

/**
 Called after a rewarded video has been dismissed
 @param adInfo The info of the ad.
 */
- (void)didCloseWithAdInfo:(ISAdInfo *)adInfo {
    [_delegate rewardedVideoLevelPlayDidCloseWithAdInfo:adInfo];
}

@end
