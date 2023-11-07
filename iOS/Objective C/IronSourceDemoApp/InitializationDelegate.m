//
//  InitializationDelegate.m
//  IronSourceDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.


#import "InitializationDelegate.h"

@implementation InitializationDelegate

- (instancetype)initWithDemoViewController:(DemoViewController *)viewController {
    self = [super init];

    if (self) {
        _demoViewController = viewController;
    }

    return self;
}

- (void)initializationDidComplete {
    NSLog(@"%s", __PRETTY_FUNCTION__);
    
    [self.demoViewController setEnablement:YES forButton:self.demoViewController.loadInterstitialButton];
    
    [self.demoViewController setEnablement:YES forButton:self.demoViewController.loadBannerButton];
}

@end
