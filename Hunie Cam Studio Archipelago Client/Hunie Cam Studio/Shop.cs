using HarmonyLib;
using HunieCamStudioArchipelagoClient.Archipelago;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HunieCamStudioArchipelagoClient.Hunie_Cam_Studio
{
    [HarmonyPatch]
    public class Shop
    {

        [HarmonyPatch(typeof(PlayerManager), "storeAccessories", MethodType.Setter)]
        [HarmonyPostfix]
        public static void archshop(PlayerManager __instance, ref List<AccessoryDefinition> ____storeAccessories)
        {
            if (____storeAccessories == null || ____storeAccessories.Count == 0) { return; }
            int shopi = Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["shop_items"]);
            int shops = Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["shop_loc_start"]);
            if (shopi > 0)
            {
                List<long> l = new List<long>();
                for (int i = 1; i <= shopi; i++)
                {
                    if (HunieCamArchipelago.curse.connected.checked_locations.Contains(shops + i)) { continue; }
                    l.Add(shops + i);
                }

                if (l.Count > 0)
                {
                    ListUtils.ShuffleList(l);
                    int i = new System.Random().Next(____storeAccessories.Count);
                    AccessoryDefinition t = Game.Data.Accessories.Get(13);
                    AccessoryDefinition def = ScriptableObject.CreateInstance<AccessoryDefinition>();
                    //def.icon = t.icon;
                    if (HunieCamArchipelago.arch == null)
                    {
                        def.icon = t.icon;
                    }
                    else
                    {
                        def.icon = Sprite.Create(HunieCamArchipelago.arch, new Rect(0, 0, HunieCamArchipelago.arch.width, HunieCamArchipelago.arch.height), new Vector2(0, 0), 100);
                    }
                    def.id = (int)l[0];
                    def.accessoryName = $"Archipelago Item #{l[0] - Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["shop_loc_start"])}";
                    def.name = $"Archipelago Item #{l[0] - Convert.ToInt32(HunieCamArchipelago.curse.connected.slot_data["shop_loc_start"])}";
                    def.description = $"buy to send {ArchipelagoData.shopdata[l[0]].Player.Name} an item in {ArchipelagoData.shopdata[l[0]].Player.Game}";
                    ____storeAccessories[i] = def;
                }
            }
        }

        [HarmonyPatch(typeof(PlayerManager), "AddAccessory")]
        [HarmonyPostfix]
        public static void archshop2(PlayerManager __instance, AccessoryDefinition accessoryDefinition)
        {
            if (accessoryDefinition == null) { return; }
            if (accessoryDefinition.name.StartsWith("Archipelago"))
            {
                HunieCamArchipelago.curse.sendLoc(accessoryDefinition.id);
                for (int i = 0; i < __instance.accessories.Count; i++)
                {
                    if (__instance.accessories[i].accessoryDefinition.accessoryName == accessoryDefinition.accessoryName) { __instance.RemoveAccessory(__instance.accessories[i]); return; }
                }
            }
        }
    }
}
