//
//  ImpressionDataDelegate.m
//  IronSourceDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.


#import "ImpressionDataDelegate.h"

@implementation ImpressionDataDelegate

- (void)impressionDataDidSucceed:(ISImpressionData *)impressionData {
    NSLog(@"impressionDataDidSucceed with impressionData %@", impressionData);
}

@end
