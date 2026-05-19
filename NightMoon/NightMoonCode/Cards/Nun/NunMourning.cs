using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunMourning() : NunCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        PrayerManager.ModifyAllEntries(Owner.Creature, entry =>
        {
            entry.RemainingTurns += IsUpgraded ? 3 : 4;
            entry.ValueMultiplier *= 2m;
        });

        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
    }
}
