//
//  LPMInterstitialAd.m
//  iOSBridge
//

#import "LPMInterstitialAd.h"
#import "LPMInterstitialAdCallbacksWrapper.h"
#import "LPMUtilities.h"
#import <IronSource/LPMInterstitialAd.h>
#import <UIKit/UIKit.h>

#ifdef __cplusplus
extern "C" {
#endif
 
    void *LPMInterstitialAdCreate(const char *adUnitId) {
        
        LPMInterstitialAd *interstitialAd = [[LPMInterstitialAd alloc] initWithAdUnitId:[LPMUtilities getStringFromCString:adUnitId]];
        
        return (__bridge_retained void *)interstitialAd;
    }
    
    void LPMInterstitialAdSetDelegate(void *interstitialAdRef, void *delegateRef) {
        LPMInterstitialAd *interstitialAd = (__bridge LPMInterstitialAd *)interstitialAdRef;
        LPMInterstitialAdCallbacksWrapper *delegate = (__bridge LPMInterstitialAdCallbacksWrapper *)delegateRef;
        [interstitialAd setDelegate:delegate];
    }
    
    void LPMInterstitialAdLoadAd(void *interstitialAdRef) {
        LPMInterstitialAd *interstitialAd = (__bridge LPMInterstitialAd *)interstitialAdRef;
        [interstitialAd loadAd];
    }
    
    void LPMInterstitialAdShowAd(void *interstitialAdRef, const char *placementName) {
        LPMInterstitialAd *interstitialAd = (__bridge LPMInterstitialAd *)interstitialAdRef;
        NSString *placementNameString = placementName ? [LPMUtilities getStringFromCString:placementName] : nil;
        [interstitialAd showAdWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController placementName:placementNameString];
    }

    bool LPMInterstitialAdIsAdReady(void *interstitialAdRef) {
        LPMInterstitialAd *interstitialAd = (__bridge LPMInterstitialAd *)interstitialAdRef;
        return [interstitialAd isAdReady];
    }
    
    bool LPMInterstitialAdIsPlacementCapped(const char *placementName) {
        return [LPMInterstitialAd isPlacementCapped:[LPMUtilities getStringFromCString:placementName]];
    }
#ifdef __cplusplus
}
#endif
