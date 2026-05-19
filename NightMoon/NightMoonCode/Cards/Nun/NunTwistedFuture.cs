using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using NightMoon.NightMoonCode.Powers.Nun;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunTwistedFuture() : NunCard(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(7m, ValueProp.Move)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.Damage(choiceContext, cardPlay.Target!, DynamicVars.Damage, this);

        var confessionAmount = Owner.Creature.GetPowerAmount<NunConfessionPower>();
        if (confessionAmount > 5)
        {
            await CreatureCmd.Damage(
                choiceContext,
                cardPlay.Target!,
                4m,
                ValueProp.Move,
                Owner.Creature,
                this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3m);
    }
}
