using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunGospel() : NunCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    public override List<CardKeyword> CanonicalKeywords =>
        IsUpgraded ? [CardKeyword.Retain] : [];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var resolved = new List<PrayerEntry>();

        async Task TrackResolved(PlayerChoiceContext _, MegaCrit.Sts2.Core.Entities.Creatures.Creature owner, PrayerEntry entry)
        {
            if (owner == Owner.Creature)
                resolved.Add(entry);

            await Task.CompletedTask;
        }

        PrayerManager.OnPrayerResolved += TrackResolved;
        try
        {
            await PrayerManager.Accelerate(choiceContext, Owner.Creature, 2);
        }
        finally
        {
            PrayerManager.OnPrayerResolved -= TrackResolved;
        }

        foreach (var entry in resolved)
        {
            var copy = CreateCopy(entry.SourceCard);
            if (copy != null)
                await CardPileCmd.AddGeneratedCardToCombat(copy, PileType.Hand, Owner);
        }
    }

    private CardModel? CreateCopy(CardModel? sourceCard)
    {
        if (sourceCard == null)
            return null;

        var copy = CombatState.CreateCard(ModelDb.GetById<CardModel>(sourceCard.Id), Owner);
        for (var i = 0; i < sourceCard.CurrentUpgradeLevel; i++)
        {
            copy.UpgradeInternal();
            copy.FinalizeUpgradeInternal();
        }

        return copy;
    }

    protected override void OnUpgrade()
    {
    }
}
