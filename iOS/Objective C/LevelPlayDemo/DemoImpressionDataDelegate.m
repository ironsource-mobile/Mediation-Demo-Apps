//
//  DemoImpressionDataDelegate.m
//  LevelPlayDemo
//
//  Copyright © 2025 LevelPlay Ltd. All rights reserved.
//

#import "DemoImpressionDataDelegate.h"

@implementation DemoImpressionDataDelegate

/**
 Called when the ad was displayed successfully and the impression data was recorded
 @param impressionData The recorded impression data 
 */
- (void)impressionDataDidSucceed:(LPMImpressionData *)impressionData { 
  logCallbackName();
}

@end
