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
        [self setNativeAdViewConstraints];
        self.translatesAutoresizingMaskIntoConstraints = NO;
    }
    return self;
}

- (void)loadViewFromNib {
    UINib *nib = [UINib nibWithNibName:@"LevelPlayNativeAdView" bundle:[NSBundle mainBundle]];
    NSArray *nibContents = [nib instantiateWithOwner:nil options:nil];
    _nativeAdLayout = (ISNativeAdView *)[nibContents firstObject] ?: [[ISNativeAdView alloc] init];
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
    [self setClearBackgroundRecursively:self.nativeAdLayout];
    self.nativeAdLayout.translatesAutoresizingMaskIntoConstraints = NO;
    [self addSubview:self.nativeAdLayout];
    [NSLayoutConstraint activateConstraints:@[
        [self.heightAnchor constraintEqualToAnchor:self.nativeAdLayout.heightAnchor constant:0],
        [self.nativeAdLayout.widthAnchor constraintEqualToAnchor:self.widthAnchor constant:0],
        [self.nativeAdLayout.centerXAnchor constraintEqualToAnchor:self.centerXAnchor constant:0],
        [self.nativeAdLayout.centerYAnchor constraintEqualToAnchor:self.centerYAnchor constant:0],
    ]];
    
    self.nativeAdLayout.adTitleView.textColor = UIColor.blackColor;
    self.nativeAdLayout.adAdvertiserView.textColor = UIColor.blackColor;
    self.nativeAdLayout.adBodyView.textColor = UIColor.blackColor;
    [self.nativeAdLayout.adCallToActionView setTitleColor:[UIColor blueColor] forState:UIControlStateNormal];
    
    UIView *mediaView = self.nativeAdLayout.adMediaView;
    if (mediaView) {
        for (UIView *subview in mediaView.subviews) {
            [subview.centerXAnchor constraintEqualToAnchor:self.centerXAnchor].active = YES;
        }
    }
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
}

- (IBAction)deleteButtonTapped:(UIButton *)sender {
    
    [_nativeAd destroyAd];
    [self removeFromSuperview];
    //[self.nativeAdlist removeObject:pair];
    //[self arrangeAdViews];
    //break;
    
}


@end
