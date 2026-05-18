using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunNatureMark() : NunCard(1, CardType.Power, CardRarity.Common, TargetType.Self)
{
    public override List<CardKeyword> CanonicalKeywords =>
        IsUpgraded ? [] : [CardKeyword.Ethereal];

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<DexterityPower>(1m)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<DexterityPower>()
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<DexterityPower>(
            choiceContext,
            Owner.Creature,
            DynamicVars[typeof(DexterityPower).Name].BaseValue,
            Owner.Creature,
            this);
    }

    protected override void OnUpgrade()
    {
    }
}
