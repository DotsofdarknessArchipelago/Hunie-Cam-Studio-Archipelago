using Archipelago;

namespace HunieCamStudioArchipelagoClient.Utils
{
    public class Ripper
    {
        public static void ripdata()
        { 
            ArchipelagoConsole.LogMessage("---------------GIRLS--------------");
            foreach (var item in Game.Data.Girls.GetAll())
            {
                string o = $"ID:{item.id}, Girl:{item.girlName}, huniepop girl: {item.IsHuniePopGirl}, start style:{item.startStyleLevel}, start talent:{item.startTalentLevel}, holdtillemploiecount:{item.holdUntilEmployeeCount}, drinks:{item.drinks}, smokes:{item.smokes}, race:{item.race}, personality:{item.personality}, ";

                o = o + "Fetish: (";
                foreach (var f in item.fetishes)
                {
                    o = o + $"(id:{f.id}, name:{f.fetishName})";
                }
                o = o + ")";
                ArchipelagoConsole.LogMessage(o);
            }
            ArchipelagoConsole.LogMessage("----------------------------------");
            ArchipelagoConsole.LogMessage("--------------LOCATIONS-----------");
            foreach (var item in Game.Data.Locations.GetAll())
            {
                ArchipelagoConsole.LogMessage($"loc id:{item.id}, location:{item.locationName}, activity id:{item.activity.id}, activity:{item.activity.activityName}");
            }
            ArchipelagoConsole.LogMessage("----------------------------------");
            ArchipelagoConsole.LogMessage("-------------FETISH---------------");
            foreach (var item in Game.Data.Fetishes.GetAll())
            {
                ArchipelagoConsole.LogMessage($"loc id:{item.id}, fetish:{item.fetishName}");
            }
            ArchipelagoConsole.LogMessage("----------------------------------");
            ArchipelagoConsole.LogMessage("-------------ACCESSORIES----------");
            foreach (var item in Game.Data.Accessories.GetAll())
            {
                ArchipelagoConsole.LogMessage($"loc id:{item.id}, fetish:{item.accessoryName}, type:{item.type}, use type:{item.useType}, mult:{item.multiplier}, fetish:{(item.fetishDefinition == null ? "NULL" : $"(id:{item.fetishDefinition.id}, name:{item.fetishDefinition.fetishName})")}");
            }
            ArchipelagoConsole.LogMessage("----------------------------------");
            ArchipelagoConsole.LogMessage("-------------WEBSITES-------------");
            foreach (var item in Game.Data.Websites.GetAll())
            {
                ArchipelagoConsole.LogMessage($"website id:{item.id}, website:{item.siteName}, fetish:(id:{item.fetishDefinition.id},name:{item.fetishDefinition.fetishName}), ordergroup:{item.orderGroup}");
            }
            ArchipelagoConsole.LogMessage("----------------------------------");
            ArchipelagoConsole.LogMessage("-------------TROPHIES-------------");
            foreach (var item in Game.Data.Trophies.GetAll())
            {
                ArchipelagoConsole.LogMessage($"Trophy id:{item.id}, Trophy:{item.trophyName}");
            }
            ArchipelagoConsole.LogMessage("----------------------------------");
            ArchipelagoConsole.LogMessage("-------------UPGRADES-------------");
            foreach (var item in Game.Data.Upgrades.GetAll())
            {
                string o = $"Upgrade id:{item.id}, Upgrade:{item.upgradeName}, visibleWhenLocked:{item.visibleWhenLocked}, levels(";
                foreach (UpgradeLevelDefinition i in item.levels)
                {
                    o = o + $"(lv value:{i.levelValue}, lv cost:{i.levelCost})";
                }
                o = o + ")";
                ArchipelagoConsole.LogMessage(o);
            }
            ArchipelagoConsole.LogMessage("----------------------------------");
            ArchipelagoConsole.LogMessage("-------------TUTORIAL-------------");
            foreach (var item in Game.Data.Tutorials.GetAll())
            {
                ArchipelagoConsole.LogMessage($"Tutorial id:{item.id}, Trigger:{item.triggerType}, Complete locked:{item.completeLocked}");
                int i = 1;
                string o = $"Tutorial id:{item.id}, Trigger:{item.triggerType}, Complete locked:{item.completeLocked}, Steps:(";
                foreach (TutorialStepDefinition s in item.steps)
                {
                    o = o + $"(i:{i}, Step Type:{s.stepType}, Step Proceed Type:{s.proceedType})";
                }
                o = o + ")";
                ArchipelagoConsole.LogMessage(o);
            }
            ArchipelagoConsole.LogMessage("----------------------------------");
        }
    }
}
