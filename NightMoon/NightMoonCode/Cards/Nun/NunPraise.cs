using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunPraise() : NunCard(0, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    public override List<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("Advance", 1m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PrayerManager.Accelerate(choiceContext, Owner.Creature, (int)DynamicVars["Advance"].BaseValue);
    }

    protected override void OnUpgrade()
    {
        _ = Keywords;
        AddKeyword(CardKeyword.Retain);
    }
}
