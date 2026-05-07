# Grounded: Realistic Stats Overhaul

![Grounded](About/Preview.png)

A pure XML mod for RimWorld 1.6 that brings real-world biology and agriculture into the game — without breaking gameplay.

## Features

### 🐾 Animals (59 vanilla + DLC + mod animals)
All animal stats recalculated from real-world zoological data:
- **Body size** — realistic mass by life stage
- **Speed** — based on real locomotion data
- **Lifespan** — actual species life expectancy
- **Gestation** — scaled from Earth's 365-day to RimWorld's 60-day cycle
- **Litter size** — species-accurate breeding
- **Diet & hunger** — realistic food type and consumption
- **Temperature tolerance** — biome-appropriate ranges
- **Meat & leather yield** — proportional to body size (MeatAmount = bodySize × 100)
- **Manhunter chance** — adjusted per species temperament

### 🌾 Crops
Differentiated crop economics for meaningful farming choices:

| Crop | Change | Why |
|------|--------|-----|
| 🥔 **Potato** | Yield 11→14, Rot 30→14 days, heavier | IRL yield king, but spoils fast without cold storage |
| 🍚 **Rice** | Lighter, needs 12°C+ | Tropical crop, stores well but labor-intensive |
| 🌽 **Corn** | No changes | Already perfectly balanced |
| 🍓 **Strawberry** | Rot 14→7 days, price up | Luxury delicacy that spoils quickly |

### 🔊 Animal Sounds
New sound effects for: Fox, Jaguar, Leopard, Ostrich, Puma, Tiger, Turkey, Wolf

## Compatibility

| Mod | Status |
|-----|--------|
| Combat Extended | ✅ Full support (body part groups, armor, bionics) |
| Vanilla Animals Expanded | ✅ All packs |
| Alpha Animals | ✅ |
| DLC: Biotech | ✅ |
| DLC: Odyssey | ✅ |

## Installation

1. Download or clone this repository
2. Copy the folder into your RimWorld `Mods/` directory
3. Enable "Grounded: Realistic Stats Overhaul" in the mod list
4. No dependencies required — no Harmony, no DLL

## Design Philosophy

- **No Harmony patches** — pure XML, zero risk of conflicts
- **No DLL** — nothing to compile, nothing to crash
- **No behavior changes** — animals act vanilla, just with realistic numbers
- **Every buff has a nerf** — more potato yield = faster rot; lighter rice = needs warmth

## Credits

Animal stat data derived from zoological literature. Crop balance inspired by real-world agricultural data.

## License

Free to use, modify, and redistribute.

Portions of the materials used to create this content/mod are trademarks and/or copyrighted works of Ludeon Studios Inc. All rights reserved by Ludeon. This content/mod is not official and is not endorsed by Ludeon.
