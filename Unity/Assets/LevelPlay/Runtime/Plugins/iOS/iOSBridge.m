//
//  iOSBridge.m
//  iOSBridge
//
//  Created by Supersonic.
//  Copyright (c) 2015 Supersonic. All rights reserved.
//

#import "iOSBridge.h"
#import <UIKit/UIKit.h>


// Converts NSString to C style string by way of copy (Mono will free it)
#define MakeStringCopy( _x_ ) ( _x_ != NULL && [_x_ isKindOfClass:[NSString class]] ) ? strdup( [_x_ UTF8String] ) : NULL

// Converts C style string to NSString
#define GetStringParam( _x_ ) ( _x_ != NULL ) ? [NSString stringWithUTF8String:_x_] : [NSString stringWithUTF8String:""]


#define BANNER_POSITION_TOP 1
#define BANNER_POSITION_BOTTOM 2

@interface iOSBridge ()
{
    ISBannerView* _bannerView;
    NSInteger _position;
    UIViewController* _bannerViewController;
    BOOL _shouldHideBanner;
}

@property (nonatomic, strong) RewardedVideoLevelPlayCallbacksWrapper *rewardedVideoLevelPlayDelegate;
@property (nonatomic, strong) InterstitialLevelPlayCallbacksWrapper  *interstitialLevelPlayDelegate;
@property (nonatomic, strong) BannerLevelPlayCallbacksWrapper        *bannerLevelPlayDelegate;

@end

static NSString * const EMPTY_STRING = @"";

@implementation iOSBridge
static ISUnityBackgroundCallback backgroundCallback;
static bool pauseGame;


char *const IRONSOURCE_EVENTS = "IronSourceEvents";
char *const IRONSOURCE_REWARDED_VIDEO_EVENTS = "IronSourceRewardedVideoEvents";
char *const IRONSOURCE_INTERSTITIAL_EVENTS = "IronSourceInterstitialEvents";
char *const IRONSOURCE_BANNER_EVENTS = "IronSourceBannerEvents";

+ (iOSBridge *)start {
    static iOSBridge *instance;
    static dispatch_once_t onceToken;
    dispatch_once( &onceToken,
                  ^{
        instance = [iOSBridge new];
    });
    
    return instance;
}

- (instancetype)init {
    if(self = [super init]){
        self.rewardedVideoLevelPlayDelegate = [[RewardedVideoLevelPlayCallbacksWrapper alloc]initWithDelegate:(id)self];
        self.interstitialLevelPlayDelegate = [[InterstitialLevelPlayCallbacksWrapper alloc]initWithDelegate:(id)self];
        self.bannerLevelPlayDelegate = [[BannerLevelPlayCallbacksWrapper alloc]initWithDelegate:(id)self];
        
        [IronSource addImpressionDataDelegate:self];
        [IronSource setConsentViewWithDelegate:self];
        [IronSource setSegmentDelegate:self];
        
        //set level play listeneres
        [IronSource setLevelPlayBannerDelegate:self.bannerLevelPlayDelegate];
        [IronSource setLevelPlayInterstitialDelegate:self.interstitialLevelPlayDelegate];
        [IronSource setLevelPlayRewardedVideoDelegate:self.rewardedVideoLevelPlayDelegate];
        
        
        _bannerView = nil;
        _bannerViewController = nil;
        _position = BANNER_POSITION_BOTTOM;
        _shouldHideBanner = NO;
        
        [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(orientationChanged:)
                                                     name:UIDeviceOrientationDidChangeNotification object:nil];
    }
    
    return self;
}

- (void)setPluginDataWithType:(NSString *)pluginType pluginVersion:(NSString *)version pluginFrameworkVersion:(NSString *)frameworkVersion {
    [ISConfigurations getConfigurations].plugin = pluginType;
    [ISConfigurations getConfigurations].pluginVersion = version;
    [ISConfigurations getConfigurations].pluginFrameworkVersion = frameworkVersion;
}

#pragma mark Base API

- (const char *)getAdvertiserId {
    NSString *advertiserId = [IronSource advertiserId];
    
    return MakeStringCopy(advertiserId);
}

- (void)validateIntegration {
    [ISIntegrationHelper validateIntegration];
}

- (void)shouldTrackNetworkState:(BOOL)flag {
    [IronSource shouldTrackReachability:flag];
}

- (BOOL)setDynamicUserId:(NSString *)dynamicUserId {
    return [IronSource setDynamicUserId:dynamicUserId];
}

- (void)setAdaptersDebug:(BOOL)enabled {
    [IronSource setAdaptersDebug:enabled];
}

- (void)setConsent:(BOOL)consent {
    [IronSource setConsent:consent];
}

