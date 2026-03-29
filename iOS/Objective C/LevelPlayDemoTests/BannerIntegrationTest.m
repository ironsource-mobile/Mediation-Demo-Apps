#import <XCTest/XCTest.h>
#import "DemoViewController.h"

@interface BannerIntegrationTest : XCTestCase <LPMBannerAdViewDelegate, LPMImpressionDataDelegate>
@property (nonatomic) XCTestExpectation *sdkInitExpectation;
@property (nonatomic) XCTestExpectation *loadExpectation;
@property (nonatomic) XCTestExpectation *impressionExpectation;
@property (nonatomic) LPMBannerAdView *bannerAd;
@end

@implementation BannerIntegrationTest

- (void)testInitLoadAndImpression {
    self.sdkInitExpectation = [self expectationWithDescription:@"Init"];
    self.loadExpectation = [self expectationWithDescription:@"Load"];
    self.impressionExpectation = [self expectationWithDescription:@"Impression"];

    [LevelPlay addImpressionDataDelegate:self];

    LPMInitRequestBuilder *builder = [[LPMInitRequestBuilder alloc] initWithAppKey:kAppKey];
    LPMInitRequest *request = [builder build];

    UIViewController *rootVC = UIApplication.sharedApplication.keyWindow.rootViewController;

    [LevelPlay initWithRequest:request completion:^(LPMConfiguration *config, NSError *error) {
        XCTAssertNil(error, @"Init failed: %@", error);
        [self.sdkInitExpectation fulfill];

        LPMAdSize *bannerSize = [LPMAdSize createAdaptiveAdSize];
        LPMBannerAdViewConfig *bannerConfig = [[[[LPMBannerAdViewConfigBuilder alloc] init] setWithAdSize:bannerSize] build];
        self.bannerAd = [[LPMBannerAdView alloc] initWithAdUnitId:kBannerAdUnitId config:bannerConfig];
        [self.bannerAd setDelegate:self];

        // Add banner to view hierarchy before loading
        dispatch_async(dispatch_get_main_queue(), ^{
            self.bannerAd.translatesAutoresizingMaskIntoConstraints = NO;
            [rootVC.view addSubview:self.bannerAd];
            [NSLayoutConstraint activateConstraints:@[
                [self.bannerAd.centerXAnchor constraintEqualToAnchor:rootVC.view.centerXAnchor],
                [self.bannerAd.bottomAnchor constraintEqualToAnchor:rootVC.view.safeAreaLayoutGuide.bottomAnchor],
                [self.bannerAd.widthAnchor constraintEqualToConstant:320],
                [self.bannerAd.heightAnchor constraintEqualToConstant:50]
            ]];
            [self.bannerAd loadAdWithViewController:rootVC];
        });
    }];

    [self waitForExpectations:@[self.sdkInitExpectation] timeout:10];
    [self waitForExpectations:@[self.loadExpectation] timeout:15];
    [self waitForExpectations:@[self.impressionExpectation] timeout:20];

    // Cleanup
    [self.bannerAd removeFromSuperview];
    [self.bannerAd destroy];
}

#pragma mark - LPMBannerAdViewDelegate

- (void)didLoadAdWithAdInfo:(LPMAdInfo *)adInfo {
    [self.loadExpectation fulfill];
}

- (void)didFailToLoadAdWithAdUnitId:(NSString *)adUnitId error:(NSError *)error {
    XCTFail(@"Load failed: %@", error);
}

- (void)didDisplayAdWithAdInfo:(LPMAdInfo *)adInfo {}
- (void)didFailToDisplayAdWithAdInfo:(LPMAdInfo *)adInfo error:(NSError *)error {}
- (void)didClickAdWithAdInfo:(LPMAdInfo *)adInfo {}
- (void)didExpandAdWithAdInfo:(LPMAdInfo *)adInfo {}
- (void)didCollapseAdWithAdInfo:(LPMAdInfo *)adInfo {}
- (void)didLeaveAppWithAdInfo:(LPMAdInfo *)adInfo {}

#pragma mark - LPMImpressionDataDelegate

- (void)impressionDataDidSucceed:(LPMImpressionData *)impressionData {
    [self.impressionExpectation fulfill];
}

@end
