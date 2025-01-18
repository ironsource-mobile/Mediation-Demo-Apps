#if LEVELPLAY_DEPENDENCIES_INSTALLED
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Unity.Services.LevelPlay;
using Unity.Services.LevelPlay.Editor;
using Unity.Services.LevelPlay.Editor.Analytics;
using Unity.Services.LevelPlay.Editor.IntegrationManager;
using Unity.Services.LevelPlay.Editor.IntegrationManager.UIComponents;

using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

public class IronSourceDependenciesManager : EditorWindow, IObserver<bool>
{
    enum PackageType
    {
        None = 0,
        Upm = 1,
        DotUnityPackage = 2
    }

    const string k_IntegrationManagerWindowTitle = "LevelPlay Network Manager";
    const string k_LevelPlayPackageInstall = Constants.PackageName + "@${VERSION}";
    const string k_LevelPlayPackageName = Constants.PackageName;
    const string k_HeaderUpm = "Unity Package (Ads Mediation UPM Package)";
    const string k_HeaderUnityPackage = "Unity Package (Unity Plugin)";
    const string k_HeaderUpmLocal = "Unity Package (Ads Mediation UPM Package) - Local";
    const int k_Height = 768;
    internal const int k_Width = 760;

    IntegrationManagerDownloader m_IntegrationManagerDownloader;
    UnityPluginComponent m_PluginComponent;
    IDrawable m_SdkComponent;
    NetworksComponent m_NetworksComponent;
    IDrawable m_MessageDisplayComponent;

    // changeable data:
    PackageType m_PackageType = PackageType.None;

    ILevelPlayNetworkManager m_LevelPlayNetworkManager;
    IFileService m_FileService;
    ILevelPlayLogger m_Logger;
    IEditorAnalyticsService m_EditorAnalyticsService;
    Vector2 m_ScrollPosition;
    IDisposable m_LevelPlayNetworkManagerUnsubscriber;

    public static void ShowISDependenciesManager()
    {
        var window = GetWindowWithRect<IronSourceDependenciesManager>(new Rect(0, 0,
            k_Width, k_Height),
            true);
        window.titleContent = new GUIContent(k_IntegrationManagerWindowTitle);
        window.ShowUtility();
    }

    PackageType LevelPlayPackageType()
    {
        var upmPath = FilePaths.UpmPackageDirectoryPath;
        var unityPackagePath = FilePaths.UnityPackageDirectoryPath;

        if (Directory.Exists(upmPath))
        {
            return PackageType.Upm;
        }

        if (Directory.Exists(unityPackagePath))
        {
            return PackageType.DotUnityPackage;
        }

        return PackageType.None;
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }

    public virtual void OnCompleted()
    {
        this.Unsubscribe();
    }

    public virtual void OnError(Exception e)
    {
    }

    public virtual void OnNext(bool value)
    {
        SetUpUIComponents();
    }

    public virtual void Unsubscribe()
    {
        m_LevelPlayNetworkManagerUnsubscriber.Dispose();
    }

    async void OnEnable()
    {
        m_LevelPlayNetworkManager = EditorServices.Instance.LevelPlayNetworkManager;
        m_LevelPlayNetworkManagerUnsubscriber = m_LevelPlayNetworkManager.Subscribe(this);
        m_FileService = EditorServices.Instance.FileService;
        m_Logger = EditorServices.Instance.LevelPlayLogger;
        m_EditorAnalyticsService = EditorServices.Instance.EditorAnalyticsService;
        // Setup Data:
        m_IntegrationManagerDownloader = new IntegrationManagerDownloader();
        m_PackageType = LevelPlayPackageType();

        try
        {
            m_LevelPlayNetworkManager.LoadVersionsFromJson();
        }
        catch (Exception e)
        {
            m_Logger.LogError($"Failed to load versions from json with : {e.ToString()}");
        }

        SetUpUIComponents();
        try
        {
            await m_LevelPlayNetworkManager.GetVersionsWebRequest();
            try
            {
                m_LevelPlayNetworkManager.LoadVersionsFromJson();
                SetUpUIComponents();
            }
            catch (Exception e)
            {
                m_Logger.LogError($"Failed to load versions from json after updating from remote with : {e.ToString()}");
            }
        }
        catch (Exception e)
        {
            m_Logger.LogError($"Failed to update versions json file with : ${e.ToString()}");
        }
    }

