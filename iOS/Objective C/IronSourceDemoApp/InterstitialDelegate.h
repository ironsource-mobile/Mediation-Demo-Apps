//
//  InterstitialDelegate.h
//  IronSourceDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <IronSource/IronSource.h>
#import "DemoViewController.h"

NS_ASSUME_NONNULL_BEGIN

@interface InterstitialDelegate : NSObject<LevelPlayInterstitialDelegate>

@property (weak, nonatomic) id<DemoViewControllerDelegate> delegate;

- (instancetype)initWithDelegate:(id<DemoViewControllerDelegate>)delegate;

@end

NS_ASSUME_NONNULL_END

