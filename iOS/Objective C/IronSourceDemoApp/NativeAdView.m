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
    UINib *nib = [UINib nibWithNibName:@"LevelPlayNativeAdView" bundle:[NSBundle mainBundle]];
    NSArray *nibContents = [nib instantiateWithOwner:nil options:nil];
    NativeAdView *nativeAdView = (NativeAdView *)[nibContents firstObject];
    
    self = nativeAdView;
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
    
    // Set up constraints
    [self setTopViewConstraints];
}

- (void)setTopViewConstraints {
    // Get the current top view
    UIView *firstSubview = self.subviews[0];
    
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

// This was triggered after a successful load of a native ad.
- (void)loadNativeAdLayout:(LevelPlayNativeAd *)nativeAd {
    _levelPlayNativeAd = nativeAd;
    
    if (_levelPlayNativeAd.icon.image) {
        self.adAppIcon.image = _levelPlayNativeAd.icon.image;
    } else {
        [self.adAppIcon removeFromSuperview];
    }
    if (_levelPlayNativeAd.title) {
        self.adTitleView.text = _levelPlayNativeAd.title;
    }
    if (_levelPlayNativeAd.advertiser) {
        self.adAdvertiserView.text = _levelPlayNativeAd.advertiser;
    }
    if (_levelPlayNativeAd.body) {
        self.adBodyView.text = _levelPlayNativeAd.body;
    }
    if (_levelPlayNativeAd.callToAction) {
        [self.adCallToActionView setTitle:_levelPlayNativeAd.callToAction forState:UIControlStateNormal];
        self.adCallToActionView.userInteractionEnabled = NO;
    }
    
    [self setNativeAd:_levelPlayNativeAd];

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
