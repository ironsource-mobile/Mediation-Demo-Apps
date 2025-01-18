#import "LPMBannerAdView.h"
#import "LPMBannerAdViewCallbacksWrapper.h"
#import "LPMUtilities.h"
#import <IronSource/LPMBannerAdView.h>
#import <UIKit/UIKit.h>


CGFloat getDeviceScreenWidth(void) {
    UIScreen *mainScreen = [UIScreen mainScreen];
    CGFloat screenWidthPoints = mainScreen.bounds.size.width;
    if (@available(iOS 11.0, *)) {
        UIWindow *mainWindow = [[UIWindow alloc] initWithFrame:mainScreen.bounds];
        UILayoutGuide *safeAreaLayoutGuide = mainWindow.safeAreaLayoutGuide;
        screenWidthPoints = safeAreaLayoutGuide.layoutFrame.size.width;
    }
    return screenWidthPoints;
}

LPMAdSize *GetAdSizeFromDescription(const char *description, int width,
                                    int height, int customWidth) {
    NSString *desc = [LPMUtilities getStringFromCString:description];
    
    if (strcmp(description, "CUSTOM") == 0) {
        return [LPMAdSize customSizeWithWidth:width height:height];
    } else if ([desc isEqualToString:@"BANNER"]) {
        return [LPMAdSize bannerSize];
    } else if ([desc isEqualToString:@"LARGE"]) {
        return [LPMAdSize largeSize];
    } else if ([desc isEqualToString:@"MEDIUM_RECTANGLE"]) {
        return [LPMAdSize mediumRectangleSize];
    } else if ([desc isEqualToString:@"LEADERBOARD"]) {
        return [LPMAdSize leaderBoardSize];
    } else if ([desc isEqualToString:@"ADAPTIVE"]) {
        if (customWidth > 0) {
            return [LPMAdSize createAdaptiveAdSizeWithWidth: customWidth];
        } else {
            return [LPMAdSize createAdaptiveAdSize];
        }
    } else {
        return [LPMAdSize bannerSize];
    }
}

