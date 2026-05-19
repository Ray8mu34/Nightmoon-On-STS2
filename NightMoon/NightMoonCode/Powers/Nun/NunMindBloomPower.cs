using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using NightMoon.NightMoonCode.Cards.Nun;

namespace NightMoon.NightMoonCode.Powers.Nun;

public class NunMindBloomPower() : NunPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override int ModifyCardPlayCount(CardModel card, Creature? target, int playCount)
    {
        if (card is not NunPrayerCard || card.Owner?.Creature != Owner || Amount <= 0)
            return playCount;

        return playCount + Amount;
    }
}