- (void)setMetaDataWithKey:(NSString *)key value:(NSString *)value {
    [IronSource setMetaDataWithKey:key value:value];
}

- (void)setMetaDataWithKey:(NSString *)key values:(NSMutableArray *)valuesArray {
    [IronSource setMetaDataWithKey:key values:valuesArray];
}

- (void)setManualLoadRewardedVideo:(BOOL) isOn {
    if (isOn) {
        [IronSource setLevelPlayRewardedVideoManualDelegate:self.rewardedVideoLevelPlayDelegate];
    }
    else {
        [IronSource setLevelPlayRewardedVideoManualDelegate:nil];
    }
}

- (void)setNetworkData:(NSString *)networkKey data:(NSString *)networkData {
    NSError* error;
    if (!networkData) {
        return;
    }
    
    NSData *data = [networkData dataUsingEncoding:NSUTF8StringEncoding];
    if (!data) {
        return;
    }
    
    NSDictionary *dict = [NSJSONSerialization JSONObjectWithData:data options:0 error:&error];
    if (!dict) {
        return;
    }
    
    [IronSource setNetworkDataWithNetworkKey:networkKey andNetworkData:dict];
}

#pragma mark Init SDK

- (void)setUserId:(NSString *)userId {
    [IronSource setUserId:userId];
}

- (void)initWithAppKey:(NSString *)appKey {
    [IronSource initWithAppKey:appKey delegate:self];
}

- (void)initWithAppKey:(NSString *)appKey adUnits:(NSArray<NSString *> *)adUnits {
    [IronSource initWithAppKey:appKey adUnits:adUnits delegate:self];
}

#pragma mark Rewarded Video API

- (void)showRewardedVideo {
    [IronSource showRewardedVideoWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController];
}

- (void)showRewardedVideoWithPlacement:(NSString *)placementName {
    [IronSource showRewardedVideoWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController placement:placementName];
}

- (const char *) getPlacementInfo:(NSString *)placementName {
    char *res = nil;
    
    if (placementName){
        ISPlacementInfo *placementInfo = [IronSource rewardedVideoPlacementInfo:placementName];
        if(placementInfo){
            NSDictionary *dict = @{@"placement_name": [placementInfo placementName],
                                   @"reward_amount": [placementInfo rewardAmount],
                                   @"reward_name": [placementInfo rewardName]};
            
            res = MakeStringCopy([self getJsonFromObj:dict]);
        }
    }
    
    return res;
}

- (BOOL)isRewardedVideoAvailable {
    return [IronSource hasRewardedVideo];
}

- (BOOL)isRewardedVideoPlacementCapped:(NSString *)placementName {
    return [IronSource isRewardedVideoCappedForPlacement:placementName];
}

- (void)setRewardedVideoServerParameters:(NSDictionary *)params {
    [IronSource setRewardedVideoServerParameters:params];
}

- (void)clearRewardedVideoServerParameters {
    [IronSource clearRewardedVideoServerParameters];
}

#pragma mark Rewarded Video Manual Load API

- (void)loadRewardedVideo {
    [IronSource loadRewardedVideo];
}

#pragma mark Init Delegate

- (void)initializationDidComplete {
    UnitySendMessage(IRONSOURCE_EVENTS, "onSdkInitializationCompleted", "");
}

#pragma mark Rewarded Video Level Play Delegate

- (void)rewardedVideoLevelPlayDidClick:(nonnull ISPlacementInfo *)placementInfo withAdInfo:(nonnull ISAdInfo *)adInfo {
    NSDictionary *dict = @{@"placement_reward_amount": placementInfo.rewardAmount,
                           @"placement_reward_name": placementInfo.rewardName,
                           @"placement_name": placementInfo.placementName};
    NSArray *params = @[dict, [self getAdInfoData:adInfo]];
    UnitySendMessage(IRONSOURCE_REWARDED_VIDEO_EVENTS, "onAdClicked", MakeStringCopy([self getJsonFromObj:params]));
}

- (void)rewardedVideoLevelPlayDidCloseWithAdInfo:(nonnull ISAdInfo *)adInfo {
    if (pauseGame) {
        UnityPause(0);
    }
    UnitySendMessage(IRONSOURCE_REWARDED_VIDEO_EVENTS, "onAdClosed",[self getAdInfoData:adInfo].UTF8String);
}

- (void)rewardedVideoLevelPlayDidFailToLoadWithError:(nonnull NSError *)error {
    UnitySendMessage(IRONSOURCE_REWARDED_VIDEO_EVENTS, "onAdLoadFailed", MakeStringCopy([self parseErrorToEvent:error]));
}

