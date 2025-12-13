using HarmonyLib;
using HunieCamStudioArchipelagoClient.Archipelago;
using System;
using System.Collections.Generic;

namespace HunieCamStudioArchipelagoClient.Hunie_Cam_Studio
{
    [HarmonyPatch]
    public class Girl
    {

        [HarmonyPatch(typeof(GirlPlayerData), "style", MethodType.Setter)]
        [HarmonyPostfix]
        public static void styleprocess(GirlPlayerData __instance)
        {
            int loc = Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data[$"{__instance.girlDefinition.girlName}_loc_start"]);
            if (__instance.styleLevel >= 2)
            {
                HunieCamArchipelago.curse.sendLoc(loc+5);
            }
            if (__instance.styleLevel >= 3)
            {
                HunieCamArchipelago.curse.sendLoc(loc + 6);
            }
            if (__instance.styleLevel >= 4)
            {
                HunieCamArchipelago.curse.sendLoc(loc + 7);
            }
            if (__instance.styleLevel >= 5)
            {
                HunieCamArchipelago.curse.sendLoc(loc + 8);
            }
        }

        [HarmonyPatch(typeof(GirlPlayerData), "talent", MethodType.Setter)]
        [HarmonyPostfix]
        public static void talentprocess(GirlPlayerData __instance)
        {
            int loc = Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data[$"{__instance.girlDefinition.girlName}_loc_start"]);
            if (__instance.talentLevel >= 2)
            {
                HunieCamArchipelago.curse.sendLoc(loc + 1);
            }
            if (__instance.talentLevel >= 3)
            {
                HunieCamArchipelago.curse.sendLoc(loc + 2);
            }
            if (__instance.talentLevel >= 4)
            {
                HunieCamArchipelago.curse.sendLoc(loc + 3);
            }
            if (__instance.talentLevel >= 5)
            {
                HunieCamArchipelago.curse.sendLoc(loc + 4);
            }
        }

        [HarmonyPatch(typeof(TutorialManager), "ProgressTutorial")]
        [HarmonyPrefix]
        public static void startinggirls(TutorialManager __instance, ref UiTutorialContainer ____tutorialContainer)
        {
            if (____tutorialContainer.recruitableGirls.Count == 3)
            {
                ____tutorialContainer.recruitableGirls.Clear();
                ____tutorialContainer.recruitableGirls.Add(Game.Data.Girls.Get(girlnametoid(HunieCamArchipelago.curse.connected.slot_data["start_girl"].ToString())));
            }
        }

        [HarmonyPatch(typeof(PlayerManager), "GetRecruitableGirls")]
        [HarmonyPostfix]
        public static void recruitgirls(PlayerManager __instance, ref List<GirlDefinition> __result, ref Dictionary<int, GirlPlayerData> ____girls)
        {
            if (__result==null) { __result = new List<GirlDefinition>(); }
            __result.Clear();
            //ArchipelagoConsole.LogMessage("getting recruitable girls");
            foreach (var item in CursedArchipelagoClient.items)
            {
                if (item.item > Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["girls_start"]) && item.item <= Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["upgrade_item_start"]))
                {
                    if (!____girls.ContainsKey((int)item.item- Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["girls_start"])))
                    {
                        //ArchipelagoConsole.LogMessage($"adding girl with ID:{(int)item.ItemId - Convert.ToInt32(ArchipelagoClient.ServerData.slotData["girls_start"])} to list");
                        __result.Add(Game.Data.Girls.Get((int)item.item - Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["girls_start"])));
                    }
                }
            }
        }

        [HarmonyPatch(typeof(PlayerManager), "GetGirlData", [typeof(GirlDefinition)])]
        [HarmonyPrefix]
        public static bool getgirl(GirlDefinition girlDef, ref  GirlPlayerData __result, ref Dictionary<int, GirlPlayerData> ____girls)
        {
            if (!____girls.ContainsKey(girlDef.id)) { __result = null; return false; }
            return true;
        }



        public static int girlidtoloc(int id)
        {
            switch (id)
            {
                case 1:
                    return Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["Jessie_loc_start"]);
                case 2:
                    return Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["Nikki_loc_start"]);
                case 3:
                    return Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["Lillian_loc_start"]);
                case 4:
                    return Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["Nora_loc_start"]);
                case 5:
                    return Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["Lola_loc_start"]);
                case 6:
                    return Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["Kyanna_loc_start"]);
                case 7:
                    return Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["Candace_loc_start"]);
                case 8:
                    return Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["Renee_loc_start"]);
                case 9:
                    return Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["Tiffany_loc_start"]);
                case 10:
                    return Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["Brooke_loc_start"]);
                case 11:
                    return Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["Zoey_loc_start"]);
                case 12:
                    return Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["Beli_loc_start"]);
                case 13:
                    return Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["Audrey_loc_start"]);
                case 14:
                    return Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["Sarah_loc_start"]);
                case 15:
                    return Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["Lailani_loc_start"]);
                case 16:
                    return Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["Aiko_loc_start"]);
                case 17:
                    return Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["Nadia_loc_start"]);
                case 18:
                    return Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["Marlena_loc_start"]);
            }
            return -1;
        }

        public static int girlnametoid(string id)
        {
            switch (id)
            {
                case "Jessie":
                    return 1;
                case "Nikki":
                    return 2;
                case "Lillian":
                    return 3;
                case "Nora":
                    return 4;
                case "Lola":
                    return 5;
                case "Kyanna":
                    return 6;
                case "Candace":
                    return 7;
                case "Renee":
                    return 8;
                case "Tiffany":
                    return 9;
                case "Brooke":
                    return 10;
                case "Zoey":
                    return 11;
                case "Beli":
                    return 12;
                case "Audrey":
                    return 13;
                case "Sarah":
                    return 14;
                case "Lailani":
                    return 15;
                case "Aiko":
                    return 16;
                case "Nadia":
                    return 17;
                case "Marlena":
                    return 18;
            }
            return -1;
        }
    }
}
