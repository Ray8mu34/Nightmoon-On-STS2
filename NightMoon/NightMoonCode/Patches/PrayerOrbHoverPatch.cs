using HarmonyLib;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Patches;

[HarmonyPatch(typeof(OrbModel), "HoverTips", MethodType.Getter)]
public static class PrayerOrbHoverPatch
{
    public static bool Prefix(OrbModel __instance, ref IEnumerable<IHoverTip> __result)
    {
        if (__instance is not NunPrayerCountdownOrb prayerOrb)
            return true;

        __result = prayerOrb.PrayerHoverTips;
        return false;
    }
}
