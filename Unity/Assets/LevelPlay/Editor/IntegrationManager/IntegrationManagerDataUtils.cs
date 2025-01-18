using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using IronSourceJSON;
using UnityEngine;
#if LEVELPLAY_DEPENDENCIES_INSTALLED
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
#endif
namespace Unity.Services.LevelPlay.Editor.IntegrationManager
{
    static class IntegrationManagerDataUtils
    {
        const string k_IronsourceXMLPath = "dependencies/unityversion";
        const string k_IronSourceDownloadDir = "Assets/LevelPlay/Editor/";
        const string k_None = "none";

        internal static string GetVersionFromXMLFile(string file)
        {
            var xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(File.ReadAllText(Path.Combine(k_IronSourceDownloadDir, file)));
            }
            catch (Exception)
            {
                return k_None;
            }
            var unityVersion = xmlDoc.SelectSingleNode(k_IronsourceXMLPath);
            if (unityVersion != null)
            {
                return unityVersion.InnerText;
            }
            File.Delete(Path.Combine(k_IronSourceDownloadDir, file));
            return k_None;
        }

        internal static List<string> GetNativeSDKVersions(string sdkInfoJson)
        {
            var nativeVersions = new List<string>();
            if (!DeserializeJsonToDictionary(sdkInfoJson, out var sdkInfoDictionary))
            {
                return nativeVersions;
            }

            nativeVersions.AddRange(sdkInfoDictionary
                .Keys.Where(key => Regex.IsMatch(key, @"\d+\.\d+\.\d+(\.\d+)?")));
            return nativeVersions;
        }

        internal static bool DeserializeJsonToDictionary(string json, out Dictionary<string, object> dictionary)
        {
            dictionary = new Dictionary<string, object>();
#if LEVELPLAY_DEPENDENCIES_INSTALLED
            if (!IsJsonValid(json))
            {
                return false;
            }
#endif
            try
            {
                dictionary = Json.Deserialize(json) as Dictionary<string, object>;

                return dictionary != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static string ReplaceValue(string downloadURL, string macro, string latestVersion, string versionValue)
        {
            if (downloadURL.Contains(macro))
            {
                return downloadURL.Replace(macro, latestVersion);
            }

            if (downloadURL.Contains(versionValue))
            {
                return downloadURL.Replace(versionValue, latestVersion);
            }

            return downloadURL;
        }

#if LEVELPLAY_DEPENDENCIES_INSTALLED
        internal static bool IsJsonValid(string json)
        {
            if (json == null)
            {
                return false;
            }

            try
            {
                var jToken = JToken.Parse(json);
                return jToken is JObject;
            }
            catch (JsonReaderException)
            {
                return false;
            }
        }

        #endif
    }
}
