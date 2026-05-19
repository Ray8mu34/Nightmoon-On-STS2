using HarmonyLib;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Cards;

namespace NightMoon.NightMoonCode.Patches;

[HarmonyPatch]
public static class CardDynamicVarRefreshPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(Creature), "ApplyPowerInternal")]
    private static void AfterPowerAdded(Creature __instance, PowerModel power)
    {
        RefreshVisibleCards(__instance);
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(Creature), nameof(Creature.InvokePowerModified))]
    private static void AfterPowerModified(Creature __instance, PowerModel power, int change, bool silent)
    {
        RefreshVisibleCards(__instance);
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(Creature), nameof(Creature.RemovePowerInternal))]
    private static void AfterPowerRemoved(Creature __instance, PowerModel power)
    {
        RefreshVisibleCards(__instance);
    }

    private static void RefreshVisibleCards(Creature creature)
    {
        var playerCombatState = creature.Player?.PlayerCombatState;
        if (!CombatManager.Instance.IsInProgress || !creature.IsPlayer || playerCombatState == null)
            return;

        foreach (var card in playerCombatState.AllCards)
        {
            var node = NCard.FindOnTable(card);
            if (node != null)
                node.UpdateVisuals(node.DisplayingPile, CardPreviewMode.Normal);
        }
    }
}
