using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Potions.Nun;

public class NunBlessingPotion() : NunPotion
{
    public override PotionRarity Rarity => PotionRarity.Rare;
    public override PotionUsage Usage => PotionUsage.CombatOnly;
    public override TargetType TargetType => TargetType.Self;

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        var entriesToCopy = new List<PrayerEntry>();
        PrayerManager.ModifyAllEntries(Owner.Creature, entry =>
        {
            entriesToCopy.Add(entry.Clone());
        });

        foreach (var entry in entriesToCopy)
        {
            await PrayerManager.Add(choiceContext, Owner.Creature, entry);
        }
    }
}
