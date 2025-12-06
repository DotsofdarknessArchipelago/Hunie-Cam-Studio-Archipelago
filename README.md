# Hunie Cam Studio Archipelago

Built using template from https://github.com/alwaysintreble/ArchipelagoBepInExPluginTemplate

A BepInEx plugin for Hunie Cam Studio to connect and talk to a Archipelago server for multiworld randomization games

BACKUP YOUR SAVE FILE BEFORE USING THIS AS I CANT GUARANTEE THAT IT WILL NOT CORRUPT IT
(also is a good idea to back up your saves when modding any game)
- Windows save location: "C:/Users/{YOUR USERNAME}/AppData/LocalLow/HuniePot/HunieCam Studio/"

INSTALLATION INSTRUCTIONS:

CLIENT:
- Have Hunie Cam Studio Installed
- Download Hunie Cam Studio Archipelago plugin (See Releases for latest version)
- Extract and copy the contents of "Hunie Cam Studio Archipelago plugin.zip" to the directory where "HunieCamStudio.exe" is

APWORLD (needed for generating archieplago game):
- Have Archipelago Launcher Installed (latest release found here: https://github.com/ArchipelagoMW/Archipelago/releases/latest )
- Download Hunie Cam Studio APWorld (See Releases for latest version)
- Select "Install APWorld" in launcher and select Hunie Cam Studio APWorld you downloaded
- Restart launcher for APWorld to properly install
- (Optional but Recomended)Select "Generate Template Options" to generate default Hunie Cam Studio YMAL

NOTE if you get a game crash when starting the game make sure that in "{hunie cam studio game directory}/bepinex/config/bepinex.cfg" the 2nd last option is "type = MonoBehaviour" <b><ins>not</ins></b> "type = Application"