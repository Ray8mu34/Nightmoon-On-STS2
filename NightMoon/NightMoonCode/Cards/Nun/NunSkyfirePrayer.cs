using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunSkyfirePrayer() : NunPrayerCard(2, CardType.Attack, CardRarity.Common, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(13m, ValueProp.Move)
    ];

    protected override int PrayerTurns => 1;

    protected override PrayerEntry CreatePrayerEntry(CardPlay cardPlay)
    {
        return new PrayerEntry(Id.Entry, PrayerTurns, async (context, owner) =>
        {
            var combatState = owner.CombatState;
            if (combatState is null)
                return;

            await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
                .FromCard(this)
                .TargetingAllOpponents(combatState)
                .WithHitFx("vfx/vfx_attack_fire")
                .Execute(context);
        });
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(4m);
    }
}
