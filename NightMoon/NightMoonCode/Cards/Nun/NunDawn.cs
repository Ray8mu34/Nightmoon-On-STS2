using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunDawn() : NunCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    public override List<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new HpLossVar(10m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.Heal(Owner.Creature, DynamicVars.HpLoss.BaseValue);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.HpLoss.UpgradeValueBy(3m);
    }
}
