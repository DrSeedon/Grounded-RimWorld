# RimWorld 1.6 — XML Patchable Fields Reference

Шпаргалка по полям которые можно патчить через PatchOperations.
Источник: рефлексия Assembly-CSharp.dll через .NET 8 MetadataReader.

---

## PlantProperties (ThingDef/plant/)

### Рост
- `growDays` — дней до полного роста
- `harvestYield` — количество предметов при сборе
- `harvestedThingDef` — что собирается (defName продукта)
- `harvestTag` — тег сбора (Standard, Drug...)
- `harvestWork` — трудоёмкость сбора
- `harvestMinGrowth` — минимальный % роста для сбора
- `harvestAfterGrowth` — авто-сбор после достижения роста
- `harvestFailable` — может ли сбор провалиться
- `harvestYieldAffectedByDifficulty` — урожай зависит от сложности

### Температура и среда
- `minGrowthTemperature` — минимальная температура роста (°C) ⚠️ НЕ minGrowTemperature
- `minOptimalGrowthTemperature` — нижняя граница оптимальной температуры
- `maxOptimalGrowthTemperature` — верхняя граница оптимальной температуры
- `maxGrowthTemperature` — максимальная температура роста
- `fertilityMin` — минимальная плодородность почвы (0.0–1.0)
- `fertilitySensitivity` — насколько плодородность влияет на скорость (0=игнор, 1=полная)
- `completelyIgnoreFertility` — полностью игнорировать плодородность

### Посев
- `sowTags` — где можно сажать (Ground, Hydroponic...)
- `sowWork` — трудоёмкость посева
- `sowMinSkill` — минимальный навык растениеводства
- `blockAdjacentSow` — блокирует посев рядом
- `sowResearchPrerequisites` — нужные исследования для посева
- `mustBeWildToSow` — только дикий (нельзя сажать)

### Свет
- `growMinGlow` — минимальное освещение для роста (0.0–1.0)
- `growOptimalGlow` — оптимальное освещение
- `diesToLight` — гибнет на свету
- `dieIfNoSunlight` — гибнет без солнца

### Поведение
- `dieIfLeafless` — гибнет если нет листьев (зима)
- `neverBlightable` — не поражается болезнями растений
- `interferesWithRoof` — мешает крыше
- `dieFromToxicFallout` — гибнет от токсичных осадков
- `humanFoodPlant` — считается едой для людей
- `treeLoversCareIfChopped` — Охотники за природой расстроятся при рубке
- `allowAutoCut` — можно авто-рубить
- `drugForHarvestPurposes` — считается наркотиком при сборе
- `vacuumResistant` — выживает в вакууме (Odyssey)
- `terraformable` — можно терраформировать

### Дикий рост
- `wildBiomes` — в каких биомах растёт дико (список с плотностью)
- `wildClusterRadius` — радиус кластера дикого роста
- `wildClusterWeight` — вес кластера
- `wildOrder` — порядок появления
- `wildEqualLocalDistribution` — равномерное распределение
- `cavePlant` — пещерное растение
- `cavePlantWeight` — вес появления в пещерах
- `plantRespawningCommonalityFactor` — фактор повторного появления

### Визуал
- `topWindExposure` — покачивание на ветру (0.0–1.0)
- `maxMeshCount` — число мешей (визуальных копий на клетку)
- `visualSizeRange` — диапазон визуального размера
- `minSpacingBetweenSamePlant` — мин. расстояние между одинаковыми растениями
- `burnedThingDef` — что остаётся после сгорания
- `choppedThingDef` — что остаётся после рубки

### Звуки
- `soundHarvesting` — звук сбора
- `soundHarvestFinish` — звук завершения сбора

---

## RaceProperties (ThingDef/race/)

### Базовые
- `intelligence` — интеллект (Animal, ToolUser, Humanlike)
- `hasGenders` — есть ли пол
- `forceGender` — принудительный пол
- `foodType` — тип еды (Omnivore, Herbivore, Carnivore, VegetarianSimple...)
- `body` — defName типа тела
- `needsRest` — нужен сон

