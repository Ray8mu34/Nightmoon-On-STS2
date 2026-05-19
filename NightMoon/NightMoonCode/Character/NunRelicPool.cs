using BaseLib.Abstracts;
using Godot;
using NightMoon.NightMoonCode.Extensions;

namespace NightMoon.NightMoonCode.Character;

public class NunRelicPool : CustomRelicPoolModel
{
    public override Color LabOutlineColor => NunCharacter.Color;

    public override string BigEnergyIconPath => "charui/nun_big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/nun_text_energy.png".ImagePath();
}
