using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Powers.Nun;

public class NunHolyAreaPower() : NunPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        PrayerManager.OnPrayerResolved += OnPrayerResolved;
        return Task.CompletedTask;
    }

    public override Task AfterRemoved(Creature oldOwner)
    {
        PrayerManager.OnPrayerResolved -= OnPrayerResolved;
        return Task.CompletedTask;
    }

    private async Task OnPrayerResolved(PlayerChoiceContext choiceContext, Creature owner, PrayerEntry entry)
    {
        if (owner != Owner)
            return;

        Flash();
        await CreatureCmd.GainBlock(Owner, Amount, ValueProp.Unpowered, null, false);
    }
}
