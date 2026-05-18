using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunSettlementTime() : NunCard(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
{
    public override List<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var lastTurnDamage = DamageTracker.GetLastTurnDamage(Owner.Creature);
        var multiplier = IsUpgraded ? 0.75m : 0.5m;
        var damage = (int)(lastTurnDamage * multiplier);
        damage = Math.Max(damage, 0);

        await DamageCmd.Attack(damage)
            .FromCard(this)
            .Targeting(cardPlay.Target!)
            .WithHitFx("vfx/vfx_attack_slash_heavy")
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
    }
}
