using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunSettlementTime() : NunCard(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
{
    public override List<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var lastTurnDamage = DamageTracker.GetLastTurnDamage(Owner.Creature);
        var multiplier = IsUpgraded ? 0.75m : 0.5m;
        var damage = Math.Max(lastTurnDamage * multiplier, 0m);

        await CreatureCmd.Damage(
            choiceContext,
            cardPlay.Target!,
            damage,
            ValueProp.Move,
            Owner.Creature,
            this);
    }

    protected override void OnUpgrade()
    {
    }
}
