using UnityEditor;
using UnityEngine;

namespace Unity.Services.LevelPlay.Editor.IntegrationManager.UIComponents
{
    internal class IntegrationManagerNormalText : IDrawable
    {
        readonly string m_Text;
        readonly int m_FontSize;

        internal IntegrationManagerNormalText(string text, int fontSize)
        {
            m_Text = text;
            m_FontSize = fontSize;
        }

        public void Draw()
        {
            var style = new GUIStyle(EditorStyles.label)
            {
                fontStyle = FontStyle.Normal,
                fontSize = m_FontSize,
                alignment = TextAnchor.MiddleLeft,
                margin = new RectOffset(0, 0, 0, 0),
                padding = new RectOffset(0, 0, 0, 0),
            };
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            GUILayout.Label(m_Text, style);
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
        }
    }
}
