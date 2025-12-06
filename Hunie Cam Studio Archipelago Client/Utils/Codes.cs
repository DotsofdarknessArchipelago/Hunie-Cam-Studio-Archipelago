using Archipelago;
using Archipelago.MultiClient.Net.Enums;
using HunieCamStudioArchipelagoClient.Archipelago;
using System;
using UnityEngine.SceneManagement;

namespace HunieCamStudioArchipelagoClient.Utils
{
    public class Codes
    {
        public static void ProcessCode(string code)
        {
            switch (code)
            {
                case "$goal":
                    if (SceneManager.GetActiveScene().name == "MainScene")
                    {
                        if (Convert.ToBoolean(ArchipelagoClient.ServerData.slotData["goal"]))
                        {
                            ArchipelagoConsole.LogMessage("Goal is to get trophy");
                        }
                        else
                        {
                            ArchipelagoConsole.LogMessage("Goal is to get max talent/style all enabled girls");
                        }

                        if (Convert.ToBoolean(ArchipelagoClient.ServerData.slotData["force_goal"]))
                        {
                            ArchipelagoConsole.LogMessage("force goal is enabled \"$goal\" code will not recheck/send locations");
                            return;
                        }

                        if (ArchipelagoClient.session.DataStorage[Scope.Slot, "complete"] == false)
                        {
                            ArchipelagoConsole.LogMessage("you must succesfully reach day 22 before you can recheck goal");
                            return;
                        }

                        ArchipelagoConsole.LogMessage("Checking and resending goal");
                        TrophyDefinition t = Game.Manager.Player.GetTrophyByCurrentFanCount();

                        if (!Convert.ToBoolean(ArchipelagoClient.ServerData.slotData["goal"])) { return; }

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
                default:
                    ArchipelagoConsole.LogMessage($"ERROR CODE:\"{code}\" NOT IMPLEMTED");
                    break;
            }
        }
    }
}
