using UnityEngine;
using UnityEngine.UI;

public class ShowRewardedVideoScript : MonoBehaviour
{
    public static string REWARDED_INSTANCE_ID = "0";
    GameObject AmountText;
    GameObject InitText;
    GameObject ShowButton;
    GameObject ShowText;
    int userTotalCredits = 0;

    // Use this for initialization
    void Start()
    {
        Debug.Log("unity-script: ShowRewardedVideoScript Start called");

        ShowButton = GameObject.Find("ShowRewardedVideo");
        ShowText = GameObject.Find("ShowRewardedVideoText");
        ShowText.GetComponent<Text>().color = Color.red;

        AmountText = GameObject.Find("RVAmount");

        // Add Rewarded Video Events
        IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
        IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
        IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
        IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;
    }

    /************* RewardedVideo API *************/
    public void ShowRewardedVideoButtonClicked()
    {
        Debug.Log("unity-script: ShowRewardedVideoButtonClicked");
        if (IronSource.Agent.isRewardedVideoAvailable())
            IronSource.Agent.showRewardedVideo();
        else
            Debug.Log("unity-script: IronSource.Agent.isRewardedVideoAvailable - False");
    }

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
        Debug.Log("unity-script: I got RewardedVideoAdOpenedEvent With Error" + ironSourceError + "And AdInfo " + adInfo);
    }

    void RewardedVideoOnAdRewardedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdRewardedEvent With Placement" + ironSourcePlacement + "And AdInfo " + adInfo);
    }

    void RewardedVideoOnAdClickedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdClickedEvent With Placement" + ironSourcePlacement + "And AdInfo " + adInfo);
    }
}