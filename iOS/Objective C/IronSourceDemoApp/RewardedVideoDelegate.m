//
//  RewardedVideoDelegate.m
//  IronSourceDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.
//

#import "RewardedVideoDelegate.h"

@implementation RewardedVideoDelegate

- (instancetype)initWithDelegate:(id<DemoViewControllerDelegate>)delegate {
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
    NSLog(@"%s", __PRETTY_FUNCTION__);
    
    [self.delegate performActionForButton:ShowRewardedVideo
                           withEnablement:YES];
}

/**
 Called after a rewarded video has changed its availability to false.
 */
- (void)hasNoAvailableAd {
    NSLog(@"%s", __PRETTY_FUNCTION__);
    
    [self.delegate performActionForButton:ShowRewardedVideo
                           withEnablement:NO];
}

/**
 Called after a rewarded video has been opened.
 @param adInfo The info of the ad.
 */
- (void)didOpenWithAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
    
    [self.delegate performActionForButton:ShowRewardedVideo
                           withEnablement:NO];
}

/**
 Called after a rewarded video has attempted to show but failed.
 @param error The reason for the error
 @param adInfo The info of the ad.
 */
- (void)didFailToShowWithError:(NSError *)error
                     andAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

/**
 Called after a rewarded video has been clicked.
 @param placementInfo An object that contains the placement's reward name and amount.
 @param adInfo The info of the ad.
 */
- (void)didClick:(ISPlacementInfo *)placementInfo
      withAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

/**
 Called after a rewarded video has been viewed completely and the user is eligible for reward.
 @param placementInfo An object that contains the placement's reward name and amount.
 @param adInfo The info of the ad.
 */
- (void)didReceiveRewardForPlacement:(ISPlacementInfo *)placementInfo
                          withAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

/**
 Called after a rewarded video has been dismissed
 @param adInfo The info of the ad.
 */
- (void)didCloseWithAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
    
    [self.delegate showVideoRewardMessage];
}

@end
