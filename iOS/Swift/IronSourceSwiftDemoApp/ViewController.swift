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

let kAPPKEY = "8545d445"


class ViewController: UIViewController, ISRewardedVideoDelegate, ISOfferwallDelegate, ISInterstitialDelegate, ISBannerDelegate,ISImpressionDataDelegate {
    
    //MARK: IBOutlets
    @IBOutlet weak var showRVButton: UIButton!
    @IBOutlet weak var showOWButton: UIButton!
    @IBOutlet weak var showISButton: UIButton!
    @IBOutlet weak var loadISButton: UIButton!
    @IBOutlet weak var loadBNButton: UIButton!
    @IBOutlet weak var destroyBNButton: UIButton!
    @IBOutlet weak var versionLabel: UILabel!
    var bannerView: ISBannerView! = nil

    //MARK: IBOutlets Actions
    @IBAction func showRVButtonAction(_ sender: Any) {
        IronSource.showRewardedVideo(with: self)
    }
    
    @IBAction func showOWButtonAction(_ sender: Any) {
        IronSource.showOfferwall(with: self)
    }

    @IBAction func showISButtonAction(_ sender: Any) {
        IronSource.showInterstitial(with: self)
    }
    
    @IBAction func loadISButtonAction(_ sender: Any) {
        IronSource.loadInterstitial()
    }
    
    @IBAction func loadBNButton(_ sender: Any) {
        let BNSize: ISBannerSize = ISBannerSize(description: "BANNER",width:320 ,height:50)
       IronSource.loadBanner(with: self, size: BNSize)
    }
    
    @IBAction func destroyBNButton(_ sender: Any) {
        if bannerView != nil {
            IronSource.destroyBanner(bannerView)
        }
        
    }
    
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
        ISIntegrationHelper.validateIntegration()
        
        
        
        // Before initializing any of our products (Rewarded video, Offerwall or Interstitial) you must set
        // their delegates. Take a look at these classes and you will see that they each implement a product
        // protocol. This is our way of letting you know what's going on, and if you don't set the delegates
        // we will not be able to communicate with you.
        // We're passing 'self' to our delegates because we want
        // to be able to enable/disable buttons to match ad availability.
        IronSource.setRewardedVideoDelegate(self)
        IronSource.setOfferwallDelegate(self)
        IronSource.setInterstitialDelegate(self)
        IronSource.setBannerDelegate(self)
        IronSource.add(self)
        
    
        
