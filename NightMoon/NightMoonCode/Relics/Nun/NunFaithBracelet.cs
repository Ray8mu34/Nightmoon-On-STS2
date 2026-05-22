using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Relics.Nun;

public class NunFaithBracelet() : NunRelic
{
    public override RelicRarity Rarity => RelicRarity.Rare;

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side != CombatSide.Player)
            return;

        var prayerCount = PrayerManager.Count(Owner.Creature);
        if (prayerCount <= 0)
            return;

        Flash();
        await CreatureCmd.GainBlock(Owner.Creature, prayerCount, ValueProp.Unpowered, null, false);
    }
}
