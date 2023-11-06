//
//  ViewController.swift
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

class ViewController: UIViewController, RewardedVideoLevelPlayCallbacksWrapper, InterstitialLevelPlayCallbacksWrapper, BannerLevelPlayCallbacksWrapper, ISImpressionDataDelegate {
    
    //MARK: IBOutlets
    
    @IBOutlet weak var showRewardedVideoButton: UIButton!
    
    @IBOutlet weak var loadInterstitialButton: UIButton!
    @IBOutlet weak var showInterstitialButton: UIButton!
    
    @IBOutlet weak var loadBannerButton: UIButton!
    @IBOutlet weak var destroyBannerButton: UIButton!
    
    @IBOutlet weak var versionLabel: UILabel!
    
    var rewardedVideoDelegate: RewardedVideoLevelPlayCallbacksHandler! = nil
    var interstitialDelegate: InterstitialLevelPlayCallbacksHandler! = nil
    var bannerDelegate: BannerLevelPlayCallbacksHandler! = nil
    
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
        //The integrationHelper is used to validate the integration. Remove the integrationHelper before going live!
        ISIntegrationHelper.validateIntegration()
        
        // Before initializing any of our products (Rewarded video, Interstitial or Banner) you must set
        // their delegates. Take a look at each of these delegates method and you will see that they each implement a product
        // protocol. This is our way of letting you know what's going on, and if you don't set the delegates
        // we will not be able to communicate with you.
        // We're passing 'self' to our delegates because we want
        // to be able to enable/disable buttons to match ad availability.
        
        rewardedVideoDelegate = .init(delegate: self)
        interstitialDelegate = .init(delegate: self)
        bannerDelegate = .init(delegate: self)
        
        IronSource.setLevelPlayRewardedVideoDelegate(rewardedVideoDelegate)
        IronSource.setLevelPlayInterstitialDelegate(interstitialDelegate)
        IronSource.setLevelPlayBannerDelegate(bannerDelegate)
        IronSource.add(self)
        
        var userId = IronSource.advertiserId()
        
        if userId.count == 0 {
            //If we couldn't get the advertiser id, we will use a default one.
            userId = defaultUserId;
        }
        
        IronSource.setUserId(userId)
        
        // After setting the delegates you can go ahead and initialize the SDK.
        IronSource.initWithAppKey(appKey)
        
        // To initialize specific ad units:
        // IronSource.initWithAppKey(appKey, adUnits:[IS_REWARDED_VIDEO, IS_INTERSTITIAL, IS_BANNER])
        
