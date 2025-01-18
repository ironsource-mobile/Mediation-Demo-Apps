using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using UnityEditor;

namespace Unity.Services.LevelPlay.Editor
{
    internal class PackageInfo
    {
        public string name;
    }

    internal class PackageRegistrationEvent
    {
        public ReadOnlyCollection<PackageInfo> added;
        public ReadOnlyCollection<PackageInfo> changedTo;
        public ReadOnlyCollection<PackageInfo> changedFrom;
        public ReadOnlyCollection<PackageInfo> removed;

        public PackageRegistrationEvent(ReadOnlyCollection<PackageInfo> added, ReadOnlyCollection<PackageInfo> changedTo, ReadOnlyCollection<PackageInfo> changedFrom, ReadOnlyCollection<PackageInfo> removed)
        {
            this.added = added;
            this.changedTo = changedTo;
            this.changedFrom = changedFrom;
            this.removed = removed;
        }

        public PackageRegistrationEvent(UnityEditor.PackageManager.PackageRegistrationEventArgs packageRegistrationEventArgs)
        {
            this.added = packageRegistrationEventArgs.added.Select(packageInfo => new PackageInfo { name = packageInfo.name }).ToList().AsReadOnly();
            this.changedTo = packageRegistrationEventArgs.changedTo.Select(packageInfo => new PackageInfo { name = packageInfo.name }).ToList().AsReadOnly();
            this.changedFrom = packageRegistrationEventArgs.changedFrom.Select(packageInfo => new PackageInfo { name = packageInfo.name }).ToList().AsReadOnly();
            this.removed = packageRegistrationEventArgs.removed.Select(packageInfo => new PackageInfo { name = packageInfo.name }).ToList().AsReadOnly();
        }
    }

    internal class UpmIronSourceSdkInstaller : IIronSourceSdkInstaller
    {
        private readonly SdkInstaller m_SdkInstaller;

        internal UpmIronSourceSdkInstaller(ILevelPlayLogger logger, ILevelPlayNetworkManager levelPlayNetworkManager)
        {
            m_SdkInstaller = new SdkInstaller(logger, levelPlayNetworkManager);
        }

        public Task OnLoad()
        {
            UnityEditor.PackageManager.Events.registeredPackages -= OnRegisteredPackages;
            UnityEditor.PackageManager.Events.registeredPackages += OnRegisteredPackages;

            return Task.CompletedTask;
        }

        async void OnRegisteredPackages(UnityEditor.PackageManager.PackageRegistrationEventArgs packageRegistrationEventArgs)
        {
            var packageRegistrationEvent = new PackageRegistrationEvent(packageRegistrationEventArgs);
            await OnRegisteredPackages(packageRegistrationEvent);
        }

        // Internal for testing
        internal async Task OnRegisteredPackages(PackageRegistrationEvent packageRegistrationEvent)
        {
            var adddedLevelPlayPackages = packageRegistrationEvent.added.ToList()
                .Where(packageInfo => packageInfo.name == Constants.PackageName);
            var changedToLevelPlayPackages = packageRegistrationEvent.changedTo.ToList()
                .Where(packageInfo => packageInfo.name == Constants.PackageName);

            if (adddedLevelPlayPackages.Any() || changedToLevelPlayPackages.Any())
            {
                // Install Latest IronSource SDK
                await m_SdkInstaller.InstallLatestIronSourceSdk();
            }
        }
    }
}
