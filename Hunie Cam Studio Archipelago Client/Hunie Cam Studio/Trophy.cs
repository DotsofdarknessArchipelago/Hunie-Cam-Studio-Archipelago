using Archipelago;
using Archipelago.MultiClient.Net.Enums;
using HarmonyLib;
using HunieCamStudioArchipelagoClient.Archipelago;
using HunieCamStudioArchipelagoClient.Utils;
using System;

namespace HunieCamStudioArchipelagoClient.Hunie_Cam_Studio
{
    [HarmonyPatch]
    public class Trophy
    {
        [HarmonyPatch(typeof(PlayerManager), "GetTrophyByCurrentFanCount")]
        [HarmonyPostfix]
        public static void trophy(TrophyDefinition __result)
        {
            Codes.dayreached = true;

            if (__result == null && Convert.ToBoolean(HunieCamArchipelago.curse.connected.slot_data["goal"])) { return; }

            if (!Convert.ToBoolean(HunieCamArchipelago.curse.connected.slot_data["goal"]))
            {
                bool b = true;
                ArchipelagoConsole.LogMessage($"checking talent/style level of enabled girls");
                foreach (var item in ArchipelagoData.enabledgirls)
                {
                    if (Game.Manager.Player.GetGirlData(Game.Data.Girls.Get(Girl.girlnametoid(item))) == null)
                    {
                        ArchipelagoConsole.LogMessage($"{item}'s style/talent not maxed out1");
                        b = false;
                        continue;
                    }
                    if (Game.Manager.Player.GetGirlData(Game.Data.Girls.Get(Girl.girlnametoid(item))).styleLevel != 5 || Game.Manager.Player.GetGirlData(Game.Data.Girls.Get(Girl.girlnametoid(item))).talentLevel != 5)
                    {
                        ArchipelagoConsole.LogMessage($"{item}'s style/talent not maxed out2");
                        b = false;
                    }
                }
                if (b)
                {
                    HunieCamArchipelago.curse.sendLoc(1);
                    HunieCamArchipelago.curse.sendCompletion();
                }
            }

            HunieCamArchipelago.curse.sendLoc(Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["trophy_loc_start"]) + 1);
            if (Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["min_trophy"]) == 0) { HunieCamArchipelago.curse.sendCompletion(); }
            if (__result.trophyName == "Bronze") { return; }
            HunieCamArchipelago.curse.sendLoc(Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["trophy_loc_start"]) + 2);
            if (Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["min_trophy"]) == 1) { HunieCamArchipelago.curse.sendCompletion(); }
            if (__result.trophyName == "Silver") { return; }
            HunieCamArchipelago.curse.sendLoc(Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["trophy_loc_start"]) + 3);
            if (Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["min_trophy"]) == 2) { HunieCamArchipelago.curse.sendCompletion(); }
            if (__result.trophyName == "Gold") { return; }
            HunieCamArchipelago.curse.sendLoc(Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["trophy_loc_start"]) + 4);
            if (Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["min_trophy"]) == 3) { HunieCamArchipelago.curse.sendCompletion(); }
            if (__result.trophyName == "Platinum") { return; }
            HunieCamArchipelago.curse.sendLoc(Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["trophy_loc_start"]) + 5);
            HunieCamArchipelago.curse.sendCompletion();
            if (__result.trophyName == "Diamond") { return; }
        }
    }
}
