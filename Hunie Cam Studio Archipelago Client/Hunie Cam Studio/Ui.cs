using HarmonyLib;
using UnityEngine;

namespace HunieCamStudioArchipelagoClient.Utils
{
    [HarmonyPatch]
    public class Ui
    {
        [HarmonyPatch(typeof(UiTitleScreen), "Start")]
        [HarmonyPostfix]
        public static void start(UiTitleScreen __instance)
        {
            __instance.wardrobeButton.Enable(true);
            HunieCamArchipelago.awin = true;
        }

        [HarmonyPatch(typeof(UiWindowButton), "OnButtonPressed")]
        [HarmonyPostfix]
        public static void title1(UiWindowButton __instance)
        {
            if (__instance.name == "TitleScreenButtonWardrobe" || __instance.name == "TitleScreenButtonSettings" || __instance.name == "TitleScreenButtonCredits")
            {
                HunieCamArchipelago.awin = false;
            }
        }

        [HarmonyPatch(typeof(UiCreditsWindow), "OnCloseButtonPressed")]
        [HarmonyPatch(typeof(UiSettingsWindow), "OnCloseButtonPressed")]
        [HarmonyPatch(typeof(UiWardrobeWindow), "OnCloseButtonPressed")]
        [HarmonyPostfix]
        public static void title2(UiWindowButton __instance)
        {
                HunieCamArchipelago.awin = true;
        }


        [HarmonyPatch(typeof(UiFansWindow), "Start")]
        [HarmonyPrefix]
        public static void title1(UiFansWindow __instance)
        {
            if (__instance.fetishListItems.Count == 18)
            {
                GameObject l = GameObject.Instantiate(GameObject.Find("Canvas/WindowContainer/FansWindow/FansWindowContent/FansWindowFetishListMask/FansWindowFetishList/FansWindowFetishListItem17"));
                l.transform.SetParent(GameObject.Find("Canvas/WindowContainer/FansWindow/FansWindowContent/FansWindowFetishListMask/FansWindowFetishList").transform, true);
                __instance.fetishListItems.Add(l.GetComponent<UiFansWindowListItem>());

                GameObject p = GameObject.Instantiate(GameObject.Find("Canvas/WindowContainer/FansWindow/FansWindowContent/FansWindowPieChart/PieChartSlice17"));
                p.transform.SetParent(GameObject.Find("Canvas/WindowContainer/FansWindow/FansWindowContent/FansWindowPieChart").transform, true);
                __instance.pieChartSlices.Add(p.GetComponent<UiFansWindowPieSlice>());
            }
        }
        //Canvas/WindowContainer/FansWindow/FansWindowContent/FansWindowFetishListMask/FansWindowFetishList/FansWindowFetishListItem17
    }
}
