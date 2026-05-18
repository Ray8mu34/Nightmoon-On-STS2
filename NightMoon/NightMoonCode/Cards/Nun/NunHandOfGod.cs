using System;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunHandOfGod() : NunCard(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
{
    public override List<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var target = cardPlay.Target!;
        var cap = IsUpgraded ? 80 : 60;
        var damage = Math.Min(target.CurrentHp / 2, cap);
        damage = Math.Max(damage, 1);

        await CreatureCmd.Damage(choiceContext, target, damage, ValueProp.Unpowered, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
    }
}
