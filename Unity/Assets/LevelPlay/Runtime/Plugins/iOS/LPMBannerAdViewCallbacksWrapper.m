//
//  LPMBannerAdViewCallbacksWrapper.m
//  iOSBridge
//
// Copyright © 2024 IronSource. All rights reserved.
//

#import "LPMBannerAdViewCallbacksWrapper.h"
#import "LPMUtilities.h"

@implementation LPMBannerAdViewCallbacksWrapper

void *LPMBannerAdViewDelegateCreate(void* bannerAdPtr, DidLoadAdWithAdInfo loadSuccessCallback, DidFailToLoadAdWithAdUnitId loadFailedCallback, DidClickAdWithAdInfo clickedCallback, DidDisplayAdWithAdInfo displayedCallback, DidFailToDisplayAdWithAdInfo failedDisplayCallback, DidExpandAdWithAdInfo expandedCallback, DidCollapseAdWithAdInfo collapsedcallback, DidLeaveAppWithAdInfo leftAppCallback) {
    LPMBannerAdViewCallbacksWrapper *delegateWrapper =
        [[LPMBannerAdViewCallbacksWrapper alloc]
        initWithSuccessCallback:loadSuccessCallback
        failCallback:loadFailedCallback
        clickedCallback:clickedCallback
        displayedCallback:displayedCallback
        failedDisplayCallback:failedDisplayCallback
        expandedCallback:expandedCallback
        collapsedCallback:collapsedcallback
        leftAppCallback:leftAppCallback
        BannerAd:bannerAdPtr];

    return (__bridge_retained void *)delegateWrapper;
}

void LPMBannerAdViewDelegateDestroy(void *delegateRef) {
    LPMBannerAdViewCallbacksWrapper *delegateWrapper = (__bridge_transfer LPMBannerAdViewCallbacksWrapper *)delegateRef;
    delegateWrapper.loadSuccess = nil;
    delegateWrapper.loadFail = nil;
    delegateWrapper.clicked = nil;
    delegateWrapper.displayed = nil;
    delegateWrapper.failedToDisplay = nil;
    delegateWrapper.expand = nil;
    delegateWrapper.collapse = nil;
    delegateWrapper.leaveApp = nil;
    delegateWrapper.bannerAd = nil;
    }

 - (instancetype)initWithSuccessCallback:(DidLoadAdWithAdInfo)loadSuccessCallback failCallback:(DidFailToLoadAdWithAdUnitId)loadFailedCallback clickedCallback:(DidClickAdWithAdInfo)clickedCallback displayedCallback:(DidDisplayAdWithAdInfo)displayedCallback failedDisplayCallback:(DidFailToDisplayAdWithAdInfo)failedDisplayCallback expandedCallback:(DidExpandAdWithAdInfo)expandedCallback collapsedCallback:(DidCollapseAdWithAdInfo)collapsedCallback leftAppCallback:(DidLeaveAppWithAdInfo)leftAppCallback BannerAd:(void *)bannerAd{
     self = [super init];
     if (self) {
         self.loadSuccess = loadSuccessCallback;
         self.loadFail = loadFailedCallback;
         self.clicked = clickedCallback;
         self.displayed = displayedCallback;
         self.failedToDisplay = failedDisplayCallback;
         self.expand = expandedCallback;
         self.collapse = collapsedCallback;
         self.leaveApp = leftAppCallback;
         self.bannerAd = bannerAd;
     }
     return self;
 }

#pragma mark - LPMBannerAdViewDelegate Methods

- (void)didLoadAdWithAdInfo:(LPMAdInfo *)adInfo {
    NSString *jsonString = [LPMUtilities serializeAdInfoToJSON:adInfo];
    const char *adInfoString = [jsonString UTF8String];
    if (self.loadSuccess) {
        self.loadSuccess( self.bannerAd, adInfoString);
    }

}

- (void)didFailToLoadAdWithAdUnitId:(NSString *)adUnitId error:(NSError *)error {
    NSString *jsonString = [LPMUtilities serializeErrorToJSON:error adUnitId:adUnitId];
    const char *errorString = [jsonString UTF8String];
    if (self.loadFail){
        self.loadFail(self.bannerAd, errorString);
    }
}

- (void)didClickAdWithAdInfo:(LPMAdInfo *)adInfo {
    NSString *jsonString = [LPMUtilities serializeAdInfoToJSON:adInfo];
    const char *adInfoString = [jsonString UTF8String];
   if (self.clicked) {
         self.clicked( self.bannerAd, adInfoString);
     }
}

- (void)didDisplayAdWithAdInfo:(LPMAdInfo *)adInfo {
    NSString *jsonString = [LPMUtilities serializeAdInfoToJSON:adInfo];
    const char *adInfoString = [jsonString UTF8String];
  if (self.displayed) {
           self.displayed(self.bannerAd, adInfoString);
       }
}

- (void)didFailToDisplayAdWithAdInfo:(LPMAdInfo *)adInfo error:(NSError *)error {
    NSString *jsonString = [LPMUtilities serializeAdInfoToJSON:adInfo];
    const char *adInfoString = [jsonString UTF8String];

    NSString *jsonStringError = [LPMUtilities serializeErrorToJSON:error];
    const char *errorString = [jsonStringError UTF8String];
    if(self.failedToDisplay) {
        self.failedToDisplay(self.bannerAd, adInfoString, errorString);
    }
}

- (void)didExpandAdWithAdInfo:(LPMAdInfo *)adInfo {
    NSString *jsonString = [LPMUtilities serializeAdInfoToJSON:adInfo];
    const char *adInfoString = [jsonString UTF8String];
    if(self.expand) {
        self.expand(self.bannerAd, adInfoString);
    }
}

- (void)didCollapseAdWithAdInfo:(LPMAdInfo *)adInfo {
    NSString *jsonString = [LPMUtilities serializeAdInfoToJSON:adInfo];
    const char *adInfoString = [jsonString UTF8String];
    if(self.collapse) {
        self.collapse(self.bannerAd, adInfoString);
    }
}

- (void)didLeaveAppWithAdInfo:(LPMAdInfo *)adInfo {
    NSString *jsonString = [LPMUtilities serializeAdInfoToJSON:adInfo];
    const char *adInfoString = [jsonString UTF8String];
    if(self.leaveApp) {
        self.leaveApp(self.bannerAd, adInfoString);
    }
}

@end
