//
//  ViewController.m
//  IronSourceDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.
//

#import "ViewController.h"
#import "RewardedVideoLevelPlayCallbacksHandler.h"
#import "InterstitialLevelPlayCallbacksHandler.h"
#import "BannerLevelPlayCallbacksHandler.h"
#import <IronSource/IronSource.h>

#define USERID @"demoapp"
#define APPKEY @"8545d445"

@interface ViewController () <RewardedVideoLevelPlayCallbacksWrapper, InterstitialLevelPlayCallbacksWrapper, BannerLevelPlayCallbacksWrapper, ISImpressionDataDelegate>

@property (weak, nonatomic) IBOutlet UIButton *showRVButton;
@property (weak, nonatomic) IBOutlet UIButton *loadISButton;
@property (weak, nonatomic) IBOutlet UIButton *showISButton;
@property (weak, nonatomic) IBOutlet UILabel  *versionLabel;

@property (nonatomic, strong) RewardedVideoLevelPlayCallbacksHandler *rewardedVideoDelegate;
@property (nonatomic, strong) InterstitialLevelPlayCallbacksHandler *interstitialDelegate;
@property (nonatomic, strong) BannerLevelPlayCallbacksHandler *bannerDelegate;

@property (nonatomic, strong) ISPlacementInfo   *rvPlacementInfo;
@property (nonatomic, strong) ISBannerView      *bannerView;

@end

@implementation ViewController

#pragma mark -
#pragma mark Lifecycle Methods

- (void)viewDidLoad {
    [super viewDidLoad];
    
    //The integrationHelper is used to validate the integration. Remove the integrationHelper before going live!
    [ISIntegrationHelper validateIntegration];
    
    // UI setup
    self.versionLabel.text = [NSString stringWithFormat:@"sdk version %@", [IronSource sdkVersion]];
    
    for (UIButton *button in @[self.showISButton, self.showRVButton, self.loadISButton]) {
        button.layer.cornerRadius = 17.0f;
        button.layer.masksToBounds = YES;
        button.layer.borderWidth = 3.5f;
        button.layer.borderColor = [[UIColor grayColor] CGColor];
    }
    
    [ISSupersonicAdsConfiguration configurations].useClientSideCallbacks = @(YES);
    
    // Before initializing any of our products (Rewarded video, Interstitial or Banner) you must set
    // their delegates. Take a look at each of these delegates method and you will see that they each implement a product
    // protocol. This is our way of letting you know what's going on, and if you don't set the delegates
    // we will not be able to communicate with you.
    // We're passing 'self' to our delegates because we want
    // to be able to enable/disable buttons to match ad availability.
    
    self.rewardedVideoDelegate = [[RewardedVideoLevelPlayCallbacksHandler alloc] initWithDelegate:self];
    self.interstitialDelegate = [[InterstitialLevelPlayCallbacksHandler alloc] initWithDelegate:self];
    self.bannerDelegate = [[BannerLevelPlayCallbacksHandler alloc] initWithDelegate:self];

    [IronSource setLevelPlayRewardedVideoDelegate:self.rewardedVideoDelegate];
    [IronSource setLevelPlayInterstitialDelegate:self.interstitialDelegate];
    [IronSource setLevelPlayBannerDelegate:self.bannerDelegate];
    [IronSource addImpressionDataDelegate:self];

    NSString *userId = [IronSource advertiserId];
    
    if ([userId length] == 0) {
        //If we couldn't get the advertiser id, we will use a default one.
        userId = USERID;
    }
    
    [IronSource setUserId:userId];
    
    // After setting the delegates you can go ahead and initialize the SDK.
    [IronSource initWithAppKey:APPKEY];
    
    // To initialize specific ad units:
    // [IronSource initWithAppKey:APPKEY adUnits:@[IS_REWARDED_VIDEO, IS_INTERSTITIAL, IS_BANNER]];
    
    // Scroll down the file to find out what happens when you click a button...
    
    /* 
     * Banner integration
     * To finalize your banner integration, you must integrate at least one of our Mediation adapters that have banner.
     */
    [self loadBanner];
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

#pragma mark -
#pragma mark Interface Handling

- (IBAction)showRVButtonTapped:(id)sender {
    // After calling 'setLevelPlayRewardedVideoDelegate' and 'initWithAppKey:'
    // you are ready to present an ad. You can supply a placement
    // by calling 'showRewardedVideoWithViewController:placement:', or you can simply
    // call 'showRewardedVideoWithViewController'. 
    // In this case the SDK will use the default placement one created for you.
    [IronSource showRewardedVideoWithViewController:self];
}

- (IBAction)loadISButtonTapped:(id)sender {
    // This will load the Interstitial.
    [IronSource loadInterstitial];
}

- (IBAction)showISButtonTapped:(id)sender {
    // This will present the Interstitial. Unlike Rewarded
    // Videos there are no placements.
    [IronSource showInterstitialWithViewController:self];
}

- (void)loadBanner {
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

- (void)destroyBanner {
    dispatch_async(dispatch_get_main_queue(), ^{
        if (self.bannerView) {
            [IronSource destroyBanner:self.bannerView];
            self.bannerView = nil;
        }
    });
}

#pragma mark - Rewarded Video LevelPlay Callbacks Wrapper Functions

// Once the rewarded video is availalbe you can call the showRewardedVideoWithViewController: API
- (void)rewardedVideoLevelPlayHasAvailableAdWithAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
    
    dispatch_async(dispatch_get_main_queue(), ^{
        [self.showRVButton setEnabled:YES];
    });
}

- (void)rewardedVideoLevelPlayHasNoAvailableAd {
    NSLog(@"%s", __PRETTY_FUNCTION__);
    
    dispatch_async(dispatch_get_main_queue(), ^{
        [self.showRVButton setEnabled:NO];
    });
}

- (void)rewardedVideoLevelPlayDidOpenWithAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

// If the rewarded video fails to show check out 'error' for more information and consult
// our knowledge center for help.
- (void)rewardedVideoLevelPlayDidFailToShowWithError:(NSError *)error
                                           andAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

- (void)rewardedVideoLevelPlayDidClick:(ISPlacementInfo *)placementInfo
                            withAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

- (void)rewardedVideoLevelPlayDidReceiveRewardForPlacement:(ISPlacementInfo *)placementInfo
                                                withAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
    self.rvPlacementInfo = placementInfo;
}