### Жизнь и размножение
- `lifeExpectancy` — ожидаемая продолжительность жизни (лет)
- `gestationPeriodDays` — период гестации (дней)
- `litterSizeCurve` — кривая размера помёта
- `mateMtbHours` — среднее время между спариванием (часов)
- `lifeStageAges` — стадии жизни с возрастами
- `ageGenerationCurve` — кривая генерации возраста

### Статы тела
- `baseBodySize` — базовый размер тела (влияет на мясо, броню, скорость)
- `baseHealthScale` — масштаб здоровья
- `baseHungerRate` — базовая скорость голода
- `bleedRateFactor` — множитель скорости кровотечения
- `isImmuneToInfections` — иммунитет к инфекциям

### Поведение
- `predator` — хищник (может охотиться)
- `maxPreyBodySize` — макс. размер добычи
- `petness` — насколько подходит как питомец (0.0–1.0)
- `nuzzleMtbHours` — среднее время между ласками
- `manhunterOnDamageChance` — шанс стать людоловом при ранении (0.0–1.0)
- `manhunterOnTameFailChance` — шанс стать людоловом при провале приручения
- `herdAnimal` — стадное (собирается в стаи)
- `packAnimal` — вьючное (несёт груз в каравана)
- `allowedOnCaravan` — разрешён в каравана
- `canReleaseToWild` — можно отпустить в дикую природу
- `roamMtbDays` — среднее время между блужданием (дней)
- `wildBiomes` — в каких биомах спавнится дикий
- `willNeverEat` — никогда не ест (совместимость)
- `disableMating` — отключить спаривание
- `canCrossBreedWith` — может скрещиваться с (список)
- `crossAggroWith` — агрессирует на (список)
- `canFishForFood` — умеет рыбачить
- `waterSeeker` — ищет воду
- `canFlyIntoMap` — прилетает на карту (птицы)
- `leaveMapOnFleeChance` — шанс покинуть карту при бегстве
- `flightSpeedFactor` — множитель скорости полёта

### Приручение
- `trainability` — уровень обучаемости (None, Simple, Intermediate, Advanced)
- `trainableTags` — теги обучаемых навыков
- `specialTrainables` — специальные обучаемые навыки
- `nameOnTameChance` — шанс получить имя при приручении
- `showTrainables` — показывать вкладку обучения
- `playerCanChangeMaster` — игрок может менять хозяина

### Мясо и кожа
- `leatherDef` — defName типа кожи
- `hasMeat` — даёт мясо при разделке
- `meatLabel` — название мяса
- `specificMeatDef` — конкретный тип мяса (если не стандартный)
- `useMeatFrom` — ⚠️ берёт мясо от другого животного (не патчить MeatAmount напрямую!)
- `useLeatherFrom` — берёт кожу от другого животного

### Звуки
- `soundCallIntervalRange` — интервал между криками
- `soundMeleeHitPawn` — звук удара по пешке
- `soundMeleeMiss` — звук промаха
- `soundAmbience` — звук окружения
- `soundMoving` — звук движения
- `soundEating` — звук еды

---

## statBases (ThingDef/statBases/)

Применимо ко всем ThingDef. Каждый тег = override базового значения StatDef.

### Общие
- `MaxHitPoints` — максимальное здоровье
- `MoveSpeed` — скорость передвижения (клеток/сек)
- `MarketValue` — рыночная стоимость (серебро)
- `Mass` — масса (кг)
- `Flammability` — воспламеняемость (0.0–1.0)
- `Beauty` — красота
- `Cleanliness` — чистота

### Животные
- `MeatAmount` — количество мяса при разделке ⚠️ не патчить если useMeatFrom
- `LeatherAmount` — количество кожи при разделке
- `Nutrition` — питательность
- `ComfyTemperatureMin` — минимальная комфортная температура (пешки)
- `ComfyTemperatureMax` — максимальная комфортная температура

### Строительство
- `WorkToBuild` — трудоёмкость постройки
- `WorkToMake` — трудоёмкость изготовления
- `Deterioration` — скорость деградации

### Оружие (ближний бой)
- `SharpDamage` — режущий урон
- `BluntDamage` — дробящий урон

