//
//  DemoBannerAdDelegate.swift
//  IronSourceSwiftDemoApp
//
//  Copyright © 2024 ironSource Mobile Ltd. All rights reserved.
//

import Foundation
import IronSource

class DemoBannerAdDelegate: NSObject, LevelPlayBannerDelegate {
    
    weak var delegate: DemoViewControllerDelegate?

    init(delegate: DemoViewControllerDelegate!) {
        self.delegate = delegate
    }
    
    /**
     Called after each banner ad has been successfully loaded, either a manual load or banner refresh
     @param adInfo The info of the ad.
     */
    func didLoad(_ bannerView: ISBannerView!, with adInfo: ISAdInfo!) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")

        self.delegate?.setAndBindBannerView(bannerView)
        self.delegate?.setButtonEnablement(ButtonIdentifiers.loadBannerButtonIdentifier, enable: false)
        self.delegate?.setButtonEnablement(ButtonIdentifiers.destroyBannerButtonIdentifier, enable: true)
    }
    
    /**
     Called after a banner has attempted to load an ad but failed.
     This delegate will be sent both for manual load and refreshed banner failures.
     @param error The reason for the error.
     */
    func didFailToLoadWithError(_ error: Error!) {
        logCallbackName(string: "\(#function) error = \(String(describing:error.self))")
    }
    
    /**
     Called after a banner has been clicked.
     @param adInfo The info of the ad.
     */
    func didClick(with adInfo: ISAdInfo!) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
    }
    
    /**
     Called when a user was taken out of the application context.
     @param adInfo The info of the ad.
     */
    func didLeaveApplication(with adInfo: ISAdInfo!) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
    }
    
    /**
     Called when a banner presented a full screen content.
     @param adInfo The info of the ad.
     */
    func didPresentScreen(with adInfo: ISAdInfo!) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
    }
    
    /**
     Called after a full screen content has been dismissed.
     @param adInfo The info of the ad.
     */
    func didDismissScreen(with adInfo: ISAdInfo!) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
    }
    
    //MARK: Helper Method
    
    func logCallbackName(string: String = #function) {
        print("DemoBannerAdDelegate \(string)")
    }
}

