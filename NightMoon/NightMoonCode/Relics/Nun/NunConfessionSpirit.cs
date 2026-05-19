using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using NightMoon.NightMoonCode.Powers.Nun;

namespace NightMoon.NightMoonCode.Relics.Nun;

public class NunConfessionSpirit() : NunRelic
{
    public override RelicRarity Rarity => RelicRarity.Common;

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner?.Creature != Owner.Creature)
            return;
        if (cardPlay.Card.Type != CardType.Skill)
            return;

        Flash();
        await PowerCmd.Apply<NunConfessionPower>(
            choiceContext,
            Owner.Creature,
            1m,
            Owner.Creature,
            null);
    }
}
