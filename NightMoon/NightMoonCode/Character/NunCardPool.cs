using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Entities.Cards;
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

    public override Texture2D? CustomFrame(CustomCardModel card)
    {
        return card.Type switch
        {
            CardType.Attack => ResourceLoader.Load<Texture2D>("card_frames/nun_attack_card_frame.png".ImagePath()),
            CardType.Skill => ResourceLoader.Load<Texture2D>("card_frames/nun_skill_card_frame.png".ImagePath()),
            CardType.Power => ResourceLoader.Load<Texture2D>("card_frames/nun_power_card_frame.png".ImagePath()),
            _ => null
        };
    }
}
