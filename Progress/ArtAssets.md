# 美术资源说明与 AI 生图提示词

此文件记录 NightMoon 修女角色的美术资源位置、规格，以及可直接用于 AI 生图的基础提示词。生成正式资源时，优先按“目标尺寸”导出 PNG；如果生图工具不支持精确尺寸，可以先按“建议画幅”生成更高分辨率版本，再裁切/缩放到目标尺寸。

## 统一风格

修女的视觉关键词：

`dark fantasy, gothic nun, black-purple habit, bone white cloth, old gold religious ornaments, crimson confession marks, ivory prayer light, restrained sacred horror, painterly game art, high contrast, clean readable silhouette, no text, no watermark`

中文语义参考：

`黑紫色修女服、骨白色头巾、旧金圣物、暗红忏悔痕迹、象牙白祷告光、宗教感、神秘、克制、罪感、可读性强、游戏美术、无文字、无水印`

负面提示词建议：

`text, letters, logo, watermark, blurry, low resolution, cropped subject, extra limbs, malformed hands, modern clothing, sci-fi armor, cute cartoon, flat icon only, messy background`

色彩约束：

- 主色：深紫、黑紫、骨白。
- 祷告：旧金、象牙白、柔和圣光。
- 忏悔：暗红、瘀紫红、血色细线。
- 稀有牌/神圣牌：金白高光。
- 负面/诅咒感：黑红、低饱和阴影。

## 文件夹与尺寸

### 角色 UI

目录：`NightMoon/NightMoon/images/charui/`

| 文件 | 目标尺寸 | 建议画幅 | 透明背景 | 用途 |
|---|---:|---|---|---|
| `nun_character_icon.png` | 128x128 | 1:1 | 是 | 角色头像 |
| `nun_character_select.png` | 132x195 | 2:3 | 是 | 角色选择立绘 |
| `nun_character_select_locked.png` | 132x195 | 2:3 | 是 | 锁定状态立绘 |
| `nun_map_marker.png` | 128x128 | 1:1 | 是 | 地图标记 |
| `nun_big_energy.png` | 74x74 | 1:1 | 是 | 大能量图标 |
| `nun_text_energy.png` | 24x24 | 1:1 | 是 | 文本内小能量图标 |

角色选择立绘基础提示词：

```text
Full-body gothic nun character for a dark fantasy card game, black-purple nun habit, bone white veil, old gold rosary and small sacred ornaments, restrained solemn expression, crimson confession marks subtly glowing, ivory prayer light around hands, elegant readable silhouette, centered character, transparent background, no text, no watermark
```

角色头像基础提示词：

```text
Square portrait icon of a gothic nun, dark purple and bone white habit, solemn face, old gold halo-like ornament, faint crimson confession glow, readable at small size, transparent background, no text, no watermark
```

地图标记基础提示词：

```text
Small map marker icon for a gothic nun character, simplified nun head and crescent prayer symbol, dark purple, bone white, old gold accent, strong silhouette, transparent background, no text, no watermark
```

能量图标基础提示词：

```text
Circular energy orb icon for a gothic nun card game character, old gold and ivory prayer light inside dark purple metal frame, subtle rosary motif, high contrast, readable at 24 pixels, transparent background, no text, no watermark
```

### 卡牌立绘

目录：

- 小图：`NightMoon/NightMoon/images/card_portraits/`
- 大图：`NightMoon/NightMoon/images/card_portraits/big/`

| 类型 | 目标尺寸 | 建议画幅 | 透明背景 | 说明 |
|---|---:|---|---|---|
| 小卡图 | 250x190 | 25:19 / 横图 | 否 | 卡牌内显示 |
| 大卡图 | 1000x760 | 25:19 / 横图 | 否 | 大图预览，同构图高清版 |

通用卡牌提示词模板：

```text
Dark fantasy gothic card illustration for the card "{CARD_CN}", theme: {CARD_EFFECT_THEME}. Black-purple, bone white, old gold, crimson accents. Religious nun magic, prayer, confession, sacred horror, dramatic but readable composition, painterly game art, no text, no watermark, no frame, no UI.
```

祷告牌提示词模板：

```text
Dark fantasy card illustration, a prayer ritual suspended in time, old gold and ivory sacred light forming countdown-like rings, gothic nun symbolism, black-purple shadows, restrained religious atmosphere, clear focal point, painterly game art, no text, no watermark, no frame
```

忏悔牌提示词模板：

```text
Dark fantasy card illustration, crimson confession marks and black-purple shadows, a solemn gothic nun facing guilt and punishment, sacred horror mood, old gold religious symbols, dramatic readable composition, painterly game art, no text, no watermark, no frame
```

攻击牌提示词模板：

