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
    self = (NativeAdView *)[nibContents firstObject];
    [self configDeleteButton];
    self.translatesAutoresizingMaskIntoConstraints = NO;
    
    return self;
}

// This adding a customized delete button to the NativeAdView that removes the native ad from the table view and call the destory API.
- (void) configDeleteButton {
    _deleteButton = [UIButton buttonWithType:UIButtonTypeCustom];
    _deleteButton.showsTouchWhenHighlighted = YES;
    [_deleteButton setTitle:@"x" forState:UIControlStateNormal];
    _deleteButton.backgroundColor = [UIColor lightGrayColor];
    _deleteButton.layer.cornerRadius = 15;
    [_deleteButton addTarget:self action:@selector(deleteButtonTapped:) forControlEvents:UIControlEventTouchUpInside];
    _deleteButton.translatesAutoresizingMaskIntoConstraints = NO;
    
    _topView = self.subviews[0];
    [_topView addSubview:_deleteButton];

    [NSLayoutConstraint activateConstraints:@[
        [_deleteButton.widthAnchor constraintEqualToConstant:30],
        [_deleteButton.heightAnchor constraintEqualToAnchor:_topView.heightAnchor],
        [_deleteButton.centerYAnchor constraintEqualToAnchor:_topView.centerYAnchor],
        [_deleteButton.trailingAnchor constraintEqualToAnchor:_topView.trailingAnchor],
    ]];
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
    
    [self registerNativeAdViews:_levelPlayNativeAd];
    
    // Bring the delete button to the front of the layer
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
