using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunMirror() : NunCard(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var playerCombatState = Owner.PlayerCombatState;
        if (playerCombatState is null || CombatState is null)
            return;

        var hand = playerCombatState.Hand.Cards;
        if (hand.Count == 0)
            return;

        var leftmost = hand[0];
        var clone = CombatState.CloneCard(leftmost);
        if (IsUpgraded)
            clone.SetToFreeThisTurn();

        await CardPileCmd.Add(clone, PileType.Hand, CardPilePosition.Bottom, this, false);
    }

    protected override void OnUpgrade()
    {
        // Upgraded copies cost 0 this turn, matching Discovery's generated-card handling.
    }
}
