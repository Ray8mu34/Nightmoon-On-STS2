using BaseLib.Abstracts;
using Godot;
using NightMoon.NightMoonCode.Extensions;

namespace NightMoon.NightMoonCode.Character;

public class NunCardPool : CustomCardPoolModel
{
    private static readonly Color NunFrameColor = new(0.14509805f, 0.11764706f, 0.1882353f);

    public override string Title => NunCharacter.CharacterId;

    public override string BigEnergyIconPath => "charui/nun_big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/nun_text_energy.png".ImagePath();

    public override Color DeckEntryCardColor => NunFrameColor;
    public override Color ShaderColor => NunFrameColor;

    public override bool IsColorless => false;
}
