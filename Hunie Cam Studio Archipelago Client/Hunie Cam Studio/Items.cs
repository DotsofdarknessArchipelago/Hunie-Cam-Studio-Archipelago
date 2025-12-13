using Archipelago;
using HarmonyLib;
using HunieCamStudioArchipelagoClient.Archipelago;
using System;

namespace HunieCamStudioArchipelagoClient.Hunie_Cam_Studio
{
    [HarmonyPatch]
    public class Items
    {

        [HarmonyPatch(typeof(UiTopPanel), "OnMinuteTicked")]
        [HarmonyPostfix]
        public static void processitems()
        {
            bool t = true;
            while (ArchipelagoData.Index < CursedArchipelagoClient.items.Count)
            {
                if (t) { t = false; ArchipelagoConsole.LogMessage("Processing Items"); }
                if (CursedArchipelagoClient.items[ArchipelagoData.Index].item > Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["items_start"]))
                {
                    NetworkItem item = CursedArchipelagoClient.items[ArchipelagoData.Index];
                    if (Game.Manager.Player.accessories.Count > 9)
                    {
                        ArchipelagoConsole.LogMessage($"inventory too full for {HunieCamArchipelago.curse.data.data.games[CursedArchipelagoClient.game].idtoitem[Convert.ToInt32(item.item)]} giving money instead");
                        Game.Manager.Player.cash += 100;
                    }
                    else
                    {
                        ArchipelagoConsole.LogMessage($"Adding {HunieCamArchipelago.curse.data.data.games[CursedArchipelagoClient.game].idtoitem[Convert.ToInt32(item.item)]} to Inventory");
                        Game.Manager.Player.AddAccessory(Game.Data.Accessories.Get(itemtoid(item.item)));
                    }
                }

                ArchipelagoData.Index++;
            }
            //ArchipelagoClient.session.DataStorage[Scope.Slot, "index"] = ArchipelagoData.Index;
            if (!t && Game.Manager.Player.upgrades != null) { }
        }

        public static int itemtoid(long id)
        {
            int item = Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["items_start"]);
            switch (id-item)
            {
                case 1: return 2;//"Vibrator"
                case 2: return 3;//"Butt Plug"
                case 3: return 5;//"Ball Gag"
                case 4: return 9;//"Cat Ears"
                case 5: return 11;//"Water Bottle"
                case 6: return 12;//"Chocolate Cake"
                case 7: return 13;//"Condom"
                case 8: return 14;//"Antibiotics"
                case 9: return 15;//"Steroids"
                case 10: return 16;//"Nicotine Patch"
                case 11: return 17;//"Wine Box"
                case 12: return 18;//"Shopping Basket"
                case 13: return 19;//"Subscribe Pillow"
                case 14: return 20;//"Weed"
                case 15: return 21;//"Coke"
                case 16: return 22;//"Stripper Heels"
                case 17: return 23;//"Fashion Magazine"
                case 18: return 24;//"Piggy Bank"
                default:
                    break;
            }
            return -1;
        }
    }
}
