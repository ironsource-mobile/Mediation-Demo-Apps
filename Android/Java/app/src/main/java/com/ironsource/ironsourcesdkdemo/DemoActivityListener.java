package com.ironsource.ironsourcesdkdemo;

import com.ironsource.mediationsdk.model.Placement;

interface DemoActivityListener {
    void setEnablementForButton(DemoButtonIdentifiers buttonIdentifier, boolean enable);
    void setBannerViewVisibility(int visibility);
    void setPlacementInfo(Placement placementInfo);
    void showRewardDialog();
}