    void OnGUI()
    {
        minSize = new Vector2(k_Width, k_Height);
        maxSize = new Vector2(k_Width, k_Height);
        var rootStyle = new GUIStyle()
        {
            margin = new RectOffset(0, 0, 0, 0),
            padding = new RectOffset(0, 0, 0, 0),
            normal =
            {
                background = IntegrationManagerUIUtils.CreateTexture(Theme.GetColors().BackgroundColor)
            },
            stretchHeight = true,
            stretchWidth = true,
        };
        GUILayout.BeginVertical(rootStyle);
        m_PluginComponent.Draw();
        GUILayout.Space(8);
        m_SdkComponent.Draw();
        GUILayout.Space(8);
        m_ScrollPosition = m_NetworksComponent.Draw(m_ScrollPosition);
        GUILayout.Space(8);
        m_MessageDisplayComponent.Draw();
        GUILayout.Space(8);
        GUILayout.EndVertical();
    }

    void OnDestroy()
    {
        AssetDatabase.Refresh();
        m_IntegrationManagerDownloader.CancelDownloads();
        this.Unsubscribe();
    }

    void SetUpUIComponents()
    {
        var unityPluginComponentViewModel = GetUnityPluginComponentViewModel();
        m_PluginComponent = new UnityPluginComponent(unityPluginComponentViewModel);

        var levelPlaySdkComponentViewModel = GetLevelPlaySdkComponentViewModel();
        m_SdkComponent = new LevelPlaySdkComponent(levelPlaySdkComponentViewModel);

        var networksComponentViewModel = GetNetworksComponentViewModel();
        m_NetworksComponent = new NetworksComponent(networksComponentViewModel);

        var messageDisplayComponentViewModel = GetMessageDisplayComponentViewModel();
        m_MessageDisplayComponent = new MessageDisplayComponent(messageDisplayComponentViewModel);
    }

    MessageDisplayComponent.ViewModel GetMessageDisplayComponentViewModel()
    {
        var installedSdkVersion = m_LevelPlayNetworkManager.InstalledSdkVersion();
        var message = string.Empty;
        if (installedSdkVersion != null)
        {
            message = installedSdkVersion.Message;
        }

        return new MessageDisplayComponent.ViewModel
        {
            Message = message
        };
    }

    UnityPluginComponent.ViewModel GetUnityPluginComponentViewModel()
    {
        var pluginHeaderText = string.Empty;
        Func<Task> unityPackageDownloadAction = async() => { await Task.CompletedTask; };
        switch (m_PackageType)
        {
            case PackageType.DotUnityPackage:
                pluginHeaderText = k_HeaderUnityPackage;
                unityPackageDownloadAction = async() => await DownloadUnityPackageAction();
                break;
            case PackageType.Upm:
                pluginHeaderText = k_HeaderUpm;
                unityPackageDownloadAction = async() => await DownloadUpmPackmanPackageAction();
                break;
            case PackageType.None:
            default:
                pluginHeaderText = k_HeaderUpmLocal;
                unityPackageDownloadAction = async() => { await Task.CompletedTask; };
                break;
        }
        var latestUnityPackageVersion = m_LevelPlayNetworkManager.LatestUnityPackageVersion();
        string latestVersion = null;
        if (latestUnityPackageVersion != null)
        {
            latestVersion = latestUnityPackageVersion.Version;
        }
        var unityPluginComponentViewModel = new UnityPluginComponent.ViewModel
        {
            CurrentVersion = m_LevelPlayNetworkManager.UnityPackageVersion(),
            LatestVersion = latestVersion,
            HeaderTitle = pluginHeaderText,
            OnButtonClick = unityPackageDownloadAction,
            ButtonTooltipText = GetUnityPackageButtonTooltip(latestVersion)
        };

        return unityPluginComponentViewModel;
    }

