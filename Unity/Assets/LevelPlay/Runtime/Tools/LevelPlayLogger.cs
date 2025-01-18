using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Unity.Services.LevelPlay
{
    static class LevelPlayLogger
    {
        const string k_Tag = "[LevelPlay]";
        const string k_VerboseLoggingDefine = "ENABLE_UNITY_SERVICES_LEVELPLAY_VERBOSE_LOGGING";
        const string k_UnityAssertions = "UNITY_ASSERTIONS";

        /// <summary>
        /// Logs a message in the console
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void Log(object message) => Debug.unityLogger.Log(k_Tag, message);

        /// <summary>
        /// Logs a warning in the console
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void LogWarning(object message) => Debug.unityLogger.LogWarning(k_Tag, message);

        /// <summary>
        /// Logs an error in the console
        /// <param name="message">Message to log</param>
        /// </summary>
         public static void LogError(object message) => Debug.unityLogger.LogError(k_Tag, message);

        /// <summary>
        /// Logs an exception in the console
        /// <param name="exception">Exception to log</param>
        /// </summary>
        public static void LogException(Exception exception) => Debug.unityLogger.Log(LogType.Exception, k_Tag, exception);

        /// <summary>
        /// Logs an assertion in the console (only available when the define is enabled)
        /// <param name="message">Message to log</param>
        /// </summary>
        [Conditional(k_UnityAssertions)]
        public static void LogAssertion(object message) => Debug.unityLogger.Log(LogType.Assert, k_Tag, message);

        /// <summary>
        /// Logs a verbose log in the console if verbose logging is activated
        /// <param name="message">Message to log</param>
        /// </summary>
#if !ENABLE_UNITY_SERVICES_VERBOSE_LOGGING
        [Conditional(k_VerboseLoggingDefine)]
#endif
        public static void LogVerbose(object message) => Debug.unityLogger.Log(k_Tag, message);
    }
}