        IronSource.initWithAppKey(kAPPKEY)
        // To initialize specific ad units:
//        IronSource.initWithAppKey(kAPPKEY, adUnits:[IS_REWARDED_VIDEO,IS_INTERSTITIAL,IS_OFFERWALL,IS_BANNER])
    }
    
    func logFunctionName(string: String = #function) {
        print("IronSource Swift Demo App: "+string)
    }
    
    //MARK: ISInterstitialDelegate Functions
    /**
     Called after an interstitial has been clicked.
     */
    public func didClickInterstitial() {
        logFunctionName()
    }
    
    /**
     Called after an interstitial has attempted to show but failed.
     
     @param error The reason for the error
     */
    public func interstitialDidFailToShowWithError(_ error: Error!) {
        logFunctionName(string: String(describing: error.self))
    }
    
    /**
     Called after an interstitial has been displayed on the screen.
     */
    public func interstitialDidShow() {
        logFunctionName()
    }
    
    /**
     Called after an interstitial has been dismissed.
     */
    public func interstitialDidClose() {
        logFunctionName()
    }
    
    /**
     Called after an interstitial has been opened.
     */
    public func interstitialDidOpen() {
        logFunctionName()
    }
    
    /**
     Called after an interstitial has attempted to load but failed.
     
     @param error The reason for the error
     */
    public func interstitialDidFailToLoadWithError(_ error: Error!) {
        logFunctionName(string: #function+String(describing: error.self))
    }
    
    /**
     Called after an interstitial has been loaded
     */
    public func interstitialDidLoad() {
        showISButton.isEnabled=true
        logFunctionName()
    }
    
    
    //MARK: ISOfferwallDelegate Functions
    /**
     Called after the 'offerwallCredits' method has attempted to retrieve user's credits info but failed.
     
     @param error The reason for the error.
     */
    public func didFailToReceiveOfferwallCreditsWithError(_ error: Error!) {
        logFunctionName(string: #function+String(describing: error.self))
    }
    
    /**
     @abstract Called each time the user completes an offer.
     @discussion creditInfo is a dictionary with the following key-value pairs:
     
     "credits" - (int) The number of credits the user has Earned since the last didReceiveOfferwallCredits event that returned YES. Note that the credits may represent multiple completions (see return parameter).
     
     "totalCredits" - (int) The total number of credits ever earned by the user.
     
     "totalCreditsFlag" - (BOOL) In some cases, we won’t be able to provide the exact amount of credits since the last event (specifically if the user clears the app’s data). In this case the ‘credits’ will be equal to the "totalCredits", and this flag will be YES.
     
     @param creditInfo Offerwall credit info.
     
     @return The publisher should return a BOOL stating if he handled this call (notified the user for example). if the return value is NO, the 'credits' value will be added to the next call.
     */
    public func didReceiveOfferwallCredits(_ creditInfo: [AnyHashable : Any]!) -> Bool {
        logFunctionName()
        
        return true;
    }
    
    /**
     Called after the offerwall has been dismissed.
     */
    public func offerwallDidClose() {
        logFunctionName()
    }
    
    /**
     Called after the offerwall has attempted to show but failed.
     
     @param error The reason for the error.
     */
    public func offerwallDidFailToShowWithError(_ error: Error!) {
        logFunctionName(string: #function+String(describing: Error.self))
    }
    
    /**
     Called after the offerwall has been displayed on the screen.
     */
    public func offerwallDidShow() {
        logFunctionName()
    }
    
    /**
     Called after the offerwall has changed its availability.
     
     @param available The new offerwall availability. YES if available and ready to be shown, NO otherwise.
     */
    public func offerwallHasChangedAvailability(_ available: Bool) {
        showOWButton.isEnabled=true
        logFunctionName(string: #function+String(describing: available.self))
    }
    
    
    //MARK: ISRewardedVideoDelegate Functions
    /**
     Called after a rewarded video has changed its availability.
     
     @param available The new rewarded video availability. YES if available and ready to be shown, NO otherwise.
     */
    public func rewardedVideoHasChangedAvailability(_ available: Bool) {
      showRVButton.isEnabled=true
        logFunctionName(string: #function+String(available.self))
    }
    
    /**
     Called after a rewarded video has finished playing.
     */
    public func rewardedVideoDidEnd() {
        logFunctionName()
    }
    
    /**
     Called after a rewarded video has started playing.
     */
    public func rewardedVideoDidStart() {
        logFunctionName()
    }
    
    /**
     Called after a rewarded video has been dismissed.
     */
    public func rewardedVideoDidClose() {
        logFunctionName()
    }
    
    /**
     Called after a rewarded video has been opened.
     */
    public func rewardedVideoDidOpen() {
        logFunctionName()
    }
    
    /**
     Called after a rewarded video has attempted to show but failed.
     
     @param error The reason for the error
     */
    public func rewardedVideoDidFailToShowWithError(_ error: Error!) {
        logFunctionName(string: #function+String(describing: error.self))
    }
    
    /**
     Called after a rewarded video has been viewed completely and the user is eligible for reward.
     
     @param placementInfo An object that contains the placement's reward name and amount.
     */
    public func didReceiveReward(forPlacement placementInfo: ISPlacementInfo!) {
        logFunctionName(string: #function+String(describing: placementInfo.self))
    }
    /**
     Called after a rewarded video has been clicked.
     
     @param placementInfo An object that contains the placement's reward name and amount.
     */
    func didClickRewardedVideo(_ placementInfo: ISPlacementInfo!) {
        logFunctionName(string: #function+String(describing: placementInfo.self))
    }
    
    
     //MARK: ISBannerDelegate Functions
    func bannerDidLoad(_ bannerView: ISBannerView!) {
        self.bannerView=bannerView
        if #available(iOS 11.0, *) {
            bannerView.frame = CGRect(x: view.frame.size.width/2 - bannerView.frame.size.width/2, y: view.frame.size.height - bannerView.frame.size.height, width: bannerView.frame.size.width, height: bannerView.frame.size.height - self.view.safeAreaInsets.bottom * 2.5)
        } else {
                bannerView.frame = CGRect(x: view.frame.size.width/2 - bannerView.frame.size.width/2, y: view.frame.size.height - bannerView.frame.size.height, width: bannerView.frame.size.width, height: bannerView.frame.size.height  * 2.5)
        }


         
         view.addSubview(bannerView)
        
        logFunctionName()
    }
    
    func bannerDidFailToLoadWithError(_ error: Error!) {
        logFunctionName(string: #function+String(describing: Error.self))

    }
    
    func didClickBanner() {
        logFunctionName()
    }
    
    func bannerWillPresentScreen() {
        logFunctionName()
    }
    
    func bannerDidDismissScreen() {
        logFunctionName()
    }
    
    func bannerWillLeaveApplication() {
        logFunctionName()
    }
    
    //MARK: ISImpressionData Functions
    func impressionDataDidSucceed(_ impressionData: ISImpressionData!) {
        logFunctionName(string: #function+String(describing: impressionData))

    }
}

