using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using NightMoon.NightMoonCode.Powers.Nun;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunShieldPrayer() : NunPrayerCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<NunShieldPrayerPower>(1m)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<NunShieldPrayerPower>()
    ];

    protected override int PrayerTurns => IsUpgraded ? PrayerTier - 1 : PrayerTier;
    protected override int MaxPrayerTier => 3;
    protected override LocString PrayerChoiceDescription =>
        new("cards", $"{Id.Entry}.prayerChoice.{PrayerTier}{(IsUpgraded ? ".upgraded" : "")}");

    protected override PrayerEntry CreatePrayerEntry(CardPlay cardPlay)
    {
        PrayerEntry? entry = null;
        entry = new PrayerEntry(Id.Entry, PrayerTurns, async (choiceContext, owner) =>
        {
            await PowerCmd.Apply<NunShieldPrayerPower>(
                choiceContext,
                owner,
                PrayerTier * (entry?.ValueMultiplier ?? 1m),
                owner,
                this);
        });

        return entry;
    }

    protected override void OnUpgrade()
    {
    }
}
