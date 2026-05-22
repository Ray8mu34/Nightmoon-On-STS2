using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunManaPrayer() : NunPrayerCard(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override int PrayerTurns => PrayerTier;
    protected override int MaxPrayerTier => 2;
    protected override LocString PrayerChoiceDescription =>
        new("cards", $"{Id.Entry}.prayerChoice.{PrayerTier}{(IsUpgraded ? ".upgraded" : "")}");

    protected override PrayerEntry CreatePrayerEntry(CardPlay cardPlay)
    {
        PrayerEntry? entry = null;
        entry = new PrayerEntry(Id.Entry, PrayerTurns, async (_, owner) =>
        {
            if (owner.Player is not null)
            {
                var amount = ((IsUpgraded ? 3m : 2m) + PrayerTier - 1) * (entry?.ValueMultiplier ?? 1m);
                await PlayerCmd.GainEnergy(amount, owner.Player);
            }
        });

        return entry;
    }

    protected override void OnUpgrade()
    {
    }

    protected override void AddExtraArgsToPrayerText(LocString text)
    {
        base.AddExtraArgsToPrayerText(text);
        text.Add("Value1", CalculateEnergy(1));
        text.Add("Value2", CalculateEnergy(2));
        text.Add("ChoiceValue", CalculateEnergy(PrayerTier));
    }

    private decimal CalculateEnergy(int tier)
    {
        return (IsUpgraded ? 3m : 2m) + tier - 1;
    }
}
