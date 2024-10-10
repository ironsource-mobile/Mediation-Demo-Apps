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

// Replace with your app key as available in the LevelPlay dashboard
#define kAppKey @"8545d445"

// Replace with your ad unit ids as available in the LevelPlay dashboard
#define kInterstitialAdUnitId @"wmgt0712uuux8ju4"
#define kBannerAdUnitId @"iep3rxsyp9na3rw8"

@interface DemoViewController ()

@property (nonatomic, strong) DemoRewardedVideoAdDelegate     *rewardedVideoDelegate;
@property (nonatomic, strong) DemoInterstitialAdDelegate      *interstitialAdDelegate;
@property (nonatomic, strong) DemoBannerAdDelegate            *bannerAdViewDelegate;

@property (nonatomic, strong) DemoImpressionDataDelegate    *impressionDataDelegate;

@property (nonatomic, strong) ISPlacementInfo           *rewardedVideoPlacementInfo;

@property (nonatomic, strong) LPMBannerAdView             *bannerAdView;
@property (nonatomic, strong) LPMAdSize                   *bannerSize;
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


- (void)dealloc {
    [self.bannerAdView destroy];
}

#pragma mark Private Methods

- (void)setupUI {
    self.versionLabel.text = [NSString stringWithFormat:@"sdk version %@", [IronSource sdkVersion]];
    
    for (UIButton *button in @[self.showRewardedVideoButton, self.loadInterstitialButton, self.showInterstitialButton, self.loadBannerButton]) {
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
    
    // Init the SDK when implementing the Multiple Ad Units Interstitial and Banner API, and Rewarded using legacy APIs
    LPMInitRequestBuilder *requestBuilder = [[LPMInitRequestBuilder alloc] initWithAppKey:kAppKey];
    [requestBuilder withLegacyAdFormats:@[IS_REWARDED_VIDEO]];

    LPMInitRequest *initRequest = [requestBuilder build];
    
    [self logMethodName:[NSString stringWithFormat:@"init ironSource SDK with appKey: %@", kAppKey]];
    [LevelPlay initWithRequest:initRequest completion:^(LPMConfiguration *_Nullable config, NSError *_Nullable error){
    
        if(error) {
            // There was an error on initialization. Take necessary actions or retry
            logCallbackName(@"sdk initialization failed, error = %@", error.localizedDescription);
            return;
        }
        
        // Initialization was successful. You can now load banner ad or perform other tasks
        logCallbackName(@"sdk initialization succeeded");
        [self createInterstititalAd];
        [self createBannerAd];
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

- (void) createInterstititalAd {
    self.interstitialAd = [[LPMInterstitialAd alloc] initWithAdUnitId:kInterstitialAdUnitId];
    self.interstitialAdDelegate = [[DemoInterstitialAdDelegate alloc] initWithDelegate:self];
    self.interstitialAd.delegate = self.interstitialAdDelegate;
    
    [self setEnablementForButton:LoadInterstitialButtonIdentifier
                                   enable:YES];

}

- (IBAction)loadInterstitialButtonTapped:(id)sender {
    // This will load an Interstitial ad
    [self logMethodName:@"loadAd for interstitial"];
    [self.interstitialAd loadAd];
}


- (IBAction)showInterstitialButtonTapped:(id)sender {
    // It is advised to make sure there is available ad that isn't capped before attempting to show it
    if ([self.interstitialAd isAdReady]) {
        // This will present the Interstitial.
        // Unlike Rewarded Videos there are no placements.

        [self logMethodName:@"showAdWithViewController for interstitial:"];
        [self.interstitialAd showAdWithViewController:self placementName:NULL];
    } else {
        // load a new ad before calling show
    }
}


- (void) createBannerAd {
    // choose banner size
    
    // 1. recommended - Adaptive ad size that adjusts to the screen width
    self.bannerSize = [LPMAdSize createAdaptiveAdSize];


    // 2. Adaptive ad size using fixed width ad size
//    self.bannerSize = [LPMAdSize createAdaptiveAdSizeWithWidth:400];

    // 3. Specific banner size - BANNER, LARGE, MEDIUM_RECTANGLE
//    self.bannerSize = [LPMAdSize mediumRectangleSize];

    // Create the banner view and set the ad unit id and ad size
    if (self.bannerSize != nil) {
        self.bannerAdView = [[LPMBannerAdView alloc] initWithAdUnitId:kBannerAdUnitId];
        [self.bannerAdView setAdSize:self.bannerSize];

        // set the banner listener
        self.bannerAdViewDelegate = [[DemoBannerAdDelegate alloc] initWithDelegate:self];
        [self.bannerAdView setDelegate:self.bannerAdViewDelegate];
        
        [self setEnablementForButton:LoadBannerButtonIdentifier
                                       enable:YES];
    }
    else {
        NSLog(@"Error creating banner size");
    }
    
}

- (IBAction)loadBannerButtonTapped:(id)sender {
    [self logMethodName:@"loadAdWithViewController for banner"];
    [self.bannerAdView loadAdWithViewController:self];
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
        }

        if (buttonToModify) {
            [buttonToModify setEnabled:enable];
        }
    });
}

- (void)didLoadBannerAd {
    dispatch_async(dispatch_get_main_queue(), ^{
        
        self.bannerAdView.translatesAutoresizingMaskIntoConstraints = NO;
        [self.view addSubview:self.bannerAdView];
        self.bannerAdView.translatesAutoresizingMaskIntoConstraints = NO;

        [NSLayoutConstraint activateConstraints:@[
            [self.bannerAdView.bottomAnchor constraintEqualToAnchor:self.view.safeAreaLayoutGuide.bottomAnchor],
            [self.bannerAdView.centerXAnchor constraintEqualToAnchor:self.view.centerXAnchor],
            [self.bannerAdView.widthAnchor constraintEqualToConstant:self.bannerSize.width],
            [self.bannerAdView.heightAnchor constraintEqualToConstant:self.bannerSize.height]
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
