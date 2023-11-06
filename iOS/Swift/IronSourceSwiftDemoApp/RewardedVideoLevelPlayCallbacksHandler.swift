//
//  RewardedVideoLevelPlayCallbacksHandler.swift
//  IronSourceSwiftDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.
//

import Foundation
import IronSource

class RewardedVideoLevelPlayCallbacksHandler: NSObject, LevelPlayRewardedVideoDelegate {
    
    /**
     Called after a rewarded video has changed its availability to true.
     @param adInfo The info of the ad.
     */
    func hasAvailableAd(with adInfo: ISAdInfo!) {
    }
    
    /**
     Called after a rewarded video has changed its availability to false.
     */
    func hasNoAvailableAd() {
    }
    
    /**
     Called after an interstitial has been opened.
     @param adInfo The info of the ad.
     */
    func didOpen(with adInfo: ISAdInfo!) {
    }
    
    /**
     Called after an interstitial has been displayed on the screen.
     @param adInfo The info of the ad.
     */
    func didShow(with adInfo: ISAdInfo!) {
    }
    
    /**
     Called after an interstitial has attempted to show but failed.
     @param error The reason for the error
     @param adInfo The info of the ad.
     */
    func didFailToShowWithError(_ error: Error!, andAdInfo adInfo: ISAdInfo!) {
    }
    
    /**
     Called after a rewarded video has been clicked.
     @param placementInfo An object that contains the placement's reward name and amount.
     @param adInfo The info of the ad.
     */
    func didClick(_ placementInfo: ISPlacementInfo!, with adInfo: ISAdInfo!) {
    }
    
    /**
     Called after a rewarded video has been viewed completely and the user is eligible for reward.
     @param placementInfo An object that contains the placement's reward name and amount.
     @param adInfo The info of the ad.
     */
    func didReceiveReward(forPlacement placementInfo: ISPlacementInfo!, with adInfo: ISAdInfo!) {
    }
    
    /**
     Called after an interstitial has been dismissed.
     @param adInfo The info of the ad.
     */
    func didClose(with adInfo: ISAdInfo!) {
    }
}
