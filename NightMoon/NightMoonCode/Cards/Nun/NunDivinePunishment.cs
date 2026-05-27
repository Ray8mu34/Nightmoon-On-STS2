using System.Linq;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunDivinePunishment() : NunPrayerCard(2, CardType.Attack, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("PrayerTurns", 4m),
        new DynamicVar("PlayCount", 4m)
    ];

    protected override int PrayerTurns => (int)DynamicVars["PrayerTurns"].BaseValue;

    protected override PrayerEntry CreatePrayerEntry(CardPlay cardPlay)
    {
        PrayerEntry? entry = null;
        entry = new PrayerEntry(Id.Entry, PrayerTurns, async (choiceContext, owner) =>
        {
            var combatState = owner.Player?.PlayerCombatState;
            if (combatState == null)
                return;

            var attackCards = combatState.DrawPile.Cards
                .Concat(combatState.DiscardPile.Cards)
                .Where(card => card.Type == CardType.Attack)
                .Distinct()
                .ToList();

            if (attackCards.Count == 0)
                return;

            var selected = (await CardSelectCmd.FromSimpleGrid(
                    choiceContext,
                    attackCards,
                    owner.Player!,
                    new CardSelectorPrefs(SelectionScreenPrompt, 1)))
                .FirstOrDefault();

            if (selected == null)
                return;

            var playCount = (int)(DynamicVars["PlayCount"].BaseValue * (entry?.ValueMultiplier ?? 1m));
            for (var i = 0; i < playCount; i++)
            {
                var copy = owner.CombatState!.CreateCard(
                    ModelDb.GetById<CardModel>(selected.Id),
                    owner.Player!);

                for (var upgrade = 0; upgrade < selected.CurrentUpgradeLevel; upgrade++)
                    copy.UpgradeInternal();

                copy.ExhaustOnNextPlay = true;
                await CardCmd.AutoPlay(choiceContext, copy, ResolveTarget(owner, copy));
            }
        });

        return entry;
    }

    protected override void OnUpgrade()
    {
        DynamicVars["PrayerTurns"].UpgradeValueBy(-1m);
    }

    private static Creature? ResolveTarget(Creature owner, CardModel card)
    {
        if (card.TargetType != TargetType.AnyEnemy)
            return null;

        return owner.CombatState?.HittableEnemies.FirstOrDefault();
    }
}
