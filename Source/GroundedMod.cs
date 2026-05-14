using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace Grounded
{
    [StaticConstructorOnStartup]
    public static class StackLimitRecalculator
    {
        static StackLimitRecalculator()
        {
            int count = 0;
            foreach (ThingDef def in DefDatabase<ThingDef>.AllDefs)
            {
                if (def.stackLimit <= 1)
                    continue;

                if (def.IsStuff == false && def.thingCategories == null)
                    continue;

                float mass = def.BaseMass;
                if (mass <= 0f)
                    continue;

                float targetMass;
                int maxCap;

                if (IsExcluded(def))
                    continue;

                if (IsFood(def))
                {
                    targetMass = 30f;
                    maxCap = 500;
                }
                else if (IsFabric(def))
                {
                    targetMass = 30f;
                    maxCap = 300;
                }
                else if (IsLeather(def))
                {
                    targetMass = 30f;
                    maxCap = 200;
                }
                else
                {
                    continue;
                }

                int newStack = (int)Math.Floor(targetMass / mass);
                newStack = Math.Min(newStack, maxCap);
                newStack = Math.Max(newStack, def.stackLimit);

                if (newStack != def.stackLimit)
                {
                    def.stackLimit = newStack;
                    count++;
                }
            }

            Log.Message($"Grounded: recalculated stackLimit for {count} items");
        }

        static bool HasCategory(ThingDef def, params string[] names)
        {
            if (def.thingCategories == null)
                return false;
            return def.thingCategories.Any(c => names.Contains(c.defName));
        }

        static bool IsFood(ThingDef def)
        {
            return HasCategory(def, "Foods", "PlantFoodRaw", "MeatRaw", "AnimalProductRaw", "EggsUnfertilized", "EggsFertilized");
        }

        static bool IsFabric(ThingDef def)
        {
            if (def.stuffProps?.categories == null)
                return false;
            return def.stuffProps.categories.Any(c => c.defName == "Fabric");
        }

        static bool IsLeather(ThingDef def)
        {
            return HasCategory(def, "Leathers");
        }

        static bool IsExcluded(ThingDef def)
        {
            return HasCategory(def, "Mortar", "Ammo", "Artillery", "MortarShells", "Medicine");
        }
    }
}
