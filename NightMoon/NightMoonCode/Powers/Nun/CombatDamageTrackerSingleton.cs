using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Powers.Nun;

/// <summary>
/// 战斗单例：通过 AfterDamageGiven 追踪每回合总伤害，供正义天平等卡牌使用。
/// </summary>
public class CombatDamageTrackerSingleton() : CustomSingletonModel(true, false)
{
    private int currentTurnDamage;

    public override Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        // 回合开始时：将当前伤害归档为上回合伤害，重置
        DamageTracker.SetLastTurnDamage(player.Creature, currentTurnDamage);
        currentTurnDamage = 0;
        return Task.CompletedTask;
    }

    public override Task AfterDamageGiven(
        PlayerChoiceContext choiceContext,
        Creature? dealer,
        DamageResult result,
        ValueProp props,
        Creature target,
        CardModel? cardSource)
    {
        if (dealer == null || result.UnblockedDamage <= 0)
            return Task.CompletedTask;

        // 只统计玩家造成的伤害
        if (dealer.IsPlayer || dealer.PetOwner != null)
        {
            currentTurnDamage += result.UnblockedDamage;
        }

        return Task.CompletedTask;
    }
}
