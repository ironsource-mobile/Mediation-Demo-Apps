using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable 0618

[Obsolete("This event will be removed in version 9.0.0. Please use ILevelPlayInterstitialAd instead.")]
/// <summary>
/// Access point to LevelPlay's interstitial events
/// </summary>
public class IronSourceInterstitialEvents : MonoBehaviour
{
#if UNITY_ANDROID
    #pragma warning disable CS0067

    [Obsolete("This event will be removed in version 9.0.0. Please use ILevelPlayInterstitialAd.OnAdLoaded instead.")]
    /// <summary>
    /// Event triggered when an interstitial ad is ready
    /// </summary>
    public static event Action<IronSourceAdInfo> onAdReadyEvent;

    [Obsolete("This event will be removed in version 9.0.0. Please use ILevelPlayInterstitialAd.OnAdLoadFailed instead.")]
    /// <summary>
    /// Event triggered when an interstitial ad fails to load
    /// </summary>
    public static event Action<IronSourceError> onAdLoadFailedEvent;

    [Obsolete("This event will be removed in version 9.0.0. Please use ILevelPlayInterstitialAd.OnAdDisplayed instead.")]
    /// <summary>
    /// Event triggered when an interstitial ad is opened
    /// </summary>
    public static event Action<IronSourceAdInfo> onAdOpenedEvent;

    [Obsolete("This event will be removed in version 9.0.0. Please use ILevelPlayInterstitialAd.OnAdClosed instead.")]
    /// <summary>
    /// Event triggered when an interstitial ad is closed
    /// </summary>
    public static event Action<IronSourceAdInfo> onAdClosedEvent;

    [Obsolete("This event will be removed in version 9.0.0.")]
    /// <summary>
    /// Event triggered when an interstitial ad showed successfully
    /// </summary>
    public static event Action<IronSourceAdInfo> onAdShowSucceededEvent;

    [Obsolete("This event will be removed in version 9.0.0. Please use ILevelPlayInterstitialAd.OnAdDisplayFailed instead.")]
    /// <summary>
    /// Event triggered when an interstitial ad failed to show
    /// </summary>
    public static event Action<IronSourceError, IronSourceAdInfo> onAdShowFailedEvent;

    [Obsolete("This event will be removed in version 9.0.0. Please use ILevelPlayInterstitialAd.OnAdClicked instead.")]
    /// <summary>
    /// Event triggered when an interstitial ad is clicked
    /// </summary>
    public static event Action<IronSourceAdInfo> onAdClickedEvent;


#endif

#if UNITY_ANDROID
    private IUnityLevelPlayInterstitial LevelPlayInterstitialAndroid;
#endif

    void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        LevelPlayInterstitialAndroid = new IronSourceInterstitialLevelPlayAndroid(); //sets this.IronSourceInterstitialLevelPlayAndroid as listener for Interstitial events in the bridge
        registerInterstitialEvents();//subscribe to Interstitial events from this.interstitialAndroid
#endif

        gameObject.name = "IronSourceInterstitialEvents";           //Change the GameObject name to IronSourceEvents.
        DontDestroyOnLoad(gameObject);                 //Makes the object not be destroyed automatically when loading a new scene.
    }

#if UNITY_ANDROID && !UNITY_EDITOR

    //subscribe to IronSourceInterstitialAndroid IS Mediation & rewarded Interstitial events and notify to subscribed events inside the app
    private void registerInterstitialEvents()
    {
        LevelPlayInterstitialAndroid.OnAdClicked += (IronSourceAdInfo) =>
        {
            if (onAdClickedEvent != null)
            {
                IronSourceEventsDispatcher.executeAction(() =>
                {
                    onAdClickedEvent?.Invoke(IronSourceAdInfo);
                });
            }
        };

        LevelPlayInterstitialAndroid.OnAdReady += (IronSourceAdInfo) =>
        {
            if (onAdReadyEvent != null)
            {
                IronSourceEventsDispatcher.executeAction(() =>
                {
                    onAdReadyEvent?.Invoke(IronSourceAdInfo);
                });
            }
        };

        LevelPlayInterstitialAndroid.OnAdClosed += (IronSourceAdInfo) =>
        {
            if (onAdClosedEvent != null)
            {
                IronSourceEventsDispatcher.executeAction(() =>
                {
                    onAdClosedEvent?.Invoke(IronSourceAdInfo);
                });
            }
        };

        LevelPlayInterstitialAndroid.OnAdOpened += (IronSourceAdInfo) =>
        {
            if (onAdOpenedEvent != null)
            {
                IronSourceEventsDispatcher.executeAction(() =>
                {
                    onAdOpenedEvent?.Invoke(IronSourceAdInfo);
                });
            }
        };

        LevelPlayInterstitialAndroid.OnAdLoadFailed += (ironsourceError) =>
        {
            if (onAdLoadFailedEvent != null)
            {
                IronSourceEventsDispatcher.executeAction(() =>
                {
                    onAdLoadFailedEvent?.Invoke(ironsourceError);
                });
            }
        };

        LevelPlayInterstitialAndroid.OnAdShowFailed += (ironSourceError, IronSourceAdInfo) =>
        {
            if (onAdShowFailedEvent != null)
            {
                IronSourceEventsDispatcher.executeAction(() =>
                {
                    onAdShowFailedEvent?.Invoke(ironSourceError, IronSourceAdInfo);
                });
            }
        };

        LevelPlayInterstitialAndroid.OnAdShowSucceeded += (IronSourceAdInfo) =>
        {
            if (onAdShowSucceededEvent != null)
            {
                IronSourceEventsDispatcher.executeAction(() =>
                {
                    onAdShowSucceededEvent?.Invoke(IronSourceAdInfo);
                });
            }
        };
    }