```text
Dark fantasy card illustration, gothic nun unleashing sacred violence, ivory-gold holy light mixed with crimson punishment energy, black-purple background, dynamic action, clear focal point, painterly game art, no text, no watermark, no frame
```

技能牌提示词模板：

```text
Dark fantasy card illustration, gothic nun performing a restrained ritual or defensive prayer, old gold sigils, ivory light, black-purple shadows, elegant composition, readable at small size, painterly game art, no text, no watermark, no frame
```

能力牌提示词模板：

```text
Dark fantasy card illustration, symbolic permanent blessing or curse around a gothic nun, sacred aura, old gold relic geometry, crimson confession traces, black-purple atmosphere, iconic composition, painterly game art, no text, no watermark, no frame
```

最新仍为通用占位的卡牌，建议优先补：

| 文件 ID | 中文名 | 生图主题 |
|---|---|---|
| `nun_gospel` | 福音 | 祷告完成、金白经文光、复制回响 |
| `nun_radiant_form` | 光辉形态 | 坚不可摧、金白圣光护体 |
| `nun_mind_bloom` | 心灵绽放 | 心灵花开、祷告重放、双重回响 |
| `nun_final_hope` | 最终希望 | 失血后的最后祷告、额外回合 |
| `nun_karma_stain` | 业力沾身 | 暗红业力缠绕、忏悔重复触发 |

### 能力图标

目录：

- 小图：`NightMoon/NightMoon/images/powers/`
- 大图：`NightMoon/NightMoon/images/powers/big/`

| 类型 | 目标尺寸 | 建议画幅 | 透明背景 | 说明 |
|---|---:|---|---|---|
| 小能力图标 | 64x64 | 1:1 | 是 | 战斗 UI 图标 |
| 大能力图标 | 256x256 | 1:1 | 是 | 大图标，同构图高清版 |

能力图标提示词模板：

```text
Square transparent power icon for a dark fantasy card game, {POWER_THEME}, gothic nun symbolism, dark purple base, bone white and old gold highlights, crimson accent, simple strong silhouette, readable at 64 pixels, no text, no watermark
```

最新仍为通用占位的能力，建议优先补：

| 文件 ID | 中文名 | 生图主题 |
|---|---|---|
| `nun_mind_bloom_power` | 心灵绽放 | 金白心灵之花、双重祷告回响 |
| `nun_karma_stain_power` | 业力沾身 | 暗红业力锁链、罪痕 |
| `nun_radiant_form_power` | 坚不可摧 | 金白护盾、圣光硬壳 |
| `nun_final_hope_power` | 最终希望 | 微弱烛火、最后祈祷 |

### 遗物图标

目录：

- 小图：`NightMoon/NightMoon/images/relics/`
- 大图：`NightMoon/NightMoon/images/relics/big/`
- 轮廓图：`NightMoon/NightMoon/images/relics/`

| 类型 | 目标尺寸 | 建议画幅 | 透明背景 | 说明 |
|---|---:|---|---|---|
| 小遗物图标 | 94x94 | 1:1 | 是 | 常规遗物图 |
| 大遗物图标 | 256x256 | 1:1 | 是 | 大图标 |
| 轮廓图 | 94x94 | 1:1 | 是 | 纯白或浅色 silhouette |

遗物图标提示词模板：

```text
Transparent relic icon for a dark fantasy card game, {RELIC_OBJECT}, gothic religious artifact, old gold metal, bone white cloth or ivory light, dark purple shadows, crimson confession detail, centered object, strong silhouette, no text, no watermark
```

轮廓图制作建议：

- 不建议单独 AI 生图。
- 用正式遗物图转成纯白/浅灰 silhouette，保留透明背景。
- 文件名必须是 `<id>_outline.png`。

### 药水图标

目录：`NightMoon/NightMoon/images/potions/`

| 类型 | 目标尺寸 | 建议画幅 | 透明背景 | 说明 |
|---|---:|---|---|---|
| 药水图标 | 96x96 | 1:1 | 是 | 药水瓶 |
| 药水轮廓 | 96x96 | 1:1 | 是 | 纯白或浅色 silhouette |

药水图标提示词模板：

```text
Transparent potion icon for a dark fantasy card game, small glass vial shaped like a gothic reliquary, {POTION_LIQUID_THEME}, old gold cap, bone white wax seal, dark purple shadows, crimson or ivory glow, centered object, readable at 96 pixels, no text, no watermark
```

药水主题建议：

| 文件 ID | 中文名 | 生图主题 |
|---|---|---|
| `nun_prayer_potion` | 祷告药水 | 象牙白圣光液体、金色祷告环 |
| `nun_confession_potion` | 忏悔药水 | 暗红液体、瘀紫沉淀、罪痕蜡封 |
| `nun_blessing_potion` | 赐福药水 | 金白液体、细小圣辉、祝福纹样 |

## 当前已生成资源

