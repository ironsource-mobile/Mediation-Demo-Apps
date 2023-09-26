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
    _nativeAd = nativeAd;
    
    if (_nativeAd.icon.image) {
        self.adAppIcon.image = _nativeAd.icon.image;
    } else {
        [self.adAppIcon removeFromSuperview];
    }
    self.adTitleView.text = _nativeAd.title;
    self.adAdvertiserView.text = _nativeAd.advertiser;
    self.adBodyView.text = _nativeAd.body;
    [self.adCallToActionView setTitle:_nativeAd.callToAction forState:UIControlStateNormal];
    self.adCallToActionView.userInteractionEnabled = NO;
    
    // The below part is copied from ISNativeAdView's (void)setNativeAd method.
    // For some unknow reason these parts were not triggered.
    // adMediaView and networkNativeAdView was affected the most.
    // Should have the below single line instead:
    // [self setNativeAd:nativeAd];
    
    ISAdapterNativeAdViewBinder *binder = _nativeAd.nativeAdViewBinder;
    if (binder) {
        [binder setIconView:self.adAppIcon];
        [binder setTitleView:self.adTitleView];
        [binder setAdvertiserView:self.adAdvertiserView];
        [binder setBodyView:self.adBodyView];
        [binder setMediaView:self.adMediaView];
        [binder setCallToActionView:self.adCallToActionView];

        // This last part of the binding is adding the container to the NetworkNativeAdView
        [binder setNativeAdView:self];
        
        // Then we add the NetworkNativeAdView as a child to this NativeAdLayout
        binder.networkNativeAdView.translatesAutoresizingMaskIntoConstraints = false;
        [self addSubview:binder.networkNativeAdView];
        [NSLayoutConstraint activateConstraints:@[
            [binder.networkNativeAdView.topAnchor constraintEqualToAnchor:self.topAnchor],
            [binder.networkNativeAdView.leadingAnchor constraintEqualToAnchor:self.leadingAnchor],
            [binder.networkNativeAdView.bottomAnchor constraintEqualToAnchor:self.bottomAnchor],
            [binder.networkNativeAdView.trailingAnchor constraintEqualToAnchor:self.trailingAnchor]
        ]];
    }
}

- (IBAction)deleteButtonTapped:(UIButton *)sender {
    
    [_nativeAd destroyAd];
    [self removeFromSuperview];
    //[self.nativeAdlist removeObject:pair];
    //[self arrangeAdViews];
    //break;
    
}


@end
