using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable 0618

[Obsolete("This class will be removed in version 9.0.0. Please use ILevelPlayBannerAd instead.")]
/// <summary>
/// Listener that handles banner events.
/// </summary>
public class IronSourceBannerEvents : MonoBehaviour
{
#if UNITY_ANDROID
    #pragma warning disable CS0067
    /// <summary>
    /// Event triggered when a banner has been loaded.
    /// </summary>
    public static event Action<IronSourceAdInfo> onAdLoadedEvent;

    /// <summary>
    /// Event triggered when a banner has left the application.
    /// </summary>
    public static event Action<IronSourceAdInfo> onAdLeftApplicationEvent;

    /// <summary>
    /// Event triggered when a banner screen has been dismissed.
    /// </summary>
    public static event Action<IronSourceAdInfo> onAdScreenDismissedEvent;

    /// <summary>
    /// Event triggered when a banner screen has been presented.
    /// </summary>
    public static event Action<IronSourceAdInfo> onAdScreenPresentedEvent;

    /// <summary>
    /// Event triggered when a banner has been clicked.
    /// </summary>
    public static event Action<IronSourceAdInfo> onAdClickedEvent;

    /// <summary>
    /// Event triggered when a banner has failed to load.
    /// </summary>
    public static event Action<IronSourceError> onAdLoadFailedEvent;
#endif

#if UNITY_ANDROID
    private IUnityLevelPlayBanner LevelPlaybannerAndroid;
#endif

    void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        LevelPlaybannerAndroid = new IronSourceBannerLevelPlayAndroid();//sets this.bannerAndroid as listener for Banner(Mediation Only) events in the bridge
        registerBannerEvents();//subscribe to Banner events from this.bannerAndroid

#endif

        gameObject.name = "IronSourceBannerEvents";           //Change the GameObject name to IronSourceEvents.
        DontDestroyOnLoad(gameObject);                 //Makes the object not be destroyed automatically when loading a new scene.
    }

#if UNITY_ANDROID && !UNITY_EDITOR

    private void registerBannerEvents()
    {
        LevelPlaybannerAndroid.OnAdLoaded += (IronSourceAdInfo) =>
        {
            if (onAdLoadedEvent != null)
            {
                IronSourceEventsDispatcher.executeAction(() =>
                {
                    onAdLoadedEvent?.Invoke(IronSourceAdInfo);
                });
            }
        };

        LevelPlaybannerAndroid.OnAdClicked += (IronSourceAdInfo) =>
        {
            if (onAdClickedEvent != null)
            {
                IronSourceEventsDispatcher.executeAction(() =>
                {
                    onAdClickedEvent?.Invoke(IronSourceAdInfo);
                });
            }
        };

        LevelPlaybannerAndroid.OnAdLoadFailed += (ironSourceError) =>
        {
            if (onAdLoadFailedEvent != null)
            {
                IronSourceEventsDispatcher.executeAction(() =>
                {
                    onAdLoadFailedEvent?.Invoke(ironSourceError);
                });
            }
        };

        LevelPlaybannerAndroid.OnAdLeftApplication += (IronSourceAdInfo) =>
        {
            if (onAdLeftApplicationEvent != null)
            {
                IronSourceEventsDispatcher.executeAction(() =>
                {
                    onAdLeftApplicationEvent?.Invoke(IronSourceAdInfo);
                });
            }
        };

        LevelPlaybannerAndroid.OnAdScreenDismissed += (IronSourceAdInfo) =>
        {
            if (onAdScreenDismissedEvent != null)
            {
                IronSourceEventsDispatcher.executeAction(() =>
                {
                    onAdScreenDismissedEvent?.Invoke(IronSourceAdInfo);
                });
            }
        };

        LevelPlaybannerAndroid.OnAdScreenPresented += (IronSourceAdInfo) =>
        {
            if (onAdScreenPresentedEvent != null)
            {
                IronSourceEventsDispatcher.executeAction(() =>
                {
                    onAdScreenPresentedEvent?.Invoke(IronSourceAdInfo);
                });
            }
        };
    }

#endif

