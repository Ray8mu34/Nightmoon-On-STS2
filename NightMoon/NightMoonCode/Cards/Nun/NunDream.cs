using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunDream() : NunCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var card = CreateRandomPrayerCard();
        if (card != null)
            await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, Owner);
    }

    private CardModel? CreateRandomPrayerCard()
    {
        var options = new Func<CardModel>[]
        {
            () => CombatState.CreateCard(ModelDb.Card<NunAttackPrayer>(), Owner),
            () => CombatState.CreateCard(ModelDb.Card<NunAngryPrayer>(), Owner),
            () => CombatState.CreateCard(ModelDb.Card<NunDoomPrayer>(), Owner),
            () => CombatState.CreateCard(ModelDb.Card<NunLifePrayer>(), Owner),
            () => CombatState.CreateCard(ModelDb.Card<NunManaPrayer>(), Owner),
            () => CombatState.CreateCard(ModelDb.Card<NunRepentPrayer>(), Owner),
            () => CombatState.CreateCard(ModelDb.Card<NunRetreatPrayer>(), Owner),
            () => CombatState.CreateCard(ModelDb.Card<NunShieldPrayer>(), Owner),
            () => CombatState.CreateCard(ModelDb.Card<NunSkyfirePrayer>(), Owner),
            () => CombatState.CreateCard(ModelDb.Card<NunSwiftPrayer>(), Owner)
        };

        return Owner.RunState.Rng.CombatCardSelection.NextItem(options)();
    }

    protected override void OnUpgrade()
    {
        EnergyCost.SetCustomBaseCost(0);
    }
}
