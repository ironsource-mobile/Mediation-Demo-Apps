#if UNITY_2019_1_OR_NEWER && UNITY_EDITOR && LEVELPLAY_DEPENDENCIES_INSTALLED
using Unity.Services.Core.Editor;

namespace Unity.Services.LevelPlay.Editor
{
    class LevelPlayService : IEditorGameService
    {
        public string GetFormattedDashboardUrl()
        {
            return null;
        }

        public string Name { get; }

        public IEditorGameServiceIdentifier Identifier { get; } = new LevelPlayIdentifier();

        public bool RequiresCoppaCompliance => true;
        public bool HasDashboard => false;
        public IEditorGameServiceEnabler Enabler { get; }
    }
}
#endif
