//
//  DemoInterstitialAdDelegate.swift
//  IronSourceSwiftDemoApp
//
//  Copyright © 2024 ironSource Mobile Ltd. All rights reserved.
//

import Foundation
import IronSource

class DemoInterstitialAdDelegate: NSObject, LPMInterstitialAdDelegate {
    
    weak var delegate: DemoViewControllerDelegate?

    init(delegate: DemoViewControllerDelegate!) {
        self.delegate = delegate
    }
    
    /**
     Called after an interstitial has been loaded
     @param adInfo The info of the ad.
     */
    func didLoadAd(with adInfo: LPMAdInfo) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
        self.delegate?.setButtonEnablement(ButtonIdentifiers.showInterstitialAdButtonIdentifier, enable: true)
    }
    
    /**
     Called after an interstitial has attempted to load but failed.
     @param error The reason for the error
     */
    func didFailToLoadAd(withAdUnitId adUnitId: String, error: Error) {
        logCallbackName(string: "\(#function) error = \(String(describing:error.self))")
        self.delegate?.setButtonEnablement(ButtonIdentifiers.showInterstitialAdButtonIdentifier, enable: false)
    }
    
    /**
     Called after an interstitial has attempted to load but failed.
     @param adUnitId The ad unit id of the ad.
     @param error The reason for the error
     */
    func didChangeAdInfo(_ adInfo: LPMAdInfo) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
    }
    
    /**
     Called after an interstitial has been opened.
     This is the indication for impression.
     @param adInfo The info of the ad.
     */
    func didDisplayAd(with adInfo: LPMAdInfo) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
        self.delegate?.setButtonEnablement(ButtonIdentifiers.showInterstitialAdButtonIdentifier, enable: false)
    }
    
    /**
     Called after an interstitial has attempted to show but failed.
     @param error The reason for the error.
     @param adInfo The info of the ad.
     */
    func didFailToDisplayAd(with adInfo: LPMAdInfo, error: Error) {
        logCallbackName(string: "\(#function) error = \(String(describing:error.self)) | adInfo =  \(String(describing:adInfo.self))")
    }
    
    /**
     Called after an interstitial has been clicked.
     @param adInfo The info of the ad.
     */
    func didClickAd(with adInfo: LPMAdInfo) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
    }
    
    /**
     Called after an interstitial has been dismissed.
     @param adInfo The info of the ad.
     */
    func didCloseAd(with adInfo: LPMAdInfo) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
    }

    //MARK: Helper Method
    
    func logCallbackName(string: String = #function) {
        print("DemoInterstitialAdDelegate \(string)")
    }
}
