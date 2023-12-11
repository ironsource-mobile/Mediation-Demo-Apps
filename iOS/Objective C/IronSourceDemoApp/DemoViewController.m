//
//  DemoViewController.m
//  IronSourceDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.
//

#import "DemoViewController.h"
#import "InitializationDelegate.h"
#import "RewardedVideoDelegate.h"
#import "InterstitialDelegate.h"
#import "BannerDelegate.h"
#import "ImpressionDataDelegate.h"

#define kDefaultUserId @"demoapp"
#define kAppKey @"8545d445"

@interface DemoViewController ()

@property (nonatomic, strong) RewardedVideoDelegate *rewardedVideoDelegate;
@property (nonatomic, strong) InterstitialDelegate *interstitialDelegate;
@property (nonatomic, strong) BannerDelegate *bannerDelegate;

@property (nonatomic, strong) InitializationDelegate *initializationDelegate;

@property (nonatomic, strong) ImpressionDataDelegate *impressionDataDelegate;

@property (nonatomic, strong) ISPlacementInfo   *rewardedVideoPlacementInfo;
@property (nonatomic, strong) ISBannerView      *bannerView;

@end

@implementation DemoViewController

#pragma mark -
#pragma mark Lifecycle Methods

- (void)viewDidLoad {
    [super viewDidLoad];

    [self setupUI];
    [self setupIronSourceSdk];
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

#pragma mark Private Methods

- (void)setupUI {
    self.versionLabel.text = [NSString stringWithFormat:@"sdk version %@", [IronSource sdkVersion]];
    
    for (UIButton *button in @[self.showRewardedVideoButton, self.loadInterstitialButton, self.showInterstitialButton, self.loadBannerButton, self.destroyBannerButton]) {
        button.layer.cornerRadius = 17.0f;
        button.layer.masksToBounds = YES;
        button.layer.borderWidth = 2.5f;
        button.layer.borderColor = [[UIColor grayColor] CGColor];
        button.contentEdgeInsets = UIEdgeInsetsMake(0, 20, 0, 20);
    }
}

- (void)setupIronSourceSdk {
    // The ISIntegrationHelper is used to validate the integration.
    // Remove it before going live!
    [ISIntegrationHelper validateIntegration];
    
    // Before initializing any of our products (Rewarded video, Interstitial or Banner) you must set
    // their delegates. Take a look at each of these delegates method and you will see that they each implement a product
    // protocol. This is our way of letting you know what's going on, and if you don't set the delegates
    // we will not be able to communicate with you.
    // We're passing 'self' to our delegates because we want
    // to be able to enable/disable buttons to match ad availability.
    
    
    self.rewardedVideoDelegate = [[RewardedVideoDelegate alloc] initWithDelegate:self];
    [IronSource setLevelPlayRewardedVideoDelegate:self.rewardedVideoDelegate];
    
    self.interstitialDelegate = [[InterstitialDelegate alloc] initWithDelegate:self];
    [IronSource setLevelPlayInterstitialDelegate:self.interstitialDelegate];
    
    self.bannerDelegate = [[BannerDelegate alloc] initWithDelegate:self];
    [IronSource setLevelPlayBannerDelegate:self.bannerDelegate];
    
    self.impressionDataDelegate = [[ImpressionDataDelegate alloc] init];
    [IronSource addImpressionDataDelegate:self.impressionDataDelegate];
    
    NSString *userId = [IronSource advertiserId];
    
    if (!userId.length) {
        //If we couldn't get the advertiser id, we will use a default one.
        userId = kDefaultUserId;
    }
    
    [IronSource setUserId:userId];
    
    self.initializationDelegate = [[InitializationDelegate alloc] initWithDelegate:self];
    
    // After setting the delegates you can go ahead and initialize the SDK. Once the iniitaliztion callback is return you can start loading your ads
    [IronSource initWithAppKey:kAppKey delegate:self.initializationDelegate];
    
    // To initialize specific ad units:
    // [IronSource initWithAppKey:kAppKey adUnits:@[IS_REWARDED_VIDEO, IS_INTERSTITIAL, IS_BANNER] delegate:self.initializationDelegate];
    
    // Scroll down the file to find out what happens when you tap a button...
}

#pragma mark -
#pragma mark Interface Handling

- (IBAction)showRewardedVideoButtonTapped:(id)sender {
    // After calling 'setLevelPlayRewardedVideoDelegate' and 'initWithAppKey:'
    // you are ready to present an ad. You can supply a placement
    // by calling 'showRewardedVideoWithViewController:placement:', or you can simply
    // call 'showRewardedVideoWithViewController'.
    // In this case the SDK will use the default placement one created for you.
    [IronSource showRewardedVideoWithViewController:self];
}

- (IBAction)loadInterstitialButtonTapped:(id)sender {
    // This will load the Interstitial.
    [IronSource loadInterstitial];
}

- (IBAction)showInterstitialButtonTapped:(id)sender {
    // This will present the Interstitial. Unlike Rewarded
    // Videos there are no placements.
    [IronSource showInterstitialWithViewController:self];
}

- (IBAction)loadBannerButtonTapped:(id)sender {
    // We call destroy banner before loading a new banner
    if (self.bannerView) {
        [self destroyBanner];
    }
    
    // This will load the Banner. You can supply a placement
    // by calling 'loadBannerWithViewController:size:placement', or you can simply
    // call 'loadBannerWithViewController:size'.
    // In this case the SDK will use the default placement one created for you.
    // You can pick any banner size: ISBannerSize_BANNER, ISBannerSize_LARGE, ISBannerSize_RECTANGLE or ISBannerSize_LEADERBOARD
    [IronSource loadBannerWithViewController:self
                                        size:ISBannerSize_BANNER];
}

- (IBAction)destroyBannerButtonTapped:(id)sender {
    [self destroyBanner];
}

- (void)destroyBanner {
    dispatch_async(dispatch_get_main_queue(), ^{
        if (self.bannerView) {
            [IronSource destroyBanner:self.bannerView];
            self.bannerView = nil;
        }
        [self setEnablementForButton:DestroyBanner
                              enable:NO];
    });
}

#pragma mark DemoViewControllerDelegate

- (void)setEnablementForButton:(ButtonAction)action
                        enable:(BOOL)enable {
    dispatch_async(dispatch_get_main_queue(), ^{
        UIButton *buttonToModify;
        
        switch (action) {
            case ShowRewardedVideo:
                buttonToModify = self.showRewardedVideoButton;
                break;
            case LoadInterstitial:
                buttonToModify = self.loadInterstitialButton;
                break;
            case ShowInterstitial:
                buttonToModify = self.showInterstitialButton;
                break;
            case LoadBanner:
                buttonToModify = self.loadBannerButton;
                break;
            case DestroyBanner:
                buttonToModify = self.destroyBannerButton;
                break;
        }

        if (buttonToModify) {
            [buttonToModify setEnabled:enable];
        }
    });
}

- (void)setAndBindBannerView:(ISBannerView *)bannerView {
    dispatch_async(dispatch_get_main_queue(), ^{
        if (self.bannerView) {
            [self.bannerView removeFromSuperview];
        }
        
        self.bannerView = bannerView;
        self.bannerView.translatesAutoresizingMaskIntoConstraints = NO;
        
        [self.view addSubview:self.bannerView];

        NSLayoutConstraint *centerX = [self.bannerView.centerXAnchor constraintEqualToAnchor:self.view.centerXAnchor];
        NSLayoutConstraint *bottom = [self.bannerView.bottomAnchor constraintEqualToAnchor:self.view.safeAreaLayoutGuide.bottomAnchor];
        NSLayoutConstraint *width = [self.bannerView.widthAnchor constraintEqualToConstant:bannerView.frame.size.width];
        NSLayoutConstraint *height = [self.bannerView.heightAnchor constraintEqualToConstant:bannerView.frame.size.height];
        [NSLayoutConstraint activateConstraints:@[centerX, bottom, width, height]];
    });
}

- (void)showVideoRewardMessage {
    if (self.rewardedVideoPlacementInfo) {
        UIViewController *rootViewController = [[[[UIApplication sharedApplication] delegate] window] rootViewController];
        NSString *message = [NSString stringWithFormat:@"You have been rewarded %d %@", [self.rewardedVideoPlacementInfo.rewardAmount intValue], self.rewardedVideoPlacementInfo.rewardName];
        
        UIAlertController *alert = [UIAlertController alertControllerWithTitle:@"Video Reward"
                                                                      message:message
                                                               preferredStyle:UIAlertControllerStyleAlert];
        
        UIAlertAction *okAction = [UIAlertAction actionWithTitle:@"OK" style:UIAlertActionStyleDefault
                                                   handler:^(UIAlertAction *action) {
        }];
        
        [alert addAction:okAction];
        [rootViewController presentViewController:alert animated:NO completion:nil];
        
        self.rewardedVideoPlacementInfo = nil;
    }
}

@end
