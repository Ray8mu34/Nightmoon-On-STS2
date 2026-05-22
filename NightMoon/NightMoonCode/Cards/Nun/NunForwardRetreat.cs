using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunForwardRetreat() : NunCard(1, CardType.Attack, CardRarity.Common, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        ..MakeCalculatedDamage(8, static (_, _) => 0m),
        new DynamicVar("DrawPerPrayer", 1m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var combatState = Owner.Creature.CombatState;
        if (combatState is null)
            return;

        await CreatureCmd.Damage(
            choiceContext,
            combatState.HittableEnemies.ToList(),
            DynamicVars.CalculatedDamage.BaseValue,
            DynamicVars.CalculatedDamage.Props,
            Owner.Creature,
            this);

        var prayerCount = PrayerManager.Count(Owner.Creature);
        if (prayerCount > 0)
        {
            await CardPileCmd.Draw(choiceContext, prayerCount * (int)DynamicVars["DrawPerPrayer"].BaseValue, Owner);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationBase.UpgradeValueBy(2m);
    }
}
