using System;
using UnityEditor;

namespace Unity.Services.LevelPlay.Editor
{
    internal class EditorAnalyticsSender : IEditorAnalyticsSender
    {
        public void SendEventWithLimit(string eventName, object body, int version)
        {
            try
            {
                EditorAnalytics.SendEventWithLimit(eventName, body, version);
            }
            catch (Exception)
            {
                // Silent catch because error in analytics shouldn't prevent users from executing their action
            }
        }
    }
}
