using Godot;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Orbs;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace NightMoon.NightMoonCode.Prayer;

public static class PrayerOrbDisplay
{
    private const string NodeNamePrefix = "NightMoonPrayerOrb";
    private const float SlotGap = 72f;
    private const float YOffset = 110f;

    private static readonly Dictionary<Creature, List<NOrb>> NodesByOwner = [];

    public static void Sync(Creature owner)
    {
        var entries = PrayerManager.GetEntries(owner);
        var creatureNode = NCombatRoom.Instance?.GetCreatureNode(owner);
        var orbManager = creatureNode?.OrbManager;
        if (orbManager == null || owner.Player == null)
        {
            Clear(owner);
            return;
        }

        Clear(owner);
        if (entries.Count == 0)
            return;

        var nodes = new List<NOrb>();
        var startX = -(entries.Count - 1) * SlotGap * 0.5f;
        for (var i = 0; i < entries.Count; i++)
        {
            var model = ((NunPrayerCountdownOrb)ModelDb.Orb<NunPrayerCountdownOrb>().ToMutable())
                .Setup(entries[i]);
            model.Owner = owner.Player;

            var orb = NOrb.Create(true, model);
            orb.Name = $"{NodeNamePrefix}{i}";
            orb.Position = new Vector2(startX + i * SlotGap, YOffset);
            orb.ZIndex = 0;
            orbManager.AddChild(orb);
            nodes.Add(orb);
        }

        NodesByOwner[owner] = nodes;
    }

    public static void Clear(Creature owner)
    {
        if (!NodesByOwner.Remove(owner, out var nodes))
            return;

        foreach (var node in nodes)
        {
            if (!GodotObject.IsInstanceValid(node))
                continue;

            node.GetParent()?.RemoveChild(node);
            node.QueueFree();
        }
    }

    public static void ClearAll()
    {
        var owners = NodesByOwner.Keys.ToList();
        foreach (var owner in owners)
            Clear(owner);
    }
}
