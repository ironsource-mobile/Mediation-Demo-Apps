//
//  DemoImpressionDataDelegate.m
//  LevelPlayDemo
//
//  Copyright © 2021-2025 Unity Technologies. All rights reserved.
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
