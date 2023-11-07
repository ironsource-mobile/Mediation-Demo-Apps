//
//  InitializationDelegate.h
//  IronSourceDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.


#import <Foundation/Foundation.h>
#import <IronSource/IronSource.h>
#import "DemoViewController.h"

NS_ASSUME_NONNULL_BEGIN

@interface InitializationDelegate : NSObject<ISInitializationDelegate>

@property (nonatomic, weak) DemoViewController *demoViewController;

- (instancetype)initWithDemoViewController:(DemoViewController *)viewController;

@end

NS_ASSUME_NONNULL_END
