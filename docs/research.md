# Grounded Research Notes

All balance research conducted during mod development. Reference for future changes.

## 1. Tile Size & Storage

- 1 tile ≈ 2.5×2.5m (6.25 m²)
- Floor: 1 stack per tile (`maxItemsInCell=1`)
- Shelf: 3 stacks (`ShelfBase`)
- Floor weight target: ~30 kg per stack (our formula anchor)

## 2. Vanilla stackLimit vs Mass

- Correlation r = -0.33 (weak)
- No formula — devs assigned values manually
- Weapons/apparel always stack=1 regardless of mass
- Resources default 75 (from `ResourceBase`), silver/gold 500, shells 25, drugs 150

## 3. stackLimit Formula

- `stack = clamp(30 / mass, original, 500)`
- Single cap 500 for all categories
- Exclusions: Medicine, stack=1 items
- Applied via C# `[StaticConstructorOnStartup]`, no Harmony needed
- Works with modded items automatically
- volumeMultiplier researched but rejected (adds complexity for ~5% accuracy gain)

## 4. Combat Extended Compatibility

- CE adds Bulk (volume) as separate stat, doesn't touch stackLimit
- Pawns limited by CarryWeight (40 kg) AND CarryBulk (20 units)
- Our stackLimit changes are compatible — CE cares about Bulk, not stack count

## 5. Storage Mods (ASF, sbz_Neat_Storage)

- Use `building.maxItemsInCell` (multiple stacks per tile)
- Don't modify stackLimit
- Our changes work multiplicatively (more stacks × bigger stacks)

## 6. Trees — Realistic growDays

Scale: `growDays = 10 + (sqrt(IRL_years) - sqrt(3)) / (sqrt(75) - sqrt(3)) × 32`
Yield: `harvestYield = 6 + IRL_years^0.6 × 4.8`

| Tree | IRL years | growDays | yield | yield/day |
|------|-----------|----------|-------|-----------|
| Bamboo | 3-5 | 11 | 10 | 0.91 |
| Cecropia | 5-8 | 13 | 20 | 1.54 |
| Poplar | 10-15 | 18 | 27 | 1.50 |
| Willow | 8-15 | 18 | 27 | 1.50 |
| Drago | 10-20 | 20 | 30 | 1.50 |
| Palm | 15-25 | 23 | 16 | 0.70 |
| Saguaro | 10-30 | 23 | 8 | 0.35 |
| Birch | 20-30 | 25 | 39 | 1.56 |
| Pine | 25-35 | 27 | 43 | 1.59 |
| Maple | 30-50 | 31 | 50 | 1.61 |
| Teak | 40-60 | 35 | 56 | 1.60 |
| Cypress | 40-70 | 36 | 59 | 1.64 |
| Oak | 60-80 | 41 | 67 | 1.63 |

Key: yield/day normalized ~1.5 (vanilla ranged 0.83-3.00). Saguaro nerfed (cactus ≠ timber). Maple buffed (was equal to birch in vanilla, now 50 vs 27).

## 7. Realistic Masses

### Changed
| Item | Vanilla | Grounded | Reason |
|------|---------|----------|--------|
| Steel | 0.5 | 0.8 | Steel ingot ~800g |
| WoodLog | 0.4 | 0.7 | Wood plank ~700g |
| Uranium | 1.0 | 2.0 | Dense material (19.1 g/cm³) |
| BlocksSandstone | 1.0 | 2.5 | Cut stone block |
| BlocksGranite | 1.25 | 3.5 | Granite is heavy |
| BlocksMarble | 1.25 | 2.8 | Marble block |
| BlocksSlate | 0.9 | 2.3 | Slate slab |
| BlocksLimestone | 1.1 | 2.6 | Limestone block |
| Shell_HE | 1.25 | 3.5 | 81mm mortar shell ~3.5kg |
| Shell_Incendiary | 1.25 | 3.0 | Incendiary ~3kg |
| Shell_EMP | 1.25 | 2.5 | EMP shell ~2.5kg |
| MealSimple | 0.44 | 0.4 | Plate of food |
| MealFine | 0.44 | 0.5 | Fancy meal, heavier |
| Pemmican | 0.018 | 0.04 | Travel ration ~40g |
| Kibble | 0.015 | 0.04 | Animal feed portion |
| RawPotatoes | 0.03 | 0.1 | Potato ~100g each |
| RawRice | 0.03 | 0.02 | Dry grain, light |

### Not Changed (and why)
- **Plasteel** (0.25): lore = ultra-light advanced alloy
- **Gold/Silver** (0.008): 8g = absurd, but changing breaks economy (currency unit)
- **Weapons**: mostly OK (±30%), not worth the conflict surface
- **Apparel**: reasonable for gameplay

