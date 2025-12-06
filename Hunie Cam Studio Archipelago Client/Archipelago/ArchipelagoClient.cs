using Archipelago;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.Models;
using Archipelago.MultiClient.Net.Packets;
using HunieCamStudioArchipelagoClient.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace HunieCamStudioArchipelagoClient.Archipelago;

public class ArchipelagoClient
{
    public const string APVersion = "0.5.0";
    private const string game = "Hunie Cam Studio";

    public static bool Authenticated;
    private bool attemptingConnection;

    public static Queue<ItemInfo> itemstoprocess = new Queue<ItemInfo>();

    public static ArchipelagoData ServerData = new();
    public static ArchipelagoSession session;

    public static bool slotstate = false;

    public const int apmajor = 0;
    public const int apminor = 1;
    public const int apbuild = 0;

    /// <summary>
    /// call to connect to an Archipelago session. Connection info should already be set up on ServerData
    /// </summary>
    /// <returns></returns>
    public void Connect()
    {
        if (Authenticated || attemptingConnection) return;

        try
        {
            session = ArchipelagoSessionFactory.CreateSession(ServerData.Uri);
            SetupSession();
        }
        catch (Exception e)
        {
            HunieCamArchipelago.BepinLogger.LogError(e);
        }

        TryConnect();
    }

    /// <summary>
    /// add handlers for Archipelago events
    /// </summary>
    private void SetupSession()
    {
        session.MessageLog.OnMessageReceived += message => ArchipelagoConsole.LogMessage(message.ToString());
        session.Items.ItemReceived += OnItemReceived;
        session.Socket.ErrorReceived += OnSessionErrorReceived;
        session.Socket.SocketClosed += OnSessionSocketClosed;
    }

    /// <summary>
    /// attempt to connect to the server with our connection info
    /// </summary>
    private void TryConnect()
    {
        try
        {
            // it's safe to thread this function call but unity notoriously hates threading so do not use excessively
            ThreadPool.QueueUserWorkItem(
                _ => HandleConnectResult(
                    session.TryConnectAndLogin(
                        game,
                        ServerData.SlotName,
                        ItemsHandlingFlags.AllItems,
                        new Version(APVersion),
                        password: ServerData.Password,
                        requestSlotData: ServerData.NeedSlotData
                    )));
        }
        catch (Exception e)
        {
            HunieCamArchipelago.BepinLogger.LogError(e);
            HandleConnectResult(new LoginFailure(e.ToString()));
            attemptingConnection = false;
        }
    }

