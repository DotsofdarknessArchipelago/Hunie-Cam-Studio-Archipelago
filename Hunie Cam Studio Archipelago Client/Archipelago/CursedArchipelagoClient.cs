using Archipelago;
using Archipelago.MultiClient.Net.Models;
using Archipelago.MultiClient.Net.Packets;
using HunieCamStudioArchipelagoClient.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HunieCamStudioArchipelagoClient.Archipelago
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void callback(string msg);

    public class CursedArchipelagoClient
    {

        public const int apworldmajor = 0;
        public const int apworldminor = 1;
        public const int apworldbuild = 0;

        public RoomInfoPacket room;
        public DataPackagePacket data;
        public ConnectedPacket connected;
        public NetworkVersion worldver;

        public string seed = "";
        public string error = null;

        public const string APVersion = "0.5.0";
        public const string Game = "Hunie Cam Studio";

        public string url;
        public string username;
        public string password;

        public bool recievedroominfo = false;
        public bool sendroomdatapackage = false;
        public bool recievedroomdatapackage = false;
        public bool processedroomdatapackage = false;
        public bool startprocessedroomdatapackage = false;
        public bool processeddatapackage = false;
        public bool sentconnectedpacket = false;
        public bool recievedconnectedpacket = false;
        public bool fullconnection = false;

        public static bool newconn;

        public static List<NetworkItem> items = new List<NetworkItem>();

        public IntPtr ws;

        public void setup(string h, string u, string p)
        {
            url = h.Trim();
            username = u.Trim();
            password = p;

            if (url != "ws://localhost:38281")
            {
                if (!url.StartsWith("wss://") && !url.StartsWith("ws://"))
                {
                    url = "wss://" + url;
                }

                if (!url.Substring(url.IndexOf("://", StringComparison.Ordinal) + 3).Contains(":"))
                {
                    url = url + ":38281";
                }

                if (url.EndsWith(":"))
                    url += 38281;
            }

            ws = helper.getWS();
            helper.seturlWS(ws, url);
            helper.setcallWS(ws);

        }

        public void connect()
        {
            if (ws == null || url == null || username == null) { return; }
            if (helper.readyWS(ws) == 3) { return; }
            if (!url.StartsWith("ws://") && !url.StartsWith("wss://")) { return; }
            helper.startWS(ws);
        }

        public void sendConnectPacket()
        {
            if (helper.readyWS(ws) != 3) { return; }
            string pack = "{\"cmd\":\"Connect\",\"game\":\"" + Game + "\",\"name\":\"" + username + "\",\"password\":\"" + password + "\",\"uuid\":\"" + Guid.NewGuid().ToString() + "\",\"version\":{\"major\":0,\"minor\":5,\"build\":1,\"class\":\"Version\"},\"tags\":[\"AP\"],\"items_handling\":7,\"slot_data\":true}";
            helper.sendWS(ws, pack);
        }

        public void sendGetPackage()
        {
            if (helper.readyWS(ws) != 3) { return; }
            string games = "";
            for (int i = 0; i < room.games.Count(); i++)
            {
                if (i == 0) { games = "\"" + room.games[i] + "\""; continue; }
                games += ",\"" + room.games[i] + "\"";
            }
            helper.sendWS(ws, "{\"cmd\":\"GetDataPackage\", \"games\":[" + games + "]}");
        }

        public void sendJson(string json)
        {
            helper.sendWS(ws, json);
        }

        public void sendLoc(int loc)
        {
            if (!this.connected.checked_locations.Contains(loc)) { this.connected.checked_locations.Add(loc); }
            helper.sendWS(ws, "{\"cmd\":\"LocationChecks\",\"locations\":[" + loc + "]}");
        }

        public void sendCompletion()
        {
            helper.sendWS(ws, "{\"cmd\":\"StatusUpdate\",\"status\":" + 30 + "}");
        }

        public void sendSay(string msg)
        {
            if (msg.StartsWith("$")) { Codes.ProcessCode(msg); return; }
            helper.sendWS(ws, "{\"cmd\":\"Say\",\"text\":\"" + msg + "\"}");
        }

        public static void msgCallback(string msg)
        {
            if (!msg.StartsWith("{") && !msg.StartsWith("["))
            {
                ArchipelagoConsole.LogError(msg);
                HunieCamArchipelago.curse.error = msg;
                return;
            }

            string cmd = "";
            JObject msgjson = JObject.Parse(msg);
            if (msgjson.ContainsKey("cmd"))
            {
                cmd = (string)msgjson["cmd"];
            }

            ArchipelagoConsole.LogDebug("MESSAGE GOTTEN\n" + msg);
            if (cmd == "RoomInfo")
            {
                HunieCamArchipelago.BepinLogger.LogMessage("RoomInfo PACKET GOTTEN");
                HunieCamArchipelago.BepinLogger.LogMessage(msg);
                HunieCamArchipelago.curse.room = JsonConvert.DeserializeObject<RoomInfoPacket>(msg);
                HunieCamArchipelago.curse.recievedroominfo = true;
            }
            else if (cmd == "ConnectionRefused")
            {
                ArchipelagoConsole.LogMessage("ConnectionRefused PACKET GOTTEN");
                ArchipelagoConsole.LogError(msg);

            }
            else if (cmd == "Connected")
            {
                HunieCamArchipelago.BepinLogger.LogMessage("Connected PACKET GOTTEN");
                HunieCamArchipelago.BepinLogger.LogMessage(msg);
                HunieCamArchipelago.curse.connected = JsonConvert.DeserializeObject<ConnectedPacket>(msg);
                NetworkVersion wv = JsonConvert.DeserializeObject<NetworkVersion>(msgjson["slot_data"]["world_version"].ToString());
                HunieCamArchipelago.curse.worldver = wv;
                if (wv.major != apworldmajor && wv.minor != apworldminor && wv.build != apworldbuild)
                {
                    ArchipelagoConsole.LogError($"APWORLD VERSION ERROR\nEXPECTED: V{apworldmajor}.{apworldminor}.{apworldbuild} GOT V{wv.major}.{wv.minor}.{wv.build}");
                }

                HunieCamArchipelago.curse.recievedconnectedpacket = true;

            }
            else if (cmd == "ReceivedItems")
            {
                //ArchipelagoConsole.LogMessage("ReceivedItems PACKET GOTTEN");
                HunieCamArchipelago.BepinLogger.LogMessage("itempacketmsg:\n" + msg);
                if (!HunieCamArchipelago.curse.fullconnection)
                {
                    HunieCamArchipelago.curse.fullconnection = true;
                    newconn = true;
                }
                ReceivedItemsPacket pack = JsonConvert.DeserializeObject<ReceivedItemsPacket>(msg);
                HunieCamArchipelago.BepinLogger.LogMessage("adding items");
                foreach (NetworkItem item in pack.items)
                {
                    try
                    {
                        items.Add(item);
                    }
                    catch (Exception)
                    {
                        ArchipelagoConsole.LogError($"EXCEPTION RECIEVING ITEM:(id:{item.item}, location:{item.location}, player:{item.player}, flags:{item.flags}) please send details to dev for fixing");
                    }
                }

            }
            else if (cmd == "LocationInfo")
            {
                ArchipelagoConsole.LogMessage("LocationInfo PACKET GOTTEN");
                ArchipelagoConsole.LogMessage(msg);

            }
            else if (cmd == "RoomUpdate")
            {
                //ArchipelagoConsole.LogMessage("RoomUpdate PACKET GOTTEN");
                //ArchipelagoConsole.LogMessage(msg);
                JsonConvert.DeserializeObject<RoomUpdatePacket>(msg).update();

            }
            else if (cmd == "PrintJSON")
            {
                //ArchipelagoConsole.LogMessage("PrintJSON PACKET GOTTEN");
                ArchipelagoConsole.LogMessage(JsonConvert.DeserializeObject<PrintJSONPacket>(msg).print());

            }
            else if (cmd == "DataPackage")
            {
                HunieCamArchipelago.BepinLogger.LogMessage("DataPackage PACKET GOTTEN");
                HunieCamArchipelago.BepinLogger.LogMessage(msg);
                HunieCamArchipelago.curse.data = JsonConvert.DeserializeObject<DataPackagePacket>(msg);
                //ArchipelagoConsole.LogMessage(Plugin.curse.data.data.games.ToString());
                HunieCamArchipelago.curse.recievedroomdatapackage = true;

            }
            else if (cmd == "Bounced")
            {
                ArchipelagoConsole.LogMessage("Bounced PACKET GOTTEN");
                ArchipelagoConsole.LogMessage(msg);

            }
            else if (cmd == "InvalidPacket")
            {
                ArchipelagoConsole.LogMessage("InvalidPacket PACKET GOTTEN");
                ArchipelagoConsole.LogMessage(msg);

            }
            else if (cmd == "Retrieved")
            {
                ArchipelagoConsole.LogMessage("Retrieved PACKET GOTTEN");
                ArchipelagoConsole.LogMessage(msg);

            }
            else if (cmd == "SetReply")
            {
                ArchipelagoConsole.LogMessage("SetReply PACKET GOTTEN");
                ArchipelagoConsole.LogMessage(msg);

            }
            else
            {
                ArchipelagoConsole.LogMessage("---MESSAGE ERROR PRINTING MESSAGE---");
                ArchipelagoConsole.LogMessage($"{msg}");
            }

        }
    }


    public class helper
    {

        [DllImport("/BepInEx/plugins/HunieCamStudioArchipelagoClient/DotsWebSocket.dll")]
        public static extern IntPtr getWS();

        [DllImport("/BepInEx/plugins/HunieCamStudioArchipelagoClient/DotsWebSocket.dll")]
        public static extern void startWS(IntPtr ws);

        [DllImport("/BepInEx/plugins/HunieCamStudioArchipelagoClient/DotsWebSocket.dll")]
        public static extern void seturlWS(IntPtr ws, string url);

        [DllImport("/BepInEx/plugins/HunieCamStudioArchipelagoClient/DotsWebSocket.dll")]
        public static extern void setcallWS(IntPtr ws);

        [DllImport("/BepInEx/plugins/HunieCamStudioArchipelagoClient/DotsWebSocket.dll")]
        public static extern void sendWS(IntPtr ws, string msg);

        [DllImport("/BepInEx/plugins/HunieCamStudioArchipelagoClient/DotsWebSocket.dll")]
        public static extern int readyWS(IntPtr ws);

        [DllImport("/BepInEx/plugins/HunieCamStudioArchipelagoClient/DotsWebSocket.dll")]
        public static extern int dotsV();

        [DllImport("/BepInEx/plugins/HunieCamStudioArchipelagoClient/DotsWebSocket.dll")]
        public static extern bool hasmsg(IntPtr ws);

        [DllImport("/BepInEx/plugins/HunieCamStudioArchipelagoClient/DotsWebSocket.dll")]
        public static extern IntPtr getmsg(IntPtr ws);


    }
}