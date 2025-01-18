using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

#pragma warning disable 0618

/// <summary>
/// Access point to LevelPlay's events
/// </summary>
public class IronSourceEvents : MonoBehaviour
{
#if UNITY_IPHONE || UNITY_IOS
    delegate void ISUnityBackgroundCallback(string args);
    [DllImport("__Internal")]
    static extern void RegisterCallback(ISUnityBackgroundCallback func);

#endif

#if UNITY_ANDROID
    #pragma warning disable CS0067
    /// <summary>
    /// Event triggered when the SDK initialization completes successfully
    /// </summary>
    public static event Action onSdkInitializationCompletedEvent;

    /// <summary>
    /// Event triggered when a segment has been received
    /// </summary>
    public static event Action<string> onSegmentReceivedEvent;

#endif

    private const string ERROR_CODE = "error_code";
    private const string ERROR_DESCRIPTION = "error_description";

#pragma warning disable CS0067
    /// <summary>
    /// Event triggered when an impression event occurs
    /// </summary>
    public static event Action<IronSourceImpressionData> onImpressionDataReadyEvent;

#if UNITY_ANDROID
    private IUnityInitialization initializationAndroid;
    private IUnitySegment segmentAndroid;
    private IUnityImpressionData impressionDataAndroid;
#endif

    void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        initializationAndroid = new IronSourceInitializationAndroid();//sets this.initialztionAndroid as listener for initialztionAndroid events in the bridge
        registerInitializationEvents(); //subscribe to initialization events from this.initializationAndroid
        impressionDataAndroid = new IronSourceImpressionDataAndroid();//sets this.impressionDataAndroid as listener for Impression Data events in the bridge
        registerImpressionDataEvents();//subscribe to onImpressionSuccess event from this.impressionDataAndroid
        segmentAndroid = new IronSourceSegmentAndroid();//sets this.segmentAndroid as listener for Segment events in the bridge
        registerSegmentEvents();//subscribe to onSegmentRecieved event from this.segmentAndroid

#endif

#if UNITY_IPHONE || UNITY_IOS
    #if !UNITY_EDITOR
        RegisterCallback(FireCallback);
    #endif
#endif
        gameObject.name = "IronSourceEvents";           //Change the GameObject name to IronSourceEvents.
        DontDestroyOnLoad(gameObject);                 //Makes the object not be destroyed automatically when loading a new scene.
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    private void registerInitializationEvents()
    {
        initializationAndroid.OnSdkInitializationCompletedEvent += () =>
        {
            if (onSdkInitializationCompletedEvent != null)
            {
                IronSourceEventsDispatcher.executeAction(() =>
                {
                    onSdkInitializationCompletedEvent?.Invoke();
                });
            }
        };
    }
    //subscribe to IronSourceSegmentAndroid onSegmentRecieved event and notify to subscribed event inside the app
    private void registerSegmentEvents()
    {
        segmentAndroid.OnSegmentRecieved += (segmentName) =>
        {
            if (onSegmentReceivedEvent != null)
            {
                IronSourceEventsDispatcher.executeAction(() =>
                {
                    onSegmentReceivedEvent?.Invoke(segmentName);
                });
            }
        };
    }

    //subscribe to IronSourceImpressionDatandroid onSegmentRecieved event and notify to subscribed event inside the app
    private void registerImpressionDataEvents()
    {
        impressionDataAndroid.OnImpressionDataReady += (impressionData) =>
        {
            if (onImpressionDataReadyEvent != null)
            {
                onImpressionDataReadyEvent?.Invoke(impressionData);
            }
        };
    }
#endif

#if !UNITY_ANDROID

#if UNITY_IPHONE || UNITY_IOS

    [Obsolete("This function will be made private in a future version.", false)]
    [AOT.MonoPInvokeCallback(typeof(ISUnityBackgroundCallback))]
    public static void FireCallback(string args)
    {
        if (onImpressionDataReadyEvent != null)
        {
            InvokeEvent(onImpressionDataReadyEvent, args);
        }
    }

#endif

    // ******************************* Init Event *******************************

    private static event Action _onSdkInitializationCompletedEvent;

    /// <summary>
    /// Event triggered when the SDK initialization completes successfully
    /// </summary>
    public static event Action onSdkInitializationCompletedEvent
    {
        add
        {
            if (_onSdkInitializationCompletedEvent == null || !_onSdkInitializationCompletedEvent.GetInvocationList().Contains(value))
            {
                _onSdkInitializationCompletedEvent += value;
            }
        }

        remove
        {
            if (_onSdkInitializationCompletedEvent != null && _onSdkInitializationCompletedEvent.GetInvocationList().Contains(value))
            {
                _onSdkInitializationCompletedEvent -= value;
            }
        }
    }

