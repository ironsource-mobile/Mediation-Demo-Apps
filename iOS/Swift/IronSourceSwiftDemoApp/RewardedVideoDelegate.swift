//
//  RewardedVideoDelegate.swift
//  IronSourceSwiftDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.
//

import Foundation
import IronSource

class RewardedVideoDelegate: NSObject, LevelPlayRewardedVideoDelegate {
    
    var demoViewController: DemoViewController!

    init(demoViewController: DemoViewController!) {
        self.demoViewController = demoViewController
    }
    
    /**
     Called after a rewarded video has changed its availability to true.
     @param adInfo The info of the ad.
     */
    func hasAvailableAd(with adInfo: ISAdInfo!) {
        logFunctionName()
        
        self.demoViewController.setButtonEnablement(self.demoViewController.showRewardedVideoButton, enabled: true)
    }
    
    /**
     Called after a rewarded video has changed its availability to false.
     */
    func hasNoAvailableAd() {
        logFunctionName()
        
        self.demoViewController.setButtonEnablement(self.demoViewController.showRewardedVideoButton, enabled: false)
    }
    
    /**
     Called after a rewarded video has been opened.
     @param adInfo The info of the ad.
     */
    func didOpen(with adInfo: ISAdInfo!) {
        logFunctionName()
        
        self.demoViewController.setButtonEnablement(self.demoViewController.showRewardedVideoButton, enabled: false)
    }
    
    /**
     Called after a rewarded video has attempted to show but failed.
     @param error The reason for the error
     @param adInfo The info of the ad.
     */
    func didFailToShowWithError(_ error: Error!, andAdInfo adInfo: ISAdInfo!) {
        logFunctionName(string: #function+String(describing: error.self))
    }
    
    /**
     Called after a rewarded video has been clicked.
     @param placementInfo An object that contains the placement's reward name and amount.
     @param adInfo The info of the ad.
     */
    func didClick(_ placementInfo: ISPlacementInfo!, with adInfo: ISAdInfo!) {
        logFunctionName(string: #function+String(describing: placementInfo.self))
    }
    
    /**
     Called after a rewarded video has been viewed completely and the user is eligible for reward.
     @param placementInfo An object that contains the placement's reward name and amount.
     @param adInfo The info of the ad.
     */
    func didReceiveReward(forPlacement placementInfo: ISPlacementInfo!, with adInfo: ISAdInfo!) {
        logFunctionName(string: #function+String(describing: placementInfo.self))
    }
    
    /**
     Called after a rewarded video has been dismissed
     @param adInfo The info of the ad.
     */
    func didClose(with adInfo: ISAdInfo!) {
        logFunctionName()
    }
    
    //MARK: Helper Method
    
    func logFunctionName(string: String = #function) {
        print("RewardedVideoDelegate: " + string)
    }
}
