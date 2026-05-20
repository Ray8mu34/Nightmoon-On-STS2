using System.Linq;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunHolyWaterBath() : NunCard(1, CardType.Power, CardRarity.Common, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var combatState = Owner.PlayerCombatState;
        if (combatState == null)
            return;

        var curses = combatState.DrawPile.Cards
            .Concat(combatState.DiscardPile.Cards)
            .Concat(combatState.Hand.Cards)
            .Where(card => card.Type == CardType.Curse)
            .Distinct()
            .ToList();

        if (curses.Count > 0)
            await CardPileCmd.Add(curses, PileType.Exhaust, CardPilePosition.Bottom, this);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.SetCustomBaseCost(0);
    }
}
