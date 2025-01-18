//
//  LPMRewardedAd.h
//  iOSBridge
//

#import <Foundation/Foundation.h>
#import <IronSource/IronSource.h>

#ifdef __cplusplus
extern "C" {
#endif

void *LPMRewardedAdCreate(const char *adUnitId);
void LPMRewardedAdSetDelegate(void *rewardedAdRef, void *delegateRef);

void LPMRewardedAdLoadAd(void *rewardedAdRef);
void LPMRewardedAdShowAd(void *rewardedAdRef, const char *placementName);
bool LPMRewardedAdIsAdReady(void *rewardedAdRef);

bool LPMRewardedAdIsPlacementCapped(const char *placementName);

#ifdef __cplusplus
}
#endif
