using MegaCrit.Sts2.Core.Entities.Powers;

namespace NightMoon.NightMoonCode.Powers.Nun;

public class NunShortcutPower() : NunPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
}
