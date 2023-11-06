//
//  InterstitialLevelPlayCallbacksHandler.swift
//  IronSourceSwiftDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.
//

import Foundation
import IronSource

protocol InterstitialLevelPlayCallbacksWrapper {
    func interstitialLevelPlayDidLoad(with adInfo: ISAdInfo!)
    func interstitialLevelPlayDidFailToLoadWithError(_ error: Error!)
    func interstitialLevelPlayDidOpen(with adInfo: ISAdInfo!)
    func interstitialLevelPlayDidShow(with adInfo: ISAdInfo!)
    func interstitialLevelPlayDidFailToShowWithError(_ error: Error!, andAdInfo adInfo: ISAdInfo!)
    func interstitialLevelPlayDidClick(with adInfo: ISAdInfo!)
    func interstitialLevelPlayDidClose(with adInfo: ISAdInfo!)
}

class InterstitialLevelPlayCallbacksHandler: NSObject, LevelPlayInterstitialDelegate {
    
    var delegate: InterstitialLevelPlayCallbacksWrapper!

    init(delegate: InterstitialLevelPlayCallbacksWrapper!) {
        self.delegate = delegate
    }
    
    /**
     Called after an interstitial has been loaded
     @param adInfo The info of the ad.
     */
    func didLoad(with adInfo: ISAdInfo) {
        delegate.interstitialLevelPlayDidLoad(with: adInfo)
    }
    
    /**
     Called after an interstitial has attempted to load but failed.
     @param error The reason for the error
     */
    func didFailToLoadWithError(_ error: Error!) {
        delegate.interstitialLevelPlayDidFailToLoadWithError(error)
    }
    
    /**
     Called after an interstitial has been opened.
     @param adInfo The info of the ad.
     */
    func didOpen(with adInfo: ISAdInfo!) {
        delegate.interstitialLevelPlayDidOpen(with: adInfo)
    }
    
    /**
     Called after an interstitial has been displayed on the screen.
     @param adInfo The info of the ad.
     */
    func didShow(with adInfo: ISAdInfo!) {
        delegate.interstitialLevelPlayDidShow(with: adInfo)
    }
    
    /**
     Called after an interstitial has attempted to show but failed.
     @param error The reason for the error
     @param adInfo The info of the ad.
     */
    func didFailToShowWithError(_ error: Error!, andAdInfo adInfo: ISAdInfo!) {
        delegate.interstitialLevelPlayDidFailToShowWithError(error, andAdInfo: adInfo)
    }
    
    /**
     Called after an interstitial has been clicked.
     @param adInfo The info of the ad.
     */
    func didClick(with adInfo: ISAdInfo!) {
        delegate.interstitialLevelPlayDidClick(with: adInfo)
    }
    
    /**
     Called after an interstitial has been dismissed.
     @param adInfo The info of the ad.
     */
    func didClose(with adInfo: ISAdInfo!) {
        delegate.interstitialLevelPlayDidClose(with: adInfo)
    }
}
