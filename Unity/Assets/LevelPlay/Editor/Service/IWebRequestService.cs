using System;
using UnityEngine.Networking;

namespace Unity.Services.LevelPlay.Editor
{
    internal interface IWebRequest
    {
        bool IsDone();
        void SendWebRequest();
        UnityWebRequest.Result Result();
        string Error();
        string DownloadHandlerText();
        byte[] DownloadHandlerData();
    }

    internal interface IWebRequestService
    {
        IWebRequest Get(string uri);
    }
}
