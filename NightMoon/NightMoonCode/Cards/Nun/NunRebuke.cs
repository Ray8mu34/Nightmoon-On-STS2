using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunRebuke() : NunCard(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(0m, ValueProp.Move)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var prayerCount = Owner.Deck.Cards.Count(c => c is NunPrayerCard);
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
