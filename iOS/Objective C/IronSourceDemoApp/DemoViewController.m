//
//  DemoViewController.m
//  IronSourceDemoApp
//
//  Copyright © 2024 ironSource Mobile Ltd. All rights reserved.

//

#import "DemoViewController.h"
#import "DemoRewardedVideoAdDelegate.h"
#import "DemoInterstitialAdDelegate.h"
#import "DemoBannerAdDelegate.h"
#import "DemoImpressionDataDelegate.h"

#define kAppKey @"8545d445"
#define kInterstitialAdUnitId @"wmgt0712uuux8ju4"
#define kBannerAdUnitId @"iep3rxsyp9na3rw8"

@interface DemoViewController ()

@property (nonatomic, strong) DemoRewardedVideoAdDelegate     *rewardedVideoDelegate;
@property (nonatomic, strong) DemoInterstitialAdDelegate      *interstitialDelegate;
@property (nonatomic, strong) DemoBannerAdDelegate            *bannerDelegate;

@property (nonatomic, strong) DemoImpressionDataDelegate    *impressionDataDelegate;

@property (nonatomic, strong) ISPlacementInfo           *rewardedVideoPlacementInfo;

@property (nonatomic, strong) LPMBannerAdView             *bannerAdView;
@property (nonatomic, strong) LPMInterstitialAd           *interstitialAd;


@end

@implementation DemoViewController

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
    self.rewardedVideoDelegate = [[DemoRewardedVideoAdDelegate alloc] initWithDelegate:self];
    [IronSource setLevelPlayRewardedVideoDelegate:self.rewardedVideoDelegate];
    
    
    self.impressionDataDelegate = [[DemoImpressionDataDelegate alloc] init];
    [IronSource addImpressionDataDelegate:self.impressionDataDelegate];
        
    // After setting the delegates you can go ahead and initialize the SDK. 
    // Once the iniitaliztion callback is return you can start loading your ads
    [self logMethodName:@"initWithAppKey:delegate:"];
    
    
    // Create a request builder with app key and ad formats. Add User ID if available
    LPMInitRequestBuilder *requestBuilder = [[LPMInitRequestBuilder alloc] initWithAppKey:kAppKey];
    [requestBuilder withLegacyAdFormats:@[IS_REWARDED_VIDEO]];

    // Build the initial request
    LPMInitRequest *initRequest = [requestBuilder build];
    // Initialize LevelPlay with the prepared request
    [LevelPlay initWithRequest:initRequest completion:^(LPMConfiguration *_Nullable config, NSError *_Nullable error){
    
        if(error) {
            // There was an error on initialization. Take necessary actions or retry
            logCallbackName(@"sdk initialization failed, error = %@", error.localizedDescription);
        } else {
            // Initialization was successful. You can now load banner ad or perform other tasks
            logCallbackName(@"sdk initialization succeeded");
            
            [self setEnablementForButton:LoadInterstitialButtonIdentifier
                                           enable:YES];
            [self setEnablementForButton:LoadBannerButtonIdentifier
                                           enable:YES];

        }
    }];
    
    
    // Scroll down the file to find out what happens when you tap a button...
}

#pragma mark Interface Handling

- (IBAction)showRewardedVideoButtonTapped:(id)sender {
    // It is advised to make sure there is available ad before attempting to show an ad
    if ([IronSource hasRewardedVideo]) {
        // This will present the Rewarded Video.

        [self logMethodName:@"showRewardedVideoWithViewController:"];
        [IronSource showRewardedVideoWithViewController:self];
    } else {
        // wait for the availability of rewarded video to change to true before calling show
    }
}

- (IBAction)loadInterstitialButtonTapped:(id)sender {
    // This will load an Interstitial ad

    self.interstitialAd = [[LPMInterstitialAd alloc] initWithAdUnitId:kInterstitialAdUnitId];
    self.interstitialDelegate = [[DemoInterstitialAdDelegate alloc] initWithDelegate:self];
    self.interstitialAd.delegate = self.interstitialDelegate;

    
    [self logMethodName:@"loadInterstitial"];
    [self.interstitialAd loadAd];
}


