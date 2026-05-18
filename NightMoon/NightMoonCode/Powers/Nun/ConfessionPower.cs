using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.ValueProps;

namespace NightMoon.NightMoonCode.Powers.Nun;

public class NunConfessionPower() : NunPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != Owner.Side || Amount <= 0)
            return;

        var combatState = Owner.CombatState;
        if (combatState is null)
            return;

        var enemies = combatState.HittableEnemies.ToList();
        if (enemies.Count == 0)
            return;

        Flash();
        var index = Owner.Player?.RunState?.Rng.Niche.NextInt(enemies.Count) ?? Rng.Chaotic.NextInt(enemies.Count);
        var props = ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.SkipHurtAnim;
        await CreatureCmd.Damage(choiceContext, enemies[index], Amount, props, Owner, null);
    }
}
