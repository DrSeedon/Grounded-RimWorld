# Grounded: Realistic Stats Overhaul

## Что это
Мод для RimWorld 1.6. Реалистичные статы животных + баланс кропов + авто-масштабирование stackLimit. XML патчи + минимальный C# (без Harmony).

packageId: `seedon.grounded`

## Структура
- `Source/` — C# исходники (Grounded.csproj, GroundedMod.cs)
- `1.6/Assemblies/` — скомпилированная Grounded.dll
- `1.6/Patches/Core/` — основные XML патчи (животные, кропы, мясо/кожа, биомы, тела, звуки)
- `1.6/Defs/` — новые Defs (тела, дамаг, звуки, обучаемые)
- `DLC/Biotech/`, `DLC/Odyssey/` — патчи для DLC (условно через LoadFolders)
- `ModPatches/` — совместимость (CombatExtended, VAE, Alpha Animals)
- `Common/` — текстуры, звуки
- `Languages/` — переводы EN + RU
- `About/` — метаданные мода

## Что мод меняет

### Животные (49 патчатся напрямую)
Поля: `bodySize`, `MoveSpeed`, `lifeExpectancy`, `gestationPeriodDays`, `litterSizeCurve`, `foodType`, `baseHungerRate`, `manhunterOnDamageChance`, `MarketValue`

MeatAmount/LeatherAmount масштабируются автоматически через vanilla `StatPart_BodySize` (140×bodySize / 40×bodySize)

### Деревья (Trees_Realism.xml, 14 видов)
growDays + harvestYield из реального возраста рубки (sqrt-scale):
- Быстрые: Bamboo 11d/10y, Cocoa 11d/20y, Cecropia 13d/20y
- Средние: Poplar 18d/27y, Willow 18d/27y, Drago 20d/30y, Palm 23d/16y
- Медленные: Birch 25d/39y, Pine 27d/43y, Maple 31d/50y
- Долгие: Teak 35d/56y, Cypress 36d/59y, Oak 41d/67y
- Нерфнут: Saguaro 23d/8y (кактус ≠ дерево)

### Кропы (CropBalance_Realism.xml)
| Культура | Изменение |
|----------|-----------|
| Plant_Potato | harvestYield 11→14, minGrowthTemperature 2°C |
| RawPotatoes | daysToRotStart 30→14, Mass 0.1, MarketValue 0.9 |
| Plant_Rice | minGrowthTemperature 12°C |
| RawRice | Mass 0.02 |
| RawBerries | daysToRotStart 14→7, MarketValue 1.5 |
| Plant_Strawberry | minGrowthTemperature 5°C |
| Plant_Corn | minGrowthTemperature 10°C |

### Массы (Mass_Realism.xml)
Ресурсы: Steel 0.5→0.8, WoodLog 0.4→0.7, Uranium 1.0→2.0
Камень: Sandstone 2.5, Granite 3.5, Marble 2.8, Slate 2.3, Limestone 2.6
Снаряды: HE 3.5, Incendiary 3.0, EMP 2.5 (было 1.25 для всех)
Еда: MealSimple 0.4, MealFine 0.5, Pemmican 0.04, Kibble 0.04

### C# DLL: stackLimit авто-расчёт
`Source/GroundedMod.cs` — `[StaticConstructorOnStartup]`, без Harmony.
Формула: `stackLimit = clamp(30 / mass, originalStack, 500)`
- Трогает ВСЕ ThingDef с stackLimit > 1 и mass > 0
- Единственное исключение: Medicine (thingCategories содержит "Medicine")
- Только увеличивает (newStack >= originalStack)
- Работает с модовыми предметами автоматически

### Прочее
- Биомы: spawn rates и pack animals для 12 биомов
- Тела: 18 новых BodyDefs (Arachnid, Hexapod, Slug, Squid, Kangaroo, Theropod...)
- Звуки: Fox, Jaguar, Leopard, Ostrich, Puma, Tiger, Turkey, Wolf

### Сборка DLL
```bash
cd Source && dotnet build -c Release
```
Выход: `1.6/Assemblies/Grounded.dll`

## Совместимость
CombatExtended (тела, бронь), VAE (все паки), Alpha Animals, Biotech, Odyssey

## Важные нюансы

### ⚠️ MeatAmount/LeatherAmount — НЕ патчить через XML
`MeatAmount` и `LeatherAmount` StatDef имеют `StatPart_BodySize` — финальное значение = `statBases.Value × bodySize`. Если задать MeatAmount в XML, bodySize умножится ДВАЖДЫ (bodySize² × base). Vanilla default (140 для мяса, 40 для кожи) × наш bodySize = адекватные числа.

### ⚠️ Животные с useMeatFrom — НЕ патчить MeatAmount
Следующие животные используют `useMeatFrom` и наследуют мясо/кожу автоматически.
Патчить их напрямую вызовет ошибки в логе.
```
Bear_Polar (→ Bear_Grizzly), Emu (→ Cassowary), Fox_Arctic (→ Fox_Fennec),
Fox_Red (→ Fox_Fennec), Goose (→ Cassowary), Megascarab (→ Megaspider),
Ostrich (→ Cassowary), Spelopede (→ Megaspider), WildBoar (→ Pig),
Wolf_Arctic (→ Wolf_Timber)
```

### ⚠️ Правильное имя поля температуры
- ❌ `minGrowTemperature` — НЕ существует в PlantProperties
- ✅ `minGrowthTemperature` — правильное поле (с "th")

### ⚠️ Расположение патч-файлов
- ✅ `1.6/Patches/Core/` — загружается игрой
- ❌ `1.6/Core/` — НЕ загружается (не зарегистрировано в LoadFolders.xml)

### LoadFolders.xml
```xml
<li>1.6</li>        <!-- загружает 1.6/Defs/ и 1.6/Patches/ -->
<li>Common</li>     <!-- звуки, текстуры -->
<li IfModActive="CETeam.CombatExtended">ModPatches/CombatExtended</li>
```

## Справочник полей
Все патчуемые XML-поля RimWorld 1.6: `docs/rimworld-xml-fields.md`
