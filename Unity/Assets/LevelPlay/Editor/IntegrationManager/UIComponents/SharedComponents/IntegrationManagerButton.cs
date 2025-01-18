using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Unity.Services.LevelPlay.Editor.IntegrationManager.UIComponents
{
    internal class IntegrationManagerButton : IDrawable
    {
        readonly Func<Task> m_OnClick;
        readonly GUIContent m_GuiContent;

        internal IntegrationManagerButton(
            string buttonText,
            string tooltip,
            Func<Task> onClick)
        {
            m_OnClick = onClick;
            m_GuiContent = new GUIContent
            {
                text = buttonText,
                tooltip = tooltip
            };
        }

        public void Draw()
        {
            GUILayout.BeginVertical();
            var buttonStyle = GUI.skin.button;
            buttonStyle.fontSize = 12;
            buttonStyle.margin = new RectOffset(0, 0, 0, 0);
            buttonStyle.alignment = TextAnchor.MiddleCenter;

            GUILayout.FlexibleSpace();
            var btn = GUILayout.Button(m_GuiContent, buttonStyle,
                GUILayout.ExpandWidth(true), GUILayout.MinWidth(72), GUILayout.Height(20));
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();

            if (btn)
            {
                m_OnClick.Invoke();
            }
        }
    }
}
