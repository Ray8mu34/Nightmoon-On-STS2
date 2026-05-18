using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunJudgement() : NunCard(3, CardType.Attack, CardRarity.Ancient, TargetType.AllEnemies)
{
    public override List<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var combatState = Owner.Creature.CombatState;
        if (combatState is null)
            return;

        var lastTurnDamage = DamageTracker.GetLastTurnDamage(Owner.Creature);
        var damage = (int)(lastTurnDamage * 0.2m);
        damage = Math.Max(damage, 0);

        await DamageCmd.Attack(damage)
            .FromCard(this)
            .TargetingAllOpponents(combatState)
            .WithHitFx("vfx/vfx_attack_slash_heavy")
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.SetCustomBaseCost(2);
    }
}
