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
    ShowRewardedVideoButtonIdentifier,
    LoadInterstitialButtonIdentifier,
    ShowInterstitialButtonIdentifier,
    LoadBannerButtonIdentifier,
    DestroyBannerButtonIdentifier
};

@protocol DemoViewControllerDelegate <NSObject>
- (void)setEnablementForButton:(ButtonIdentifiers)buttonIdentifier
                        enable:(BOOL)enable;
- (void)setAndBindBannerView:(ISBannerView *)bannerView;
- (void)setPlacementInfo:(ISPlacementInfo *)placementInfo;
- (void)showVideoRewardMessage;
@end


@interface DemoViewController : UIViewController <DemoViewControllerDelegate>

@property (weak, nonatomic) IBOutlet UIButton *showRewardedVideoButton;

@property (weak, nonatomic) IBOutlet UIButton *loadInterstitialButton;
@property (weak, nonatomic) IBOutlet UIButton *showInterstitialButton;

@property (weak, nonatomic) IBOutlet UIButton *loadBannerButton;
@property (weak, nonatomic) IBOutlet UIButton *destroyBannerButton;

@property (weak, nonatomic) IBOutlet UILabel  *versionLabel;

@end

