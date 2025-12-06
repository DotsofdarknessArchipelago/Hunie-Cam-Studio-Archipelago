using HarmonyLib;
using HunieCamStudioArchipelagoClient.Archipelago;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HunieCamStudioArchipelagoClient.Hunie_Cam_Studio
{
    [HarmonyPatch]
    public class Upgrade
    {
        [HarmonyPatch(typeof(PlayerManager), "upgrades", MethodType.Getter)]
        [HarmonyPrefix]
        public static void start(ref Dictionary<UpgradeDefinition, int> ____upgrades, PlayerManager __instance)
        {
            Dictionary<UpgradeDefinition, int> o = new Dictionary<UpgradeDefinition, int>();
            List<UpgradeDefinition> all = Game.Data.Upgrades.GetAll();
            for (int i = 0; i < all.Count; i++)
            {
                o.Add(all[i], 0);
            }
            foreach (var item in ArchipelagoClient.session.Items.AllItemsReceived)
            {
                switch (item.ItemId - Convert.ToInt32(ArchipelagoClient.ServerData.slotData["upgrade_item_start"]))
                {
                    case 1:
                        o[Game.Data.Upgrades.Get(1)]++;
                        break;
                    case 2:
                        o[Game.Data.Upgrades.Get(2)]++;
                        break;
                    case 3:
                        o[Game.Data.Upgrades.Get(3)]++;
                        break;
                    case 4:
                        o[Game.Data.Upgrades.Get(4)]++;
                        break;
                    case 5:
                        o[Game.Data.Upgrades.Get(5)]++;
                        break;
                    case 6:
                        o[Game.Data.Upgrades.Get(6)]++;
                        break;
                    case 7:
                        o[Game.Data.Upgrades.Get(7)]++;
                        break;
                    case 8:
                        o[Game.Data.Upgrades.Get(8)]++;
                        break;
                    case 9:
                        o[Game.Data.Upgrades.Get(9)]++;
                        break;
                    case 10:
                        o[Game.Data.Upgrades.Get(10)]++;
                        break;
                    case 11:
                        o[Game.Data.Upgrades.Get(11)]++;
                        break;
                    case 12:
                        o[Game.Data.Upgrades.Get(12)]++;
                        break;
                    default:
                        break;
                }
            }
            ____upgrades = o;
            if (__instance.booths.Count() != (o[Game.Data.Upgrades.Get(1)] + 1))
            {
                __instance.AddBooth(false);
            }
        }


        [HarmonyPatch(typeof(UiInvestWindowUpgrade), "Refresh")]
        [HarmonyPostfix]
        public static void start(UiInvestWindowUpgrade __instance, UpgradeDefinition upgradeDef)
        {
            if (upgradeDef == null) return;
            int lv = 0;
            foreach (var item in ArchipelagoClient.session.Items.AllItemsReceived)
            {
                if (item.ItemId == Convert.ToInt32(ArchipelagoClient.ServerData.slotData["upgrade_item_start"]) + upgradeDef.id)
                {
                    lv++;
                }
            }
            
            __instance.buyButton.gameObject.SetActive(false);

            if(lv>= upgradeDef.levels.Count) { lv = upgradeDef.levels.Count - 1; }

            __instance.descriptionLabel.text = __instance.upgradeDefinition.upgradeDescription.Replace("[X]", upgradeDef.levels[lv].levelValue.ToString()).Replace("[XX]",((upgradeDef.levels[lv].levelValue - 1) * upgradeDef.descXXMultiplier).ToString());
            __instance.costLabel.text = $"{lv}/{upgradeDef.levels.Count-1}";
        }

        [HarmonyPatch(typeof(PlayerManager), "GetUpgradeLevel", [typeof(UpgradeDefinition),typeof(int)])]
        [HarmonyPostfix]
        public static void upgradelimit(UpgradeDefinition upgradeDefinition, int offset, PlayerManager __instance,ref UpgradeLevelDefinition __result)
        {
            if (__result == null && (__instance.GetUpgradeLevelIndex(upgradeDefinition) + offset) >= upgradeDefinition.levels.Count)
            {
                __result = upgradeDefinition.levels[upgradeDefinition.levels.Count-1];
            }
        }

        [HarmonyPatch(typeof(UiInvestWindowUpgrade), "Refresh")]
        [HarmonyILManipulator]
        public static void waudrobeoverite(ILContext ctx, MethodBase orig)
        {
            bool b = true;
            for (int i = 0; i < ctx.Instrs.Count; i++)
            {
                if (ctx.Instrs[i].OpCode == OpCodes.Ldc_I4_1) 
                {
                    if (b) { b = !b;continue; }
                    ctx.Instrs[i].OpCode = OpCodes.Ldc_I4;
                    ctx.Instrs[i].Operand = 30;
                    
                }
            }
        }
    }
}
