#if LEVELPLAY_DEPENDENCIES_INSTALLED
using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;


namespace Unity.Services.LevelPlay.Editor.IntegrationManager.UIComponents
{
    internal class UnityPluginComponent : IDrawable
    {
        internal struct ViewModel
        {
            public string CurrentVersion;
            public string LatestVersion;
            public string HeaderTitle;
            public Func<Task> OnButtonClick;
            public string ButtonTooltipText;
        }

        readonly ViewModel m_ViewModel;
        readonly IntegrationManagerButton m_Button;
        bool m_ButtonClickInProgress = false;
        readonly IntegrationManagerVersion m_Version;

        internal UnityPluginComponent(ViewModel viewModel)
        {
            m_ViewModel = viewModel;

            var versionViewModel = new IntegrationManagerVersion.ViewModel
            {
                Version = $"Installed version: {m_ViewModel.CurrentVersion}",
                Icon = null,
                IconTooltipText = null,
            };
            m_Version = new IntegrationManagerVersion(versionViewModel, 20);

            m_Button = new IntegrationManagerButton(
                GetButtonText(),
                m_ViewModel.ButtonTooltipText,
                async() => {
                    m_ButtonClickInProgress = true;
                    await m_ViewModel.OnButtonClick();
                    m_ButtonClickInProgress = false;
                });
        }

        public void Draw()
        {
            var titleStyle = new GUIStyle(EditorStyles.label)
            {
                fontStyle = FontStyle.Bold,
                fontSize = 12,
                fixedHeight = 20,
                fixedWidth = 445,
                alignment = TextAnchor.MiddleLeft,
                margin = new RectOffset(0, 0, 0, 0),
                padding = new RectOffset(0, 0, 0, 0),
            };
            var verticalBorderBoxStyle = new GUIStyle()
            {
                normal =
                {
                    background = IntegrationManagerUIUtils.CreateTexture(Theme.GetColors().BorderColor)
                },
                margin = new RectOffset(0, 0, 0, 0),
                padding = new RectOffset(0, 0, 1, 1),
                fixedHeight = 62,
            };
            var verticalStyle = new GUIStyle()
            {
                normal =
                {
                    background = IntegrationManagerUIUtils.CreateTexture(Theme.GetColors().BackgroundColor)
                },
                margin = new RectOffset(0, 0, 0, 0),
                padding = new RectOffset(8, 8, 0, 0),
            };
            var horizontalStyle = new GUIStyle()
            {
                margin = new RectOffset(0, 0, 0, 0),
                padding = new RectOffset(0, 0, 0, 0),
                fixedHeight = 20,
            };
            var infoTextStyle = new GUIStyle(EditorStyles.label)
            {
                fontStyle = FontStyle.Normal,
                fontSize = 12,
                alignment = TextAnchor.UpperLeft,
                margin = new RectOffset(0, 0, 0, 0),
                padding = new RectOffset(0, 0, 0, 0),
                stretchHeight = true,
            };
            GUILayout.BeginVertical(verticalBorderBoxStyle);
            GUILayout.BeginVertical(verticalStyle);
            GUILayout.Space(8);
            GUILayout.BeginHorizontal(horizontalStyle);
            GUILayout.Label(m_ViewModel.HeaderTitle, titleStyle);
            GUI.enabled = IsButtonEnabled();
            m_Version.Draw();
            GUILayout.FlexibleSpace();
            m_Button.Draw();
            GUI.enabled = true;
            GUILayout.EndHorizontal();
            GUILayout.Space(4);
            GUILayout.Label(GetUpgradeInfoText(), infoTextStyle);
            GUILayout.EndVertical();
            GUILayout.EndVertical();
        }

        private bool IsButtonEnabled()
        {
            if (m_ButtonClickInProgress)
            {
                return false;
            }

            if (m_ViewModel.CurrentVersion == m_ViewModel.LatestVersion)
            {
                return false;
            }

            return true;
        }

        private string GetButtonText()
        {
            if (m_ViewModel.CurrentVersion == null || m_ViewModel.CurrentVersion == string.Empty)
            {
                return "Install";
            }
            else if (m_ViewModel.CurrentVersion != m_ViewModel.LatestVersion)
            {
                return $"Update to {m_ViewModel.LatestVersion}";
            }

            return "Updated";
        }

        private string GetUpgradeInfoText()
        {
            if (m_ViewModel.CurrentVersion != null && m_ViewModel.CurrentVersion == m_ViewModel.LatestVersion)
            {
                return "Your Unity Package is up to date.";
            }
            else
            {
                return $"Upgrade the Unity Package integration for LevelPlay. " +
                    "Upgrading gives you access to the latest Network SDK versions.";
            }
        }
    }
}
#endif
