using Archipelago;
using Archipelago.MultiClient.Net.Enums;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using HunieCamStudioArchipelagoClient.Archipelago;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

namespace HunieCamStudioArchipelagoClient;

[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
public class HunieCamArchipelago : BaseUnityPlugin
{
    public const string PluginGUID = "com.yourName.projectName";
    public const string PluginName = "Hunie Cam Studio Archipelago Client";
    public const string PluginVersion = "0.2.0";

    public const string ModDisplayInfo = $"{PluginName} v{PluginVersion}";
    public static ManualLogSource BepinLogger;
    //public static ArchipelagoClient ArchipelagoClient;
    public static CursedArchipelagoClient curse;

    public static bool awin = false;
    private static Texture2D SolidBoxTex;
    public static Rect win;


    public static GUIStyle mlabel;
    public static GUIStyle mlabel2;
    public static GUIStyle mlabel3;
    public static GUIStyle mbutton;
    public static GUIStyle mtext;

    public GameData data => Game.Data;
    public Game game => Game.Manager;

    public static Dictionary<string, List<int>> temp;

    public static Texture2D arch = null;


    public string playeruri = "ws://localhost:38281";
    public string playername = "Player1";
    public string playerpass = "";


    public static bool tringtoconnect = false;
    public static bool dll = false;


    private void Awake()
    {
        // Plugin startup logic
        BepinLogger = Logger;
        //ArchipelagoClient = new ArchipelagoClient();
        curse = new CursedArchipelagoClient();
        ArchipelagoConsole.Awake();

        if (arch == null)
        {
            if (File.Exists("BepInEx/plugins/HunieCamStudioArchipelagoClient/arch.png"))
            {
                arch = new Texture2D(2, 2);
                var rawData = System.IO.File.ReadAllBytes("BepInEx/plugins/HunieCamStudioArchipelagoClient/arch.png");
                arch.LoadImage(rawData);
            }
            else
            {
                arch = new Texture2D(2, 2);
                ArchipelagoConsole.LogError("DEV forgot to package the nessary image files in the mod ask him to fix it");
            }
        }

        new Harmony(PluginGUID).PatchAll();

        ArchipelagoConsole.LogDebug("PATCHES DONE");

        if (File.Exists("BepInEx/plugins/HunieCamStudioArchipelagoClient/DotsWebsocket.dll"))
        {
            try
            {
                ArchipelagoConsole.LogMessage("DotsWebsocket.dll version:" + helper.dotsV().ToString());
                if (helper.dotsV() == 3) { dll = true; }
                else { ArchipelagoConsole.LogMessage("DotsWebsocket Not Correct Version\nplease update your client"); }

            }
            catch (Exception e)
            {
                ArchipelagoConsole.LogError("FATAL ERROR: DotsWebSocket.dll not able to be accessed");
                ArchipelagoConsole.LogError("DotsWebSocket.dll exists but errored on client.");
            }
        }
        else
        {
            ArchipelagoConsole.LogError("DEV forgot to package the nessary DLL files in the mod ask him to fix it");
        }

        ArchipelagoConsole.LogMessage($"{ModDisplayInfo} loaded! (F8 To Toggle Console)");

    }


    void Update()
    {
        if (dll)
        {
            if (tringtoconnect && helper.hasmsg(curse.ws))
            {
                CursedArchipelagoClient.msgCallback(Marshal.PtrToStringAnsi(helper.getmsg(curse.ws)));
            }
        }

        if (Input.GetKeyDown(KeyCode.F8))
        {
            ArchipelagoConsole.toggleConsole();
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
        }

    }

    private void OnGUI()
    {
        if (mlabel == null)
        {
            mlabel = new GUIStyle(GUI.skin.label.name);
            mlabel2 = new GUIStyle(GUI.skin.label.name);
            mlabel3 = new GUIStyle(GUI.skin.label.name);
            mbutton = new GUIStyle(GUI.skin.button.name);
            mtext = new GUIStyle(GUI.skin.textField.name);
            mlabel.fontSize = 15;
            mlabel2.fontSize = 18;
            mlabel3.fontSize = 18;
            mtext.fontSize = 20;
            mbutton.fontSize = 20;
            mtext.alignment = TextAnchor.MiddleLeft;
            mlabel.alignment = TextAnchor.MiddleRight;
            mlabel2.alignment = TextAnchor.MiddleCenter;
            mlabel3.alignment = TextAnchor.MiddleCenter;
        }

        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            if (GameObject.Find("Canvas/TitleScreen/TitleScreenMenu/TitleScreenButtonNew") != null)
            {
                GameObject.Find("Canvas/TitleScreen/TitleScreenMenu/TitleScreenButtonNew").SetActive(false);
            }
            if (GameObject.Find("Canvas/TitleScreen/TitleScreenMenu/TitleScreenButtonLoad") != null)
            {
                GameObject.Find("Canvas/TitleScreen/TitleScreenMenu/TitleScreenButtonLoad").SetActive(false);
            }
        }
        else if (SceneManager.GetActiveScene().name == "MainScene")
        {
            awin = false;
            if (GameObject.Find("Canvas/WindowContainer/SettingsWindow/SettingsWindowButtonLoad") != null)
            {
                GameObject.Find("Canvas/WindowContainer/SettingsWindow/SettingsWindowButtonLoad").SetActive(false);
            }
            if (GameObject.Find("Canvas/WindowContainer/SettingsWindow/SettingsWindowButtonNew") != null)
            {
                GameObject.Find("Canvas/WindowContainer/SettingsWindow/SettingsWindowButtonNew").SetActive(false);
            }
        }

        if (awin)
        {
            win = new Rect(Screen.width / 2 - (385 / 2), Screen.height / 2 - (445 / 2), 385, 445);
            DrawSolidBox(win);
            GUI.Window(61, win, archwindow, "");
        }

        ArchipelagoConsole.OnGUI();
    }

    public static void DrawSolidBox(Rect boxRect)
    {
        if (SolidBoxTex == null)
        {
            var windowBackground = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            windowBackground.SetPixel(0, 0, new Color(0, 0, 0));
            windowBackground.Apply();
            SolidBoxTex = windowBackground;
        }

        // It's necessary to make a new GUIStyle here or the texture doesn't show up
        GUI.Box(boxRect, "", new GUIStyle { normal = new GUIStyleState { background = SolidBoxTex } });
    }

    public void archwindow(int id)
    {

        if (curse.fullconnection)
        {
            string state = "";
            float loc = 0;
            float item = 0;
            bool b = true;
            if (!CursedArchipelagoClient.newconn)
            {
                state = "GAME STARTED";
                if (Game.Persistence.saveData.saveFiles[0].complete && !Game.Persistence.saveData.saveFiles[0].success)
                {
                    state = "NEW GAME";
                    b = false;
                }
            }
            else
            {
                state = "NEW GAME";
            }

            if (curse.connected.checked_locations.Count() > 0)
            {
                //loc = ArchipelagoClient.session.Locations.AllLocationsChecked.Count() / ArchipelagoClient.totalloc * 100;
                loc = curse.connected.checked_locations.Count();
            }
            else
            {
                loc = 0;
            }
            if (CursedArchipelagoClient.items.Count > 0)
            {
                //item = ArchipelagoClient.session.Items.AllItemsReceived.Count / ArchipelagoClient.totalitem * 100;
                item = CursedArchipelagoClient.items.Count;
            }
            else
            {
                item = 0;
            }

            //((ArchipelagoClient.session.Locations.AllLocationsChecked.Count()/ ArchipelagoClient.totalloc)*100)
            GUI.Label(new Rect(10, 30, 375, 40), $"CLIENT STATE: {state}", mlabel3);
            GUI.Label(new Rect(10, 60, 375, 40), $"APWORLD V{curse.worldver.major}.{curse.worldver.minor}.{curse.worldver.build}", mlabel3);
            GUI.Label(new Rect(10, 100, 375, 40), $"LOCATIONS CHECKED:", mlabel3);
            GUI.Label(new Rect(10, 130, 375, 40), $"{loc:G4}", mlabel3);
            GUI.Label(new Rect(10, 200, 375, 40), $"ITEMS RECIEVED:", mlabel3);
            GUI.Label(new Rect(10, 230, 375, 40), $"{item:G4}", mlabel3);

            if (!CursedArchipelagoClient.newconn && b)
            {
                if (GUI.Button(new Rect(10, 300, 150, 60), "Start Again", mbutton))
                {
                    startarch(true);
                }
                if (GUI.Button(new Rect(225, 300, 150, 60), "Continue", mbutton))
                {
                    startarch(false);
                }
            }
            else
            {
                if (GUI.Button(new Rect(118, 300, 150, 60), "Start Game", mbutton))
                {
                    CursedArchipelagoClient.newconn = false;
                    startarch(true);
                }
            }
        }
        else if (tringtoconnect)
        {
            GUI.Label(new Rect(20, 60, 300, 20), "-connecting to:" + curse.url);
            if (helper.readyWS(curse.ws) == 3)
            {
                GUI.Label(new Rect(20, 80, 300, 20), "-initial server connection established");
            }
            if (curse.recievedroominfo)
            {
                GUI.Label(new Rect(20, 100, 300, 20), "-sending archipelago GetDataPackages packet");
                if (!curse.sendroomdatapackage)
                {
                    curse.sendGetPackage();
                    curse.sendroomdatapackage = true;
                }
            }
            if (curse.recievedroomdatapackage)
            {
                GUI.Label(new Rect(20, 120, 300, 20), "-recieved archipelago GetDataPackages packet");
                if (!curse.startprocessedroomdatapackage && !curse.processeddatapackage)
                {
                    curse.data.data.setup();
                    curse.processeddatapackage = true;
                }
            }
            if (curse.processeddatapackage)
            {
                GUI.Label(new Rect(20, 140, 300, 20), "-processed archipelago GetDataPackages");
                if (!curse.startprocessedroomdatapackage)
                {
                    GUI.Label(new Rect(20, 160, 300, 20), "-sending archipelago Connect Packet");
                    if (!curse.sentconnectedpacket)
                    {
                        curse.sendConnectPacket();
                        curse.sentconnectedpacket = true;
                    }
                }
            }
            if (curse.recievedconnectedpacket)
            {
                GUI.Label(new Rect(20, 180, 300, 20), "-connection to archipelago server established");
                GUI.Label(new Rect(20, 200, 300, 20), "-waiting on geting a packet to know if connection");
                GUI.Label(new Rect(20, 220, 300, 20), "is fully working");
            }
            if (helper.readyWS(curse.ws) == 2)
            {
                if (GUI.Button(new Rect(win.width / 2 - 50, 280, 100, 20), "RESET")) { tringtoconnect = false; }
            }
        }
        else
        {
            GUI.Label(new Rect(0, 20, 385, 60), "Client V(" + PluginVersion + "): Status: NOT Connected", mlabel2);
            GUI.Label(new Rect(5, 120, 95, 40), "Host: ", mlabel);
            GUI.Label(new Rect(5, 160, 95, 40), "Player Name: ", mlabel);
            GUI.Label(new Rect(5, 200, 95, 40), "Password: ", mlabel);

            playeruri = GUI.TextField(new Rect(100, 120, 270, 40), playeruri, mtext);
            playername = GUI.TextField(new Rect(100, 160, 270, 40), playername, mtext);
            playerpass = GUI.TextField(new Rect(100, 200, 270, 40), playerpass, mtext);


            if (GUI.Button(new Rect(win.width / 2 - 50, 280, 100, 40), "Connect", mbutton) &&
                !playeruri.IsNullOrWhiteSpace())
            {
                if (dll)
                {
                    tringtoconnect = true;
                    curse.setup(playeruri, playername, playerpass);
                    curse.connect();
                }
            }

        }
    }

    public static void startarch(bool newgame)
    {
        awin = false;
        if (newgame)
        {
            Game.Persistence.saveData.saveFiles[0].ResetFile();
            ArchipelagoData.Index = 0;
        }
        Game.Persistence.activeSaveFileIndex = 0;
        UiLoadingOverlay component = GameObject.Find("LoadingOverlay").GetComponent<UiLoadingOverlay>();
        component.LoadNextScene(true);
    }
}