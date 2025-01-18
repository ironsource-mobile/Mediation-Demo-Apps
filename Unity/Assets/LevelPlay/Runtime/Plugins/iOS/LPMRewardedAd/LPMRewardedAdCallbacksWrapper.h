//
//  LPMRewardedAdCallbacksWrapper.h
//  iOSBridge
//

#import <Foundation/Foundation.h>
#import <IronSource/IronSource.h>
#import <IronSource/LPMAdInfo.h>
#import <IronSource/LPMRewardedAdDelegate.h>
#import "iOSBridge.h"

NS_ASSUME_NONNULL_BEGIN
typedef void (*DidLoadAdWithAdInfo)(void *rewardedAd, const char* adinfo);
typedef void (*DidFailToLoadAdWithAdUnitId)(void *rewardedAd, const char *error);
typedef void (*DidDisplayAdWithAdInfo)(void *rewardedAd, const char *adinfo);
typedef void (*DidFailToDisplayAdWithAdInfo)(void *rewardedAd, const char *adInfo, const char *error);
typedef void (*DidAdRewardedWithAdInfo)(void *rewardedAd, const char *adInfo, const char *rewardName, int *rewardAmount);
typedef void (*DidClickAdWithAdInfo)(void *rewardedAd, const char *adinfo);
typedef void (*DidCloseWithAdInfo)(void *rewardedAd, const char *adinfo);
typedef void (*DidChangeAdInfo)(void *rewardedAd, const char *adinfo);

@interface LPMRewardedAdCallbacksWrapper : NSObject <LPMRewardedAdDelegate>
@property (nonatomic) void *rewardedAd;
@property (assign) DidLoadAdWithAdInfo loadSuccess;
@property (assign) DidFailToLoadAdWithAdUnitId loadFail;
@property (assign) DidDisplayAdWithAdInfo displayed;
@property (assign) DidFailToDisplayAdWithAdInfo failedToDisplay;
@property (assign) DidAdRewardedWithAdInfo rewarded;
@property (assign) DidClickAdWithAdInfo clicked;
@property (assign) DidCloseWithAdInfo closed;
@property (assign) DidChangeAdInfo changed;

- (instancetype)initWithLoadSuccessCallback:(DidLoadAdWithAdInfo)loadSuccess
                        loadFailedCallback:(DidFailToLoadAdWithAdUnitId)loadFail
                        displayedCallback:(DidDisplayAdWithAdInfo)displayed
                        failedToDisplayCallback:(DidFailToDisplayAdWithAdInfo)failedToDisplay
                        rewardedCallback:(DidAdRewardedWithAdInfo)rewarded
                        clickedCallback:(DidClickAdWithAdInfo)clicked
                        closedCallback:(DidCloseWithAdInfo)closed
                        changeAdCallback:(DidChangeAdInfo)changed
                        rewardedAd:(void *)rewardedAd;

@end
NS_ASSUME_NONNULL_END
