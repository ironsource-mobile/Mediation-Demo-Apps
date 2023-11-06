//
//  InterstitialLevelPlayCallbacksHandler.swift
//  IronSourceSwiftDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.
//

import Foundation
import IronSource

class InterstitialLevelPlayCallbacksHandler: NSObject, LevelPlayInterstitialDelegate {
    
    /**
     Called after an interstitial has been loaded
     @param adInfo The info of the ad.
     */
    func didLoad(with adInfo: ISAdInfo) {
    }
    
    /**
     Called after an interstitial has attempted to load but failed.
     @param error The reason for the error
     */
    func didFailToLoadWithError(_ error: Error!) {
    }
    
    /**
     Called after an interstitial has been opened.
     @param adInfo The info of the ad.
     */
    func didOpen(with adInfo: ISAdInfo!) {
    }
    
    /**
     Called after an interstitial has been displayed on the screen.
     @param adInfo The info of the ad.
     */
    func didShow(with adInfo: ISAdInfo!) {
    }
    
    /**
     Called after an interstitial has attempted to show but failed.
     @param error The reason for the error
     @param adInfo The info of the ad.
     */
    func didFailToShowWithError(_ error: Error!, andAdInfo adInfo: ISAdInfo!) {
        <#code#>
    }
    
    /**
     Called after an interstitial has been clicked.
     @param adInfo The info of the ad.
     */
    func didClick(with adInfo: ISAdInfo!) {
    }
    
    /**
     Called after an interstitial has been dismissed.
     @param adInfo The info of the ad.
     */
    func didClose(with adInfo: ISAdInfo!) {
        <#code#>
    }
}
