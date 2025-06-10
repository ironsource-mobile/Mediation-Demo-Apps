//
//  DemoRewardedVideoAdDelegate.swift
//  IronSourceSwiftDemoApp
//
//  Copyright © 2024 ironSource Mobile Ltd. All rights reserved.
//

import Foundation
import IronSource

class DemoRewardedVideoAdDelegate: NSObject, LPMRewardedAdDelegate {
    
    weak var delegate: DemoViewControllerDelegate?

    init(delegate: DemoViewControllerDelegate!) {
        self.delegate = delegate
    }
    
    /**
     Called after an rewarded has been loaded
     @param adInfo The info of the ad.
     */
    func didLoadAd(with adInfo: LPMAdInfo) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
        self.delegate?.setButtonEnablement(ButtonIdentifiers.showRewardedVideoButtonIdentifier, enable: true)
    }
    
    /**
     Called after an rewarded has attempted to load but failed.
     @param error The reason for the error
     */
    func didFailToLoadAd(withAdUnitId adUnitId: String, error: Error) {
        logCallbackName(string: "\(#function) error = \(String(describing:error.self))")
        self.delegate?.setButtonEnablement(ButtonIdentifiers.showRewardedVideoButtonIdentifier, enable: false)
    }
    
    /**
     Called after an rewarded has attempted to load but failed.
     @param adUnitId The ad unit id of the ad.
     @param error The reason for the error
     */
    func didChangeAdInfo(_ adInfo: LPMAdInfo) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
    }
    
    /**
     Called after an rewarded has been opened.
     This is the indication for impression.
     @param adInfo The info of the ad.
     */
    func didDisplayAd(with adInfo: LPMAdInfo) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
        self.delegate?.setButtonEnablement(ButtonIdentifiers.showRewardedVideoButtonIdentifier, enable: false)
    }
    
    /**
     Called after an rewarded has attempted to show but failed.
     @param error The reason for the error.
     @param adInfo The info of the ad.
     */
    func didFailToDisplayAd(with adInfo: LPMAdInfo, error: Error) {
        logCallbackName(string: "\(#function) error = \(String(describing:error.self)) | adInfo =  \(String(describing:adInfo.self))")
    }
    
    /**
     Called after an rewarded has been clicked.
     @param adInfo The info of the ad.
     */
    func didClickAd(with adInfo: LPMAdInfo) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
    }
    
    
    func didRewardAd(with adInfo: LPMAdInfo, reward: LPMReward) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
        self.delegate?.setReward(reward);
    }

    /**
     Called after an rewarded has been dismissed.
     @param adInfo The info of the ad.
     */
    func didCloseAd(with adInfo: LPMAdInfo) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
        self.delegate?.showVideoRewardMessage()
    }

    //MARK: Helper Method
    
    func logCallbackName(string: String = #function) {
        print("DemoRewardedVideoAdDelegate \(string)")
    }
}
