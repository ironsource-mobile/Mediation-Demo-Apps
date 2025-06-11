//
//  DemoImpressionDataDelegate.swift
//  LevelPlayDemo
//
//  Copyright © 2024 ironSource Mobile Ltd. All rights reserved.
//

import Foundation
import IronSource

class DemoImpressionDataDelegate: NSObject, LPMImpressionDataDelegate {
  
    /**
     Called when the ad was displayed successfully and the impression data was recorded
     @param impressionData The recorded impression data
     */
    func impressionDataDidSucceed(_ impressionData: LPMImpressionData!) {
        print("DemoImpressionDataDelegate \(#function)")
    }
}

