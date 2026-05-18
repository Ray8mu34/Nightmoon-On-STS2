using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunLifePrayer() : NunPrayerCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new HpLossVar(3m)
    ];

    protected override int PrayerTurns => 1;

    protected override PrayerEntry CreatePrayerEntry(CardPlay cardPlay)
    {
        return new PrayerEntry(Id.Entry, PrayerTurns, async (_, owner) =>
        {
            await CreatureCmd.Heal(owner, DynamicVars.HpLoss.BaseValue);
        });
    }

    protected override void OnUpgrade()
    {
        DynamicVars.HpLoss.UpgradeValueBy(2m);
    }
}
