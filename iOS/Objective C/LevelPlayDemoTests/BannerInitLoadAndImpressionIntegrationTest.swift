import Testing
import IronSource
import UIKit

@Test(.timeLimit(.minutes(1)))
@MainActor
func testShouldInitLoadBannerAndReceiveImpression() async throws {
    // Given
    let delegate = BannerTestDelegate()
    let impressionDelegate = BannerImpressionDelegate()

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

    let bannerSize = LPMAdSize.createAdaptive()!
    let config = LPMBannerAdViewConfigBuilder().set(adSize: bannerSize).build()
    let bannerAd = LPMBannerAdView(adUnitId: kBannerAdUnitId, config: config)
    bannerAd.setDelegate(delegate)

    let windowScene = UIApplication.shared.connectedScenes.first as? UIWindowScene
    let rootVC = windowScene?.windows.first(where: { $0.isKeyWindow })?.rootViewController

    bannerAd.translatesAutoresizingMaskIntoConstraints = false
    rootVC!.view.addSubview(bannerAd)
    NSLayoutConstraint.activate([
        bannerAd.centerXAnchor.constraint(equalTo: rootVC!.view.centerXAnchor),
        bannerAd.bottomAnchor.constraint(equalTo: rootVC!.view.safeAreaLayoutGuide.bottomAnchor),
        bannerAd.widthAnchor.constraint(equalToConstant: CGFloat(bannerSize.width)),
        bannerAd.heightAnchor.constraint(equalToConstant: CGFloat(bannerSize.height))
    ])

    // When
    try await withCheckedThrowingContinuation { (continuation: CheckedContinuation<Void, Error>) in
        delegate.onLoad = { continuation.resume() }
        delegate.onLoadFail = { error in continuation.resume(throwing: error) }
        bannerAd.loadAd(with: rootVC!)
    }

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
    private(set) var received = false

    func impressionDataDidSucceed(_ impressionData: LPMImpressionData) {
        received = true
        onImpression?()
    }
}
