//
//  RewardedVideoLevelPlayCallbacksHandler.swift
//  IronSourceSwiftDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.
//

import Foundation
import IronSource

protocol RewardedVideoLevelPlayCallbacksWrapper {
    func rewardedVideoLevelPlayHasAvailableAd(with adInfo: ISAdInfo!)
    func rewardedVideoLevelPlayHasNoAvailableAd()
    func rewardedVideoLevelPlayDidOpen(with adInfo: ISAdInfo!)
    func rewardedVideoLevelPlayDidFailToShowWithError(_ error: Error!, andAdInfo adInfo: ISAdInfo!)
    func rewardedVideoLevelPlayDidClick(_ placementInfo: ISPlacementInfo!, with adInfo: ISAdInfo!)
    func rewardedVideoLevelPlayDidReceiveRewardForPlacement(_ placementInfo: ISPlacementInfo!, with adInfo: ISAdInfo!)
    func rewardedVideoLevelPlayDidClose(with adInfo: ISAdInfo!)
}

class RewardedVideoLevelPlayCallbacksHandler: NSObject, LevelPlayRewardedVideoDelegate {
    
    var delegate: RewardedVideoLevelPlayCallbacksWrapper!

    init(delegate: RewardedVideoLevelPlayCallbacksWrapper!) {
        self.delegate = delegate
    }
    
    /**
     Called after a rewarded video has changed its availability to true.
     @param adInfo The info of the ad.
     */
    func hasAvailableAd(with adInfo: ISAdInfo!) {
        delegate.rewardedVideoLevelPlayHasAvailableAd(with: adInfo)
    }
    
    /**
     Called after a rewarded video has changed its availability to false.
     */
    func hasNoAvailableAd() {
        delegate.rewardedVideoLevelPlayHasNoAvailableAd()
    }
    
    /**
     Called after a rewarded video has been opened.
     @param adInfo The info of the ad.
     */
    func didOpen(with adInfo: ISAdInfo!) {
        delegate.rewardedVideoLevelPlayDidOpen(with: adInfo)
    }
    
    /**
     Called after a rewarded video has attempted to show but failed.
     @param error The reason for the error
     @param adInfo The info of the ad.
     */
    func didFailToShowWithError(_ error: Error!, andAdInfo adInfo: ISAdInfo!) {
        delegate.rewardedVideoLevelPlayDidFailToShowWithError(error, andAdInfo: adInfo)
    }
    
    /**
     Called after a rewarded video has been clicked.
     @param placementInfo An object that contains the placement's reward name and amount.
     @param adInfo The info of the ad.
     */
    func didClick(_ placementInfo: ISPlacementInfo!, with adInfo: ISAdInfo!) {
        delegate.rewardedVideoLevelPlayDidClick(placementInfo, with: adInfo)
    }
    
    /**
     Called after a rewarded video has been viewed completely and the user is eligible for reward.
     @param placementInfo An object that contains the placement's reward name and amount.
     @param adInfo The info of the ad.
     */
    func didReceiveReward(forPlacement placementInfo: ISPlacementInfo!, with adInfo: ISAdInfo!) {
        delegate.rewardedVideoLevelPlayDidReceiveRewardForPlacement(placementInfo, with: adInfo)
    }
    
    /**
     Called after a rewarded video has been dismissed
     @param adInfo The info of the ad.
     */
    func didClose(with adInfo: ISAdInfo!) {
        delegate.rewardedVideoLevelPlayDidClose(with: adInfo)
    }
}
