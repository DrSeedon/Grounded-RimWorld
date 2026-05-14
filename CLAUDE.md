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

MeatAmount/LeatherAmount — отдельный файл `MeatLeather_Scaling.xml` (формула: bodySize×100 / ×40)

### Кропы (CropBalance_Realism.xml)
| Культура | Изменение |
|----------|-----------|
| Plant_Potato | harvestYield 11→14, minGrowthTemperature 2°C |
| RawPotatoes | daysToRotStart 30→14, Mass 0.05, MarketValue 0.9 |
| Plant_Rice | minGrowthTemperature 12°C |
| RawRice | Mass 0.02 |
| RawBerries | daysToRotStart 14→7, MarketValue 1.5 |

### Прочее
- Биомы: spawn rates и pack animals для 12 биомов
- Тела: 18 новых BodyDefs (Arachnid, Hexapod, Slug, Squid, Kangaroo, Theropod...)
- Звуки: Fox, Jaguar, Leopard, Ostrich, Puma, Tiger, Turkey, Wolf

### C# DLL: stackLimit авто-расчёт
`Source/GroundedMod.cs` — `[StaticConstructorOnStartup]`, без Harmony.
Формула: `stackLimit = clamp(30 / mass, originalStack, 500)`
- Трогает ВСЕ ThingDef с stackLimit > 1 и mass > 0
- Единственное исключение: Medicine (thingCategories содержит "Medicine")
- Только увеличивает (newStack >= originalStack)

### Сборка DLL
```bash
cd Source && dotnet build -c Release
```
Выход: `1.6/Assemblies/Grounded.dll`

## Совместимость
CombatExtended (тела, бронь), VAE (все паки), Alpha Animals, Biotech, Odyssey

## Важные нюансы

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
