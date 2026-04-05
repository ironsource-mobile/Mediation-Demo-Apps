//
//  DemoViewController.h
//  LevelPlayDemo
//
//  Copyright © 2021-2025 Unity Technologies. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <IronSource/IronSource.h>

#define logCallbackName(fmt, ...) NSLog((@"%s " fmt), __PRETTY_FUNCTION__, ##__VA_ARGS__);

// Replace with your app key as available in the LevelPlay dashboard
#define kAppKey @"25c43a4a5"

// Replace with your ad unit ids as available in the LevelPlay dashboard
#define kInterstitialAdUnitId @"obg6ohwts3y690ks"
#define kBannerAdUnitId @"xc2bsuntn9ea734t"
#define kRewardedAdUnit @"l1quzz1xmmdhw5er"

typedef NS_ENUM(NSInteger, ButtonIdentifiers) {
    LoadRewardedVideoButtonIdentifier,
    ShowRewardedVideoButtonIdentifier,
    LoadInterstitialButtonIdentifier,
    ShowInterstitialButtonIdentifier,
    LoadBannerButtonIdentifier
};

@protocol DemoViewControllerDelegate <NSObject>
- (void)setEnablementForButton:(ButtonIdentifiers)buttonIdentifier
                        enable:(BOOL)enable;
- (void)setReward:(LPMReward *)reward;
- (void)showVideoRewardMessage;
@end


@interface DemoViewController : UIViewController <DemoViewControllerDelegate>


@property (weak, nonatomic) IBOutlet UIButton *loadRewardedVideoButton;
@property (weak, nonatomic) IBOutlet UIButton *showRewardedVideoButton;

@property (weak, nonatomic) IBOutlet UIButton *loadInterstitialButton;
@property (weak, nonatomic) IBOutlet UIButton *showInterstitialButton;

@property (weak, nonatomic) IBOutlet UIButton *loadBannerButton;

@property (weak, nonatomic) IBOutlet UILabel  *versionLabel;

@end

