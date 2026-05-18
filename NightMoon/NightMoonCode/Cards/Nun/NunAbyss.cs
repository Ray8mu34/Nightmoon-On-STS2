using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using NightMoon.NightMoonCode.Powers.Nun;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunAbyss() : NunCard(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    public override List<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var power = Owner.Creature.GetPower<NunConfessionPower>();
        if (power != null)
        {
            // 翻倍忏悔层数
            await PowerCmd.ModifyAmount(choiceContext, power, power.Amount, Owner.Creature, this, false);
        }

        if (IsUpgraded)
        {
            await PowerCmd.Apply<NunConfessionPower>(
                choiceContext,
                Owner.Creature,
                3m,
                Owner.Creature,
                this);
        }
    }

    protected override void OnUpgrade()
    {
    }
}
