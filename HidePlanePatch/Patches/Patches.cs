using HarmonyLib;

namespace HidePlanePatch.Patches
{
    [HarmonyPatch(typeof(HeightHidePlane), "Awake")]
    internal sealed class HeightHidePlanePatch
    {
        internal static HeightHidePlane HeightHidePlane;
        static void Postfix(ref HeightHidePlane __instance)
        {
            HeightHidePlane = __instance;
            HPPPlugin.SetHHPSize();
        }
    }
}