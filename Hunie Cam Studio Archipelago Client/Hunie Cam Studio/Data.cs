using HarmonyLib;
using HunieCamStudioArchipelagoClient.Archipelago;
using System.Collections.Generic;
using UnityEngine;

namespace HunieCamStudioArchipelagoClient.Hunie_Cam_Studio
{
    [HarmonyPatch]
    public class Data
    {
        [HarmonyPatch(typeof(FetishData), "Get")]
        [HarmonyPostfix]
        public static void fetishoverrite(int id,ref FetishDefinition __result, ref Dictionary<int, FetishDefinition> ____definitions, ref int ____highestId)
        {
            if (__result == null && id == 25)
            {
                FetishDefinition tmp = new FetishDefinition();
                tmp.id = id;
                tmp.fetishName = "Archipelago";
                ____definitions.Add(id, tmp);
                ____highestId++;
                __result = tmp;
            }
        }
        [HarmonyPatch(typeof(FetishData), "GetAll")]
        [HarmonyPrefix]
        public static void fetishoverrite2(ref Dictionary<int, FetishDefinition> ____definitions, ref int ____highestId)
        {
            if (____highestId!=25)
            {
                FetishDefinition tmp = new FetishDefinition();
                tmp.id = 25;
                tmp.fetishName = "Archipelago";
                ____definitions.Add(25, tmp);
                ____highestId++;
            }
        }

        [HarmonyPatch(typeof(GirlData), "Get")]
        [HarmonyPostfix]
        public static void girloverrite(int id,ref GirlDefinition __result)
        {
            if (ArchipelagoData.girldata != null)
            {
                if (ArchipelagoData.girldata.ContainsKey(id))
                {
                    __result = ArchipelagoData.girldata[id];
                }
            }
        }
        [HarmonyPatch(typeof(GirlData), "GetAll")]
        [HarmonyPrefix]
        public static void girloverrite2(ref Dictionary<int, GirlDefinition> ____definitions, ref int ____highestId)
        {
            if (ArchipelagoData.girldata != null)
            {
                ____definitions = ArchipelagoData.girldata;
            }
        }

        [HarmonyPatch(typeof(WebsiteData), "Get")]
        [HarmonyPostfix]
        public static void weboverrite(int id, ref WebsiteDefinition __result, ref Dictionary<int, WebsiteDefinition> ____definitions, ref int ____highestId)
        {
            if (__result == null && id == 73)
            {
                WebsiteDefinition tmp = ScriptableObject.CreateInstance<WebsiteDefinition>();
                tmp.id = id;
                tmp.siteName = "Archipelago.gg";
                tmp.orderGroup = 1;
                tmp.fetishDefinition = Game.Data.Fetishes.Get(25);
                ____definitions.Add(id, tmp);
                if (____highestId<id) { ____highestId = id; }
                __result = tmp;
            }
            else if (__result == null && id == 74)
            {
                WebsiteDefinition tmp = ScriptableObject.CreateInstance<WebsiteDefinition>();
                tmp.id = id;
                tmp.siteName = "girlcockx.com";
                tmp.orderGroup = 2;
                tmp.fetishDefinition = Game.Data.Fetishes.Get(25);
                ____definitions.Add(id, tmp);
                if (____highestId < id) { ____highestId = id; }
                __result = tmp;
            }
            else if (__result == null && id == 75)
            {
                WebsiteDefinition tmp = ScriptableObject.CreateInstance<WebsiteDefinition>();
                tmp.id = id;
                tmp.siteName = "bk.com";
                tmp.orderGroup = 3;
                tmp.fetishDefinition = Game.Data.Fetishes.Get(25);
                ____definitions.Add(id, tmp);
                if (____highestId < id) { ____highestId = id; }
                __result = tmp;
            }
        }
        [HarmonyPatch(typeof(WebsiteData), "GetAll")]
        [HarmonyPrefix]
        public static void weboverrite2(ref Dictionary<int, WebsiteDefinition> ____definitions, ref int ____highestId)
        {
            
            if (____highestId != 75)
            {
                if (!____definitions.ContainsKey(73))
                {
                    WebsiteDefinition tmp1 = ScriptableObject.CreateInstance<WebsiteDefinition>();
                    tmp1.id = 73;
                    tmp1.siteName = "Archipelago.gg";
                    tmp1.orderGroup = 1;
                    tmp1.fetishDefinition = Game.Data.Fetishes.Get(25);
                    ____definitions.Add(73, tmp1);
                }
                if (!____definitions.ContainsKey(74))
                {
                    WebsiteDefinition tmp2 = ScriptableObject.CreateInstance<WebsiteDefinition>();
                    tmp2.id = 74;
                    tmp2.siteName = "girlcockx.com";
                    tmp2.orderGroup = 2;
                    tmp2.fetishDefinition = Game.Data.Fetishes.Get(25);
                    ____definitions.Add(74, tmp2);
                }
                if (!____definitions.ContainsKey(75))
                {
                    WebsiteDefinition tmp3 = ScriptableObject.CreateInstance<WebsiteDefinition>();
                    tmp3.id = 75;
                    tmp3.siteName = "bk.com";
                    tmp3.orderGroup = 3;
                    tmp3.fetishDefinition = Game.Data.Fetishes.Get(25);
                    ____definitions.Add(75, tmp3);
                }
                ____highestId = 75; 
            }
        }
    }
}
