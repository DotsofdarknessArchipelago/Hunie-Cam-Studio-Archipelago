using HarmonyLib;

namespace HunieCamStudioArchipelagoClient.Hunie_Cam_Studio
{
    [HarmonyPatch]
    public class Locations
    {
        [HarmonyPatch(typeof(LocationPlayerData), "GetActivityWarning")]
        [HarmonyPostfix]
        public static void locedit(ref string __result, ref LocationDefinition ____locationDefinition)
        {
            if(__result == null && ____locationDefinition.id == 7 && Game.Manager.Player.GetRecruitableGirls().Count == 0) 
            {
                __result = "No Girls Are Avaliable To Be Recruited";
            }
        }
    }
}
