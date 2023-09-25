//
//  NativeAdView.m
//  IronSourceDemoApp
//
//  Created by Luga Wang on 9/15/23.
//  Copyright © 2023 supersonic. All rights reserved.
//

#import "NativeAdView.h"

@implementation NativeAdView

- (instancetype)init {
    self = [super init];
    if (self) {
        [self loadViewFromNib];
    }
    return self;
}

- (void)loadViewFromNib {
    UINib *nib = [UINib nibWithNibName:@"LevelPlayNativeAdView" bundle:[NSBundle mainBundle]];
    NSArray *nibContents = [nib instantiateWithOwner:nil options:nil];
    _nativeAdLayout = (ISNativeAdView *)[nibContents firstObject] ?: [[ISNativeAdView alloc] init];
}

- (void)loadNativeAdLayout:(LevelPlayNativeAd *)nativeAd {
    self.nativeAd = nativeAd;
    
    if (nativeAd.icon.image) {
        self.nativeAdLayout.adAppIcon.image = nativeAd.icon.image;
    } else {
        [self.nativeAdLayout.adAppIcon removeFromSuperview];
    }
    self.nativeAdLayout.adTitleView.text = nativeAd.title;
    self.nativeAdLayout.adAdvertiserView.text = nativeAd.advertiser;
    self.nativeAdLayout.adBodyView.text = nativeAd.body;
    [self.nativeAdLayout.adCallToActionView setTitle:nativeAd.callToAction forState:UIControlStateNormal];
    self.nativeAdLayout.adCallToActionView.userInteractionEnabled = NO;

    [self.nativeAdLayout setNativeAd:self.nativeAd ?: nativeAd];

    [self invalidateIntrinsicContentSize];
}


@end
