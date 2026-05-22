using System.Linq;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunProphecy() : NunCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("Look", 3m),
        new DynamicVar("Choose", 2m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var lookCount = (int)DynamicVars["Look"].BaseValue;
        var cards = Owner.PlayerCombatState.DrawPile.Cards.Take(lookCount).ToList();
        if (cards.Count == 0)
            return;

        var selectCount = Math.Min((int)DynamicVars["Choose"].BaseValue, cards.Count);
        var selected = await CardSelectCmd.FromSimpleGrid(
            choiceContext,
            cards,
            Owner,
            new CardSelectorPrefs(SelectionScreenPrompt, selectCount));

        foreach (var card in selected.ToList())
            await CardPileCmd.Add(card, PileType.Hand, CardPilePosition.Bottom, this, false);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Look"].UpgradeValueBy(2m);
    }
}
