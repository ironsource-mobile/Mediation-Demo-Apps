//
//  DemoViewController.swift
//  IronSourceSwiftDemoApp
//
//  Copyright © 2024 ironSource Mobile Ltd. All rights reserved.
//

import UIKit
import Foundation
import ObjectiveC.runtime
import IronSource

let defaultUserId = "demoapp"
let appKey = "8545d445"

enum ButtonIdentifiers : Int {
    case showRewardedVideoButtonIdentifier
    case loadInterstitialButtonIdentifier
    case showInterstitialButtonIdentifier
    case loadBannerButtonIdentifier
    case destroyBannerButtonIdentifier
}

protocol DemoViewControllerDelegate: NSObjectProtocol {
    func setButtonEnablement(
            _ buttonIdentifier: ButtonIdentifiers,
            enable: Bool
        )
    func setAndBindBannerView(_ bannerView: ISBannerView!)
    func setPlacementInfo(_ placementInfo: ISPlacementInfo?)
    func showVideoRewardMessage()
}

class DemoViewController: UIViewController, DemoViewControllerDelegate {
    
    //MARK: IBOutlets
    
    @IBOutlet weak var showRewardedVideoButton: UIButton!
    
    @IBOutlet weak var loadInterstitialButton: UIButton!
    @IBOutlet weak var showInterstitialButton: UIButton!
    
    @IBOutlet weak var loadBannerButton: UIButton!
    @IBOutlet weak var destroyBannerButton: UIButton!
    
    @IBOutlet weak var versionLabel: UILabel!
    
    var rewardedVideoDelegate: DemoRewardedVideoAdDelegate! = nil
    var interstitialDelegate: DemoInterstitialAdDelegate! = nil
    var bannerDelegate: DemoBannerAdDelegate! = nil
    
    var initializationDelegate: DemoInitializationDelegate! = nil
    
    var impressionDataDelegate: DemoImpressionDataDelegate! = nil
    
    var rewardedVideoPlacementInfo: ISPlacementInfo! = nil
    var bannerView: ISBannerView! = nil
    
    //MARK: Lifecycle Methods
    
