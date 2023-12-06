//
//  InitializationDelegate.m
//  IronSourceDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.


#import "InitializationDelegate.h"

@implementation InitializationDelegate

- (instancetype)initWithDelegate:(id<DemoViewControllerDelegate>)delegate {
    self = [super init];
    if (self) {
        _delegate = delegate;
    }
    return self;
}

- (void)initializationDidComplete {
    NSLog(@"%s", __PRETTY_FUNCTION__);
    
    [self.delegate setEnablementForButton:LoadInterstitial
                                   enable:YES];
    [self.delegate setEnablementForButton:LoadBanner
                                   enable:YES];
}

@end
