//
//  BannerLevelPlayCallbacksHandler.swift
//  IronSourceSwiftDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.
//

import Foundation
import IronSource

protocol BannerLevelPlayCallbacksWrapper {
    func bannerLevelPlayDidLoad(_ bannerView: ISBannerView!, andAdInfo adInfo: ISAdInfo!)
    func bannerLevelPlayDidFailToLoadWithError(_ error: Error!)
    func bannerLevelPlayDidClick(with adInfo: ISAdInfo!)
    func bannerLevelPlayDidLeaveApplication(with adInfo: ISAdInfo!)
    func bannerLevelPlayDidPresentScreen(with adInfo: ISAdInfo!)
    func bannerLevelPlayDidDismissScreen(with adInfo: ISAdInfo!)
}

class BannerLevelPlayCallbacksHandler: NSObject, LevelPlayBannerDelegate {

    var delegate: BannerLevelPlayCallbacksWrapper!

    init(delegate: BannerLevelPlayCallbacksWrapper!) {
        self.delegate = delegate
    }
    
    /**
     Called after a banner ad has been successfully loaded
     @param adInfo The info of the ad.
     */
    func didLoad(_ bannerView: ISBannerView!, with adInfo: ISAdInfo!) {
        delegate.bannerLevelPlayDidLoad(bannerView, andAdInfo: adInfo)
    }
    
    /**
     Called after a banner has attempted to load an ad but failed.
     @param error The reason for the error
     */
    func didFailToLoadWithError(_ error: Error!) {
        delegate.bannerLevelPlayDidFailToLoadWithError(error)
    }
    
    /**
     Called after a banner has been clicked.
     @param adInfo The info of the ad.
     */
    func didClick(with adInfo: ISAdInfo!) {
        delegate.bannerLevelPlayDidClick(with: adInfo)
    }
    
    /**
     Called when a user was taken out of the application context.
     @param adInfo The info of the ad.
     */
    func didLeaveApplication(with adInfo: ISAdInfo!) {
        delegate.bannerLevelPlayDidLeaveApplication(with: adInfo)
    }
    
    /**
     Called when a banner presented a full screen content.
     @param adInfo The info of the ad.
     */
    func didPresentScreen(with adInfo: ISAdInfo!) {
        delegate.bannerLevelPlayDidPresentScreen(with: adInfo)
    }
    
    /**
     Called after a full screen content has been dismissed.
     @param adInfo The info of the ad.
     */
    func didDismissScreen(with adInfo: ISAdInfo!) {
        delegate.bannerLevelPlayDidDismissScreen(with: adInfo)
    }
}

