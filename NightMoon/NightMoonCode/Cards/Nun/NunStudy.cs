using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunStudy() : NunCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(8m, ValueProp.Move)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CardPileCmd.Draw(choiceContext, 1, Owner, false);

        await CreatureCmd.Damage(
            choiceContext,
            CombatState!.HittableEnemies.ToList(),
            DynamicVars.Damage.BaseValue,
            DynamicVars.Damage.Props,
            Owner.Creature,
            this);

        // 若手牌卡牌类型均不同，则重复
        var hand = Owner.PlayerCombatState.Hand.Cards;
        var types = hand.Select(c => c.Type).Distinct().Count();
        if (types == hand.Count)
        {
            await CardPileCmd.Draw(choiceContext, 1, Owner, false);

            await CreatureCmd.Damage(
                choiceContext,
                CombatState.HittableEnemies.ToList(),
                DynamicVars.Damage.BaseValue,
                DynamicVars.Damage.Props,
                Owner.Creature,
                this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(4m);
    }
}
