using Archipelago;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Models;
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
            while (ArchipelagoData.Index < ArchipelagoClient.session.Items.AllItemsReceived.Count)
            {
                if (t) { t = false; ArchipelagoConsole.LogMessage("Processing Items"); }
                if (ArchipelagoClient.session.Items.AllItemsReceived[ArchipelagoData.Index].ItemId > Convert.ToInt32(ArchipelagoClient.ServerData.slotData["items_start"]))
                {
                    ItemInfo item = ArchipelagoClient.session.Items.AllItemsReceived[ArchipelagoData.Index];
                    if (Game.Manager.Player.accessories.Count > 9)
                    {
                        ArchipelagoConsole.LogMessage($"inventory too full for {item.ItemName} giving money instead");
                        Game.Manager.Player.cash += 100;
                    }
                    else
                    {
                        ArchipelagoConsole.LogMessage($"Adding {item.ItemName} to Inventory");
                        Game.Manager.Player.AddAccessory(Game.Data.Accessories.Get(itemtoid(item.ItemId)));
                    }
                }

                ArchipelagoData.Index++;
            }
            ArchipelagoClient.session.DataStorage[Scope.Slot, "index"] = ArchipelagoData.Index;
            if (!t && Game.Manager.Player.upgrades != null) { }
        }

        public static int itemtoid(long id)
        {
            int item = Convert.ToInt32(ArchipelagoClient.ServerData.slotData["items_start"]);
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
