using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunRetreatPrayer() : NunPrayerCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(7m, ValueProp.Move)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        ..base.ExtraHoverTips,
        HoverTipFactory.Static(StaticHoverTip.Block)
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
            await CreatureCmd.GainBlock(owner, CalculateBlock() * (entry?.ValueMultiplier ?? 1m), DynamicVars.Block.Props, null);
        });

        return entry;
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(3m);
    }

    protected override void AddExtraArgsToPrayerText(LocString text)
    {
        base.AddExtraArgsToPrayerText(text);
        text.Add("Value1", CalculateBlock(1));
        text.Add("Value2", CalculateBlock(2));
        text.Add("Value3", CalculateBlock(3));
        text.Add("ChoiceValue", CalculateBlock(PrayerTier));
    }

    private decimal CalculateBlock() => CalculateBlock(PrayerTier);

    private decimal CalculateBlock(int tier)
    {
        return DynamicVars.Block.BaseValue + 3m * (tier - 1);
    }
}

