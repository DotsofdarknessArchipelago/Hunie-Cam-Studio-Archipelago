using Archipelago;
using Archipelago.MultiClient.Net.Enums;
using HarmonyLib;
using HunieCamStudioArchipelagoClient.Archipelago;
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
            ArchipelagoClient.session.DataStorage[Scope.Slot, "complete"] = true;

            if (__result == null && Convert.ToBoolean(ArchipelagoClient.ServerData.slotData["goal"])) { return; }

            if (!Convert.ToBoolean(ArchipelagoClient.ServerData.slotData["goal"]))
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
                    ArchipelagoClient.sendloc(1);
                }
            }

            ArchipelagoClient.sendloc(Convert.ToInt32(ArchipelagoClient.ServerData.slotData["trophy_loc_start"]) + 1);
            if (__result.trophyName == "Bronze") { return; }
            ArchipelagoClient.sendloc(Convert.ToInt32(ArchipelagoClient.ServerData.slotData["trophy_loc_start"]) + 2);
            if (__result.trophyName == "Silver") { return; }
            ArchipelagoClient.sendloc(Convert.ToInt32(ArchipelagoClient.ServerData.slotData["trophy_loc_start"]) + 3);
            if (__result.trophyName == "Gold") { return; }
            ArchipelagoClient.sendloc(Convert.ToInt32(ArchipelagoClient.ServerData.slotData["trophy_loc_start"]) + 4);
            if (__result.trophyName == "Platinum") { return; }
            ArchipelagoClient.sendloc(Convert.ToInt32(ArchipelagoClient.ServerData.slotData["trophy_loc_start"]) + 5);
            if (__result.trophyName == "Diamond") { return; }
        }
    }
}
