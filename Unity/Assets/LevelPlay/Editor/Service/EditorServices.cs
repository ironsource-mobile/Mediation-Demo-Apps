using System;

namespace Unity.Services.LevelPlay.Editor
{
    internal class EditorServices : IEditorServices
    {
        private static readonly EditorServices k_Instance = new EditorServices();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static EditorServices()
        {
        }

        internal EditorServices()
        {
        }

        // For Testing Only
        internal EditorServices(IPackageTypeService packageTypeService)
        {
            m_PackageTypeService = packageTypeService;
        }

        internal static EditorServices Instance
        {
            get
            {
                return k_Instance;
            }
        }

        private readonly object m_PropertyLock = new object();

        private IFileService m_FileService;
        public IFileService FileService
        {
            get
            {
                var directoryService = this.DirectoryService;
                lock (m_PropertyLock) {
                    if (m_FileService == null)
                    {
                        m_FileService = new FileService(directoryService);
                    }
                    return m_FileService;
                }
            }
        }

        private IDirectoryService m_DirectoryService;
        public IDirectoryService DirectoryService
        {
            get
            {
                lock (m_PropertyLock) {
                    if (m_DirectoryService == null)
                    {
                        m_DirectoryService = new DirectoryService();
                    }
                    return m_DirectoryService;
                }
            }
        }

        private IXmlDocumentFactory m_XmlDocumentFactory;
        public IXmlDocumentFactory XmlDocumentFactory
        {
            get
            {
                lock (m_PropertyLock) {
                    if (m_XmlDocumentFactory == null)
                    {
                        m_XmlDocumentFactory = new XmlDocumentFactory();
                    }
                    return m_XmlDocumentFactory;
                }
            }
        }

        private IWebRequestService m_WebRequestService;
        public IWebRequestService WebRequestService
        {
            get
            {
                lock (m_PropertyLock) {
                    if (m_WebRequestService == null)
                    {
                        m_WebRequestService = new WebRequestService();
                    }
                    return m_WebRequestService;
                }
            }
        }

        private ILevelPlayNetworkManager m_LevelPlayNetworkManager;
        public ILevelPlayNetworkManager LevelPlayNetworkManager
        {
            get
            {
                var fileService = this.FileService;
                var xmlDocumentFactory = this.XmlDocumentFactory;
                var webRequestService = this.WebRequestService;
                var levelPlayLogger = this.LevelPlayLogger;
                lock (m_PropertyLock) {
                    if (m_LevelPlayNetworkManager == null)
                    {
                        m_LevelPlayNetworkManager = new LevelPlayNetworkManager(fileService, xmlDocumentFactory, webRequestService, levelPlayLogger);
                    }
                    return m_LevelPlayNetworkManager;
                }
            }
        }

        private IEditorAnalyticsSender m_EditorAnalyticsSender;
        public IEditorAnalyticsSender EditorAnalyticsSender
        {
            get
            {
                lock (m_PropertyLock) {
                    if (m_EditorAnalyticsSender == null)
                    {
                        m_EditorAnalyticsSender = new EditorAnalyticsSender();
                    }
                    return m_EditorAnalyticsSender;
                }
            }
        }

        private IEditorAnalyticsService m_EditorAnalyticsService;
        public IEditorAnalyticsService EditorAnalyticsService
        {
            get
            {
                var editorAnalyticsSender = this.EditorAnalyticsSender;
                lock (m_PropertyLock) {
                    if (m_EditorAnalyticsService == null)
                    {
                        m_EditorAnalyticsService = new EditorAnalyticsService(editorAnalyticsSender);
                    }
                    return m_EditorAnalyticsService;
                }
            }
        }

        private ILevelPlayLogger m_LevelPlayLogger;
        public ILevelPlayLogger LevelPlayLogger
        {
            get
            {
                lock (m_PropertyLock) {
                    if (m_LevelPlayLogger == null)
                    {
                        m_LevelPlayLogger = new LevelPlayLogger();
                    }
                    return m_LevelPlayLogger;
                }
            }
        }

        private IOnLoadService m_OnLoadService;
        public IOnLoadService OnLoadService
        {
            get
            {
                var editorAnalyticsService = this.EditorAnalyticsService;
                var ironSourceSdkInstaller = this.IronSourceSdkInstaller;
                lock (m_PropertyLock) {
                    if (m_OnLoadService == null)
                    {
                        m_OnLoadService = new OnLoadService(editorAnalyticsService, ironSourceSdkInstaller);
                    }
                    return m_OnLoadService;
                }
            }
        }

        private IIronSourceSdkInstaller m_IronSourceSdkInstaller;
        public IIronSourceSdkInstaller IronSourceSdkInstaller
        {
            get
            {
                var packageTypeService = this.PackageTypeService;
                var logger = this.LevelPlayLogger;
                var levelPlayNetworkManager = this.LevelPlayNetworkManager;
                var fileService = this.FileService;
                var editorAnalyticsService = this.EditorAnalyticsService;
                lock (m_PropertyLock) {
                    if (m_IronSourceSdkInstaller == null)
                    {
                        switch (packageTypeService.PackageType)
                        {
                            case PackageType.Upm:
                                m_IronSourceSdkInstaller = new UpmIronSourceSdkInstaller(logger, levelPlayNetworkManager);
                                break;
                            case PackageType.DotUnityPackage:
                                m_IronSourceSdkInstaller = new DotUnityPackageIronSourceSdkInstaller(logger, levelPlayNetworkManager, fileService, editorAnalyticsService);
                                break;
                        }
                    }
                    return m_IronSourceSdkInstaller;
                }
            }
        }

        private IPackageTypeService m_PackageTypeService;
        public IPackageTypeService PackageTypeService
        {
            get
            {
                var directoryService = this.DirectoryService;
                lock (m_PropertyLock) {
                    if (m_PackageTypeService == null)
                    {
                        m_PackageTypeService = new PackageTypeService(directoryService);
                    }
                    return m_PackageTypeService;
                }
            }
        }
    }
}