    override func viewDidLoad() {
        super.viewDidLoad()
        self.setupUI()
        self.setupIronSourceSdk()
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    //MARK: Private Methods
    
    func setupUI() {
        self.versionLabel.text =  String(format: "sdk version %@", IronSource.sdkVersion());
        let buttons = [self.showRewardedVideoButton, self.loadInterstitialButton, self.showInterstitialButton, self.loadBannerButton, self.destroyBannerButton]

        for button in buttons {
            button?.layer.cornerRadius = 17.0
            button?.layer.masksToBounds = true
            button?.layer.borderWidth = 2.5
            button?.layer.borderColor = UIColor.gray.cgColor
            button?.contentEdgeInsets = UIEdgeInsets(top: 8, left: 20, bottom: 8, right: 20)
        }
    }
    
    func setupIronSourceSdk() {
#if debug
        // The ISIntegrationHelper is used to validate the integration.
        // Remove it before going live!
        ISIntegrationHelper.validateIntegration()
#endif
        
        // Before initializing any of our products (Rewarded video, Interstitial or Banner) you must set
        // their delegates. Take a look at each of these delegates method and you will see that they each implement a product
        // protocol. This is our way of letting you know what's going on, and if you don't set the delegates
        // we will not be able to communicate with you.
        // We're passing 'self' to our delegates because we want
        // to be able to enable/disable buttons to match ad availability.
        rewardedVideoDelegate = .init(delegate: self)
        IronSource.setLevelPlayRewardedVideoDelegate(rewardedVideoDelegate)
        
        interstitialDelegate = .init(delegate: self)
        IronSource.setLevelPlayInterstitialDelegate(interstitialDelegate)
        
        bannerDelegate = .init(delegate: self)
        IronSource.setLevelPlayBannerDelegate(bannerDelegate)
        
        impressionDataDelegate = .init()
        IronSource.add(self.impressionDataDelegate)
        
        initializationDelegate = .init(delegate: self)
        
        // After setting the delegates you can go ahead and initialize the SDK.
        // Once the iniitaliztion callback is return you can start loading your ads
        logMethodName(string: "initWithAppKey(\(String(describing: appKey)), initializationDelegate)")
        IronSource.initWithAppKey(appKey, delegate: self.initializationDelegate)
        
        // To initialize specific ad units:
        // IronSource.initWithAppKey(appKey, adUnits: [IS_REWARDED_VIDEO, IS_INTERSTITIAL, IS_BANNER], delegate: self.initializationDelegate)
        
        // Scroll down the file to find out what happens when you tap a button...
    }
    
    //MARK: Interface Handling
    
    @IBAction func showRewardedVideoButtonTapped(_ sender: Any) {
        // It is advised to make sure there is available ad before attempting to show an ad
        if IronSource.hasRewardedVideo() {
            // This will present the Rewarded Video.

            logMethodName(string: "showRewardedVideo(with:)")
            IronSource.showRewardedVideo(with: self)
        } else {
            // wait for the availability of rewarded video to change to true before calling show
        }
    }
    
    @IBAction func loadInterstitialButtonTapped(_ sender: Any) {
        // This will load an Interstitial ad

        logMethodName(string: "loadInterstitial()")
        IronSource.loadInterstitial()
    }

    @IBAction func showInterstitialButtonTapped(_ sender: Any) {
        // It is advised to make sure there is available ad before attempting to show an ad
        if IronSource.hasInterstitial() {
            // This will present the Interstitial.
            // Unlike Rewarded Videos there are no placements.

            logMethodName(string: "showInterstitial(with:)")
            IronSource.showInterstitial(with: self)
        } else {
            // load a new ad before calling show
        }
    }
    
    @IBAction func loadBannerButtonTapped(_ sender: Any) {
        // call IronSource.destroyBanner(bannerView) before loading a new banner
        if bannerView != nil {
            destroyBanner()
        }
        
        let bannerSize: ISBannerSize = ISBannerSize(description:kSizeBanner, width:320, height:50)
        
        // This will load the Banner. You can supply a placement
        // by calling 'loadBanner(with: viewControler, size: bannerSize, placement: placement)', or you can simply
        // call 'loadBanner(with: self, size: bannerSize)'.
        // In this case the SDK will use the default placement one created for you.
        // You can pick any banner size: kSizeBanner, kSizeLarge, kSizeRectangle or kSizeLeaderboard

        logMethodName(string: "loadBanner(with:, size:)")
        IronSource.loadBanner(with: self, size: bannerSize)
    }
    
    @IBAction func destroyBannerButtonTapped(_ sender: Any) {
        destroyBanner()
    }

    func destroyBanner() {
        DispatchQueue.main.async {
            if self.bannerView != nil {
                self.logMethodName(string: "destroyBanner(with:)")
                IronSource.destroyBanner(self.bannerView)
                self.bannerView = nil
            }
            
            self.setButtonEnablement(ButtonIdentifiers.loadBannerButtonIdentifier, enable: true)
            self.setButtonEnablement(ButtonIdentifiers.destroyBannerButtonIdentifier, enable: false)
        }
    }
    
    //MARK: DemoViewControllerDelegate
    
    func setButtonEnablement(_ buttonIdentifier: ButtonIdentifiers, enable: Bool) {
        DispatchQueue.main.async {
            var buttonToModify: UIButton?

            switch buttonIdentifier {
            case .showRewardedVideoButtonIdentifier:
                buttonToModify = self.showRewardedVideoButton
            case .loadInterstitialButtonIdentifier:
                buttonToModify = self.loadInterstitialButton
            case .showInterstitialButtonIdentifier:
                buttonToModify = self.showInterstitialButton
            case .loadBannerButtonIdentifier:
                buttonToModify = self.loadBannerButton
            case .destroyBannerButtonIdentifier:
                buttonToModify = self.destroyBannerButton
            }
            
            buttonToModify?.isEnabled = enable
        }
    }
    
    func setAndBindBannerView(_ bannerView: ISBannerView!) {
        DispatchQueue.main.async {
            if (self.bannerView != nil) {
                self.bannerView.removeFromSuperview()
            }
            
            self.bannerView = bannerView
            self.bannerView.translatesAutoresizingMaskIntoConstraints = false
            self.view.addSubview(bannerView)
            
            let centerX = self.bannerView.centerXAnchor.constraint(equalTo: self.view.centerXAnchor)
            let bottom = self.bannerView.bottomAnchor.constraint(equalTo: self.view.safeAreaLayoutGuide.bottomAnchor)
            let width = self.bannerView.widthAnchor.constraint(equalToConstant: bannerView.frame.size.width)
            let height = self.bannerView.heightAnchor.constraint(equalToConstant: bannerView.frame.size.height)
            NSLayoutConstraint.activate([centerX, bottom, width, height])
        }
    }
    
    func setPlacementInfo(_ placementInfo: ISPlacementInfo?) {
        // Setting the rewarded video placement info, an object that contains the placement's reward name and amount
        self.rewardedVideoPlacementInfo = placementInfo
    }
    
    func showVideoRewardMessage() {
        // Showing a graphical indication of the reward name and amount after the user closed the rewarded video ad
        if (self.rewardedVideoPlacementInfo != nil) {
            let rootViewController = UIApplication.shared.delegate?.window?!.rootViewController
            let message = String(format: "You have been rewarded %d %@", (rewardedVideoPlacementInfo.rewardAmount.intValue), rewardedVideoPlacementInfo.rewardName)
            
            let alert = UIAlertController(
                title: "Video Reward",
                message: message,
                preferredStyle: .alert)
            
            let okAction = UIAlertAction(
                title: "OK",
                style: .default,
                handler: { action in
                })
            
            alert.addAction(okAction)
            rootViewController?.present(alert, animated: false)
            
            rewardedVideoPlacementInfo = nil
        }
    }
    
    //MARK:  Demo Utils

    func logMethodName(string: String = #function) {
        print("DemoViewController \(string)")
    }
}
