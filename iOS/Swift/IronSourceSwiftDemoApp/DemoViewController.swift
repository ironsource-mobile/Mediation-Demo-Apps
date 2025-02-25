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

// Replace with your app key as available in the LevelPlay dashboard
let appKey = "8545d445"

// Replace with your ad unit ids as available in the LevelPlay dashboard
let interstitialAdAdUnitId = "wmgt0712uuux8ju4"
let bannerAdAdUnitId = "iep3rxsyp9na3rw8"
let rewardedAdAdUnitId = "qwouvdrkuwivay5q"


enum ButtonIdentifiers : Int {
    case loadRewardedAdButtonIdentifier
    case showRewardedAdButtonIdentifier
    case loadInterstitialAdButtonIdentifier
    case showInterstitialAdButtonIdentifier
    case loadBannerAdButtonIdentifier
}

protocol DemoViewControllerDelegate: NSObjectProtocol {
    func setButtonEnablement(
            _ buttonIdentifier: ButtonIdentifiers,
            enable: Bool
        )
    func setRewardInfo(_ reward: LPMReward?)
    func showVideoRewardMessage()
}

class DemoViewController: UIViewController, DemoViewControllerDelegate {
    
    //MARK: IBOutlets
    
    @IBOutlet weak var loadRewardedAdButton: UIButton!
    @IBOutlet weak var showRewardedAdButton: UIButton!
    var rewardedAdDelegate: DemoRewardedVideoAdDelegate! = nil
    var reward: LPMReward! = nil
    var rewardedAd: LPMRewardedAd! = nil

    @IBOutlet weak var loadInterstitialAdButton: UIButton!
    @IBOutlet weak var showInterstitialAdButton: UIButton!
    var interstitialAdDelegate: DemoInterstitialAdDelegate! = nil
    var interstitialAd: LPMInterstitialAd! = nil

    @IBOutlet weak var loadBannerAdButton: UIButton!
    var bannerAdViewDelegate: DemoBannerAdDelegate! = nil
    var bannerAd: LPMBannerAdView! = nil
    var bannerSize: LPMAdSize! = nil

    var impressionDataDelegate: DemoImpressionDataDelegate! = nil

    @IBOutlet weak var versionLabel: UILabel!
    
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
    
    deinit {
        self.bannerAd.destroy()
    }

    //MARK: Initialization Methods
    
    func setupUI() {
        self.versionLabel.text =  String(format: "sdk version %@", IronSource.sdkVersion());
        let buttons = [self.loadRewardedAdButton, self.showRewardedAdButton, self.loadInterstitialAdButton, self.showInterstitialAdButton, self.loadBannerAdButton]

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

        // Register a listener for all impressions within the current session
        impressionDataDelegate = .init()
        IronSource.add(self.impressionDataDelegate)
        
        // After setting the delegates you can go ahead and initialize the SDK.
        // Once the iniitaliztion callback is return you can start loading your ads
        
        self.logMethodName(string: "init LevePlay SDK with appKey:  \(appKey)")
        let requestBuilder = LPMInitRequestBuilder(appKey: appKey)
        let initRequest = requestBuilder.build()
        LevelPlay.initWith(initRequest)
        { config, error in

            guard error == nil else {
                self.logMethodName(string: "sdk initialization failed, error =\(error?.localizedDescription ?? "unknown error")")
                return
            }
            self.logMethodName(string: "sdk initialization succeeded")
            self.createRewardedAd()
            self.createInterstititalAd()
            self.createBannerAd()
        }
        
        // Scroll down the file to find out what happens when you tap a button...
    }
    
    
    //MARK: Interstitial Methods

    func createInterstititalAd() {
        self.interstitialAd = LPMInterstitialAd(adUnitId: interstitialAdAdUnitId)
        interstitialAdDelegate = .init(delegate: self)
        self.interstitialAd.setDelegate(interstitialAdDelegate)

        self.setButtonEnablement(ButtonIdentifiers.loadInterstitialAdButtonIdentifier, enable: true)
    }
    
    @IBAction func loadInterstitialButtonTapped(_ sender: Any) {
        guard let interstitialAd = self.interstitialAd else {
            return
        }

        // This will load an Interstitial ad
        logMethodName(string: "loadAd for interstitial")
        interstitialAd.loadAd()
    }
    
    @IBAction func showInterstitialButtonTapped(_ sender: Any) {
        // It is advised to make sure there is available ad before attempting to show an ad
        if (self.interstitialAd != nil && self.interstitialAd.isAdReady()) {
            // This will present the Interstitial.
            // Unlike Rewarded Videos there are no placements.

            logMethodName(string: "showAd for interstitial")
            self.interstitialAd.showAd(viewController: self, placementName: nil)
        } else {
            // load a new ad before calling show
        }
    }

