using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunStudy() : NunCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        ..MakeCalculatedDamage(8, static (_, _) => 0m),
        new DynamicVar("Draw", 1m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CardPileCmd.Draw(choiceContext, (int)DynamicVars["Draw"].BaseValue, Owner, false);

        await CreatureCmd.Damage(
            choiceContext,
            CombatState!.HittableEnemies.ToList(),
            DynamicVars.CalculatedDamage.BaseValue,
            DynamicVars.CalculatedDamage.Props,
            Owner.Creature,
            this);

        // 若手牌卡牌类型均不同，则重复
        var hand = Owner.PlayerCombatState.Hand.Cards;
        var types = hand.Select(c => c.Type).Distinct().Count();
        if (types == hand.Count)
        {
            await CardPileCmd.Draw(choiceContext, (int)DynamicVars["Draw"].BaseValue, Owner, false);

            await CreatureCmd.Damage(
                choiceContext,
                CombatState.HittableEnemies.ToList(),
                DynamicVars.CalculatedDamage.BaseValue,
                DynamicVars.CalculatedDamage.Props,
                Owner.Creature,
                this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationBase.UpgradeValueBy(4m);
    }
}
