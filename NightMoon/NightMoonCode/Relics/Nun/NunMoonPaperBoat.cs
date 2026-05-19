using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace NightMoon.NightMoonCode.Relics.Nun;

public class NunMoonPaperBoat() : NunRelic
{
    public override RelicRarity Rarity => RelicRarity.Rare;

    private static readonly SpireField<Creature, int> TurnCounter = new(() => 0);

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player.Creature != Owner.Creature)
            return;

        TurnCounter.Set(Owner.Creature, TurnCounter.Get(Owner.Creature) + 1);

        if (TurnCounter.Get(Owner.Creature) >= 5)
        {
            TurnCounter.Set(Owner.Creature, 0);
            Flash();
            await PowerCmd.Apply<BufferPower>(choiceContext, Owner.Creature, 1m, Owner.Creature, null);
        }
    }
}
