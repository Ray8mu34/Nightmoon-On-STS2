using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunSwiftPrayer() : NunPrayerCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override int PrayerTurns => 1;

    protected override PrayerEntry CreatePrayerEntry(CardPlay cardPlay)
    {
        return new PrayerEntry(Id.Entry, PrayerTurns, async (context, owner) =>
        {
            if (owner.Player is not null)
            {
                await CardPileCmd.Draw(context, IsUpgraded ? 3 : 2, owner.Player);
            }
        });
    }

    protected override void OnUpgrade()
    {
    }
}
