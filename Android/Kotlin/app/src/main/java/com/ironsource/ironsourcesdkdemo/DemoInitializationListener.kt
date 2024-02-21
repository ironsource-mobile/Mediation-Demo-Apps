package com.ironsource.ironsourcesdkdemo

import com.ironsource.ironsourcesdkdemo.DemoActivity.Companion.logCallbackName
import com.ironsource.mediationsdk.sdk.InitializationListener

class DemoInitializationListener(private val listener: DemoActivityListener) : InitializationListener {

    private val TAG = DemoInitializationListener::class.java.name

    /**
    Called after the Mediation successfully completes its initialization
     */
    override fun onInitializationComplete() {
        logCallbackName(TAG, "")
        listener.setEnablementForButton(DemoButtonIdentifiers.LOAD_INTERSTITIAL_BUTTON_IDENTIFIER, true)
        listener.setEnablementForButton(DemoButtonIdentifiers.LOAD_BANNER_BUTTON_IDENTIFIER, true)
    }
}