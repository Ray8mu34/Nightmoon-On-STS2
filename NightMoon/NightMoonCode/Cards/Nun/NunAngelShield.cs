using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunAngelShield() : NunCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(9m, ValueProp.Move)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.Static(StaticHoverTip.Block)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);

        var clone = CombatState.CreateCard(ModelDb.GetById<CardModel>(Id), Owner);
        for (var i = 0; i < CurrentUpgradeLevel; i++)
        {
            clone.UpgradeInternal();
            clone.FinalizeUpgradeInternal();
        }

        await CardPileCmd.AddGeneratedCardToCombat(clone, PileType.Draw, Owner);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(3m);
    }
}

