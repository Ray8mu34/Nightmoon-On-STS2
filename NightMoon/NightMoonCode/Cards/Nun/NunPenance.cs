using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using NightMoon.NightMoonCode.Powers.Nun;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunPenance() : NunCard(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CardPileCmd.Draw(choiceContext, IsUpgraded ? 2 : 1, Owner, false);

        var confession = Owner.Creature.GetPower<NunConfessionPower>();
        if (confession is { Amount: > 0 })
            await confession.Trigger(choiceContext, confession.Amount);
    }

    protected override void OnUpgrade()
    {
    }
}
