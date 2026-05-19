using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using NightMoon.NightMoonCode.Prayer;

namespace NightMoon.NightMoonCode.Powers.Nun;

public class NunHolyImagePower() : NunPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    static NunHolyImagePower()
    {
        PrayerManager.OnPrayerTimerAdvanced += OnPrayerTimerAdvanced;
    }

    private static async Task OnPrayerTimerAdvanced(PlayerChoiceContext choiceContext, Creature owner, int turns)
    {
        var power = owner.GetPower<NunHolyImagePower>();
        if (power == null || power.Amount <= 0 || turns <= 0)
            return;

        power.Flash();
        await PowerCmd.Apply<NunConfessionPower>(
            choiceContext,
            owner,
            power.Amount * turns,
            owner,
            null,
            silent: false);
    }
}
