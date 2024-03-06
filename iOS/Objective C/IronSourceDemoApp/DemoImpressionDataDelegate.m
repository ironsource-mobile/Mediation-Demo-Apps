//
//  DemoImpressionDataDelegate.m
//  IronSourceDemoApp
//
//  Copyright © 2024 ironSource Mobile Ltd. All rights reserved.
//

#import "DemoImpressionDataDelegate.h"

@implementation DemoImpressionDataDelegate

/**
 Called when the ad was displayed successfully and the impression data was recorded
 @param impressionData The recorded impression data 
 */
- (void)impressionDataDidSucceed:(ISImpressionData *)impressionData {
    logCallbackName();
}

@end