    LevelPlaySdkComponent.ViewModel GetLevelPlaySdkComponentViewModel()
    {
        IEnumerable<SdkVersion> compatibleSdkVersions = new List<SdkVersion>();
        try
        {
            compatibleSdkVersions = m_LevelPlayNetworkManager.CompatibleIronSourceSdkVersions();
        }
        catch (Exception e)
        {
            m_Logger.LogError(e.ToString());
        }

        var latestSdkVersion = compatibleSdkVersions.FirstOrDefault();
        var latestVersion = String.Empty;
        var tooltipText = String.Empty;
        if (latestSdkVersion != null)
        {
            latestVersion = latestSdkVersion.Version;
            tooltipText = this.GetSdkVersionButtonTooltip(m_LevelPlayNetworkManager.IronSourceSdk, latestSdkVersion);
        }
        var currentVersion = m_LevelPlayNetworkManager.InstalledSdkVersionString();
        var isCurrentVersionCompatible = true;
        string currentVersionString = String.Empty;
        if (currentVersion != null)
        {
            currentVersionString = currentVersion;
            isCurrentVersionCompatible = compatibleSdkVersions.Select(version => version.Version).Contains(currentVersionString);
        }

        var currentVersionViewModel = new IntegrationManagerVersion.ViewModel
        {
            Version = GetVersionDisplayText(currentVersionString),
            Icon = this.GetVersionIcon(currentVersionString, latestVersion, isCurrentVersionCompatible),
            IconTooltipText = this.GetSdkVersionIconTooltip(currentVersionString, latestVersion, isCurrentVersionCompatible),
        };
        var latestVersionViewModel = new IntegrationManagerVersion.ViewModel
        {
            Version = latestVersion,
            Icon = null,
            IconTooltipText = null,
        };

        var lineItemComponent = new LineItemComponent.ViewModel
        {
            NetworkName = m_LevelPlayNetworkManager.IronSourceSdk.DisplayName,
            CurrentVersion = currentVersionString,
            LatestVersion = latestVersion,
            IsCurrentVersionCompatible = isCurrentVersionCompatible,
            TooltipText = tooltipText,
            IsRecommended = false,
            RecommendedIconPath = m_FileService.GetPathRelativeToLevelPlayPackage(FilePaths.RecommendedIconPath),
            IsNew = false,
            NewIconPath = m_FileService.GetPathRelativeToLevelPlayPackage(FilePaths.NewIconPath),
            OnNetworkButtonClick = async() =>
            {
                await DownloadSdkAction();
            },
            CurrentVersionViewModel = currentVersionViewModel,
            LatestVersionViewModel = latestVersionViewModel,
        };

        return new LevelPlaySdkComponent.ViewModel
        {
            LineItemComponentViewModel = lineItemComponent
        };
    }

    NetworksComponent.ViewModel GetNetworksComponentViewModel()
    {
        var lineItemComponentViewModels = new List<LineItemComponent.ViewModel>();
        foreach (var adapter in m_LevelPlayNetworkManager.Adapters.Values)
        {
            try
            {
                var compatibleVersions = m_LevelPlayNetworkManager.CompatibleAdapterVersions(adapter);
                var latestAdapterVersion = compatibleVersions.First();
                var latestVersion = "";
                if (latestAdapterVersion != null)
                {
                    latestVersion = latestAdapterVersion.Version;
                }
                var currentVersion = m_LevelPlayNetworkManager.InstalledAdapterVersionString(adapter);
                string currentVersionString = null;
                var isCurrentVersionCompatible = true;
                if (currentVersion != null)
                {
                    currentVersionString = currentVersion;
                    isCurrentVersionCompatible = compatibleVersions.Select(version => version.Version).Contains(currentVersionString);
                }

                var currentVersionViewModel = new IntegrationManagerVersion.ViewModel
                {
                    Version = GetVersionDisplayText(currentVersionString),
                    Icon = this.GetVersionIcon(currentVersionString, latestVersion, isCurrentVersionCompatible),
                    IconTooltipText = this.GetAdapterVersionIconTooltip(currentVersionString, latestVersion, isCurrentVersionCompatible),
                };
                var latestVersionViewModel = new IntegrationManagerVersion.ViewModel
                {
                    Version = latestVersion,
                    Icon = null,
                    IconTooltipText = null,
                };
                var lineItemViewModel = new LineItemComponent.ViewModel
                {
                    NetworkName = adapter.DisplayName,
                    CurrentVersion = currentVersionString,
                    IsCurrentVersionCompatible = isCurrentVersionCompatible,
                    LatestVersion = latestVersion,
                    TooltipText = this.GetAdapterVersionButtonTooltip(adapter, latestAdapterVersion),
                    IsRecommended = adapter.IsRecommended,
                    RecommendedIconPath = m_FileService.GetPathRelativeToLevelPlayPackage(FilePaths.RecommendedIconPath),
                    IsNew = adapter.IsNew,
                    NewIconPath = m_FileService.GetPathRelativeToLevelPlayPackage(FilePaths.NewIconPath),
                    OnNetworkButtonClick = async() =>
                    {
                        await DownloadAdapterNetworkAction(adapter);
                    },
                    CurrentVersionViewModel = currentVersionViewModel,
                    LatestVersionViewModel = latestVersionViewModel,
                };
                lineItemComponentViewModels.Add(lineItemViewModel);
            }
            catch
            {
                // Skip this line item
            }
        }
        ;
        NetworksComponent.ViewModel viewModel;
        viewModel.LineItemComponentViewModels = lineItemComponentViewModels;
        return viewModel;
    }

