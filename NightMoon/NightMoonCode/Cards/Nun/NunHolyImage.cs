using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using NightMoon.NightMoonCode.Powers.Nun;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunHolyImage() : NunCard(2, CardType.Power, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<NunHolyImagePower>(1m)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<NunHolyImagePower>()
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<NunHolyImagePower>(
            choiceContext,
            Owner.Creature,
            DynamicVars[typeof(NunHolyImagePower).Name].BaseValue,
            Owner.Creature,
            this);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.SetCustomBaseCost(1);
    }
}
