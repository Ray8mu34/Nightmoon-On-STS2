using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunHolyPurification() : NunCard(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        ..MakeCalculatedDamage(16, static (_, _) => 0m),
        new DynamicVar("Threshold", 30m),
        new DynamicVar("Multiplier", 2m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var target = cardPlay.Target!;
        var baseDamage = DynamicVars.CalculatedDamage.BaseValue;
        var damage = (decimal)target.GetHpPercentRemaining() < DynamicVars["Threshold"].BaseValue / 100m
            ? baseDamage * DynamicVars["Multiplier"].BaseValue
            : baseDamage;

        await CreatureCmd.Damage(
            choiceContext,
            target,
            damage,
            DynamicVars.CalculatedDamage.Props,
            Owner.Creature,
            this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationBase.UpgradeValueBy(4m);
    }
}
