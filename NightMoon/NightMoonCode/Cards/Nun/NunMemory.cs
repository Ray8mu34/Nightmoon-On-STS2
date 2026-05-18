using System.Linq;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunMemory() : NunCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var exhaustPile = Owner.PlayerCombatState.ExhaustPile.Cards
            .Where(card => card != this)
            .ToList();
        if (exhaustPile.Count == 0)
            return;

        var selectCount = Math.Min(IsUpgraded ? 2 : 1, exhaustPile.Count);
        var selected = await CardSelectCmd.FromSimpleGrid(
            choiceContext,
            exhaustPile,
            Owner,
            new CardSelectorPrefs(SelectionScreenPrompt, selectCount));

        foreach (var card in selected.ToList())
        {
            var clone = CombatState.CloneCard(card);
            await CardPileCmd.Add(clone, PileType.Hand, CardPilePosition.Bottom, this, false);
        }
    }

    protected override void OnUpgrade()
    {
    }
}
