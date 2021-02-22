#pragma clang diagnostic ignored "-Wdeprecated-declarations"
#pragma clang diagnostic ignored "-Wtypedef-redefinition"
#pragma clang diagnostic ignored "-Wobjc-designated-initializers"
#pragma clang diagnostic ignored "-Wunguarded-availability-new"
#define DEBUG 1
#include <stdarg.h>
#include <objc/objc.h>
#include <objc/runtime.h>
#include <objc/message.h>
#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

@class SceneDelegate;
@class AppDelegate;
@class ISRewardedVideoDelegate;
@class DemoApp_iOS_ViewController_RVDelegate;
@class ISOfferwallDelegate;
@class DemoApp_iOS_ViewController_OWDelegate;
@class ISInterstitialDelegate;
@class DemoApp_iOS_ViewController_ISDelegate;
@class ViewController;
@class ISBannerDelegate;
@class DemoApp_iOS_BannerWrapper;
@class UIKit_UIControlEventProxy;
@class Foundation_NSDispatcher;
@class __MonoMac_NSActionDispatcher;
@class UIKit_UIView_UIViewAppearance;
@class __NSObject_Disposer;
@class IronSource;
@class ISBannerSize;
@class ISConfigurations;
@class ISIntegrationHelper;
@class ISPlacementInfo;
@class ISSegment;
@class ISSegmentDelegate;
@class ISSupersonicAdsConfiguration;
@class IronSourceiOS_ISBannerView_ISBannerViewAppearance;
@class ISBannerView;

