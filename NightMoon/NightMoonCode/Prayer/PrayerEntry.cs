using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace NightMoon.NightMoonCode.Prayer;

public sealed class PrayerEntry(
    string sourceId,
    int remainingTurns,
    Func<PlayerChoiceContext, Creature, Task> resolve)
{
    public string SourceId { get; } = sourceId;
    public int RemainingTurns { get; set; } = remainingTurns;
    public decimal ValueMultiplier { get; set; } = 1m;
    public Func<PlayerChoiceContext, Creature, Task> ResolveFunc { get; } = resolve;
    public CardModel? SourceCard { get; private set; }
    public string? EffectDescription { get; private set; }

    public bool Tick()
    {
        if (RemainingTurns > 0)
            RemainingTurns--;

        return RemainingTurns <= 0;
    }

    public Task Resolve(PlayerChoiceContext choiceContext, Creature owner)
    {
        return resolve(choiceContext, owner);
    }

    public void SetSourceCard(CardModel sourceCard)
    {
        SourceCard = sourceCard;
    }

    public void SetEffectDescription(string effectDescription)
    {
        EffectDescription = effectDescription;
    }

    public PrayerEntry Clone()
    {
        var clone = new PrayerEntry(SourceId, RemainingTurns, ResolveFunc)
        {
            ValueMultiplier = ValueMultiplier
        };

        if (SourceCard != null)
            clone.SetSourceCard(SourceCard);

        if (EffectDescription != null)
            clone.SetEffectDescription(EffectDescription);

        return clone;
    }
}
