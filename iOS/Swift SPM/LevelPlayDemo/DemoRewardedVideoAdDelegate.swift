//
//  DemoRewardedVideoAdDelegate.swift
//  LevelPlayDemo
//
//  Copyright © 2021-2025 Unity Technologies. All rights reserved.
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
     @param adUnitId The ad unit id of the ad.
     @param error The reason for the error
     */
    func didFailToLoadAd(withAdUnitId adUnitId: String, error: Error) {
        logCallbackName(string: "\(#function) error = \(String(describing:error.self))")
        self.delegate?.setButtonEnablement(ButtonIdentifiers.showRewardedVideoButtonIdentifier, enable: false)
    }
    
    /**
     Called after the ad info is updated. Available when another rewarded ad has loaded, and includes a higher CPM/Rate
     @param adInfo The info of the ad.
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
     @param adInfo The info of the ad.
     @param error The reason for the error.
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
    
    /**
     Called after a rewarded ad has been viewed completely and the user is eligible for a reward.
     @param adInfo The info of the ad.
     @param reward An object that contains the placement's reward name and amount.
     */
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
