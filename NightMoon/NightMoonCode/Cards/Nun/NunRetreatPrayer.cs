using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunRetreatPrayer() : NunPrayerCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(7m, ValueProp.Unpowered)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        ..base.ExtraHoverTips,
        HoverTipFactory.Static(StaticHoverTip.Block)
    ];

    protected override int PrayerTurns => 1;

    protected override PrayerEntry CreatePrayerEntry(CardPlay cardPlay)
    {
        return new PrayerEntry(Id.Entry, PrayerTurns, async (_, owner) =>
        {
            await CreatureCmd.GainBlock(owner, DynamicVars.Block.BaseValue, DynamicVars.Block.Props, null);
        });
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(3m);
    }
}
