using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using NightMoon.NightMoonCode.Character;
using NightMoon.NightMoonCode.Extensions;

namespace NightMoon.NightMoonCode.Potions.Nun;

[Pool(typeof(NunPotionPool))]
public abstract class NunPotion : CustomPotionModel
{
    public override string? CustomPackedImagePath => $"potions/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".ImagePath();
    public override string? CustomPackedOutlinePath => $"potions/{Id.Entry.RemovePrefix().ToLowerInvariant()}_outline.png".ImagePath();
}
