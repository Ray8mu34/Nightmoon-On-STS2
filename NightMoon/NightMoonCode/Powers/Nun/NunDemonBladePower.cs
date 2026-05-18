using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace NightMoon.NightMoonCode.Powers.Nun;

public class NunDemonBladePower() : NunPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player.Creature != Owner)
            return;

        var combatState = Owner.CombatState;
        var target = combatState?.HittableEnemies.FirstOrDefault();
        if (target == null)
            return;

        var damage = Math.Max(0, Owner.Player.RunState.TotalFloor / 5 + Amount);
        if (damage <= 0)
            return;

        Flash();
        await DamageCmd.Attack(damage)
            .Targeting(target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
    }
}
