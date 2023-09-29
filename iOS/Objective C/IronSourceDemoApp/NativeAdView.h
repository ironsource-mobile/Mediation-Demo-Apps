//
//  NativeAdView.h
//  IronSourceDemoApp
//
//  Created by Luga Wang on 9/15/23.
//  Copyright © 2023 supersonic. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <IronSource/IronSource.h>

@class NativeAdView;
@protocol NativeAdViewDelegate <NSObject>
- (void)nativeAdViewDeleteButtonTapped:(NativeAdView *)adView;
@end

@interface NativeAdView : ISNativeAdView

@property (nonatomic, weak) id<NativeAdViewDelegate> delegate;

@property (nonatomic, strong) LevelPlayNativeAd *nativeAd;
- (void)loadNativeAdLayout: (LevelPlayNativeAd *)nativeAd;

@property (nonatomic, strong) UILabel *adBadge;
@property (nonatomic, strong) UIButton *deleteButton;
@property (nonatomic, strong) UIView *topView;

@end
