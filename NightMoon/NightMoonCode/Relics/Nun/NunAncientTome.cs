using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Relics.Nun;

public class NunAncientTome() : NunRelic
{
    public override RelicRarity Rarity => RelicRarity.Rare;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player.Creature != Owner.Creature)
            return;

        var count = PrayerManager.Count(Owner.Creature);
        if (count <= 0)
            return;

        Flash();
        await PrayerManager.Accelerate(choiceContext, Owner.Creature, 1);
    }
}
