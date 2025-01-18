#if LEVELPLAY_DEPENDENCIES_INSTALLED
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UnityEditor;
using UnityEngine;


namespace Unity.Services.LevelPlay.Editor.IntegrationManager.UIComponents
{
    class NetworksComponent
    {
        internal struct ViewModel
        {
            internal List<LineItemComponent.ViewModel> LineItemComponentViewModels;
        }

        readonly List<LineItemComponent> m_LineItemComponents;
        readonly ViewModel m_ViewModel;
        Rect m_InstalledRect;
        Rect m_LatestRect;

        internal NetworksComponent(ViewModel viewModel)
        {
            m_ViewModel = viewModel;
            m_LineItemComponents = m_ViewModel.LineItemComponentViewModels.ToArray().Select(viewModel => new LineItemComponent(
                viewModel)).ToList();

            m_InstalledRect = new Rect(IronSourceDependenciesManager.k_Width / 3 + 75, 0, 200, 20);
            m_LatestRect = new Rect(IronSourceDependenciesManager.k_Width / 3 + 200, 0, 200, 20);
        }

        internal Vector2 Draw(Vector2 scrollPosition)
        {
            var titleStyle = new GUIStyle(EditorStyles.label)
            {
                fontStyle = FontStyle.Bold,
                fontSize = 12,
                fixedHeight = 20,
                stretchWidth = true,
                alignment = TextAnchor.MiddleLeft,
                margin = new RectOffset(0, 0, 0, 0),
                padding = new RectOffset(0, 0, 0, 0),
            };

            var headerHorizontalStyle = new GUIStyle()
            {
                margin = new RectOffset(0, 0, 0, 0),
                padding = new RectOffset(0, 0, 0, 0),
                fixedHeight = 24
            };
            var borderBoxStyle = new GUIStyle("box")
            {
                padding = new RectOffset(1, 1, 1, 1),
                margin = new RectOffset(0, 0, 0, 0),
                normal = { background = IntegrationManagerUIUtils.CreateTexture(Theme.GetColors().BorderColor) },
                fixedHeight = 400
            };
            var boxStyle = new GUIStyle("box")
            {
                padding = new RectOffset(0, 0, 0, 0),
                margin = new RectOffset(0, 0, 0, 0)
            };
            var scrollViewStyle = new GUIStyle()
            {
                padding = new RectOffset(0, 0, 0, 0),
                margin = new RectOffset(0, 0, 0, 0),
            };
            Vector2 newScrollPosition;
            using (new EditorGUILayout.HorizontalScope(GUILayout.ExpandWidth(false)))
            {
                GUILayout.Space(10);
                GUILayout.BeginVertical();
                GUILayout.Space(8);
                GUILayout.BeginHorizontal(headerHorizontalStyle);
                GUILayout.Label("Networks", titleStyle);
                var horizontalRect = GUILayoutUtility.GetLastRect();
                m_InstalledRect.y = horizontalRect.y;
                m_LatestRect.y = horizontalRect.y;
                EditorGUI.LabelField(m_InstalledRect, "Installed version", titleStyle);
                EditorGUI.LabelField(m_LatestRect, "Latest compatible version", titleStyle);
                GUILayout.EndHorizontal();
                GUILayout.Space(4);
                using (new EditorGUILayout.VerticalScope(borderBoxStyle, GUILayout.ExpandHeight(false)))
                {
                    using (new EditorGUILayout.VerticalScope(boxStyle, GUILayout.ExpandHeight(false)))
                    {
                        newScrollPosition = GUILayout.BeginScrollView(scrollPosition, scrollViewStyle);
                        for (int i = 0; i < m_LineItemComponents.Count; i++)
                        {
                            var lineItem = m_LineItemComponents[i];
                            lineItem.Draw(i);
                        }
                        GUILayout.EndScrollView();
                    }
                }

                GUILayout.EndVertical();
                GUILayout.Space(10);
            }

            return newScrollPosition;
        }
    }
}
#endif
