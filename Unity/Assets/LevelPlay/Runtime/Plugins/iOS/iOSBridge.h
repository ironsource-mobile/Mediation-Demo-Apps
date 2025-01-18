//
//  iOSBridge.h
//  iOSBridge
//
//  Created by Supersonic.
//  Copyright (c) 2015 Supersonic. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <IronSource/IronSource.h>
#import "RewardedVideoLevelPlayCallbacksWrapper.h"
#import "InterstitialLevelPlayCallbacksWrapper.h"
#import "BannerLevelPlayCallbacksWrapper.h"

static NSString *  UnityGitHash = @"603786b";
typedef void (*ISUnityBackgroundCallback)(const char* args);
typedef void (*ISUnityPauseGame)(const bool gamePause);

#ifdef __cplusplus
extern "C" {
#endif
    void UnityPause(int pause);
    extern void UnitySendMessage( const char *className, const char *methodName, const char *param );
    
#ifdef __cplusplus
}
#endif

@interface iOSBridge : NSObject<ISSegmentDelegate,
								ISImpressionDataDelegate,
								ISConsentViewDelegate,
								ISInitializationDelegate>

@end


