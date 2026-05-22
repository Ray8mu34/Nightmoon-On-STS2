using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunRockfall() : NunCard(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        ..MakeCalculatedDamage(4, static (card, _) => card.Owner?.Creature.CombatState?.RoundNumber ?? 0m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var roundBonus = Owner.Creature.CombatState?.RoundNumber ?? 0;
        await CreatureCmd.Damage(
            choiceContext,
            cardPlay.Target!,
            DynamicVars.CalculatedDamage.BaseValue + roundBonus,
            DynamicVars.CalculatedDamage.Props,
            Owner.Creature,
            this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationBase.UpgradeValueBy(2m);
    }
}
