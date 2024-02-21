package com.ironsource.ironsourcesdkdemo

import com.ironsource.ironsourcesdkdemo.DemoActivity.Companion.logCallbackName
import com.ironsource.mediationsdk.impressionData.ImpressionData
import com.ironsource.mediationsdk.impressionData.ImpressionDataListener

class DemoImpressionDataListener : ImpressionDataListener {
    private val TAG = DemoImpressionDataListener::class.java.name

    /**
    Called when the ad was displayed successfully and the impression data was recorded
    @param impressionData The recorded impression data
     */
    override fun onImpressionSuccess(impressionData: ImpressionData) {
        logCallbackName(TAG, "")
    }
}