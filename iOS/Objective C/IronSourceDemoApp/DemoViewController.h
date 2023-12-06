//
//  DemoViewController.h
//  IronSourceDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <IronSource/IronSource.h>

typedef NS_ENUM(NSInteger, ButtonAction) {
    ShowRewardedVideo,
    LoadInterstitial,
    ShowInterstitial,
    LoadBanner,
    DestroyBanner
};

@protocol DemoViewControllerDelegate <NSObject>
- (void)setEnablementForButton:(ButtonAction)action 
                        enable:(BOOL)enable;
- (void)setAndBindBannerView:(ISBannerView *)bannerView;
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

