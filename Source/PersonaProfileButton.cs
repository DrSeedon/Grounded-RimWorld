using System;
using System.Linq;
using System.Reflection;
using System.Text;
using HarmonyLib;
using RimWorld;
using RimTalk.UI;
using UnityEngine;
using Verse;

namespace Grounded
{
    [StaticConstructorOnStartup]
    public static class GroundedHarmonyInit
    {
        static GroundedHarmonyInit()
        {
            new Harmony("seedon.grounded").PatchAll();
        }
    }

    [HarmonyPatch(typeof(PersonaEditorWindow), "DoWindowContents")]
    public static class Patch_PersonaEditor_ProfileButton
    {
        static readonly FieldInfo PawnField = AccessTools.Field(typeof(PersonaEditorWindow), "_pawn");
        static readonly FieldInfo TextField = AccessTools.Field(typeof(PersonaEditorWindow), "_editingPersonality");

        public static void Postfix(Rect inRect, Window __instance)
        {
            if (PawnField == null || TextField == null)
                return;

            var pawn = PawnField.GetValue(__instance) as Pawn;
            if (pawn == null)
                return;

            float btnW = 80f;
            float btnH = 24f;
            float y = inRect.y + 267f;
            float x = inRect.xMax - btnW;

            Rect btnRect = new Rect(x, y, btnW, btnH);

            if (Widgets.ButtonText(btnRect, "Профиль"))
            {
                string profile = BuildProfile(pawn);
                TextField.SetValue(__instance, profile);
            }

            if (Mouse.IsOver(btnRect))
                TooltipHandler.TipRegion(btnRect, "Собрать профиль пешки: предыстория, черты, навыки, здоровье, экипировка, отношения");
        }

        static string BuildProfile(Pawn pawn)
        {
            var sb = new StringBuilder();

            if (pawn.story != null)
            {
                sb.AppendLine("=== ПРЕДЫСТОРИЯ ===");
                if (pawn.story.Childhood != null)
                {
                    sb.AppendLine($"Детство: {pawn.story.Childhood.TitleCapFor(pawn.gender)}");
                    string desc = pawn.story.Childhood.FullDescriptionFor(pawn);
                    if (!string.IsNullOrEmpty(desc))
                        sb.AppendLine(desc.Trim());
                }
                if (pawn.story.Adulthood != null)
                {
                    sb.AppendLine($"Взрослость: {pawn.story.Adulthood.TitleCapFor(pawn.gender)}");
                    string desc = pawn.story.Adulthood.FullDescriptionFor(pawn);
                    if (!string.IsNullOrEmpty(desc))
                        sb.AppendLine(desc.Trim());
                }
                sb.AppendLine();

                if (pawn.story.traits?.allTraits?.Count > 0)
                {
                    sb.AppendLine("=== ЧЕРТЫ ===");
                    foreach (var trait in pawn.story.traits.allTraits)
                    {
                        string label = trait.LabelCap;
                        string desc = trait.TipString(pawn)?.Trim();
                        sb.AppendLine(!string.IsNullOrEmpty(desc) ? $"{label}: {desc}" : label);
                    }
                    sb.AppendLine();
                }
            }

            if (pawn.skills?.skills != null)
            {
                sb.AppendLine("=== НАВЫКИ ===");
                foreach (var skill in pawn.skills.skills.Where(s => !s.TotallyDisabled))
                {
                    string passion = skill.passion switch
                    {
                        Passion.Minor => " ★",
                        Passion.Major => " ★★",
                        _ => ""
                    };
                    sb.AppendLine($"{skill.def.LabelCap}: {skill.Level}{passion}");
                }
                sb.AppendLine();
            }

            if (pawn.health?.hediffSet != null)
            {
                var visible = pawn.health.hediffSet.hediffs
                    .Where(h => h.Visible && h.def.defName != "RimTalk_PersonaData")
                    .ToList();
                if (visible.Count > 0)
                {
                    sb.AppendLine("=== ЗДОРОВЬЕ ===");
                    foreach (var h in visible)
                        sb.AppendLine($"{h.LabelCap} ({h.Part?.LabelCap ?? "общее"})");
                    sb.AppendLine();
                }
            }

            sb.AppendLine("=== ЭКИПИРОВКА ===");
            if (pawn.equipment?.Primary != null)
                sb.AppendLine($"Оружие: {pawn.equipment.Primary.LabelCap}");
            if (pawn.apparel?.WornApparel?.Count > 0)
                sb.AppendLine($"Одежда: {string.Join(", ", pawn.apparel.WornApparel.Select(a => a.LabelCap))}");
            sb.AppendLine();

            if (pawn.relations?.DirectRelations?.Count > 0)
            {
                sb.AppendLine("=== ОТНОШЕНИЯ ===");
                foreach (var rel in pawn.relations.DirectRelations)
                {
                    string other = rel.otherPawn?.Name?.ToStringShort ?? "???";
                    string dead = rel.otherPawn?.Dead == true ? " (мёртв)" : "";
                    sb.AppendLine($"{rel.def.LabelCap}: {other}{dead}");
                }
                sb.AppendLine();
            }

            if (pawn.genes?.GenesListForReading?.Count > 0)
            {
                var active = pawn.genes.GenesListForReading
                    .Where(g => g.Active && g.def.displayCategory != null)
                    .ToList();
                if (active.Count > 0)
                {
                    sb.AppendLine("=== ГЕНЫ ===");
                    string xenoLabel = pawn.genes.XenotypeLabel;
                    if (!string.IsNullOrEmpty(xenoLabel))
                        sb.AppendLine($"Ксенотип: {xenoLabel}");
                    foreach (var g in active)
                        sb.AppendLine($"- {g.LabelCap}");
                    sb.AppendLine();
                }
            }

            return sb.ToString().Trim();
        }
    }
}
