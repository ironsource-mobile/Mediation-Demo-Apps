#if LEVELPLAY_DEPENDENCIES_INSTALLED
using UnityEditor;
using UnityEngine;

namespace Unity.Services.LevelPlay.Editor.IntegrationManager.UIComponents
{
    class IntegrationManagerVersion : IDrawable
    {
        internal struct ViewModel
        {
            internal string Version;
            #nullable enable
            internal Texture2D? Icon;
            internal string? IconTooltipText;
            #nullable disable
        }

        readonly ViewModel m_ViewModel;
        readonly float m_Height;

        internal IntegrationManagerVersion(ViewModel viewModel, float height)
        {
            m_Height = height;
            m_ViewModel = viewModel;
        }

        public void Draw()
        {
            var textStyle = new GUIStyle(EditorStyles.label)
            {
                fontStyle = FontStyle.Normal,
                fontSize = 10,
                alignment = TextAnchor.MiddleLeft,
                margin = new RectOffset(0, 0, 0, 0),
                padding = new RectOffset(0, 0, 0, 0),
                stretchWidth = true,
            };
            var iconStyle = new GUIStyle
            {
                margin = new RectOffset(0, 0, 0, 0),
                padding = new RectOffset(0, 0, 0, 0),
                fixedHeight = 14,
                fixedWidth = 13,
            };
            var horizontalStyle = new GUIStyle
            {
                margin = new RectOffset(0, 0, 0, 0),
                padding = new RectOffset(0, 0, 0, 0),
                fixedHeight = m_Height,
            };

            GUILayout.BeginHorizontal(horizontalStyle);
            if (m_ViewModel.Icon != null)
            {
                GUILayout.BeginVertical(new GUIStyle { fixedHeight = m_Height });
                GUILayout.FlexibleSpace();
                GUILayout.Label(
                    new GUIContent
                    {
                        image = m_ViewModel.Icon,
                        tooltip = m_ViewModel.IconTooltipText
                    },
                    iconStyle);
                GUILayout.FlexibleSpace();
                GUILayout.EndVertical();
                GUILayout.Space(4);
            }
            GUILayout.BeginVertical(new GUIStyle { fixedHeight = m_Height });
            GUILayout.FlexibleSpace();
            GUILayout.Label(m_ViewModel.Version, textStyle);
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}
#endif
