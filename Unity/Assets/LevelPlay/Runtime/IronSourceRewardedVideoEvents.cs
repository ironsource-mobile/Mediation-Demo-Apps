using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable 0618

/// <summary>
/// Access point to LevelPlay's rewarded video events
/// </summary>
public class IronSourceRewardedVideoEvents : MonoBehaviour
{
#if UNITY_ANDROID
    #pragma warning disable CS0067
    /// <summary>
    /// Event triggered when a rewarded ad failed to show
    /// </summary>
    public static event Action<IronSourceError, IronSourceAdInfo> onAdShowFailedEvent;

    /// <summary>
    /// Event triggered when a rewarded ad opens
    /// </summary>
    public static event Action<IronSourceAdInfo> onAdOpenedEvent;

    /// <summary>
    /// Event triggered when a rewarded ad is closed
    /// </summary>
    public static event Action<IronSourceAdInfo> onAdClosedEvent;

    /// <summary>
    /// Event triggered when a rewarded ad is rewarded
    /// </summary>
    public static event Action<IronSourcePlacement, IronSourceAdInfo> onAdRewardedEvent;

    /// <summary>
    /// Event triggered when a rewarded ad is clicked
    /// </summary>
    public static event Action<IronSourcePlacement, IronSourceAdInfo> onAdClickedEvent;

    /// <summary>
    /// Event triggered when a rewarded ad becomes available
    /// </summary>
    public static event Action<IronSourceAdInfo> onAdAvailableEvent;

    /// <summary>
    /// Event triggered when a rewarded ad becomes unavailable
    /// </summary>
    public static event Action onAdUnavailableEvent;

    /// <summary>
    /// Event triggered when a rewarded ad failed to load
    /// </summary>
    public static event Action<IronSourceError> onAdLoadFailedEvent;

    /// <summary>
    /// Event triggered when a rewarded ad becomes ready
    /// </summary>
    public static event Action<IronSourceAdInfo> onAdReadyEvent;

#endif

#if UNITY_ANDROID
    private IUnityLevelPlayRewardedVideo LevelPlayRewardedVideoAndroid;
    private IUnityLevelPlayRewardedVideoManual LevelPlayRewardedVideoAndroidManual;
#endif

    void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        LevelPlayRewardedVideoAndroid = new IronSourceRewardedVideoLevelPlayAndroid();//sets this.IronSourceRewardedVideoLevelPlayAndroid as listener for RV events in the bridge
        registerRewardedVideoEvents();//subscribe to RV events from this.rewardedVideoLevelPlayAndroid
        LevelPlayRewardedVideoAndroidManual = new IronSourceRewardedVideoLevelPlayManualAndroid();
        registerRewardedVideoManualEvents();//subscribe to RV Manual events from this.rewardedVideoManualLevelPlayAndroid
#endif

        gameObject.name = "IronSourceRewardedVideoEvents";           //Change the GameObject name to IronSourceEvents.
        DontDestroyOnLoad(gameObject);                 //Makes the object not be destroyed automatically when loading a new scene.
    }

#if UNITY_ANDROID && !UNITY_EDITOR

    //subscribe to IronSourceRewardedVideoLevelPlayAndroid RV Mediation events and notify to subscribed events inside the app
    private void registerRewardedVideoEvents()
    {
        LevelPlayRewardedVideoAndroid.OnAdClicked += (IronSourcePlacement, IronSourceAdInfo) =>
        {
            if (onAdClickedEvent != null)
            {
                IronSourceEventsDispatcher.executeAction(() =>
                {
                    onAdClickedEvent?.Invoke(IronSourcePlacement, IronSourceAdInfo);
                });
            }
        };
        LevelPlayRewardedVideoAndroid.OnAdShowFailed += (IronSourceError, IronSourceAdInfo) =>
        {
            if (onAdShowFailedEvent != null)
            {
                IronSourceEventsDispatcher.executeAction(() =>
                {
                    onAdShowFailedEvent?.Invoke(IronSourceError, IronSourceAdInfo);
                });
            }
        };
        LevelPlayRewardedVideoAndroid.OnAdOpened += (IronSourceAdInfo) =>
        {
            if (onAdOpenedEvent != null)
            {
                IronSourceEventsDispatcher.executeAction(() =>
                {
                    onAdOpenedEvent?.Invoke(IronSourceAdInfo);
                });
            }
        };
        LevelPlayRewardedVideoAndroid.OnAdClosed += (IronSourceAdInfo) =>
        {
            if (onAdClosedEvent != null)
            {
                IronSourceEventsDispatcher.executeAction(() =>
                {
                    onAdClosedEvent?.Invoke(IronSourceAdInfo);
                });
            }
        };

        LevelPlayRewardedVideoAndroid.OnAdRewarded += (IronSourcePlacement, IronSourceAdInfo) =>
        {
            if (onAdRewardedEvent != null)
            {
                IronSourceEventsDispatcher.executeAction(() =>
                {
                    onAdRewardedEvent?.Invoke(IronSourcePlacement, IronSourceAdInfo);
                });
            }
        };

        LevelPlayRewardedVideoAndroid.OnAdAvailable += (IronSourceAdInfo) =>
        {
            if (onAdAvailableEvent != null)
            {
                IronSourceEventsDispatcher.executeAction(() =>
                {
                    onAdAvailableEvent?.Invoke(IronSourceAdInfo);
                });
            }
        };

        LevelPlayRewardedVideoAndroid.OnAdUnavailable += () =>
        {
            if (onAdUnavailableEvent != null)
            {
                IronSourceEventsDispatcher.executeAction(() =>
                {
                    onAdUnavailableEvent?.Invoke();
                });
            }
        };
    }

    // ******************************* RewardedVideo Manual Load Events *******************************

    [Obsolete("This function will be made private in a future version.", false)]
    public void registerRewardedVideoManualEvents()
    {
        LevelPlayRewardedVideoAndroidManual.OnAdReady += (IronSourceAdInfo) =>
        {
            if (onAdReadyEvent != null)
            {
                IronSourceEventsDispatcher.executeAction(() =>
                {
                    onAdReadyEvent?.Invoke(IronSourceAdInfo);
                });
            }
        };

        LevelPlayRewardedVideoAndroidManual.OnAdLoadFailed += (IronSourceError) =>
        {
            if (onAdLoadFailedEvent != null)
            {
                IronSourceEventsDispatcher.executeAction(() =>
                {
                    onAdLoadFailedEvent?.Invoke(IronSourceError);
                });
            }
        };
    }

