using System.Collections.Generic;
using MegaCrit.Sts2.Core.Entities.Creatures;

namespace NightMoon.NightMoonCode.Prayer;

/// <summary>
/// 追踪每个玩家上回合造成的总伤害，用于正义天平等卡牌效果。
/// 由 CombatDamageTrackerSingleton 在每回合开始时更新。
/// </summary>
public static class DamageTracker
{
    private static readonly Dictionary<Creature, int> LastTurnDamage = new();

    public static void SetLastTurnDamage(Creature playerCreature, int damage)
    {
        LastTurnDamage[playerCreature] = damage;
    }

    public static int GetLastTurnDamage(Creature playerCreature)
    {
        return LastTurnDamage.GetValueOrDefault(playerCreature, 0);
    }

    public static void Clear()
    {
        LastTurnDamage.Clear();
    }
}
