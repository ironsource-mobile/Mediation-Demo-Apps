using System;

namespace Unity.Services.LevelPlay.Editor
{
    #nullable enable
    internal interface IDirectoryService
    {
        bool Exists(string? path);
    }
    #nullable disable
}
