using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Unity.Services.LevelPlay.Editor
{
    internal class LevelPlayLogger : ILevelPlayLogger
    {
        const string k_Tag = "[LevelPlay]";
        const string k_VerboseLoggingDefine = "ENABLE_UNITY_SERVICES_LEVELPLAY_VERBOSE_LOGGING";

        public void Log(object message)
        {
            Debug.unityLogger.Log(k_Tag, message);
        }

        public void LogWarning(object message)
        {
            Debug.unityLogger.LogWarning(k_Tag, message);
        }

        public void LogError(object message)
        {
            Debug.unityLogger.LogError(k_Tag, message);
        }

        public void LogException(Exception exception)
        {
            Debug.unityLogger.Log(LogType.Exception, k_Tag, exception);
        }

        public void LogVerbose(object message)
        {
            LogVerboseConditional(message);
        }

#if !ENABLE_UNITY_SERVICES_VERBOSE_LOGGING
        [Conditional(k_VerboseLoggingDefine)]
#endif
        private void LogVerboseConditional(object message)
        {
            Debug.unityLogger.Log(k_Tag, message);
        }
    }
}
