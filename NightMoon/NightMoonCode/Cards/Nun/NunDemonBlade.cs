using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using NightMoon.NightMoonCode.Powers.Nun;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunDemonBlade() : NunCard(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<NunDemonBladePower>(0m)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<NunDemonBladePower>()
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<NunDemonBladePower>(
            choiceContext,
            Owner.Creature,
            1m,
            Owner.Creature,
            this);

        var power = Owner.Creature.GetPower<NunDemonBladePower>();
        if (power != null && DynamicVars[typeof(NunDemonBladePower).Name].BaseValue > 0)
            power.AddBonusDamage(DynamicVars[typeof(NunDemonBladePower).Name].BaseValue);
    }

    protected override void OnUpgrade()
    {
        DynamicVars[typeof(NunDemonBladePower).Name].UpgradeValueBy(3m);
    }
}
