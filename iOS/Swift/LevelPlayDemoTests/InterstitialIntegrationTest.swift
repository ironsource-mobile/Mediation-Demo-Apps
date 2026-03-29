import Testing
import IronSource
import UIKit
@testable import LevelPlayDemo

@Test(.timeLimit(.minutes(1)))
@MainActor
func testInterstitialInitLoadAndImpression() async throws {
    let delegate = InterstitialTestDelegate()
    let impressionDelegate = TestImpressionDelegate()

    LevelPlay.add(impressionDelegate as LPMImpressionDataDelegate)

    // Init SDK
    try await withCheckedThrowingContinuation { (continuation: CheckedContinuation<Void, Error>) in
        let requestBuilder = LPMInitRequestBuilder(appKey: DemoViewController.appKey)
        let initRequest = requestBuilder.build()
        LevelPlay.initWith(initRequest) { config, error in
            if let error = error {
                continuation.resume(throwing: error)
            } else {
                continuation.resume()
            }
        }
    }

    // Create and load ad
    let interstitialAd = LPMInterstitialAd(adUnitId: DemoViewController.interstitialAdUnitId)
    interstitialAd.setDelegate(delegate)

    try await withCheckedThrowingContinuation { (continuation: CheckedContinuation<Void, Error>) in
        delegate.onLoad = { continuation.resume() }
        delegate.onLoadFail = { error in continuation.resume(throwing: error) }
        interstitialAd.loadAd()
    }

    // Show ad
    let rootVC = UIApplication.shared.keyWindow?.rootViewController

    interstitialAd.showAd(viewController: rootVC!, placementName: nil as String?)

    // Wait for impression
    try await withCheckedThrowingContinuation { (continuation: CheckedContinuation<Void, Error>) in
        impressionDelegate.onImpression = { continuation.resume() }
    }
}

private class InterstitialTestDelegate: NSObject, LPMInterstitialAdDelegate {
    var onLoad: (() -> Void)?
    var onLoadFail: ((Error) -> Void)?

    func didLoadAd(with adInfo: LPMAdInfo) { onLoad?() }
    func didFailToLoadAd(withAdUnitId adUnitId: String, error: Error) { onLoadFail?(error) }
    func didDisplayAd(with adInfo: LPMAdInfo) {}
    func didFailToDisplayAd(with adInfo: LPMAdInfo, error: Error) {}
    func didClickAd(with adInfo: LPMAdInfo) {}
    func didCloseAd(with adInfo: LPMAdInfo) {}
    func didChangeAdInfo(_ adInfo: LPMAdInfo) {}
}

private class TestImpressionDelegate: NSObject, LPMImpressionDataDelegate {
    var onImpression: (() -> Void)?

    func impressionDataDidSucceed(_ impressionData: LPMImpressionData) {
        onImpression?()
    }
}
