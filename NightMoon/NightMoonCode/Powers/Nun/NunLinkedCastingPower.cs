using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace NightMoon.NightMoonCode.Powers.Nun;

public class NunLinkedCastingPower() : NunPower
{
    private int pendingSelfTriggerSkips = 1;

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public void QueueSelfTriggerSkip()
    {
        pendingSelfTriggerSkips++;
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner.Creature != Owner || cardPlay.Card.Type != CardType.Skill || Amount <= 0)
            return;

        if (pendingSelfTriggerSkips > 0)
        {
            pendingSelfTriggerSkips--;
            return;
        }

        Flash();
        await CardPileCmd.Draw(context, 1, cardPlay.Card.Owner);
        await PowerCmd.Decrement(this);
    }
}