- (IBAction)showInterstitialButtonTapped:(id)sender {
    // It is advised to make sure there is available ad before attempting to show an ad
    if ([self.interstitialAd isAdReady]) {
        // This will present the Interstitial.
        // Unlike Rewarded Videos there are no placements.

        [self logMethodName:@"showInterstitialWithViewController:"];
        [self.interstitialAd showAdWithViewController:self placementName:NULL];
    } else {
        // load a new ad before calling show
    }
}

- (IBAction)loadBannerButtonTapped:(id)sender {
    // call [IronSource destroyBanner:self.bannerView] before loading a new banner
    if (self.bannerAdView) {
        [self destroyBanner];
    }
    

    // choose banner size
    // 1. recommended - Adaptive ad size that adjusts to the screen width
    // 2. Adaptive ad size using fixed width ad size
    // 3. Specific banner size - BANNER, LARGE, MEDIUM_RECTANGLE
    LPMAdSize *bannerSize = [LPMAdSize createAdaptiveAdSize];


//    LPMAdSize *bannerSize = [LPMAdSize createAdaptiveAdSizeWithWidth:400];
//    LPMAdSize *bannerSize = [LPMAdSize mediumRectangleSize];

    // Create the banner view and set the ad unit id and ad size
    self.bannerAdView = [[LPMBannerAdView alloc] initWithAdUnitId:kBannerAdUnitId];
    [self.bannerAdView setAdSize:bannerSize];
//    self.bannerAdView.frame = CGRectMake(0, 0, bannerSize.width, bannerSize.height);


    
    // set the banner listener
    self.bannerDelegate = [[DemoBannerAdDelegate alloc] initWithDelegate:self
                                                              bannerView:self.bannerAdView
                                                              bannerSize:bannerSize];

    [self.bannerAdView setDelegate:self.bannerDelegate];

    
    [self logMethodName:@"loadBannerWithViewController:size:"];
    [self.bannerAdView loadAdWithViewController:self];
}

- (IBAction)destroyBannerButtonTapped:(id)sender {
    [self destroyBanner];
}

- (void)destroyBanner {
    dispatch_async(dispatch_get_main_queue(), ^{
        if (self.bannerAdView) {
            [self logMethodName:@"destroyBanner:"];
            [self.bannerAdView destroy];
            self.bannerAdView = nil;
        }
        
        [self setEnablementForButton:LoadBannerButtonIdentifier
                              enable:YES];
        [self setEnablementForButton:DestroyBannerButtonIdentifier
                              enable:NO];
    });
}

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

- (void)setAndBindBannerView:(LPMBannerAdView *)bannerView
                  bannerSize:(LPMAdSize *)bannerSize {
    dispatch_async(dispatch_get_main_queue(), ^{
        if (self.bannerAdView) {
            [self.bannerAdView removeFromSuperview];
        }
        
        self.bannerAdView = bannerView;
        self.bannerAdView.translatesAutoresizingMaskIntoConstraints = NO;
        
        [self.view addSubview:self.bannerAdView];

        
        self.bannerAdView.translatesAutoresizingMaskIntoConstraints = NO;

        [NSLayoutConstraint activateConstraints:@[
            [self.bannerAdView.bottomAnchor constraintEqualToAnchor:self.view.safeAreaLayoutGuide.bottomAnchor],
            [self.bannerAdView.centerXAnchor constraintEqualToAnchor:self.view.centerXAnchor],
            [self.bannerAdView.widthAnchor constraintEqualToConstant:bannerSize.width],
            [self.bannerAdView.heightAnchor constraintEqualToConstant:bannerSize.height]
        ]];

    });
}

- (void)setPlacementInfo:(ISPlacementInfo *)placementInfo {
    // Setting the rewarded video placement info, an object that contains the placement's reward name and amount
    self.rewardedVideoPlacementInfo = placementInfo;
}

- (void)showVideoRewardMessage {
    // Showing a graphical indication of the reward name and amount after the user closed the rewarded video ad
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
        [rootViewController presentViewController:alert 
                                         animated:NO
                                       completion:nil];
        
        self.rewardedVideoPlacementInfo = nil;
    }
}

#pragma mark Demo Utils

- (void)logMethodName:(NSString *)methodName {
    NSLog(@"DemoViewController %@", methodName);
}

@end