@interface SceneDelegate : UIResponder<UIWindowSceneDelegate> {
}
	@property (nonatomic, assign) UIWindow * window;
	-(void) release;
	-(id) retain;
	-(uint32_t) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (uint32_t) gchandle;
	-(UIWindow *) window;
	-(void) setWindow:(UIWindow *)p0;
	-(void) scene:(UIScene *)p0 willConnectToSession:(UISceneSession *)p1 options:(UISceneConnectionOptions *)p2;
	-(void) sceneDidDisconnect:(UIScene *)p0;
	-(void) sceneDidBecomeActive:(UIScene *)p0;
	-(void) sceneWillResignActive:(UIScene *)p0;
	-(void) sceneWillEnterForeground:(UIScene *)p0;
	-(void) sceneDidEnterBackground:(UIScene *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface AppDelegate : UIResponder<UIApplicationDelegate> {
}
	@property (nonatomic, assign) UIWindow * window;
	-(void) release;
	-(id) retain;
	-(uint32_t) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (uint32_t) gchandle;
	-(UIWindow *) window;
	-(void) setWindow:(UIWindow *)p0;
	-(BOOL) application:(UIApplication *)p0 didFinishLaunchingWithOptions:(NSDictionary *)p1;
	-(UISceneConfiguration *) application:(UIApplication *)p0 configurationForConnectingSceneSession:(UISceneSession *)p1 options:(UISceneConnectionOptions *)p2;
	-(void) application:(UIApplication *)p0 didDiscardSceneSessions:(NSSet <UISceneSession *>*)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface ISRewardedVideoDelegate : NSObject {
}
	-(id) init;
@end

@interface ISOfferwallDelegate : NSObject {
}
	-(id) init;
@end

@interface ISInterstitialDelegate : NSObject {
}
	-(id) init;
@end

@interface ViewController : UIViewController {
}
	@property (nonatomic, assign) UIButton * LoadIS;
	@property (nonatomic, assign) UIButton * ShowIS;
	@property (nonatomic, assign) UIButton * ShowOW;
	@property (nonatomic, assign) UIButton * ShowRV;
	@property (nonatomic, assign) UILabel * version_lbl;
	-(void) release;
	-(id) retain;
	-(uint32_t) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (uint32_t) gchandle;
	-(UIButton *) LoadIS;
	-(void) setLoadIS:(UIButton *)p0;
	-(UIButton *) ShowIS;
	-(void) setShowIS:(UIButton *)p0;
	-(UIButton *) ShowOW;
	-(void) setShowOW:(UIButton *)p0;
	-(UIButton *) ShowRV;
	-(void) setShowRV:(UIButton *)p0;
	-(UILabel *) version_lbl;
	-(void) setVersion_lbl:(UILabel *)p0;
	-(void) viewDidLoad;
	-(void) didReceiveMemoryWarning;
	-(void) LoadIS_TouchUpInside:(UIButton *)p0;
	-(void) ShowIS_TouchUpInside:(UIButton *)p0;
	-(void) ShowOW_TouchUpInside:(UIButton *)p0;
	-(void) ShowRV_TouchUpInside:(UIButton *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ISBannerDelegate : NSObject {
}
	-(id) init;
@end

@interface DemoApp_iOS_BannerWrapper : NSObject {
}
	-(void) release;
	-(id) retain;
	-(uint32_t) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (uint32_t) gchandle;
	-(void) didClickBanner;
	-(void) bannerDidDismissScreen;
	-(void) bannerDidFailToLoadWithError:(NSError *)p0;
	-(void) bannerDidLoad:(id)p0;
	-(void) bannerWillLeaveApplication;
	-(void) bannerWillPresentScreen;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface UIKit_UIView_UIViewAppearance : NSObject {
}
	-(void) release;
	-(id) retain;
	-(uint32_t) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (uint32_t) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface IronSource : NSObject {
}
	-(id) init;
@end

@interface ISBannerSize : NSObject {
}
	-(int) height;
	-(NSString *) sizeDescription;
	-(int) width;
	-(id) initWithDescription:(NSString *)p0;
@end

@interface ISConfigurations : NSObject {
}
	-(NSString *) plugin;
	-(void) setPlugin:(NSString *)p0;
	-(NSString *) pluginFrameworkVersion;
	-(void) setPluginFrameworkVersion:(NSString *)p0;
	-(NSString *) pluginVersion;
	-(void) setPluginVersion:(NSString *)p0;
	-(NSDictionary *) rewardedVideoCustomParameters;
	-(void) setRewardedVideoCustomParameters:(NSDictionary *)p0;
	-(id) init;
@end

@interface ISIntegrationHelper : NSObject {
}
	-(id) init;
@end

@interface ISPlacementInfo : NSObject {
}
	-(NSString *) placementName;
	-(NSNumber *) rewardAmount;
	-(NSString *) rewardName;
	-(id) initWithPlacement:(NSString *)p0 reward:(NSString *)p1 rewardAmount:(NSNumber *)p2;
@end

@interface ISSegment : NSObject {
}
	-(void) setCustomValue:(NSString *)p0 forKey:(NSString *)p1;
	-(int) age;
	-(void) setAge:(int)p0;
	-(NSDictionary *) customKeys;
	-(int) gender;
	-(void) setGender:(int)p0;
	-(double) iapTotal;
	-(void) setIapTotal:(double)p0;
	-(int) level;
	-(void) setLevel:(int)p0;
	-(BOOL) paying;
	-(void) setPaying:(BOOL)p0;
	-(NSString *) segmentName;
	-(void) setSegmentName:(NSString *)p0;
	-(NSDate *) userCreationDate;
	-(void) setUserCreationDate:(NSDate *)p0;
	-(id) init;
@end

@interface ISSegmentDelegate : NSObject {
}
	-(id) init;
@end

@interface ISSupersonicAdsConfiguration : NSObject {
}
	-(NSNumber *) useClientSideCallbacks;
	-(void) setUseClientSideCallbacks:(NSNumber *)p0;
	-(id) init;
@end

@interface IronSourceiOS_ISBannerView_ISBannerViewAppearance : UIKit_UIView_UIViewAppearance {
}
@end

@interface ISBannerView : UIView {
}
	-(id) init;
	-(id) initWithCoder:(NSCoder *)p0;
@end