    [Obsolete("This function will be made private in a future version. Please avoid triggering events manually.", false)]
    public void onSdkInitializationCompleted(string empty)
    {
        if (_onSdkInitializationCompletedEvent != null)
        {
            _onSdkInitializationCompletedEvent();
        }
    }

    private static event Action<string> _onSegmentReceivedEvent;

    /// <summary>
    /// Event triggered when a segment has been received
    /// </summary>
    public static event Action<string> onSegmentReceivedEvent
    {
        add
        {
            if (_onSegmentReceivedEvent == null || !_onSegmentReceivedEvent.GetInvocationList().Contains(value))
            {
                _onSegmentReceivedEvent += value;
            }
        }

        remove
        {
            if (_onSegmentReceivedEvent != null && _onSegmentReceivedEvent.GetInvocationList().Contains(value))
            {
                _onSegmentReceivedEvent -= value;
            }
        }
    }

    [Obsolete("This function will be made private in a future version. Please avoid triggering events manually.", false)]
    public void onSegmentReceived(string segmentName)
    {
        if (_onSegmentReceivedEvent != null)
            _onSegmentReceivedEvent(segmentName);
    }
    
#endif

    // ******************************* ConsentView Callbacks *******************************

    //iOS callbacks only - in order to prevent using macro for iOS it's not only iOS
    private static event Action<string, IronSourceError> _onConsentViewDidFailToLoadWithErrorEvent;

    /// <summary>
    /// Event triggered when the consent view fails to load
    /// </summary>
    public static event Action<string, IronSourceError> onConsentViewDidFailToLoadWithErrorEvent
    {
        add
        {
            if (_onConsentViewDidFailToLoadWithErrorEvent == null || !_onConsentViewDidFailToLoadWithErrorEvent.GetInvocationList().Contains(value))
            {
                _onConsentViewDidFailToLoadWithErrorEvent += value;
            }
        }

        remove
        {
            if (_onConsentViewDidFailToLoadWithErrorEvent != null && _onConsentViewDidFailToLoadWithErrorEvent.GetInvocationList().Contains(value))
            {
                _onConsentViewDidFailToLoadWithErrorEvent -= value;
            }
        }
    }

    [Obsolete("This function will be made private in a future version. Please avoid triggering events manually.", false)]
    public void onConsentViewDidFailToLoadWithError(string args)
    {
        if (_onConsentViewDidFailToLoadWithErrorEvent != null && !String.IsNullOrEmpty(args))
        {
            List<object> argList = IronSourceJSON.Json.Deserialize(args) as List<object>;
            IronSourceError err = getErrorFromErrorObject(argList[1]);
            string consentViewType = argList[0].ToString();
            _onConsentViewDidFailToLoadWithErrorEvent(consentViewType, err);
        }
    }

    private static event Action<string, IronSourceError> _onConsentViewDidFailToShowWithErrorEvent;

    /// <summary>
    /// Event triggered when the consent view fails to show
    /// </summary>
    public static event Action<string, IronSourceError> onConsentViewDidFailToShowWithErrorEvent
    {
        add
        {
            if (_onConsentViewDidFailToShowWithErrorEvent == null || !_onConsentViewDidFailToShowWithErrorEvent.GetInvocationList().Contains(value))
            {
                _onConsentViewDidFailToShowWithErrorEvent += value;
            }
        }

        remove
        {
            if (_onConsentViewDidFailToShowWithErrorEvent != null && _onConsentViewDidFailToShowWithErrorEvent.GetInvocationList().Contains(value))
            {
                _onConsentViewDidFailToShowWithErrorEvent -= value;
            }
        }
    }

    [Obsolete("This function will be made private in a future version. Please avoid triggering events manually.", false)]
    public void onConsentViewDidFailToShowWithError(string args)
    {
        if (_onConsentViewDidFailToShowWithErrorEvent != null && !String.IsNullOrEmpty(args))
        {
            List<object> argList = IronSourceJSON.Json.Deserialize(args) as List<object>;
            IronSourceError err = getErrorFromErrorObject(argList[1]);
            string consentViewType = argList[0].ToString();
            _onConsentViewDidFailToShowWithErrorEvent(consentViewType, err);
        }
    }

