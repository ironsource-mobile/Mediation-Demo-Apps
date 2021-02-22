using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ShowOfferwallScript : MonoBehaviour
{
	GameObject InitText;
	GameObject ShowButton;
	GameObject ShowText;
	GameObject AmountText;
	int userCredits = 0;

	// Use this for initialization
	void Start ()
	{
		Debug.Log ("ShowOfferwallScript Start called");

		ShowButton = GameObject.Find ("ShowOfferwall");
		ShowText = GameObject.Find ("ShowOfferwallText");
		ShowText.GetComponent<UnityEngine.UI.Text> ().color = UnityEngine.Color.red;

		AmountText = GameObject.Find ("OWAmount");

		// Add Offerwall Events
		IronSourceEvents.onOfferwallClosedEvent += OfferwallClosedEvent;
		IronSourceEvents.onOfferwallOpenedEvent += OfferwallOpenedEvent;
		IronSourceEvents.onOfferwallShowFailedEvent += OfferwallShowFailedEvent;
		IronSourceEvents.onOfferwallAdCreditedEvent += OfferwallAdCreditedEvent;
		IronSourceEvents.onGetOfferwallCreditsFailedEvent += GetOfferwallCreditsFailedEvent;
		IronSourceEvents.onOfferwallAvailableEvent += OfferwallAvailableEvent;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void ShowOfferwallButtonClicked ()
	{
		if (IronSource.Agent.isOfferwallAvailable ()) {
			IronSource.Agent.showOfferwall ();
		} else {
			Debug.Log ("IronSource.Agent.isOfferwallAvailable - False");
		}
	}
		
	void OfferwallOpenedEvent ()
	{
		Debug.Log ("I got OfferwallOpenedEvent");
	}

	void OfferwallClosedEvent ()
	{
		Debug.Log ("I got OfferwallClosedEvent");
	}

	void OfferwallShowFailedEvent (IronSourceError error)
	{
		Debug.Log ("I got OfferwallShowFailedEvent, code :  " + error.getCode () + ", description : " + error.getDescription ());
	}

	void OfferwallAdCreditedEvent (Dictionary<string, object> dict)
	{
		Debug.Log ("I got OfferwallAdCreditedEvent, current credits = " + dict ["credits"] + " totalCredits = " + dict ["totalCredits"]);
		userCredits = userCredits + Convert.ToInt32 (dict ["credits"]);
		AmountText.GetComponent<UnityEngine.UI.Text> ().text = "" + userCredits;
	}

	void GetOfferwallCreditsFailedEvent (IronSourceError error)
	{
		Debug.Log ("I got GetOfferwallCreditsFailedEvent, code :  " + error.getCode () + ", description : " + error.getDescription ());
	}

	void OfferwallAvailableEvent (bool canShowOfferwal)
	{
		Debug.Log ("I got OfferwallAvailableEvent, value = " + canShowOfferwal);
		if (canShowOfferwal) {
			ShowText.GetComponent<UnityEngine.UI.Text> ().color = UnityEngine.Color.blue;
		} else {
			ShowText.GetComponent<UnityEngine.UI.Text> ().color = UnityEngine.Color.red;
		}
	}
}
