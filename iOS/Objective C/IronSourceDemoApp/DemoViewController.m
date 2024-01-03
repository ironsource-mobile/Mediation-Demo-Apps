//
//  DemoViewController.m
//  IronSourceDemoApp
//
//  Copyright © 2024 ironSource Mobile Ltd. All rights reserved.

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

#pragma mark -
#pragma mark Private Methods

- (void)setupUI {
    self.versionLabel.text = [NSString stringWithFormat:@"sdk version %@", [IronSource sdkVersion]];
    
    for (UIButton *button in @[self.showRewardedVideoButton, self.loadInterstitialButton, self.showInterstitialButton, self.loadBannerButton, self.destroyBannerButton]) {
        button.layer.cornerRadius = 17.0f;
        button.layer.masksToBounds = YES;
        button.layer.borderWidth = 2.5f;
        button.layer.borderColor = [[UIColor grayColor] CGColor];
        button.contentEdgeInsets = UIEdgeInsetsMake(8, 20, 8, 20);
    }
}

- (void)setupIronSourceSdk {
#ifdef DEBUG
    // The ISIntegrationHelper is used to validate the integration.
    // Remove it before going live!
    [ISIntegrationHelper validateIntegration];
#endif
    
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
    
    self.initializationDelegate = [[InitializationDelegate alloc] initWithDelegate:self];
    
    // After setting the delegates you can go ahead and initialize the SDK. Once the iniitaliztion callback is return you can start loading your ads
    
    [self logMethodName:@"initWithAppKey:delegate:"];
    [IronSource initWithAppKey:kAppKey delegate:self.initializationDelegate];
    
    // To initialize specific ad units:
    // [IronSource initWithAppKey:kAppKey adUnits:@[IS_REWARDED_VIDEO, IS_INTERSTITIAL, IS_BANNER] delegate:self.initializationDelegate];
    
    // Scroll down the file to find out what happens when you tap a button...
}

- (void)destroyBanner {
    dispatch_async(dispatch_get_main_queue(), ^{
        if (self.bannerView) {
            [self logMethodName:@"destroyBanner:"];
            [IronSource destroyBanner:self.bannerView];
            self.bannerView = nil;
        }
        
        [self setEnablementForButton:LoadBannerButtonIdentifier
                              enable:YES];
        [self setEnablementForButton:DestroyBannerButtonIdentifier
                              enable:NO];
    });
}

- (void)logMethodName:(NSString *)methodName {
    NSLog(@"DemoViewController %@", methodName);
}

#pragma mark -
#pragma mark Interface Handling

- (IBAction)showRewardedVideoButtonTapped:(id)sender {
    // After calling 'setLevelPlayRewardedVideoDelegate' and 'initWithAppKey:'
    // you are ready to present an ad. You can supply a placement
    // by calling 'showRewardedVideoWithViewController:placement:', or you can simply
    // call 'showRewardedVideoWithViewController'.
    // In this case the SDK will use the default placement one created for you.
    
    [self logMethodName:@"showRewardedVideoWithViewController:"];
    [IronSource showRewardedVideoWithViewController:self];
}

- (IBAction)loadInterstitialButtonTapped:(id)sender {
    // This will load an Interstitial ad
    
    [self logMethodName:@"loadInterstitial"];
    [IronSource loadInterstitial];
}

- (IBAction)showInterstitialButtonTapped:(id)sender {
    // This will present the Interstitial. Unlike Rewarded
    // Videos there are no placements.
    
    [self logMethodName:@"showInterstitialWithViewController:"];
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
    
    [self logMethodName:@"loadBannerWithViewController:size:"];
    [IronSource loadBannerWithViewController:self
                                        size:ISBannerSize_BANNER];
}

- (IBAction)destroyBannerButtonTapped:(id)sender {
    [self destroyBanner];
}

#pragma mark -
#pragma mark DemoViewControllerDelegate

- (void)setEnablementForButton:(ButtonIdentifiers)buttonIdentifier
                        enable:(BOOL)enable {
    dispatch_async(dispatch_get_main_queue(), ^{
        UIButton *buttonToModify;
        
        switch (buttonIdentifier) {
            case ShowRewardedVideoButtonIdentifier:
                buttonToModify = self.showRewardedVideoButton;
                break;
            case LoadInterstitialButtonIdentifier:
                buttonToModify = self.loadInterstitialButton;
                break;
            case ShowInterstitialButtonIdentifier:
                buttonToModify = self.showInterstitialButton;
                break;
            case LoadBannerButtonIdentifier:
                buttonToModify = self.loadBannerButton;
                break;
            case DestroyBannerButtonIdentifier:
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

// Setting the rewarded video placement info, an object that contains the placement's reward name and amount
- (void)setPlacementInfo:(ISPlacementInfo *)placementInfo {
    self.rewardedVideoPlacementInfo = placementInfo;
}

// Showing a graphical indication of the reward name and amount after the user closed the rewarded video ad
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
