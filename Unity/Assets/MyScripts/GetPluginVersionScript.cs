using UnityEngine;
using System.Collections;

public class GetPluginVersionScript : MonoBehaviour
{
	GameObject Text;

	// Use this for initialization
	void Start ()
	{
		Text = GameObject.Find ("PluginVersionText");
		Text.GetComponent<UnityEngine.UI.Text> ().text = IronSource.pluginVersion ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
