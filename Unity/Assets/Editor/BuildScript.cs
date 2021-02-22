using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEditor;

public class BuildScript : MonoBehaviour
{

	static private string[] collectBuildScenes()
    {
        var scenes = new List<string>();

        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (scene == null)
                continue;
            if (scene.enabled)
                scenes.Add(scene.path);
        }
        return scenes.ToArray();
    }

	[MenuItem(@"BuildScript/BuildAndroid")]
	static void BuildAndroid ()
	{
		EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
		EditorUserBuildSettings.exportAsGoogleAndroidProject = true;
		var scenes = new [] {"Assets/adunitsDemo.unity"};
		BuildPipeline.BuildPlayer(scenes, "Unity-Android-Project" , BuildTarget.Android, BuildOptions.AcceptExternalModificationsToPlayer);
	}

	[MenuItem(@"BuildScript/BuildIOS")]
	static void BuildIOS ()
	{
		PlayerSettings.iOS.allowHTTPDownload=true;
		var scenes = new [] {"Assets/adunitsDemo.unity"};
		BuildPipeline.BuildPlayer(scenes, "iOS-Unity-Project" , BuildTarget.iOS, BuildOptions.None);
	}
}