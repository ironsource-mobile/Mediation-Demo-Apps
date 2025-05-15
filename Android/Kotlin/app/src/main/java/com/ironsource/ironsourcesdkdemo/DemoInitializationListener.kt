package com.ironsource.ironsourcesdkdemo

import android.content.Context
import com.ironsource.ironsourcesdkdemo.DemoActivity.Companion.logCallbackName
import com.ironsource.mediationsdk.IronSource
import com.unity3d.mediation.LevelPlayConfiguration
import com.unity3d.mediation.LevelPlayInitError
import com.unity3d.mediation.LevelPlayInitListener

class DemoInitializationListener(private val listener: DemoActivityListener, private val context: Context) :
    LevelPlayInitListener {

    private val TAG = DemoInitializationListener::class.java.name


    /**
    triggered when the initialization is completed successfully. After you receive this indication, ads can be loaded
    @param configuration The configuration
     */
    override fun onInitSuccess(configuration: LevelPlayConfiguration) {
        logCallbackName(TAG, "")
        IronSource.launchTestSuite(context);
//        this.listener.createInterstitialAd()
//        this.listener.createBannerAd()
    }

    /**
    the configuration was not retrieved successfully and ads cannot be loaded. It is recommended to try and initialize the ironSource SDK later (when internet connection is available, or when the failure reason is resolved)
    @param error The reason for the error
     */
    override fun onInitFailed(error: LevelPlayInitError) {
        logCallbackName(TAG, "error = $error")
    }


}