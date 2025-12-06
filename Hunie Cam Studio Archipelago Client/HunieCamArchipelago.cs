using Archipelago;
using Archipelago.MultiClient.Net.Enums;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using HunieCamStudioArchipelagoClient.Archipelago;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HunieCamStudioArchipelagoClient;

[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
public class HunieCamArchipelago : BaseUnityPlugin
{
    public const string PluginGUID = "com.yourName.projectName";
    public const string PluginName = "Hunie Cam Studio Archipelago Client";
    public const string PluginVersion = "0.1.0";

    public const string ModDisplayInfo = $"{PluginName} v{PluginVersion}";
    private const string APDisplayInfo = $"Archipelago v{ArchipelagoClient.APVersion}";
    public static ManualLogSource BepinLogger;
    public static ArchipelagoClient ArchipelagoClient;

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


    private void Awake()
    {
        // Plugin startup logic
        BepinLogger = Logger;
        ArchipelagoClient = new ArchipelagoClient();
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
                ArchipelagoConsole.LogError("DEV forgot to package the nessary files in the mod ask him to fix it");
            }
        }

        new Harmony(PluginGUID).PatchAll();

        ArchipelagoConsole.LogMessage($"{ModDisplayInfo} loaded! (F8 To Toggle Console)");
    }


    void Update()
    {
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

        ArchipelagoConsole.OnGUI();

        if (awin)
        {
            win = new Rect(Screen.width / 2 - (385 / 2), Screen.height / 2 - (445 / 2), 385, 445);
            DrawSolidBox(win);
            GUI.Window(61, win, archwindow, "");
        }
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

        if (ArchipelagoClient.Authenticated)
        {
            string state = "";
            float loc = 0;
            float item = 0;
            bool b = true;
            if (ArchipelagoClient.slotstate)
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

            if (ArchipelagoClient.session.Locations.AllLocationsChecked.Count() > 0)
            {
                //loc = ArchipelagoClient.session.Locations.AllLocationsChecked.Count() / ArchipelagoClient.totalloc * 100;
                loc = ArchipelagoClient.session.Locations.AllLocationsChecked.Count();
            }
            else
            {
                loc = 0;
            }
            if (ArchipelagoClient.session.Items.AllItemsReceived.Count > 0)
            {
                //item = ArchipelagoClient.session.Items.AllItemsReceived.Count / ArchipelagoClient.totalitem * 100;
                item = ArchipelagoClient.session.Items.AllItemsReceived.Count;
            }
            else
            {
                item = 0;
            }

            //((ArchipelagoClient.session.Locations.AllLocationsChecked.Count()/ ArchipelagoClient.totalloc)*100)
            GUI.Label(new Rect(10, 30, 375, 40), $"CLIENT STATE: {state}", mlabel3);
            GUI.Label(new Rect(10, 60, 375, 40), $"APWORLD V{ArchipelagoData.worldversion["major"]}.{ArchipelagoData.worldversion["minor"]}.{ArchipelagoData.worldversion["build"]}", mlabel3);
            GUI.Label(new Rect(10, 100, 375, 40), $"LOCATIONS CHECKED:", mlabel3);
            GUI.Label(new Rect(10, 130, 375, 40), $"{loc:G4}", mlabel3);
            GUI.Label(new Rect(10, 200, 375, 40), $"ITEMS RECIEVED:", mlabel3);
            GUI.Label(new Rect(10, 230, 375, 40), $"{item:G4}", mlabel3);

            if (ArchipelagoClient.slotstate && b)
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
                    startarch(true);
                }
            }
        }
        else
        {
            GUI.Label(new Rect(0, 20, 385, 60), "Client V(" + PluginVersion + "): Status: NOT Connected", mlabel2);
            GUI.Label(new Rect(5, 120, 95, 40), "Host: ",mlabel);
            GUI.Label(new Rect(5, 160, 95, 40), "Player Name: ", mlabel);
            GUI.Label(new Rect(5, 200, 95, 40), "Password: ", mlabel);

            ArchipelagoClient.ServerData.Uri = GUI.TextField(new Rect(100, 120, 270, 40), ArchipelagoClient.ServerData.Uri, mtext);
            ArchipelagoClient.ServerData.SlotName = GUI.TextField(new Rect(100, 160, 270, 40), ArchipelagoClient.ServerData.SlotName, mtext);
            ArchipelagoClient.ServerData.Password = GUI.TextField(new Rect(100, 200, 270, 40), ArchipelagoClient.ServerData.Password, mtext);

            if (GUI.Button(new Rect( win.width/2-50, 280, 100, 40), "Connect",mbutton) &&
                !ArchipelagoClient.ServerData.SlotName.IsNullOrWhiteSpace())
            {
                ArchipelagoClient.Connect();
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
            ArchipelagoClient.session.DataStorage[Scope.Slot, "state"] = true;
        }
        Game.Persistence.activeSaveFileIndex = 0;
        UiLoadingOverlay component = GameObject.Find("LoadingOverlay").GetComponent<UiLoadingOverlay>();
        component.LoadNextScene(true);
    }
}