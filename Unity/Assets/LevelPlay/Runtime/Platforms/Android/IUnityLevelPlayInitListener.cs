#if UNITY_ANDROID
using UnityEngine;

namespace com.unity3d.mediation
{
    interface IUnityLevelPlayInitListener
    {
        void onInitSuccess(string configuration);
        void onInitFailed(string error);
    }
}
#endif
