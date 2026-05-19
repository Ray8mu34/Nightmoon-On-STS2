using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace NightMoon.NightMoonCode.Powers.Nun;

public class NunConfessionRecordPower() : NunPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player.Creature != Owner)
            return;

        Flash();
        await CreatureCmd.Damage(
            choiceContext,
            Owner,
            1m,
            ValueProp.Unblockable | ValueProp.Unpowered,
            Owner,
            null);

        await PowerCmd.Apply<NunConfessionPower>(
            choiceContext,
            Owner,
            Amount,
            Owner,
            null);
    }
}
