using HarmonyLib;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Patches;

[HarmonyPatch]
public static class PrayerCombatCleanupPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(CombatManager), nameof(CombatManager.EndCombatInternal))]
    public static void AfterCombatEnd()
    {
        PrayerManager.ClearAll();
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(NCombatRoom), nameof(NCombatRoom._ExitTree))]
    public static void AfterCombatRoomExit()
    {
        PrayerManager.ClearAll();
    }
}