- (void)rewardedVideoLevelPlayDidFailToShowWithError:(nonnull NSError *)error andAdInfo:(nonnull ISAdInfo *)adInfo {
    NSArray *params = @[(error) ? [self parseErrorToEvent:error] : @"" , [self getAdInfoData:adInfo]];
    UnitySendMessage(IRONSOURCE_REWARDED_VIDEO_EVENTS, "onAdShowFailed", MakeStringCopy([self getJsonFromObj:params]));
}

- (void)rewardedVideoLevelPlayDidLoadWithAdInfo:(nonnull ISAdInfo *)adInfo {
    UnitySendMessage(IRONSOURCE_REWARDED_VIDEO_EVENTS, "onAdReady", [self getAdInfoData:adInfo].UTF8String);
}

- (void)rewardedVideoLevelPlayDidOpenWithAdInfo:(nonnull ISAdInfo *)adInfo {
    UnitySendMessage(IRONSOURCE_REWARDED_VIDEO_EVENTS, "onAdOpened", [self getAdInfoData:adInfo].UTF8String);
    if (pauseGame) {
        UnityPause(1);
    }
}

- (void)rewardedVideoLevelPlayDidReceiveRewardForPlacement:(nonnull ISPlacementInfo *)placementInfo withAdInfo:(nonnull ISAdInfo *)adInfo {
    NSDictionary *dict = @{@"placement_reward_amount": placementInfo.rewardAmount,
                           @"placement_reward_name": placementInfo.rewardName,
                           @"placement_name": placementInfo.placementName};
    NSArray *params = @[dict, [self getAdInfoData:adInfo]];
    UnitySendMessage(IRONSOURCE_REWARDED_VIDEO_EVENTS, "onAdRewarded", MakeStringCopy([self getJsonFromObj:params]));
}

- (void)hasAvailableAdWithAdInfo:(nonnull ISAdInfo *)adInfo {
    UnitySendMessage(IRONSOURCE_REWARDED_VIDEO_EVENTS, "onAdAvailable", [self getAdInfoData:adInfo].UTF8String);
}

- (void)hasNoAvailableAd {
    UnitySendMessage(IRONSOURCE_REWARDED_VIDEO_EVENTS, "onAdUnavailable","");
    
}

#pragma mark Interstitial API

- (void)loadInterstitial {
    [IronSource loadInterstitial];
}

- (void)showInterstitial {
    [IronSource showInterstitialWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController];
}

- (void)showInterstitialWithPlacement:(NSString *)placementName {
    [IronSource showInterstitialWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController placement:placementName];
}

- (BOOL)isInterstitialReady {
    return [IronSource hasInterstitial];
}

- (BOOL)isInterstitialPlacementCapped:(NSString *)placementName {
    return [IronSource isInterstitialCappedForPlacement:placementName];
}

#pragma mark Interstitial Level Play Delegate

- (void)interstitialLevelPlayDidClickWithAdInfo:(nonnull ISAdInfo *)adInfo {
    UnitySendMessage(IRONSOURCE_INTERSTITIAL_EVENTS, "onAdClicked", [self getAdInfoData:adInfo].UTF8String);
}

- (void)interstitialLevelPlayDidCloseWithAdInfo:(nonnull ISAdInfo *)adInfo {
    if (pauseGame) {
        UnityPause(0);
    }
    UnitySendMessage(IRONSOURCE_INTERSTITIAL_EVENTS, "onAdClosed", [self getAdInfoData:adInfo].UTF8String);
}

- (void)interstitialLevelPlayDidFailToLoadWithError:(nonnull NSError *)error {
    UnitySendMessage(IRONSOURCE_INTERSTITIAL_EVENTS, "onAdLoadFailed", MakeStringCopy([self parseErrorToEvent:error]));
}

- (void)interstitialLevelPlayDidFailToShowWithError:(nonnull NSError *)error andAdInfo:(nonnull ISAdInfo *)adInfo {
    NSArray *params = @[(error) ? [self parseErrorToEvent:error] : @"" , [self getAdInfoData:adInfo]];
    UnitySendMessage(IRONSOURCE_INTERSTITIAL_EVENTS, "onAdShowFailed", MakeStringCopy([self getJsonFromObj:params]));
}

- (void)interstitialLevelPlayDidLoadWithAdInfo:(nonnull ISAdInfo *)adInfo {
    UnitySendMessage(IRONSOURCE_INTERSTITIAL_EVENTS, "onAdReady", [self getAdInfoData:adInfo].UTF8String);
}

