//
//  LPMUtilities.h
//  iOSBridge
//
//  Copyright © 2024 IronSource. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <IronSource/LPMAdInfo.h>
#import <IronSource/LPMAdSize.h>

@interface LPMUtilities : NSObject

// Converts C style string to NSString
+ (NSString *)getStringFromCString:(const char *)_x_;

// LPMAdInfo conversion methods
+ (NSString *)serializeAdInfoToJSON:(LPMAdInfo *)adInfo;
+ (NSString *)serializeErrorToJSON:(NSError *)adError;
+ (NSString *)serializeErrorToJSON:(NSError *)adError adUnitId:(NSString *)adUnitId;

@end
