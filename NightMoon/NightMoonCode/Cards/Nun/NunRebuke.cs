using System.Linq;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunRebuke() : NunCard(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var combatState = Owner.PlayerCombatState;
        var prayerCount = 0;
        if (combatState != null)
        {
            prayerCount += combatState.DrawPile.Cards.Count(c => c is NunPrayerCard);
            prayerCount += combatState.DiscardPile.Cards.Count(c => c is NunPrayerCard);
            prayerCount += combatState.Hand.Cards.Count(c => c is NunPrayerCard);
            prayerCount += combatState.ExhaustPile.Cards.Count(c => c is NunPrayerCard);
        }

        var damage = prayerCount + (IsUpgraded ? 3 : 0);
        await CreatureCmd.Damage(
            choiceContext,
            cardPlay.Target!,
            damage,
            ValueProp.Move,
            Owner.Creature,
            this);
    }

    protected override void OnUpgrade()
    {
    }
}
