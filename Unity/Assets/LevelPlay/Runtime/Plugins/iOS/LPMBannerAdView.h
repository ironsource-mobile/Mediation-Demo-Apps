// LPMBannerAdView.h

#import <Foundation/Foundation.h>
#import <IronSource/IronSource.h>

#ifdef __cplusplus
extern "C" {
#endif

void *LPMBannerAdViewCreate(const char *adUnitId, const char *placementName, const char *description, int width, int height, int customWidth);
void LPMBannerAdViewSetDelegate(void *bannerAdViewRef, void *delegateRef);

void LPMBannerAdViewLoadAd(void *bannerAdViewRef);
void LPMBannerAdViewDestroy(void *bannerAdViewRef);
void LPMBannerAdViewSetPosition(void *bannerAdViewRef, int position);
void LPMBannerAdViewShow(void *bannerAdViewRef);
void LPMBannerAdViewHide(void *bannerAdViewRef);

void LPMBannerAdViewPauseAutoRefresh(void *bannerAdViewRef);
void LPMBannerAdViewResumeAutoRefresh(void *bannerAdViewRef);

#ifdef __cplusplus
}
#endif