- 卡牌立绘：81 张小图 PNG 和 81 张大图 PNG。
- 能力图标：19 个小图标 PNG 和 19 个大图标 PNG。
- 遗物图标：9 个遗物，包含小图标、轮廓图和大图标。
- 药水图标：3 个药水，包含图标和轮廓图。
- 角色 UI：修女专属图标、选择立绘、锁定选择立绘、地图标记、大能量图标和文本能量图标均已就位。

## 修女角色专属资源规划

### 第一阶段：最小可用版本

| 类型          | 文件建议                            | 内容             |
| ----------- | ------------------------------- | -------------- |
| 战斗人物        | `battle/nun.png`                | 透明背景、面朝右、黑紫修女服 |
| 普通卡图        | `cards/*.png`                   | 250×190        |
| 遗物小图        | `relics/*_85.png`               | 85×85          |
| 遗物大图        | `relics/*_256.png`              | 256×256        |
| 药水图         | `potions/*.png`                 | 256×256        |
| 药水轮廓        | `potions/*_outline.png`         | 256×256        |
| 忏悔 Power 图标 | `powers/confession.png`         | 暗红圣痕 / 荆棘      |
| 祷告 Power 图标 | `powers/prayer.png`             | 金白烛火 / 圣环      |
| 小能量图标       | `energy/energy_nun.png`         | 24×24          |
| 大能量图标       | `energy/energy_nun_big.png`     | 74×74          |
| 角色选择图标      | `ui/char_select_nun.png`        | 选人界面小图标        |
| 锁定图标        | `ui/char_select_nun_locked.png` | 未解锁状态          |

---

### 第二阶段：完整职业观感

| 类型      | 文件建议                          | 内容              |
| ------- | ----------------------------- | --------------- |
| 角色选择大图  | `ui/character_select_nun.png` | 2:3，深紫背景，左上净化之光 |
| 角色选择背景  | `ui/character_select_bg.png`  | 深紫教堂 / 抽象圣堂背景   |
| 地图标记    | `ui/map_marker_nun.png`       | 修女头巾、蜡烛或圣环剪影    |
| 忏悔关键词图标 | `keywords/confession.png`     | tooltip 用       |
| 祷告关键词图标 | `keywords/prayer.png`         | tooltip 用       |
| 攻击卡框    | `cards/frame_attack_nun.png`  | 深紫 + 暗红         |
| 技能卡框    | `cards/frame_skill_nun.png`   | 深紫 + 骨白         |
| 能力卡框    | `cards/frame_power_nun.png`   | 深紫 + 旧金         |
| 简单能量表盘  | `ui/energy_counter_*.png`     | 暗紫圆盘 + 金白圣环     |
| 忏悔触发特效  | `vfx/confession_tick.png`     | 暗红裂纹、血色闪光       |
| 祷告触发特效  | `vfx/prayer_trigger.png`      | 象牙白光、旧金符文       |

---

### 第三阶段：高完成度版本

| 类型        | 文件建议                        | 内容          |
| --------- | --------------------------- | ----------- |
| idle 动画   | `anim/idle_loop`            | 抱书、斗篷轻动     |
| attack 动画 | `anim/attack`               | 荆棘 / 圣痕攻击   |
| cast 动画   | `anim/cast`                 | 合掌祷告、光落下    |
| hurt 动画   | `anim/hurt`                 | 忏悔痕迹裂开      |
| die 动画    | `anim/die`                  | 跪倒、光熄灭      |
| 商店场景      | `scenes/merchant_nun.tscn`  | 坐在祭坛旁或整理祷文  |
| 篝火场景      | `scenes/rest_site_nun.tscn` | 点蜡烛、翻祷书     |
| 时间线小图     | `timeline/*_small.png`      | 解锁、胜利、剧情节点  |
| 时间线大图     | `timeline/*_big.png`        | 时间线详情图      |
| 职业卡牌拖尾    | `vfx/card_trail_nun.tscn`   | 金白祷光或暗红忏悔痕迹 |
| 多人模式手臂图   | `multiplayer/arm_*.png`     | 后期再做        |

---

## 绘制总原则

| 类型       | 原则                   |
| -------- | -------------------- |
| 卡图       | 只画插画，不画卡框、费用、文字      |
| 人物图      | 面朝右，透明背景，竖向比例，剪影清楚   |
| 遗物       | 正方形透明 PNG，主体居中，留安全边距 |
| 药水       | 主图和轮廓图同尺寸、同位置        |
| Power 图标 | 小尺寸可读，避免复杂细节         |
| 能量图标     | 越简单越好，24×24 下也要看得懂   |
| 角色选择大图   | 2:3，完整展示角色气质，可带背景    |
| 特效       | 忏悔偏暗红，祷告偏金白，不要混色太严重  |
