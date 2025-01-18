#if LEVELPLAY_DEPENDENCIES_INSTALLED
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;


namespace Unity.Services.LevelPlay.Editor.IntegrationManager.UIComponents
{
    internal class LevelPlaySdkComponent : IDrawable
    {
        internal struct ViewModel
        {
            internal LineItemComponent.ViewModel LineItemComponentViewModel;
        }

        readonly ViewModel m_ViewModel;
        readonly LineItemComponent m_SdkLineItemComponent;
        const int k_InfoTextFontSize = 11;
        private Rect m_InstalledRect;
        private Rect m_LatestRect;

        internal LevelPlaySdkComponent(ViewModel viewModel)
        {
            m_ViewModel = viewModel;
            m_ViewModel.LineItemComponentViewModel.NetworkName = "LevelPlay Mediation";
            m_SdkLineItemComponent = new LineItemComponent(m_ViewModel.LineItemComponentViewModel);
            m_InstalledRect = new Rect(IronSourceDependenciesManager.k_Width / 3 + 75, 0, 200, 20);
            m_LatestRect = new Rect(IronSourceDependenciesManager.k_Width / 3 + 200, 0, 200, 20);
        }

        public void Draw()
        {
            var rootStyle = new GUIStyle()
            {
                margin = new RectOffset(0, 0, 0, 0),
                padding = new RectOffset(0, 0, 0, 0),
            };
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
            var verticalBorderBoxStyle = new GUIStyle("box")
            {
                normal =
                {
                    background = IntegrationManagerUIUtils.CreateTexture(Theme.GetColors().BorderColor)
                },
                margin = new RectOffset(0, 0, 0, 0),
                padding = new RectOffset(1, 1, 1, 1),
                stretchHeight = false,
            };
            var verticalBoxStyle = new GUIStyle("box")
            {
                normal =
                {
                    background = IntegrationManagerUIUtils.CreateTexture(Theme.GetColors().RowColor1)
                },
                margin = new RectOffset(0, 0, 0, 0),
                padding = new RectOffset(0, 0, 0, 0),
                stretchHeight = false,
            };
            var infoTextStyle = new GUIStyle(EditorStyles.label)
            {
                fontStyle = FontStyle.Normal,
                fontSize = 12,
                alignment = TextAnchor.MiddleLeft,
                fixedHeight = 14,
                margin = new RectOffset(0, 0, 0, 0),
                padding = new RectOffset(0, 0, 0, 0),
            };
            var ironSourceTextStyle = new GUIStyle(EditorStyles.label)
            {
                fontStyle = FontStyle.Normal,
                fontSize = 12,
                alignment = TextAnchor.MiddleLeft,
                fixedHeight = 24,
                margin = new RectOffset(0, 0, 0, 0),
                padding = new RectOffset(4, 4, 0, 0),
            };
            GUILayout.BeginHorizontal(rootStyle);
            GUILayout.Space(10);
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal(headerHorizontalStyle);
            GUILayout.Label("ironSource SDK", titleStyle);
            var horizontalRect = GUILayoutUtility.GetLastRect();
            m_InstalledRect.y = horizontalRect.y;
            m_LatestRect.y = horizontalRect.y;
            EditorGUI.LabelField(m_InstalledRect, "Installed version", titleStyle);
            EditorGUI.LabelField(m_LatestRect, "Latest compatible version", titleStyle);
            GUILayout.EndHorizontal();
            GUILayout.Label("A bundled SDK that includes LevelPlay Mediation " +
                "and the ironSource Ad Network.", infoTextStyle);
            GUILayout.Space(8);
            GUILayout.BeginVertical(verticalBorderBoxStyle);
            GUILayout.BeginVertical(verticalBoxStyle);
            m_SdkLineItemComponent.Draw();
            GUI.enabled = m_ViewModel.LineItemComponentViewModel.CurrentVersion != m_ViewModel.LineItemComponentViewModel.LatestVersion;
            GUILayout.Label("ironSource Ads", ironSourceTextStyle);
            GUI.enabled = true;
            GUILayout.EndVertical();
            GUILayout.EndVertical();

            GUILayout.EndVertical();
            GUILayout.Space(10);
            GUILayout.EndHorizontal();
        }
    }
}
#endif
