using System;
using System.Linq;
using Verse;

namespace Grounded
{
    [StaticConstructorOnStartup]
    public static class StackLimitRecalculator
    {
        const float TargetMass = 30f;
        const int MaxCap = 500;

        static StackLimitRecalculator()
        {
            int count = 0;
            foreach (ThingDef def in DefDatabase<ThingDef>.AllDefs)
            {
                if (def.stackLimit <= 1)
                    continue;

                float mass = def.BaseMass;
                if (mass <= 0f)
                    continue;

                if (IsMedicine(def))
                    continue;

                int newStack = (int)Math.Floor(TargetMass / mass);
                newStack = Math.Min(newStack, MaxCap);
                newStack = Math.Max(newStack, def.stackLimit);

                if (newStack != def.stackLimit)
                {
                    def.stackLimit = newStack;
                    count++;
                }
            }

            Log.Message($"Grounded: recalculated stackLimit for {count} items");
        }

        static bool IsMedicine(ThingDef def)
        {
            if (def.thingCategories == null)
                return false;
            return def.thingCategories.Any(c => c.defName == "Medicine");
        }
    }
}
