using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using NightMoon.NightMoonCode.Prayer;
using NightMoon.NightMoonCode.Powers.Nun;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunRepentPrayer() : NunPrayerCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override int PrayerTurns => 3;

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<NunConfessionPower>(3m)
    ];

    protected override PrayerEntry CreatePrayerEntry(CardPlay cardPlay)
    {
        var confessionAmount = DynamicVars[typeof(NunConfessionPower).Name].BaseValue;
        return CreatePrayerEntry(cardPlay.Card.Id.Entry, async choiceContext =>
        {
            await PowerCmd.Apply<NunConfessionPower>(
                choiceContext,
                Owner.Creature,
                confessionAmount,
                Owner.Creature,
                this);
        });
    }

    protected override void OnUpgrade()
    {
        EnergyCost.SetCustomBaseCost(0);
    }
}
