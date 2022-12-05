using System.Reflection;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace MyMod
{
    public class ModSettings : Verse.ModSettings
    {
        public float nicknameProbability = 0.9f;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref nicknameProbability, "nicknameProbability", 0.9f);
        }
    }

    [HarmonyPatch(typeof(PawnBioAndNameGenerator))]
    [HarmonyPatch("GenerateFullPawnName")]
    public static class PawnBioAndNameGenerator_GenerateFullPawnName_Patch
    {
        [HarmonyPrefix]
        public static bool Prefix(ref Name __result, ThingDef genFor, RulePackDef pawnKindNameMaker, Pawn_StoryTracker story, XenotypeDef xenotype, RulePackDef nameGenner, CultureDef primaryCulture, Gender gender, PawnNameCategory nameCategory, string forcedLastName, ref bool forceNoNick)
        {
            var mod = LoadedModManager.GetMod<MostPeopleDontHaveANickName>();
            if (mod == null) return true;

            if (Rand.Value < mod.settings.nicknameProbability)
            {
                forceNoNick = true;
            }
            else
            {
                forceNoNick = false;
            }
            return true;
        }
    }

    public class MostPeopleDontHaveANickName : Mod
    {
        public ModSettings settings;

        public MostPeopleDontHaveANickName(ModContentPack content) : base(content)
        {
            settings = GetSettings<ModSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);

            // Add label that displays the current value of the nicknameProbability setting.
            listingStandard.Label($"Chance a pawn will just go by their first name: {settings.nicknameProbability * 100}%");

            // Draw the nickname probability slider.
            settings.nicknameProbability = listingStandard.Slider(settings.nicknameProbability, 0f, 1f);

            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Most People Dont Have A Nickname";
        }
    }
}
