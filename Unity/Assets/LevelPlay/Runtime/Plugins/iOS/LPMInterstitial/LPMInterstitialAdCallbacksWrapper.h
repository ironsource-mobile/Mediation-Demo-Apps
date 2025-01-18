//
//  LPMInterstitialAdCallbacksWrapper.h
//  iOSBridge
//

#import <Foundation/Foundation.h>
#import <IronSource/IronSource.h>
#import <IronSource/LPMAdInfo.h>
#import <IronSource/LPMInterstitialAdDelegate.h>
#import "iOSBridge.h"

NS_ASSUME_NONNULL_BEGIN
typedef void (*DidLoadAdWithAdInfo)(void *interstitialAd, const char* adinfo);
typedef void (*DidFailToLoadAdWithAdUnitId)(void *interstitialAd, const char *error);
typedef void (*DidDisplayAdWithAdInfo)(void *interstitialAd, const char *adinfo);
typedef void (*DidFailToDisplayAdWithAdInfo)(void *interstitialAd, const char *adInfo, const char *error);

typedef void (*DidClickAdWithAdInfo)(void *interstitialAd, const char *adinfo);
typedef void (*DidCloseWithAdInfo)(void *interstitialAd, const char *adinfo);
typedef void (*DidChangeAdInfo)(void *interstitialAd, const char *adinfo);

@interface LPMInterstitialAdCallbacksWrapper : NSObject <LPMInterstitialAdDelegate>
@property (nonatomic) void *interstitialAd;
@property (assign) DidLoadAdWithAdInfo loadSuccess;
@property (assign) DidFailToLoadAdWithAdUnitId loadFail;
@property (assign) DidDisplayAdWithAdInfo displayed;
@property (assign) DidFailToDisplayAdWithAdInfo failedToDisplay;

@property (assign) DidClickAdWithAdInfo clicked;
@property (assign) DidCloseWithAdInfo closed;
@property (assign) DidChangeAdInfo changed;

- (id)loadSuccessCallback:(DidLoadAdWithAdInfo)loadSuccess loadFailedCallback:(DidFailToLoadAdWithAdUnitId)loadFail displayedCallback:(DidDisplayAdWithAdInfo)displayed failedToDisplayCallback:(DidFailToDisplayAdWithAdInfo)failedToDisplay clickedCallback:(DidClickAdWithAdInfo)clicked closedCallback:(DidCloseWithAdInfo)closed changeAdCallback:(DidChangeAdInfo)changed interstitialAd:(void *)interstitialAd;
@end
NS_ASSUME_NONNULL_END
