using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace NightMoon.NightMoonCode.Powers.Nun;

public class NunShieldPrayerPower() : NunPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player.Creature != Owner || Amount <= 0)
            return;

        Flash();
        await CreatureCmd.GainBlock(Owner, 7m, ValueProp.Unpowered, null);
        await PowerCmd.Decrement(this);
    }
}
