using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.Services.LevelPlay.Editor.IntegrationManager;
using UnityEngine.Networking;
using UnityEngine;
using UnityEditor;
using Unity.Services.LevelPlay.Editor;

namespace Unity.Services.LevelPlay.Editor
{
    internal class LevelPlayNetworkManager : ILevelPlayNetworkManager
    {
        const string k_SdkVersionsUrl = "https://s3.amazonaws.com/ssa.public/Unity/Package/V1/LevelPlayVersions.json";
        const string k_LevelPlayPackageVersion = Constants.PackageVersion;
        readonly string k_VersionsJsonPath = FilePaths.LevelPlayVersionsJsonPath;
        readonly string k_PackageVersionsJsonPath = FilePaths.LevelPlayPackageVersionsJsonPath;

        internal readonly Regex RangeVersionRegex;
        internal readonly Regex SpecificVersionRegex;

        private readonly VersionComparer m_VersionComparer = new VersionComparer();
        private readonly string m_LevelPlayPackageVersion;
        private readonly IFileService m_FileService;
        private readonly IXmlDocumentFactory m_XmlDocumentFactory;
        private readonly IWebRequestService m_WebRequestService;
        private readonly ILevelPlayLogger m_LevelPlayLogger;

        public UnityPackage UnityPackage { get; private set; }
        public Dictionary<string, Adapter> Adapters { get; private set; }
        public Sdk IronSourceSdk { get; private set; }

        internal LevelPlayNetworkManager(
            IFileService fileService,
            IXmlDocumentFactory xmlDocumentFactory,
            IWebRequestService webRequestService,
            ILevelPlayLogger levelPlayLogger) : this(
            fileService,
            xmlDocumentFactory,
            webRequestService,
            levelPlayLogger,
            k_LevelPlayPackageVersion,
            new Dictionary<string, Adapter>(),
            null,
            null
            )
        {
        }

        internal LevelPlayNetworkManager(
            IFileService fileService,
            IXmlDocumentFactory xmlDocumentFactory,
            IWebRequestService webRequestService,
            ILevelPlayLogger levelPlayLogger,
            string levelPlayPackageVersion,
            Dictionary<string, Adapter> adapters,
            UnityPackage unityPackage,
            Sdk ironSourceSdk
        )
        {
            m_FileService = fileService;
            m_XmlDocumentFactory = xmlDocumentFactory;
            m_WebRequestService = webRequestService;
            m_LevelPlayLogger = levelPlayLogger;
            m_LevelPlayPackageVersion = levelPlayPackageVersion;
            Adapters = adapters;
            UnityPackage = unityPackage;
            IronSourceSdk = ironSourceSdk;
            RangeVersionRegex = new Regex(@"^([\[\]\(\)])(\S*)\s*,\s*(\S*)([\[\]\(\)])$", RegexOptions.None, TimeSpan.FromSeconds(2));
            SpecificVersionRegex = new Regex(@"^[\.0-9]+$", RegexOptions.None, TimeSpan.FromSeconds(2));
            m_Observers = new List<IObserver<bool>>();
        }

        public void UiUpdate()
        {
            foreach (var observer in m_Observers)
            {
                observer.OnNext(true);
            }
        }