    string GetPluginHeaderTitle()
    {
        switch (m_PackageType)
        {
            case PackageType.DotUnityPackage:
                return k_HeaderUnityPackage;
            case PackageType.Upm:
                return k_HeaderUpm;
            case PackageType.None:
            default:
                return string.Empty;
        }
    }

    async Task DownloadUnityPackageAction()
    {
        var currentPackageVersion = m_LevelPlayNetworkManager.UnityPackageVersion();
        var latestPackageVersion = m_LevelPlayNetworkManager.LatestUnityPackageVersion();
        if (latestPackageVersion != null && currentPackageVersion != latestPackageVersion.Version)
        {
            m_EditorAnalyticsService.SendEventEditorClick(
                EditorAnalyticsService.LevelPlayComponent.LevelPlayNetworkManager,
                EditorAnalyticsService.LevelPlayAction.ClickButton_UpdatePackage + "_DotUnityPackage");

            m_Logger.Log($"Upgrading from package version {currentPackageVersion} to {latestPackageVersion.Version}.");
            await m_LevelPlayNetworkManager.Install(latestPackageVersion);
        }

        SetUpUIComponents();
    }

    async Task DownloadUpmPackmanPackageAction()
    {
        var currentPackageVersion = m_LevelPlayNetworkManager.UnityPackageVersion();
        var latestPackageVersion = m_LevelPlayNetworkManager.LatestUnityPackageVersion();
        if (latestPackageVersion != null && currentPackageVersion != latestPackageVersion.Version)
        {
            m_EditorAnalyticsService.SendEventEditorClick(
                EditorAnalyticsService.LevelPlayComponent.LevelPlayNetworkManager,
                EditorAnalyticsService.LevelPlayAction.ClickButton_UpdatePackage + "_UPM");


            var packageVersion = k_LevelPlayPackageInstall.Replace("${VERSION}", latestPackageVersion.Version);
            m_Logger.Log($"Upgrading from package version {currentPackageVersion} to {latestPackageVersion.Version}.");
            var request = Client.Add(packageVersion);
            while (!request.IsCompleted)
            {
                await Task.Yield();
            }

            if (request.Status == StatusCode.Failure)
            {
                var message = $"Failed to install package {packageVersion} with : {request.Error}";
                m_Logger.LogError(message);
            }
        }

        SetUpUIComponents();
    }

    async Task DownloadSdkAction()
    {
        var sdkVersions = m_LevelPlayNetworkManager.CompatibleIronSourceSdkVersions();
        var sdkVersionToInstall = sdkVersions.First();
        if (sdkVersionToInstall != null)
        {
            await m_LevelPlayNetworkManager.Install(sdkVersionToInstall);
            AssetDatabase.Refresh();
        }

        SetUpUIComponents();
    }

