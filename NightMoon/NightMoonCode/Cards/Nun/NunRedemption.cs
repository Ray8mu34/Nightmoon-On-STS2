using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;
using NightMoon.NightMoonCode.Powers.Nun;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunRedemption() : NunCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    public override List<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var confessionAmount = Owner.Creature.GetPowerAmount<NunConfessionPower>();
        if (confessionAmount <= 0)
            return;

        await CreatureCmd.Damage(
            choiceContext,
            cardPlay.Target!,
            confessionAmount,
            ValueProp.Unpowered,
            Owner.Creature,
            this);
    }

    protected override void OnUpgrade()
    {
        _ = Keywords;
        AddKeyword(CardKeyword.Retain);
    }
}
