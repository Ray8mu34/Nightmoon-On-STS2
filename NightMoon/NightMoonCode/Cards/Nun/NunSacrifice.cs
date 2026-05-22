using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunSacrifice() : NunCard(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        ..MakeCalculatedDamage(17, static (_, _) => 0m),
        new DynamicVar("Energy", 1m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var target = cardPlay.Target!;
        await CreatureCmd.Damage(choiceContext, target, DynamicVars.CalculatedDamage.BaseValue, DynamicVars.CalculatedDamage.Props, Owner.Creature, this);

        if (target.IsDead)
        {
            await PlayerCmd.GainEnergy((int)DynamicVars["Energy"].BaseValue, Owner);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationBase.UpgradeValueBy(7m);
    }
}
