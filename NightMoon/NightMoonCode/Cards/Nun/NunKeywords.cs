using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace NightMoon.NightMoonCode.Cards.Nun;

public static class NunKeywords
{
    [CustomEnum("NUN_PRAYER"), KeywordProperties(AutoKeywordPosition.Before)]
    public static CardKeyword Prayer;
}
