package {

import com.ironsource.adobeair.IronSource;
import com.ironsource.adobeair.IronSourceBannerSize;

import flash.display.DisplayObjectContainer;
import flash.display.Loader;

import flash.display.Shape;
import flash.display.SimpleButton;

import flash.display.Sprite;
import flash.display.StageAlign;
import flash.display.StageScaleMode;
import flash.events.DataEvent;
import flash.events.Event;
import flash.events.MouseEvent;
import flash.net.URLRequest;
import flash.system.Capabilities;
import flash.system.Capabilities;
import flash.text.TextField;
import flash.text.TextFieldAutoSize;
import flash.text.TextFormat;

public class Main extends Sprite {
    static const btnW:Number = Capabilities.screenResolutionX * 0.8;
    static const btnH:Number = Capabilities.screenResolutionY * 0.07;
    static const COLOR_GREEN:uint = 0xFF0ddb29;
    static const COLOR_RED:uint = 0xFFbf2011;

    public var btnShowInterstitial:SimpleButton;
    public var btnShowRewardedVideo:SimpleButton;
    public var loader:Loader;
    public var Appkey;

    public function Main() {
        stage.align = StageAlign.TOP_LEFT;
        stage.scaleMode = StageScaleMode.NO_SCALE;
        loader= new Loader();
        loader.contentLoaderInfo.addEventListener(Event.COMPLETE, onComplete);
        loader.load(new URLRequest("assets/logo.png"));
        loader.y = 0;
        loader.x = loader.width;
        addChild(loader);

        createUI();
        addEventListener(Event.DEACTIVATE, onPause);
        addEventListener(Event.ACTIVATE, onResume);


        if (Capabilities.version.substr(0, 3) == "AND") {
            Appkey = "1dc3db545";

        }else {
            Appkey = "1dc3deecd";
            // Register for apple attribution and request ATT
            IronSource.instance.registerAppForAdNetworkAttribution();
            IronSource.instance.requestTrackingAuthorizationWithCompletionHandler();
            var userStatus:String = IronSource.instance.trackingAuthorizationStatus();
            trace("trackingAuthorizationStatus is "+userStatus.toString());
        }


        IronSource.instance.addEventListener("levelPlayRewardedAvailable", levelPlayRewardedAvailable);
        IronSource.instance.addEventListener("levelPlayRewardedAdUnavailable", levelPlayRewardedAdUnavailable);
        IronSource.instance.addEventListener("levelPlayRewardedAdOpened", levelPlayRewardedAdOpened);
        IronSource.instance.addEventListener("levelPlayRewardedOnAdRewarded", levelPlayRewardedOnAdRewarded);
        IronSource.instance.addEventListener("levelPlayRewardedAdShowFailed", levelPlayRewardedAdShowFailed);
        IronSource.instance.addEventListener("levelPlayRewardedVideoAdClicked", levelPlayRewardedVideoAdClicked);
        IronSource.instance.addEventListener("levelPlayRewardedAdClosed", levelPlayRewardedAdClosed);


        IronSource.instance.addEventListener("levelPlayInterstitialAdReady", levelPlayInterstitialAdReady);
        IronSource.instance.addEventListener("levelPlayInterstitialAdLoadFailed", levelPlayInterstitialAdLoadFailed);
        IronSource.instance.addEventListener("levelPlayInterstitialAdOpened", levelPlayInterstitialAdOpened);
        IronSource.instance.addEventListener("levelPlayInterstitialAdShowSucceeded", levelPlayInterstitialAdShowSucceeded);
        IronSource.instance.addEventListener("levelPlayInterstitialAdShowFailed", levelPlayInterstitialAdShowFailed);
        IronSource.instance.addEventListener("levelPlayInterstitialAdClicked", levelPlayInterstitialAdClicked);
        IronSource.instance.addEventListener("levelPlayInterstitialAdClosed", levelPlayInterstitialAdClosed);


        IronSource.instance.addEventListener("levelPlayBannerAdLoaded", levelPlayBannerAdLoaded);
        IronSource.instance.addEventListener("levelPlayBannerAdLoadFailed", levelPlayBannerAdLoadFailed);
        IronSource.instance.addEventListener("levelPlayBannerAdClicked", levelPlayBannerAdClicked);
        IronSource.instance.addEventListener("levelPlayBannerAdLeftApplication", levelPlayBannerAdLeftApplication);
        IronSource.instance.addEventListener("levelPlayBannerAdScreenPresented", levelPlayBannerAdScreenPresented);
        IronSource.instance.addEventListener("levelPlayBannerAdScreenDismissed", levelPlayBannerAdScreenDismissed);

        IronSource.instance.addEventListener("onSegmentReceived", onSegmentReceived);
        IronSource.instance.addEventListener("onImpressionDataDidSucceed", onImpressionDataDidSucceed);

        IronSource.instance.setIronSourceClientSideCallbacks(true);
        IronSource.instance.init(Appkey);
        loadBanner();
        loadInterstitial();
    }

    private function onComplete(event:Event):void {
        loader.x = Capabilities.screenResolutionX/2 - loader.width/2;
    }

    private function showRewardedVideo(event:MouseEvent):void {
        if (IronSource.instance.isRewardedVideoAvailable())
            IronSource.instance.showRewardedVideo();
        else
            trace("Rewarded Video is not available");
    }



    private function loadInterstitial():void {
        IronSource.instance.loadInterstitial();
    }

    private function showInterstitial(event:MouseEvent):void {
        if (IronSource.instance.isInterstitialReady()) {
            IronSource.instance.showInterstitial();
            btnShowInterstitial.enabled = false;
        }
        else {
            trace("Interstitial is not available");
        }
    }

    private function loadBanner():void {
        IronSource.instance.loadBanner(IronSourceBannerSize.BANNER, IronSource.POSITION_BOTTOM);
    }

    private function onResume(event:Event=null):void {
        IronSource.instance.onResume();
    }

    private function onPause(event:Event=null):void {
        IronSource.instance.onPause();
    }

    //************************** RewardedVideo Callbacks **************************
    private function levelPlayRewardedAdOpened(event:DataEvent):void {
        trace("levelPlayRewardedAdOpened");
    }

    private function levelPlayRewardedAdClosed(event:DataEvent):void {
        trace("levelPlayRewardedAdClosed");
    }

    private function levelPlayRewardedAvailable(event:DataEvent):void {
        trace("levelPlayRewardedAvailable: " + event.data);
        btnRewardedVideo.label = "Show Rewarded Video";
        btnRewardedVideo.enabled = true;
    }

    private function levelPlayRewardedAdUnavailable(event:DataEvent):void {
        trace("levelPlayRewardedAdUnavailable: " + event.data);
        btnRewardedVideo.label = "Rewarded Video Unavailable";
        btnRewardedVideo.enabled = false;
    }


    private function levelPlayRewardedOnAdRewarded(event:DataEvent):void {
        var placementId:String;
        var placementName:String;
        var rewardName:String;
        var rewardAmount:int;

        if (event.data) {
            var placement:Object = JSON.parse(event.data);
            placementId = placement.placement_id;
            placementName = placement.placement_name;
            rewardName = placement.placement_reward_name;
            rewardAmount = parseInt(placement.placement_reward_amount);
        }

        trace("levelPlayRewardedOnAdRewarded. Placement Id: " + placementId + " ; Placement name: " + placementName + " ; Reward name: " + rewardName + " ; Reward amount: " + rewardAmount);
    }

    private function levelPlayRewardedAdShowFailed(event:DataEvent):void {
        var errorCode:String;
        var errorDescription:String;

        if (event.data) {
            var error:Object = JSON.parse(event.data);
            errorDescription = error.error_description;
            errorCode = error.error_code;
        }

        trace("levelPlayRewardedAdShowFailed. Error code: " + errorCode + " ; Description: " + errorDescription);
    }

    private function levelPlayRewardedVideoAdClicked(event:DataEvent):void {
        trace("levelPlayRewardedVideoAdClicked");
    }

    //************************** Interstitial Callbacks **************************
    private function levelPlayInterstitialAdReady(event:DataEvent):void {
        trace("levelPlayInterstitialAdReady");
        btnShowInterstitial.enabled = true;
    }

    private function levelPlayInterstitialAdLoadFailed(event:DataEvent):void {
        var errorCode:String;
        var errorDescription:String;

        if (event.data) {
            var error:Object = JSON.parse(event.data);
            errorDescription = error.error_description;
            errorCode = error.error_code;
        }

        trace("levelPlayInterstitialAdLoadFailed. Error code: " + errorCode + " ; Description: " + errorDescription);

        btnShowInterstitial.enabled = false;
    }

    private function levelPlayInterstitialAdOpened(event:DataEvent):void {
        trace("levelPlayInterstitialAdOpened");
    }

    private function levelPlayInterstitialAdClosed(event:DataEvent):void {
        trace("levelPlayInterstitialAdClosed");
    }

    private function levelPlayInterstitialAdShowSucceeded(event:DataEvent):void {
        trace("levelPlayInterstitialAdShowSucceeded");
    }

    private function levelPlayInterstitialAdShowFailed(event:DataEvent):void {
        var errorCode:String;
        var errorDescription:String;

        if (event.data) {
            var error:Object = JSON.parse(event.data);
            errorDescription = error.error_description;
            errorCode = error.error_code;
        }

        trace("levelPlayInterstitialAdShowFailed. Error code: " + errorCode + " ; Description: " + errorDescription);
    }

    private function levelPlayInterstitialAdClicked(event:DataEvent):void {
        trace("levelPlayInterstitialAdClicked");
    }



    //************************** Banner Callbacks **************************

    private function levelPlayBannerAdLoaded(event:DataEvent):void {
        trace("levelPlayBannerAdLoaded");
    }

    private function levelPlayBannerAdLoadFailed(event:DataEvent):void {
        trace("levelPlayBannerAdLoadFailed: " + event.data);
    }

    private function levelPlayBannerAdClicked(event:DataEvent):void {
        trace("levelPlayBannerAdClicked");
    }

    private function levelPlayBannerAdScreenPresented(event:DataEvent):void {
        trace("levelPlayBannerAdScreenPresented");
    }

    private function levelPlayBannerAdScreenDismissed(event:DataEvent):void {
        trace("levelPlayBannerAdScreenDismissed");
    }

    private function levelPlayBannerAdLeftApplication(event:DataEvent):void {
        trace("levelPlayBannerAdLeftApplication");
    }


    //************************** Segment Callbacks **************************
    private function onSegmentReceived(event:DataEvent):void {
        trace("onSegmentReceived: " + event.data);
    }

    //************************** ARM Callbacks **************************
    private function onImpressionDataDidSucceed(event:DataEvent):void {
        trace("onImpressionDataDidSucceed: " + event.data);

    }

    //************************** UI Methods **************************

    static function addLabel(text:String,parent:DisplayObjectContainer, currentY:Number):void {
        var tempLabel:TextField = new TextField();
        tempLabel.defaultTextFormat = new TextFormat('Verdana', 36, 0xFF000000);
        tempLabel.text = text;
        tempLabel.y = currentY;
        tempLabel.width = Capabilities.screenResolutionX / 4;
        tempLabel.height = Capabilities.screenResolutionY * 0.05;
        tempLabel.x = Capabilities.screenResolutionX / 2 - tempLabel.width / 2;

        tempLabel.wordWrap = true;
        parent.addChild(tempLabel);
    }

    static function addFullButton(name:String, parent:DisplayObjectContainer, currentY:Number):SimpleButton
    {
        // Text format
        var tf:TextFormat = new TextFormat();
        tf.color = 0x000000;
        tf.font = "Verdana";
        tf.size = 36;
        tf.align = "center";

        // Create colored rectangle for background of buttons
        var buttonColor:Shape = new Shape();
        buttonColor.graphics.beginFill( 0xFFc3c1ce );
        buttonColor.graphics.drawRect( 0, 0, btnW, btnH );
        buttonColor.graphics.endFill();

        var pressedButtonColor:Shape = new Shape();
        pressedButtonColor.graphics.beginFill( 0xFFa7a6b2 );
        pressedButtonColor.graphics.drawRect( 0, 0, btnW, btnH );
        pressedButtonColor.graphics.endFill();

        // Create text fields for buttons
        var txt:TextField = new TextField();
        txt.autoSize = TextFieldAutoSize.CENTER;
        txt.text = name;
        txt.setTextFormat( tf );
        txt.x = 0;
        txt.y = (buttonColor.height - txt.textHeight) / 2;
        txt.width = buttonColor.width;
        txt.height = buttonColor.height;

        var pressedText:TextField = new TextField();
        pressedText.autoSize = TextFieldAutoSize.CENTER;
        pressedText.text = name;
        pressedText.setTextFormat( tf );
        pressedText.x = 0;
        pressedText.y = (pressedButtonColor.height - pressedText.textHeight) / 2;
        pressedText.width = pressedButtonColor.width;
        pressedText.height = pressedButtonColor.height;

        // Group text and background in a sprite
        var button:Sprite = new Sprite();
        button.addChild( buttonColor );
        button.addChild( txt );

        var pressedButton:Sprite = new Sprite();
        pressedButton.addChild( pressedButtonColor );
        pressedButton.addChild( pressedText );

        // Add it all together and make a button
        var btn:SimpleButton = new SimpleButton();
        btn.upState = button;
        btn.overState = btn.upState;
        btn.hitTestState = btn.upState;
        btn.downState = pressedButton;
        btn.x = Capabilities.screenResolutionX / 2 - btn.width / 2;
        btn.y = currentY;
        btn.name = name;

        parent.addChild( btn );
        return btn;
    }

    private function createUI():void{
        btnShowInterstitial = addFullButton("Show Interstitial", this, Capabilities.screenResolutionY * 0.3);
        btnShowInterstitial.addEventListener(MouseEvent.CLICK, showInterstitial);
        btnShowInterstitial.enabled = false;
        btnShowRewardedVideo = addFullButton("Show Rewarded Video", this,Capabilities.screenResolutionY * 0.4 );
        btnShowRewardedVideo.addEventListener(MouseEvent.CLICK , showRewardedVideo);
        btnShowRewardedVideo.enabled = false;

        addLabel("Mediation SDK Version " + IronSource.instance.getPluginVersion(),this,Capabilities.screenResolutionY * 0.8);
    }
}
}
