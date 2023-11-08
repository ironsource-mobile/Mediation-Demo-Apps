//
//  ImpressionDataDelegate.swift
//  IronSourceSwiftDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.
//

import Foundation
import IronSource

class ImpressionDataDelegate: NSObject, ISImpressionDataDelegate {
    
    func impressionDataDidSucceed(_ impressionData: ISImpressionData!) {
        print("ImpressionDataDelegate: impressionDataDidSucceed with impressionData " + String(describing: impressionData.self))
    }
}

