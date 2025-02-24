using UnityEngine;
using UnityEngine.UI;

public class GetPluginVersionScript : MonoBehaviour
{
    GameObject Text;

    // Use this for initialization
    void Start()
    {
        Text = GameObject.Find("PluginVersionText");
        Text.GetComponent<Text>().text = IronSource.pluginVersion();
    }
}