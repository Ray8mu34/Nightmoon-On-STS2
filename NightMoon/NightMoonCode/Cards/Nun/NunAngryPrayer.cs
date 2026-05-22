using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.ValueProps;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunAngryPrayer() : NunPrayerCard(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(8m, ValueProp.Move)
    ];

    protected override int PrayerTurns => PrayerTier;
    protected override int MaxPrayerTier => 4;
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

            await CreatureCmd.Damage(
                context,
                resolvedTarget,
                CalculateDamage() * (entry?.ValueMultiplier ?? 1m),
                ValueProp.Unblockable | ValueProp.Unpowered,
                owner,
                this);
        });

        return entry;
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2m);
    }

    protected override void AddExtraArgsToPrayerText(LocString text)
    {
        base.AddExtraArgsToPrayerText(text);
        text.Add("Value1", CalculateDamage(1));
        text.Add("Value2", CalculateDamage(2));
        text.Add("Value3", CalculateDamage(3));
        text.Add("Value4", CalculateDamage(4));
        text.Add("ChoiceValue", CalculateDamage(PrayerTier));
    }

    private decimal CalculateDamage() => CalculateDamage(PrayerTier);

    private decimal CalculateDamage(int tier)
    {
        var normal = tier switch
        {
            1 => 8m,
            2 => 12m,
            3 => 16m,
            _ => 21m
        };

        var upgraded = tier switch
        {
            1 => 10m,
            2 => 15m,
            3 => 20m,
            _ => 25m
        };

        return IsUpgraded ? upgraded : normal;
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
