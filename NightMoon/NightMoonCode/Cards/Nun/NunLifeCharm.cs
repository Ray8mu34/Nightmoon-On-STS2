using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using NightMoon.NightMoonCode.Powers.Nun;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunLifeCharm() : NunCard(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<NunLifeCharmPower>(3m)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<NunLifeCharmPower>()
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<NunLifeCharmPower>(
            choiceContext,
            Owner.Creature,
            DynamicVars[typeof(NunLifeCharmPower).Name].BaseValue,
            Owner.Creature,
            this);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.SetCustomBaseCost(0);
    }
}
