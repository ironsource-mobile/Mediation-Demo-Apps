package com.ironsource.ironsourcesdkdemo

import com.ironsource.mediationsdk.model.Placement

interface DemoActivityListener {
    fun setEnablementForButton(buttonIdentifier: DemoButtonIdentifiers, enable: Boolean)
    fun setBannerViewVisibility(visibility: Int)
    fun setPlacementInfo(placementInfo: Placement)
    fun showRewardDialog()
}