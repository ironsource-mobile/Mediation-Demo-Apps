import 'package:flutter/material.dart';
import 'package:ironsource_mediation/ironsource_mediation.dart';

const _btnColorActive = Colors.lightBlueAccent;
const _btnColorInactive = Color.fromARGB(255, 205, 205, 205);
const _bgColor = Color.fromARGB(255, 0, 0, 204);
const double _buttonWidth = 270;

class HomePage extends StatefulWidget {
  const HomePage({Key? key}) : super(key: key);

  @override
  State<HomePage> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage>
    with
        IronSourceRewardedVideoListener,
        IronSourceInterstitialListener,
        IronSourceOfferwallListener,
        IronSourceBannerListener {
  // ButtonStyle by button state
  final ButtonStyle _btnStyle = ButtonStyle(
    textStyle: MaterialStateProperty.resolveWith<TextStyle>((states) => TextStyle(
        fontWeight: FontWeight.w600,
        foreground: Paint()
          ..color = Colors.black.withOpacity(states.contains(MaterialState.disabled) ? 0.5 : 1))),
    backgroundColor: MaterialStateProperty.resolveWith<Color>(
      (states) => states.contains(MaterialState.disabled) ? _btnColorInactive : _btnColorActive,
    ),
  );
  // States
  bool _isRVAvailable = false,
      _isISAvailable = false,
      _isOWAvailable = false,
      _isBannerLoaded = false;

  @override
  void initState() {
    super.initState();
    initIronSource();
  }

  /// Make sure this is called only once per session
  void initIronSource() async {
    const appKey = '880456f5';
    try {
      print('initIronSource for appKey: $appKey');
      IronSource.setRVListener(this);
      IronSource.setOWListener(this);
      IronSource.setISListener(this);
      IronSource.setBNListener(this);

      // TODO: Add pre-init configurations

      await IronSource.init(appKey: appKey);
    } catch (e) {
      print('initIronSource error: $e');
    }
  }

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
        home: Scaffold(
      backgroundColor: _bgColor,
      body: SafeArea(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.center,
          children: <Widget>[
            Row(
              children: <Widget>[
                SizedBox(
                    child: Image.asset(
                  'images/New_Logo.png',
                  height: 100,
                  width: 200,
                )),
              ],
            ),
            const SizedBox(height: 60),
            SizedBox(
              //TODO: rv button
              width: _buttonWidth,
              child: ElevatedButton(
                  style: _btnStyle,
                  onPressed: _isRVAvailable
                      ? () async {
                          if (await IronSource.isRewardedVideoAvailable()) {
                            IronSource.showRewardedVideo();
                          }
                        }
                      : null,
                  child:
                      Text(_isRVAvailable ? "Show Rewarded Video" : "Initializing Rewarded Video")),
            ),
            const SizedBox(height: 20),
            SizedBox(
              width: _buttonWidth, //TODO: ow button
              child: ElevatedButton(
                  style: _btnStyle,
                  onPressed: _isOWAvailable
                      ? () async {
                          if (await IronSource.isOfferwallAvailable()) {
                            IronSource.showOfferwall();
                          }
                        }
                      : null,
                  child: Text(_isOWAvailable ? "Show Offerwall" : "Initializing Offerwall")),
            ),
            const SizedBox(height: 20),
            SizedBox(
              width: _buttonWidth, //TODO: is load button
              child: ElevatedButton(
                  style: _btnStyle,
                  onPressed: !_isISAvailable
                      ? () {
                          IronSource.loadInterstitial();
                        }
                      : null,
                  child: const Text("Load Interstitial")),
            ),
            const SizedBox(height: 20),
            SizedBox(
              width: _buttonWidth, //TODO: is show button
              child: ElevatedButton(
                  style: _btnStyle,
                  onPressed: _isISAvailable
                      ? () async {
                          if (await IronSource.isInterstitialReady()) {
                            IronSource.showInterstitial();
                          }
                        }
                      : null,
                  child: const Text("Show Interstitial")),
            ),
            const SizedBox(height: 20),
            SizedBox(
              width: _buttonWidth,
              child: TextButton(
                  //TODO: bn load button
                  style: _btnStyle,
                  onPressed: !_isBannerLoaded
                      ? () {
                          IronSource.loadBanner(
                              size: IronSourceBannerSize.BANNER,
                              position: IronSourceBannerPosition.Bottom);
                        }
                      : null,
                  child: const Text("Load Banner")),
            ),
            const SizedBox(height: 20),
            SizedBox(
              width: _buttonWidth,
              child: ElevatedButton(
                  //TODO: bn destroy button
                  style: _btnStyle,
                  onPressed: _isBannerLoaded
                      ? () {
                          IronSource.destroyBanner();
                          setState(() {
                            _isBannerLoaded = false;
                          });
                        }
                      : null,
                  child: const Text("Destroy Banner")),
            ),
          ],
        ),
      ),
    ));
  }

  @override
  void onBannerAdClicked() {
    // TODO: implement onBannerAdClicked
  }

  @override
  void onBannerAdLeftApplication() {
    // TODO: implement onBannerAdLeftApplication
  }

  @override
  void onBannerAdLoadFailed(IronSourceError error) {
    // TODO: implement onBannerAdLoadFailed
  }

  @override
  void onBannerAdLoaded() {
    setState(() {
      _isBannerLoaded = true;
    });
    // TODO: implement onBannerAdLoaded
  }

  @override
  void onBannerAdScreenDismissed() {
    // TODO: implement onBannerAdScreenDismissed
  }

  @override
  void onBannerAdScreenPresented() {
    // TODO: implement onBannerAdScreenPresented
  }

  @override
  void onGetOfferwallCreditsFailed(IronSourceError error) {
    // TODO: implement onGetOfferwallCreditsFailed
  }

  @override
  void onInterstitialAdClicked() {
    // TODO: implement onInterstitialAdClicked
  }

  @override
  void onInterstitialAdClosed() {
    // TODO: implement onInterstitialAdClosed
    setState(() {
      _isISAvailable = false;
    });
  }

  @override
  void onInterstitialAdLoadFailed(IronSourceError error) {
    // TODO: implement onInterstitialAdLoadFailed
  }

  @override
  void onInterstitialAdOpened() {
    // TODO: implement onInterstitialAdOpened
  }

  @override
  void onInterstitialAdReady() {
    setState(() {
      _isISAvailable = true;
    });
    // TODO: implement onInterstitialAdReady
  }

  @override
  void onInterstitialAdShowFailed(IronSourceError error) {
    // TODO: implement onInterstitialAdShowFailed
  }

  @override
  void onInterstitialAdShowSucceeded() {
    // TODO: implement onInterstitialAdShowSucceeded
  }

  @override
  void onOfferwallAdCredited(IronSourceOWCreditInfo creditInfo) {
    // TODO: implement onOfferwallAdCredited
  }

  @override
  void onOfferwallAvailabilityChanged(bool isAvailable) {
    setState(() {
      _isOWAvailable = isAvailable;
    });
    // TODO: implement onOfferwallAvailabilityChanged
  }

  @override
  void onOfferwallClosed() {
    // TODO: implement onOfferwallClosed
  }

  @override
  void onOfferwallOpened() {
    // TODO: implement onOfferwallOpened
  }

  @override
  void onOfferwallShowFailed(IronSourceError error) {
    // TODO: implement onOfferwallShowFailed
  }

  @override
  void onRewardedVideoAdClicked(IronSourceRVPlacement placement) {
    // TODO: implement onRewardedVideoAdClicked
  }

  @override
  void onRewardedVideoAdClosed() {
    // TODO: implement onRewardedVideoAdClosed
  }

  @override
  void onRewardedVideoAdEnded() {
    // TODO: implement onRewardedVideoAdEnded
  }

  @override
  void onRewardedVideoAdOpened() {
    // TODO: implement onRewardedVideoAdOpened
  }

  @override
  void onRewardedVideoAdRewarded(IronSourceRVPlacement placement) {
    // TODO: implement onRewardedVideoAdRewarded
  }

  @override
  void onRewardedVideoAdShowFailed(IronSourceError error) {
    // TODO: implement onRewardedVideoAdShowFailed
  }

  @override
  void onRewardedVideoAdStarted() {
    // TODO: implement onRewardedVideoAdStarted
  }

  @override
  void onRewardedVideoAvailabilityChanged(bool isAvailable) {
    setState(() {
      _isRVAvailable = true;
    });
    // TODO: implement onRewardedVideoAvailabilityChanged
  }
}
