//
//  RewardedVideoLevelPlayCallbacksHandler.h
//  IronSourceDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <IronSource/IronSource.h>

NS_ASSUME_NONNULL_BEGIN

@protocol RewardedVideoLevelPlayCallbacksWrapper <NSObject>

- (void)rewardedVideoLevelPlayHasAvailableAdWithAdInfo:(ISAdInfo *)adInfo;
- (void)rewardedVideoLevelPlayHasNoAvailableAd;
- (void)rewardedVideoLevelPlayDidOpenWithAdInfo:(ISAdInfo *)adInfo;
- (void)rewardedVideoLevelPlayDidFailToShowWithError:(NSError *)error
                                           andAdInfo:(ISAdInfo *)adInfo;
- (void)rewardedVideoLevelPlayDidClick:(ISPlacementInfo *)placementInfo
                            withAdInfo:(ISAdInfo *)adInfo;
- (void)rewardedVideoLevelPlayDidReceiveRewardForPlacement:(ISPlacementInfo *)placementInfo
                                                withAdInfo:(ISAdInfo *)adInfo;
- (void)rewardedVideoLevelPlayDidCloseWithAdInfo:(ISAdInfo *)adInfo;

@end

@interface RewardedVideoLevelPlayCallbacksHandler : NSObject<LevelPlayRewardedVideoDelegate>

@property (nonatomic, weak) id<RewardedVideoLevelPlayCallbacksWrapper> delegate;

- (instancetype)initWithDelegate:(id<RewardedVideoLevelPlayCallbacksWrapper>)delegate;

@end

NS_ASSUME_NONNULL_END

