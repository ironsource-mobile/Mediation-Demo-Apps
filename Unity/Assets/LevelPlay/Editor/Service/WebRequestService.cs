using System;
using System.IO;
using UnityEngine.Networking;

namespace Unity.Services.LevelPlay.Editor
{
    internal class WebRequest : IWebRequest
    {
        private readonly UnityWebRequest m_Request;

        internal WebRequest(string uri)
        {
            m_Request = UnityWebRequest.Get(uri);
        }

        public bool IsDone()
        {
            return m_Request.isDone;
        }

        public void SendWebRequest()
        {
            m_Request.SendWebRequest();
        }

        public UnityWebRequest.Result Result()
        {
            return m_Request.result;
        }

        public string Error()
        {
            return m_Request.error;
        }

        public string DownloadHandlerText()
        {
            return m_Request.downloadHandler.text;
        }

        public byte[] DownloadHandlerData()
        {
            return m_Request.downloadHandler.data;
        }
    }

    internal class WebRequestService : IWebRequestService
    {
        public IWebRequest Get(string uri)
        {
            return new WebRequest(uri);
        }
    }
}
