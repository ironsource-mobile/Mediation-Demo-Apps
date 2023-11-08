using UnityEngine;
using System;
using System.Collections;

public class ShowRewardedVideoScript : MonoBehaviour
{
    GameObject InitText;
    GameObject ShowButton;
    GameObject ShowText;
    GameObject AmountText;
    int userTotalCredits = 0;

    public static String REWARDED_INSTANCE_ID = "0";

    // Use this for initialization
    void Start()
    {
        Debug.Log("unity-script: ShowRewardedVideoScript Start called");

        ShowButton = GameObject.Find("ShowRewardedVideo");
        ShowText = GameObject.Find("ShowRewardedVideoText");
        ShowText.GetComponent<UnityEngine.UI.Text>().color = UnityEngine.Color.red;

        AmountText = GameObject.Find("RVAmount");

        //Add AdInfo Rewarded Video Events
        IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
        IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
        IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
        IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;


        //Add Rewarded Video DemandOnly Events
        IronSourceEvents.onRewardedVideoAdOpenedDemandOnlyEvent += RewardedVideoAdOpenedDemandOnlyEvent;
        IronSourceEvents.onRewardedVideoAdClosedDemandOnlyEvent += RewardedVideoAdClosedDemandOnlyEvent;
        IronSourceEvents.onRewardedVideoAdLoadedDemandOnlyEvent += this.RewardedVideoAdLoadedDemandOnlyEvent;
        IronSourceEvents.onRewardedVideoAdRewardedDemandOnlyEvent += RewardedVideoAdRewardedDemandOnlyEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedDemandOnlyEvent += RewardedVideoAdShowFailedDemandOnlyEvent;
        IronSourceEvents.onRewardedVideoAdClickedDemandOnlyEvent += RewardedVideoAdClickedDemandOnlyEvent;
        IronSourceEvents.onRewardedVideoAdLoadFailedDemandOnlyEvent += this.RewardedVideoAdLoadFailedDemandOnlyEvent;
    }

    // Update is called once per frame
    void Update()
    {
    }

    /************* RewardedVideo API *************/
    public void ShowRewardedVideoButtonClicked()
    {
        Debug.Log("unity-script: ShowRewardedVideoButtonClicked");
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            Debug.Log("unity-script: IronSource.Agent.isRewardedVideoAvailable - False");
        }

        // DemandOnly
        // ShowDemandOnlyRewardedVideo ();
    }

    void LoadDemandOnlyRewardedVideo()
    {
        Debug.Log("unity-script: LoadDemandOnlyRewardedVideoButtonClicked");
        IronSource.Agent.loadISDemandOnlyRewardedVideo(REWARDED_INSTANCE_ID);
    }

    void ShowDemandOnlyRewardedVideo()
    {
        Debug.Log("unity-script: ShowDemandOnlyRewardedVideoButtonClicked");
        if (IronSource.Agent.isISDemandOnlyRewardedVideoAvailable(REWARDED_INSTANCE_ID))
        {
            IronSource.Agent.showISDemandOnlyRewardedVideo(REWARDED_INSTANCE_ID);
        }
        else
        {
            Debug.Log("unity-script: IronSource.Agent.isISDemandOnlyRewardedVideoAvailable - False");
        }
    }

    /************* RewardedVideo Delegates *************/
    void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdOpenedEvent With AdInfo " + adInfo);
    }

    void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdClosedEvent With AdInfo " + adInfo);
    }

    void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdAvailable With AdInfo " + adInfo);
    }

    void RewardedVideoOnAdUnavailable()
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdUnavailable");
    }

    void RewardedVideoOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo)
    {
        Debug.Log(
            "unity-script: I got RewardedVideoAdOpenedEvent With Error" + ironSourceError + "And AdInfo " + adInfo);
    }

    void RewardedVideoOnAdRewardedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdRewardedEvent With Placement" + ironSourcePlacement +
                  "And AdInfo " + adInfo);
    }

    void RewardedVideoOnAdClickedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdClickedEvent With Placement" + ironSourcePlacement +
                  "And AdInfo " + adInfo);
    }

    /************* RewardedVideo DemandOnly Delegates *************/

    void RewardedVideoAdLoadedDemandOnlyEvent(string instanceId)
    {
        ShowText.GetComponent<UnityEngine.UI.Text>().color = UnityEngine.Color.blue;
        Debug.Log("unity-script: I got RewardedVideoAdLoadedDemandOnlyEvent for instance: " + instanceId);
    }

    void RewardedVideoAdLoadFailedDemandOnlyEvent(string instanceId, IronSourceError error)
    {
        ShowText.GetComponent<UnityEngine.UI.Text>().color = UnityEngine.Color.red;
        Debug.Log("unity-script: I got RewardedVideoAdLoadFailedDemandOnlyEvent for instance: " + instanceId +
                  ", code :  " + error.getCode() + ", description : " + error.getDescription());
    }

    void RewardedVideoAdOpenedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got RewardedVideoAdOpenedDemandOnlyEvent for instance: " + instanceId);
    }

    void RewardedVideoAdRewardedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got RewardedVideoAdRewardedDemandOnlyEvent for instance: " + instanceId);
    }

    void RewardedVideoAdClosedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got RewardedVideoAdClosedDemandOnlyEvent for instance: " + instanceId);
    }

    void RewardedVideoAdShowFailedDemandOnlyEvent(string instanceId, IronSourceError error)
    {
        Debug.Log("unity-script: I got RewardedVideoAdShowFailedDemandOnlyEvent for instance: " + instanceId +
                  ", code :  " + error.getCode() + ", description : " + error.getDescription());
    }

    void RewardedVideoAdClickedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got RewardedVideoAdClickedDemandOnlyEvent for instance: " + instanceId);
    }
}