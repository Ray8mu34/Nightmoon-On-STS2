using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunManaPrayer() : NunPrayerCard(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override int PrayerTurns => 1;

    protected override PrayerEntry CreatePrayerEntry(CardPlay cardPlay)
    {
        return new PrayerEntry(Id.Entry, PrayerTurns, async (_, owner) =>
        {
            if (owner.Player is not null)
            {
                await PlayerCmd.GainEnergy(IsUpgraded ? 3m : 2m, owner.Player);
            }
        });
    }

    protected override void OnUpgrade()
    {
    }
}
