using System;
using System.Threading.Tasks;

namespace Unity.Services.LevelPlay.Editor
{
    enum PackageType
    {
        Upm = 0,
        DotUnityPackage = 1
    }

    internal interface IPackageTypeService
    {
        PackageType PackageType { get; }
    }
}
