using BaseLib.Abstracts;
using Godot;
using NightMoon.NightMoonCode.Extensions;

namespace NightMoon.NightMoonCode.Character;

public class NunCardPool : CustomCardPoolModel
{
    private static readonly Color NunBrandColor = new("251e30");

    public override string Title => NunCharacter.CharacterId;

    public override string BigEnergyIconPath => "charui/nun_big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/nun_text_energy.png".ImagePath();

    public override Color DeckEntryCardColor => NunBrandColor;
    public override float H => 0.7135417f;
    public override float S => 0.46376812f;
    public override float V => 0.5411765f;

    public override bool IsColorless => false;
}
