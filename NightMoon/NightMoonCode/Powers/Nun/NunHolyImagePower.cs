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
        // 通过 CombatState 查找所有拥有此能力的 creature
        var combatState = owner.CombatState;
        if (combatState == null) return;

        foreach (var creature in combatState.PlayerCreatures)
        {
            var power = creature.GetPower<NunHolyImagePower>();
            if (power == null || power.Amount <= 0) continue;

            for (var i = 0; i < turns; i++)
            {
                power.Flash();
                await PowerCmd.Apply<NunConfessionPower>(
                    choiceContext,
                    creature,
                    1,
                    creature,
                    null,
                    silent: false);
            }
        }
    }
}
