using System;
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
        if (player != Owner)
            return;

        var count = PrayerManager.Count(Owner.Creature);
        if (count <= 0)
            return;

        Flash();
        PrayerManager.ModifyAllEntries(Owner.Creature, entry =>
        {
            entry.RemainingTurns = Math.Max(0, entry.RemainingTurns - 1);
        });
    }
}
