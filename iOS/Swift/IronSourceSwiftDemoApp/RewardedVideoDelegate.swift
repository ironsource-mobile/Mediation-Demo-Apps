//
//  RewardedVideoDelegate.swift
//  IronSourceSwiftDemoApp
//
//  Copyright © 2024 ironSource Mobile Ltd. All rights reserved.
//

import Foundation
import IronSource

class RewardedVideoDelegate: NSObject, LevelPlayRewardedVideoDelegate {
    
    weak var delegate: DemoViewControllerDelegate?

    init(delegate: DemoViewControllerDelegate!) {
        self.delegate = delegate
    }
    
    /**
     Called after a rewarded video has changed its availability to true.
     @param adInfo The info of the ad.
     */
    func hasAvailableAd(with adInfo: ISAdInfo!) {
        logCallbackName()
        
        self.delegate?.setButtonEnablement(ButtonIdentifiers.showRewardedVideoButtonIdentifier, enable: true)
    }
    
    /**
     Called after a rewarded video has changed its availability to false.
     */
    func hasNoAvailableAd() {
        logCallbackName()
        
        self.delegate?.setButtonEnablement(ButtonIdentifiers.showRewardedVideoButtonIdentifier, enable: false)
    }
    
    /**
     Called after a rewarded video has been opened.
     @param adInfo The info of the ad.
     */
    func didOpen(with adInfo: ISAdInfo!) {
        logCallbackName()
        
        self.delegate?.setButtonEnablement(ButtonIdentifiers.showRewardedVideoButtonIdentifier, enable: true)
    }
    
    /**
     Called after a rewarded video has attempted to show but failed.
     @param error The reason for the error.
     @param adInfo The info of the ad.
     */
    func didFailToShowWithError(_ error: Error!, andAdInfo adInfo: ISAdInfo!) {
        logCallbackName(string: #function + String(describing: error.self))
    }
    
    /**
     Called after a rewarded video has been clicked.
     This callback is not supported by all networks, and we recommend using it
     only if it's supported by all networks you included in your build
     @param placementInfo An object that contains the placement's reward name and amount.
     @param adInfo The info of the ad.
     */ 
    func didClick(_ placementInfo: ISPlacementInfo!, with adInfo: ISAdInfo!) {
        logCallbackName(string: #function + String(describing: placementInfo.self))
    }
    
    /**
     Called after a rewarded video has been viewed completely and the user is eligible for a reward.
     @param placementInfo An object that contains the placement's reward name and amount.
     @param adInfo The info of the ad.
     */
    func didReceiveReward(forPlacement placementInfo: ISPlacementInfo!, with adInfo: ISAdInfo!) {
        logCallbackName(string: #function + String(describing: placementInfo.self))
        
        self.delegate?.setPlacementInfo(placementInfo)
    }
    
    /**
     Called after a rewarded video has been dismissed.
     @param adInfo The info of the ad.
     */
    func didClose(with adInfo: ISAdInfo!) {
        logCallbackName()
        
        self.delegate?.showVideoRewardMessage()
    }
    
    //MARK: Helper Method
    
    func logCallbackName(string: String = #function) {
        print("RewardedVideoDelegate " + string)
    }
}
