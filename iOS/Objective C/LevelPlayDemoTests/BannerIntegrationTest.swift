import Testing
import IronSource
import UIKit

@Test(.timeLimit(.minutes(1)))
@MainActor
func testBannerInitLoadAndImpression() async throws {
    let delegate = BannerTestDelegate()
    let impressionDelegate = BannerImpressionDelegate()

    LevelPlay.add(impressionDelegate as LPMImpressionDataDelegate)

    // Init SDK
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

    // Create banner ad
    let bannerSize = LPMAdSize.createAdaptive()!
    let config = LPMBannerAdViewConfigBuilder().set(adSize: bannerSize).build()
    let bannerAd = LPMBannerAdView(adUnitId: kBannerAdUnitId, config: config)
    bannerAd.setDelegate(delegate)

    // Add banner to view hierarchy before loading
    let rootVC = UIApplication.shared.keyWindow?.rootViewController
    bannerAd.translatesAutoresizingMaskIntoConstraints = false
    rootVC!.view.addSubview(bannerAd)
    NSLayoutConstraint.activate([
        bannerAd.centerXAnchor.constraint(equalTo: rootVC!.view.centerXAnchor),
        bannerAd.bottomAnchor.constraint(equalTo: rootVC!.view.safeAreaLayoutGuide.bottomAnchor),
        bannerAd.widthAnchor.constraint(equalToConstant: 320),
        bannerAd.heightAnchor.constraint(equalToConstant: 50)
    ])

    // Load ad (banner auto-displays on load)
    try await withCheckedThrowingContinuation { (continuation: CheckedContinuation<Void, Error>) in
        delegate.onLoad = { continuation.resume() }
        delegate.onLoadFail = { error in continuation.resume(throwing: error) }
        bannerAd.loadAd(with: rootVC!)
    }

    // Wait for impression
    try await withCheckedThrowingContinuation { (continuation: CheckedContinuation<Void, Error>) in
        impressionDelegate.onImpression = { continuation.resume() }
    }

    // Cleanup
    bannerAd.removeFromSuperview()
    bannerAd.destroy()
}

private class BannerTestDelegate: NSObject, LPMBannerAdViewDelegate {
    var onLoad: (() -> Void)?
    var onLoadFail: ((Error) -> Void)?

    func didLoadAd(with adInfo: LPMAdInfo) { onLoad?() }
    func didFailToLoadAd(withAdUnitId adUnitId: String, error: Error) { onLoadFail?(error) }
    func didDisplayAd(with adInfo: LPMAdInfo) {}
    func didFailToDisplayAd(with adInfo: LPMAdInfo, error: Error) {}
    func didClickAd(with adInfo: LPMAdInfo) {}
    func didExpandAd(with adInfo: LPMAdInfo) {}
    func didCollapseAd(with adInfo: LPMAdInfo) {}
    func didLeaveApp(with adInfo: LPMAdInfo) {}
}

private class BannerImpressionDelegate: NSObject, LPMImpressionDataDelegate {
    var onImpression: (() -> Void)?

    func impressionDataDidSucceed(_ impressionData: LPMImpressionData) {
        onImpression?()
    }
}
