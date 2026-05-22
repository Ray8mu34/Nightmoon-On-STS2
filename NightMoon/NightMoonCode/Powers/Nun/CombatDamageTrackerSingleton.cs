using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Powers.Nun;

public class CombatDamageTrackerSingleton() : CustomSingletonModel(true, false)
{
    private int currentTurnDamage;

    public override Task BeforeCombatStart()
    {
        currentTurnDamage = 0;
        DamageTracker.Clear();
        return Task.CompletedTask;
    }

    public override Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        DamageTracker.SetLastTurnDamage(player.Creature, currentTurnDamage);
        currentTurnDamage = 0;
        return Task.CompletedTask;
    }

    public override Task AfterDamageGiven(
        PlayerChoiceContext choiceContext,
        Creature? dealer,
        DamageResult result,
        ValueProp props,
        Creature target,
        CardModel? cardSource)
    {
        if (dealer == null || result.UnblockedDamage <= 0)
            return Task.CompletedTask;

        if (dealer.IsPlayer || dealer.PetOwner != null)
        {
            var damage = (int)result.UnblockedDamage;
            currentTurnDamage += damage;

            var playerCreature = dealer.IsPlayer ? dealer : dealer.PetOwner?.Creature;
            if (playerCreature != null)
                DamageTracker.AddCombatDamage(playerCreature, damage);
        }

        return Task.CompletedTask;
    }
}
