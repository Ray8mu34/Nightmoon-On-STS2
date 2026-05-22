using System;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunHandOfGod() : NunCard(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
{
    public override List<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("Percent", 50m),
        new DynamicVar("Cap", 60m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var target = cardPlay.Target!;
        var damage = Math.Min(target.CurrentHp * DynamicVars["Percent"].BaseValue / 100m, DynamicVars["Cap"].BaseValue);
        damage = Math.Max(damage, 1);

        await CreatureCmd.Damage(
            choiceContext,
            target,
            damage,
            ValueProp.Unblockable | ValueProp.Unpowered,
            Owner.Creature,
            this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Cap"].UpgradeValueBy(20m);
    }
}
