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
    [self setupTopView];
    self.translatesAutoresizingMaskIntoConstraints = NO;
    
    return self;
}

// This adding a customized top view to the NativeAdView that contains an adBadge on the left and a delete button on the right.
// The delete button is to remove the native ad from the table view.
- (void)setupTopView {
    // Initialize UI elements
    _adBadge = [[UILabel alloc] init];
    _deleteButton = [UIButton buttonWithType:UIButtonTypeCustom];
    _topView = [[UIView alloc] init];
    
    // Configure adBadge
    _adBadge.text = @"AD";
    _adBadge.font = [UIFont systemFontOfSize:10];
    _adBadge.textAlignment = NSTextAlignmentCenter;
    _adBadge.textColor = [UIColor whiteColor];
    _adBadge.backgroundColor = [UIColor colorWithRed:0.0 green:0.0 blue:1.0 alpha:1.0]; // Replace with your desired color
    _adBadge.layer.cornerRadius = 4.5;
    _adBadge.clipsToBounds = YES;
    _adBadge.translatesAutoresizingMaskIntoConstraints = NO;
    
    // Configure deleteButton
    [_deleteButton setTitle:@"x" forState:UIControlStateNormal];
    _deleteButton.backgroundColor = [UIColor lightGrayColor];
    _deleteButton.layer.cornerRadius = 15;
    [_deleteButton addTarget:self action:@selector(deleteButtonTapped:) forControlEvents:UIControlEventTouchUpInside];
    _deleteButton.translatesAutoresizingMaskIntoConstraints = NO;
    
    // Add subviews
    [_topView addSubview:_adBadge];
    [_topView addSubview:_deleteButton];
    _topView.translatesAutoresizingMaskIntoConstraints = NO;
    [self addSubview:_topView];
    
    // Get the current top view
    UIView *firstSubview = self.subviews[0];
    
    // Set up constraints
    [NSLayoutConstraint activateConstraints:@[
        [_topView.leadingAnchor constraintEqualToAnchor:self.leadingAnchor constant:8],
        [_topView.trailingAnchor constraintEqualToAnchor:self.trailingAnchor constant:-8],
        [_topView.heightAnchor constraintEqualToConstant:30],
        
        [_adBadge.widthAnchor constraintEqualToConstant:20],
        [_adBadge.heightAnchor constraintEqualToConstant:20],
        [_adBadge.leadingAnchor constraintEqualToAnchor:_topView.leadingAnchor],
        [_deleteButton.widthAnchor constraintEqualToConstant:30],
        [_deleteButton.heightAnchor constraintEqualToAnchor:_topView.heightAnchor],
        [_deleteButton.trailingAnchor constraintEqualToAnchor:_topView.trailingAnchor],
        
        // Set the _topView as the top-most view for the native ad.
        [self.topAnchor constraintEqualToAnchor:_topView.topAnchor constant:-8],
        [firstSubview.topAnchor constraintEqualToAnchor:_topView.bottomAnchor constant:8],
    ]];
}

- (void)setClearBackgroundRecursively:(UIView *)parentView {
    if (![parentView isKindOfClass:[UIButton class]]) {
        parentView.backgroundColor = [UIColor clearColor];
    }
    for (UIView *subview in parentView.subviews) {
        [self setClearBackgroundRecursively:subview];
    }
}

// Predefine the colors and constraints of the native ad's properties.
// Replace with your desired colors and look.
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

// This was triggered after a successful load of a native ad.
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
    
    // Bring the adBadge and the delete button to the front of the layer
    [self bringSubviewToFront:_topView];
}

// Remove the native ad from the table view.
- (void)deleteButtonTapped:(UIButton *)sender {
    // Check if the delegate responds to the delete button tap selector
    if ([self.delegate respondsToSelector:@selector(nativeAdViewDeleteButtonTapped:)]) {
        // Call the delegate method
        [self.delegate nativeAdViewDeleteButtonTapped:self];
    }
    
}


@end
