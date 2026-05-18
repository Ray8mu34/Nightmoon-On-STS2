using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace NightMoon.NightMoonCode.Prayer;

public sealed class PrayerEntry(
    string sourceId,
    int remainingTurns,
    Func<PlayerChoiceContext, Creature, Task> resolve)
{
    public string SourceId { get; } = sourceId;
    public int RemainingTurns { get; set; } = remainingTurns;
    public Func<PlayerChoiceContext, Creature, Task> ResolveFunc { get; } = resolve;

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
}