        // Scroll down the file to find out what happens when you click a button...
    }
    
    func logFunctionName(string: String = #function) {
        print("IronSource Swift Demo App: " + string)
    }
    
    
    func destroyBanner() {
        if bannerView != nil {
            IronSource.destroyBanner(bannerView)
        }
        
        destroyBannerButton.isEnabled = false
    }
    
    //MARK: IBOutlets Actions
    
    @IBAction func showRewardedVideoButtonAction(_ sender: Any) {
        // After calling 'setLevelPlayRewardedVideoDelegate' and 'initWithAppKey'
        // you are ready to present an ad. You can supply a placement
        // by calling 'showRewardedVideo(with: viewController, placement: placement)', or you can simply
        // call 'showRewardedVideo(with:viewController)'.
        // In this case the SDK will use the default placement one created for you.
        IronSource.showRewardedVideo(with: self)
    }
    
    @IBAction func loadInterstitialButtonAction(_ sender: Any) {
        // This will load the Interstitial.
        IronSource.loadInterstitial()
    }

    @IBAction func showInterstitialButtonAction(_ sender: Any) {
        // This will present the Interstitial. Unlike Rewarded
        // Videos there are no placements.
        IronSource.showInterstitial(with: self)
    }
    
    @IBAction func loadBannerButtonAction(_ sender: Any) {
        // We call destroy banner before loading a new banner
        if bannerView != nil {
            destroyBanner()
        }
        
        let bannerSize: ISBannerSize = ISBannerSize(description:"BANNER", width:320, height:50)
        
        // This will load the Banner. You can supply a placement
        // by calling 'loadBanner(with: viewControler, size: bannerSize, placement: placement)', or you can simply
        // call 'loadBanner(with: self, size: bannerSize)'.
        // In this case the SDK will use the default placement one created for you.
        IronSource.loadBanner(with: self, size: bannerSize)
    }
    
    @IBAction func destroyBannerButtonAction(_ sender: Any) {
        destroyBanner()
    }
    
    //MARK: Rewarded Video LevelPlay Callbacks Wrapper Functions
    
    func rewardedVideoLevelPlayHasAvailableAd(with adInfo: ISAdInfo!) {
        showRewardedVideoButton.isEnabled = true
        logFunctionName()
    }
    
    func rewardedVideoLevelPlayHasNoAvailableAd() {
        showRewardedVideoButton.isEnabled = false
        logFunctionName()
    }
    
    func rewardedVideoLevelPlayDidOpen(with adInfo: ISAdInfo!) {
        showRewardedVideoButton.isEnabled = false
        logFunctionName()
    }
    
    func rewardedVideoLevelPlayDidFailToShowWithError(_ error: Error!, andAdInfo adInfo: ISAdInfo!) {
        logFunctionName(string: #function+String(describing: error.self))
    }
    
    func rewardedVideoLevelPlayDidClick(_ placementInfo: ISPlacementInfo!, with adInfo: ISAdInfo!) {
        logFunctionName(string: #function+String(describing: placementInfo.self))
    }
    
    func rewardedVideoLevelPlayDidReceiveRewardForPlacement(_ placementInfo: ISPlacementInfo!, with adInfo: ISAdInfo!) {
        logFunctionName(string: #function+String(describing: placementInfo.self))
    }
    
    func rewardedVideoLevelPlayDidClose(with adInfo: ISAdInfo!) {
        logFunctionName()
    }
    
    
    //MARK: Interstitial LevelPlay Callbacks Wrapper Functions
    
    func interstitialLevelPlayDidLoad(with adInfo: ISAdInfo!) {
        showInterstitialButton.isEnabled = true
        logFunctionName()
    }
    
    func interstitialLevelPlayDidFailToLoadWithError(_ error: Error!) {
        showInterstitialButton.isEnabled = false
        logFunctionName(string: String(describing: error.self))
    }
    
    func interstitialLevelPlayDidOpen(with adInfo: ISAdInfo!) {
        showInterstitialButton.isEnabled = false
        logFunctionName()
    }
    
    func interstitialLevelPlayDidShow(with adInfo: ISAdInfo!) {
        logFunctionName()
    }
    
    func interstitialLevelPlayDidFailToShowWithError(_ error: Error!, andAdInfo adInfo: ISAdInfo!) {
        logFunctionName(string: String(describing: error.self))
    }
    
    func interstitialLevelPlayDidClick(with adInfo: ISAdInfo!) {
        logFunctionName()
    }
        
    func interstitialLevelPlayDidClose(with adInfo: ISAdInfo!) {
        logFunctionName()
    }


    //MARK: Banner LevelPlay Callbacks Wrapper Functions
    
    func bannerLevelPlayDidLoad(_ bannerView: ISBannerView!, andAdInfo adInfo: ISAdInfo!) {
        self.bannerView = bannerView
        
        var xPosition = (view.frame.size.width - bannerView.frame.size.width) / 2.0
        var yPosition = view.frame.size.height - bannerView.frame.size.height
        var bannerWidth = bannerView.frame.size.width
        var bannerHeight = 0.0
        
        if #available(iOS 11.0, *) {
            bannerHeight = bannerView.frame.size.height - self.view.safeAreaInsets.bottom * 2.5
        } else {
            bannerHeight = bannerView.frame.size.height * 2.5
        }
        
        bannerView.frame = CGRect(x: xPosition, y: yPosition, width: bannerWidth, height: bannerHeight)
        view.addSubview(bannerView)
        destroyBannerButton.isEnabled = true
        
        logFunctionName()
    }
    
    func bannerLevelPlayDidFailToLoadWithError(_ error: Error!) {
        logFunctionName(string: #function+String(describing: Error.self))
    }
    
    func bannerLevelPlayDidClick(with adInfo: ISAdInfo!) {
        logFunctionName()
    }
    
    func bannerLevelPlayDidLeaveApplication(with adInfo: ISAdInfo!) {
        logFunctionName()
    }
    
    func bannerLevelPlayDidPresentScreen(with adInfo: ISAdInfo!) {
        logFunctionName()
    }
    
    func bannerLevelPlayDidDismissScreen(with adInfo: ISAdInfo!) {
        logFunctionName()
    }


    //MARK: ISImpressionData Functions

    func impressionDataDidSucceed(_ impressionData: ISImpressionData!) {
        logFunctionName(string: #function+String(describing: impressionData))
    }
}
