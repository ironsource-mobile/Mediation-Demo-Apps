//
//  InitializationDelegate.m
//  IronSourceDemoApp
//
//  Copyright © 2024 ironSource Mobile Ltd. All rights reserved.
//

#import "InitializationDelegate.h"

@implementation InitializationDelegate

- (instancetype)initWithDelegate:(id<DemoViewControllerDelegate>)delegate {
    self = [super init];
    
    if (self) {
        _delegate = delegate;
    }
    
    return self;
}

/**
 Called after the Mediation successfully complets its initialization
 */
- (void)initializationDidComplete {
    logCallbackName();
    
    [self.delegate setEnablementForButton:LoadInterstitialButtonIdentifier
                                   enable:YES];
    [self.delegate setEnablementForButton:LoadBannerButtonIdentifier
                                   enable:YES];
}

@end