### Оружие (дальний бой)
- `RangedWeapon_Cooldown` — перезарядка
- `AccuracyTouch` — точность вплотную
- `AccuracyShort` — точность на короткой
- `AccuracyMedium` — точность на средней
- `AccuracyLong` — точность на дальней

### Броня / одежда
- `ArmorRating_Sharp` — защита от режущего
- `ArmorRating_Blunt` — защита от дробящего
- `ArmorRating_Heat` — защита от жара
- `Insulation_Cold` — утепление от холода
- `Insulation_Heat` — защита от жары

---

## ApparelProperties (ThingDef/apparel/)

- `bodyPartGroups` — покрываемые части тела (Torso, Legs, FullHead...)
- `layers` — слои (OnSkin, Middle, Shell, Belt, Overhead)
- `wornGraphicPath` — путь к текстуре при ношении
- `tags` — теги для генерации у пешек
- `defaultOutfitTags` — дефолтные теги outfit
- `wearPerDay` — скорость износа в день
- `careIfWornByCorpse` — персонажи расстраиваются от носки с трупа
- `careIfDamaged` — расстраиваются от повреждённой одежды
- `blocksVision` — блокирует зрение
- `immuneToToxGasExposure` — иммунитет к токсичному газу
- `gender` — ограничение по полу
- `developmentalStageFilter` — ограничение по стадии развития (Baby, Child, Adult)
- `soundWear` — звук надевания
- `soundRemove` — звук снятия
- `canBeGeneratedToSatisfyWarmth` — может генерироваться для тепла
- `slaveApparel` — одежда раба
- `countsAsClothingForNudity` — считается одеждой против наготы

---

## IngestibleProperties (ThingDef/ingestible/)

- `foodType` — тип еды (Meal, Vegetable, Meat, Plant, Corpse, Kibble...)
- `preferability` — предпочтительность (NeverForNutrition, DesperateOnly, RawBad, RawTasty, MealAwful, MealSimple, MealFine, MealLavish)
- `maxNumToIngestAtOnce` — максимум съедаемого за раз
- `baseIngestTicks` — время поедания (тики)
- `joy` — даваемое удовольствие
- `joyKind` — тип удовольствия
- `tasteThought` — мысль от вкуса
- `specialThoughtDirect` — прямая мысль при поедании
- `drugCategory` — категория наркотика (Hard, Social, Medical)
- `ingestEffect` — эффект при поедании
- `ingestSound` — звук поедания
- `ingestCommandString` — текст команды поедания
- `nurseable` — можно кормить грудничков
- `humanlikeOnly` — только для людей
- `outcomeDoers` — эффекты (хеджи, мысли, стат-изменения)
- `canAutoSelectAsFoodForCaravan` — автовыбор для каравана

---

## StuffProperties (ThingDef/stuffProps/)

Для материалов (сталь, золото, дерево...).

- `stuffAdjective` — прилагательное материала ("деревянный", "стальной")
- `categories` — категории материала (Metallic, Woody, Stony, Fabric, Leathery)
- `commonality` — вес появления в генерации
- `statOffsets` — аддитивные модификаторы к статам предметов из материала
- `statFactors` — мультипликативные модификаторы
- `statOffsetsQuality` — офсеты с учётом качества
- `statFactorsQuality` — факторы с учётом качества
- `color` — цвет материала
- `appearance` — внешний вид (Fabric, Smooth, Coarse, Metallic...)
- `isAirtight` — герметично (важно для Odyssey)
- `soundImpactBullet` — звук попадания пули
- `soundImpactMelee` — звук удара ближнего боя

---

## VerbProperties (ThingDef/verbs/li/)

Для оружия и атак.

- `range` — дальность стрельбы
- `minRange` — минимальная дальность
- `warmupTime` — время прицеливания (сек)
- `defaultCooldownTime` — кулдаун (сек)
- `burstShotCount` — патронов за очередь
- `ticksBetweenBurstShots` — тики между выстрелами очереди
- `accuracyTouch/Short/Medium/Long` — точность по дистанциям (0.0–1.0)
- `meleeDamageBaseAmount` — базовый урон ближнего боя
- `meleeArmorPenetrationBase` — пробитие брони ближнего боя
- `meleeDamageDef` — тип урона ближнего боя (Cut, Blunt, Stab...)
- `defaultProjectile` — defName снаряда
- `noiseRadius` — радиус шума выстрела
- `requireLineOfSight` — нужна прямая видимость
- `violent` — является насилием
- `ai_IsWeapon` — ИИ считает это оружием
- `ai_IsBuildingDestroyer` — ИИ использует для разрушения зданий
- `ai_AvoidFriendlyFireRadius` — радиус избежания дружественного огня

