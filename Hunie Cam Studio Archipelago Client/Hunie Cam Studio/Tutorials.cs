using Archipelago;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HunieCamStudioArchipelagoClient.Hunie_Cam_Studio
{
    [HarmonyPatch]
    public class Tutorials
    {

        [HarmonyPatch(typeof(TutorialManager), "ProgressTutorial")]
        [HarmonyPrefix]
        public static void tutorialovverwite(ref bool skipStep, TutorialManager __instance, List<TutorialDefinition> ____incompleteTutorials)
        {
            if (Game.Manager.Player.activeTutorial == null) return;
            ArchipelagoConsole.LogDebug($"PROGRESSING TUTORIAL ID:{Game.Manager.Player.activeTutorial.id}");
            if (Game.Manager.Player.activeTutorial.id != 1) return;
            ArchipelagoConsole.LogDebug($"CURRENT STEP:{Game.Manager.Player.activeTutorialStep}");

            if (Game.Manager.Player.activeTutorialStep >= 8)
            {
                if (Game.Manager.Player.activeTutorialStep + 1 == Game.Manager.Player.activeTutorial.steps.Count)
                {
                    ArchipelagoConsole.LogDebug("SKIPPING TUTORIALS");
                    //__instance.SkipTutorial();
                    for (int i = 0; i < ____incompleteTutorials.Count; i++)
                    {
                        if (!____incompleteTutorials[i].completeLocked)
                        {
                            Game.Manager.Player.SkipTutorial(____incompleteTutorials[i]);
                            ____incompleteTutorials.RemoveAt(i);
                            i--;
                        }
                    }
                    List<LocationDefinition> all = Game.Data.Locations.GetAll();
                    for (int j = 0; j < all.Count; j++)
                    {
                        Game.Manager.Player.UnlockLocation(all[j]);
                    }
                    List<WindowDefinition> all2 = Game.Data.Windows.GetAll();
                    for (int k = 0; k < all2.Count; k++)
                    {
                        if (all2[k].unlockable)
                        {
                            Game.Manager.Player.UnlockWindow(all2[k]);
                        }
                    }
                }
                if (!skipStep) skipStep = true;
            }

        }

    }
}
