using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using NightMoon.NightMoonCode.Extensions;

namespace NightMoon.NightMoonCode.Prayer;

public class NunPrayerCountdownOrb() : CustomOrbModel
{
    private int _remainingTurns = 1;
    private CardModel? _sourceCard;
    private string _effectDescription = "";

    public override string? CustomIconPath => _sourceCard?.PortraitPath ?? "nun_prayer_zone_power.png".PowerImagePath();
    public override Color DarkenedColor => new("7c5aa6");
    public override decimal PassiveVal => _remainingTurns;
    public override decimal EvokeVal => _remainingTurns;
    public override List<(string, string)> Localization => new OrbLoc("祷告", "", "");

    public IEnumerable<IHoverTip> PrayerHoverTips
    {
        get
        {
            var title = new LocString("cards", "NIGHTMOON-PRAYER_ORB.title");
            title.Add("Turns", _remainingTurns);

            var description = string.IsNullOrWhiteSpace(_effectDescription)
                ? new LocString("cards", "NIGHTMOON-PRAYER_ORB.unknownDescription").GetFormattedText()
                : _effectDescription;

            return [new HoverTip(title, description, _sourceCard?.Portrait)];
        }
    }

    public NunPrayerCountdownOrb Setup(PrayerEntry entry)
    {
        _remainingTurns = Math.Max(0, entry.RemainingTurns);
        _sourceCard = entry.SourceCard;
        _effectDescription = entry.EffectDescription ?? "";
        return this;
    }

    public override Node2D? CreateCustomSprite()
    {
        var root = new Node2D();
        var sprite = new Sprite2D
        {
            Texture = ResourceLoader.Load<Texture2D>(CustomIconPath),
            Centered = true,
            Scale = Vector2.One * 0.18f
        };
        root.AddChild(sprite);
        return root;
    }
}
