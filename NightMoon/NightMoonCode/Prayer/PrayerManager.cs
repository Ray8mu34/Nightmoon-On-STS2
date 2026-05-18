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
    public static event Action<Creature, int>? OnPrayerTimerAdvanced;
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
        for (var i = 0; i < turns; i++)
        {
            ready.AddRange(entries.Where(entry => entry.Tick()).Where(entry => !ready.Contains(entry)));
        }

        if (entries.Count > 0 || ready.Count > 0)
            OnPrayerTimerAdvanced?.Invoke(owner, turns);

        foreach (var entry in ready)
        {
            entries.Remove(entry);
            await NotifyPrayerResolved(choiceContext, owner, entry);
            await entry.Resolve(choiceContext, owner);
        }

        if (entries.Count == 0)
            EntriesByOwner.Remove(owner);

        return ready.Count;
    }

    public static int Count(Creature owner)
    {
        return EntriesByOwner.TryGetValue(owner, out var entries) ? entries.Count : 0;
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
    }

    private static async Task NotifyPrayerResolved(PlayerChoiceContext choiceContext, Creature owner, PrayerEntry entry)
    {
        if (OnPrayerResolved == null)
            return;

        foreach (Func<PlayerChoiceContext, Creature, PrayerEntry, Task> handler in OnPrayerResolved.GetInvocationList())
            await handler(choiceContext, owner, entry);
    }
}
