using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Cards.Nun;

public class NunDevourPrayer() : NunPrayerCard(1, CardType.Skill, CardRarity.Rare, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<StrengthPower>(1m)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<StrengthPower>()
    ];

    protected override int PrayerTurns => 1;

    protected override PrayerEntry CreatePrayerEntry(CardPlay cardPlay)
    {
        return new PrayerEntry(Id.Entry, PrayerTurns, async (choiceContext, owner) =>
        {
            var enemies = owner.CombatState?.HittableEnemies;
            if (enemies == null)
                return;

            foreach (var enemy in enemies)
            {
                await PowerCmd.Apply<StrengthPower>(
                    choiceContext,
                    enemy,
                    -DynamicVars[typeof(StrengthPower).Name].BaseValue,
                    owner,
                    this);
            }
        });
    }

    protected override void OnUpgrade()
    {
        EnergyCost.SetCustomBaseCost(0);
    }
}
