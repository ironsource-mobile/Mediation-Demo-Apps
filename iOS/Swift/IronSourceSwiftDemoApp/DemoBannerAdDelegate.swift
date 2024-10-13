//
//  DemoBannerAdDelegate.swift
//  IronSourceSwiftDemoApp
//
//  Copyright © 2024 ironSource Mobile Ltd. All rights reserved.
//

import Foundation
import IronSource

class DemoBannerAdDelegate: NSObject, LPMBannerAdViewDelegate {
    
    weak var delegate: DemoViewControllerDelegate?

    init(delegate: DemoViewControllerDelegate!) {
        self.delegate = delegate
    }
    
    /**
     Called after each banner ad has been successfully loaded, either a manual load or banner refresh
     @param adInfo The info of the ad.
     */
    func didLoadAd(with adInfo: LPMAdInfo) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
        self.delegate?.setButtonEnablement(ButtonIdentifiers.loadBannerButtonIdentifier, enable: false)
    }
    
    /**
     Called after a banner has attempted to load an ad but failed.
     This delegate will be sent both for manual load and refreshed banner failures.
     @param error The reason for the error.
     */
    func didFailToLoadAd(withAdUnitId adUnitId: String, error: Error) {
        logCallbackName(string: "\(#function) error = \(String(describing:error.self))")
    }
    
    /**
     Called after a banner has been clicked.
     @param adInfo The info of the ad.
     */
    func didClickAd(with adInfo: LPMAdInfo) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
    }
    
    /**
     Called after a banner was displayed and visible on screen.
     @param adInfo The info of the ad.
     */
    func didDisplayAd(with adInfo: LPMAdInfo) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
    }
    
    /**
     Called after a banner failed to be displayed on screen.
     @param adInfo The info of the ad.
     @param error The reason for the error.
     */
    func didFailToDisplayAd(with adInfo: LPMAdInfo, error: Error) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
    }
    
    /**
     Called when a user was taken out of the application context.
     @param adInfo The info of the ad.
     */
    func didLeaveApp(with adInfo: LPMAdInfo) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
    }

    /**
     Called when a banner presented a full screen content.
     @param adInfo The info of the ad.
     */
    func didExpandAd(with adInfo: LPMAdInfo) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
    }
    
    /**
     Called after a full screen content has been dismissed.
     @param adInfo The info of the ad.
     */
    func didCollapseAd(with adInfo: LPMAdInfo) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
    }
    
    //MARK: Helper Method
    
    func logCallbackName(string: String = #function) {
        print("DemoBannerAdDelegate \(string)")
    }
}

