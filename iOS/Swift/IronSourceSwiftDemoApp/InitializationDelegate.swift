//
//  InitializationDelegate.swift
//  IronSourceSwiftDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.
//

import Foundation
import IronSource

class InitializationDelegate: NSObject, ISInitializationDelegate {
    
    var demoViewController: DemoViewController!

    init(demoViewController: DemoViewController!) {
        self.demoViewController = demoViewController
    }
    
    func initializationDidComplete() {
        print("InitializationDelegate: initializationDidComplete")
        
        self.demoViewController.setButtonEnablement(self.demoViewController.loadInterstitialButton, enabled: true)
        self.demoViewController.setButtonEnablement(self.demoViewController.loadBannerButton, enabled: true)
        
    }
    
}

