package com.ironsource.ironsourcesdkdemo;

import static com.ironsource.ironsourcesdkdemo.DemoActivity.logCallbackName;

import com.ironsource.mediationsdk.sdk.InitializationListener;

public class DemoInitializationListener implements InitializationListener {
    private final String TAG = DemoInitializationListener.class.getSimpleName();
    private final DemoActivityListener listener;

    public DemoInitializationListener(DemoActivityListener listener) {
        this.listener = listener;
    }

    /**
     Called after the Mediation successfully completes its initialization
     */
    @Override
    public void onInitializationComplete() {
        logCallbackName(TAG, "");
        this.listener.setEnablementForButton(DemoButtonIdentifiers.LOAD_INTERSTITIAL_BUTTON_IDENTIFIER, true);
        this.listener.setEnablementForButton(DemoButtonIdentifiers.LOAD_BANNER_BUTTON_IDENTIFIER, true);
    }
}
