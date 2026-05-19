using System.Linq;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using NightMoon.NightMoonCode.Powers.Nun;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunDivineScepter() : NunCard(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    public override List<CardKeyword> CanonicalKeywords =>
        IsUpgraded ? [CardKeyword.Exhaust, CardKeyword.Retain] : [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var prayers = Owner.Deck.Cards.OfType<NunPrayerCard>().ToList();
        var addedCount = 0;
        foreach (var prayer in prayers)
        {
            var copy = (NunPrayerCard)CombatState.CreateCard(
                ModelDb.GetById<CardModel>(prayer.Id),
                Owner);

            for (var i = 0; i < prayer.CurrentUpgradeLevel; i++)
                copy.UpgradeInternal();

            copy.SetPrayerTier(4);
            await PrayerManager.Add(choiceContext, Owner.Creature, copy.CreatePrayerEntryForPrayerZone(cardPlay));
            addedCount++;
        }

        if (addedCount > 0)
        {
            await PowerCmd.Apply<NunPrayerZonePower>(
                choiceContext,
                Owner.Creature,
                1m,
                Owner.Creature,
                this);
        }
    }

    protected override void OnUpgrade()
    {
    }
}