- (void)interstitialLevelPlayDidOpenWithAdInfo:(nonnull ISAdInfo *)adInfo {
    UnitySendMessage(IRONSOURCE_INTERSTITIAL_EVENTS, "onAdOpened", [self getAdInfoData:adInfo].UTF8String);
    if (pauseGame) {
        UnityPause(1);
    }
}

- (void)interstitialLevelPlayDidShowWithAdInfo:(nonnull ISAdInfo *)adInfo {
    UnitySendMessage(IRONSOURCE_INTERSTITIAL_EVENTS, "onAdShowSucceeded", [self getAdInfoData:adInfo].UTF8String);
}

#pragma mark ConsentView API

-(void)loadConsentViewWithType:(NSString *)consentViewType {
    [IronSource loadConsentViewWithType: consentViewType];
}

-(void)showConsentViewWithType:(NSString *)consentViewType {
    @synchronized(self) {
        UIViewController* viewController = [UIApplication sharedApplication].keyWindow.rootViewController;
        [IronSource showConsentViewWithViewController:viewController andType:consentViewType];
    }
}

#pragma mark Banner API

- (void)loadBanner:(NSString *)description width:(NSInteger)width height:(NSInteger)height position:(NSInteger)position placement:(NSString *)placement adaptive:(bool) isAdaptive containerWidth:(float)containerWidth containerHeight:(float)containerHeight {
    @synchronized(self) {
        _position = position;
        ISBannerSize* size = [self getBannerSize:description width:width height:height];
        
        // Handle the new Adaptive Banner
        if (isAdaptive) {
            size.adaptive = isAdaptive;
            
            float widthx = containerWidth;
            float heightx = containerHeight;
            
            if (widthx <= 0) {
                widthx = [self getDeviceScreenWidth];
            }
            if (heightx <= 0) {
                heightx = [self getMaximalAdaptiveHeightWithWidth:widthx];
            }
            
            ISContainerParams *params = [[ISContainerParams alloc] initWithWidth:widthx height:heightx];
            [size setContainerParams:params];
            
        }
        
        _bannerViewController = [UIApplication sharedApplication].keyWindow.rootViewController;
        [IronSource loadBannerWithViewController:_bannerViewController size:size placement:placement];
    }
}

- (void)destroyBanner {
    dispatch_async(dispatch_get_main_queue(), ^{
        @synchronized(self) {
            if (_bannerView != nil) {
                [IronSource destroyBanner:_bannerView];
                _bannerView = nil;
                _bannerViewController = nil;
                _shouldHideBanner = NO;
            }
        }
    });
}

- (void)displayBanner {
    _shouldHideBanner = NO;
    dispatch_async(dispatch_get_main_queue(), ^{
        @synchronized(self) {
            if (_bannerView != nil) {
                [_bannerView setHidden:_shouldHideBanner];
            }
        }
    });
}

- (void)hideBanner {
    _shouldHideBanner = YES;
    dispatch_async(dispatch_get_main_queue(), ^{
        @synchronized(self) {
            if (_bannerView != nil) {
                [_bannerView setHidden:_shouldHideBanner];
            }
        }
    });
}

- (BOOL)isBannerPlacementCapped:(NSString *)placementName {
    return [IronSource isBannerCappedForPlacement:placementName];
}

- (ISBannerSize *) getBannerSize:(NSString *)description width:(NSInteger)width height:(NSInteger)height {
    if ([description isEqualToString:@"CUSTOM"]) {
        return [[ISBannerSize alloc] initWithWidth:width andHeight:height];
    }
    if ([description isEqualToString:@"SMART"]) {
        return ISBannerSize_SMART;
    }
    if ([description isEqualToString:@"RECTANGLE"]) {
        return ISBannerSize_RECTANGLE;
    }
    if ([description isEqualToString:@"LARGE"]) {
        return ISBannerSize_LARGE;
    }
    else {
        return ISBannerSize_BANNER;
    }
}

-(float)getMaximalAdaptiveHeightWithWidth:(float)width{
    return [ISBannerSize getMaximalAdaptiveHeightWithWidth:width];
}

-(float)getDeviceScreenWidth{
    UIScreen *mainScreen = [UIScreen mainScreen];
    CGFloat screenWidthPoints;
    UIWindow *mainWindow = [[UIWindow alloc] initWithFrame:mainScreen.bounds];
    // Get the safe area layout guide
    UILayoutGuide *safeAreaLayoutGuide = mainWindow.safeAreaLayoutGuide;
    // Get the width of the screen in points considering safe areas
    screenWidthPoints = safeAreaLayoutGuide.layoutFrame.size.width;
    return screenWidthPoints;
}


#pragma mark Banner utils

