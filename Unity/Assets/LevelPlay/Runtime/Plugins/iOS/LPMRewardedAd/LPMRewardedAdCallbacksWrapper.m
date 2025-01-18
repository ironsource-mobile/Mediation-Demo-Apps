//
//  LPMRewardedAdCallbacksWrapper.m
//  iOSBridge
//
//  Copyright © 2024 IronSource. All rights reserved.
//

#import "LPMRewardedAdCallbacksWrapper.h"
#import "LPMUtilities.h"
#import "LPMInitializer.h"

@implementation LPMRewardedAdCallbacksWrapper

void *LPMRewardedAdDelegateCreate(void* rewardedAdPtr, DidLoadAdWithAdInfo loadSuccessCallback, DidFailToLoadAdWithAdUnitId loadFailedCallback, DidDisplayAdWithAdInfo displayedCallback, DidFailToDisplayAdWithAdInfo failedToDisplayCallback, DidAdRewardedWithAdInfo rewardedCallback, DidClickAdWithAdInfo clickedCallback, DidCloseWithAdInfo closedCallback, DidChangeAdInfo changedCallback) {
    if (!rewardedAdPtr) return NULL;  // Ensure rewardedAdPtr is non-null

    LPMRewardedAdCallbacksWrapper *delegateWrapper = [[LPMRewardedAdCallbacksWrapper alloc] initWithLoadSuccessCallback:loadSuccessCallback loadFailedCallback:loadFailedCallback displayedCallback:displayedCallback failedToDisplayCallback:failedToDisplayCallback rewardedCallback:rewardedCallback clickedCallback:clickedCallback closedCallback:closedCallback changeAdCallback:changedCallback rewardedAd:rewardedAdPtr];
    
    return (__bridge_retained void *)delegateWrapper;
}

void LPMRewardedAdDelegateDestroy(void *delegateRef) {
    LPMRewardedAdCallbacksWrapper *delegateWrapper = (__bridge_transfer LPMRewardedAdCallbacksWrapper *)delegateRef;
    delegateWrapper.loadSuccess = nil;
    delegateWrapper.loadFail = nil;
    delegateWrapper.displayed = nil;
    delegateWrapper.failedToDisplay = nil;
    delegateWrapper.rewarded = nil;
    delegateWrapper.clicked = nil;
    delegateWrapper.closed = nil;
    delegateWrapper.changed = nil;
    delegateWrapper.rewardedAd = nil;
}

- (instancetype)initWithLoadSuccessCallback:(DidLoadAdWithAdInfo)loadSuccessCallback loadFailedCallback:(DidFailToLoadAdWithAdUnitId)loadFailedCallback displayedCallback:(DidDisplayAdWithAdInfo)displayedCallback failedToDisplayCallback:(DidFailToDisplayAdWithAdInfo)failedToDisplayCallback rewardedCallback:(DidAdRewardedWithAdInfo)rewardedCallback clickedCallback:(DidClickAdWithAdInfo)clickedCallback closedCallback:(DidCloseWithAdInfo)closedCallback changeAdCallback:(DidChangeAdInfo)changedCallback rewardedAd:(void *)rewardedAd {
    self = [super init];
    if (self) {
        self.loadSuccess = loadSuccessCallback;
        self.loadFail = loadFailedCallback;
        self.displayed = displayedCallback;
        self.failedToDisplay = failedToDisplayCallback;
        self.rewarded = rewardedCallback;
        self.clicked = clickedCallback;
        self.closed = closedCallback;
        self.changed = changedCallback;
        
        self.rewardedAd = rewardedAd;
    }
    return self;
}

#pragma mark - LPMRewardedAdDelegate Methods

- (void)didLoadAdWithAdInfo:(LPMAdInfo *)adInfo {
    NSString *jsonString = [LPMUtilities serializeAdInfoToJSON:adInfo];
    const char *adInfoString = [jsonString UTF8String];
    if (self.loadSuccess) {
        self.loadSuccess(self.rewardedAd, adInfoString);
    }
}

- (void)didFailToLoadAdWithAdUnitId:(NSString *)adUnitId error:(NSError *)error {
    NSString *jsonString = [LPMUtilities serializeErrorToJSON:error adUnitId:adUnitId];
    const char *errorString = [jsonString UTF8String];
    if (self.loadFail) {
        self.loadFail(self.rewardedAd, errorString);
    }
}

- (void)didDisplayAdWithAdInfo:(LPMAdInfo *)adInfo {
   if ([LPMInitializer.sharedInstance isUnityPauseGame]) {
        UnityPause(1);
    }
    NSString *jsonString = [LPMUtilities serializeAdInfoToJSON:adInfo];
    const char *adInfoString = [jsonString UTF8String];
    if (self.displayed) {
        self.displayed(self.rewardedAd, adInfoString);
    }
}

- (void)didFailToDisplayAdWithAdInfo:(LPMAdInfo *)adInfo error:(NSError *)error {
    NSString *jsonString = [LPMUtilities serializeAdInfoToJSON:adInfo];
    const char *adInfoString = [jsonString UTF8String];
    
    NSString *jsonStringError = [LPMUtilities serializeErrorToJSON:error];
    const char *errorString = [jsonStringError UTF8String];
    if (self.failedToDisplay) {
        self.failedToDisplay(self.rewardedAd, adInfoString, errorString);
    }
}

- (void)didRewardAdWithAdInfo:(LPMAdInfo *)adInfo reward:(LPMReward *)reward {
    NSString *jsonString = [LPMUtilities serializeAdInfoToJSON:adInfo];
    const char *adInfoString = [jsonString UTF8String];
    
    const char *rewardName = [reward.name UTF8String];
    int rewardAmount = (int)reward.amount;
    if (self.rewarded) {
        self.rewarded(self.rewardedAd, adInfoString, rewardName, &rewardAmount);
    }
}

- (void)didClickAdWithAdInfo:(LPMAdInfo *)adInfo {
    NSString *jsonString = [LPMUtilities serializeAdInfoToJSON:adInfo];
    const char *adInfoString = [jsonString UTF8String];
    if (self.clicked) {
        self.clicked(self.rewardedAd, adInfoString);
    }
}

- (void)didCloseAdWithAdInfo:(LPMAdInfo *)adInfo {
   if ([LPMInitializer.sharedInstance isUnityPauseGame]) {
        UnityPause(0);
    }
    NSString *jsonString = [LPMUtilities serializeAdInfoToJSON:adInfo];
    const char *adInfoString = [jsonString UTF8String];
    if (self.closed) {
        self.closed(self.rewardedAd, adInfoString);
    }
}

- (void)didChangeAdInfo:(LPMAdInfo *)adInfo {
    NSString *jsonString = [LPMUtilities serializeAdInfoToJSON:adInfo];
    const char *adInfoString = [jsonString UTF8String];
    if (self.changed) {
        self.changed(self.rewardedAd, adInfoString);
    }
}

@end
