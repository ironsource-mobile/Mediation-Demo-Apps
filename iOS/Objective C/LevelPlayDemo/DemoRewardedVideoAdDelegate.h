//
//  DemoRewardedVideoAdDelegate.h
//  LevelPlayDemo
//
//  Copyright © 2025 LevelPlay Ltd. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <IronSource/IronSource.h>
#import "DemoViewController.h"

NS_ASSUME_NONNULL_BEGIN

@interface DemoRewardedVideoAdDelegate : NSObject<LPMRewardedAdDelegate>

@property (weak, nonatomic) id<DemoViewControllerDelegate> delegate;

- (instancetype)initWithDelegate:(id<DemoViewControllerDelegate>)delegate;

@end

NS_ASSUME_NONNULL_END

