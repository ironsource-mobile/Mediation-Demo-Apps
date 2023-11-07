//
//  DemoViewController.h
//  IronSourceDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "RewardedVideoLevelPlayCallbacksHandler.h"
#import "InterstitialLevelPlayCallbacksHandler.h"
#import "BannerLevelPlayCallbacksHandler.h"

@interface DemoViewController : UIViewController <ISImpressionDataDelegate>

@property (weak, nonatomic) IBOutlet UIButton *showRewardedVideoButton;

@property (weak, nonatomic) IBOutlet UIButton *loadInterstitialButton;
@property (weak, nonatomic) IBOutlet UIButton *showInterstitialButton;

@property (weak, nonatomic) IBOutlet UIButton *loadBannerButton;
@property (weak, nonatomic) IBOutlet UIButton *destroyBannerButton;

@property (weak, nonatomic) IBOutlet UILabel  *versionLabel;

@property (nonatomic, strong) RewardedVideoLevelPlayCallbacksHandler *rewardedVideoDelegate;
@property (nonatomic, strong) InterstitialLevelPlayCallbacksHandler *interstitialDelegate;
@property (nonatomic, strong) BannerLevelPlayCallbacksHandler *bannerDelegate;

@property (nonatomic, strong) ISPlacementInfo   *rewardedVideoPlacementInfo;
@property (nonatomic, strong) ISBannerView      *bannerView;

- (void)setAndBindBannerView:(ISBannerView *)bannerView;
- (void)setEnablement:(BOOL)enablement forButton:(UIButton *)button;
- (void)showVideoRewardMessage;

@end

