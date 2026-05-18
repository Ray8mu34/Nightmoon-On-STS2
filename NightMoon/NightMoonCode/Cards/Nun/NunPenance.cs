using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;
using NightMoon.NightMoonCode.Powers.Nun;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunPenance() : NunCard(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CardPileCmd.Draw(choiceContext, IsUpgraded ? 2 : 1, Owner, false);

        // 触发忏悔效果：随机敌方失去忏悔层数生命
        var confessionAmount = Owner.Creature.GetPowerAmount<NunConfessionPower>();
        if (confessionAmount > 0)
        {
            var enemies = CombatState!.HittableEnemies;
            if (enemies.Count > 0)
            {
                var target = Owner.RunState.Rng.CombatTargets.NextItem(enemies)!;
                await CreatureCmd.Damage(choiceContext, target, confessionAmount, ValueProp.Unpowered, Owner.Creature, null);
            }
        }
    }

    protected override void OnUpgrade()
    {
    }
}
