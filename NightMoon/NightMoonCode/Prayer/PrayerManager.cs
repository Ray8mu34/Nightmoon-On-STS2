using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace NightMoon.NightMoonCode.Prayer;

public static class PrayerManager
{
    private static readonly Dictionary<Creature, List<PrayerEntry>> EntriesByOwner = [];

    /// <summary>
    /// 当祷告计时减少时触发（参数：owner creature, 减少的次数）
    /// </summary>
    public static event Func<PlayerChoiceContext, Creature, int, Task>? OnPrayerTimerAdvanced;
    public static event Func<PlayerChoiceContext, Creature, PrayerEntry, Task>? OnPrayerResolved;

    public static async Task<int> Add(
        PlayerChoiceContext choiceContext,
        Creature owner,
        PrayerEntry entry)
    {
        if (entry.RemainingTurns <= 0)
        {
            await NotifyPrayerResolved(choiceContext, owner, entry);
            await entry.Resolve(choiceContext, owner);
            return 0;
        }

        EntriesFor(owner).Add(entry);
        PrayerOrbDisplay.Sync(owner);
        return Count(owner);
    }

    public static async Task<int> Tick(PlayerChoiceContext choiceContext, Creature owner)
    {
        return await Advance(choiceContext, owner, 1);
    }

    public static async Task<int> Accelerate(PlayerChoiceContext choiceContext, Creature owner, int turns)
    {
        return await Advance(choiceContext, owner, turns);
    }

    private static async Task<int> Advance(PlayerChoiceContext choiceContext, Creature owner, int turns)
    {
        if (!EntriesByOwner.TryGetValue(owner, out var entries))
            return 0;

        var ready = new List<PrayerEntry>();
        var totalTimerDecrements = 0;
        for (var i = 0; i < turns; i++)
        {
            foreach (var entry in entries)
            {
                if (entry.RemainingTurns > 0)
                    totalTimerDecrements++;

                if (entry.Tick() && !ready.Contains(entry))
                    ready.Add(entry);
            }
        }

        if (totalTimerDecrements > 0)
        {
            try
            {
                await NotifyPrayerTimerAdvanced(choiceContext, owner, totalTimerDecrements);
            }
            catch
            {
                // swallow to prevent prayer zone from getting stuck
            }
        }

        foreach (var entry in ready)
        {
            entries.Remove(entry);
            try
            {
                await NotifyPrayerResolved(choiceContext, owner, entry);
                await entry.Resolve(choiceContext, owner);
            }
            catch
            {
                // swallow to prevent prayer zone from getting stuck
            }
        }

        if (entries.Count == 0)
        {
            EntriesByOwner.Remove(owner);
            PrayerOrbDisplay.Clear(owner);
        }
        else
        {
            PrayerOrbDisplay.Sync(owner);
        }

        return ready.Count;
    }

    public static int Count(Creature owner)
    {
        return EntriesByOwner.TryGetValue(owner, out var entries) ? entries.Count : 0;
    }

    public static void ClearAll()
    {
        EntriesByOwner.Clear();
        PrayerOrbDisplay.ClearAll();
    }

    public static IReadOnlyList<PrayerEntry> GetEntries(Creature owner)
    {
        return EntriesByOwner.TryGetValue(owner, out var entries) ? entries.ToList() : [];
    }

    private static List<PrayerEntry> EntriesFor(Creature owner)
    {
        if (!EntriesByOwner.TryGetValue(owner, out var entries))
        {
            entries = [];
            EntriesByOwner[owner] = entries;
        }

        return entries;
    }

    /// <summary>
    /// 修改指定玩家的所有祷告条目（用于悼唁等效果）
    /// </summary>
    public static void ModifyAllEntries(Creature owner, Action<PrayerEntry> modifier)
    {
        if (!EntriesByOwner.TryGetValue(owner, out var entries))
            return;

        foreach (var entry in entries)
            modifier(entry);

        PrayerOrbDisplay.Sync(owner);
    }

    private static async Task NotifyPrayerResolved(PlayerChoiceContext choiceContext, Creature owner, PrayerEntry entry)
    {
        if (OnPrayerResolved == null)
            return;

        foreach (Func<PlayerChoiceContext, Creature, PrayerEntry, Task> handler in OnPrayerResolved.GetInvocationList())
            await handler(choiceContext, owner, entry);
    }

    private static async Task NotifyPrayerTimerAdvanced(PlayerChoiceContext choiceContext, Creature owner, int turns)
    {
        if (OnPrayerTimerAdvanced == null)
            return;

        foreach (Func<PlayerChoiceContext, Creature, int, Task> handler in OnPrayerTimerAdvanced.GetInvocationList())
            await handler(choiceContext, owner, turns);
    }
}
