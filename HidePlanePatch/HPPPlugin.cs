using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using PluginUtilities;
using BepInEx.Logging;
using ModdingTales;

namespace HidePlanePatch
{
    [BepInPlugin(Guid, Name, Version)]
    [BepInDependency(SetInjectionFlag.Guid)]
    public sealed class HPPPlugin : BaseUnityPlugin
    {
        // constants
        public const string Guid = "org.HF.plugins.HPP";
        public const string Version = "1.1.0.0";
        private const string Name = "Hide Plane Patch";

        internal static Harmony harmony;

        internal static ManualLogSource _logger;

        internal static ConfigEntry<float> CullBoxMultiplier;

        public static void DoPatching()
        {
            harmony = new Harmony(Guid);
            harmony.PatchAll();
            _logger.LogInfo($"{Name}: Patched.");
        }

        private static void DoConfig(ConfigFile config)
        {
            CullBoxMultiplier = config.Bind("CullBox", "Multiplier", 100f);
        }

        private void Awake()
        {
            _logger = Logger;
            
            DoConfig(Config);
            DoPatching();
            _logger.LogInfo($"{Name} is Active.");

            ModdingUtils.AddPluginToMenuList(this);
        }

    }
}