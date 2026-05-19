using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using NightMoon.NightMoonCode.Powers.Nun;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunLinkedCasting() : NunCard(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<NunLinkedCastingPower>(2m)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<NunLinkedCastingPower>()
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var existingPower = Owner.Creature.GetPower<NunLinkedCastingPower>();
        existingPower?.QueueSelfTriggerSkip();

        await PowerCmd.Apply<NunLinkedCastingPower>(
            choiceContext,
            Owner.Creature,
            DynamicVars[typeof(NunLinkedCastingPower).Name].BaseValue,
            Owner.Creature,
            this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars[typeof(NunLinkedCastingPower).Name].UpgradeValueBy(1m);
    }
}