#ifdef __cplusplus
extern "C" {
#endif
    
    void *LPMBannerAdViewCreate(const char *adUnitId, const char *placementName,
                                const char *description, int width, int height, int customWidth) {
        LPMBannerAdView *bannerAdView =
        [[LPMBannerAdView alloc] initWithAdUnitId:[LPMUtilities getStringFromCString:adUnitId]];
        
        [bannerAdView setPlacementName:[LPMUtilities getStringFromCString:placementName]];
        
        LPMAdSize *adSize = GetAdSizeFromDescription(description, width, height, customWidth);
        [bannerAdView setAdSize:adSize];
        
        bannerAdView.frame = CGRectMake(0, 0, (CGFloat)adSize.width, (CGFloat)adSize.height);
        
        return (__bridge_retained void *)bannerAdView;
    }
    
    void LPMBannerAdViewSetDelegate(void *bannerAdViewRef, void *delegateRef) {
        LPMBannerAdView *bannerAdView = (__bridge LPMBannerAdView *)bannerAdViewRef;
        LPMBannerAdViewCallbacksWrapper *delegate = (__bridge LPMBannerAdViewCallbacksWrapper *)delegateRef;
        [bannerAdView setDelegate:delegate];
    }
    
    void LPMBannerAdViewLoadAd(void *bannerAdViewRef) {
        LPMBannerAdView *bannerAdView = (__bridge LPMBannerAdView *)bannerAdViewRef;
        [bannerAdView loadAdWithViewController:[UIApplication sharedApplication]
         .keyWindow.rootViewController];
    }
    
    void LPMBannerAdViewDestroy(void *bannerAdViewRef) {
        LPMBannerAdView *bannerAdView =
        (__bridge_transfer LPMBannerAdView *)bannerAdViewRef;
        [bannerAdView destroy];
    }
    
    void LPMBannerAdViewPauseAutoRefresh(void *bannerAdViewRef) {
        LPMBannerAdView *bannerAdView = (__bridge LPMBannerAdView *)bannerAdViewRef;
        [bannerAdView pauseAutoRefresh];
    }
    
    void LPMBannerAdViewResumeAutoRefresh(void *bannerAdViewRef) {
        LPMBannerAdView *bannerAdView = (__bridge LPMBannerAdView *)bannerAdViewRef;
        [bannerAdView resumeAutoRefresh];
    }
    
    void LPMBannerAdViewSetPosition(void *bannerAdViewRef, int position) {
        LPMBannerAdView *bannerAdView = (__bridge LPMBannerAdView *)bannerAdViewRef;
        dispatch_async(dispatch_get_main_queue(), ^{
            UIView *rootView =
            [UIApplication sharedApplication].keyWindow.rootViewController.view;
            
            if (bannerAdView.superview != rootView) {
                [bannerAdView removeFromSuperview];
                [rootView addSubview:bannerAdView];
                bannerAdView.translatesAutoresizingMaskIntoConstraints = NO;
            }
            
            // Remove previous constraints
            [rootView.constraints
             enumerateObjectsUsingBlock:^(NSLayoutConstraint *constraint,
                                          NSUInteger idx, BOOL *stop) {
                if (constraint.firstItem == bannerAdView ||
                    constraint.secondItem == bannerAdView) {
                    [rootView removeConstraint:constraint];
                }
            }];
            
            // Center horizontally
            [rootView addConstraint:[NSLayoutConstraint
                                     constraintWithItem:bannerAdView
                                     attribute:NSLayoutAttributeCenterX
                                     relatedBy:NSLayoutRelationEqual
                                     toItem:rootView
                                     attribute:NSLayoutAttributeCenterX
                                     multiplier:1.0
                                     constant:0]];
            
            // Position vertically
            NSLayoutAttribute verticalAttribute =
            (position == 1) ? NSLayoutAttributeTop : NSLayoutAttributeBottom;
            [rootView addConstraint:[NSLayoutConstraint
                                     constraintWithItem:bannerAdView
                                     attribute:verticalAttribute
                                     relatedBy:NSLayoutRelationEqual
                                     toItem:rootView.safeAreaLayoutGuide
                                     attribute:verticalAttribute
                                     multiplier:1.0
                                     constant:(position == 1 ? 10 : -10)]];
            
            // Specify height and width
            [rootView
             addConstraint:[NSLayoutConstraint
                            constraintWithItem:bannerAdView
                            attribute:NSLayoutAttributeHeight
                            relatedBy:NSLayoutRelationEqual
                            toItem:nil
                            attribute:NSLayoutAttributeNotAnAttribute
                            multiplier:1.0
                            constant:bannerAdView.frame.size.height]];
            
            [rootView
             addConstraint:[NSLayoutConstraint
                            constraintWithItem:bannerAdView
                            attribute:NSLayoutAttributeWidth
                            relatedBy:NSLayoutRelationEqual
                            toItem:nil
                            attribute:NSLayoutAttributeNotAnAttribute
                            multiplier:1.0
                            constant:bannerAdView.frame.size.width]];
        });
    }
    
    void LPMBannerAdViewShow(void *bannerAdViewRef) {
        LPMBannerAdView *bannerAdView = (__bridge LPMBannerAdView *)bannerAdViewRef;
        dispatch_async(dispatch_get_main_queue(), ^{
            bannerAdView.hidden = false;
        });
    }
    
    void LPMBannerAdViewHide(void *bannerAdViewRef) {
        LPMBannerAdView *bannerAdView = (__bridge LPMBannerAdView *)bannerAdViewRef;
        dispatch_async(dispatch_get_main_queue(), ^{
            bannerAdView.hidden = true;
        });
    }
    
#ifdef __cplusplus
}
#endif