        private List<IObserver<bool>> m_Observers;
        public IDisposable Subscribe(IObserver<bool> observer)
        {
            if (!m_Observers.Contains(observer))
                m_Observers.Add(observer);
            return new Unsubscriber(m_Observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<bool>> m_Observers;
            private IObserver<bool> m_Observer;

            public Unsubscriber(List<IObserver<bool>> observers, IObserver<bool> observer)
            {
                this.m_Observers = observers;
                this.m_Observer = observer;
            }

            public void Dispose()
            {
                if (m_Observer != null && m_Observers.Contains(m_Observer))
                    m_Observers.Remove(m_Observer);
            }
        }

        public void LoadVersionsFromJson()
        {
            var packageVersionsJsonPath = m_FileService.GetPathRelativeToLevelPlayPackage(k_PackageVersionsJsonPath);
            m_FileService.CreateDirectoryForFilePath(k_VersionsJsonPath);
            if (!m_FileService.Exists(k_VersionsJsonPath))
            {
                if (m_FileService.Exists(packageVersionsJsonPath))
                {
                    m_FileService.Copy(packageVersionsJsonPath, k_VersionsJsonPath, true);
                }
                else
                {
                    throw new FileNotFoundException($"File not found at {packageVersionsJsonPath}");
                }
            }

            if (!m_FileService.Exists(k_VersionsJsonPath))
            {
                throw new FileNotFoundException($"File not found at {k_VersionsJsonPath} after copying from {packageVersionsJsonPath}");
            }

            var json = m_FileService.ReadAllText(k_VersionsJsonPath);
            if (!IntegrationManagerDataUtils.DeserializeJsonToDictionary(json, out var sdkVersions))
            {
                var message = $"Failed to parse json string : {json}";
                throw new IOException(message);
            }

            try
            {
                this.ParseJson(sdkVersions);
            }
            catch (Exception e)
            {
                m_FileService.Delete(k_VersionsJsonPath);
                m_LevelPlayLogger.LogWarning($"Invalid json object from file {k_VersionsJsonPath}: with error: {e.Message}.");
            }
        }

        public async Task GetVersionsWebRequest()
        {
            var webRequest = m_WebRequestService.Get(k_SdkVersionsUrl);
            webRequest.SendWebRequest();

            while (!webRequest.IsDone())
            {
                await Task.Yield();
            }

            if (webRequest.Result() != UnityWebRequest.Result.Success)
            {
                var message = $"Failed to download {k_SdkVersionsUrl} with : {webRequest.Error()}";
                throw new IOException(message);
            }

            m_FileService.WriteAllText(k_VersionsJsonPath, webRequest.DownloadHandlerText());
        }

        private void ParseJson(Dictionary<string, object> jsonDictionary)
        {
            Dictionary<string, Adapter> newAdapters = new Dictionary<string, Adapter>();
            var jsonAdapters = jsonDictionary["adapters"] as Dictionary<string, object>;
            foreach (var adapter in jsonAdapters)
            {
                newAdapters.Add(adapter.Key, new Adapter(adapter.Key, adapter.Value as Dictionary<string, object>));
            }
            this.Adapters = newAdapters;
            this.IronSourceSdk = new Sdk("IronSourceSdk", jsonDictionary["IronSourceSdk"] as Dictionary<string, object>);
            this.UnityPackage = new UnityPackage(jsonDictionary["unityPackage"] as Dictionary<string, object>);
        }

        public string UnityPackageVersion()
        {
            return m_LevelPlayPackageVersion;
        }

        public UnityPackageVersion LatestUnityPackageVersion()
        {
            return this.GetVersionsOrderedLargestToSmallest(this.UnityPackage.Versions.Values.AsEnumerable()).First();
        }

        private Func<string, string, bool> GetStartComparisonFunction(string rangeNotation)
        {
            if (rangeNotation == "[")
            {
                return (string lowestVersion, string version) => this.m_VersionComparer.Compare(version, lowestVersion) >= 0;
            }
            else if (rangeNotation == "]" || rangeNotation == "(")
            {
                return (string lowestVersion, string version) => this.m_VersionComparer.Compare(version, lowestVersion) > 0;
            }
            else
            {
                throw new ArgumentException($"Invalid start rangeNotation : {rangeNotation}");
            }
        }

        private Func<string, string, bool> GetEndComparisonFunction(string rangeNotation)
        {
            if (rangeNotation == "]")
            {
                return (string highestVersion, string version) => this.m_VersionComparer.Compare(version, highestVersion) <= 0;
            }
            else if (rangeNotation == "[" || rangeNotation == ")")
            {
                return (string highestVersion, string version) => this.m_VersionComparer.Compare(version, highestVersion) < 0;
            }
            else
            {
                throw new ArgumentException($"Invalid end rangeNotation : {rangeNotation}");
            }
        }

        public IEnumerable<AdapterVersion> CompatibleAdapterVersions(Adapter adapter)
        {
            string installedIronSourceSdkVersion = this.InstalledSdkVersionString();
            if (installedIronSourceSdkVersion == null)
            {
                throw new IOException("IronSourceSdk is not installed.");
            }

            var compatibleAdapterVersions = adapter.Versions.Values.AsEnumerable().Where(version =>
            {
                var rangeMatches = RangeVersionRegex.Matches(version.IronSourceSdkVersion);
                var specificVersionMatches = SpecificVersionRegex.Matches(version.IronSourceSdkVersion);
                if (rangeMatches.Count == 1)
                {
                    var match = rangeMatches[0];
                    if (match.Groups.Count != 5)
                    {
                        throw new ArgumentException($"Invalid IronSourceSdkVersion({version.IronSourceSdkVersion}) in {adapter.KeyName}({version.Version}) : 5 groups expected, {match.Groups.Count} groups found.");
                    }
                    var rangeStart = match.Groups[1].Value;
                    var rangeStartVersion = match.Groups[2].Value;
                    var rangeEndVersion = match.Groups[3].Value;
                    var rangeEnd = match.Groups[4].Value;
                    var startComparisonFunction = this.GetStartComparisonFunction(rangeStart);
                    var endComparisonFunction = this.GetEndComparisonFunction(rangeEnd);
                    return startComparisonFunction(rangeStartVersion, installedIronSourceSdkVersion) && endComparisonFunction(rangeEndVersion, installedIronSourceSdkVersion);
                }
                else if (specificVersionMatches.Count == 1)
                {
                    return version.IronSourceSdkVersion == installedIronSourceSdkVersion;
                }
                else
                {
                    throw new ArgumentException($"Invalid IronSourceSdkVersion({version.IronSourceSdkVersion}) in {adapter.KeyName}({version.Version})");
                }
            }).ToList();

            return this.GetVersionsOrderedLargestToSmallest(compatibleAdapterVersions);
        }

        public IEnumerable<SdkVersion> CompatibleIronSourceSdkVersions()
        {
            if (m_LevelPlayPackageVersion == null)
            {
                return new List<SdkVersion>();
            }

            if (!this.UnityPackage.Versions.ContainsKey(m_LevelPlayPackageVersion))
            {
                return new List<SdkVersion>();
            }

            var unityPackageVersion = this.UnityPackage.Versions[m_LevelPlayPackageVersion];
            var rangeMatches = RangeVersionRegex.Matches(unityPackageVersion.IronSourceSdkVersion);
            var specificVersionMatches = SpecificVersionRegex.Matches(unityPackageVersion.IronSourceSdkVersion);
            if (rangeMatches.Count == 1)
            {
                var match = rangeMatches[0];
                if (match.Groups.Count != 5)
                {
                    throw new ArgumentException($"Invalid IronSourceSdkVersion({unityPackageVersion.IronSourceSdkVersion}) in UnityPackageVersion({unityPackageVersion.Version}) : 5 groups expected, {match.Groups.Count} groups found.");
                }
                var rangeStart = match.Groups[1].Value;
                var rangeStartVersion = match.Groups[2].Value;
                var rangeEndVersion = match.Groups[3].Value;
                var rangeEnd = match.Groups[4].Value;

                var startComparisonFunction = this.GetStartComparisonFunction(rangeStart);
                var endComparisonFunction = this.GetEndComparisonFunction(rangeEnd);

                IEnumerable<SdkVersion> versions = GetVersionsOrderedLargestToSmallest(this.IronSourceSdk.Versions.Values.AsEnumerable())
                    .Where(version =>
                {
                    var resultStart = startComparisonFunction(rangeStartVersion, version.Version);
                    var resultEnd = endComparisonFunction(rangeEndVersion, version.Version);
                    return resultStart && resultEnd;
                }).AsEnumerable();

                return versions;
            }
            else if (specificVersionMatches.Count == 1)
            {
                IronSourceSdk.Versions.TryGetValue(unityPackageVersion.IronSourceSdkVersion, out SdkVersion specificVersion);
                if (specificVersion != null)
                {
                    return new List<SdkVersion> { specificVersion };
                }
                else
                {
                    throw new ArgumentException($"Invalid IronSourceSdkVersion({unityPackageVersion.IronSourceSdkVersion}) in UnityPackageVersion({unityPackageVersion.Version}) does not exist!");
                }
            }
            else
            {
                throw new ArgumentException($"Invalid IronSourceSdkVersion({unityPackageVersion.IronSourceSdkVersion}) in UnityPackageVersion({unityPackageVersion.Version})");
            }
        }

        private bool IsInstalled(Adapter adapter)
        {
            if (adapter == null)
            {
                return false;
            }

            return m_FileService.Exists(adapter.GetDependencyXmlPath());
        }

        private bool IsInstalled(Sdk sdk)
        {
            if (sdk == null)
            {
                return false;
            }

            return m_FileService.Exists(sdk.GetDependencyXmlPath());
        }

        public string InstalledAdapterVersionString(Adapter adapter)
        {
            if (this.IsInstalled(adapter))
            {
                try
                {
                    var xmlPath = adapter.GetDependencyXmlPath();
                    IXmlDocument doc = m_XmlDocumentFactory.CreateXmlDocument();
                    doc.Load(xmlPath);
                    var unityVersion = doc.SelectSingleNode("dependencies/unityversion")?.InnerText;
                    if (unityVersion != null)
                    {
                        return unityVersion;
                    }
                    else
                    {
                        m_FileService.Delete(xmlPath);
                    }
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }

        public AdapterVersion InstalledAdapterVersion(Adapter adapter)
        {
            var installedVersion = this.InstalledAdapterVersionString(adapter);

            if (installedVersion != null && adapter.Versions.ContainsKey(installedVersion))
            {
                return adapter.Versions[installedVersion];
            }
            return null;
        }

        public string InstalledSdkVersionString()
        {
            if (this.IsInstalled(this.IronSourceSdk))
            {
                try
                {
                    var xmlPath = this.IronSourceSdk.GetDependencyXmlPath();
                    IXmlDocument doc = m_XmlDocumentFactory.CreateXmlDocument();
                    doc.Load(xmlPath);
                    string unityVersion = doc.SelectSingleNode("dependencies/unityversion")?.InnerText;
                    if (unityVersion != null)
                    {
                        return unityVersion;
                    }
                    else
                    {
                        m_FileService.Delete(xmlPath);
                    }
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }

        public SdkVersion InstalledSdkVersion()
        {
            var installedVersion = this.InstalledSdkVersionString();

            if (installedVersion != null && this.IronSourceSdk.Versions.ContainsKey(installedVersion))
            {
                return this.IronSourceSdk.Versions[installedVersion];
            }
            return null;
        }

        public async Task Install(Adapter adapter, AdapterVersion adapterVersion)
        {
            if (adapterVersion != null)
            {
                var url = adapter.DependencyXmlURL.Replace("${VERSION}", adapterVersion.Version);
                var webRequest = m_WebRequestService.Get(url);
                webRequest.SendWebRequest();

                while (!webRequest.IsDone())
                {
                    await Task.Yield();
                }

                if (webRequest.Result() != UnityWebRequest.Result.Success)
                {
                    var message = $"Failed to download {url} with : {webRequest.Error()}";
                    throw new IOException(message);
                }

                IXmlDocument doc = m_XmlDocumentFactory.CreateXmlDocument();
                doc.LoadXml(webRequest.DownloadHandlerText());
                var xmlPath = adapter.GetDependencyXmlPath();
                m_FileService.Delete(xmlPath);
                doc.Save(xmlPath);
            }
        }

        public async Task Install(UnityPackageVersion unityPackageVersion)
        {
            if (unityPackageVersion != null)
            {
                var url = this.UnityPackage.PackageURL.Replace("${VERSION}", unityPackageVersion.Version);
                var webRequest = m_WebRequestService.Get(url);
                webRequest.SendWebRequest();

                while (!webRequest.IsDone())
                {
                    await Task.Yield();
                }

                if (webRequest.Result() != UnityWebRequest.Result.Success)
                {
                    var message = $"Failed to download {url} with : {webRequest.Error()}";
                    throw new IOException(message);
                }

                var localDownloadPath = m_FileService.GetNewTempFilePath() + ".unitypackage";
                m_FileService.Delete(localDownloadPath);
                m_FileService.WriteAllBytes(localDownloadPath, webRequest.DownloadHandlerData());
                m_FileService.ImportPackage(localDownloadPath, true);
            }
        }

        public async Task Install(SdkVersion sdkVersion)
        {
            if (sdkVersion != null)
            {
                var url = this.IronSourceSdk.DependencyXmlURL.Replace("${VERSION}", sdkVersion.Version);

                var webRequest = m_WebRequestService.Get(url);
                webRequest.SendWebRequest();

                while (!webRequest.IsDone())
                {
                    await Task.Yield();
                }

                if (webRequest.Result() != UnityWebRequest.Result.Success)
                {
                    var message = $"Failed to download {url} with : {webRequest.Error()}";
                    throw new IOException(message);
                }

                IXmlDocument doc = m_XmlDocumentFactory.CreateXmlDocument();
                doc.LoadXml(webRequest.DownloadHandlerText());
                var xmlPath = this.IronSourceSdk.GetDependencyXmlPath();
                m_FileService.Delete(xmlPath);
                doc.Save(xmlPath);
            }
        }

        private IEnumerable<AdapterVersion> GetVersionsOrderedLargestToSmallest(IEnumerable<AdapterVersion> versions)
        {
            return versions.OrderByDescending(version => version.Version, this.m_VersionComparer).AsEnumerable();
        }

        private IEnumerable<UnityPackageVersion> GetVersionsOrderedLargestToSmallest(IEnumerable<UnityPackageVersion> versions)
        {
            return versions.OrderByDescending(version => version.Version, this.m_VersionComparer).AsEnumerable();
        }

        private IEnumerable<SdkVersion> GetVersionsOrderedLargestToSmallest(IEnumerable<SdkVersion> versions)
        {
            return versions.OrderByDescending(version => version.Version, this.m_VersionComparer).AsEnumerable();
        }
    }
}
