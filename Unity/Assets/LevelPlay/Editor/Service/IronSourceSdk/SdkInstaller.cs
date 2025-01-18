using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Unity.Services.LevelPlay.Editor
{
    class SdkInstaller
    {
        private readonly ILevelPlayLogger m_Logger;
        private readonly ILevelPlayNetworkManager m_LevelPlayNetworkManager;

        internal SdkInstaller(ILevelPlayLogger logger, ILevelPlayNetworkManager levelPlayNetworkManager)
        {
            m_Logger = logger;
            m_LevelPlayNetworkManager = levelPlayNetworkManager;
        }

        internal async Task InstallLatestIronSourceSdk()
        {
            try
            {
                m_LevelPlayNetworkManager.LoadVersionsFromJson();
            }
            catch (Exception e)
            {
                m_Logger.LogError($"Failed to load versions json : {e.ToString()}");
            }
            try
            {
                await m_LevelPlayNetworkManager.GetVersionsWebRequest();
            }
            catch (Exception e)
            {
                m_Logger.LogError($"Failed to fetch versions json : {e.ToString()}");
            }
            try
            {
                m_LevelPlayNetworkManager.LoadVersionsFromJson();
            }
            catch (Exception e)
            {
                m_Logger.LogError($"Failed to load versions json after fetching from remote : {e.ToString()}");
            }
            try
            {
                var latestIronSourceSdkVersion = m_LevelPlayNetworkManager.CompatibleIronSourceSdkVersions().FirstOrDefault();
                if (latestIronSourceSdkVersion != null)
                {
                    await m_LevelPlayNetworkManager.Install(latestIronSourceSdkVersion);
                    AssetDatabase.Refresh();
                    m_LevelPlayNetworkManager.UiUpdate();
                }
            }
            catch (Exception e)
            {
                m_Logger.LogError($"Failed to install IronSource SDK with exception : {e.ToString()}");
            }
        }
    }
}
