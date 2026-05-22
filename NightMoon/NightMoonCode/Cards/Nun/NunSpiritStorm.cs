using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunSpiritStorm() : NunCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    public override List<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        ..MakeCalculatedDamage(10, static (_, _) => 0m),
        new DynamicVar("Threshold", 50m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var target = cardPlay.Target!;

        await CreatureCmd.Damage(choiceContext, target, DynamicVars.CalculatedDamage.BaseValue, DynamicVars.CalculatedDamage.Props, Owner.Creature, this);

        if (target.IsAlive && (decimal)target.GetHpPercentRemaining() < DynamicVars["Threshold"].BaseValue / 100m)
        {
            await CreatureCmd.Stun(target, (string?)null);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationBase.UpgradeValueBy(5m);
    }
}
