using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunLifePrayer() : NunPrayerCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new HpLossVar(3m)
    ];

    protected override int PrayerTurns => PrayerTier;
    protected override int MaxPrayerTier => 3;
    protected override LocString PrayerChoiceDescription =>
        new("cards", $"{Id.Entry}.prayerChoice.{PrayerTier}{(IsUpgraded ? ".upgraded" : "")}");

    protected override PrayerEntry CreatePrayerEntry(CardPlay cardPlay)
    {
        PrayerEntry? entry = null;
        entry = new PrayerEntry(Id.Entry, PrayerTurns, async (_, owner) =>
        {
            await CreatureCmd.Heal(owner, CalculateHeal() * (entry?.ValueMultiplier ?? 1m));
        });

        return entry;
    }

    protected override void OnUpgrade()
    {
        DynamicVars.HpLoss.UpgradeValueBy(2m);
    }

    private decimal CalculateHeal()
    {
        return DynamicVars.HpLoss.BaseValue + PrayerTier switch
        {
            1 => 0m,
            2 => 2m,
            _ => 5m
        };
    }
}
