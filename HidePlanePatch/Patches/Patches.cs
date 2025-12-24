using HarmonyLib;

namespace HidePlanePatch.Patches
{
    [HarmonyPatch(typeof(HeightHidePlane), "Awake")]
    internal sealed class HeightHidePlanePatch
    {
        static void Postfix(ref HeightHidePlane __instance)
        {
            __instance.transform.localScale = new UnityEngine.Vector3(HPPPlugin.CullBoxMultiplier.Value, 1f, HPPPlugin.CullBoxMultiplier.Value);
        }
    }
}