#endif

#if !UNITY_ANDROID

    // ******************************* Rewarded Video Events *******************************
    private static event Action<IronSourceError, IronSourceAdInfo> _onAdShowFailedEvent;

    /// <summary>
    /// Event triggered when a rewarded ad failed to show
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

    private static event Action<IronSourceAdInfo> _onAdOpenedEvent;

    /// <summary>
    /// Event triggered when a rewarded ad opens
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
    /// Event triggered when a rewarded ad is closed
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

    private static event Action<IronSourcePlacement, IronSourceAdInfo> _onAdRewardedEvent;

    /// <summary>
    /// Event triggered when a rewarded ad is rewarded
    /// </summary>
    public static event Action<IronSourcePlacement, IronSourceAdInfo> onAdRewardedEvent
    {
        add
        {
            if (_onAdRewardedEvent == null || !_onAdRewardedEvent.GetInvocationList().Contains(value))
            {
                _onAdRewardedEvent += value;
            }
        }

        remove
        {
            if (_onAdRewardedEvent != null && _onAdRewardedEvent.GetInvocationList().Contains(value))
            {
                _onAdRewardedEvent -= value;
            }
        }
    }

    [Obsolete("This function will be made private in a future version. Please avoid triggering events manually.", false)]
    public void onAdRewarded(string args)
    {
        if (_onAdRewardedEvent != null)
        {
            List<object> argList = IronSourceJSON.Json.Deserialize(args) as List<object>;
            IronSourcePlacement ssp = getPlacementFromObject(argList[0]);
            IronSourceAdInfo adInfo = new IronSourceAdInfo(argList[1].ToString());
            _onAdRewardedEvent(ssp, adInfo);
        }
    }

    private static event Action<IronSourcePlacement, IronSourceAdInfo> _onAdClickedEvent;

    /// <summary>
    /// Event triggered when a rewarded ad is clicked
    /// </summary>
    public static event Action<IronSourcePlacement, IronSourceAdInfo> onAdClickedEvent
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
            List<object> argList = IronSourceJSON.Json.Deserialize(args) as List<object>;
            IronSourcePlacement ssp = getPlacementFromObject(argList[0]);
            IronSourceAdInfo adInfo = new IronSourceAdInfo(argList[1].ToString());
            _onAdClickedEvent(ssp, adInfo);
        }
    }

    private static event Action<IronSourceAdInfo> _onAdAvailableEvent;

    /// <summary>
    /// Event triggered when a rewarded ad becomes available
    /// </summary>
    public static event Action<IronSourceAdInfo> onAdAvailableEvent
    {
        add
        {
            if (_onAdAvailableEvent == null || !_onAdAvailableEvent.GetInvocationList().Contains(value))
            {
                _onAdAvailableEvent += value;
            }
        }

        remove
        {
            if (_onAdAvailableEvent != null && _onAdAvailableEvent.GetInvocationList().Contains(value))
            {
                _onAdAvailableEvent -= value;
            }
        }
    }

    [Obsolete("This function will be made private in a future version. Please avoid triggering events manually.", false)]
    public void onAdAvailable(string args)
    {
        if (_onAdAvailableEvent != null)
        {
            IronSourceAdInfo adInfo = new IronSourceAdInfo(args);
            _onAdAvailableEvent(adInfo);
        }
    }

    private static event Action _onAdUnavailableEvent;

    /// <summary>
    /// Event triggered when a rewarded ad becomes unavailable
    /// </summary>
    public static event Action onAdUnavailableEvent
    {
        add
        {
            if (_onAdUnavailableEvent == null || !_onAdUnavailableEvent.GetInvocationList().Contains(value))
            {
                _onAdUnavailableEvent += value;
            }
        }

        remove
        {
            if (_onAdUnavailableEvent != null && _onAdUnavailableEvent.GetInvocationList().Contains(value))
            {
                _onAdUnavailableEvent -= value;
            }
        }
    }

    [Obsolete("This function will be made private in a future version. Please avoid triggering events manually.", false)]
    public void onAdUnavailable()
    {
        if (_onAdUnavailableEvent != null)
        {
            _onAdUnavailableEvent();
        }
    }

    // ******************************* RewardedVideo Manual Load Events *******************************

    private static event Action<IronSourceError> _onAdLoadFailedEvent;

    /// <summary>
    /// Event triggered when a rewarded ad failed to load
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

    private static event Action<IronSourceAdInfo> _onAdReadyEvent;

    /// <summary>
    /// Event triggered when a rewarded ad becomes ready
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
    public void onAdReady(string adinfo)
    {
        if (_onAdReadyEvent != null)
        {
            IronSourceAdInfo adInfo = new IronSourceAdInfo(adinfo);
            _onAdReadyEvent(adInfo);
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
