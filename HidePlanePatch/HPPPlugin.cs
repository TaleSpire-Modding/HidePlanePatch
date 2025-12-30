using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using PluginUtilities;
using BepInEx.Logging;
using HidePlanePatch.Patches;

namespace HidePlanePatch
{
    [BepInPlugin(Guid, Name, Version)]
    [BepInDependency(SetInjectionFlag.Guid)]
    public sealed class HPPPlugin : DependencyUnityPlugin
    {
        // constants
        public const string Guid = "org.HF.plugins.HPP";
        public const string Version = "0.0.0.0";
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
            CullBoxMultiplier = config.Bind("CullBox", "Size", 100f, new ConfigDescription("", null, new ConfigurationAttributes
            {
                CallbackAction = (object o) => { SetHHPSize(); },
                DispName = "Cull Box Size"
            }));
        }

        protected override void OnAwake()
        {
            _logger = Logger;
            
            DoConfig(Config);
            DoPatching();
            _logger.LogInfo($"{Name} is Active.");

            if (HeightHidePlanePatch.HeightHidePlane == null)
            {
                HeightHidePlanePatch.HeightHidePlane = FindAnyObjectByType<HeightHidePlane>();
            }
            SetHHPSize();
        }

        internal static void SetHHPSize()
        {
            if (HeightHidePlanePatch.HeightHidePlane != null)
            {
                HeightHidePlanePatch.HeightHidePlane.transform.localScale = new UnityEngine.Vector3(CullBoxMultiplier.Value, 1f, CullBoxMultiplier.Value);
            }
        }

        protected override void OnDestroyed()
        {
            // Reset the Hide Plane Scale
            if (HeightHidePlanePatch.HeightHidePlane != null)
            {
                HeightHidePlanePatch.HeightHidePlane.transform.localScale = new UnityEngine.Vector3(1f, 1f, 1f);
                HeightHidePlanePatch.HeightHidePlane = null;
            }

            // Unpatch Harmony Patches
            harmony.UnpatchSelf();

            // unbind static variables
            CullBoxMultiplier = null;
            _logger = null;
            harmony = null;

            Logger.LogDebug($"{Name} unloaded");
        }
    }
}