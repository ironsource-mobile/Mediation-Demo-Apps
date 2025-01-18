using JetBrains.Annotations;
using UnityEngine;

namespace Unity.Services.LevelPlay.Editor.IntegrationManager.UIComponents
{
    class IntegrationManagerRecommendedBox : IDrawable
    {
        readonly Texture2D m_Image;
        readonly int m_Width;
        readonly int m_Height;
        readonly int m_RowHeight;

        internal IntegrationManagerRecommendedBox([CanBeNull] Texture2D recommendedIcon, int width, int height, int rowHeight)
        {
            m_Image = recommendedIcon;
            m_Width = width;
            m_Height = height;
            m_RowHeight = rowHeight;
        }

        public void Draw()
        {
            var style = new GUIStyle
            {
                margin = new RectOffset(0, 0, 0, 0),
                padding = new RectOffset(0, 0, 0, 0),
                fixedWidth = m_Width,
                fixedHeight = m_Height,
            };
            GUILayout.Space(8);
            GUILayout.BeginVertical(new GUIStyle { fixedHeight = m_RowHeight });
            GUILayout.FlexibleSpace();
            GUILayout.Label(m_Image, style);
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
        }
    }
}
