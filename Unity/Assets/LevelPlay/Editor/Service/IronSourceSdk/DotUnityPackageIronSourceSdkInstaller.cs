using System;
using System.Threading.Tasks;

namespace Unity.Services.LevelPlay.Editor
{
    internal class DotUnityPackageIronSourceSdkInstaller : IIronSourceSdkInstaller
    {
        private readonly IEditorAnalyticsService m_EditorAnalyticsService;
        private readonly IFileService m_FileService;
        private readonly SdkInstaller m_SdkInstaller;
        private bool m_IsAdsMediationBuilderProject;

        internal DotUnityPackageIronSourceSdkInstaller(
            ILevelPlayLogger logger,
            ILevelPlayNetworkManager levelPlayNetworkManager,
            IFileService fileService,
            IEditorAnalyticsService editorAnalyticsService
        ) : this(
            logger,
            levelPlayNetworkManager,
            fileService,
            editorAnalyticsService,
#if AdsMediation_BuilderProject
            true
#else
            false
#endif
        )
        {}

        internal DotUnityPackageIronSourceSdkInstaller(
            ILevelPlayLogger logger,
            ILevelPlayNetworkManager levelPlayNetworkManager,
            IFileService fileService,
            IEditorAnalyticsService editorAnalyticsService,
            bool isAdsMediationBuilderProject
        )
        {
            m_EditorAnalyticsService = editorAnalyticsService;
            m_FileService = fileService;
            m_SdkInstaller = new SdkInstaller(logger, levelPlayNetworkManager);
            m_IsAdsMediationBuilderProject = isAdsMediationBuilderProject;
        }

        public async Task OnLoad()
        {
            if (!m_IsAdsMediationBuilderProject)
            {
                if (m_FileService.Exists(FilePaths.DotUnityPackageFootprintFilePath))
                {
                    m_FileService.Delete(FilePaths.DotUnityPackageFootprintFilePath);
                    m_FileService.Delete(FilePaths.DotUnityPackageFootprintMetaFilePath);
                    SendInstallAnalyticsEvent();
                    await m_SdkInstaller.InstallLatestIronSourceSdk();
                }
            }
        }

        void SendInstallAnalyticsEvent()
        {
            m_EditorAnalyticsService.SendInstallPackage(EditorAnalyticsService.LevelPlayComponent.UnityPackage);
        }
    }
}
