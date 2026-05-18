using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using NightMoon.NightMoonCode.Powers.Nun;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunRedemption() : NunCard(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    public override List<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var confessionAmount = Owner.Creature.GetPowerAmount<NunConfessionPower>();
        if (confessionAmount <= 0) return;

        await CreatureCmd.Damage(choiceContext, cardPlay.Target!, confessionAmount, ValueProp.Unpowered, Owner.Creature, this);

        // 忏悔层数减半
        var newAmount = confessionAmount / 2;
        var power = Owner.Creature.GetPower<NunConfessionPower>();
        if (power != null)
        {
            var diff = newAmount - power.Amount;
            if (diff < 0)
                await PowerCmd.ModifyAmount(choiceContext, power, diff, Owner.Creature, this, false);
        }
    }

    protected override void OnUpgrade()
    {
        // 升级：添加保留
    }
}
