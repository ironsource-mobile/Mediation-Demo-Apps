//
//  LPMInitializer.m
//  iOSBridge
//
// Copyright © 2024 IronSource. All rights reserved.
//

#import "LPMInitializer.h"
#import "LPMUtilities.h"

#ifdef __cplusplus
extern "C" {
#endif
    static bool isUnityPauseGame = false;
    void UnitySendMessage(const char* obj, const char* method, const char* msg);
    
    void LPMInitialize(const char *appKey, const char *userId, const char **adFormats) {
        NSMutableArray *formatsArray = [NSMutableArray array];
        const char **current = adFormats;
        if(current != NULL){
            while (*current != NULL) {
                NSString *format = [LPMUtilities getStringFromCString:*current];
                if (format) {
                    [formatsArray addObject:format];
                }
                current++;
            }
        }
        [[LPMInitializer sharedInstance] LPMInitialize:[LPMUtilities getStringFromCString:appKey]
                                                userId:[LPMUtilities getStringFromCString:userId]
                                             adFormats:formatsArray];
    }
    
    void setPluginData(const char *pluginType, const char *pluginVersion, const char *pluginFrameworkVersion) {
        NSString *type = [LPMUtilities getStringFromCString:pluginType];
        NSString *version = [LPMUtilities getStringFromCString:pluginVersion];
        NSString *frameworkVersion = [LPMUtilities getStringFromCString:pluginFrameworkVersion];
        
        // Use the sharedInstance to set plugin data
        [[LPMInitializer sharedInstance] setPluginData:type pluginVersion:version pluginFrameworkVersion:frameworkVersion];
    }
    
    void LPMSetPauseGame(BOOL pause) {
        isUnityPauseGame = pause;
    }
    
#ifdef __cplusplus
}
#endif

@implementation LPMInitializer

+ (instancetype)sharedInstance {
    static LPMInitializer *sharedInstance = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        sharedInstance = [[self alloc] init];
    });
    return sharedInstance;
}

- (void)LPMInitialize:(NSString *)appKey userId:(NSString *)userId adFormats:(NSArray *)adFormats {
    
    LPMInitRequestBuilder *requestBuilder = [[LPMInitRequestBuilder alloc] initWithAppKey:appKey];
    [requestBuilder withUserId:userId];
    [requestBuilder withLegacyAdFormats:adFormats];
    LPMInitRequest *request = [requestBuilder build];
    
    [LevelPlay initWithRequest:request completion:^(LPMConfiguration * _Nullable config, NSError * _Nullable error) {
        if (error) {
            [self initializationDidFailWithError:error];
        } else {
            [self initializationDidCompleteWithConfiguration: config];
        }
    }];
}

- (void)setPluginData:(NSString *)pluginType pluginVersion:(NSString *)pluginVersion pluginFrameworkVersion:(NSString *)pluginFrameworkVersion {
    if (pluginType) {
        [ISConfigurations getConfigurations].plugin = pluginType;
    }
    
    if (pluginVersion) {
        [ISConfigurations getConfigurations].pluginVersion = pluginVersion;
    }
    
    if (pluginFrameworkVersion) {
        [ISConfigurations getConfigurations].pluginFrameworkVersion = pluginFrameworkVersion;
    }
    NSLog(@"PLUGIN IS: %@ %@ %@", [ISConfigurations getConfigurations].plugin, [ISConfigurations getConfigurations].pluginVersion, [ISConfigurations getConfigurations].pluginFrameworkVersion);
}

- (void)LPMSetPauseGame:(BOOL)pause {
    isUnityPauseGame = pause;
}

- (BOOL)isUnityPauseGame {
    return isUnityPauseGame;
}

- (NSString *)serializeConfigToJSON:(LPMConfiguration *)config {
    NSDictionary *configDict = @{
        @"isAdQualityEnabled": config.isAdQualityEnabled == true ? @"true" : @"false"
    };
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:configDict options:0 error:&error];
    return jsonData ? [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding] : @"";
}

- (void)initializationDidCompleteWithConfiguration:(LPMConfiguration *)config {
    NSString *jsonString = [self serializeConfigToJSON:config];
    const char *configString = [jsonString UTF8String];
    UnitySendMessage("IosLevelPlaySdk", "OnInitializationSuccess", configString);
}

- (void)initializationDidFailWithError:(NSError *)error {
    NSString *jsonString = [LPMUtilities serializeErrorToJSON:error];
    const char *errorString = [jsonString UTF8String];
    UnitySendMessage("IosLevelPlaySdk", "OnInitializationFailed", errorString);
}

@end
