//
//  DemoViewController.h
//  IronSourceDemoApp
//
//  Copyright © 2024 ironSource Mobile Ltd. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <IronSource/IronSource.h>

#define logCallbackName(fmt, ...) NSLog((@"%s " fmt), __PRETTY_FUNCTION__, ##__VA_ARGS__);

typedef NS_ENUM(NSInteger, ButtonIdentifiers) {
    LoadRewardedAdButtonIdentifier,
    ShowRewardedAdButtonIdentifier,
    LoadInterstitialAdButtonIdentifier,
    ShowInterstitialAdButtonIdentifier,
    LoadBannerAdButtonIdentifier
};

@protocol DemoViewControllerDelegate <NSObject>
- (void)setEnablementForButton:(ButtonIdentifiers)buttonIdentifier
                        enable:(BOOL)enable;
- (void)setRewardInfo:(LPMReward *)reward;
- (void)showVideoRewardMessage;
@end


@interface DemoViewController : UIViewController <DemoViewControllerDelegate>

@property (weak, nonatomic) IBOutlet UIButton *loadRewardedAdButton;
@property (weak, nonatomic) IBOutlet UIButton *showRewardedAdButton;

@property (weak, nonatomic) IBOutlet UIButton *loadInterstitialAdButton;
@property (weak, nonatomic) IBOutlet UIButton *showInterstitialAdButton;

@property (weak, nonatomic) IBOutlet UIButton *loadBannerAdButton;

@property (weak, nonatomic) IBOutlet UILabel  *versionLabel;

@end

