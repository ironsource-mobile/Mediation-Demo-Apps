//
//  NativeAdView.m
//  IronSourceDemoApp
//
//  Created by Luga Wang on 9/15/23.
//  Copyright © 2023 supersonic. All rights reserved.
//

#import "NativeAdView.h"

@implementation NativeAdView

@dynamic adAppIcon;
@dynamic adTitleView;
@dynamic adAdvertiserView;
@dynamic adBodyView;
@dynamic adMediaView;
@dynamic adCallToActionView;

- (instancetype)init {
    UINib *nib = [UINib nibWithNibName:@"LevelPlayNativeAdView" bundle:[NSBundle mainBundle]];
    NSArray *nibContents = [nib instantiateWithOwner:nil options:nil];
    NativeAdView *nativeAdView = (NativeAdView *)[nibContents firstObject];
    
    if (!nativeAdView) {
        nativeAdView = [[NativeAdView alloc] init];
    }
    
    self = nativeAdView;
    [self setNativeAdViewConstraints];
    self.translatesAutoresizingMaskIntoConstraints = NO;
    //}
    return self;
}

- (void)setClearBackgroundRecursively:(UIView *)parentView {
    if (![parentView isKindOfClass:[UIButton class]]) {
        parentView.backgroundColor = [UIColor clearColor];
    }
    for (UIView *subview in parentView.subviews) {
        [self setClearBackgroundRecursively:subview];
    }
}

- (void)setNativeAdViewConstraints {
    [self setClearBackgroundRecursively:self];
    self.translatesAutoresizingMaskIntoConstraints = NO;
    
    self.adTitleView.textColor = UIColor.blackColor;
    self.adAdvertiserView.textColor = UIColor.blackColor;
    self.adBodyView.textColor = UIColor.blackColor;
    [self.adCallToActionView setTitleColor:[UIColor blueColor] forState:UIControlStateNormal];
    
    UIView *mediaView = self.adMediaView;
    if (mediaView) {
        for (UIView *subview in mediaView.subviews) {
            [subview.centerXAnchor constraintEqualToAnchor:self.centerXAnchor].active = YES;
        }
    }
}

- (void)loadNativeAdLayout:(LevelPlayNativeAd *)nativeAd {
    self.nativeAd = nativeAd;
    
    if (nativeAd.icon.image) {
        self.adAppIcon.image = nativeAd.icon.image;
    } else {
        [self.adAppIcon removeFromSuperview];
    }
    self.adTitleView.text = nativeAd.title;
    self.adAdvertiserView.text = nativeAd.advertiser;
    self.adBodyView.text = nativeAd.body;
    [self.adCallToActionView setTitle:nativeAd.callToAction forState:UIControlStateNormal];
    self.adCallToActionView.userInteractionEnabled = NO;
    
    [self setNativeAd:self.nativeAd];
}

- (IBAction)deleteButtonTapped:(UIButton *)sender {
    
    [_nativeAd destroyAd];
    [self removeFromSuperview];
    //[self.nativeAdlist removeObject:pair];
    //[self arrangeAdViews];
    //break;
    
}


@end
