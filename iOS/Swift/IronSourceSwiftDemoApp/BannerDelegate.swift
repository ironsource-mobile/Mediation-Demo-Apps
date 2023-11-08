//
//  BannerDelegate.swift
//  IronSourceSwiftDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.
//

import Foundation
import IronSource

class BannerDelegate: NSObject, LevelPlayBannerDelegate {
    
    var demoViewController: DemoViewController!

    init(demoViewController: DemoViewController!) {
        self.demoViewController = demoViewController
    }
    
    /**
     Called after a banner ad has been successfully loaded
     @param adInfo The info of the ad.
     */
    func didLoad(_ bannerView: ISBannerView!, with adInfo: ISAdInfo!) {
        logFunctionName()
        
        self.demoViewController.setAndBindBannerView(bannerView)
        self.demoViewController.setButtonEnablement(self.demoViewController.loadBannerButton, enabled: false)
        self.demoViewController.setButtonEnablement(self.demoViewController.destroyBannerButton, enabled: true)
    }
    
    /**
     Called after a banner has attempted to load an ad but failed.
     @param error The reason for the error
     */
    func didFailToLoadWithError(_ error: Error!) {
        logFunctionName(string: #function+String(describing: error.self))
    }
    
    /**
     Called after a banner has been clicked.
     @param adInfo The info of the ad.
     */
    func didClick(with adInfo: ISAdInfo!) {
        logFunctionName()
    }
    
    /**
     Called when a user was taken out of the application context.
     @param adInfo The info of the ad.
     */
    func didLeaveApplication(with adInfo: ISAdInfo!) {
        logFunctionName()
    }
    
    /**
     Called when a banner presented a full screen content.
     @param adInfo The info of the ad.
     */
    func didPresentScreen(with adInfo: ISAdInfo!) {
        logFunctionName()
    }
    
    /**
     Called after a full screen content has been dismissed.
     @param adInfo The info of the ad.
     */
    func didDismissScreen(with adInfo: ISAdInfo!) {
        logFunctionName()
    }
    
    //MARK: Helper Method
    
    func logFunctionName(string: String = #function) {
        print("BannerDelegate: " + string)
    }
}

