package com.levelplay.levelplaysdkdemo

import com.levelplay.levelplaysdkdemo.DemoActivity.Companion.logCallbackName
import com.unity3d.mediation.LevelPlayConfiguration
import com.unity3d.mediation.LevelPlayInitError
import com.unity3d.mediation.LevelPlayInitListener

class DemoInitializationListener(private val listener: DemoActivityListener) :
    LevelPlayInitListener {

    private val TAG = DemoInitializationListener::class.java.name


    /**
    triggered when the initialization is completed successfully. After you receive this indication, ads can be loaded
    @param configuration The configuration
     */
    override fun onInitSuccess(configuration: LevelPlayConfiguration) {
        logCallbackName(TAG, "")
        this.listener.createInterstitialAd()
        this.listener.createRewardedAd()
        this.listener.createBannerAd()
    }

    /**
    the configuration was not retrieved successfully and ads cannot be loaded. It is recommended to try and initialize the levelPlay SDK later (when internet connection is available, or when the failure reason is resolved)
    @param error The reason for the error
     */
    override fun onInitFailed(error: LevelPlayInitError) {
        logCallbackName(TAG, "error = $error")
    }


}