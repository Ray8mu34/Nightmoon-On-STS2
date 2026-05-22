using System.Linq;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunInspirationBurst() : NunCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        ..MakeCalculatedBlock(6, static (_, _) => 0m),
        new DynamicVar("Choices", 3m),
        new DynamicVar("Pick", 1m)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.Static(StaticHoverTip.Block)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.CalculatedBlock.BaseValue, DynamicVars.CalculatedBlock.Props, cardPlay);

        var choices = TakeRandomCards(
            Owner.PlayerCombatState.DrawPile.Cards.Where(card => card.Type == CardType.Skill),
            (int)DynamicVars["Choices"].BaseValue);

        var selected = await CardSelectCmd.FromChooseACardScreen(choiceContext, choices, Owner, canSkip: true);
        if (selected != null)
            await CardPileCmd.Add(selected, PileType.Hand, CardPilePosition.Bottom, this, false);
    }

    private List<CardModel> TakeRandomCards(IEnumerable<CardModel> source, int count)
    {
        var pool = source.ToList();
        var result = new List<CardModel>();

        while (pool.Count > 0 && result.Count < count)
        {
            var card = Owner.RunState.Rng.CombatCardSelection.NextItem(pool);
            pool.Remove(card);
            result.Add(card);
        }

        return result;
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationBase.UpgradeValueBy(2m);
    }
}

