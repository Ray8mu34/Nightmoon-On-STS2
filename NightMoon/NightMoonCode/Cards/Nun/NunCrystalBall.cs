using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunCrystalBall() : NunCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CardPileCmd.AutoPlayFromDrawPile(
            choiceContext,
            Owner,
            IsUpgraded ? 2 : 1,
            CardPilePosition.Top,
            forceExhaust: false);
    }

    protected override void OnUpgrade()
    {
    }
}
