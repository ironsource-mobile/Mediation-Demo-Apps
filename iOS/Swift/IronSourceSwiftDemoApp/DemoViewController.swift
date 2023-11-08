//
//  DemoViewController.swift
//  IronSourceSwiftDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.
//

import UIKit
import Foundation
import ObjectiveC.runtime
import IronSource

let defaultUserId = "demoapp"
let appKey = "8545d445"

class DemoViewController: UIViewController {
    
    //MARK: IBOutlets
    
    @IBOutlet weak var showRewardedVideoButton: UIButton!
    
    @IBOutlet weak var loadInterstitialButton: UIButton!
    @IBOutlet weak var showInterstitialButton: UIButton!
    
    @IBOutlet weak var loadBannerButton: UIButton!
    @IBOutlet weak var destroyBannerButton: UIButton!
    
    @IBOutlet weak var versionLabel: UILabel!
    
    var rewardedVideoDelegate: RewardedVideoDelegate! = nil
    var interstitialDelegate: InterstitialDelegate! = nil
    var bannerDelegate: BannerDelegate! = nil
    
    var initializationDelegate: InitializationDelegate! = nil
    var impressionDataDelegate: ImpressionDataDelegate! = nil
    
    var bannerView: ISBannerView! = nil
    
    //MARK: ViewLifecycle Functions
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        // Do any additional setup after loading the view, typically from a nib.
        self.versionLabel.text =  String(format: "%@%@", "sdk version: ", IronSource.sdkVersion());
        self.setupIronSourceSdk()
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    //MARK: Private Functions
    
    func setupIronSourceSdk() {
        // The integrationHelper is used to validate the integration.
        // Remove the integrationHelper before going live!
        ISIntegrationHelper.validateIntegration()
        
        // Before initializing any of our products (Rewarded video, Interstitial or Banner) you must set
        // their delegates. Take a look at each of these delegates method and you will see that they each implement a product
        // protocol. This is our way of letting you know what's going on, and if you don't set the delegates
        // we will not be able to communicate with you.
        // We're passing 'self' to our delegates because we want
        // to be able to enable/disable buttons to match ad availability.
        
        
        rewardedVideoDelegate = .init(demoViewController: self)
        IronSource.setLevelPlayRewardedVideoDelegate(rewardedVideoDelegate)
        
        interstitialDelegate = .init(demoViewController: self)
        IronSource.setLevelPlayInterstitialDelegate(interstitialDelegate)
        
        bannerDelegate = .init(demoViewController: self)
        IronSource.setLevelPlayBannerDelegate(bannerDelegate)
        
        impressionDataDelegate = .init()
        IronSource.add(self.impressionDataDelegate)
        
        var userId = IronSource.advertiserId()
        
        if userId.count == 0 {
            //If we couldn't get the advertiser id, we will use a default one.
            userId = defaultUserId;
        }
        
        IronSource.setUserId(userId)
        
        // After setting the delegates you can go ahead and initialize the SDK. Once the iniitaliztion callback is return you can start loading your ads
        initializationDelegate = .init(demoViewController: self)
        
        logFunctionName(string: "initWithAppKey(" + String(describing: appKey) + ")")
        IronSource.initWithAppKey(appKey, delegate: self.initializationDelegate)
        
        // To initialize specific ad units:
        // IronSource.initWithAppKey(appKey, adUnits: [IS_REWARDED_VIDEO, IS_INTERSTITIAL, IS_BANNER], delegate: self.initializationDelegate)
        
        // Scroll down the file to find out what happens when you tap a button...
    }
    
    func logFunctionName(string: String = #function) {
        print("DemoViewController: " + string)
    }
    
    
    func destroyBanner() {
        if bannerView != nil {
            logFunctionName()
            IronSource.destroyBanner(bannerView)
            self.bannerView = nil
        }
        
        setButtonEnablement(self.loadBannerButton, enabled: true)
        setButtonEnablement(self.destroyBannerButton, enabled: false)
    }
    
    //MARK: IBOutlets Actions
    
    @IBAction func showRewardedVideoButtonAction(_ sender: Any) {
        // After calling 'setLevelPlayRewardedVideoDelegate' and 'initWithAppKey'
        // you are ready to present an ad. You can supply a placement
        // by calling 'showRewardedVideo(with: viewController, placement: placement)', or you can simply
        // call 'showRewardedVideo(with:viewController)'.
        // In this case the SDK will use the default placement one created for you.
        logFunctionName(string: "showRewardedVideo()")
        IronSource.showRewardedVideo(with: self)
    }
    
    @IBAction func loadInterstitialButtonAction(_ sender: Any) {
        // This will load the Interstitial.
        
        logFunctionName(string: "loadInterstitial()")
        IronSource.loadInterstitial()
    }

    @IBAction func showInterstitialButtonAction(_ sender: Any) {
        // This will present the Interstitial. Unlike Rewarded
        // Videos there are no placements.
        
        logFunctionName(string: "showInterstitial()")
        IronSource.showInterstitial(with: self)
    }
    
    @IBAction func loadBannerButtonAction(_ sender: Any) {
        // We call destroy banner before loading a new banner
        if bannerView != nil {
            destroyBanner()
        }
        
        let bannerSize: ISBannerSize = ISBannerSize(description:kSizeBanner, width:320, height:50)
        
        // This will load the Banner. You can supply a placement
        // by calling 'loadBanner(with: viewControler, size: bannerSize, placement: placement)', or you can simply
        // call 'loadBanner(with: self, size: bannerSize)'.
        // In this case the SDK will use the default placement one created for you.
        
        logFunctionName(string: "loadBanner()")
        IronSource.loadBanner(with: self, size: bannerSize)
    }
    
    @IBAction func destroyBannerButtonAction(_ sender: Any) {
        destroyBanner()
    }

    //MARK: Helper Methods
    
    func setAndBindBannerView(_ bannerView: ISBannerView!) {
        
        DispatchQueue.main.async {
            self.bannerView = bannerView
            
            let xPosition = (self.view.frame.size.width - bannerView.frame.size.width) / 2.0
            let yPosition = self.view.frame.size.height - bannerView.frame.size.height
            let bannerWidth = bannerView.frame.size.width
            var bannerHeight = 0.0
            
            if #available(iOS 11.0, *) {
                bannerHeight = bannerView.frame.size.height - self.view.safeAreaInsets.bottom * 2.5
            } else {
                bannerHeight = bannerView.frame.size.height * 2.5
            }
            
            bannerView.frame = CGRect(x: xPosition, y: yPosition, width: bannerWidth, height: bannerHeight)
            self.view.addSubview(bannerView)
        }
    }
    
    
    func setButtonEnablement(_ button: UIButton!, enabled: Bool) {
        DispatchQueue.main.async {
            button.isEnabled = enabled
        }
    }
}