## 8. Crafting Costs

Researched but **not changed**:
- Short bow = 30 WoodLog (6 walls worth of wood for a stick)
- Longsword = 100 stuff (50kg of steel for a 1.4kg sword)
- Revolver = 30 Steel + 2 Components

These are pure gameplay balance, zero realism. Changing would require rebalancing the entire economy. Not in scope.

## 9. MeatAmount / LeatherAmount — Critical Bug Found

**Bug**: `MeatAmount` StatDef has `StatPart_BodySize` → final value = `statBases.MeatAmount × bodySize`.

Our old `MeatLeather_Scaling.xml` set `MeatAmount = bodySize × 100` in statBases. With StatPart_BodySize also multiplying by bodySize: **final = bodySize² × 100**.

| Animal | bodySize | Our XML | Actual final | Should be |
|--------|----------|---------|-------------|-----------|
| Chicken | 0.20 | 20 | 4 | 28 |
| Cow | 3.00 | 300 | 900 | 420 |
| Elephant | 5.00 | 500 | 2500 | 700 |

**Fix**: Deleted `MeatLeather_Scaling.xml`. Vanilla defaults (`MeatAmount=140`, `LeatherAmount=40`) × our bodySize = correct values.

**Rule**: Never set MeatAmount/LeatherAmount in XML for animals. StatPart_BodySize handles scaling automatically.

## 10. Food Chain — Full Calculation

### Unit Conversion
| RimWorld | IRL equivalent |
|----------|---------------|
| 1 nutrition | ≈ 1250 kcal |
| 1 raw meat (0.05 nutr) | ≈ 62.5 kcal |
| Human 1.6 nutr/day | ≈ 2000 kcal/day |
| 1 simple meal (0.9 nutr) | ≈ 1125 kcal |

### Cooking Efficiency
- Simple meal: 0.5 nutrition input → 0.9 nutrition output (×1.8)
- Fine meal: 0.25 meat + 0.25 veg → 0.9 nutrition (×1.8)
- Realistic: cooking does increase caloric availability ~1.5-2×

### Animal Meat Yield (140 × bodySize)
| Animal | bodySize | Meat | Colonist-days | IRL days | Ratio |
|--------|----------|------|--------------|----------|-------|
| Chicken | 0.20 | 28 | 1.6 | 1.5 | 1.05× ✅ |
| Goose | 0.35 | 49 | 2.8 | 2.8 | 1.00× ✅ |
| Deer | 0.90 | 126 | 7.1 | 35 | 0.20× |
| Pig | 2.25 | 315 | 17.7 | 66 | 0.27× |
| Cow | 3.00 | 420 | 23.6 | 301 | 0.08× |
| Elephant | 5.00 | 700 | 39.4 | 2000 | 0.02× |

Small animals = perfect match. Large animals give ×10-50 less than IRL because bodySize scales linearly while IRL weight scales cubically. This is acceptable gameplay design — a cow feeding one colonist for 300 days would eliminate food pressure entirely.

### Starvation Timeline
| Stage | Time |
|-------|------|
| Full → Hungry | ~15 hours |
| Hungry → Starving | +2-3 days |
| Starvation → Death | ~10 days |
| **Total** | **~12-14 days** |
| IRL equivalent | ~30 days (×2.5 compression) |

## 11. Corpse Mass

Formula: `BasePawn.Mass(60) × bodySize`

| Animal | bodySize | Corpse mass | IRL weight | Ratio |
|--------|----------|-------------|-----------|-------|
| Chicken | 0.20 | 12 kg | 2.5 kg | 4.8× |
| Fox_Fennec | 0.20 | 12 kg | 1.5 kg | 8.0× |
| Cow | 3.00 | 180 kg | 700 kg | 0.26× |
| Elephant | 5.00 | 300 kg | 5000 kg | 0.06× |

Root cause: BasePawn.Mass=60 designed for humans (60kg). Small animals always too heavy, large always too light. Our bodySize patches improve ratios vs vanilla but can't fix the fundamental 60kg base. Separate mod exists for this ("Animal Corpse Mass Realism Fix"). Not in our scope.

## 12. Crop Balance

- All vanilla raw food: nutrition=0.05 per unit (not changed — would break cooking recipes)
- Our changes: rot speed, mass, temperature requirements, price
- `minGrowthTemperature` (with "th"!) is the correct XML field name
- `minGrowTemperature` does NOT exist — was a bug in early patches

| Crop | Changes |
|------|---------|
| Potato | yield 14, rot 14d, mass 0.1, temp 2°C, price 0.9 |
| Rice | mass 0.02, temp 12°C |
| Corn | temp 10°C |
| Strawberry | rot 7d, temp 5°C, price 1.5 |