- (CGPoint)getBannerCenter:(NSInteger)position rootView:(UIView *)rootView {
    CGFloat y;
    if (position == BANNER_POSITION_TOP) {
        y = (_bannerView.frame.size.height / 2) + rootView.safeAreaInsets.top;
    }
    else {
        y = rootView.frame.size.height - (_bannerView.frame.size.height / 2) - rootView.safeAreaInsets.bottom;
    }
    
    return CGPointMake(rootView.frame.size.width / 2, y);
}



- (void)centerBanner {
    dispatch_async(dispatch_get_main_queue(), ^{
        @synchronized(self) {
            if (_bannerView != nil && _bannerViewController != nil) {
                _bannerView.center = [self getBannerCenter:_position rootView:_bannerViewController.view];
            }
        }
    });
}

- (void)orientationChanged:(NSNotification *)notification {
    _bannerViewController = [UIApplication sharedApplication].keyWindow.rootViewController;
    dispatch_after(dispatch_time(DISPATCH_TIME_NOW, (int64_t)(0.1 * NSEC_PER_SEC)), dispatch_get_main_queue(), ^{
        [self centerBanner];
    });
}

#pragma mark Banner Level Play Delegate


- (void)bannerLevelPlayDidClickWithAdInfo:(nonnull ISAdInfo *)adInfo {
    UnitySendMessage(IRONSOURCE_BANNER_EVENTS, "onAdClicked", [self getAdInfoData:adInfo].UTF8String);
    
}

- (void)bannerLevelPlayDidDismissScreenWithAdInfo:(nonnull ISAdInfo *)adInfo {
    UnitySendMessage(IRONSOURCE_BANNER_EVENTS, "onAdScreenDismissed", [self getAdInfoData:adInfo].UTF8String);
    
}

- (void)bannerLevelPlayDidFailToLoadWithError:(nonnull NSError *)error {
    UnitySendMessage(IRONSOURCE_BANNER_EVENTS, "onAdLoadFailed", (error) ? MakeStringCopy([self parseErrorToEvent:error]):"");
}

- (void)bannerLevelPlayDidLeaveApplicationWithAdInfo:(nonnull ISAdInfo *)adInfo {
    UnitySendMessage(IRONSOURCE_BANNER_EVENTS, "onAdLeftApplication", [self getAdInfoData:adInfo].UTF8String);
}

- (void)bannerLevelPlayDidLoad:(nonnull ISBannerView *)bannerView withAdInfo:(nonnull ISAdInfo *)adInfo {
    dispatch_async(dispatch_get_main_queue(), ^{
        @synchronized(self) {
            _bannerView = bannerView;
            [_bannerView setAccessibilityLabel:@"bannerContainer"];
            [_bannerView setHidden:_shouldHideBanner];
            
            _bannerView.center = [self getBannerCenter:_position rootView:_bannerViewController.view];
            [_bannerViewController.view addSubview:_bannerView];
            
            UnitySendMessage(IRONSOURCE_BANNER_EVENTS, "onAdLoaded", [self getAdInfoData:adInfo].UTF8String);
        }
    });
}

- (void)bannerLevelPlayDidPresentScreenWithAdInfo:(nonnull ISAdInfo *)adInfo {
    UnitySendMessage(IRONSOURCE_BANNER_EVENTS, "onAdScreenPresented", [self getAdInfoData:adInfo].UTF8String);
}

#pragma mark Helper methods

- (void) setSegment:(NSString*) segmentJSON {
    
    ISSegment *segment = [[ISSegment alloc] init];
    NSError* error;
    if (!segmentJSON)
        return;
    
    NSData *data = [segmentJSON dataUsingEncoding:NSUTF8StringEncoding];
    if (!data)
        return;
    
    NSDictionary *dict = [NSJSONSerialization JSONObjectWithData:data options:0 error:&error];
    
    if (!dict)
        return;
    
    NSMutableArray *allKeys = [[dict allKeys] mutableCopy];
    for (id key in allKeys)
    {
        NSString* keyString = (NSString*)key;
        NSString *object = [dict objectForKey: keyString];
        if ([keyString isEqualToString:@"age"]){
            segment.age = [object intValue] ;
        }
        else if([keyString isEqualToString:@"gender"]){
            if([object caseInsensitiveCompare:@"male"] == NSOrderedSame)
                segment.gender = IRONSOURCE_USER_MALE ;
            else if([object caseInsensitiveCompare:@"female"] == NSOrderedSame)
                segment.gender = IRONSOURCE_USER_FEMALE;
            
        }
        else if ([keyString isEqualToString:@"level"]){
            segment.level =  [object intValue];
        }
        else if ([keyString isEqualToString:@"isPaying"]){
            segment.paying = [object boolValue];
        }
        else if ([keyString isEqualToString:@"userCreationDate"]){
            NSDate *date = [NSDate dateWithTimeIntervalSince1970: [object longLongValue]/1000];
            segment.userCreationDate = date;
            
        }
        else if ([keyString isEqualToString:@"segmentName"]){
            segment.segmentName = object;
            
        } else if ([keyString isEqualToString:@"iapt"]){
            segment.iapTotal = [object doubleValue];
        }
        else{
            [segment setCustomValue:object forKey:keyString];
        }
        
    }
    
    [IronSource setSegment:segment];
}

