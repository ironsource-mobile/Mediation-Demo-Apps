namespace Unity.Services.LevelPlay.Editor
{
    internal interface IEditorAnalyticsSender
    {
        void SendEventWithLimit(string eventName, object body, int version);
    }
}
