using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.ValueProps;

namespace NightMoon.NightMoonCode.Powers.Nun;

public class NunDemonBladePower() : NunPower
{
    private decimal divisor = 5m;

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public void SetDivisor(decimal value)
    {
        if (value > 0)
            divisor = Math.Min(divisor, value);
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player.Creature != Owner)
            return;

        var combatState = Owner.CombatState;
        var enemies = combatState?.HittableEnemies.ToList();
        if (enemies == null || enemies.Count == 0)
            return;

        var index = Owner.Player?.RunState?.Rng.Niche.NextInt(enemies.Count) ?? Rng.Chaotic.NextInt(enemies.Count);
        var target = enemies[index];

        var totalFloor = Owner.Player?.RunState?.TotalFloor ?? 0;
        var damage = Math.Max(0m, totalFloor / divisor);
        if (damage <= 0)
            return;

        Flash();
        await CreatureCmd.Damage(choiceContext, target, damage, ValueProp.Move, Owner, null);
    }
}
