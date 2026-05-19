using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace NightMoon.NightMoonCode.Powers.Nun;

public class NunFinalHopePower() : NunPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override bool ShouldTakeExtraTurn(Player player)
    {
        if (player.Creature != Owner || Amount <= 0)
            return false;

        Flash();
        SetAmount(0);
        return true;
    }
}