#endif

#if !UNITY_ANDROID

    // ******************************* Interstitial Events *******************************

    private static event Action<IronSourceAdInfo> _onAdReadyEvent;

    /// <summary>
    /// Event triggered when an interstitial ad is ready
    /// </summary>
    public static event Action<IronSourceAdInfo> onAdReadyEvent
    {
        add
        {
            if (_onAdReadyEvent == null || !_onAdReadyEvent.GetInvocationList().Contains(value))
            {
                _onAdReadyEvent += value;
            }
        }

        remove
        {
            if (_onAdReadyEvent != null && _onAdReadyEvent.GetInvocationList().Contains(value))
            {
                _onAdReadyEvent -= value;
            }
        }
    }

    [Obsolete("This function will be made private in a future version. Please avoid triggering events manually.", false)]
    public void onAdReady(string args)
    {
        if (_onAdReadyEvent != null)
        {
            IronSourceAdInfo adInfo = new IronSourceAdInfo(args);
            _onAdReadyEvent(adInfo);
        }
    }

    private static event Action<IronSourceError> _onAdLoadFailedEvent;

    /// <summary>
    /// Event triggered when an interstitial ad fails to load
    /// </summary>
    public static event Action<IronSourceError> onAdLoadFailedEvent
    {
        add
        {
            if (_onAdLoadFailedEvent == null || !_onAdLoadFailedEvent.GetInvocationList().Contains(value))
            {
                _onAdLoadFailedEvent += value;
            }
        }

        remove
        {
            if (_onAdLoadFailedEvent != null && _onAdLoadFailedEvent.GetInvocationList().Contains(value))
            {
                _onAdLoadFailedEvent -= value;
            }
        }
    }

    [Obsolete("This function will be made private in a future version. Please avoid triggering events manually.", false)]
    public void onAdLoadFailed(string description)
    {
        if (_onAdLoadFailedEvent != null)
        {
            IronSourceError sse = getErrorFromErrorObject(description);
            _onAdLoadFailedEvent(sse);
        }
    }

    private static event Action<IronSourceAdInfo> _onAdOpenedEvent;

    /// <summary>
    /// Event triggered when an interstitial ad is opened
    /// </summary>
    public static event Action<IronSourceAdInfo> onAdOpenedEvent
    {
        add
        {
            if (_onAdOpenedEvent == null || !_onAdOpenedEvent.GetInvocationList().Contains(value))
            {
                _onAdOpenedEvent += value;
            }
        }

        remove
        {
            if (_onAdOpenedEvent != null && _onAdOpenedEvent.GetInvocationList().Contains(value))
            {
                _onAdOpenedEvent -= value;
            }
        }
    }

    [Obsolete("This function will be made private in a future version. Please avoid triggering events manually.", false)]
    public void onAdOpened(string args)
    {
        if (_onAdOpenedEvent != null)
        {
            IronSourceAdInfo adInfo = new IronSourceAdInfo(args);
            _onAdOpenedEvent(adInfo);
        }
    }

    private static event Action<IronSourceAdInfo> _onAdClosedEvent;

    /// <summary>
    /// Event triggered when an interstitial ad is closed
    /// </summary>
    public static event Action<IronSourceAdInfo> onAdClosedEvent
    {
        add
        {
            if (_onAdClosedEvent == null || !_onAdClosedEvent.GetInvocationList().Contains(value))
            {
                _onAdClosedEvent += value;
            }
        }

        remove
        {
            if (_onAdClosedEvent != null && _onAdClosedEvent.GetInvocationList().Contains(value))
            {
                _onAdClosedEvent -= value;
            }
        }
    }

    [Obsolete("This function will be made private in a future version. Please avoid triggering events manually.", false)]
    public void onAdClosed(string args)
    {
        if (_onAdClosedEvent != null)
        {
            IronSourceAdInfo adInfo = new IronSourceAdInfo(args);
            _onAdClosedEvent(adInfo);
        }
    }

    private static event Action<IronSourceAdInfo> _onAdShowSucceededEvent;

    /// <summary>
    /// Event triggered when an interstitial ad showed successfully
    /// </summary>
    public static event Action<IronSourceAdInfo> onAdShowSucceededEvent
    {
        add
        {
            if (_onAdShowSucceededEvent == null || !_onAdShowSucceededEvent.GetInvocationList().Contains(value))
            {
                _onAdShowSucceededEvent += value;
            }
        }

        remove
        {
            if (_onAdShowSucceededEvent != null && _onAdShowSucceededEvent.GetInvocationList().Contains(value))
            {
                _onAdShowSucceededEvent -= value;
            }
        }
    }

    [Obsolete("This function will be made private in a future version. Please avoid triggering events manually.", false)]
    public void onAdShowSucceeded(string args)
    {
        if (_onAdShowSucceededEvent != null)
        {
            IronSourceAdInfo adInfo = new IronSourceAdInfo(args);
            _onAdShowSucceededEvent(adInfo);
        }
    }

    private static event Action<IronSourceError, IronSourceAdInfo> _onAdShowFailedEvent;

    /// <summary>
    /// Event triggered when an interstitial ad failed to show
    /// </summary>
    public static event Action<IronSourceError, IronSourceAdInfo> onAdShowFailedEvent
    {
        add
        {
            if (_onAdShowFailedEvent == null || !_onAdShowFailedEvent.GetInvocationList().Contains(value))
            {
                _onAdShowFailedEvent += value;
            }
        }

        remove
        {
            if (_onAdShowFailedEvent != null && _onAdShowFailedEvent.GetInvocationList().Contains(value))
            {
                _onAdShowFailedEvent -= value;
            }
        }
    }

    [Obsolete("This function will be made private in a future version. Please avoid triggering events manually.", false)]
    public void onAdShowFailed(string args)
    {
        if (_onAdShowFailedEvent != null)
        {
            List<object> argList = IronSourceJSON.Json.Deserialize(args) as List<object>;
            IronSourceError err = getErrorFromErrorObject(argList[0]);
            IronSourceAdInfo adInfo = new IronSourceAdInfo(argList[1].ToString());
            _onAdShowFailedEvent(err, adInfo);
        }
    }

    private static event Action<IronSourceAdInfo> _onAdClickedEvent;

    /// <summary>
    /// Event triggered when an interstitial ad is clicked
    /// </summary>
    public static event Action<IronSourceAdInfo> onAdClickedEvent
    {
        add
        {
            if (_onAdClickedEvent == null || !_onAdClickedEvent.GetInvocationList().Contains(value))
            {
                _onAdClickedEvent += value;
            }
        }

        remove
        {
            if (_onAdClickedEvent != null && _onAdClickedEvent.GetInvocationList().Contains(value))
            {
                _onAdClickedEvent -= value;
            }
        }
    }

    [Obsolete("This function will be made private in a future version. Please avoid triggering events manually.", false)]
    public void onAdClicked(string args)
    {
        if (_onAdClickedEvent != null)
        {
            IronSourceAdInfo adInfo = new IronSourceAdInfo(args);
            _onAdClickedEvent(adInfo);
        }
    }

