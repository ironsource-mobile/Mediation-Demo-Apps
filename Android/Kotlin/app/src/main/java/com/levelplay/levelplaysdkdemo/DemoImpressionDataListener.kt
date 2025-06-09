package com.levelplay.levelplaysdkdemo

import com.levelplay.levelplaysdkdemo.DemoActivity.Companion.logCallbackName
import com.unity3d.mediation.impression.LevelPlayImpressionData
import com.unity3d.mediation.impression.LevelPlayImpressionDataListener

class DemoImpressionDataListener : LevelPlayImpressionDataListener {
    private val TAG = DemoImpressionDataListener::class.java.name

    /**
    Called when the ad was displayed successfully and the impression data was recorded
    @param impressionData The recorded impression data
     */
    override fun onImpressionSuccess(impressionData: LevelPlayImpressionData) {
        logCallbackName(TAG, "")
    }
}