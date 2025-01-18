using System;
using System.Collections.Generic;
using UnityEditor;

namespace Unity.Services.LevelPlay.Editor
{
    class EditorAnalyticsService : IEditorAnalyticsService
    {
        const string k_EventName = "editorgameserviceeditor";
        const int k_EventVersion = 1;
        const string k_ServicesCorePackageName = "com.unity.services.core";

        private readonly IEditorAnalyticsSender m_EditorAnalyticsSender;
        private readonly string m_PackageVersion = Constants.AnnotatedPackageVersion;
        private readonly Queue<QueuedEvent> m_EventsQueue = new Queue<QueuedEvent>();
        private bool m_ServicesCoreIsReady;

        public void Initialize()
        {
            LevelPlayPackmanQuerier.instance.CheckIfPackageIsInstalledWithUpm(k_ServicesCorePackageName, coreIsInstalled =>
            {
                SetServicesCoreIsReady(coreIsInstalled);
            });
        }

        internal void SetServicesCoreIsReady(bool isReady)
        {
            m_ServicesCoreIsReady = isReady;
            if (m_ServicesCoreIsReady)
            {
                while (m_EventsQueue.Count > 0)
                {
                    var eventEntry = m_EventsQueue.Dequeue();
                    SendEventWithBody(eventEntry.Name, eventEntry.Body);
                }
            }
        }

        internal EditorAnalyticsService(IEditorAnalyticsSender editorAnalyticsSender)
        {
            m_EditorAnalyticsSender = editorAnalyticsSender;
        }

        public void SendEventEditorClick(string component, string action)
        {
            SendEvent(k_EventName,
                new EventBody()
                {
                    component = component,
                    action = action,
                    package = Constants.PackageAnalyticsIdentifier,
                    package_ver = m_PackageVersion
                });
        }

        public void SendInstallAdapterEvent(string adapterName,
            string newVersion, string currentVersion)
        {
            SendEvent(k_EventName,
                new EventBody()
                {
                    component = LevelPlayComponent.LevelPlayNetworkManager,
                    action = LevelPlayAction.Install + "_" + adapterName.Replace("_", "-") + "_" + newVersion,
                    package = Constants.PackageAnalyticsIdentifier,
                    package_ver = m_PackageVersion,
                });
        }

        public void SendUpdateAdapterEvent(string adapterName,
            string newVersion, string currentVersion)
        {
            SendEvent(k_EventName,
                new EventBody()
                {
                    component = LevelPlayComponent.LevelPlayNetworkManager,
                    action = LevelPlayAction.Update + "_" + adapterName.Replace("_", "-") + "_" + newVersion,
                    package = Constants.PackageAnalyticsIdentifier,
                    package_ver = m_PackageVersion,
                });
        }

        public void SendNewSession(string packageType)
        {
            SendEvent(k_EventName,
                new EventBody
                {
                    component = packageType,
                    action = LevelPlayAction.NewSession,
                    package = Constants.PackageAnalyticsIdentifier,
                    package_ver = m_PackageVersion
                });
        }

        public void SendInstallPackage(string component)
        {
            SendEvent(k_EventName,
                new EventBody
                {
                    action = LevelPlayAction.Install,
                    component = component,
                    package = Constants.PackageAnalyticsIdentifier,
                    package_ver = m_PackageVersion
                });
        }

        private void SendEvent(string eventName, EventBody body)
        {
            if (!m_ServicesCoreIsReady)
            {
                m_EventsQueue.Enqueue(new QueuedEvent { Name = eventName, Body = body, });
            }
            else
            {
                SendEventWithBody(eventName, body);
            }
        }

        public void SendEventWithBody(string eventName, object body)
        {
            m_EditorAnalyticsSender.SendEventWithLimit(eventName, body, k_EventVersion);
        }

        internal static class LevelPlayComponent
        {
            public const string TopMenuAdsMediation = "TopMenu_AdsMediation";
            public const string TopMenuDeveloperSettings = "TopMenu_DeveloperSettings";
            public const string LevelPlayNetworkManager = "LevelPlay_Network_Manager";

            public const string UpmPackage = "upm";
            public const string UnityPackage = ".unitypackage";
        }

        internal static class LevelPlayAction
        {
            public const string OpenChangelog = "Open_SDKChangelog";
            public const string OpenLevelPlayMediationSettings = "Open_LevelPlayMediationSettings";
            public const string OpenMediatedNetworkSettings = "Open_MediatedNetworkSettings";
            public const string OpenNetworkManager = "Open_NetworkManager";
            public const string OpenDocumentation = "Open_Documentation";
            public const string OpenTroubleShootingGuide = "Open_TroubleShootingGuide";

            public const string ClickButton_UpdatePackage = "ClickButton_UpdatePackage";

            public const string NewSession = "NewSession";
            public const string Install = "Install";
            public const string Update = "Update";
        }
    }

    internal class QueuedEvent
    {
        internal string Name;
        internal object Body;
    }

    [Serializable]
    internal class EventBody
    {
        public string action;
        public string component;
        public string package;
        public string package_ver;
    }

    internal class EventBodyComparer : IEqualityComparer<EventBody>
    {
        public bool Equals(EventBody one, EventBody two)
        {
            if (object.ReferenceEquals(one, two))
            {
                return true;
            }

            if (one == null || two == null)
            {
                return false;
            }

            if (!one.action.Equals(two.action))
            {
                return false;
            }

            if (!one.component.Equals(two.component))
            {
                return false;
            }

            if (!one.package.Equals(two.package))
            {
                return false;
            }

            if (!one.package_ver.Equals(two.package_ver))
            {
                return false;
            }

            return true;
        }

        public int GetHashCode(EventBody eventBody)
        {
            return eventBody.action.GetHashCode() ^ eventBody.component.GetHashCode() ^ eventBody.package.GetHashCode() ^ eventBody.package_ver.GetHashCode();
        }
    }
}
