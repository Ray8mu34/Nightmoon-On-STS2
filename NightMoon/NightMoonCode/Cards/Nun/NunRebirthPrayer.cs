using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunRebirthPrayer() : NunPrayerCard(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    protected override int PrayerTurns => 2;

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("Create", 3m)
    ];

    protected override PrayerEntry CreatePrayerEntry(CardPlay cardPlay)
    {
        PrayerEntry? entry = null;
        entry = new PrayerEntry(Id.Entry, PrayerTurns, async (choiceContext, owner) =>
        {
            var count = (int)(DynamicVars["Create"].BaseValue * (entry?.ValueMultiplier ?? 1m));
            for (var i = 0; i < count; i++)
            {
                var prayer = CreateRandomPrayerCard();
                var generatedEntry = prayer.CreatePrayerEntryForPrayerZone(cardPlay);
                await PrayerManager.Add(choiceContext, owner, generatedEntry);
            }
        });

        return entry;
    }

    private NunPrayerCard CreateRandomPrayerCard()
    {
        var options = new Func<NunPrayerCard>[]
        {
            () => (NunPrayerCard)CombatState.CreateCard(ModelDb.Card<NunAttackPrayer>(), Owner),
            () => (NunPrayerCard)CombatState.CreateCard(ModelDb.Card<NunAngryPrayer>(), Owner),
            () => (NunPrayerCard)CombatState.CreateCard(ModelDb.Card<NunDoomPrayer>(), Owner),
            () => (NunPrayerCard)CombatState.CreateCard(ModelDb.Card<NunLifePrayer>(), Owner),
            () => (NunPrayerCard)CombatState.CreateCard(ModelDb.Card<NunManaPrayer>(), Owner),
            () => (NunPrayerCard)CombatState.CreateCard(ModelDb.Card<NunRepentPrayer>(), Owner),
            () => (NunPrayerCard)CombatState.CreateCard(ModelDb.Card<NunRetreatPrayer>(), Owner),
            () => (NunPrayerCard)CombatState.CreateCard(ModelDb.Card<NunShieldPrayer>(), Owner),
            () => (NunPrayerCard)CombatState.CreateCard(ModelDb.Card<NunSkyfirePrayer>(), Owner),
            () => (NunPrayerCard)CombatState.CreateCard(ModelDb.Card<NunSwiftPrayer>(), Owner)
        };

        return Owner.RunState.Rng.CombatCardSelection.NextItem(options)();
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Create"].UpgradeValueBy(1m);
    }
}
