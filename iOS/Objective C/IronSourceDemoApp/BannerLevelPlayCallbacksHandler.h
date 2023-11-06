//
//  BannerLevelPlayCallbacksHandler.h
//  IronSourceDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.


#import <Foundation/Foundation.h>
#import <IronSource/IronSource.h>

NS_ASSUME_NONNULL_BEGIN

@protocol BannerLevelPlayCallbacksWrapper <NSObject>

- (void)bannerLevelPlayDidLoad:(ISBannerView *)bannerView
                    withAdInfo:(ISAdInfo *)adInfo;
- (void)bannerLevelPlayDidFailToLoadWithError:(NSError *)error;
- (void)bannerLevelPlayDidClickWithAdInfo:(ISAdInfo *)adInfo;
- (void)bannerLevelPlayDidLeaveApplicationWithAdInfo:(ISAdInfo *)adInfo;
- (void)bannerLevelPlayDidPresentScreenWithAdInfo:(ISAdInfo *)adInfo;
- (void)bannerLevelPlayDidDismissScreenWithAdInfo:(ISAdInfo *)adInfo;

@end

@interface BannerLevelPlayCallbacksHandler : NSObject<LevelPlayBannerDelegate>

@property (nonatomic, weak) id<BannerLevelPlayCallbacksWrapper> delegate;

- (instancetype)initWithDelegate:(id<BannerLevelPlayCallbacksWrapper>)delegate;

@end

NS_ASSUME_NONNULL_END
