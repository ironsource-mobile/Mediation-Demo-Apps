//
//  LPMInterstitialAd.h
//  iOSBridge
//

#import <Foundation/Foundation.h>
#import <IronSource/IronSource.h>

#ifdef __cplusplus
extern "C" {
#endif

void *LPMInterstitialAdCreate(const char *adUnitId);
void LPMInterstitialAdSetDelegate(void *interstitialAdRef, void *delegateRef);

void LPMInterstitialAdLoadAd(void *interstitialAdRef);
void LPMInterstitialAdShowAd(void *interstitialAdRef, const char *placementName);
bool LPMInterstitialAdIsAdReady(void *interstitialAdRef);

bool LPMInterstitialAdIsPlacementCapped(const char *placementName);

#ifdef __cplusplus
}
#endif
