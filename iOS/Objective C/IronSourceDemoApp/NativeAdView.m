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
        UINib *nib = [UINib nibWithNibName:@"LevelPlayNativeAdView" bundle:[NSBundle mainBundle]];
        NSArray *nibContents = [nib instantiateWithOwner:nil options:nil];
        _isNativeAdView = (ISNativeAdView *)[nibContents firstObject];
        [self configDeleteButton];
        
        // Avoid conflicts of constraints
        self.translatesAutoresizingMaskIntoConstraints = NO;
        _isNativeAdView.translatesAutoresizingMaskIntoConstraints = NO;
    }
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
    
    [_isNativeAdView addSubview:_deleteButton];
    [NSLayoutConstraint activateConstraints:@[
        [_deleteButton.widthAnchor constraintEqualToConstant:30],
        [_deleteButton.heightAnchor constraintEqualToConstant:30],
        [_deleteButton.topAnchor constraintEqualToAnchor:_isNativeAdView.topAnchor],
        [_deleteButton.trailingAnchor constraintEqualToAnchor:_isNativeAdView.trailingAnchor],
    ]];
}

// This was triggered after a successful load of a native ad.
- (void)loadNativeAdLayout:(LevelPlayNativeAd *)nativeAd {
    _levelPlayNativeAd = nativeAd;
    
    if (_levelPlayNativeAd.icon.image) {
        _isNativeAdView.adAppIcon.image = _levelPlayNativeAd.icon.image;
    } else {
        [_isNativeAdView.adAppIcon removeFromSuperview];
    }
    
    if (_levelPlayNativeAd.title) {
        _isNativeAdView.adTitleView.text = _levelPlayNativeAd.title;
    }
    
    if (_levelPlayNativeAd.advertiser) {
        _isNativeAdView.adAdvertiserView.text = _levelPlayNativeAd.advertiser;
    }
    
    if (_levelPlayNativeAd.body) {
        _isNativeAdView.adBodyView.text = _levelPlayNativeAd.body;
    }
    
    if (_levelPlayNativeAd.callToAction) {
        [_isNativeAdView.adCallToActionView setTitle:_levelPlayNativeAd.callToAction forState:UIControlStateNormal];
        _isNativeAdView.adCallToActionView.userInteractionEnabled = NO;
    }
    
    [_isNativeAdView registerNativeAdViews:_levelPlayNativeAd];
    
    // Bring the delete button to the front of the layer
    [_isNativeAdView bringSubviewToFront:_deleteButton];
    
    [self addSubview:_isNativeAdView];
    // Constraints between self and _isNativeAdView
    [NSLayoutConstraint activateConstraints:@[
        [_isNativeAdView.widthAnchor constraintEqualToAnchor:self.widthAnchor],
        [self.heightAnchor constraintEqualToAnchor:_isNativeAdView.heightAnchor],
        [_isNativeAdView.centerXAnchor constraintEqualToAnchor:self.centerXAnchor],
    ]];
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
