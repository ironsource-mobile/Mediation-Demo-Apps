import Testing
import IronSource
import UIKit

@Test(.timeLimit(.minutes(1)))
@MainActor
func testShouldInitLoadShowInterstitialAndReceiveImpression() async throws {
    // Given
    let delegate = InterstitialTestDelegate()
    let impressionDelegate = TestImpressionDelegate()

    LevelPlay.add(impressionDelegate as LPMImpressionDataDelegate)

    try await withCheckedThrowingContinuation { (continuation: CheckedContinuation<Void, Error>) in
        let requestBuilder = LPMInitRequestBuilder(appKey: kAppKey)
        let initRequest = requestBuilder.build()
        LevelPlay.initWith(initRequest) { config, error in
            if let error = error {
                continuation.resume(throwing: error)
            } else {
                continuation.resume()
            }
        }
    }

    let interstitialAd = LPMInterstitialAd(adUnitId: kInterstitialAdUnitId)
    interstitialAd.setDelegate(delegate)

    // When
    try await withCheckedThrowingContinuation { (continuation: CheckedContinuation<Void, Error>) in
        delegate.onLoad = { continuation.resume() }
        delegate.onLoadFail = { error in continuation.resume(throwing: error) }
        interstitialAd.loadAd()
    }

    let windowScene = UIApplication.shared.connectedScenes.first as? UIWindowScene
    let rootVC = windowScene?.windows.first(where: { $0.isKeyWindow })?.rootViewController

    interstitialAd.showAd(viewController: rootVC!, placementName: nil as String?)

    // Then
    if !impressionDelegate.received {
        try await withCheckedThrowingContinuation { (continuation: CheckedContinuation<Void, Error>) in
            if impressionDelegate.received {
                continuation.resume()
            } else {
                impressionDelegate.onImpression = { continuation.resume() }
            }
        }
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
    private(set) var received = false

    func impressionDataDidSucceed(_ impressionData: LPMImpressionData) {
        received = true
        onImpression?()
    }
}
