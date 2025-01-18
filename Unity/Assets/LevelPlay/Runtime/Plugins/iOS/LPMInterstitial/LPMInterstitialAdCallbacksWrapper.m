//
//  LPMInterstitialAdCallbacksWrapper.m
//  iOSBridge
//
//  Copyright © 2024 IronSource. All rights reserved.
//

#import "LPMInterstitialAdCallbacksWrapper.h"
#import "LPMUtilities.h"
#import "LPMInitializer.h"

@implementation LPMInterstitialAdCallbacksWrapper

void *LPMInterstitialAdDelegateCreate(void* interstitialAdPtr, DidLoadAdWithAdInfo loadSuccessCallback, DidFailToLoadAdWithAdUnitId loadFailedCallback, DidDisplayAdWithAdInfo displayedCallback, DidFailToDisplayAdWithAdInfo failedToDisplayCallback, DidClickAdWithAdInfo clickedCallback, DidCloseWithAdInfo closedCallback, DidChangeAdInfo changedCallback) {
    LPMInterstitialAdCallbacksWrapper *delegateWrapper =
    [[LPMInterstitialAdCallbacksWrapper alloc]
     initWithLoadSuccessCallback:loadSuccessCallback loadFailedCallback:loadFailedCallback displayedCallback:displayedCallback failedDisplayCallback:failedToDisplayCallback clickedCallback:clickedCallback closedCallback:closedCallback changeAdCallback:changedCallback interstitialAd:interstitialAdPtr];
    
    return (__bridge_retained void *)delegateWrapper;
}

void LPMInterstitialAdDelegateDestroy(void *delegateRef) {
    LPMInterstitialAdCallbacksWrapper *delegateWrapper = (__bridge_transfer LPMInterstitialAdCallbacksWrapper *)delegateRef;
    delegateWrapper.loadSuccess = nil;
    delegateWrapper.loadFail = nil;
    delegateWrapper.displayed = nil;
    delegateWrapper.failedToDisplay = nil;
    
    delegateWrapper.clicked = nil;
    delegateWrapper.closed = nil;
    delegateWrapper.changed = nil;
    delegateWrapper.interstitialAd = nil;
}

- (instancetype)initWithLoadSuccessCallback:(DidLoadAdWithAdInfo)loadSuccessCallback loadFailedCallback:(DidFailToLoadAdWithAdUnitId)loadFailedCallback displayedCallback:(DidDisplayAdWithAdInfo)displayedCallback failedDisplayCallback:(DidFailToDisplayAdWithAdInfo)failedDisplayCallback clickedCallback:(DidClickAdWithAdInfo)clickedCallback closedCallback:(DidCloseWithAdInfo)closedCallback changeAdCallback:(DidChangeAdInfo)changedCallback interstitialAd:(void *)interstitialAd {
    self = [super init];
    if (self) {
        self.loadSuccess = loadSuccessCallback;
        self.loadFail = loadFailedCallback;
        self.displayed = displayedCallback;
        self.failedToDisplay = failedDisplayCallback;
        
        self.clicked = clickedCallback;
        self.closed = closedCallback;
        self.changed = changedCallback;
        
        self.interstitialAd = interstitialAd;
    }
    return self;
}

#pragma mark - LPMInterstitialAdDelegate Methods

- (void)didLoadAdWithAdInfo:(LPMAdInfo *)adInfo {
    NSString *jsonString = [LPMUtilities serializeAdInfoToJSON:adInfo];
    const char *adInfoString = [jsonString UTF8String];
    if (self.loadSuccess) {
        self.loadSuccess(self.interstitialAd, adInfoString);
    }
}

- (void)didFailToLoadAdWithAdUnitId:(NSString *)adUnitId error:(NSError *)error {
    NSString *jsonString = [LPMUtilities serializeErrorToJSON:error adUnitId:adUnitId];
    const char *errorString = [jsonString UTF8String];
    if (self.loadFail) {
        self.loadFail(self.interstitialAd, errorString);
    }
}

- (void)didDisplayAdWithAdInfo:(LPMAdInfo *)adInfo {
    if ([LPMInitializer.sharedInstance isUnityPauseGame]) {
        UnityPause(1);
    }
    NSString *jsonString = [LPMUtilities serializeAdInfoToJSON:adInfo];
    const char *adInfoString = [jsonString UTF8String];
    if (self.displayed) {
        self.displayed(self.interstitialAd, adInfoString);
    }
}

- (void)didFailToDisplayAdWithAdInfo:(LPMAdInfo *)adInfo error:(NSError *)error {
    NSString *jsonString = [LPMUtilities serializeAdInfoToJSON:adInfo];
    const char *adInfoString = [jsonString UTF8String];
    
    NSString *jsonStringError = [LPMUtilities serializeErrorToJSON:error];
    const char *errorString = [jsonStringError UTF8String];
    if (self.failedToDisplay) {
        self.failedToDisplay(self.interstitialAd, adInfoString, errorString);
    }
}

- (void)didClickAdWithAdInfo:(LPMAdInfo *)adInfo {
    NSString *jsonString = [LPMUtilities serializeAdInfoToJSON:adInfo];
    const char *adInfoString = [jsonString UTF8String];
    if (self.clicked) {
        self.clicked(self.interstitialAd, adInfoString);
    }
}

- (void)didCloseAdWithAdInfo:(LPMAdInfo *)adInfo {
    if ([LPMInitializer.sharedInstance isUnityPauseGame]) {
        UnityPause(0);
    }
    NSString *jsonString = [LPMUtilities serializeAdInfoToJSON:adInfo];
    const char *adInfoString = [jsonString UTF8String];
    if (self.closed) {
        self.closed(self.interstitialAd, adInfoString);
    }
}

- (void)didChangeAdInfo:(LPMAdInfo *)adInfo {
    NSString *jsonString = [LPMUtilities serializeAdInfoToJSON:adInfo];
    const char *adInfoString = [jsonString UTF8String];
    if (self.changed) {
        self.changed(self.interstitialAd, adInfoString);
    }
}

@end
