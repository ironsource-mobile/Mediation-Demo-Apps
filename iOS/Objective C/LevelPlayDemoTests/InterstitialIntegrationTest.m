#import <XCTest/XCTest.h>
#import "DemoViewController.h"

@interface InterstitialIntegrationTest : XCTestCase <LPMInterstitialAdDelegate, LPMImpressionDataDelegate>
@property (nonatomic) XCTestExpectation *sdkInitExpectation;
@property (nonatomic) XCTestExpectation *loadExpectation;
@property (nonatomic) XCTestExpectation *impressionExpectation;
@property (nonatomic) LPMInterstitialAd *interstitialAd;
@end

@implementation InterstitialIntegrationTest

- (void)testInitLoadAndImpression {
    self.sdkInitExpectation = [self expectationWithDescription:@"Init"];
    self.loadExpectation = [self expectationWithDescription:@"Load"];
    self.impressionExpectation = [self expectationWithDescription:@"Impression"];

    [LevelPlay addImpressionDataDelegate:self];

    LPMInitRequestBuilder *builder = [[LPMInitRequestBuilder alloc] initWithAppKey:kAppKey];
    LPMInitRequest *request = [builder build];

    [LevelPlay initWithRequest:request completion:^(LPMConfiguration *config, NSError *error) {
        XCTAssertNil(error, @"Init failed: %@", error);
        [self.sdkInitExpectation fulfill];

        self.interstitialAd = [[LPMInterstitialAd alloc] initWithAdUnitId:kInterstitialAdUnitId];
        [self.interstitialAd setDelegate:self];
        [self.interstitialAd loadAd];
    }];

    [self waitForExpectations:@[self.sdkInitExpectation] timeout:10];
    [self waitForExpectations:@[self.loadExpectation] timeout:15];

    UIViewController *rootVC = UIApplication.sharedApplication.keyWindow.rootViewController;
    [self.interstitialAd showAdWithViewController:rootVC placementName:nil];

    [self waitForExpectations:@[self.impressionExpectation] timeout:20];
}

#pragma mark - LPMInterstitialAdDelegate

- (void)didLoadAdWithAdInfo:(LPMAdInfo *)adInfo {
    [self.loadExpectation fulfill];
}

- (void)didFailToLoadAdWithAdUnitId:(NSString *)adUnitId error:(NSError *)error {
    XCTFail(@"Load failed: %@", error);
}

- (void)didDisplayAdWithAdInfo:(LPMAdInfo *)adInfo {}
- (void)didFailToDisplayAdWithAdInfo:(LPMAdInfo *)adInfo error:(NSError *)error {}
- (void)didClickAdWithAdInfo:(LPMAdInfo *)adInfo {}
- (void)didCloseAdWithAdInfo:(LPMAdInfo *)adInfo {}
- (void)didChangeAdInfo:(LPMAdInfo *)adInfo {}

#pragma mark - LPMImpressionDataDelegate

- (void)impressionDataDidSucceed:(LPMImpressionData *)impressionData {
    [self.impressionExpectation fulfill];
}

@end
