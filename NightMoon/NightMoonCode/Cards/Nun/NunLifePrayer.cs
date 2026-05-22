using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunLifePrayer() : NunPrayerCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    public override List<CardKeyword> CanonicalKeywords => [NunKeywords.Prayer, CardKeyword.Exhaust];

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

    protected override void AddExtraArgsToPrayerText(LocString text)
    {
        base.AddExtraArgsToPrayerText(text);
        text.Add("Value1", CalculateHeal(1));
        text.Add("Value2", CalculateHeal(2));
        text.Add("Value3", CalculateHeal(3));
        text.Add("ChoiceValue", CalculateHeal(PrayerTier));
    }

    private decimal CalculateHeal() => CalculateHeal(PrayerTier);

    private decimal CalculateHeal(int tier)
    {
        return DynamicVars.HpLoss.BaseValue + tier switch
        {
            1 => 0m,
            2 => 2m,
            _ => 5m
        };
    }
}
