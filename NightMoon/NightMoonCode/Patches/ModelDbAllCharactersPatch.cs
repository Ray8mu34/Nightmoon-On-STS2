using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Characters;
using NightMoon.NightMoonCode.Character;

namespace NightMoon.NightMoonCode.Patches;

[HarmonyPatch(typeof(ModelDb), nameof(ModelDb.AllCharacters), MethodType.Getter)]
public static class ModelDbAllCharactersPatch
{
    public static void Postfix(ref IEnumerable<CharacterModel> __result)
    {
        __result = __result
            .Append(ModelDb.Character<NunCharacter>())
            .Distinct();
    }
}
