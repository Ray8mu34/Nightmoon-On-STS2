using System.Collections.Generic;
using MegaCrit.Sts2.Core.Entities.Creatures;

namespace NightMoon.NightMoonCode.Prayer;

public static class DamageTracker
{
    private static readonly Dictionary<Creature, int> LastTurnDamage = new();
    private static readonly Dictionary<Creature, int> CombatDamage = new();

    public static void SetLastTurnDamage(Creature playerCreature, int damage)
    {
        LastTurnDamage[playerCreature] = damage;
    }

    public static int GetLastTurnDamage(Creature playerCreature)
    {
        return LastTurnDamage.GetValueOrDefault(playerCreature, 0);
    }

    public static void AddCombatDamage(Creature playerCreature, int damage)
    {
        if (damage <= 0)
            return;

        CombatDamage[playerCreature] = CombatDamage.GetValueOrDefault(playerCreature, 0) + damage;
    }

    public static int GetCombatDamage(Creature playerCreature)
    {
        return CombatDamage.GetValueOrDefault(playerCreature, 0);
    }

    public static void Clear()
    {
        LastTurnDamage.Clear();
        CombatDamage.Clear();
    }
}
