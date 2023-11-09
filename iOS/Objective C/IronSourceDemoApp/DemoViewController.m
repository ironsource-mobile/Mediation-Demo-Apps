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
    
    
    self.rewardedVideoDelegate = [[RewardedVideoDelegate alloc] initWithDemoViewController:self];
    [IronSource setLevelPlayRewardedVideoDelegate:self.rewardedVideoDelegate];
    
    self.interstitialDelegate = [[InterstitialDelegate alloc] initWithDemoViewController:self];
    [IronSource setLevelPlayInterstitialDelegate:self.interstitialDelegate];
    
    self.bannerDelegate = [[BannerDelegate alloc] initWithDemoViewController:self];
    [IronSource setLevelPlayBannerDelegate:self.bannerDelegate];
    
    self.impressionDataDelegate = [[ImpressionDataDelegate alloc] init];
    [IronSource addImpressionDataDelegate:self.impressionDataDelegate];
    
    NSString *userId = [IronSource advertiserId];
    
    if (!userId.length) {
        //If we couldn't get the advertiser id, we will use a default one.
        userId = kDefaultUserId;
    }
    
    [IronSource setUserId:userId];
    
    self.initializationDelegate = [[InitializationDelegate alloc] initWithDemoViewController:self];
    
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
        
        [self.destroyBannerButton setEnabled:NO];
    });
}

#pragma mark Helper Methods

- (void)setAndBindBannerView:(ISBannerView *)bannerView {
    dispatch_async(dispatch_get_main_queue(), ^{
        self.bannerView = bannerView;
        CGFloat y = self.view.frame.size.height - (bannerView.frame.size.height / 2.0);
        
        if (@available(ios 11.0, *)) {
             y -= self.view.safeAreaInsets.bottom;
        }
        
        bannerView.center = CGPointMake(self.view.frame.size.width / 2.0, y);
        [self.view addSubview:bannerView];
    });
}

- (void)setEnablement:(BOOL)enablement forButton:(UIButton *)button {
    dispatch_async(dispatch_get_main_queue(), ^{
        [button setEnabled:enablement];
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
