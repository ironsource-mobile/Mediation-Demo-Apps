#if LEVELPLAY_DEPENDENCIES_INSTALLED
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Services.LevelPlay.Editor;

namespace Unity.Services.LevelPlay.Editor.IntegrationManager
{
    class IntegrationManagerDownloader
    {
        // Package
        const string k_IronsourceUnityPackageURL =
            "https://github.com/ironsource-mobile/Unity-sdk/raw/master/${PackageVersion}/UnityLevelPlay_v${PackageVersion}.unitypackage";

        // Core functionality links
        const string k_IronsourceSDKInfo = "https://s3.amazonaws.com/ssa.public/Network-Manager/IronSourceSDKInfo.json";
        const string k_IronsourceSDKXMLLinks = "https://s3.amazonaws.com/ssa.public/Network-Manager/IronSourceSDKLinks.json";
        const string k_IronsourceUnityPackageVersions = "https://s3.amazonaws.com/ssa.public/Network-Manager/PackageVersionNativeCompatibility.json";

        // Icons
        const string k_RemoteRecommendedIconDark = "https://s3.amazonaws.com/ssa.public/Network-Manager/Images/Dark_Recommended.png";
        const string k_RemoteRecommendedIconLight = "https://s3.amazonaws.com/ssa.public/Network-Manager/Images/Light_Recommended.png";

        const string k_PackageVersionMacro = "${PackageVersion}";
        const string k_IronsourceDownloadDir = "Assets/LevelPlay/Editor/";
        const string k_PackageVersionNativeCompatibility = "PackageVersionNativeCompatibility";

        List<UnityWebRequest> activeDownloads = new List<UnityWebRequest>();

        enum DownloadStatus
        {
            Success,
            Failure
        }

        public void CancelDownloads()
        {
            foreach (var download in activeDownloads)
            {
                download.Abort();
            }
            activeDownloads.Clear();
        }

        public void DownloadAdapterXML(string downloadUrl, Action<string, string> onUpdate)
        {
            IronSourceEditorCoroutines.StartEditorCoroutine(
                DownloadFile(downloadUrl,
                    (filePath, fileName, status) =>
                    {
                        if (status == DownloadStatus.Success)
                        {
                            var newVersion = IntegrationManagerDataUtils.GetVersionFromXMLFile(fileName);
                            onUpdate(newVersion, fileName);
                        }
                        else
                        {
                            if (File.Exists(filePath))
                            {
                                File.Delete(filePath);
                            }
                        }
                    }));
        }

        public async Task<(string sdkInfo, string sdkLinks, List<PackageVersionNativeCompatibility> packages)> DownloadSDKInfoData()
        {
            var sdkInfoTask = DownloadFileAsync(k_IronsourceSDKInfo);
            var sdkLinksTask = DownloadFileAsync(k_IronsourceSDKXMLLinks);
            var packagesTask = DownloadFileAsync(k_IronsourceUnityPackageVersions);

            await Task.WhenAll(sdkInfoTask, sdkLinksTask, packagesTask);

            var jsonObject = JsonConvert.DeserializeObject<JObject>(packagesTask.Result);
            var packageVersions = ((JArray)jsonObject[k_PackageVersionNativeCompatibility])?.ToObject<List<PackageVersionNativeCompatibility>>();

            return (
                sdkInfoTask.Result,
                sdkLinksTask.Result,
                packageVersions);
        }

        public void DownloadUnityPackage(string latestPackageVersion, Action onDownloadSuccess)
        {
            var downloadUrl =
                IntegrationManagerDataUtils.ReplaceValue(
                    k_IronsourceUnityPackageURL,
                    k_PackageVersionMacro,
                    latestPackageVersion,
                    latestPackageVersion);

            IronSourceEditorCoroutines.StartEditorCoroutine(
                DownloadFile(downloadUrl,
                    (path, fileName, downloadStatus) =>
                    {
                        if (downloadStatus == DownloadStatus.Failure)
                        {
                            return;
                        }

                        AssetDatabase.ImportPackage(path, true);
                        onDownloadSuccess.Invoke();
                    }));
        }

        public async Task<Texture2D> GetRecommendedIcon()
        {
            return EditorGUIUtility.isProSkin ?
                await DownloadImageAsync(k_RemoteRecommendedIconDark) :
                await DownloadImageAsync(k_RemoteRecommendedIconLight);
        }

        async Task<Texture2D> DownloadImageAsync(string url)
        {
            var unityWebRequest = UnityWebRequestTexture.GetTexture(url);
            unityWebRequest.SendWebRequest();
            activeDownloads.Add(unityWebRequest);

            while (!unityWebRequest.isDone)
            {
                await Task.Yield();
            }

            activeDownloads.Remove(unityWebRequest);
            return CheckRequestResults(unityWebRequest) ? DownloadHandlerTexture.GetContent(unityWebRequest) : null;
        }

        async Task<string> DownloadFileAsync(string url)
        {
            var unityWebRequest = UnityWebRequest.Get(url);
            unityWebRequest.SendWebRequest();
            activeDownloads.Add(unityWebRequest);

            while (!unityWebRequest.isDone)
            {
                await Task.Yield();
            }

            activeDownloads.Remove(unityWebRequest);
            return CheckRequestResults(unityWebRequest) ? unityWebRequest.downloadHandler.text : unityWebRequest.error;
        }

        IEnumerator<WaitForSeconds> DownloadFile(string downloadFileUrl,
            Action<string, string, DownloadStatus> downloadCallback)
        {
            var downloadFileName = Path.GetFileName(downloadFileUrl);
            var fileDownloading = $"Downloading {downloadFileName}";
            var genericFileName = Regex.Replace(downloadFileName, @"_v+(\d\.\d\.\d\.\d|\d\.\d\.\d)", "");
            var localDownloadPath = Path.Combine(k_IronsourceDownloadDir, genericFileName);
            var requestCancelledOrFailed = false;

            using (var downloadWebClient = new UnityWebRequest(downloadFileUrl))
            {
                activeDownloads.Add(downloadWebClient);
                downloadWebClient.downloadHandler = new DownloadHandlerFile(localDownloadPath);
                downloadWebClient.SendWebRequest();

                if (CheckRequestResults(downloadWebClient))
                {
                    while (!downloadWebClient.isDone)
                    {
                        yield return new WaitForSeconds(0.1f);

                        if (EditorUtility.DisplayCancelableProgressBar("Download Manager", fileDownloading,
                            downloadWebClient.downloadProgress))
                        {
                            if (downloadWebClient.error != null)
                            {
                                EditorServices.Instance.LevelPlayLogger.LogError(downloadWebClient.error);
                            }

                            requestCancelledOrFailed = true;
                            break;
                        }
                    }
                }
                else
                {
                    requestCancelledOrFailed = true;
                    EditorServices.Instance.LevelPlayLogger.LogError("Error Downloading " + downloadFileUrl + " : " + downloadWebClient.error);
                }

                activeDownloads.Remove(downloadWebClient);
            }

            EditorUtility.ClearProgressBar();
            downloadCallback?.Invoke(localDownloadPath, genericFileName, requestCancelledOrFailed ? DownloadStatus.Failure : DownloadStatus.Success);
        }

        static bool CheckRequestResults(UnityWebRequest unityWebRequest)
        {
#if UNITY_2020_1_OR_NEWER
            return unityWebRequest.result != UnityWebRequest.Result.ProtocolError
                && unityWebRequest.result != UnityWebRequest.Result.ConnectionError;
#else
            return !unityWebRequest.isHttpError && !unityWebRequest.isNetworkError && !unityWebRequest.isError;
#endif
        }
    }
}
#endif
