using System;

namespace Unity.Services.LevelPlay.Editor
{
    internal class PackageTypeService : IPackageTypeService
    {
        private IDirectoryService m_DirectoryService;
        internal PackageTypeService(IDirectoryService directoryService)
        {
            m_DirectoryService = directoryService;
            m_PackageType = GetPackageTypeFromDirectory();
        }

        private PackageType m_PackageType = PackageType.Upm;
        public PackageType PackageType
        {
            get
            {
                return m_PackageType;
            }
        }

        private PackageType GetPackageTypeFromDirectory()
        {
            var upmPath = FilePaths.UpmPackageDirectoryPath;
            var unityPackagePath = FilePaths.UnityPackageDirectoryPath;

            if (m_DirectoryService.Exists(upmPath))
            {
                return PackageType.Upm;
            }

            if (m_DirectoryService.Exists(unityPackagePath))
            {
                return PackageType.DotUnityPackage;
            }

            return PackageType.Upm;
        }
    }
}
