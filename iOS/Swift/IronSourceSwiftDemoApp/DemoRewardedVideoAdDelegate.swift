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
     Called after a rewarded ad has been loaded
     @param adInfo The info of the ad.
     */
    func didLoadAd(with adInfo: LPMAdInfo) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
        self.delegate?.setButtonEnablement(ButtonIdentifiers.showRewardedAdButtonIdentifier, enable: true)
    }
    
    /**
     Called after a rewarded ad  has attempted to load but failed.
     @param error The reason for the error
     */
    func didFailToLoadAd(withAdUnitId adUnitId: String, error: Error) {
        logCallbackName(string: "\(#function) error = \(String(describing:error.self))")
        self.delegate?.setButtonEnablement(ButtonIdentifiers.showRewardedAdButtonIdentifier, enable: false)
    }
    
    /**
     Called after the ad info is updated. Available when another rewarded ad has loaded, and includes a higher CPM/Rate
     @param adInfo The info of the ad
    */
    func didChangeAdInfo(_ adInfo: LPMAdInfo) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
    }
    
    /**
     Called after a rewarded ad has been opened.
     This is the indication for impression.
     @param adInfo The info of the ad.
     */
    func didDisplayAd(with adInfo: LPMAdInfo) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
        self.delegate?.setButtonEnablement(ButtonIdentifiers.showRewardedAdButtonIdentifier, enable: false)
    }
    
    /**
     Called after a rewarded ad has attempted to show but failed.
     @param error The reason for the error.
     @param adInfo The info of the ad.
     */
    func didFailToDisplayAd(with adInfo: LPMAdInfo, error: Error) {
        logCallbackName(string: "\(#function) error = \(String(describing:error.self)) | adInfo =  \(String(describing:adInfo.self))")
    }
    
    /**
     Called after a rewarded ad has been viewed completely and the user is eligible for a reward.
     @param reward An object that contains the reward name and amount.
     @param adInfo The info of the ad.
     */
    func didRewardAd(with adInfo: LPMAdInfo, reward: LPMReward) {
        logCallbackName(string: "\(#function) reward = \(String(describing:reward.self)) | adInfo = \(String(describing:adInfo.self))")
        self.delegate?.setRewardInfo(reward)
    }
    
    /**
     Called after a rewarded ad has been clicked.
     @param adInfo The info of the ad.
     */
    func didClickAd(with adInfo: LPMAdInfo) {
        logCallbackName(string: "\(#function) adInfo = \(String(describing:adInfo.self))")
    }
    
    /**
     Called after a rewarded ad has been dismissed.
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
