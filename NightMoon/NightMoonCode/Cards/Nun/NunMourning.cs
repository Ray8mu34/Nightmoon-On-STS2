using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunMourning() : NunCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        // 所有祷告牌数值翻倍，祷告回合数+4
        PrayerManager.ModifyAllEntries(Owner.Creature, entry =>
        {
            entry.RemainingTurns += IsUpgraded ? 3 : 4;
        });
    }

    protected override void OnUpgrade()
    {
    }
}
