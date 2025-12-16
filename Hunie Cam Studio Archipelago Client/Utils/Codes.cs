using Archipelago;
using Archipelago.MultiClient.Net.Enums;
using HunieCamStudioArchipelagoClient.Archipelago;
using System;
using UnityEngine.SceneManagement;

namespace HunieCamStudioArchipelagoClient.Utils
{
    public class Codes
    {
        public static bool dayreached = false;
        public static void ProcessCode(string code)
        {
            switch (code)
            {
                case "$goal":
                    if (SceneManager.GetActiveScene().name == "MainScene")
                    {
                        if (Convert.ToBoolean(HunieCamArchipelago.curse.connected.slot_data["goal"]))
                        {
                            ArchipelagoConsole.LogMessage("Goal is to get trophy");
                        }
                        else
                        {
                            ArchipelagoConsole.LogMessage("Goal is to get max talent/style all enabled girls");
                        }

                        if (Convert.ToBoolean(HunieCamArchipelago.curse.connected.slot_data["force_goal"]))
                        {
                            ArchipelagoConsole.LogMessage("force goal is enabled \"$goal\" code will not recheck/send locations");
                            return;
                        }

                        if (!dayreached)
                        {
                            ArchipelagoConsole.LogMessage("you must succesfully reach day 22 (in current client session) before you can recheck goal");
                            return;
                        }

                        ArchipelagoConsole.LogMessage("Checking and resending goal");
                        TrophyDefinition t = Game.Manager.Player.GetTrophyByCurrentFanCount();

                        if (!Convert.ToBoolean(HunieCamArchipelago.curse.connected.slot_data["goal"])) { return; }

                        if (t != null)
                        {
                            ArchipelagoConsole.LogMessage($"Trophy Level Achieved:{t.trophyName}");
                        }
                        else
                        {
                            ArchipelagoConsole.LogMessage($"Trophy Level Achieved:NONE");
                        }

                    }
                    else
                    {
                        ArchipelagoConsole.LogMessage("Must be in game to use goal code");
                    }
                    break;
                case "$day22":
                    dayreached = true;
                    break;
                case "$oldgame":
                    CursedArchipelagoClient.newconn = false;
                    break;
                default:
                    ArchipelagoConsole.LogMessage($"ERROR CODE:\"{code}\" NOT IMPLEMTED");
                    break;
            }
        }
    }
}