- (void)rewardedVideoLevelPlayDidCloseWithAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
    
    if (self.rvPlacementInfo) {
        UIViewController *rootVC = [[[[UIApplication sharedApplication] delegate] window] rootViewController];
        NSString *message = [NSString stringWithFormat:@"You have been rewarded %d %@", [self.rvPlacementInfo.rewardAmount intValue], self.rvPlacementInfo.rewardName];
        
        UIAlertController *alert = [UIAlertController alertControllerWithTitle:@"Video Reward"
                                                                      message:message
                                                               preferredStyle:UIAlertControllerStyleAlert];
        
        UIAlertAction *okAction = [UIAlertAction actionWithTitle:@"OK" style:UIAlertActionStyleDefault
                                                   handler:^(UIAlertAction * action) {
        }];
        
        [alert addAction:okAction];
        [rootVC presentViewController:alert animated:NO completion:nil];
        
        self.rvPlacementInfo = nil;
    }
}

#pragma mark - Interstitial LevelPlay Callbacks Wrapper Functions

- (void)interstitialLevelPlayDidLoadWithAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
    
    dispatch_async(dispatch_get_main_queue(), ^{
        [self.showISButton setEnabled:YES];
    });
}

- (void)interstitialLevelPlayDidFailToLoadWithError:(NSError *)error {
    NSLog(@"%s", __PRETTY_FUNCTION__);
    
    dispatch_async(dispatch_get_main_queue(), ^{
        [self.showISButton setEnabled:NO];
    });
}

- (void)interstitialLevelPlayDidOpenWithAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

- (void)interstitialLevelPlayDidShowWithAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

// If the interstitial fails to show check out 'error' for more information and consult
// our knowledge center for help.
- (void)interstitialLevelPlayDidFailToShowWithError:(NSError *)error
                                          andAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

- (void)interstitialLevelPlayDidClickWithAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

- (void)interstitialLevelPlayDidCloseWithAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

#pragma mark - Banner LevelPlay Callbacks Wrapper Functions

- (void)bannerLevelPlayDidLoad:(ISBannerView *)bannerView
                    withAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
    
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

- (void)bannerLevelPlayDidFailToLoadWithError:(NSError *)error {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

- (void)bannerLevelPlayDidClickWithAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

- (void)bannerLevelPlayDidLeaveApplicationWithAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

- (void)bannerLevelPlayDidPresentScreenWithAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

- (void)bannerLevelPlayDidDismissScreenWithAdInfo:(ISAdInfo *)adInfo {
    NSLog(@"%s", __PRETTY_FUNCTION__);
}

#pragma mark - Impression Data Delegate Functions

- (void)impressionDataDidSucceed:(ISImpressionData *)impressionData {
    NSLog(@"impressionData %@", impressionData);
}

@end
