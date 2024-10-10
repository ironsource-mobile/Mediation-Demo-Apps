//
//  DemoBannerAdDelegate.m
//  IronSourceDemoApp
//
//  Copyright © 2024 ironSource Mobile Ltd. All rights reserved.
//

#import "DemoBannerAdDelegate.h"

@implementation DemoBannerAdDelegate

- (instancetype)initWithDelegate:(id<DemoViewControllerDelegate>)delegate{
    self = [super init];
    
    if (self) {
        _delegate = delegate;
    }
    
    return self;
}

/**
 Called after each banner ad has been successfully loaded, either a manual load or banner refresh
 @param adInfo The info of the ad.
 */
- (void)didLoadAdWithAdInfo:(nonnull LPMAdInfo *)adInfo {
    logCallbackName(@"adInfo = %@", adInfo);
    [self.delegate didLoadAdWithAdInfo];
    [self.delegate setEnablementForButton:LoadBannerButtonIdentifier
                                   enable:NO];

}

/**
 Called after a banner has attempted to load an ad but failed.
 This delegate will be sent both for manual load and refreshed banner failures.
 @param adUnitId The ad unit id of the ad.
 @param error The reason for the error.
 */
- (void)didFailToLoadAdWithAdUnitId:(nonnull NSString *)adUnitId error:(nonnull NSError *)error {
// Ad load failed
    logCallbackName(@"error = %@ | adUnitId = %@", error.localizedDescription, adUnitId);

}

/**
 Called after a banner has been clicked.
 @param adInfo The info of the ad.
 */
- (void)didClickAdWithAdInfo:(LPMAdInfo *)adInfo {
    logCallbackName(@"adInfo = %@", adInfo);
}

/**
 Called after a banner was displayed and visible on screen.
 @param adInfo The info of the ad.
 */
- (void)didDisplayAdWithAdInfo:(LPMAdInfo *)adInfo {
    logCallbackName(@"adInfo = %@", adInfo);
}

/**
 Called after a banner failed to be displayed on screen.
 @param adInfo The info of the ad.
 @param error The reason for the error.

 */
- (void)didFailToDisplayAdWithAdInfo:(LPMAdInfo *)adInfo error:(NSError *)error {
    logCallbackName(@"error = %@ | adInfo = %@", error.localizedDescription, adInfo);
}

/**
 Called when a user was taken out of the application context.
 @param adInfo The info of the ad.
 */
- (void)didLeaveAppWithAdInfo:(LPMAdInfo *)adInfo {
    logCallbackName(@"adInfo = %@", adInfo);
}

/**
 Called when a banner presented a full screen content.
 @param adInfo The info of the ad.
 */
- (void)didExpandAdWithAdInfo:(LPMAdInfo *)adInfo {
    logCallbackName(@"adInfo = %@", adInfo);
}

/**
 Called after a full screen content has been dismissed.
 @param adInfo The info of the ad.
 */
- (void)didCollapseAdWithAdInfo:(LPMAdInfo *)adInfo {
    logCallbackName(@"adInfo = %@", adInfo);
}



@end
