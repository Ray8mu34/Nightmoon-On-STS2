using System.Linq;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunHolyWaterBath() : NunCard(1, CardType.Power, CardRarity.Common, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var curses = Owner.Deck.Cards
            .Where(card => card.Type == CardType.Curse)
            .ToList();

        if (curses.Count > 0)
            await CardPileCmd.RemoveFromDeck(curses, showPreview: true);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.SetCustomBaseCost(0);
    }
}
