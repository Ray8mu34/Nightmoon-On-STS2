using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes.Combat;
using NightMoon.NightMoonCode.Cards.Nun;
using NightMoon.NightMoonCode.Extensions;
using NightMoon.NightMoonCode.Relics.Nun;

namespace NightMoon.NightMoonCode.Character;

public class NunCharacter : PlaceholderCharacterModel
{
    public const string CharacterId = "Nun";

    public static readonly Color Color = new("d8edf4");

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Feminine;
    public override int StartingHp => 70;

    public override IEnumerable<CardModel> StartingDeck => [
        ModelDb.Card<NunStrike>(),
        ModelDb.Card<NunStrike>(),
        ModelDb.Card<NunStrike>(),
        ModelDb.Card<NunStrike>(),
        ModelDb.Card<NunDefend>(),
        ModelDb.Card<NunDefend>(),
        ModelDb.Card<NunDefend>(),
        ModelDb.Card<NunDefend>(),
        ModelDb.Card<NunConfession>(),
        ModelDb.Card<NunAttackPrayer>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<NunConfessionHeart>()
    ];

    public override CardPoolModel CardPool => ModelDb.CardPool<NunCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<NunRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<NunPotionPool>();
    public override CustomEnergyCounter? CustomEnergyCounter => new(
        layer => $"energy_counter/nun_energy_layer_{layer}.png".ImagePath(),
        new Color("251e30"),
        new Color("d8edf4"));

    public override Control CustomIcon
    {
        get
        {
            var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
            icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
            return icon;
        }
    }

    public override string CustomIconTexturePath => "nun_character_icon.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "nun_character_select.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "nun_character_select_locked.png".CharacterUiPath();
    public override string CustomCharacterSelectBg => Path.Join(MainFile.ResPath, "scenes", "screens", "char_select", "nun_character_select_bg.tscn");
    public override string CustomMapMarkerPath => "nun_map_marker.png".CharacterUiPath();

    public override NCreatureVisuals? CreateCustomVisuals()
    {
        var texture = ResourceLoader.Load<Texture2D>("nun_battle.png".CharacterUiPath());
        return NodeFactory<NCreatureVisuals>.CreateFromResource(texture);
    }
}
