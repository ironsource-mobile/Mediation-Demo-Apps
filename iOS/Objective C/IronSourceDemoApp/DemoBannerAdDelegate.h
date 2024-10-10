//
//  DemoBannerAdDelegate.h
//  IronSourceDemoApp
//
//  Copyright © 2024 ironSource Mobile Ltd. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <IronSource/IronSource.h>
#import "DemoViewController.h"

NS_ASSUME_NONNULL_BEGIN

@interface DemoBannerAdDelegate : NSObject<LPMBannerAdViewDelegate>

@property (weak, nonatomic) id<DemoViewControllerDelegate> delegate;
@property (weak, nonatomic) LPMBannerAdView *bannerView;
@property (weak, nonatomic) LPMAdSize *bannerSize;

- (instancetype)initWithDelegate:(id<DemoViewControllerDelegate>)delegate;

@end

NS_ASSUME_NONNULL_END