    async Task DownloadAdapterNetworkAction(Adapter adapter)
    {
        var currentAdapterVersion = m_LevelPlayNetworkManager.InstalledAdapterVersion(adapter)?.Version;
        var adapterVersions = m_LevelPlayNetworkManager.CompatibleAdapterVersions(adapter);
        var adapterVersionToInstall = adapterVersions.First();
        if (adapterVersionToInstall != null)
        {
            if (currentAdapterVersion == null)
            {
                m_EditorAnalyticsService.SendInstallAdapterEvent(adapter.KeyName, adapterVersionToInstall.Version, currentAdapterVersion);
            }
            else
            {
                m_EditorAnalyticsService.SendUpdateAdapterEvent(adapter.KeyName, adapterVersionToInstall.Version, currentAdapterVersion);
            }

            await m_LevelPlayNetworkManager.Install(adapter, adapterVersionToInstall);
            AssetDatabase.Refresh();
        }

        SetUpUIComponents();
    }

    #nullable enable
    private string? GetAdapterVersionIconTooltip(string currentVersion, string latestVersion, bool isCurrentVersionCompatible)
    {
        if (!isCurrentVersionCompatible)
        {
            return $"This ad network adapter version is incompatible with your SDK. Update now to version {latestVersion} to ensure compatibility and avoid build failures.";
        }

        if (currentVersion != null && currentVersion != latestVersion)
        {
            return $"This network adapter version is not the latest version compatible with your SDK version. Update to version {latestVersion} for best performance.";
        }

        return null;
    }

    private string? GetSdkVersionIconTooltip(string currentVersion, string latestVersion, bool isCurrentVersionCompatible)
    {
        if (!isCurrentVersionCompatible)
        {
            return $"This SDK version is incompatible with your Unity package. Update to version {latestVersion} to ensure compatibility and avoid build failures.";
        }

        if (currentVersion != null && currentVersion != latestVersion)
        {
            return $"This SDK version is not the latest version compatible with your Unity package version. Update to version {latestVersion} for best performance.";
        }

        return null;
    }

    #nullable disable

    private string GetAdapterVersionButtonTooltip(Adapter adapter, AdapterVersion adapterVersion)
    {
        return $"Latest Version:\n" +
            $"{adapter.DisplayName} Adapter Version: {adapterVersion.Version}\n" +
            $"Android Adapter Version: {adapterVersion.AndroidVersion?.AdapterVersion}\n" +
            $"Android Ad Network SDK Version: {adapterVersion.AndroidVersion?.AdNetworkVersion}\n" +
            $"iOS Adapter Version: {adapterVersion.IosVersion?.AdapterVersion}\n" +
            $"iOS Ad Network SDK Version: {adapterVersion.IosVersion?.AdNetworkVersion}";
    }

    private string GetSdkVersionButtonTooltip(Sdk sdk, SdkVersion sdkVersion)
    {
        return $"Latest Version:\n" +
            $"{sdk.DisplayName} Version: {sdkVersion.Version}\n" +
            $"Android SDK Version: {sdkVersion.AndroidSdkVersion}\n" +
            $"iOS SDK Version: {sdkVersion.IosSdkVersion}";
    }

    private string GetUnityPackageButtonTooltip(string latestVersion)
    {
        return $"Latest Version: {latestVersion}";
    }

    #nullable enable
    private Texture2D? GetVersionIcon(string currentVersion, string latestVersion, bool isCurrentVersionCompatible)
    {
        if (!isCurrentVersionCompatible)
        {
            return GetErrorTexture();
        }

        if (currentVersion != null && currentVersion != latestVersion)
        {
            return GetInfoTexture();
        }

        return null;
    }

    private string? GetVersionDisplayText(string? version)
    {
        return string.IsNullOrEmpty(version) ? "None" : version;
    }

    #nullable disable

    private Texture2D GetErrorTexture()
    {
        string filename = m_FileService.GetPathRelativeToLevelPlayPackage(FilePaths.ErrorIconPath);
        var rawData = System.IO.File.ReadAllBytes(filename);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(rawData);
        return texture;
    }

    private Texture2D GetInfoTexture()
    {
        string filename = m_FileService.GetPathRelativeToLevelPlayPackage(FilePaths.InfoIconPath);
        var rawData = System.IO.File.ReadAllBytes(filename);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(rawData);
        return texture;
    }