    private static event Action<string> _onConsentViewDidAcceptEvent;

    /// <summary>
    /// Event triggered when the consent view was accepted
    /// </summary>
    public static event Action<string> onConsentViewDidAcceptEvent
    {
        add
        {
            if (_onConsentViewDidAcceptEvent == null || !_onConsentViewDidAcceptEvent.GetInvocationList().Contains(value))
            {
                _onConsentViewDidAcceptEvent += value;
            }
        }

        remove
        {
            if (_onConsentViewDidAcceptEvent != null && _onConsentViewDidAcceptEvent.GetInvocationList().Contains(value))
            {
                _onConsentViewDidAcceptEvent -= value;
            }
        }
    }

    [Obsolete("This function will be made private in a future version. Please avoid triggering events manually.", false)]
    public void onConsentViewDidAccept(string consentViewType)
    {
        if (_onConsentViewDidAcceptEvent != null)
        {
            _onConsentViewDidAcceptEvent(consentViewType);
        }
    }

    private static event Action<string> _onConsentViewDidDismissEvent;

    /// <summary>
    /// Event triggered when the consent view is dismissed
    /// </summary>
    public static event Action<string> onConsentViewDidDismissEvent
    {
        add
        {
            if (_onConsentViewDidDismissEvent == null || !_onConsentViewDidDismissEvent.GetInvocationList().Contains(value))
            {
                _onConsentViewDidDismissEvent += value;
            }
        }

        remove
        {
            if (_onConsentViewDidDismissEvent != null && _onConsentViewDidDismissEvent.GetInvocationList().Contains(value))
            {
                _onConsentViewDidDismissEvent -= value;
            }
        }
    }

    [Obsolete("This function will be made private in a future version. Please avoid triggering events manually.", false)]
    public void onConsentViewDidDismiss(string consentViewType)
    {
        if (_onConsentViewDidDismissEvent != null)
        {
            _onConsentViewDidDismissEvent(consentViewType);
        }
    }

    private static event Action<string> _onConsentViewDidLoadSuccessEvent;

    /// <summary>
    /// Event triggered when the consent view loads successfully
    /// </summary>
    public static event Action<string> onConsentViewDidLoadSuccessEvent
    {
        add
        {
            if (_onConsentViewDidLoadSuccessEvent == null || !_onConsentViewDidLoadSuccessEvent.GetInvocationList().Contains(value))
            {
                _onConsentViewDidLoadSuccessEvent += value;
            }
        }

        remove
        {
            if (_onConsentViewDidLoadSuccessEvent != null && _onConsentViewDidLoadSuccessEvent.GetInvocationList().Contains(value))
            {
                _onConsentViewDidLoadSuccessEvent -= value;
            }
        }
    }

    [Obsolete("This function will be made private in a future version. Please avoid triggering events manually.", false)]
    public void onConsentViewDidLoadSuccess(string consentViewType)
    {
        if (_onConsentViewDidLoadSuccessEvent != null)
        {
            _onConsentViewDidLoadSuccessEvent(consentViewType);
        }
    }

    private static event Action<string> _onConsentViewDidShowSuccessEvent;

    /// <summary>
    /// Event triggered when the consent view shows successfully
    /// </summary>
    public static event Action<string> onConsentViewDidShowSuccessEvent
    {
        add
        {
            if (_onConsentViewDidShowSuccessEvent == null || !_onConsentViewDidShowSuccessEvent.GetInvocationList().Contains(value))
            {
                _onConsentViewDidShowSuccessEvent += value;
            }
        }

        remove
        {
            if (_onConsentViewDidShowSuccessEvent != null && _onConsentViewDidShowSuccessEvent.GetInvocationList().Contains(value))
            {
                _onConsentViewDidShowSuccessEvent -= value;
            }
        }
    }

    [Obsolete("This function will be made private in a future version. Please avoid triggering events manually.", false)]
    public void onConsentViewDidShowSuccess(string consentViewType)
    {
        if (_onConsentViewDidShowSuccessEvent != null)
        {
            _onConsentViewDidShowSuccessEvent(consentViewType);
        }
    }

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
            int eCode = Convert.ToInt32(error[ERROR_CODE].ToString());
            string eDescription = error[ERROR_DESCRIPTION].ToString();
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

    // Invoke ImpressionDataReady Events

    private static void InvokeEvent(Action<IronSourceImpressionData> evt, String args)
    {
        IronSourceImpressionData impressionData = new IronSourceImpressionData(args);
        evt(impressionData);
    }
}
