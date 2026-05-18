using System.Linq;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunDivinePunishment() : NunPrayerCard(2, CardType.Attack, CardRarity.Rare, TargetType.Self)
{
    public override List<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override int PrayerTurns => IsUpgraded ? 3 : 4;

    protected override PrayerEntry CreatePrayerEntry(CardPlay cardPlay)
    {
        return new PrayerEntry(Id.Entry, PrayerTurns, async (choiceContext, owner) =>
        {
            var selected = (await CardSelectCmd.FromDeckGeneric(
                    owner.Player,
                    new CardSelectorPrefs(SelectionScreenPrompt, 1),
                    card => card.Type == CardType.Attack))
                .FirstOrDefault();

            if (selected == null)
                return;

            for (var i = 0; i < 4; i++)
            {
                var copy = owner.CombatState!.CreateCard(
                    ModelDb.GetById<CardModel>(selected.Id),
                    owner.Player);

                for (var upgrade = 0; upgrade < selected.CurrentUpgradeLevel; upgrade++)
                    copy.UpgradeInternal();

                copy.ExhaustOnNextPlay = true;
                await CardCmd.AutoPlay(choiceContext, copy, ResolveTarget(owner, copy));
            }
        });
    }

    protected override void OnUpgrade()
    {
    }

    private static Creature? ResolveTarget(Creature owner, CardModel card)
    {
        if (card.TargetType != TargetType.AnyEnemy)
            return null;

        return owner.CombatState?.HittableEnemies.FirstOrDefault();
    }
}
