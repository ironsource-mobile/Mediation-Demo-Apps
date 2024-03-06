//
//  DemoInterstitialAdDelegate.swift
//  IronSourceSwiftDemoApp
//
//  Copyright © 2024 ironSource Mobile Ltd. All rights reserved.
//

import Foundation
import IronSource

class DemoInterstitialAdDelegate: NSObject, LevelPlayInterstitialDelegate {
    
    weak var delegate: DemoViewControllerDelegate?

    init(delegate: DemoViewControllerDelegate!) {
        self.delegate = delegate
    }
    
    /**
     Called after an interstitial has been loaded
     @param adInfo The info of the ad.
     */
    func didLoad(with adInfo: ISAdInfo) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
        self.delegate?.setButtonEnablement(ButtonIdentifiers.showInterstitialButtonIdentifier, enable: true)
    }
    
    /**
     Called after an interstitial has attempted to load but failed.
     @param error The reason for the error
     */
    func didFailToLoadWithError(_ error: Error!) {
        logCallbackName(string: "\(#function) error = \(String(describing:error.self))")
        self.delegate?.setButtonEnablement(ButtonIdentifiers.showInterstitialButtonIdentifier, enable: false)
    }
    
    /**
     Called after an interstitial has been opened.
     This is the indication for impression.
     @param adInfo The info of the ad.
     */
    func didOpen(with adInfo: ISAdInfo!) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
        self.delegate?.setButtonEnablement(ButtonIdentifiers.showInterstitialButtonIdentifier, enable: false)
    }
    
    /**
     Called after an interstitial has been displayed on the screen.
     This callback is not supported by all networks, and we recommend using it
     only if it's supported by all networks you included in your build.
     @param adInfo The info of the ad.
     */
    func didShow(with adInfo: ISAdInfo!) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
    }
    
    /**
     Called after an interstitial has attempted to show but failed.
     @param error The reason for the error.
     @param adInfo The info of the ad.
     */
    func didFailToShowWithError(_ error: Error!, andAdInfo adInfo: ISAdInfo!) {
        logCallbackName(string: "\(#function) error = \(String(describing:error.self)) | adInfo =  \(String(describing:adInfo.self))")
    }
    
    /**
     Called after an interstitial has been clicked.
     @param adInfo The info of the ad.
     */
    func didClick(with adInfo: ISAdInfo!) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
    }
    
    /**
     Called after an interstitial has been dismissed.
     @param adInfo The info of the ad.
     */
    func didClose(with adInfo: ISAdInfo!) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
    }
    
    //MARK: Helper Method
    
    func logCallbackName(string: String = #function) {
        print("DemoInterstitialAdDelegate \(string)")
    }
}