    [Obsolete("ProviderInfo has been deprecated without an equivalent API.")]
    public class ProviderInfo
    {
        [Obsolete("currentStatues has been deprecated without an equivalent API.")] public Status currentStatues;
        [Obsolete("providerName has been deprecated without an equivalent API.")] public string providerName;
        [Obsolete("currentUnityVersion has been deprecated without an equivalent API.")] public string currentUnityVersion;
        [Obsolete("latestUnityVersion has been deprecated without an equivalent API.")] public string latestUnityVersion;
        [Obsolete("latestUnityAdsVersion has been deprecated without an equivalent API.")] public string latestUnityAdsVersion;
        [Obsolete("downloadURL has been deprecated without an equivalent API.")] public string downloadURL;
        [Obsolete("displayProviderName has been deprecated without an equivalent API.")] public string displayProviderName;
        [Obsolete("isNewProvider has been deprecated without an equivalent API.")] public bool isNewProvider;
        [Obsolete("fileName has been deprecated without an equivalent API.")] public string fileName;
        [Obsolete("sdkVersionDic has been deprecated without an equivalent API.")] public Dictionary<string, string> sdkVersionDic;

        [Obsolete("ProviderInfo has been deprecated without an equivalent API.")]
        public ProviderInfo()
        {
            isNewProvider = false;
            fileName = string.Empty;
            downloadURL = string.Empty;
            currentUnityVersion = IronSourceDependenciesManagerConstants.NONE;
            sdkVersionDic = new Dictionary<string, string>();
        }

        [Obsolete("Status has been deprecated without an equivalent API.")]
        public enum Status
        {
            INSTALLED = 1,
            NONE = 2,
            UPDATED = 3
        }

        [Obsolete("SetProviderDataProperties has been deprecated without an equivalent API.")]
        public bool SetProviderDataProperties(string provider, Dictionary<string, object> providerData, Dictionary<string, object> providerXML)
        {
            providerName = provider;
            object obj;

            if (providerData.TryGetValue(IronSourceDependenciesManagerConstants.PROVIDER_KEY_NAME, out obj))
            {
                displayProviderName = obj as string;
            }
            else { displayProviderName = providerName; }

            if (providerData.TryGetValue(IronSourceDependenciesManagerConstants.PROVIDER_IS_NEW, out obj))
            {
                isNewProvider = bool.Parse(obj as string);
            }

            if (providerXML.TryGetValue(IronSourceDependenciesManagerConstants.PROVIDER_DOWNLOAD_URL, out obj))
            {
                downloadURL = obj as string;
            }

            if (providerXML.TryGetValue(IronSourceDependenciesManagerConstants.PROVIDER_FILE_NAME, out obj))
            {
                fileName = obj as string;
            }

            if (providerData.TryGetValue(IronSourceDependenciesManagerConstants.PROVIDER_ANDROID_SDK_VER, out obj))
            {
                sdkVersionDic.Add(IronSourceDependenciesManagerConstants.ANDROID, obj as string);
            }

            if (providerData.TryGetValue(IronSourceDependenciesManagerConstants.PROVIDER_IOS_SDK_VER, out obj))
            {
                sdkVersionDic.Add(IronSourceDependenciesManagerConstants.IOS, obj as string);
            }

            if (providerData.TryGetValue(IronSourceDependenciesManagerConstants.PROVIDER_UNITY_ADAPTER_VERSION, out obj))
            {
                if ((providerName.ToLower() != IronSourceDependenciesManagerConstants.IRONSOURCE))
                {
                    latestUnityVersion = obj as string;
                }
                else
                {
                    latestUnityVersion = string.Empty;
                }

                downloadURL = downloadURL.Replace(IronSourceDependenciesManagerConstants.UNITY_ADAPTER_MACRO, latestUnityVersion);
            }

            if (providerData.TryGetValue(IronSourceDependenciesManagerConstants.PROVIDER_UNITY_ADAPTER_VERSION, out obj))
            {
                if ((providerName.ToLower() == IronSourceDependenciesManagerConstants.UNITYADS))
                {
                    latestUnityAdsVersion = obj as string;
                }


                downloadURL = downloadURL.Replace(IronSourceDependenciesManagerConstants.UNITY_ADAPTER_MACRO, latestUnityVersion);
            }

            currentUnityVersion = string.Empty;

            if (currentUnityVersion.Equals(IronSourceDependenciesManagerConstants.NONE))
            {
                currentStatues = Status.NONE;
            }
            else
            {
                currentStatues = Status.UPDATED;
            }

            return true;
        }
    }
}
#endif