---

## PawnKindDef

Определяет тип пешки (SpawnGroup, в квестах, у фракций).

- `race` — ThingDef расы
- `combatPower` — боевая мощь (для баланса рейдов)
- `isFighter` — является бойцом
- `wildGroupSize` — размер дикой группы
- `ecoSystemWeight` — вес в экосистеме
- `weaponMoney` — бюджет на оружие
- `weaponTags` — теги оружия
- `apparelMoney` — бюджет на одежду
- `apparelTags` — теги одежды
- `apparelRequired` — обязательная одежда
- `minGenerationAge` — минимальный возраст при генерации
- `maxGenerationAge` — максимальный возраст
- `fleeHealthThresholdRange` — порог здоровья для бегства
- `skills` — навыки с уровнями
- `lifeStages` — переопределение стадий жизни
- `fixedGender` — фиксированный пол
- `canArriveManhunter` — может появиться людоловом
- `canBeSapper` — может быть сапёром в рейде

---

## ThingDef (верхний уровень)

- `label` — название (в именительном падеже)
- `description` — описание
- `stackLimit` — максимальный стак
- `techLevel` — технологический уровень
- `thingCategories` — категории (для хранения, UI)
- `tradeTags` — теги для торговли
- `tradeability` — торгуемость (Sellable, Buyable, Stockable, None)
- `mineable` — добываемо шахтёрами
- `recipes` — список рецептов где используется
- `tools` — инструменты атаки (для ближнего боя)
- `comps` — компоненты (CompPowerTrader, CompRefuelable...)
- `equipmentType` — тип экипировки (None, Primary, Biocoded)
- `weaponClasses` — классы оружия
- `weaponTags` — теги оружия
- `equippedStatOffsets` — бонусы к статам пешки при экипировке
- `rotatable` — можно поворачивать при постройке
- `leaveResourcesWhenKilled` — оставляет ресурсы при уничтожении
- `killedLeavings` — что оставляет при уничтожении
- `butcherProducts` — продукты разделки (не для животных)
- `smeltProducts` — продукты переплавки
- `smeltable` — можно переплавить
- `generateCommonality` — вероятность генерации в мире
- `defaultStuff` — дефолтный материал
- `socialPropernessMatters` — важно для социальных взаимодействий
- `stealable` — можно украсть

---

## Важные нюансы

### ⚠️ Частые ошибки
1. `minGrowTemperature` — НЕВЕРНО. Правильно: `minGrowthTemperature` (с "th")
2. Патчить `MeatAmount` на животных с `useMeatFrom` — операция упадёт с ошибкой
3. Файлы патчей в `1.6/Core/` — не загружаются (только `1.6/Patches/`)
4. `LoadFolders.xml` должен явно регистрировать все папки

### PatchOperation типы
- `PatchOperationReplace` — заменить узел целиком
- `PatchOperationAdd` — добавить узел
- `PatchOperationRemove` — удалить узел
- `PatchOperationConditional` — если xpath найден → match, иначе → nomatch
- `PatchOperationAttributeSet` — установить атрибут XML-тега
- `PatchOperationSequence` — список операций подряд
- `PatchOperationFindMod` — выполнить если мод активен (вместо IfModActive в LoadFolders)

### Полезные xpath паттерны
```xml
<!-- По defName -->
/Defs/ThingDef[defName="Rat"]/statBases/MoveSpeed

<!-- По атрибуту класса в списке -->
/Defs/ThingDef[defName="Rat"]/comps/li[@Class="CompProperties_Rottable"]/daysToRotStart

<!-- По значению в списке -->
/Defs/BiomeDef[defName="TropicalForest"]/wildAnimals/li[defName="Rat"]

<!-- С условием -->
/Defs/ThingDef[defName="Plant_Potato"]/plant/harvestYield
```
