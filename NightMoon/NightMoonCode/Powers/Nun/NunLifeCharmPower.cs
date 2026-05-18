using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace NightMoon.NightMoonCode.Powers.Nun;

public class NunLifeCharmPower() : NunPower
{
    private int turnCounter;

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player.Creature != Owner)
            return;

        turnCounter++;
        if (turnCounter < 3)
            return;

        turnCounter = 0;
        Flash();
        await CreatureCmd.Heal(Owner, Amount);
    }
}
