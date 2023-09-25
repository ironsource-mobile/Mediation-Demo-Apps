//
//  NativeAdView.h
//  IronSourceDemoApp
//
//  Created by Luga Wang on 9/15/23.
//  Copyright © 2023 supersonic. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <IronSource/IronSource.h>

@interface NativeAdView : UIView

@property (weak, nonatomic) IBOutlet UILabel *adAdvertiserView;
@property (weak, nonatomic) IBOutlet UIImage *adAppIcon;
@property (weak, nonatomic) IBOutlet UILabel *adBodyView;
@property (weak, nonatomic) IBOutlet UIButton *adCallToActionView;
@property (weak, nonatomic) IBOutlet UIView *adMediaView;
@property (weak, nonatomic) IBOutlet UILabel *adTitleView;

@property (weak, nonatomic) IBOutlet UIButton *deleteButton;

@property (nonatomic, strong) LevelPlayNativeAd *nativeAd;
@property (nonatomic, strong) ISNativeAdView *nativeAdLayout;

- (void)loadNativeAdLayout: (LevelPlayNativeAd *)nativeAd;

@end
