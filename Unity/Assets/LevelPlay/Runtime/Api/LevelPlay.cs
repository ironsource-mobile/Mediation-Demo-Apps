using System;
using System.Linq;

namespace com.unity3d.mediation
{
    /// <summary>
    /// Manages initialization and basic operations of the LevelPlay SDK.
    /// This class provides methods to initialize the SDK and handles global events for initialization success and failure.
    /// </summary>
    public class LevelPlay
    {
        static event Action<LevelPlayConfiguration> InitSuccessReceived;
        static event Action<LevelPlayInitError> OnInitFailedReceived;

        /// <summary>
        /// Adds or removes event handlers for the SDK initialization success event.
        /// Ensures that the same handler cannot be added multiple times.
        /// </summary>
        public static event Action<LevelPlayConfiguration> OnInitSuccess
        {
            add
            {
                if (InitSuccessReceived == null || !InitSuccessReceived.GetInvocationList().Contains(value))
                {
                    InitSuccessReceived += value;
                }
            }

            remove
            {
                if (InitSuccessReceived != null && InitSuccessReceived.GetInvocationList().Contains(value))
                {
                    InitSuccessReceived -= value;
                }
            }
        }

        /// <summary>
        /// Adds or removes event handlers for the SDK initialization failure event.
        /// Ensures that the same handler cannot be added multiple times.
        /// </summary>
        public static event Action<LevelPlayInitError> OnInitFailed
        {
            add
            {
                if (OnInitFailedReceived == null || !OnInitFailedReceived.GetInvocationList().Contains(value))
                {
                    OnInitFailedReceived += value;
                }
            }

            remove
            {
                if (OnInitFailedReceived != null && OnInitFailedReceived.GetInvocationList().Contains(value))
                {
                    OnInitFailedReceived -= value;
                }
            }
        }

        /// <summary>
        /// Static constructor to hook up platform-specific initialization callbacks.
        /// </summary>
        static LevelPlay()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidLevelPlaySdk.OnInitSuccess += (configuration) =>
            {
                InitSuccessReceived?.Invoke(configuration);
            };
            AndroidLevelPlaySdk.OnInitFailed += (error) =>
            {
                OnInitFailedReceived?.Invoke(error);
            };
#elif UNITY_IOS && !UNITY_EDITOR
            IosLevelPlaySdk.OnInitSuccess += (configuration) =>
            {
                InitSuccessReceived?.Invoke(configuration);
            };
            IosLevelPlaySdk.OnInitFailed += (error) =>
            {
                OnInitFailedReceived?.Invoke(error);
            };
#endif
        }

        /// <summary>
        /// Initializes the LevelPlay SDK with the specified app key and optional user ID and ad format list.
        /// </summary>
        /// <param name="appKey">The application key for the SDK.</param>
        /// <param name="userId">Optional user identifier for use within the SDK.</param>
        /// <param name="adFormats">Optional array of ad formats to initialize.</param>
        public static void Init(string appKey, string userId = null, LevelPlayAdFormat[] adFormats = null)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidLevelPlaySdk.Initialize(appKey, userId, adFormats);
#elif UNITY_IOS && !UNITY_EDITOR
            IosLevelPlaySdk.Initialize(appKey, userId, adFormats);
#endif
        }

        /// <summary>
        /// When setting your PauseGame status to true, all your Unity 3D game activities will be paused (Except the ad callbacks).
        /// The game activity will be resumed automatically when the ad is closed.
        /// You should call the setPauseGame once in your session, before or after initializing the ironSource SDK,
        /// as it affects all ads (Rewarded Video and Interstitial ads) in the session.
        /// </summary>
        /// <param name="pause">Is the game paused</param>
        public static void SetPauseGame(bool pause)
        {
#if UNITY_IOS && !UNITY_EDITOR
            IosLevelPlaySdk.SetPauseGame(pause);
#endif
            IronSource.Agent.SetPauseGame(pause);
        }
    }
}
