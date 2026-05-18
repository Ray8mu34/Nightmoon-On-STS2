using System;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using NightMoon.NightMoonCode.Powers.Nun;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public abstract class NunPrayerCard(int cost, CardType type, CardRarity rarity, TargetType target) :
    NunCard(cost, type, rarity, target)
{
    protected abstract int PrayerTurns { get; }

    /// <summary>
    /// 祷告档位（1-4），影响祷告回合数和效果强度。默认为1。
    /// 子类可重写以支持多档选择。
    /// </summary>
    protected virtual int PrayerTier => _prayerTier;

    public void SetPrayerTier(int tier)
    {
        _prayerTier = Math.Clamp(tier, 1, 4);
    }

    private int _prayerTier = 1;

    public override List<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<NunPrayerZonePower>()
    ];

    protected sealed override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var entry = CreatePrayerEntry(cardPlay);
        var manaSurge = Owner.Creature.GetPower<NunManaSurgePower>();
        if (manaSurge is { Amount: > 0 })
            entry.RemainingTurns += (int)manaSurge.Amount;

        var shortcut = Owner.Creature.GetPower<NunShortcutPower>();
        if (shortcut is { Amount: > 0 })
        {
            entry.RemainingTurns = Math.Max(0, entry.RemainingTurns - 1);
            await PowerCmd.Decrement(shortcut);
        }

        await PrayerManager.Add(
            choiceContext,
            Owner.Creature,
            entry);

        await PowerCmd.Apply<NunPrayerZonePower>(
            choiceContext,
            Owner.Creature,
            1m,
            Owner.Creature,
            this);
    }

    protected abstract PrayerEntry CreatePrayerEntry(CardPlay cardPlay);

    public PrayerEntry CreatePrayerEntryForPrayerZone(CardPlay cardPlay)
    {
        return CreatePrayerEntry(cardPlay);
    }

    protected PrayerEntry CreatePrayerEntry(string sourceId, Func<PlayerChoiceContext, Task> resolve)
    {
        return new PrayerEntry(sourceId, PrayerTurns, async (choiceContext, _) => await resolve(choiceContext));
    }
}
