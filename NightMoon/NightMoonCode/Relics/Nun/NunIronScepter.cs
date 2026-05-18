using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.ValueProps;
using NightMoon.NightMoonCode.Cards.Nun;

namespace NightMoon.NightMoonCode.Relics.Nun;

public class NunIronScepter() : NunRelic
{
    public override RelicRarity Rarity => RelicRarity.Uncommon;

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != Owner)
            return;
        if (cardPlay.Card is not NunPrayerCard)
            return;

        var enemies = Owner.Creature.CombatState?.HittableEnemies;
        if (enemies is null || enemies.Count == 0)
            return;

        var index = Owner.RunState?.Rng.Niche.NextInt(enemies.Count) ?? Rng.Chaotic.NextInt(enemies.Count);
        var target = enemies[index];

        Flash();
        await CreatureCmd.Damage(choiceContext, target, 1m, ValueProp.Unpowered, Owner.Creature, null);
    }
}
