using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunMourning() : NunCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("Multiplier", 2m),
        new DynamicVar("Delay", 4m)
    ];

    protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        PrayerManager.ModifyAllEntries(Owner.Creature, entry =>
        {
            entry.RemainingTurns += (int)DynamicVars["Delay"].BaseValue;
            entry.ValueMultiplier *= DynamicVars["Multiplier"].BaseValue;
        });

        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Delay"].UpgradeValueBy(-1m);
    }
}