    /// <summary>
    /// handle the connection result and do things
    /// </summary>
    /// <param name="result"></param>
    private void HandleConnectResult(LoginResult result)
    {
        string outText;
        if (result.Successful)
        {
            var success = (LoginSuccessful)result;

            ArchipelagoData.Index = session.DataStorage[Scope.Slot, "index"];
            slotstate = session.DataStorage[Scope.Slot, "state"];

            session.DataStorage[Scope.Slot, "complete"].Initialize(false);

            ServerData.SetupSession(success.SlotData, session.RoomState.Seed);
            Authenticated = true;

            session.Locations.CompleteLocationChecksAsync(null, ServerData.CheckedLocations.ToArray());
            outText = $"Successfully connected to {ServerData.Uri} as {ServerData.SlotName}!";

            ArchipelagoConsole.LogDebug($"slot forcegoal = {Convert.ToBoolean(ServerData.slotData["force_goal"])}");
            ArchipelagoConsole.LogDebug($"slot goal = {Convert.ToBoolean(ServerData.slotData["force_goal"])}");

            if (Convert.ToInt32(ServerData.slotData["shop_items"]) > 0)
            {
                ArchipelagoConsole.LogDebug($"slot has {Convert.ToInt32(ServerData.slotData["shop_items"])} shop locations");
                List<long> shopid = new List<long>();
                for (int i = 0; i < Convert.ToInt32(ServerData.slotData["shop_items"]); i++)
                {
                    shopid.Add(Convert.ToInt32(ServerData.slotData["shop_loc_start"]) + i + 1);
                }
                session.Locations.ScoutLocationsAsync((x) => ArchipelagoData.shopdata = x, HintCreationPolicy.None, shopid.ToArray());
            }

            ArchipelagoData.worldversion = JsonConvert.DeserializeObject<Dictionary<string, int>>(ServerData.slotData["world_version"].ToString());
            if (ArchipelagoData.worldversion["major"] != apmajor || ArchipelagoData.worldversion["minor"] != apminor || ArchipelagoData.worldversion["build"] != apbuild)
            {
                ArchipelagoConsole.LogError($"APWORLD VERSION ERROR\nEXPECTED: V{apmajor}.{apminor}.{apbuild} GOT V{ArchipelagoData.worldversion["major"]}.{ArchipelagoData.worldversion["minor"]}.{ArchipelagoData.worldversion["build"]}");
            }

            Dictionary<string, List<int>> data = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(ServerData.slotData["girldata"].ToString());
            ArchipelagoData.enabledgirls = JsonConvert.DeserializeObject<List<string>>(ServerData.slotData["enabled_girls"].ToString());
            HunieCamArchipelago.temp = data;

            foreach (var g in Game.Data.Girls.GetAll())
            {
                if (ArchipelagoData.girldata == null) { ArchipelagoData.girldata = new Dictionary<int, GirlDefinition>(); }
                List<int> t = data[g.girlName];
                g.startStyleLevel = 1;
                g.startTalentLevel = 1;
                g.holdUntilEmployeeCount = 0;
                g.smokes = (GirlSmokesType)t[0];
                g.drinks = (GirlDrinksType)t[1];
                g.fetishes.Clear();
                g.fetishes.Add(Game.Data.Fetishes.Get(t[2]));
                g.fetishes.Add(Game.Data.Fetishes.Get(t[3]));
                ArchipelagoData.girldata.Add(g.id, g);
            }
            Game.Data.Girls.GetAll();

            ArchipelagoConsole.LogMessage(outText);
        }
        else
        {
            var failure = (LoginFailure)result;
            outText = $"Failed to connect to {ServerData.Uri} as {ServerData.SlotName}.";
            outText = failure.Errors.Aggregate(outText, (current, error) => current + $"\n    {error}");

            HunieCamArchipelago.BepinLogger.LogError(outText);

            Authenticated = false;
            Disconnect();
        }

        ArchipelagoConsole.LogMessage(outText);
        attemptingConnection = false;
    }

    /// <summary>
    /// something went wrong, or we need to properly disconnect from the server. cleanup and re null our session
    /// </summary>
    private void Disconnect()
    {
        HunieCamArchipelago.BepinLogger.LogDebug("disconnecting from server...");
        session?.Socket.Disconnect();
        session = null;
        Authenticated = false;
    }

    public void SendMessage(string message)
    {
        if (message.StartsWith("$"))
        {
            Codes.ProcessCode(message);
        }
        else
        {
            session.Socket.SendPacketAsync(new SayPacket { Text = message });
        }
    }

    public static void sendloc(int loc)
    {
        //ArchipelagoConsole.LogDebug($"SENDING LOCATION ID: {loc}");
        session.Locations.CompleteLocationChecks(loc);
    }

    /// <summary>
    /// we received an item so reward it here
    /// </summary>
    /// <param name="helper">item helper which we can grab our item from</param>
    private void OnItemReceived(ReceivedItemsHelper helper)
    {
        //var receivedItem = helper.DequeueItem();
        //
        //if (helper.Index <= ServerData.Index) return;
        //
        //itemstoprocess.Enqueue(receivedItem);
        //
        //ServerData.Index++;

        // TODO reward the item here
        // if items can be received while in an invalid state for actually handling them, they can be placed in a local
        // queue/collection to be handled later
    }

    /// <summary>
    /// something went wrong with our socket connection
    /// </summary>
    /// <param name="e">thrown exception from our socket</param>
    /// <param name="message">message received from the server</param>
    private void OnSessionErrorReceived(Exception e, string message)
    {
        HunieCamArchipelago.BepinLogger.LogError(e);
        ArchipelagoConsole.LogMessage(message);
    }

    /// <summary>
    /// something went wrong closing our connection. disconnect and clean up
    /// </summary>
    /// <param name="reason"></param>
    private void OnSessionSocketClosed(string reason)
    {
        HunieCamArchipelago.BepinLogger.LogError($"Connection to Archipelago lost: {reason}");
        Disconnect();
    }
}