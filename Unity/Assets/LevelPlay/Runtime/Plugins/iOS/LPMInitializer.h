//
//  LPMInitializer.h
//  iOSBridge
//
// Copyright © 2024 IronSource. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <IronSource/IronSource.h>
#import <IronSource/LevelPlay.h>
#import <IronSource/LPMInitRequestBuilder.h>

@interface LPMInitializer : NSObject
+ (instancetype)sharedInstance;
- (void)LPMInitialize:(NSString *)appKey userId:(NSString *)userId adFormats:(NSArray *)adFormats;
- (void)setPluginData:(NSString *)pluginType pluginVersion:(NSString *)pluginVersion pluginFrameworkVersion:(NSString *)pluginFrameworkVersion;
- (void)LPMSetPauseGame:(BOOL) pauseGame;
- (BOOL)isUnityPauseGame;
@end

