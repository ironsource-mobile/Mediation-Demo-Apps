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

let appKey = "8545d445"
let interstitialAdUnitId = "wmgt0712uuux8ju4"
let bannerAdUnitId = "iep3rxsyp9na3rw8"

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
    func setAndBindBannerView(_ bannerView: LPMBannerAdView!, _ bannerSize: LPMAdSize!)
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
        
    var impressionDataDelegate: DemoImpressionDataDelegate! = nil
    
    var rewardedVideoPlacementInfo: ISPlacementInfo! = nil
    var bannerAdView: LPMBannerAdView! = nil
    var interstitialAd: LPMInterstitialAd! = nil

    
        
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
                
        impressionDataDelegate = .init()
        IronSource.add(self.impressionDataDelegate)
                
        // After setting the delegates you can go ahead and initialize the SDK.
        // Once the iniitaliztion callback is return you can start loading your ads
        self.logMethodName(string: "init ironSource SDK with appKey:  \(appKey)")

        // Create a request builder with app key and ad formats. Add User ID if available
           let requestBuilder = LPMInitRequestBuilder(appKey: appKey)
                   .withLegacyAdFormats([IS_REWARDED_VIDEO])
           // Build the initial request
           let initRequest = requestBuilder.build()
           // Initialize LevelPlay with the prepared request
           LevelPlay.initWith(initRequest)
           { config, error in
               if let error = error {
                   self.logMethodName(string: "sdk initialization failed, error =\(error.localizedDescription)")

               } else {
                   self.logMethodName(string: "sdk initialization succeeded")
                   self.setButtonEnablement(ButtonIdentifiers.loadInterstitialButtonIdentifier, enable: true)
                   self.setButtonEnablement(ButtonIdentifiers.loadBannerButtonIdentifier, enable: true)


             }
       }
                
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
        self.interstitialAd = LPMInterstitialAd(adUnitId: interstitialAdUnitId)
        interstitialDelegate = .init(delegate: self)
        self.interstitialAd.setDelegate(interstitialDelegate)
        self.interstitialAd.loadAd()
    }

    @IBAction func showInterstitialButtonTapped(_ sender: Any) {
        // It is advised to make sure there is available ad before attempting to show an ad
        if self.interstitialAd.isAdReady() {
            // This will present the Interstitial.
            // Unlike Rewarded Videos there are no placements.

            logMethodName(string: "showInterstitial(with:)")
            self.interstitialAd.showAd(viewController: self, placementName: nil)
        } else {
            // load a new ad before calling show
        }
    }
    
    @IBAction func loadBannerButtonTapped(_ sender: Any) {
        // call IronSource.destroyBanner(bannerView) before loading a new banner
        if bannerAdView != nil {
            destroyBanner()
        }
        
        // choose banner size
        // 1. recommended - Adaptive ad size that adjusts to the screen width
        // 2. Adaptive ad size using fixed width ad size
        // 3. Specific banner size - BANNER, LARGE, MEDIUM_RECTANGLE
        let bannerSize = LPMAdSize.createAdaptive()
        
        //        let bannerSize = LPMAdSize.createAdaptiveAdSize(withWidth: 400)
        //        let bannerSize = LPMAdSize.mediumRectangle()

        
        guard let bannerSize = bannerSize else {
            return
        }




        //  Create the banner ad view object with required & optional params
        self.bannerAdView = LPMBannerAdView(adUnitId: bannerAdUnitId)
        self.bannerAdView.setAdSize(bannerSize)

    
        
        // set the banner listener
        bannerDelegate = .init(delegate: self, bannerView: bannerAdView, bannerSize: bannerSize)

        self.bannerAdView.setDelegate(bannerDelegate)

        logMethodName(string: "load banner Ad")
        self.bannerAdView.loadAd(with: self)


    }
    
    @IBAction func destroyBannerButtonTapped(_ sender: Any) {
        destroyBanner()
    }

    func destroyBanner() {
        DispatchQueue.main.async {
            if self.bannerAdView != nil {
                self.logMethodName(string: "destroyBanner(with:)")
                self.bannerAdView.destroy()
                self.bannerAdView = nil
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
    
    func setAndBindBannerView(_ bannerView: LPMBannerAdView!,
                              _ bannerSize: LPMAdSize!) {
        DispatchQueue.main.async {
            if (self.bannerAdView != nil) {
                self.bannerAdView.removeFromSuperview()
            }
            
            self.bannerAdView = bannerView
            self.bannerAdView.translatesAutoresizingMaskIntoConstraints = false
            self.view.addSubview(bannerView)
            
            let centerX = self.bannerAdView.centerXAnchor.constraint(equalTo: self.view.centerXAnchor)
            let bottom = self.bannerAdView.bottomAnchor.constraint(equalTo: self.view.safeAreaLayoutGuide.bottomAnchor)
            let width = self.bannerAdView.widthAnchor.constraint(equalToConstant: CGFloat(bannerSize.width))
            let height = self.bannerAdView.heightAnchor.constraint(equalToConstant: CGFloat(bannerSize.height))
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
