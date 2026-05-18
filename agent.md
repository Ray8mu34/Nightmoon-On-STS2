# Agent 工作指南

> 本文档指导 Agent 如何在此项目中高效工作。

---

## 项目概述

杀戮尖塔2（Slay the Spire 2）MOD 项目，使用 C#/.NET 9 + Godot 4.5 + BaseLib 社区库开发。

- **设计文档：** `Design/修女.md` — 角色机制、卡池、遗物池、药水池的完整定义
- **进度跟踪：** `Progress/修女.md` — 当前开发阶段、已完成/未完成/待验证清单
- **MOD 代码：** `NightMoon/NightMoonCode/` — 所有 C# 源码
- **本地化：** `NightMoon/NightMoon/localization/eng/` — 英文本地化 JSON

---

## API 查询路径

按优先级依次查询，命中即停：

### 1. BaseLib 参考手册（首选）

**文件：** `MOD制作教程/BaseLib参考手册.md`

覆盖：CustomCardModel、CustomRelicModel、CustomPotionModel、CustomPowerModel、CustomSingletonModel、SpireField、关键词、本地化、Hooks 等。

### 2. 已缓存的反编译结果

**摘要：** `MOD制作教程/已查询API记录.md` — 精简版 API 签名和用法示例

**完整源码：** `MOD制作教程/decompiled_api/` 目录下的 .cs 文件

| 文件 | 内容 |
|------|------|
| CardModel.cs | 卡牌模型（OnPlay、升级、关键词等） |
| CardPileCmd.cs | 牌堆命令（抽牌、洗牌、加入牌堆） |
| CreatureCmd.cs | 生物命令（伤害、治疗、格挡） |
| Creature.cs / Player.cs | 生物/玩家属性 |
| PotionModel.cs | 药水模型（OnUse、Rarity、Usage） |
| SpireField.cs | BaseLib 字段工具类 |

### 3. 实时反编译（最后手段）

游戏 DLL 路径：
```
d:/Program Files/Steam/steamapps/common/Slay the Spire 2/data_sts2_windows_x86_64/sts2.dll
```

BaseLib DLL 路径：
```
c:/Users/30674/.nuget/packages/alchyr.sts2.baselib/3.1.3/lib/net9.0/BaseLib.dll
```

反编译工具路径：
```
c:/Users/30674/.dotnet/tools/ilspycmd.exe
```

反编译命令：
```bash
# 反编译指定类型
"c:/Users/30674/.dotnet/tools/ilspycmd.exe" -t "MegaCrit.Sts2.Core.Models.PotionModel" "d:/Program Files/Steam/steamapps/common/Slay the Spire 2/data_sts2_windows_x86_64/sts2.dll"

# 列出所有类型（配合 grep 筛选）
"c:/Users/30674/.dotnet/tools/ilspycmd.exe" "path/to/dll" 2>&1 | grep "关键词"

# 反编译结果保存到缓存
"c:/Users/30674/.dotnet/tools/ilspycmd.exe" -t "Namespace.TypeName" "path/to/dll" > "MOD制作教程/decompiled_api/TypeName.cs"
```

**查询到新 API 后，应保存到 `decompiled_api/` 并更新 `已查询API记录.md`，避免重复反编译。**

---

## 常用命名空间速查

| 类型 | 命名空间 |
|------|---------|
| CardType, CardRarity, TargetType, CardKeyword | `MegaCrit.Sts2.Core.Entities.Cards` |
| RelicRarity | `MegaCrit.Sts2.Core.Entities.Relics` |
| PotionRarity, PotionUsage | `MegaCrit.Sts2.Core.Entities.Potions` |
| CombatSide | `MegaCrit.Sts2.Core.Combat` |
| CreatureCmd, PowerCmd, CardPileCmd | `MegaCrit.Sts2.Core.Commands` |
| SpireField | `BaseLib.Utils` |
| CustomSingletonModel | `BaseLib.Abstracts` |
| Purge 关键词 | `BaseLib.Cards` |

---

## 开发工作流

### 1. 读取上下文

```
读 Design/    → 理解目标机制和设计意图
读 Progress/  → 了解当前进度、哪些已完成/未完成
```

### 2. 查询 API

按上述 API 查询路径，找到需要的类、方法、属性签名。

### 3. 编写代码

- **卡牌：** 继承 `NunCard`（普通牌）或 `NunPrayerCard`（祷告牌），放在 `NightMoon/NightMoonCode/Cards/Nun/`
- **遗物：** 继承 `NunRelic`，放在 `NightMoon/NightMoonCode/Relics/Nun/`
- **药水：** 继承 `NunPotion`，放在 `NightMoon/NightMoonCode/Potions/Nun/`
- **能力：** 继承 `CustomPowerModel`，放在 `NightMoon/NightMoonCode/Powers/Nun/`
- **单例：** 继承 `CustomSingletonModel`，放在 `NightMoon/NightMoonCode/Powers/Nun/`

所有内容类使用 `[Pool(typeof(对应Pool))]` 特性自动注册，无需手动 AddContent。

### 4. 添加本地化

- 卡牌：`NightMoon/localization/eng/cards.json`
- 遗物：`NightMoon/localization/eng/relics.json`
- 药水：`NightMoon/localization/eng/potions.json`
- 能力：`NightMoon/localization/eng/powers.json`

键名格式：`NIGHTMOON-NUN_大写英文名.title` / `.description` / `.flavor`

### 5. 构建验证

```bash
cd NightMoon && dotnet build
```

确保 0 Error。如有 STS001 本地化错误，补充对应 JSON 条目。

### 6. 更新进度

在 `Progress/修女.md` 中勾选已完成项，更新卡牌实现清单。

---

## 注意事项

- `NunPrayerCard` 默认带 Exhaust 关键词，打出后进入消耗堆
- `PrayerManager` 是静态类，管理所有祷告条目（不是卡牌本身）
- `SpireField<TKey, TVal>` 的 TKey 必须是 class（如 `Creature`、`CardModel`）
- 药水的 `PotionRarity`/`PotionUsage` 在 `MegaCrit.Sts2.Core.Entities.Potions` 命名空间
- 遗物/药水/卡牌的本地化缺失会导致 STS001 编译错误