- (void)didReceiveSegement:(NSString *)segment{
    UnitySendMessage(IRONSOURCE_EVENTS, "onSegmentReceived", MakeStringCopy(segment));
}

- (NSString *)parseErrorToEvent:(NSError *)error{
    if (error){
        NSString* codeStr =  [NSString stringWithFormat:@"%ld", (long)[error code]];
        
        NSDictionary *dict = @{@"error_description": [error localizedDescription],
                               @"error_code": codeStr};
        
        return [self getJsonFromObj:dict];
    }
    return nil;
}

- (NSString *)getJsonFromObj:(id)obj {
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:obj options:0 error:&error];
    if (!jsonData) {
        NSLog(@"Got an error: %@", error);
        return @"";
    } else {
        NSString *jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        return jsonString;
    }
}

- (NSString *) getAdInfoData:(ISAdInfo *) adinfo{
    if (adinfo!=nil) {
        NSString *adInfoString = [NSString stringWithFormat:@"%@", adinfo];
        return adInfoString;
    }
    return EMPTY_STRING;
}

#pragma mark ImpressionData Delegate

- (void)impressionDataDidSucceed:(ISImpressionData *)impressionData {
    if (backgroundCallback!=nil) {
        const char * serializedParameters = [self getJsonFromObj:[impressionData all_data]].UTF8String;
        backgroundCallback(serializedParameters);
    }
    UnitySendMessage(IRONSOURCE_EVENTS, "onImpressionSuccess", [self getJsonFromObj:[impressionData all_data]].UTF8String);
    
}

#pragma mark ConsentView Delegate

- (void)consentViewDidAccept:(NSString *)consentViewType {
    UnitySendMessage(IRONSOURCE_EVENTS, "onConsentViewDidAccept", MakeStringCopy(consentViewType));
}

- (void)consentViewDidDismiss:(NSString *)consentViewType {
    UnitySendMessage(IRONSOURCE_EVENTS, "onConsentViewDidDismiss", MakeStringCopy(consentViewType));
}

- (void)consentViewDidFailToLoadWithError:(NSError *)error consentViewType:(NSString *)consentViewType {
    NSArray *params;
    if (error)
        params = @[consentViewType, [self parseErrorToEvent:error]];
    else
        params = @[consentViewType, @""];
    
    UnitySendMessage(IRONSOURCE_EVENTS, "onConsentViewDidFailToLoadWithError", MakeStringCopy([self getJsonFromObj:params]));
}

- (void)consentViewDidLoadSuccess:(NSString *)consentViewType {
    UnitySendMessage(IRONSOURCE_EVENTS, "onConsentViewDidLoadSuccess", MakeStringCopy(consentViewType));
}

- (void)consentViewDidFailToShowWithError:(NSError *)error consentViewType:(NSString *)consentViewType {
    NSArray *params;
    if (error)
        params = @[consentViewType, [self parseErrorToEvent:error]];
    else
        params = @[consentViewType, @""];
    
    UnitySendMessage(IRONSOURCE_EVENTS, "onConsentViewDidFailToShowWithError", MakeStringCopy([self getJsonFromObj:params]));
}

- (void)consentViewDidShowSuccess:(NSString *)consentViewType {
    UnitySendMessage(IRONSOURCE_EVENTS, "onConsentViewDidShowSuccess", MakeStringCopy(consentViewType));
}

#pragma mark ConversionValue API

-(const char *) getConversionValue {
    NSNumber *conversionValue = [IronSource getConversionValue];
    char *res = MakeStringCopy([conversionValue stringValue]);
    return res;
}

#pragma mark ILRD API
- (void)setAdRevenueData:(NSString *)dataSource impressionData:(NSData *)impressionData {
    [IronSource setAdRevenueDataWithDataSource:dataSource impressionData:impressionData];
}

#pragma mark TestSuite API
- (void)launchTestSuite {
    [IronSource launchTestSuite:[UIApplication sharedApplication].keyWindow.rootViewController];
}

#pragma mark - C Section

