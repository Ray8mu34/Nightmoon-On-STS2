using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
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

    protected override int PrayerTurns => IsUpgraded ? 0 : 1;

    protected override PrayerEntry CreatePrayerEntry(CardPlay cardPlay)
    {
        return new PrayerEntry(Id.Entry, PrayerTurns, async (choiceContext, owner) =>
        {
            await PowerCmd.Apply<NunShieldPrayerPower>(
                choiceContext,
                owner,
                DynamicVars[typeof(NunShieldPrayerPower).Name].BaseValue,
                owner,
                this);
        });
    }

    protected override void OnUpgrade()
    {
    }
}
