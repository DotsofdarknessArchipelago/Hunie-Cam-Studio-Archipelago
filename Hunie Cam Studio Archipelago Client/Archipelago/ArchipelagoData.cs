using System.Collections.Generic;
using Archipelago.MultiClient.Net.Models;
using Newtonsoft.Json;

namespace HunieCamStudioArchipelagoClient.Archipelago;

public class ArchipelagoData
{
    public string Uri = "";
    public string SlotName = "";
    public string Password = "";
    public static int Index;

    public List<long> CheckedLocations;

    /// <summary>
    /// seed for this archipelago data. Can be used when loading a file to verify the session the player is trying to
    /// load is valid to the room it's connecting to.
    /// </summary>
    private string seed;

    public Dictionary<string, object> slotData;

    public static Dictionary<string, int> worldversion = null;

    public bool NeedSlotData => slotData == null;

    public static Dictionary<int, GirlDefinition> girldata = null;
    public static List<string> enabledgirls = null;
    public static Dictionary<long, ScoutedItemInfo> shopdata = null;
    

    public ArchipelagoData()
    {
        Uri = "localhost";
        SlotName = "Player1";
        CheckedLocations = new();
    }

    public ArchipelagoData(string uri, string slotName, string password)
    {
        Uri = uri;
        SlotName = slotName;
        Password = password;
        CheckedLocations = new();
    }

    /// <summary>
    /// assigns the slot data and seed to our data handler. any necessary setup using this data can be done here.
    /// </summary>
    /// <param name="roomSlotData">slot data of your slot from the room</param>
    /// <param name="roomSeed">seed name of this session</param>
    public void SetupSession(Dictionary<string, object> roomSlotData, string roomSeed)
    {
        slotData = roomSlotData;
        seed = roomSeed;
    }

    /// <summary>
    /// returns the object as a json string to be written to a file which you can then load
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}