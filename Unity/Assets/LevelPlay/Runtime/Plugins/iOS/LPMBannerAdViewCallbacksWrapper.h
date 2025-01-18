//
//  LPMBannerAdViewCallbacksWrapper.h
//  iOSBridge
//
// Copyright © 2024 IronSource. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <IronSource/IronSource.h>
#import <IronSource/LPMAdInfo.h>
#import <IronSource/LPMBannerAdViewDelegate.h>

NS_ASSUME_NONNULL_BEGIN
typedef void (*DidLoadAdWithAdInfo)(void *bannerAd, const char* adinfo);
typedef void (*DidFailToLoadAdWithAdUnitId)(void *bannerAd, const char *error);
typedef void (*DidClickAdWithAdInfo)(void *bannerAd, const char *adinfo);
typedef void (*DidDisplayAdWithAdInfo)(void *bannerAd, const char *adinfo);
typedef void (*DidFailToDisplayAdWithAdInfo)(void *bannerAd, const char *adInfo, const char *error);
typedef void (*DidLeaveAppWithAdInfo)(void *bannerAd, const char *adinfo);
typedef void (*DidExpandAdWithAdInfo)(void *bannerAd, const char *adinfo);
typedef void (*DidCollapseAdWithAdInfo)(void *bannerAd, const char *adinfo);

@interface LPMBannerAdViewCallbacksWrapper : NSObject <LPMBannerAdViewDelegate>
@property (nonatomic) void *bannerAd;
@property (assign) DidLoadAdWithAdInfo loadSuccess;
@property (assign) DidFailToLoadAdWithAdUnitId loadFail;
@property (assign) DidClickAdWithAdInfo clicked;
@property (assign) DidDisplayAdWithAdInfo displayed;
@property (assign) DidFailToDisplayAdWithAdInfo failedToDisplay;
@property (assign) DidExpandAdWithAdInfo expand;
@property (assign) DidCollapseAdWithAdInfo collapse;
@property (assign) DidLeaveAppWithAdInfo leaveApp;

- (id)initWithSuccessCallback:(DidLoadAdWithAdInfo)loadSuccess failCallback:(DidFailToLoadAdWithAdUnitId)loadFail clickedCallback:(DidClickAdWithAdInfo)clicked displayedCallback:(DidDisplayAdWithAdInfo)displayed failedToDisplayCallback:(DidFailToDisplayAdWithAdInfo)failedToDisplay expandCallback:(DidExpandAdWithAdInfo)expand collapseCallback:(DidCollapseAdWithAdInfo)collapse leaveAppCallback:(DidLeaveAppWithAdInfo)leaveApp bannerAd:(void *)bannerAd;
@end
NS_ASSUME_NONNULL_END
