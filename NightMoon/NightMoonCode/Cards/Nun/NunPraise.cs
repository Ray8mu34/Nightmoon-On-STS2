using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunPraise() : NunCard(0, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    public override List<CardKeyword> CanonicalKeywords =>
        IsUpgraded ? [CardKeyword.Exhaust, CardKeyword.Retain] : [CardKeyword.Exhaust];

    protected override IEnumerable<MegaCrit.Sts2.Core.Localization.DynamicVars.DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PrayerManager.Accelerate(choiceContext, Owner.Creature, 1);
    }

    protected override void OnUpgrade()
    {
    }
}
