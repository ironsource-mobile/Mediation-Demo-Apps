using System;

namespace Unity.Services.LevelPlay.Editor
{
    internal interface ILevelPlayLogger
    {
        void Log(object message);
        void LogWarning(object message);
        void LogError(object message);
        void LogException(Exception exception);
        void LogVerbose(object message);
    }
}
