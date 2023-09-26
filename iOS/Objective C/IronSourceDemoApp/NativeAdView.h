//
//  NativeAdView.h
//  IronSourceDemoApp
//
//  Created by Luga Wang on 9/15/23.
//  Copyright © 2023 supersonic. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <IronSource/IronSource.h>

@interface NativeAdView : ISNativeAdView

@property (weak, nonatomic) IBOutlet UIButton *deleteButton;
@property (nonatomic, strong) LevelPlayNativeAd *nativeAd;

- (void)loadNativeAdLayout: (LevelPlayNativeAd *)nativeAd;

@end
