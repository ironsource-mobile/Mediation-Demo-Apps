//
//  LPMRewardedAd.m
//  iOSBridge
//

#import "LPMRewardedAd.h"
#import "LPMRewardedAdCallbacksWrapper.h"
#import "LPMUtilities.h"
#import <IronSource/LPMRewardedAd.h>
#import <UIKit/UIKit.h>

#ifdef __cplusplus
extern "C" {
#endif
 
    void *LPMRewardedAdCreate(const char *adUnitId) {
        
        LPMRewardedAd *rewardedAd = [[LPMRewardedAd alloc] initWithAdUnitId:[LPMUtilities getStringFromCString:adUnitId]];
        
        return (__bridge_retained void *)rewardedAd;
    }
    
    void LPMRewardedAdSetDelegate(void *rewardedAdRef, void *delegateRef) {
        LPMRewardedAd *rewardedAd = (__bridge LPMRewardedAd *)rewardedAdRef;
        LPMRewardedAdCallbacksWrapper *delegate = (__bridge LPMRewardedAdCallbacksWrapper *)delegateRef;
        [rewardedAd setDelegate:delegate];
    }
    
    void LPMRewardedAdLoadAd(void *rewardedAdRef) {
        LPMRewardedAd *rewardedAd = (__bridge LPMRewardedAd *)rewardedAdRef;
        [rewardedAd loadAd];
    }
    
    void LPMRewardedAdShowAd(void *rewardedAdRef, const char *placementName) {
        LPMRewardedAd *rewardedAd = (__bridge LPMRewardedAd *)rewardedAdRef;
        NSString *placementNameString = placementName ? [LPMUtilities getStringFromCString:placementName] : nil;
        [rewardedAd showAdWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController placementName:placementNameString];
    }

    bool LPMRewardedAdIsAdReady(void *rewardedAdRef) {
        LPMRewardedAd *rewardedAd = (__bridge LPMRewardedAd *)rewardedAdRef;
        return [rewardedAd isAdReady];
    }
    
    bool LPMRewardedAdIsPlacementCapped(const char *placementName) {
        return [LPMRewardedAd isPlacementCapped:[LPMUtilities getStringFromCString:placementName]];
    }
#ifdef __cplusplus
}
#endif