#if !UNITY_ANDROID

    // ******************************* Banner Events *******************************
    private static event Action<IronSourceAdInfo> _onAdLoadedEvent;

    [Obsolete("This event will be removed in version 9.0.0. Please use ILevelPlayBannerAd.OnAdLoaded instead.")]
    /// <summary>
    /// Event triggered when a banner has been loaded.
    /// </summary>
    public static event Action<IronSourceAdInfo> onAdLoadedEvent
    {
        add
        {
            if (_onAdLoadedEvent
                == null || !_onAdLoadedEvent
                    .GetInvocationList().Contains(value))
            {
                _onAdLoadedEvent
                    += value;
            }
        }

        remove
        {
            if (_onAdLoadedEvent
                != null && _onAdLoadedEvent
                    .GetInvocationList().Contains(value))
            {
                _onAdLoadedEvent
                    -= value;
            }
        }
    }

    [Obsolete("This function will be made private in a future version. Please avoid triggering events manually.", false)]
    public void onAdLoaded(string args)
    {
        if (_onAdLoadedEvent != null)
        {
            IronSourceAdInfo adInfo = new IronSourceAdInfo(args);
            _onAdLoadedEvent(adInfo);
        }
    }

    private static event Action<IronSourceError> _onAdLoadFailedEvent;

    [Obsolete("This event will be removed in version 9.0.0. Please use ILevelPlayBannerAd.OnAdLoadFailed instead.")]
    /// <summary>
    /// Event triggered when a banner has failed to load.
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

    private static event Action<IronSourceAdInfo> _onAdClickedEvent;

    [Obsolete("This event will be removed in version 9.0.0. Please use ILevelPlayBannerAd.OnAdClicked instead.")]
    /// <summary>
    /// Event triggered when a banner has been clicked.
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

    private static event Action<IronSourceAdInfo> _onAdScreenPresentedEvent;

    [Obsolete("This event will be removed in version 9.0.0. Please use ILevelPlayBannerAd.OnAdDisplayed instead.")]
    /// <summary>
    /// Event triggered when a banner has been presented.
    /// </summary>
    public static event Action<IronSourceAdInfo> onAdScreenPresentedEvent
    {
        add
        {
            if (_onAdScreenPresentedEvent == null || !_onAdScreenPresentedEvent.GetInvocationList().Contains(value))
            {
                _onAdScreenPresentedEvent += value;
            }
        }

        remove
        {
            if (_onAdScreenPresentedEvent != null && _onAdScreenPresentedEvent.GetInvocationList().Contains(value))
            {
                _onAdScreenPresentedEvent -= value;
            }
        }
    }

    [Obsolete("This function will be made private in a future version. Please avoid triggering events manually.", false)]
    public void onAdScreenPresented(string args)
    {
        if (_onAdScreenPresentedEvent != null)
        {
            IronSourceAdInfo adInfo = new IronSourceAdInfo(args);
            _onAdScreenPresentedEvent(adInfo);
        }
    }

    private static event Action<IronSourceAdInfo> _onAdScreenDismissedEvent;

    [Obsolete("This event will be removed in version 9.0.0.")]
    /// <summary>
    /// Event triggered when a banner has been dismissed.
    /// </summary>
    public static event Action<IronSourceAdInfo> onAdScreenDismissedEvent
    {
        add
        {
            if (_onAdScreenDismissedEvent == null || !_onAdScreenDismissedEvent.GetInvocationList().Contains(value))
            {
                _onAdScreenDismissedEvent += value;
            }
        }

        remove
        {
            if (_onAdScreenDismissedEvent != null && _onAdScreenDismissedEvent.GetInvocationList().Contains(value))
            {
                _onAdScreenDismissedEvent -= value;
            }
        }
    }

    [Obsolete("This function will be made private in a future version. Please avoid triggering events manually.", false)]
    public void onAdScreenDismissed(string args)
    {
        if (_onAdScreenDismissedEvent != null)
        {
            IronSourceAdInfo adInfo = new IronSourceAdInfo(args);
            _onAdScreenDismissedEvent(adInfo);
        }
    }

    private static event Action<IronSourceAdInfo> _onAdLeftApplicationEvent;

    [Obsolete("This event will be removed in version 9.0.0. Please use ILevelPlayBannerAd.OnAdLeftApplication instead.")]
    /// <summary>
    /// Event triggered when a banner has left the application.
    /// </summary>
    public static event Action<IronSourceAdInfo> onAdLeftApplicationEvent
    {
        add
        {
            if (_onAdLeftApplicationEvent == null || !_onAdLeftApplicationEvent.GetInvocationList().Contains(value))
            {
                _onAdLeftApplicationEvent += value;
            }
        }

        remove
        {
            if (_onAdLeftApplicationEvent != null && _onAdLeftApplicationEvent.GetInvocationList().Contains(value))
            {
                _onAdLeftApplicationEvent -= value;
            }
        }
    }

    [Obsolete("This function will be made private in a future version. Please avoid triggering events manually.", false)]
    public void onAdLeftApplication(string args)
    {
        if (_onAdLeftApplicationEvent != null)
        {
            IronSourceAdInfo adInfo = new IronSourceAdInfo(args);
            _onAdLeftApplicationEvent(adInfo);
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
