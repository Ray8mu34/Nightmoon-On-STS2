using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace NightMoon.NightMoonCode.Relics.Nun;

public class NunBlessingStatue() : NunRelic
{
    public override RelicRarity Rarity => RelicRarity.Shop;

    private static readonly SpireField<Creature, bool> AppliedThisCombat = new(() => false);

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner)
            return;

        if (AppliedThisCombat.Get(Owner.Creature))
            return;

        AppliedThisCombat.Set(Owner.Creature, true);
        Flash();
        await PowerCmd.Apply<ArtifactPower>(
            choiceContext,
            Owner.Creature,
            2m,
            Owner.Creature,
            null);
    }
}
