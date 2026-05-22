using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Random;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunDoomPrayer() : NunPrayerCard(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<WeakPower>(2m)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<WeakPower>()
    ];

    protected override int PrayerTurns => PrayerTier;
    protected override int MaxPrayerTier => 3;
    protected override LocString PrayerChoiceDescription =>
        new("cards", $"{Id.Entry}.prayerChoice.{PrayerTier}{(IsUpgraded ? ".upgraded" : "")}");

    protected override PrayerEntry CreatePrayerEntry(CardPlay cardPlay)
    {
        var target = cardPlay.Target;

        PrayerEntry? entry = null;
        entry = new PrayerEntry(Id.Entry, PrayerTurns, async (context, owner) =>
        {
            var resolvedTarget = ResolveTarget(owner, target);
            if (resolvedTarget is null)
                return;

            var multiplier = entry?.ValueMultiplier ?? 1m;
            await PowerCmd.Apply<WeakPower>(
                context,
                resolvedTarget,
                DynamicVars[typeof(WeakPower).Name].BaseValue * multiplier,
                owner,
                this);

            if (PrayerTier >= 2)
            {
                await PowerCmd.Apply<VulnerablePower>(
                    context,
                    resolvedTarget,
                    (PrayerTier - 1) * multiplier,
                    owner,
                    this);
            }
        });

        return entry;
    }

    protected override void OnUpgrade()
    {
    }

    private static Creature? ResolveTarget(Creature owner, Creature? preferredTarget)
    {
        var combatState = owner.CombatState;
        if (combatState is null)
            return null;

        var enemies = combatState.HittableEnemies.ToList();
        if (preferredTarget is not null && enemies.Contains(preferredTarget))
            return preferredTarget;

        if (enemies.Count == 0)
            return null;

        var index = owner.Player?.RunState?.Rng.Niche.NextInt(enemies.Count) ?? Rng.Chaotic.NextInt(enemies.Count);
        return enemies[index];
    }
}