    //MARK: Rewarded Video Methods
    
    func createRewardedAd() {
        self.rewardedAd = LPMRewardedAd(adUnitId: rewardedAdAdUnitId)
        rewardedAdDelegate = .init(delegate: self)
        self.rewardedAd.setDelegate(rewardedAdDelegate)

        self.setButtonEnablement(ButtonIdentifiers.loadRewardedAdButtonIdentifier, enable: true)
    }
    
    @IBAction func loadRewardedAdButtonTapped(_ sender: Any) {
        guard let rewardedAd = self.rewardedAd else {
            return
        }

        // This will load a rewarded ad
        logMethodName(string: "loadAd for rewarded")
        rewardedAd.loadAd()
    }

    @IBAction func showRewardedAdButtonTapped(_ sender: Any) {
        // It is advised to make sure there is available ad before attempting to show an ad
        if (self.rewardedAd != nil && self.rewardedAd.isAdReady()) {
            // This will present the Rewarded ad.

            logMethodName(string: "showAd for rewarded")
            self.rewardedAd.showAd(viewController: self, placementName: nil)
        } else {
            // load a new ad before calling show
        }
    }
    
    func setRewardInfo(_ reward: LPMReward?) {
        // Setting the rewarded ad info, an object that contains the reward name and amount
        self.reward = reward
    }
    
    func showVideoRewardMessage() {
        // Showing a graphical indication of the reward name and amount after the user closed the rewarded video ad
        if (self.reward != nil) {
            let rootViewController = UIApplication.shared.delegate?.window?!.rootViewController
            let message = String(format: "You have been rewarded %d %@", reward.amount as Int, reward.name)
            
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
            
            reward = nil
        }
    }
    

    //MARK: Banner Methods

    func createBannerAd() {
        // choose banner size
        // 1. recommended - Adaptive ad size that adjusts to the screen width
        self.bannerSize = LPMAdSize.createAdaptive()
        
        // 2. Adaptive ad size using fixed width ad size
//        self.bannerSize = LPMAdSize.createAdaptiveAdSize(withWidth: 400)
        
        // 3. Specific banner size - BANNER, LARGE, MEDIUM_RECTANGLE
//        self.bannerSize = LPMAdSize.mediumRectangle()

        guard let bannerSize = bannerSize else {
            print("Error creating banner size")
            return
        }

        //  Create the banner ad view object with required & optional params
        self.bannerAd = LPMBannerAdView(adUnitId: bannerAdAdUnitId)
        self.bannerAd.setAdSize(bannerSize)

        // set the banner listener
        bannerAdViewDelegate = .init(delegate: self)
        self.bannerAd.setDelegate(bannerAdViewDelegate)
        
        addBannerToView()
        
        self.setButtonEnablement(ButtonIdentifiers.loadBannerAdButtonIdentifier, enable: true)
    }
    
    func addBannerToView() {
        DispatchQueue.main.async {
            self.bannerAd.translatesAutoresizingMaskIntoConstraints = false
            self.view.addSubview(self.bannerAd)

            let centerX = self.bannerAd.centerXAnchor.constraint(equalTo: self.view.centerXAnchor)
            let bottom = self.bannerAd.bottomAnchor.constraint(equalTo: self.view.safeAreaLayoutGuide.bottomAnchor)
            let width = self.bannerAd.widthAnchor.constraint(equalToConstant: CGFloat(self.bannerSize.width))
            let height = self.bannerAd.heightAnchor.constraint(equalToConstant: CGFloat(self.bannerSize.height))
            NSLayoutConstraint.activate([centerX, bottom, width, height])
        }

    }

    @IBAction func loadBannerAdButtonTapped(_ sender: Any) {
        
        guard let bannerAd = self.bannerAd else {
            return
        }

        logMethodName(string: "loadAd for banner")
        bannerAd.loadAd(with: self)
    }

    //MARK: Utility methods

    func setButtonEnablement(_ buttonIdentifier: ButtonIdentifiers, enable: Bool) {
        DispatchQueue.main.async {
            var buttonToModify: UIButton?

            switch buttonIdentifier {
            case .loadRewardedAdButtonIdentifier:
                buttonToModify = self.loadRewardedAdButton
            case .showRewardedAdButtonIdentifier:
                buttonToModify = self.showRewardedAdButton
            case .loadInterstitialAdButtonIdentifier:
                buttonToModify = self.loadInterstitialAdButton
            case .showInterstitialAdButtonIdentifier:
                buttonToModify = self.showInterstitialAdButton
            case .loadBannerAdButtonIdentifier:
                buttonToModify = self.loadBannerAdButton
            }
            
            buttonToModify?.isEnabled = enable
        }
    }

    func logMethodName(string: String = #function) {
        print("DemoViewController \(string)")
    }
}
