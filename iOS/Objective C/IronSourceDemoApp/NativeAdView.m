//
//  NativeAdView.m
//  IronSourceDemoApp
//
//  Created by Luga Wang on 9/15/23.
//  Copyright © 2023 supersonic. All rights reserved.
//

#import "NativeAdView.h"

@implementation NativeAdView

+ (instancetype)loadFromNib {
    NSArray *nibContents = [[NSBundle mainBundle] loadNibNamed:@"LevelPlayNativeAdView" owner:nil options:nil];
    return [nibContents firstObject];
}

- (IBAction)deleteButtonTapped:(id)sender {
    // Delete the current Native Ad from the view
    [self removeFromSuperview];
}

@end
