using BaseLib.Abstracts;
using Godot;
using NightMoon.NightMoonCode.Extensions;

namespace NightMoon.NightMoonCode.Character;

public class NunCardPool : CustomCardPoolModel
{
    public override string Title => NunCharacter.CharacterId;

    public override string BigEnergyIconPath => "charui/nun_big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/nun_text_energy.png".ImagePath();

    public override float H => 1f;
    public override float S => 1f;
    public override float V => 1f;

    public override Color DeckEntryCardColor => NunCharacter.Color;

    public override bool IsColorless => false;
}