#endif

    // ******************************* Helper methods *******************************

    private IronSourceError getErrorFromErrorObject(object descriptionObject)
    {
        Dictionary<string, object> error = null;
        if (descriptionObject is IDictionary)
        {
            error = descriptionObject as Dictionary<string, object>;
        }
        else if (descriptionObject is String && !String.IsNullOrEmpty(descriptionObject.ToString()))
        {
            error = IronSourceJSON.Json.Deserialize(descriptionObject.ToString()) as Dictionary<string, object>;
        }

        IronSourceError sse = new IronSourceError(-1, "");
        if (error != null && error.Count > 0)
        {
            int eCode = Convert.ToInt32(error[IronSourceConstants.ERROR_CODE].ToString());
            string eDescription = error[IronSourceConstants.ERROR_DESCRIPTION].ToString();
            sse = new IronSourceError(eCode, eDescription);
        }

        return sse;
    }

    private IronSourcePlacement getPlacementFromObject(object placementObject)
    {
        Dictionary<string, object> placementJSON = null;
        if (placementObject is IDictionary)
        {
            placementJSON = placementObject as Dictionary<string, object>;
        }
        else if (placementObject is String)
        {
            placementJSON = IronSourceJSON.Json.Deserialize(placementObject.ToString()) as Dictionary<string, object>;
        }

        IronSourcePlacement ssp = null;
        if (placementJSON != null && placementJSON.Count > 0)
        {
            int rewardAmount = Convert.ToInt32(placementJSON["placement_reward_amount"].ToString());
            string rewardName = placementJSON["placement_reward_name"].ToString();
            string placementName = placementJSON["placement_name"].ToString();

            ssp = new IronSourcePlacement(placementName, rewardName, rewardAmount);
        }

        return ssp;
    }
}
