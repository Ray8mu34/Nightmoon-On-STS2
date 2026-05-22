using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunBladeShield() : NunCard(3, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        ..MakeCalculatedDamage("BladeDamage", 17, static (_, _) => 0m),
        ..MakeCalculatedBlock("BladeBlock", 17, static (_, _) => 0m)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.Static(StaticHoverTip.Block)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var damage = (CalculatedDamageVar)DynamicVars["BladeDamage"];
        var block = (CalculatedBlockVar)DynamicVars["BladeBlock"];

        await CreatureCmd.Damage(
            choiceContext,
            cardPlay.Target!,
            damage.BaseValue,
            damage.Props,
            Owner.Creature,
            this);

        await CreatureCmd.GainBlock(Owner.Creature, block.BaseValue, block.Props, cardPlay);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["BladeDamageBase"].UpgradeValueBy(3m);
        DynamicVars["BladeBlockBase"].UpgradeValueBy(3m);
    }
}

