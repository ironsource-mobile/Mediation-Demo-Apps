//
//  InterstitialLevelPlayCallbacksHandler.m
//  IronSourceDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.
//

#import "InterstitialLevelPlayCallbacksHandler.h"
#import "DemoViewController.h"

@implementation InterstitialLevelPlayCallbacksHandler

- (instancetype)initWithDemoViewController:(DemoViewController *)viewController {
    self = [super init];

    if (self) {
        _demoViewController = viewController;
    }

    return self;
}

/**
 Called after an interstitial has been loaded
 @param adInfo The info of the ad.
 */
- (void)didLoadWithAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
    
    [self.demoViewController setEnablement:YES forButton:self.demoViewController.showInterstitialButton];
}

/**
 Called after an interstitial has attempted to load but failed.
 @param error The reason for the error
 */
- (void)didFailToLoadWithError:(NSError *)error {
    NSLog(@"%s", __PRETTY_FUNCTION__);
    
    [self.demoViewController setEnablement:NO forButton:self.demoViewController.showInterstitialButton];
}

/**
 Called after an interstitial has been opened.
 @param adInfo The info of the ad.
 */
- (void)didOpenWithAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
    
    [self.demoViewController setEnablement:NO forButton:self.demoViewController.showInterstitialButton];
}

/**
 Called after an interstitial has been displayed on the screen.
 @param adInfo The info of the ad.
 */
- (void)didShowWithAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

/**
 Called after an interstitial has attempted to show but failed.
 @param error The reason for the error
 @param adInfo The info of the ad.
 */
- (void)didFailToShowWithError:(NSError *)error
                     andAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

/**
 Called after an interstitial has been clicked.
 @param adInfo The info of the ad.
 */
- (void)didClickWithAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

/**
 Called after an interstitial has been dismissed.
 @param adInfo The info of the ad.
 */
- (void)didCloseWithAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

@end