#ifdef __cplusplus
extern "C" {
#endif
    
    typedef struct {
        double floor;
        double ceiling;
    } LPPWaterfallConfigurationData;
    
    enum LPPAdFormat
    {
        LPPAdFormatRewardedVideo,
        LPPAdFormatInterstitial,
        LPPAdFormatBanner
    };
    
    void RegisterCallback(ISUnityBackgroundCallback func){
        backgroundCallback=func;
    }
    void RegisterPauseGameFunction(bool func){
        pauseGame=func;
    }
    
    void CFSetPluginData(const char *pluginType, const char *pluginVersion, const char *pluginFrameworkVersion){
        [[iOSBridge start] setPluginDataWithType:GetStringParam(pluginType) pluginVersion:GetStringParam(pluginVersion) pluginFrameworkVersion:GetStringParam(pluginFrameworkVersion)];
    }
    
    const char *CFGetAdvertiserId(){
        return [[iOSBridge start] getAdvertiserId];
    }
    
    void CFValidateIntegration(){
        [[iOSBridge start] validateIntegration];
    }
    
    void CFShouldTrackNetworkState(bool flag){
        [[iOSBridge start] shouldTrackNetworkState:flag];
    }
    
    bool CFSetDynamicUserId(char *dynamicUserId){
        return [[iOSBridge start] setDynamicUserId:GetStringParam(dynamicUserId)];
    }
    
    void CFSetAdaptersDebug(bool enabled){
        [[iOSBridge start] setAdaptersDebug:enabled];
    }
    
    void CFSetUserId(char *userId){
        return [[iOSBridge start] setUserId:GetStringParam(userId)];
    }
    
    void CFSetConsent (bool consent) {
        [[iOSBridge start] setConsent:consent];
    }
    
    void CFSetMetaData (char *key, char *value) {
        [[iOSBridge start] setMetaDataWithKey:GetStringParam(key) value:GetStringParam(value)];
    }
    
    void CFSetMetaDataWithValues (char *key,const char *values[]) {
        NSMutableArray *valuesArray = [NSMutableArray new];
        if(values != nil ) {
            int i = 0;
            
            while (values[i] != nil) {
                [valuesArray addObject: [NSString stringWithCString: values[i] encoding:NSASCIIStringEncoding]];
                i++;
            }
            
            [[iOSBridge start] setMetaDataWithKey:GetStringParam(key) values:valuesArray];
        }
    }
    
    void CFSetManualLoadRewardedVideo(bool isOn) {
        [[iOSBridge start] setManualLoadRewardedVideo:isOn];
    }
    
    void CFSetNetworkData (char *networkKey, char *networkData) {
        [[iOSBridge start] setNetworkData:GetStringParam(networkKey) data:GetStringParam(networkData)];
    }
    
#pragma mark Init SDK
    
    void CFInit(const char *appKey){
        [[iOSBridge start] initWithAppKey:GetStringParam(appKey)];
    }
    
    void CFInitWithAdUnits(const char *appKey, const char *adUnits[]){
        NSMutableArray *adUnitsArray = [NSMutableArray new];
        
        if(adUnits != nil ) {
            int i = 0;
            
            while (adUnits[i] != nil) {
                [adUnitsArray addObject: [NSString stringWithCString: adUnits[i] encoding:NSASCIIStringEncoding]];
                i++;
            }
            
            [[iOSBridge start] initWithAppKey:GetStringParam(appKey) adUnits:adUnitsArray];
        }
    }
    
#pragma mark RewardedVideo API
    
    void CFLoadRewardedVideo() {
        [[iOSBridge start] loadRewardedVideo];
    }
    
    void CFShowRewardedVideo(){
        [[iOSBridge start] showRewardedVideo];
    }
    
    void CFShowRewardedVideoWithPlacementName(char *placementName){
        [[iOSBridge start] showRewardedVideoWithPlacement:GetStringParam(placementName)];
    }
    
    const char *CFGetPlacementInfo(char *placementName){
        return [[iOSBridge start] getPlacementInfo:GetStringParam(placementName)];
    }
    
    bool CFIsRewardedVideoAvailable(){
        return [[iOSBridge start] isRewardedVideoAvailable];
    }
    
    bool CFIsRewardedVideoPlacementCapped(char *placementName){
        return [[iOSBridge start] isRewardedVideoPlacementCapped:GetStringParam(placementName)];
    }
    
    void CFSetRewardedVideoServerParameters(char *jsonString) {
        NSData *data = [GetStringParam(jsonString) dataUsingEncoding:NSUTF8StringEncoding];
        if (!data) {
            return;
        }
        
        NSDictionary *dict = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
        if (dict) {
            [[iOSBridge start] setRewardedVideoServerParameters:dict];
        }
    }
    
    void CFClearRewardedVideoServerParameters() {
        [[iOSBridge start] clearRewardedVideoServerParameters];
    }
    
#pragma mark Interstitial API
    
    void CFLoadInterstitial(){
        [[iOSBridge start] loadInterstitial];
    }
    
    void CFShowInterstitial(){
        [[iOSBridge start] showInterstitial];
    }
    
    void CFShowInterstitialWithPlacementName(char *placementName){
        [[iOSBridge start] showInterstitialWithPlacement:GetStringParam(placementName)];
    }
    
    bool CFIsInterstitialReady(){
        return [[iOSBridge start] isInterstitialReady];
    }
    
    bool CFIsInterstitialPlacementCapped(char *placementName){
        return [[iOSBridge start] isInterstitialPlacementCapped:GetStringParam(placementName)];
    }
    
#pragma mark Banner API
    
    void CFLoadBanner(char* description, int width, int height, int position, char* placementName, bool isAdaptive,float containerWidth,float containerHeight){
        [[iOSBridge start] loadBanner:GetStringParam(description) width:width height:height position:position placement:GetStringParam(placementName) adaptive:isAdaptive containerWidth:containerWidth containerHeight:containerHeight];
    }
    
    void CFDestroyBanner (){
        [[iOSBridge start] destroyBanner];
    }
    
    void CFDisplayBanner (){
        [[iOSBridge start] displayBanner];
    }
    
    void CFHideBanner (){
        [[iOSBridge start] hideBanner];
    }
    
    bool CFIsBannerPlacementCapped (char *placementName){
        return [[iOSBridge start] isBannerPlacementCapped:GetStringParam(placementName)];
    }
    
    float CFIGetMaximalAdaptiveHeight(float width){
        return [[iOSBridge start] getMaximalAdaptiveHeightWithWidth:width];
    }
    
    float CFIGetDeviceScreenWidth(){
        return [[iOSBridge start] getDeviceScreenWidth];
    }
    
#pragma mark Segment API
    
    void CFSetSegment (char* jsonString) {
        [[iOSBridge start] setSegment:GetStringParam(jsonString)];
    }
    
#pragma mark Set Waterfall Configuration API
    
    void LPPSetWaterfallConfiguration(LPPWaterfallConfigurationData configurationParams, enum LPPAdFormat adFormat) {
        ISWaterfallConfigurationBuilder *builder = [ISWaterfallConfiguration builder];
        const double defaultValue = 0.00;
        
        if (configurationParams.floor != defaultValue) {
            NSNumber *floorValue = [NSNumber numberWithDouble:configurationParams.floor];
            [builder setFloor:floorValue];
        }
        
        if (configurationParams.ceiling != defaultValue) {
            NSNumber *ceilingValue = [NSNumber numberWithDouble:configurationParams.ceiling];
            [builder setCeiling:ceilingValue];
        }
        
        ISWaterfallConfiguration *waterfallConfig = [builder build];
        ISAdUnit *adUnit;
        switch (adFormat) {
            case LPPAdFormatInterstitial:
                adUnit = [ISAdUnit IS_AD_UNIT_INTERSTITIAL];
                break;
            case LPPAdFormatRewardedVideo:
                adUnit = [ISAdUnit IS_AD_UNIT_REWARDED_VIDEO];
                break;
            case LPPAdFormatBanner:
                adUnit = [ISAdUnit IS_AD_UNIT_BANNER];
                break;
            default:
                return;
        }
        
        [IronSource setWaterfallConfiguration:waterfallConfig forAdUnit:adUnit];
    }
    
#pragma mark ConsentView API
    
    void CFLoadConsentViewWithType (char* consentViewType){
        [[iOSBridge start] loadConsentViewWithType:GetStringParam(consentViewType)];
    }
    
    void CFShowConsentViewWithType (char* consentViewType){
        [[iOSBridge start] showConsentViewWithType:GetStringParam(consentViewType)];
    }
    
#pragma mark ConversionValue API
    
    const char *CFGetConversionValue(){
        return [[iOSBridge start] getConversionValue];
    }
    
#pragma mark ILRD API
    void  CFSetAdRevenueData(char* datasource,char* impressiondata){
        NSData *data=[GetStringParam(impressiondata)dataUsingEncoding:NSUTF8StringEncoding];
        if (!data) {
            return;
        }
        return [[iOSBridge start] setAdRevenueData:GetStringParam(datasource)impressionData:data];
    }
    
#pragma mark TestSuite API
    void CFLaunchTestSuite(){
        [[iOSBridge start] launchTestSuite];
    }
    
    
#ifdef __cplusplus
}
#endif

@end

