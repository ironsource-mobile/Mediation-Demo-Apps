//
//  NativeAdViewController.m
//  IronSourceDemoApp
//
//  Created by Luga Wang on 9/15/23.
//  Copyright © 2023 supersonic. All rights reserved.
//

#import "NativeAdViewController.h"
#import "NativeAdView.h"

@interface NativeAdViewController ()

@property (weak, nonatomic) IBOutlet UIScrollView *scrollView;
@property (weak, nonatomic) IBOutlet UIButton *loadNAButton;

@property (nonatomic, strong) NSMutableArray<NativeAdView *> *nativeAdViews;

@end

@implementation NativeAdViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view.
}

- (IBAction)loadNAButtonTapped:(id)sender {
    
    // This will load a Native Ad.
    NativeAdView *newAdView = [NativeAdView loadFromNib];
    [newAdView.deleteButton addTarget:self action:@selector(deleteButtonTapped:) forControlEvents:UIControlEventTouchUpInside];
    
    [self.scrollView addSubview:newAdView];
    
    [self.nativeAdViews addObject:newAdView];
    
    [self arrangeAdViews];
}

- (void)arrangeAdViews {
    CGFloat yOffset = 0;
    for (NativeAdView *adView in self.nativeAdViews) {
        CGRect frame = adView.frame;
        frame.origin.y = yOffset;
        adView.frame = frame;
        yOffset += frame.size.height;
    }

    // Update the content size of the scroll view
    self.scrollView.contentSize = CGSizeMake(self.scrollView.frame.size.width, yOffset);
}

- (IBAction)deleteButtonTapped:(UIButton *)sender {
    // Find the corresponding NativeAdView and remove it
    for (NativeAdView *adView in self.nativeAdViews) {
        if (adView.deleteButton == sender) {
            [adView removeFromSuperview];
            [self.nativeAdViews removeObject:adView];
            [self arrangeAdViews];
            break; // Break the loop since we found the matching view
        }
    }
}

@end
