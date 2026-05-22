using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.ValueProps;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Powers.Nun;

public class NunJusticeScalePower() : NunPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player.Creature != Owner)
            return;

        var combatState = Owner.CombatState;
        var enemies = combatState?.HittableEnemies.ToList();
        if (enemies == null || enemies.Count == 0 || Amount <= 0)
            return;

        var combatDamage = DamageTracker.GetCombatDamage(Owner);
        var damage = Math.Max(0m, combatDamage * Amount / 100m);
        if (damage <= 0)
            return;

        var index = Owner.Player?.RunState?.Rng.Niche.NextInt(enemies.Count) ?? Rng.Chaotic.NextInt(enemies.Count);
        var target = enemies[index];

        Flash();
        await CreatureCmd.Damage(choiceContext, target, damage, ValueProp.Move, Owner, null);
    }
}
