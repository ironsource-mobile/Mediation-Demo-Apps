using UnityEditor;
using UnityEngine;

namespace Unity.Services.LevelPlay.Editor.IntegrationManager.UIComponents
{
    class MessageDisplayComponent : IDrawable
    {
        internal struct ViewModel
        {
            internal string Message;
        }

        readonly ViewModel m_ViewModel;

        internal MessageDisplayComponent(ViewModel viewModel)
        {
            m_ViewModel = viewModel;
        }

        public void Draw()
        {
            var messageStyle = new GUIStyle(EditorStyles.textField)
            {
                stretchWidth = true,
                stretchHeight = true,
                wordWrap = true,
                richText = true,
                fontSize = 12,
            };
            if (!string.IsNullOrWhiteSpace(m_ViewModel.Message))
            {
                using (new EditorGUILayout.VerticalScope(GUILayout.ExpandHeight(true)))
                {
                    using (new EditorGUILayout.HorizontalScope(GUILayout.ExpandWidth(true)))
                    {
                        GUILayout.Space(10);
                        EditorGUILayout.SelectableLabel(m_ViewModel.Message, messageStyle);
                        GUILayout.Space(8);
                    }
                }
            }
        }
    }
}
