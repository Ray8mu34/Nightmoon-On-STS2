using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunSkyfirePrayer() : NunPrayerCard(2, CardType.Attack, CardRarity.Common, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(13m, ValueProp.Move)
    ];

    protected override int PrayerTurns => PrayerTier;
    protected override int MaxPrayerTier => 3;
    protected override LocString PrayerChoiceDescription =>
        new("cards", $"{Id.Entry}.prayerChoice.{PrayerTier}{(IsUpgraded ? ".upgraded" : "")}");

    protected override PrayerEntry CreatePrayerEntry(CardPlay cardPlay)
    {
        PrayerEntry? entry = null;
        entry = new PrayerEntry(Id.Entry, PrayerTurns, async (context, owner) =>
        {
            var combatState = owner.CombatState;
            if (combatState is null)
                return;

            await CreatureCmd.Damage(
                context,
                combatState.HittableEnemies.ToList(),
                CalculateDamage() * (entry?.ValueMultiplier ?? 1m),
                ValueProp.Unblockable | ValueProp.Unpowered,
                owner,
                this);
        });

        return entry;
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(4m);
    }

    protected override void AddExtraArgsToPrayerText(LocString text)
    {
        base.AddExtraArgsToPrayerText(text);
        text.Add("Value1", CalculateDamage(1));
        text.Add("Value2", CalculateDamage(2));
        text.Add("Value3", CalculateDamage(3));
        text.Add("ChoiceValue", CalculateDamage(PrayerTier));
    }

    private decimal CalculateDamage() => CalculateDamage(PrayerTier);

    private decimal CalculateDamage(int tier)
    {
        return DynamicVars.Damage.BaseValue + 5m * (tier - 1);
    }
}
