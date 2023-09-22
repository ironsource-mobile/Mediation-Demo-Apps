//
//  NativeAdView.h
//  IronSourceDemoApp
//
//  Created by Luga Wang on 9/15/23.
//  Copyright © 2023 supersonic. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface NativeAdView : UIView

@property (weak, nonatomic) IBOutlet UIView *adAdvertiserView;
@property (weak, nonatomic) IBOutlet UIImage *adAppIcon;
@property (weak, nonatomic) IBOutlet UIView *adBodyView;
@property (weak, nonatomic) IBOutlet UIView *adCallToActionView;
@property (weak, nonatomic) IBOutlet UIView *adMediaView;
@property (weak, nonatomic) IBOutlet UIView *adTitleView;
@property (weak, nonatomic) IBOutlet UIButton *deleteButton;

+ (instancetype)loadFromNib;
- (IBAction)deleteButtonTapped:(id)sender;

@end
