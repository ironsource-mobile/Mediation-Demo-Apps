//
//  NativeAdViewController.m
//  IronSourceDemoApp
//
//  Created by Luga Wang on 9/15/23.
//  Copyright © 2023 supersonic. All rights reserved.
//

#import "NativeAdViewController.h"
#import "NativeAdView.h"
#import <IronSource/IronSource.h>

@interface NativeAdViewController () <LevelPlayNativeAdDelegate, UITableViewDelegate, UITableViewDataSource>

@property (strong, nonatomic) IBOutlet UITableView *tableView;
@property (weak, nonatomic) IBOutlet UIButton *loadNAButton;
@property (nonatomic, strong) NSMutableArray<NSArray *> *nativeAdlist;
@property (nonatomic, strong) NSMutableArray *tableViewData;

@end

@implementation NativeAdViewController

- (instancetype)initWithCoder:(NSCoder *)aDecoder {
    self = [super initWithCoder:aDecoder];
    if (self) {
        _nativeAdlist = [NSMutableArray array];
        _tableViewData = [NSMutableArray array];
    }
    return self;
}

- (void)viewDidLoad {
    [super viewDidLoad];
}


- (IBAction)loadNAButtonTapped:(id)sender {
    // This will load a Native Ad.
    
    LevelPlayNativeAd *nativeAd = [[[[LevelPlayNativeAdBuilder new] withViewController:self] withPlacementName:@""] withDelegate:self].build;
    
    if (nativeAd) {
        [nativeAd loadAd];
        
        NativeAdView *newView = [[NativeAdView alloc] init];
        [self.nativeAdlist addObject:@[nativeAd, newView]];
    }
}

//- (IBAction)deleteButtonTapped:(UIButton *)sender {
//    // Find the corresponding NativeAdView and remove it
//    for (NSArray *pair in self.nativeAdlist) {
//        if (pair.count == 2) {
//            LevelPlayNativeAd *nativeAd = pair[0];
//            NativeAdView *adView = pair[1];
//            if (adView.deleteButton == sender) {
//                [nativeAd destroyAd];
//                [adView removeFromSuperview];
//                [self.nativeAdlist removeObject:pair];
//                [self arrangeAdViews];
//                break;
//            }
//        }
//    }
//}

#pragma mark - Native Ad Delegate Functions

// This method gets invoked after a failed attempt to load Native Ad.
// If it does happen, check out 'error' for more information and consult our
// Knowledge center.
- (void)didFailToLoad:(nonnull LevelPlayNativeAd *)nativeAd withError:(nonnull NSError *)error {
    NSLog(@"%s",__PRETTY_FUNCTION__);
}

- (void)didLoad:(nonnull LevelPlayNativeAd *)nativeAd withAdInfo:(nonnull ISAdInfo *)adInfo {
    NSLog(@"%s",__PRETTY_FUNCTION__);
    
    NSInteger index = -1;
    for (NSInteger i = 0; i < self.nativeAdlist.count; i++) {
        NSArray *pair = self.nativeAdlist[i];
        if ([pair.firstObject isEqual:nativeAd]) {
            index = i;
            break;
        }
    }
    
    if (index != -1) {
        NSArray *pair = self.nativeAdlist[index];
        NativeAdView *adView = pair[1];
        
        [adView loadNativeAdLayout:nativeAd];
        
        NSIndexPath *indexPath = [NSIndexPath indexPathForRow:index inSection:0];
        [self.tableViewData addObject:adView]; // Add the adView to your data source.
        [self.tableView insertRowsAtIndexPaths:@[indexPath] withRowAnimation:UITableViewRowAnimationAutomatic];
    }
}

// This method gets invoked after a video has been clicked
- (void)didClick:(nonnull LevelPlayNativeAd *)nativeAd withAdInfo:(nonnull ISAdInfo *)adInfo {
    NSLog(@"%s",__PRETTY_FUNCTION__);
}

- (void)didRecordImpression:(nonnull LevelPlayNativeAd *)nativeAd withAdInfo:(nonnull ISAdInfo *)adInfo {
    NSLog(@"%s",__PRETTY_FUNCTION__);
}

- (void)encodeWithCoder:(nonnull NSCoder *)coder {
    NSLog(@"%s",__PRETTY_FUNCTION__);
}

- (void)traitCollectionDidChange:(nullable UITraitCollection *)previousTraitCollection {
    NSLog(@"%s",__PRETTY_FUNCTION__);
}

- (void)preferredContentSizeDidChangeForChildContentContainer:(nonnull id<UIContentContainer>)container {
    NSLog(@"%s",__PRETTY_FUNCTION__);
}

- (CGSize)sizeForChildContentContainer:(nonnull id<UIContentContainer>)container withParentContainerSize:(CGSize)parentSize {
    NSLog(@"%s",__PRETTY_FUNCTION__);
    return parentSize; //?
}

- (void)systemLayoutFittingSizeDidChangeForChildContentContainer:(nonnull id<UIContentContainer>)container {
    NSLog(@"%s",__PRETTY_FUNCTION__);
}

- (void)viewWillTransitionToSize:(CGSize)size withTransitionCoordinator:(nonnull id<UIViewControllerTransitionCoordinator>)coordinator {
    NSLog(@"%s",__PRETTY_FUNCTION__);
}

- (void)willTransitionToTraitCollection:(nonnull UITraitCollection *)newCollection withTransitionCoordinator:(nonnull id<UIViewControllerTransitionCoordinator>)coordinator {
    NSLog(@"%s",__PRETTY_FUNCTION__);
}

- (void)didUpdateFocusInContext:(nonnull UIFocusUpdateContext *)context withAnimationCoordinator:(nonnull UIFocusAnimationCoordinator *)coordinator {
    NSLog(@"%s",__PRETTY_FUNCTION__);
}

- (void)setNeedsFocusUpdate {
    NSLog(@"%s",__PRETTY_FUNCTION__);
}

- (BOOL)shouldUpdateFocusInContext:(nonnull UIFocusUpdateContext *)context {
    NSLog(@"%s",__PRETTY_FUNCTION__);
    return YES; //?
}

- (void)updateFocusIfNeeded {
    NSLog(@"%s",__PRETTY_FUNCTION__);
}

#pragma mark - UI TableView Delegate Functions
- (nonnull UITableViewCell *)tableView:(nonnull UITableView *)tableView cellForRowAtIndexPath:(nonnull NSIndexPath *)indexPath {
    id cellData = _tableViewData[indexPath.row];
    if ([cellData isKindOfClass:[NativeAdView class]]) {
        // Return the NativeAdView as a cell.
        NativeAdView *adView = (NativeAdView *)cellData;
        UITableViewCell *adCell = [[UITableViewCell alloc] init];
        [adCell.contentView addSubview:adView];
        
        [NSLayoutConstraint activateConstraints:@[
            [adView.widthAnchor constraintEqualToAnchor:adCell.contentView.widthAnchor constant:0],
            [adView.centerXAnchor constraintEqualToAnchor:adCell.contentView.centerXAnchor constant:0],
            
            [adCell.contentView.heightAnchor constraintEqualToAnchor:adView.heightAnchor constant:0],
            [adView.centerYAnchor constraintEqualToAnchor:adCell.contentView.centerYAnchor constant:0],
        ]];
        
        return adCell;
    }
    return nil;
}

- (NSInteger)tableView:(nonnull UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    return _tableViewData.count;
}

@end
