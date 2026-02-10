using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace DS_Game_Maker
{

    public partial class MainForm
    {

        private bool ShownPro = false;
        private bool NeedsDKP = false;
        private bool CacheHasTinternet = true;

        public MainForm()
        {
            InitializeComponent();
        }

        public byte ScriptArgumentStringToType(string Type)
        {
            if (Type == "Integer")
                return 0;
            if (Type == "Boolean")
                return 1;
            if (Type == "Float")
                return 2;
            if (Type == "Signed Byte")
                return 3;
            if (Type == "Unsigned Byte")
                return 4;
            if (Type == "String")
                return 5;
            return 0;
        }

        public void PatchSetting(string SettingName, string SettingValue)
        {
            bool DoTheAdd = true;
            string FS = string.Empty;
            foreach (string SettingLine in File.ReadAllLines(DS_Game_Maker.SettingsLib.SettingsPath))
            {
                // If SettingLine.Length = 0 Then Continue For
                if (SettingLine.StartsWith(SettingName + " "))
                    DoTheAdd = false;
                FS += SettingLine + Constants.vbCrLf;
            }
            if (DoTheAdd)
            {
                FS += SettingName + " " + SettingValue;
                File.WriteAllText(DS_Game_Maker.SettingsLib.SettingsPath, FS);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // If Not System.IO.Directory.Exists(System.IO.Path.GetTempPath + "DSGameMaker") Then
            // My.Computer.FileSystem.CreateDirectory(System.IO.Path.GetTempPath + "DSGameMaker")
            // End If

            // Load plugins
            // For Each X As String In File.ReadAllLines(AppPath + "pluginList.dat")
            // PluginsToolStripMenuItem.DropDownItems.Add(X, Nothing, New EventHandler(AddressOf RunPlugin))
            // PluginsToolStripMenuItem.DropDownItems.Add(X)
            // PluginsToolStripMenuItem.DropDownItems.Item(PluginsToolStripMenuItem.DropDownItems.Count - 1)
            // Next

            // Initialize Apply Finders
            {
                ref var withBlock = ref DS_Game_Maker.ScriptsLib.ApplyFinders;
                withBlock.Add("[X]");
                withBlock.Add("[Y]");
                withBlock.Add("[VX]");
                withBlock.Add("[VY]");
                withBlock.Add("[OriginalX]");
                withBlock.Add("[OriginalY]");
                withBlock.Add("[Screen]");
                withBlock.Add("[Width]");
                withBlock.Add("[Height]");
            }
            // Initialize Variable Types
            {
                ref var withBlock1 = ref DS_Game_Maker.ScriptsLib.VariableTypes;
                withBlock1.Add("Integer");
                withBlock1.Add("Boolean");
                withBlock1.Add("Float");
                withBlock1.Add("Signed Byte");
                withBlock1.Add("Unsigned Byte");
                withBlock1.Add("String");
            }
            DS_Game_Maker.DSGMlib.AppPath = Application.StartupPath;
            if (DS_Game_Maker.DSGMlib.AppPath.EndsWith(@"\bin\Debug"))
                DS_Game_Maker.DSGMlib.AppPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\" + Application.ProductName;
            DS_Game_Maker.DSGMlib.AppPath += @"\";
            // Set Up Action icons
            DS_Game_Maker.DSGMlib.ActionBG = (Bitmap)(File.Exists(DS_Game_Maker.DSGMlib.AppPath + "ActionBG.png") ? DS_Game_Maker.DSGMlib.PathToImage(DS_Game_Maker.DSGMlib.AppPath + "ActionBG.png") : DS_Game_Maker.My.Resources.Resources.ActionBG);
            DS_Game_Maker.DSGMlib.ActionConditionalBG = (Bitmap)(File.Exists(DS_Game_Maker.DSGMlib.AppPath + "ActionConditionalBG.png") ? DS_Game_Maker.DSGMlib.PathToImage(DS_Game_Maker.DSGMlib.AppPath + "ActionConditionalBG.png") : DS_Game_Maker.My.Resources.Resources.ActionConditionalBG);
            DS_Game_Maker.DSGMlib.CDrive = DS_Game_Maker.DSGMlib.AppPath.Substring(0, 3);
            foreach (Control ctl in Controls)
            {
                if (ctl is MdiClient)
                    ctl.BackgroundImage = DS_Game_Maker.My.Resources.Resources.MDIBG;
            }
            string System32Path = Environment.GetFolderPath(Environment.SpecialFolder.System);
            CacheHasTinternet = DS_Game_Maker.DSGMlib.HasInternetConnection("http://google.com");
            if (!File.Exists(System32Path + @"\SciLexer.dll"))
            {
                File.Copy(DS_Game_Maker.DSGMlib.AppPath + "SciLexer.dll", System32Path + @"\SciLexer.dll");
            }
            if (!File.Exists(System32Path + @"\ScintillaNet.dll"))
            {
                File.Copy(DS_Game_Maker.DSGMlib.AppPath + "ScintillaNet.dll", System32Path + @"\ScintillaNet.dll");
            }
            // Also into Windows... nasty, rare suggested fix
            string WindowsPath = System32Path.Substring(0, System32Path.LastIndexOf(@"\"));
            if (!File.Exists(WindowsPath + @"\SciLexer.dll"))
            {
                File.Copy(DS_Game_Maker.DSGMlib.AppPath + "SciLexer.dll", WindowsPath + @"\SciLexer.dll");
            }
            if (!File.Exists(WindowsPath + @"\ScintillaNet.dll"))
            {
                File.Copy(DS_Game_Maker.DSGMlib.AppPath + "ScintillaNet.dll", WindowsPath + @"\ScintillaNet.dll");
            }
            try
            {
                DS_Game_Maker.RegistryLib.SetFileType(".dsgm", "DSGMFile");
                DS_Game_Maker.RegistryLib.SetFileDescription("DSGMFile", Application.ProductName + " Project");
                DS_Game_Maker.RegistryLib.AddAction("DSGMFile", "open", "Open");
                DS_Game_Maker.RegistryLib.SetExtensionCommandLine("open", "DSGMFile", "\"" + DS_Game_Maker.DSGMlib.AppPath + Application.ProductName + ".exe\" \"%1\"");
                DS_Game_Maker.RegistryLib.SetDefaultIcon("DSGMFile", "\"" + DS_Game_Maker.DSGMlib.AppPath + "Icon.ico\"");
            }
            catch (Exception ex)
            {
                DS_Game_Maker.DSGMlib.MsgWarn("You should run " + Application.ProductName + " as an Administrator." + Constants.vbCrLf + Constants.vbCrLf + "(" + ex.Message + ")");
            }
            var VitalFiles = new Collection();
            VitalFiles.Add(DS_Game_Maker.DSGMlib.AppPath + @"Resources\NoSprite.png");
            VitalFiles.Add(DS_Game_Maker.DSGMlib.AppPath + @"ActionIcons\Empty.png");
            VitalFiles.Add(DS_Game_Maker.DSGMlib.AppPath + @"DefaultResources\Sprite.png");
            VitalFiles.Add(DS_Game_Maker.DSGMlib.AppPath + @"DefaultResources\Background.png");
            VitalFiles.Add(DS_Game_Maker.DSGMlib.AppPath + @"DefaultResources\Sound.wav");
            byte VitalBuggered = 0;
            foreach (string X in VitalFiles)
            {
                if (!File.Exists(X))
                    VitalBuggered = (byte)(VitalBuggered + 1);
            }
            if (VitalBuggered == 1)
            {
                DS_Game_Maker.DSGMlib.MsgError("A vital file is missing. You must reinstall " + Application.ProductName + ".");
                return;
            }
            if (VitalBuggered > 1)
            {
                DS_Game_Maker.DSGMlib.MsgError("Some vital files are missing. You must reinstall " + Application.ProductName + ".");
                return;
            }
            DS_Game_Maker.DSGMlib.RebuildFontCache();
            // Toolstrip Renderers
            MainToolStrip.Renderer = new DS_Game_Maker.clsToolstripRenderer();
            DMainMenuStrip.Renderer = new DS_Game_Maker.clsMenuRenderer();
            ResRightClickMenu.Renderer = new DS_Game_Maker.clsMenuRenderer();
            // Resources Setup
            DS_Game_Maker.DSGMlib.ResourceTypes[0] = "Sprites";
            MainImageList.Images.Add("SpriteIcon", DS_Game_Maker.My.Resources.Resources.SpriteIcon);
            DS_Game_Maker.DSGMlib.ResourceTypes[1] = "Objects";
            MainImageList.Images.Add("ObjectIcon", DS_Game_Maker.My.Resources.Resources.ObjectIcon);
            DS_Game_Maker.DSGMlib.ResourceTypes[2] = "Backgrounds";
            MainImageList.Images.Add("BackgroundIcon", DS_Game_Maker.My.Resources.Resources.BackgroundIcon);
            DS_Game_Maker.DSGMlib.ResourceTypes[3] = "Sounds";
            MainImageList.Images.Add("SoundIcon", DS_Game_Maker.My.Resources.Resources.SoundIcon);
            DS_Game_Maker.DSGMlib.ResourceTypes[4] = "Rooms";
            MainImageList.Images.Add("RoomIcon", DS_Game_Maker.My.Resources.Resources.RoomIcon);
            DS_Game_Maker.DSGMlib.ResourceTypes[5] = "Paths";
            MainImageList.Images.Add("PathIcon", DS_Game_Maker.My.Resources.Resources.PathIcon);
            DS_Game_Maker.DSGMlib.ResourceTypes[6] = "Scripts";
            MainImageList.Images.Add("ScriptIcon", DS_Game_Maker.My.Resources.Resources.ScriptIcon);
            // Imagelist Setup
            // MainImageList.Images.Add("ScriptIcon", My.Resources.ScriptIcon)
            MainImageList.Images.Add("FolderIcon", DS_Game_Maker.My.Resources.Resources.FolderIcon);
            // Resources Setup
            for (byte Resource = 0, loopTo = (byte)(DS_Game_Maker.DSGMlib.ResourceTypes.Length - 1); Resource <= loopTo; Resource++)
                ResourcesTreeView.Nodes.Add(string.Empty, DS_Game_Maker.DSGMlib.ResourceTypes[(int)Resource], 7, 7);
            // Settings
            if (!File.Exists(DS_Game_Maker.DSGMlib.AppPath + "data.dat"))
            {
                File.Copy(DS_Game_Maker.DSGMlib.AppPath + "restore.dat", DS_Game_Maker.DSGMlib.AppPath + "data.dat");
            }
            DS_Game_Maker.SettingsLib.SettingsPath = DS_Game_Maker.DSGMlib.AppPath + "data.dat";
            PatchSetting("USE_EXTERNAL_SCRIPT_EDITOR", "0");
            PatchSetting("RIGHT_CLICK", "1");
            PatchSetting("HIDE_OLD_ACTIONS", "1");
            PatchSetting("SHRINK_ACTIONS_LIST", "0");
            DS_Game_Maker.SettingsLib.LoadSettings();
            // Fonts Setup
            foreach (string FontFile in Directory.GetFiles(DS_Game_Maker.DSGMlib.AppPath + "Fonts"))
            {
                string FontName = FontFile.Substring(FontFile.LastIndexOf(@"\") + 1);
                FontName = FontName.Substring(0, FontName.IndexOf("."));
                DS_Game_Maker.DSGMlib.FontNames.Add(FontName);
            }
            Text = DS_Game_Maker.DSGMlib.TitleDataWorks();
        }

        public void GenerateShite(string DisplayResult)
        {
            short DW = Convert.ToInt16(DS_Game_Maker.SettingsLib.GetSetting("DEFAULT_ROOM_WIDTH"));
            short DH = Convert.ToInt16(DS_Game_Maker.SettingsLib.GetSetting("DEFAULT_ROOM_HEIGHT"));




            // FIX:
            if (DW < 256)
                DW = 256;
            if (DW > 4096)
                DW = 4096;
            if (DW < 192)
                DW = 192;
            if (DH > 4096)
                DH = 4096;




            DS_Game_Maker.DSGMlib.CurrentXDS = "ROOM Room_1," + DW.ToString() + "," + DH.ToString() + ",1,," + DW.ToString() + "," + DH.ToString() + ",1," + Constants.vbCrLf;
            DS_Game_Maker.DSGMlib.CurrentXDS += "BOOTROOM Room_1" + Constants.vbCrLf;
            DS_Game_Maker.DSGMlib.CurrentXDS += "SCORE 0" + Constants.vbCrLf;
            DS_Game_Maker.DSGMlib.CurrentXDS += "LIVES 3" + Constants.vbCrLf;
            DS_Game_Maker.DSGMlib.CurrentXDS += "HEALTH 100" + Constants.vbCrLf;
            DS_Game_Maker.DSGMlib.CurrentXDS += "PROJECTNAME " + DisplayResult + Constants.vbCrLf;
            DS_Game_Maker.DSGMlib.CurrentXDS += "TEXT2 " + Constants.vbCrLf;
            DS_Game_Maker.DSGMlib.CurrentXDS += "TEXT3 " + Constants.vbCrLf;
            DS_Game_Maker.DSGMlib.CurrentXDS += "FAT_CALL 0" + Constants.vbCrLf;
            DS_Game_Maker.DSGMlib.CurrentXDS += "NITROFS_CALL 1" + Constants.vbCrLf;
            DS_Game_Maker.DSGMlib.CurrentXDS += "MIDPOINT_COLLISIONS 0" + Constants.vbCrLf;
            DS_Game_Maker.DSGMlib.CurrentXDS += "INCLUDE_WIFI_LIB 0" + Constants.vbCrLf;
        }

        private void NewProject_Click(object sender, EventArgs e)
        {
            Interaction.Shell(DS_Game_Maker.DSGMlib.AppPath + ProductName + ".exe /skipauto");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DS_Game_Maker.DSGMlib.BeingUsed)
            {
                bool WillExit = false;
                string TheText = "Your new project";
                if (!DS_Game_Maker.DSGMlib.IsNewProject)
                {
                    TheText = "'" + DS_Game_Maker.DSGMlib.CacheProjectName + "'";
                }
                int Result = (int)MessageBox.Show(TheText + " may have unsaved changes." + Constants.vbCrLf + Constants.vbCrLf + "Do you want to save just in case?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (Result == (int)MsgBoxResult.Yes)
                {
                    SaveButton_Click(new object(), new EventArgs());
                    WillExit = true;
                }
                else if (Result == (int)MsgBoxResult.No)
                {
                    e.Cancel = false;
                    WillExit = true;
                }
                else if (Result == (int)MsgBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
                try
                {
                    if (WillExit)
                    {
                        Directory.Delete(DS_Game_Maker.SessionsLib.SessionPath, true);
                        Directory.Delete(DS_Game_Maker.SessionsLib.CompilePath, true);
                        if (DS_Game_Maker.DSGMlib.IsNewProject)
                            File.Delete(DS_Game_Maker.DSGMlib.AppPath + @"NewProjects\" + DS_Game_Maker.SessionsLib.Session + ".dsgm");
                    }
                }
                catch
                {
                }
            }
        }

        public void InternalSave()
        {
            DS_Game_Maker.DSGMlib.CleanInternalXDS();
            SaveButton.Enabled = false;
            SaveButtonTool.Enabled = false;
            File.WriteAllText(DS_Game_Maker.SessionsLib.SessionPath + "XDS.xds", DS_Game_Maker.DSGMlib.CurrentXDS);
            string MyBAT = "zip.exe a save.zip Sprites Backgrounds Sounds Scripts IncludeFiles NitroFSFiles XDS.xds" + Constants.vbCrLf + "exit";
            DS_Game_Maker.DSGMlib.RunBatchString(MyBAT, DS_Game_Maker.SessionsLib.SessionPath, true);
            // File.Delete(ProjectPath)
            File.Copy(DS_Game_Maker.SessionsLib.SessionPath + "save.zip", DS_Game_Maker.DSGMlib.ProjectPath, true);
            File.Delete(DS_Game_Maker.SessionsLib.SessionPath + "save.bat");
            File.Delete(DS_Game_Maker.SessionsLib.SessionPath + "save.zip");
            SaveButton.Enabled = true;
            SaveButtonTool.Enabled = true;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // If it's a new project, call Save As instead.
            if (DS_Game_Maker.DSGMlib.IsNewProject)
            {
                SaveAsButton_Click(new object(), new EventArgs());
                return;
            }
            InternalSave();
            DS_Game_Maker.DSGMlib.IsNewProject = false;
        }

        private void AddSpriteButton_Click(object sender, EventArgs e)
        {
            string NewName = DS_Game_Maker.DSGMlib.MakeResourceName("Sprite", "SPRITE");
            File.Copy(DS_Game_Maker.DSGMlib.AppPath + @"DefaultResources\Sprite.png", DS_Game_Maker.SessionsLib.SessionPath + @"Sprites\0_" + NewName + ".png");
            DS_Game_Maker.DSGMlib.XDSAddLine("SPRITE " + NewName + ",32,32");
            byte argResourceID = (byte)DS_Game_Maker.DSGMlib.ResourceIDs.Sprite;
            DS_Game_Maker.DSGMlib.AddResourceNode(ref argResourceID, NewName, "SpriteNode", true);
            foreach (Form X in MdiChildren)
            {
                if (!DS_Game_Maker.DSGMlib.IsObject(X.Text))
                    continue;
                ((DS_Game_Maker.DObject)X).AddSprite(NewName);
            }
            DS_Game_Maker.DSGMlib.RedoSprites = true;
        }

        private void AddObjectButton_Click(object sender, EventArgs e)
        {
            byte ObjectCount = (byte)DS_Game_Maker.DSGMlib.GetXDSFilter("OBJECT ").Length;
            string NewName = DS_Game_Maker.DSGMlib.MakeResourceName("Object", "OBJECT");
            DS_Game_Maker.DSGMlib.XDSAddLine("OBJECT " + NewName + ",None,0");
            byte argResourceID = (byte)DS_Game_Maker.DSGMlib.ResourceIDs.DObject;
            DS_Game_Maker.DSGMlib.AddResourceNode(ref argResourceID, NewName, "ObjectNode", true);
            foreach (Form X in MdiChildren)
            {
                if (!(X.Name == "Room"))
                    continue;
                ((DS_Game_Maker.Room)X).AddObjectToDropper(NewName);
            }
        }

        private void AddBackgroundButton_Click(object sender, EventArgs e)
        {
            string NewName = DS_Game_Maker.DSGMlib.MakeResourceName("Background", "BACKGROUND");
            File.Copy(DS_Game_Maker.DSGMlib.AppPath + @"DefaultResources\Background.png", DS_Game_Maker.SessionsLib.SessionPath + @"Backgrounds\" + NewName + ".png");
            DS_Game_Maker.DSGMlib.XDSAddLine("BACKGROUND " + NewName);
            byte argResourceID = (byte)DS_Game_Maker.DSGMlib.ResourceIDs.Background;
            DS_Game_Maker.DSGMlib.AddResourceNode(ref argResourceID, NewName, "BackgroundNode", true);
            foreach (Form X in MdiChildren)
            {
                if (!DS_Game_Maker.DSGMlib.IsRoom(X.Text))
                    continue;
                foreach (Control Y in X.Controls)
                {
                    if (!(Y.Name == "ObjectsTabControl"))
                        continue;
                    foreach (Control Z in ((TabControl)Y).TabPages[0].Controls)
                    {
                        if (Z.Name == "TopScreenGroupBox" | Z.Name == "BottomScreenGroupBox")
                        {
                            foreach (Control I in Z.Controls)
                            {
                                if (I.Name.EndsWith("BGDropper"))
                                {
                                    ((ComboBox)I).Items.Add(NewName);
                                }
                            }
                        }
                    }
                }
            }
            DS_Game_Maker.DSGMlib.BGsToRedo.Add(NewName);
        }

        private void AddSoundButton_Click(object sender, EventArgs e)
        {
            string NewName = DS_Game_Maker.DSGMlib.MakeResourceName("Sound", "SOUND");
            DS_Game_Maker.My.MyProject.Forms.SoundType.ShowDialog();
            bool SB = DS_Game_Maker.My.MyProject.Forms.SoundType.IsSoundEffect;
            File.Copy(DS_Game_Maker.DSGMlib.AppPath + @"DefaultResources\Sound." + (SB ? "wav" : "mp3"), DS_Game_Maker.SessionsLib.SessionPath + @"Sounds\" + NewName + "." + (SB ? "wav" : "mp3"));
            DS_Game_Maker.DSGMlib.XDSAddLine("SOUND " + NewName + "," + (SB ? "0" : "1"));
            byte argResourceID = (byte)DS_Game_Maker.DSGMlib.ResourceIDs.Sound;
            DS_Game_Maker.DSGMlib.AddResourceNode(ref argResourceID, NewName, "SoundNode", true);
            DS_Game_Maker.DSGMlib.SoundsToRedo.Add(NewName);
        }

        private void AddRoomButton_Click(object sender, EventArgs e)
        {
            byte RoomCount = (byte)DS_Game_Maker.DSGMlib.GetXDSFilter("ROOM ").Length;
            string NewName = DS_Game_Maker.DSGMlib.MakeResourceName("Room", "ROOM");
            short DW = Convert.ToInt16(DS_Game_Maker.SettingsLib.GetSetting("DEFAULT_ROOM_WIDTH"));
            short DH = Convert.ToInt16(DS_Game_Maker.SettingsLib.GetSetting("DEFAULT_ROOM_HEIGHT"));
            if (DW < 256)
                DW = 256;
            if (DW > 4096)
                DW = 4096;
            if (DW < 192)
                DW = 192;
            if (DH > 4096)
                DH = 4096;
            DS_Game_Maker.DSGMlib.XDSAddLine("ROOM " + NewName + "," + DW.ToString() + "," + DH.ToString() + ",1,," + DW.ToString() + "," + DH.ToString() + ",1,");
            byte argResourceID = (byte)DS_Game_Maker.DSGMlib.ResourceIDs.Room;
            DS_Game_Maker.DSGMlib.AddResourceNode(ref argResourceID, NewName, "RoomNode", true);
        }

        private void AddPathButton_Click(object sender, EventArgs e)
        {
            string NewName = DS_Game_Maker.DSGMlib.MakeResourceName("Path", "PATH");
            DS_Game_Maker.DSGMlib.XDSAddLine("PATH " + NewName);
            byte argResourceID = (byte)DS_Game_Maker.DSGMlib.ResourceIDs.Path;
            DS_Game_Maker.DSGMlib.AddResourceNode(ref argResourceID, NewName, "PathNode", true);
        }

        private void AddScriptButton_Click(object sender, EventArgs e)
        {
            string NewName = DS_Game_Maker.DSGMlib.MakeResourceName("Script", "SCRIPT");
            File.CreateText(DS_Game_Maker.SessionsLib.SessionPath + @"Scripts\" + NewName + ".dbas").Dispose();
            DS_Game_Maker.DSGMlib.XDSAddLine("SCRIPT " + NewName + ",1");
            byte argResourceID = (byte)DS_Game_Maker.DSGMlib.ResourceIDs.Script;
            DS_Game_Maker.DSGMlib.AddResourceNode(ref argResourceID, NewName, "ScriptNode", true);
        }

        public bool OpenWarn()
        {
            string TheText = "'" + DS_Game_Maker.DSGMlib.CacheProjectName + "'";
            if (DS_Game_Maker.DSGMlib.IsNewProject)
                TheText = "your new Project";
            byte Answer = (byte)MessageBox.Show("Are you sure you want to open another Project?" + Constants.vbCrLf + Constants.vbCrLf + "You will lose any changes you have made to " + TheText + ".", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Answer == (int)MsgBoxResult.Yes)
                return true;
            else
                return false;
        }

        private void OpenProjectButton_Click(object sender, EventArgs e)
        {
            if (DS_Game_Maker.DSGMlib.BeingUsed)
            {
                if (!OpenWarn())
                {
                    return;
                }
            }
            string Result = DS_Game_Maker.DSGMlib.OpenFile(string.Empty, Application.ProductName + " Projects|*.dsgm");
            if (Result.Length == 0)
                return;
            DS_Game_Maker.DSGMlib.OpenProject(Result);
        }

        public void LoadLastProject(bool Automatic)
        {
            // IsNewProject = False
            string LastPath = DS_Game_Maker.SettingsLib.GetSetting("LAST_PROJECT");
            if (Automatic)
            {
                if (File.Exists(LastPath))
                {
                    DS_Game_Maker.DSGMlib.OpenProject(LastPath);
                }
                return;
            }
            if (DS_Game_Maker.DSGMlib.BeingUsed)
            {
                if ((LastPath ?? "") == (DS_Game_Maker.DSGMlib.ProjectPath ?? ""))
                {
                    // Same Project - Reload job
                    byte Result = (byte)MessageBox.Show("Do you want to reload the current Project?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (Result == (int)MsgBoxResult.Yes)
                    {
                        DS_Game_Maker.DSGMlib.CleanFresh(false);
                        DS_Game_Maker.DSGMlib.OpenProject(DS_Game_Maker.DSGMlib.ProjectPath);
                        return;
                    }
                }
                else
                {
                    // Loading a different project
                    if (!OpenWarn())
                        return;
                    // Yes load a new file
                    if (File.Exists(LastPath))
                    {
                        DS_Game_Maker.DSGMlib.OpenProject(LastPath);
                        return;
                    }
                    else
                    {
                        OpenProjectButton_Click(new object(), new EventArgs());
                    }
                }
            }
            // Fresh session
            else if (File.Exists(LastPath))
            {
                DS_Game_Maker.DSGMlib.OpenProject(LastPath);
            }
            else
            {
                OpenProjectButton_Click(new object(), new EventArgs());
            }
        }

        private void OpenLastProjectButton_Click(object sender, EventArgs e)
        {
            LoadLastProject(false);
        }

        private void SaveAsButton_Click(object sender, EventArgs e)
        {
            string Directory = DS_Game_Maker.DSGMlib.ProjectPath;
            Directory = Directory.Substring(0, Directory.IndexOf(@"\"));
            string Result = string.Empty;
            if (DS_Game_Maker.DSGMlib.IsNewProject)
            {
                Result = DS_Game_Maker.DSGMlib.SaveFile(Directory, Application.ProductName + " Projects|*.dsgm", "New Project.dsgm");
            }
            else
            {
                Result = DS_Game_Maker.DSGMlib.SaveFile(Directory, Application.ProductName + " Projects|*.dsgm", DS_Game_Maker.DSGMlib.CacheProjectName + ".dsgm");
            }
            if (Result.Length == 0)
                return;
            DS_Game_Maker.DSGMlib.CleanInternalXDS();
            SaveButton.Enabled = false;
            SaveButtonTool.Enabled = false;
            File.WriteAllText(DS_Game_Maker.SessionsLib.SessionPath + "XDS.xds", DS_Game_Maker.DSGMlib.CurrentXDS);
            string MyBAT = "zip.exe a save.zip Sprites Backgrounds Sounds Scripts IncludeFiles NitroFSFiles XDS.xds" + Constants.vbCrLf + "exit";
            DS_Game_Maker.DSGMlib.RunBatchString(MyBAT, DS_Game_Maker.SessionsLib.SessionPath, true);
            // File.Delete(ProjectPath)
            DS_Game_Maker.DSGMlib.ProjectPath = Result;
            File.Copy(DS_Game_Maker.SessionsLib.SessionPath + "save.zip", DS_Game_Maker.DSGMlib.ProjectPath, true);
            File.Delete(DS_Game_Maker.SessionsLib.SessionPath + "save.bat");
            File.Delete(DS_Game_Maker.SessionsLib.SessionPath + "save.zip");
            SaveButton.Enabled = true;
            SaveButtonTool.Enabled = true;
            DS_Game_Maker.DSGMlib.IsNewProject = false;
            Text = DS_Game_Maker.DSGMlib.TitleDataWorks();
        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            DS_Game_Maker.My.MyProject.Forms.Options.ShowDialog();
        }

        private void ResourcesTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent is not null)
            {
                DS_Game_Maker.DSGMlib.OpenResource(e.Node.Text, (byte)e.Node.Parent.Index, false);
            }
        }

        private void GameSettingsButton_Click(object sender, EventArgs e)
        {
            DS_Game_Maker.My.MyProject.Forms.GameSettings.ShowDialog();
        }

        private void TestGameButton_Click(object sender, EventArgs e)
        {
            if (Conversions.ToBoolean(!DS_Game_Maker.DSGMlib.CompileWrapper()))
                return;
            DS_Game_Maker.My.MyProject.Forms.Compile.HasDoneIt = false;
            DS_Game_Maker.My.MyProject.Forms.Compile.ShowDialog();
            if (DS_Game_Maker.My.MyProject.Forms.Compile.Success)
            {
                DS_Game_Maker.DSGMlib.NOGBAShizzle();
            }
            else
            {
                DS_Game_Maker.DSGMlib.CompileFail();
            }
        }

        private void CompileGameButton_Click(object sender, EventArgs e)
        {
            if (Conversions.ToBoolean(!DS_Game_Maker.DSGMlib.CompileWrapper()))
                return;
            DS_Game_Maker.My.MyProject.Forms.Compile.HasDoneIt = false;
            DS_Game_Maker.My.MyProject.Forms.Compile.ShowDialog();
            if (DS_Game_Maker.My.MyProject.Forms.Compile.Success)
            {
                {
                    var withBlock = DS_Game_Maker.My.MyProject.Forms.Compiled;
                    withBlock.Text = DS_Game_Maker.DSGMlib.CacheProjectName;
                    withBlock.MainLabel.Text = DS_Game_Maker.DSGMlib.CacheProjectName;
                    withBlock.SubLabel.Text = Conversions.ToString(Operators.AddObject("Compiled by " + Environment.UserName + " at ", DS_Game_Maker.DSGMlib.GetTime()));
                    withBlock.ShowDialog();
                }
            }
            else
            {
                DS_Game_Maker.DSGMlib.CompileFail();
            }
        }

        private void ActionEditorButton_Click(object sender, EventArgs e)
        {
            foreach (Form X in MdiChildren)
            {
                if (X.Text == "Action Editor")
                {
                    X.Focus();
                    return;
                }
            }
            // Dim ActionForm As New ActionEditor
            object argInstance = (object)DS_Game_Maker.My.MyProject.Forms.ActionEditor;
            DS_Game_Maker.DSGMlib.ShowInternalForm(ref argInstance);
            DS_Game_Maker.My.MyProject.Forms.ActionEditor = (DS_Game_Maker.ActionEditor)argInstance;
        }

        private void VariableManagerButton_Click(object sender, EventArgs e)
        {
            DS_Game_Maker.My.MyProject.Forms.GlobalVariables.ShowDialog();
        }

        public void ActuallyCleanUp()
        {
            foreach (string X in Directory.GetDirectories(DS_Game_Maker.DSGMlib.CDrive))
            {
                if ((X ?? "") == (DS_Game_Maker.SessionsLib.CompilePath.Substring(0, DS_Game_Maker.SessionsLib.CompilePath.Length - 1) ?? ""))
                    continue;
                try
                {
                    if (X.StartsWith(DS_Game_Maker.DSGMlib.CDrive + "DSGMTemp"))
                        Directory.Delete(X, true);
                }
                catch
                {
                }
            }
            foreach (string X in Directory.GetDirectories(DS_Game_Maker.DSGMlib.AppPath + "ProjectTemp"))
            {
                if ((X ?? "") == (DS_Game_Maker.SessionsLib.SessionPath.Substring(0, DS_Game_Maker.SessionsLib.SessionPath.Length - 1) ?? ""))
                    continue;
                try
                {
                    Directory.Delete(X, true);
                }
                catch
                {
                }
            }
            // For Each X As String In Directory.GetFiles(AppPath + "NewProjects")
            // If X.EndsWith(CacheProjectName + ".dsgm") Then Continue For
            // File.Delete(X)
            // Next
        }

        private void CleanUpButton_Click(object sender, EventArgs e)
        {
            // If ProjectPath.Length > 0 Then MsgError("You have a project loaded, so temporary data may not be cleared.") : Exit Sub
            DS_Game_Maker.DSGMlib.MsgWarn("This will clean up all unused data created by the sessions system." + Constants.vbCrLf + Constants.vbCrLf + "Ensure you do not have other instances of the application open.");
            ActuallyCleanUp();
            DS_Game_Maker.DSGMlib.MsgInfo("The process completed successfully.");
        }

        private void ProjectStatisticsButton_Click(object sender, EventArgs e)
        {
            DS_Game_Maker.My.MyProject.Forms.Statistics.ShowDialog();
        }

        private void OpenCompileTempButton_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", DS_Game_Maker.SessionsLib.CompilePath);
        }

        private void OpenProjectTempButton_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", DS_Game_Maker.SessionsLib.SessionPath);
        }

        private void WebsiteButton_Click()
        {
            DS_Game_Maker.DSGMlib.URL(DS_Game_Maker.DSGMlib.Domain);
        }

        private void ForumButton_Click()
        {
            DS_Game_Maker.DSGMlib.URL(DS_Game_Maker.DSGMlib.Domain + "dsgmforum");
        }

        private void EditInternalXDSButton_Click(object sender, EventArgs e)
        {
            if (MdiChildren.Count() > 0)
            {
                DS_Game_Maker.DSGMlib.MsgWarn("You must close all open windows to continue.");
                return;
            }
            var Thing = new DS_Game_Maker.EditCode();
            Thing.CodeMode = (byte)DS_Game_Maker.DSGMlib.CodeMode.XDS;
            Thing.ImportExport = false;
            Thing.ReturnableCode = DS_Game_Maker.DSGMlib.CurrentXDS;
            Thing.StartPosition = FormStartPosition.CenterParent;
            Thing.Text = "Edit Internal XDS";
            Thing.ShowDialog();
            DS_Game_Maker.DSGMlib.CurrentXDS = Thing.MainTextBox.Text;
        }

        private void FontViewerButton_Click(object sender, EventArgs e)
        {
            foreach (Form X in MdiChildren)
            {
                if (X.Text == "Font Viewer")
                {
                    X.Focus();
                    return;
                }
            }
            // Dim ActionForm As New ActionEditor
            object argInstance = (object)DS_Game_Maker.My.MyProject.Forms.FontViewer;
            DS_Game_Maker.DSGMlib.ShowInternalForm(ref argInstance);
            DS_Game_Maker.My.MyProject.Forms.FontViewer = (DS_Game_Maker.FontViewer)argInstance;
            // FontEditor.ShowDialog()
        }

        private void GlobalArraysButton_Click(object sender, EventArgs e)
        {
            DS_Game_Maker.My.MyProject.Forms.GlobalArrays.ShowDialog();
        }

        private void AboutDSGMButton_Click(object sender, EventArgs e)
        {
            DS_Game_Maker.My.MyProject.Forms.AboutDSGM.ShowDialog();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            bool BlankNew = true;
            if (DS_Game_Maker.DSGMlib.UpdateVersion > DS_Game_Maker.DSGMlib.IDVersion)
            {
                DS_Game_Maker.My.MyProject.Forms.DUpdate.ShowDialog();
            }
            if (!Directory.Exists(DS_Game_Maker.DSGMlib.CDrive + "devkitPro"))
            {
                DS_Game_Maker.DSGMlib.MsgInfo("Thank you for installing " + Application.ProductName + "." + Constants.vbCrLf + Constants.vbCrLf + "The toolchain will now be installed to compile your games.");
                DS_Game_Maker.Toolchain.RundevkitProUpdater();
            }
            bool SkipAuto = false;
            var Args = new List<string>();
            foreach (string X in DS_Game_Maker.My.MyProject.Application.CommandLineArgs)
            {
                if (X == "/skipauto")
                {
                    SkipAuto = true;
                    continue;
                }
                Args.Add(X);
            }
            if (Args.Count > 1)
            {
                DS_Game_Maker.DSGMlib.MsgWarn("You can only open one Project with " + Application.ProductName + " at once.");
            }
            else if (Args.Count == 1)
            {
                if (File.Exists(Args[0]))
                    DS_Game_Maker.DSGMlib.OpenProject(Args[0]);
                BlankNew = false;
            }
            else if (!SkipAuto)
            {
                if ((int)Convert.ToByte(DS_Game_Maker.SettingsLib.GetSetting("OPEN_LAST_PROJECT_STARTUP")) == 1)
                {
                    LoadLastProject(true);
                    BlankNew = false;
                }
            }
            if (BlankNew)
            {
                DS_Game_Maker.DSGMlib.BeingUsed = true;
                string SessionName = string.Empty;
                for (byte Looper = 0; Looper <= 10; Looper++)
                {
                    SessionName = Conversions.ToString(Operators.AddObject("NewProject", DS_Game_Maker.SessionsLib.MakeSessionName()));
                    if (!Directory.Exists(DS_Game_Maker.DSGMlib.AppPath + @"ProjectTemp\" + SessionName))
                        break;
                }
                DS_Game_Maker.SessionsLib.FormSession(SessionName);
                DS_Game_Maker.SessionsLib.FormSessionFS();
                DS_Game_Maker.DSGMlib.IsNewProject = true;
                DS_Game_Maker.DSGMlib.ProjectPath = DS_Game_Maker.DSGMlib.AppPath + @"NewProjects\" + SessionName + ".dsgm";
                Text = DS_Game_Maker.DSGMlib.TitleDataWorks();
                GenerateShite("<New Project>");
                DS_Game_Maker.DSGMlib.RedoAllGraphics = true;
                DS_Game_Maker.DSGMlib.RedoSprites = true;
                DS_Game_Maker.DSGMlib.BGsToRedo.Clear();
                byte argResourceID = (byte)DS_Game_Maker.DSGMlib.ResourceIDs.Room;
                DS_Game_Maker.DSGMlib.AddResourceNode(ref argResourceID, "Room_1", "RoomNode", false);
                InternalSave();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DS_Game_Maker.DSGMlib.DeleteResource(ActiveMdiChild.Text, ActiveMdiChild.Name);
        }

        private void CompilesToNitroFSButton_Click(object sender, EventArgs e)
        {
            DS_Game_Maker.DSGMlib.RedoAllGraphics = true;
            DS_Game_Maker.DSGMlib.RedoSprites = true;
            DS_Game_Maker.DSGMlib.BGsToRedo.Clear();
            string ResourceName = ResourcesTreeView.SelectedNode.Text;
            if (File.Exists(DS_Game_Maker.SessionsLib.CompilePath + @"nitrofiles\" + ResourceName + ".c"))
            {
                File.Delete(DS_Game_Maker.SessionsLib.CompilePath + @"nitrofiles\" + ResourceName + ".c");
            }
            if (File.Exists(DS_Game_Maker.SessionsLib.CompilePath + @"nitrofiles\" + ResourceName + "_Map.bin"))
            {
                File.Delete(DS_Game_Maker.SessionsLib.CompilePath + @"nitrofiles\" + ResourceName + "_Map.bin");
            }
            if (File.Exists(DS_Game_Maker.SessionsLib.CompilePath + @"nitrofiles\" + ResourceName + "_Tiles.bin"))
            {
                File.Delete(DS_Game_Maker.SessionsLib.CompilePath + @"nitrofiles\" + ResourceName + "_Tiles.bin");
            }
            if (File.Exists(DS_Game_Maker.SessionsLib.CompilePath + @"nitrofiles\" + ResourceName + "_Pal.bin"))
            {
                File.Delete(DS_Game_Maker.SessionsLib.CompilePath + @"nitrofiles\" + ResourceName + "_Pal.bin");
            }
            string OldLine = DS_Game_Maker.DSGMlib.GetXDSLine(ResourcesTreeView.SelectedNode.Parent.Text.ToUpper().Substring(0, ResourcesTreeView.SelectedNode.Parent.Text.Length - 1) + " " + ResourcesTreeView.SelectedNode.Text);
            string NewLine = DS_Game_Maker.DSGMlib.GetXDSLine(ResourcesTreeView.SelectedNode.Parent.Text.ToUpper().Substring(0, ResourcesTreeView.SelectedNode.Parent.Text.Length - 1) + " " + ResourcesTreeView.SelectedNode.Text);
            if (ResourcesTreeView.SelectedNode.Parent.Text == "Sprites")
            {
                if (DS_Game_Maker.DSGMlib.iGet(NewLine, (byte)3, ",") == "NoNitro")
                {
                    NewLine = NewLine.Replace(",NoNitro", ",Nitro");
                }
                else
                {
                    NewLine = NewLine.Replace(",Nitro", ",NoNitro");
                }
                if (string.IsNullOrEmpty(DS_Game_Maker.DSGMlib.iGet(NewLine, (byte)3, ",")))
                {
                    NewLine += ",Nitro";
                }
            }
            if (ResourcesTreeView.SelectedNode.Parent.Text == "Backgrounds")
            {
                if (DS_Game_Maker.DSGMlib.iGet(NewLine, (byte)1, ",") == "NoNitro")
                {
                    NewLine = NewLine.Replace(",NoNitro", ",Nitro");
                }
                else
                {
                    NewLine = NewLine.Replace(",Nitro", ",NoNitro");
                }
                if (string.IsNullOrEmpty(DS_Game_Maker.DSGMlib.iGet(NewLine, (byte)1, ",")))
                {
                    NewLine += ",Nitro";
                }
            }
            DS_Game_Maker.DSGMlib.XDSChangeLine(OldLine, NewLine);
            // MsgBox(ResourcesTreeView.SelectedNode.Parent.Text.ToUpper.Substring(0, ResourcesTreeView.SelectedNode.Parent.Text.Length - 1) + " " + ResourcesTreeView.SelectedNode.Text)
        }

        private void ManualButton_Click(object sender, EventArgs e)
        {
            DS_Game_Maker.My.MyProject.Forms.HelpViewer.ShowDialog();
        }

        private void TutorialsButton_Click(object sender, EventArgs e)
        {
            DS_Game_Maker.DSGMlib.URL(DS_Game_Maker.DSGMlib.Domain + "dsgmforum/viewforum.php?f=6");
        }

        private void GlobalStructuresButton_Click(object sender, EventArgs e)
        {
            DS_Game_Maker.My.MyProject.Forms.GlobalStructures.ShowDialog();
        }

        private void RunDevkitProUpdaterButton(object sender, EventArgs e)
        {
            DS_Game_Maker.Toolchain.RundevkitProUpdater();
        }

        private void EditMenu_DropDownOpening(object sender, EventArgs e)
        {
            GoToLastFoundButton.Enabled = false;
            if (DS_Game_Maker.DSGMlib.LastResourceFound.Length > 0)
                GoToLastFoundButton.Enabled = true;
            bool OuiOui = ActiveMdiChild is not null;
            DeleteButton.Enabled = OuiOui;
            DuplicateButton.Enabled = OuiOui;
        }

        private void CompileChangeButton_Click(object sender, EventArgs e)
        {
            switch (((ToolStripMenuItem)sender).Name ?? "")
            {
                case "GraphicsChangeButton":
                    {
                        DS_Game_Maker.DSGMlib.RedoAllGraphics = true;
                        DS_Game_Maker.DSGMlib.RedoSprites = true;
                        DS_Game_Maker.DSGMlib.BGsToRedo.Clear();
                        break;
                    }
                case "SoundChangeButton":
                    {
                        DS_Game_Maker.DSGMlib.BuildSoundsRedoFromFile();
                        break;
                    }
            }
        }

        private void RunNOGBAButton_Click()
        {
            Process.Start(DS_Game_Maker.DSGMlib.FormNOGBAPath() + @"\NO$GBA.exe");
        }

        private void ResRightClickMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TreeNode ToWorkFrom;
            do
            {
                try
                {
                    ToWorkFrom = ResourcesTreeView.SelectedNode.Parent;
                    DeleteResourceRightClickButton.Enabled = true;
                    OpenResourceRightClickButton.Enabled = true;
                    DuplicateResourceRightClickButton.Enabled = true;
                    CompilesToNitroFSButton.Enabled = true;
                    CompilesToNitroFSButton.Image = DS_Game_Maker.My.Resources.Resources.DeleteIcon;
                    if (string.IsNullOrEmpty(ToWorkFrom.Text))
                        break;
                }
                catch (Exception ex)
                {
                    ToWorkFrom = ResourcesTreeView.SelectedNode;
                    DeleteResourceRightClickButton.Enabled = false;
                    OpenResourceRightClickButton.Enabled = false;
                    DuplicateResourceRightClickButton.Enabled = false;
                    CompilesToNitroFSButton.Enabled = false;
                    CompilesToNitroFSButton.Image = DS_Game_Maker.My.Resources.Resources.DeleteIcon;
                }
            }
            while (false);
            {
                var withBlock = AddResourceRightClickButton;
                withBlock.Image = DS_Game_Maker.My.Resources.Resources.PlusIcon;
                switch (ToWorkFrom.Text.Substring(0, ToWorkFrom.Text.Length - 1) ?? "")
                {
                    case "Sprite":
                        {
                            withBlock.Image = AddSpriteButton.Image;
                            break;
                        }
                    case "Background":
                        {
                            withBlock.Image = AddBackgroundButton.Image;
                            break;
                        }
                    case "Script":
                        {
                            withBlock.Image = AddScriptButton.Image;
                            break;
                        }
                    case "Room":
                        {
                            withBlock.Image = AddRoomButton.Image;
                            break;
                        }
                    case "Path":
                        {
                            withBlock.Image = AddPathButton.Image;
                            break;
                        }
                    case "Object":
                        {
                            withBlock.Image = AddObjectButton.Image;
                            break;
                        }
                    case "Sound":
                        {
                            withBlock.Image = AddSoundButton.Image;
                            break;
                        }
                }
                withBlock.Text = "Add " + ToWorkFrom.Text.Substring(0, ToWorkFrom.Text.Length - 1);
            }
            {
                var withBlock1 = CompilesToNitroFSButton;
                if (!(ToWorkFrom.Text.Substring(0, ToWorkFrom.Text.Length - 1) == "Sprite") & !(ToWorkFrom.Text.Substring(0, ToWorkFrom.Text.Length - 1) == "Background"))
                {
                    withBlock1.Enabled = false;
                    if (!(ToWorkFrom.Text.Substring(0, ToWorkFrom.Text.Length - 1) == "Sound"))
                    {
                        withBlock1.Image = DS_Game_Maker.My.Resources.Resources.DeleteIcon;
                    }
                    else
                    {
                        withBlock1.Image = DS_Game_Maker.My.Resources.Resources.AcceptIcon;
                    }
                }
                else
                {
                    if (ToWorkFrom.Text.Substring(0, ToWorkFrom.Text.Length - 1) == "Sprite")
                    {
                        if (DS_Game_Maker.DSGMlib.iGet(DS_Game_Maker.DSGMlib.GetXDSLine("SPRITE " + ResourcesTreeView.SelectedNode.Text), (byte)3, ",") == "Nitro")
                        {
                            withBlock1.Image = DS_Game_Maker.My.Resources.Resources.AcceptIcon;
                        }
                    }
                    if (ToWorkFrom.Text.Substring(0, ToWorkFrom.Text.Length - 1) == "Background")
                    {
                        if (DS_Game_Maker.DSGMlib.iGet(DS_Game_Maker.DSGMlib.GetXDSLine("BACKGROUND " + ResourcesTreeView.SelectedNode.Text), (byte)1, ",") == "Nitro")
                        {
                            withBlock1.Image = DS_Game_Maker.My.Resources.Resources.AcceptIcon;
                        }
                    }
                }
            }
        }

        private void ResourcesTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ResourcesTreeView.SelectedNode = e.Node;
        }

        private void DeleteResourceRightClickButton_Click(object sender, EventArgs e)
        {
            string Type = ResourcesTreeView.SelectedNode.Parent.Text.Substring(0, ResourcesTreeView.SelectedNode.Parent.Text.Length - 1);
            if (Type == "Object")
                Type = "DObject";
            Type = Type.Replace(" ", string.Empty);
            DS_Game_Maker.DSGMlib.DeleteResource(ResourcesTreeView.SelectedNode.Text, Type);
        }

        private void OpenResourceRightClickButton_Click(object sender, EventArgs e)
        {
            DS_Game_Maker.DSGMlib.OpenResource(ResourcesTreeView.SelectedNode.Text, (byte)ResourcesTreeView.SelectedNode.Parent.Index, false);
        }

        private void AddResourceRightClickButton_Click(object sender, EventArgs e)
        {
            string TheText = AddResourceRightClickButton.Text.Substring(4);
            switch (TheText ?? "")
            {
                case "Sprite":
                    {
                        AddSpriteButton_Click(new object(), new EventArgs());
                        break;
                    }
                case "Background":
                    {
                        AddBackgroundButton_Click(new object(), new EventArgs());
                        break;
                    }
                case "Object":
                    {
                        AddObjectButton_Click(new object(), new EventArgs());
                        break;
                    }
                case "Sound":
                    {
                        AddSoundButton_Click(new object(), new EventArgs());
                        break;
                    }
                case "Room":
                    {
                        AddRoomButton_Click(new object(), new EventArgs());
                        break;
                    }
                case "Path":
                    {
                        AddPathButton_Click(new object(), new EventArgs());
                        break;
                    }
                case "Script":
                    {
                        AddScriptButton_Click(new object(), new EventArgs());
                        break;
                    }
            }
        }

        private void DuplicateResourceRightClickButton_Click(object sender, EventArgs e)
        {
            string TheName = ResourcesTreeView.SelectedNode.Text;
            DS_Game_Maker.DSGMlib.CopyResource(TheName, DS_Game_Maker.DSGMlib.GenerateDuplicateResourceName(TheName), (byte)ResourcesTreeView.SelectedNode.Parent.Index);
        }

        private void FindResourceButton_Click(object sender, EventArgs e)
        {
            string Result = DS_Game_Maker.DSGMlib.GetInput("Which resource are you looking for?", DS_Game_Maker.DSGMlib.LastResourceFound, (byte)DS_Game_Maker.DSGMlib.ValidateLevel.None);
            DS_Game_Maker.DSGMlib.FindResource(Result);
        }

        private void GoToLastFoundButton_Click(object sender, EventArgs e)
        {
            DS_Game_Maker.DSGMlib.FindResource(DS_Game_Maker.DSGMlib.LastResourceFound);
        }

        private void FindInScriptsButton_Click(object sender, EventArgs e)
        {
            string Result = DS_Game_Maker.DSGMlib.GetInput("Search term:", DS_Game_Maker.DSGMlib.LastScriptTermFound, (byte)DS_Game_Maker.DSGMlib.ValidateLevel.None);
            var Shower = new DS_Game_Maker.FoundInScripts();
            Shower.Term = Result;
            Shower.Text = "Searching Scripts for '" + Result + "'";
            object argInstance = (object)Shower;
            DS_Game_Maker.DSGMlib.ShowInternalForm(ref argInstance);
            Shower = (DS_Game_Maker.FoundInScripts)argInstance;
        }

        private void DuplicateButton_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild is null)
                return;
            string TheName = ActiveMdiChild.Text;
            byte ResT = 0;
            switch (ActiveMdiChild.Name ?? "")
            {
                case "Sprite":
                    {
                        ResT = (byte)DS_Game_Maker.DSGMlib.ResourceIDs.Sprite;
                        break;
                    }
                case "Background":
                    {
                        ResT = (byte)DS_Game_Maker.DSGMlib.ResourceIDs.Background;
                        break;
                    }
                case "DObject":
                    {
                        ResT = (byte)DS_Game_Maker.DSGMlib.ResourceIDs.DObject;
                        break;
                    }
                case "Room":
                    {
                        ResT = (byte)DS_Game_Maker.DSGMlib.ResourceIDs.Room;
                        break;
                    }
                case "Path":
                    {
                        ResT = (byte)DS_Game_Maker.DSGMlib.ResourceIDs.Path;
                        break;
                    }
                case "Script":
                    {
                        ResT = (byte)DS_Game_Maker.DSGMlib.ResourceIDs.Script;
                        break;
                    }
                case "Sound":
                    {
                        ResT = (byte)DS_Game_Maker.DSGMlib.ResourceIDs.Sound;
                        break;
                    }
            }
            DS_Game_Maker.DSGMlib.CopyResource(TheName, DS_Game_Maker.DSGMlib.GenerateDuplicateResourceName(TheName), ResT);
        }

        private void WebsiteButton_Click(object sender, EventArgs e) => WebsiteButton_Click();
        private void ForumButton_Click(object sender, EventArgs e) => ForumButton_Click();
        private void RunNOGBAButton_Click(object sender, EventArgs e) => RunNOGBAButton_Click();

        // Private Sub InstallPluginButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InstallPluginButton.Click, InstallPluginToolStripMenuItem.Click
        // Dim FilePath As String = OpenFile(String.Empty, "DS Game Maker Plugins|*.dsgmp")
        // If FilePath.Length = 0 Then Exit Sub
        // Dim P As String = AppPath + "PluginInstall\"
        // Directory.CreateDirectory(P)
        // File.Copy(FilePath, P + "Plugin.zip")
        // Dim MyBAT As String = "zip.exe x Plugin.zip -y" + vbCrLf + "exit"
        // RunBatchString(MyBAT, P, True)
        // Dim PName As String = String.Empty
        // Dim PAuthor As String = String.Empty
        // Dim PLink As String = String.Empty
        // Dim Files As New List(Of String)
        // Dim MakeLines As New List(Of String)

        // For Each X As String In File.ReadAllLines(P + "data.txt")
        // If X.StartsWith("NAME ") Then PName = X.Substring(5)
        // If X.StartsWith("AUTHOR ") Then PAuthor = X.Substring(7)
        // If X.StartsWith("LINK ") Then PLink = X.Substring(5)
        // Next

        // File.Copy(P + "Executable.exe", AppPath + "Plugins\" + PName + ".exe")
        // File.WriteAllText(AppPath + "pluginList.dat", File.ReadAllText(AppPath + "pluginList.dat") + PName + vbCrLf)
        // My.Computer.FileSystem.DeleteDirectory(P, FileIO.DeleteDirectoryOption.DeleteAllContents)
        // End Sub

        // Private Sub RunPlugin(ByVal sender As Object, ByVal e As System.EventArgs)

        // DirectCast(sender, ToolStripMenuItem).Name


        // Dim Plugins As Integer
        // Dim SelectedPlugin As Integer
        // For Each X As String In File.ReadAllLines(AppPath + "pluginList.dat")
        // Plugins += 1
        // Next
        // If PluginsToolStripMenuItem.DropDownItems.Item(3).Pressed Then
        // MsgBox("fdm")
        // End If
        // MsgBox(PluginsToolStripMenuItem.DropDownItems.Item(3).Text)
        // For LoopVar As Integer = 2 To Plugins + 2
        // If PluginsToolStripMenuItem.DropDownItems.Item(LoopVar).Pressed Then
        // SelectedPlugin = LoopVar
        // End If
        // Next
        // MsgInfo("Running Plugin " + PluginsToolStripMenuItem.DropDownItems.Item(SelectedPlugin).Text)
        // End Sub

    }
}