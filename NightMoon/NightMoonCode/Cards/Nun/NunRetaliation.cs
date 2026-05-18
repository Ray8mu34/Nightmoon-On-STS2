using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunRetaliation() : NunCard(1, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
{
    public override List<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var combatState = Owner.Creature.CombatState;
        if (combatState is null)
            return;

        var lostHp = Owner.Creature.MaxHp - Owner.Creature.CurrentHp;
        var cap = IsUpgraded ? 25 : 20;
        var damage = Math.Min(lostHp, cap);

        await DamageCmd.Attack(damage)
            .FromCard(this)
            .TargetingAllOpponents(combatState)
            .WithHitFx("vfx/vfx_attack_slash_heavy")
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
    }
}
