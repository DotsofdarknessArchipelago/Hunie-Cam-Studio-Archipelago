using HarmonyLib;
using System.IO;
using UnityEngine;

namespace HunieCamStudioArchipelagoClient.Utils
{
    [HarmonyPatch]
    public class Save
    {

        [HarmonyPatch(typeof(GamePersistence), MethodType.Constructor)]
        [HarmonyPostfix]
        public static void saveover(GamePersistence __instance, ref string ____loadFileName, ref string ____saveFileName)
        {
            if (File.Exists(Application.persistentDataPath + "/ARCHSaveData.dat"))
            {
                ____loadFileName = Application.persistentDataPath + "/ARCHSaveData.dat";
            }
            ____saveFileName = Application.persistentDataPath + "/ARCHSaveData.dat";

        }

        [HarmonyPatch(typeof(GamePersistence), "Save")]
        [HarmonyPrefix]
        public static void saveover2(ref string ____saveFileName)
        {
            ____saveFileName = Application.persistentDataPath + "/ARCHSaveData.dat";
        }

        [HarmonyPatch(typeof(GamePersistence), "Load")]
        [HarmonyPostfix]
        public static void saveover3(ref string ____loadFileName)
        {
            ____loadFileName = Application.persistentDataPath + "/ARCHSaveData.dat";
        }

        [HarmonyPatch(typeof(SaveData), "ResetData")]
        [HarmonyPostfix]
        public static void saveover3(SaveData __instance)
        {
            __instance.totalTokens = 69;
            for (int i = 1; i < 19; i++)
            {
                WardrobeGirlSaveData d = new WardrobeGirlSaveData();
                d.girlId = i;
                d.hairstyleCount = 5;
                d.outfitCount = 5;
                __instance.wardrobeGirls.Add(d);
            }
        }

    }
}
