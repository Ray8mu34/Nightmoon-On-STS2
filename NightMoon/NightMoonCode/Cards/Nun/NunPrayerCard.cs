using System;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using NightMoon.NightMoonCode.Powers.Nun;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public abstract class NunPrayerCard(int cost, CardType type, CardRarity rarity, TargetType target) :
    NunCard(cost, type, rarity, target)
{
    protected abstract int PrayerTurns { get; }

    /// <summary>
    /// Prayer tier (usually 1-4), controlling the delay and effect strength.
    /// Subclasses can override the min/max tier to support multiple choices.
    /// </summary>
    protected virtual int PrayerTier => _prayerTier;
    protected virtual int MinPrayerTier => 1;
    protected virtual int MaxPrayerTier => 1;
    protected virtual LocString PrayerChoiceDescription => Description;
    protected virtual LocString NormalPrayerDescription => new("cards", $"{Id.Entry}.normalDescription");

    public void SetPrayerTier(int tier)
    {
        _prayerTier = Math.Clamp(tier, MinPrayerTier, MaxPrayerTier);
    }

    private int _prayerTier = 1;
    private bool _isPrayerChoice;

    public override List<CardKeyword> CanonicalKeywords => [NunKeywords.Prayer];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<NunPrayerZonePower>()
    ];

    protected sealed override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ChoosePrayerTier(choiceContext);

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

        entry.SetSourceCard(this);
        entry.SetEffectDescription(GetPrayerEffectDescription());

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

    private async Task ChoosePrayerTier(PlayerChoiceContext choiceContext)
    {
        if (MaxPrayerTier <= MinPrayerTier)
        {
            SetPrayerTier(MinPrayerTier);
            return;
        }

        var choices = new List<CardModel>();
        for (var tier = MinPrayerTier; tier <= MaxPrayerTier; tier++)
        {
            var choice = (NunPrayerCard)ClonePreservingMutability();
            choice.SetPrayerTier(tier);
            choice._isPrayerChoice = true;
            choices.Add(choice);
        }

        var selected = await CardSelectCmd.FromSimpleGrid(
            choiceContext,
            choices,
            Owner,
            new CardSelectorPrefs(new LocString("cards", "NIGHTMOON-PRAYER_TIER.selectionScreenPrompt"), 1));

        if (selected.FirstOrDefault() is NunPrayerCard prayerChoice)
            SetPrayerTier(prayerChoice.PrayerTier);
    }

    public PrayerEntry CreatePrayerEntryForPrayerZone(CardPlay cardPlay)
    {
        var entry = CreatePrayerEntry(cardPlay);
        entry.SetSourceCard(this);
        entry.SetEffectDescription(GetPrayerEffectDescription());
        return entry;
    }

    protected PrayerEntry CreatePrayerEntry(string sourceId, Func<PlayerChoiceContext, Task> resolve)
    {
        return new PrayerEntry(sourceId, PrayerTurns, async (choiceContext, _) => await resolve(choiceContext));
    }

    private string GetPrayerEffectDescription()
    {
        var prayerChoiceDescription = PrayerChoiceDescription;
        AddExtraArgsToPrayerText(prayerChoiceDescription);
        var text = prayerChoiceDescription.GetFormattedText().Trim();
        var separator = text.IndexOf('：');
        if (separator < 0)
            separator = text.IndexOf(':');

        return separator >= 0
            ? text[(separator + 1)..].Trim()
            : text;
    }

    protected override void AddExtraArgsToDescription(LocString description)
    {
        base.AddExtraArgsToDescription(description);
        var normalDescription = NormalPrayerDescription;
        var prayerChoiceDescription = PrayerChoiceDescription;
        AddExtraArgsToPrayerText(normalDescription);
        AddExtraArgsToPrayerText(prayerChoiceDescription);
        description.Add("IsPrayerChoice", _isPrayerChoice);
        description.Add("PrayerText", _isPrayerChoice
            ? prayerChoiceDescription.GetFormattedText()
            : normalDescription.Exists()
                ? normalDescription.GetFormattedText()
                : "");
    }

    protected virtual void AddExtraArgsToPrayerText(LocString text)
    {
        DynamicVars.AddTo(text);
    }
}
