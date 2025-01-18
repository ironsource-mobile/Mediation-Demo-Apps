#if UNITY_2019_1_OR_NEWER && UNITY_EDITOR && LEVELPLAY_DEPENDENCIES_INSTALLED
using Unity.Services.Core.Editor;

namespace Unity.Services.LevelPlay.Editor
{
    class LevelPlayGameServiceEnabler : EditorGameServiceFlagEnabler
    {
        static bool s_Enabled;

        protected override string FlagName { get; } = "levelplay";

        protected override void EnableLocalSettings()
        {
            s_Enabled = true;
        }

        protected override void DisableLocalSettings()
        {
            s_Enabled = false;
        }

        public override bool IsEnabled()
        {
            return  s_Enabled;
        }
    }
}
#endif
