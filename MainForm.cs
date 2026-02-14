using System.Diagnostics;

namespace DS_Game_Maker
{
    public partial class MainForm
    {

        private bool ShownPro = false;
        private bool NeedsDKP = false;
        private bool CacheHasTinternet = true;

        public Compile compileForm;

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
            foreach (string SettingLine in File.ReadAllLines(SettingsLib.SettingsPath))
            {
                // If SettingLine.Length = 0 Then Continue For
                if (SettingLine.StartsWith(SettingName + " "))
                    DoTheAdd = false;
                FS += SettingLine + Constants.vbCrLf;
            }
            if (DoTheAdd)
            {
                FS += SettingName + " " + SettingValue;
                File.WriteAllText(SettingsLib.SettingsPath, FS);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            compileForm = new Compile();

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
                ref var withBlock = ref ScriptsLib.ApplyFinders;
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
                ref var withBlock1 = ref ScriptsLib.VariableTypes;
                withBlock1.Add("Integer");
                withBlock1.Add("Boolean");
                withBlock1.Add("Float");
                withBlock1.Add("Signed Byte");
                withBlock1.Add("Unsigned Byte");
                withBlock1.Add("String");
            }
           
            // Set Up Action icons
            DSGMlib.ActionBG = (Bitmap)(File.Exists(Constants.AppDirectory + "ActionBG.png") ? DSGMlib.PathToImage(Constants.AppDirectory + "ActionBG.png") : Properties.Resources.ActionBG);
            DSGMlib.ActionConditionalBG = (Bitmap)(File.Exists(Constants.AppDirectory + "ActionConditionalBG.png") ? DSGMlib.PathToImage(Constants.AppDirectory + "ActionConditionalBG.png") : Properties.Resources.ActionConditionalBG);

            foreach (Control ctl in Controls)
            {
                if (ctl is MdiClient)
                {
                    ctl.BackgroundImage = Properties.Resources.MDIBG;
                }
            }

            List<string> VitalFiles =
            [
                Constants.AppDirectory + "Resources/NoSprite.png",
                Constants.AppDirectory + "ActionIcons/Empty.png",
                Constants.AppDirectory + "DefaultResources/Sprite.png",
                Constants.AppDirectory + "DefaultResources/Background.png",
                Constants.AppDirectory + "DefaultResources/Sound.wav",
            ];

            byte VitalBuggered = 0;

            foreach (string X in VitalFiles)
            {
                if (File.Exists(X) == false)
                {
                    VitalBuggered = (byte)(VitalBuggered + 1);
                }
            }
            if (VitalBuggered == 1)
            {
                DSGMlib.MsgError("A vital file is missing. You must reinstall " + Application.ProductName + ".");
                return;
            }
            if (VitalBuggered > 1)
            {
                DSGMlib.MsgError("Some vital files are missing. You must reinstall " + Application.ProductName + ".");
                return;
            }

            DSGMlib.RebuildFontCache();

            // Toolstrip Renderers
            MainToolStrip.Renderer = new clsToolstripRenderer();
            DMainMenuStrip.Renderer = new clsMenuRenderer();
            ResRightClickMenu.Renderer = new clsMenuRenderer();

            // Resources Setup
            DSGMlib.ResourceTypes[0] = "Sprites";
            MainImageList.Images.Add("SpriteIcon", Properties.Resources.SpriteIcon);
            DSGMlib.ResourceTypes[1] = "Objects";
            MainImageList.Images.Add("ObjectIcon", Properties.Resources.ObjectIcon);
            DSGMlib.ResourceTypes[2] = "Backgrounds";
            MainImageList.Images.Add("BackgroundIcon", Properties.Resources.BackgroundIcon);
            DSGMlib.ResourceTypes[3] = "Sounds";
            MainImageList.Images.Add("SoundIcon", Properties.Resources.SoundIcon);
            DSGMlib.ResourceTypes[4] = "Rooms";
            MainImageList.Images.Add("RoomIcon", Properties.Resources.RoomIcon);
            DSGMlib.ResourceTypes[5] = "Paths";
            MainImageList.Images.Add("PathIcon", Properties.Resources.PathIcon);
            DSGMlib.ResourceTypes[6] = "Scripts";
            MainImageList.Images.Add("ScriptIcon", Properties.Resources.ScriptIcon);

            // Imagelist Setup
            // MainImageList.Images.Add("ScriptIcon", My.Resources.ScriptIcon)
            MainImageList.Images.Add("FolderIcon", Properties.Resources.FolderIcon);
            // Resources Setup
            for (byte Resource = 0, loopTo = (byte)(DSGMlib.ResourceTypes.Length - 1); Resource <= loopTo; Resource++)
            {
                ResourcesTreeView.Nodes.Add(string.Empty, DSGMlib.ResourceTypes[(int)Resource], 7, 7);
            }


            // Settings 
            if (File.Exists(Constants.AppDirectory + "data.dat") == false)
            {
                File.Copy(Constants.AppDirectory + "restore.dat", Constants.AppDirectory + "data.dat");
            }


            SettingsLib.SettingsPath = Constants.AppDirectory + "data.dat";
            PatchSetting("USE_EXTERNAL_SCRIPT_EDITOR", "0");
            PatchSetting("RIGHT_CLICK", "1");
            PatchSetting("HIDE_OLD_ACTIONS", "1");
            PatchSetting("SHRINK_ACTIONS_LIST", "0");
            SettingsLib.LoadSettings();
            // Fonts Setup
            foreach (string FontFile in Directory.GetFiles(Constants.AppDirectory + "Fonts"))
            {
                string FontName = FontFile.Substring(FontFile.LastIndexOf("/") + 1);
                FontName = FontName.Substring(0, FontName.IndexOf("."));
                DSGMlib.FontNames.Add(FontName);
            }
            Text = DSGMlib.TitleDataWorks();
        }

        public void GenerateShite(string DisplayResult)
        {
            short DW = Convert.ToInt16(SettingsLib.GetSetting("DEFAULT_ROOM_WIDTH"));
            short DH = Convert.ToInt16(SettingsLib.GetSetting("DEFAULT_ROOM_HEIGHT"));




            // FIX:
            if (DW < 256)
                DW = 256;
            if (DW > 4096)
                DW = 4096;
            if (DW < 192)
                DW = 192;
            if (DH > 4096)
                DH = 4096;




            DSGMlib.CurrentXDS = "ROOM Room_1," + DW.ToString() + "," + DH.ToString() + ",1,," + DW.ToString() + "," + DH.ToString() + ",1," + Constants.vbCrLf;
            DSGMlib.CurrentXDS += "BOOTROOM Room_1" + Constants.vbCrLf;
            DSGMlib.CurrentXDS += "SCORE 0" + Constants.vbCrLf;
            DSGMlib.CurrentXDS += "LIVES 3" + Constants.vbCrLf;
            DSGMlib.CurrentXDS += "HEALTH 100" + Constants.vbCrLf;
            DSGMlib.CurrentXDS += "PROJECTNAME " + DisplayResult + Constants.vbCrLf;
            DSGMlib.CurrentXDS += "TEXT2 " + Constants.vbCrLf;
            DSGMlib.CurrentXDS += "TEXT3 " + Constants.vbCrLf;
            DSGMlib.CurrentXDS += "FAT_CALL 0" + Constants.vbCrLf;
            DSGMlib.CurrentXDS += "NITROFS_CALL 1" + Constants.vbCrLf;
            DSGMlib.CurrentXDS += "MIDPOINT_COLLISIONS 0" + Constants.vbCrLf;
            DSGMlib.CurrentXDS += "INCLUDE_WIFI_LIB 0" + Constants.vbCrLf;
        }

        private void NewProject_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.AppDirectory + ProductName, "/skipauto");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DSGMlib.BeingUsed)
            {
                bool WillExit = false;
                string TheText = "Your new project";

                if (DSGMlib.IsNewProject == false)
                {
                    TheText = "'" + DSGMlib.CacheProjectName + "'";
                }

                DialogResult Result = MessageBox.Show(TheText + " may have unsaved changes." + Constants.vbCrLf + Constants.vbCrLf + "Do you want to save just in case?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (Result == DialogResult.Yes)
                {
                    SaveButton_Click(new object(), new EventArgs());
                    WillExit = true;
                }
                else if (Result == DialogResult.No)
                {
                    e.Cancel = false;
                    WillExit = true;
                }
                else if (Result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                try
                {
                    if (WillExit)
                    {
                        Directory.Delete(SessionsLib.SessionPath, true);
                        Directory.Delete(SessionsLib.CompilePath, true);

                        if (DSGMlib.IsNewProject)
                        {
                            File.Delete(Constants.AppDirectory + "NewProjects/" + SessionsLib.Session + ".dsgm");
                        }
                    }
                }
                catch
                {
                }
            }
        }

        public void InternalSave()
        {
            DSGMlib.CleanInternalXDS();
            SaveButton.Enabled = false;
            SaveButtonTool.Enabled = false;
            File.WriteAllText(SessionsLib.SessionPath + "XDS.xds", DSGMlib.CurrentXDS);
            string MyBAT = "zip.exe a save.zip Sprites Backgrounds Sounds Scripts IncludeFiles NitroFSFiles XDS.xds" + Constants.vbCrLf + "exit";
            DSGMlib.RunBatchString(MyBAT, SessionsLib.SessionPath, true);
            // File.Delete(ProjectPath)
            File.Copy(SessionsLib.SessionPath + "save.zip", DSGMlib.ProjectPath, true);
            File.Delete(SessionsLib.SessionPath + "save.bat");
            File.Delete(SessionsLib.SessionPath + "save.zip");
            SaveButton.Enabled = true;
            SaveButtonTool.Enabled = true;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // If it's a new project, call Save As instead.
            if (DSGMlib.IsNewProject)
            {
                SaveAsButton_Click(new object(), new EventArgs());
                return;
            }
            InternalSave();
            DSGMlib.IsNewProject = false;
        }

        private void AddSpriteButton_Click(object sender, EventArgs e)
        {
            string NewName = DSGMlib.MakeResourceName("Sprite", "SPRITE");
            File.Copy(Constants.AppDirectory + "DefaultResources/Sprite.png", SessionsLib.SessionPath + "Sprites/0_" + NewName + ".png");
            DSGMlib.XDSAddLine("SPRITE " + NewName + ",32,32");
            byte argResourceID = (byte)DSGMlib.ResourceIDs.Sprite;
            DSGMlib.AddResourceNode(ref argResourceID, NewName, "SpriteNode", true);
            foreach (Form X in MdiChildren)
            {
                if (!DSGMlib.IsObject(X.Text))
                    continue;
                ((DObject)X).AddSprite(NewName);
            }
            DSGMlib.RedoSprites = true;
        }

        private void AddObjectButton_Click(object sender, EventArgs e)
        {
            byte ObjectCount = (byte)DSGMlib.GetXDSFilter("OBJECT ").Length;
            string NewName = DSGMlib.MakeResourceName("Object", "OBJECT");
            DSGMlib.XDSAddLine("OBJECT " + NewName + ",None,0");
            byte argResourceID = (byte)DSGMlib.ResourceIDs.DObject;
            DSGMlib.AddResourceNode(ref argResourceID, NewName, "ObjectNode", true);
            foreach (Form X in MdiChildren)
            {
                if (!(X.Name == "Room"))
                    continue;
                ((Room)X).AddObjectToDropper(NewName);
            }
        }

        private void AddBackgroundButton_Click(object sender, EventArgs e)
        {
            string NewName = DSGMlib.MakeResourceName("Background", "BACKGROUND");
            File.Copy(Constants.AppDirectory + "DefaultResources/Background.png", SessionsLib.SessionPath + "Backgrounds/" + NewName + ".png");
            DSGMlib.XDSAddLine("BACKGROUND " + NewName);
            byte argResourceID = (byte)DSGMlib.ResourceIDs.Background;
            DSGMlib.AddResourceNode(ref argResourceID, NewName, "BackgroundNode", true);
            foreach (Form X in MdiChildren)
            {
                if (!DSGMlib.IsRoom(X.Text))
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
            DSGMlib.BGsToRedo.Add(NewName);
        }

        private void AddSoundButton_Click(object sender, EventArgs e)
        {
            string NewName = DSGMlib.MakeResourceName("Sound", "SOUND");
            Program.Forms.soundType_Form.ShowDialog();
            bool SB = Program.Forms.soundType_Form.IsSoundEffect;
            File.Copy(Constants.AppDirectory + "DefaultResources/Sound." + (SB ? "wav" : "mp3"), SessionsLib.SessionPath + "Sounds/" + NewName + "." + (SB ? "wav" : "mp3"));
            DSGMlib.XDSAddLine("SOUND " + NewName + "," + (SB ? "0" : "1"));
            byte argResourceID = (byte)DSGMlib.ResourceIDs.Sound;
            DSGMlib.AddResourceNode(ref argResourceID, NewName, "SoundNode", true);
            DSGMlib.SoundsToRedo.Add(NewName);
        }

        private void AddRoomButton_Click(object sender, EventArgs e)
        {
            byte RoomCount = (byte)DSGMlib.GetXDSFilter("ROOM ").Length;
            string NewName = DSGMlib.MakeResourceName("Room", "ROOM");
            short DW = Convert.ToInt16(SettingsLib.GetSetting("DEFAULT_ROOM_WIDTH"));
            short DH = Convert.ToInt16(SettingsLib.GetSetting("DEFAULT_ROOM_HEIGHT"));
            if (DW < 256)
                DW = 256;
            if (DW > 4096)
                DW = 4096;
            if (DW < 192)
                DW = 192;
            if (DH > 4096)
                DH = 4096;
            DSGMlib.XDSAddLine("ROOM " + NewName + "," + DW.ToString() + "," + DH.ToString() + ",1,," + DW.ToString() + "," + DH.ToString() + ",1,");
            byte argResourceID = (byte)DSGMlib.ResourceIDs.Room;
            DSGMlib.AddResourceNode(ref argResourceID, NewName, "RoomNode", true);
        }

        private void AddPathButton_Click(object sender, EventArgs e)
        {
            string NewName = DSGMlib.MakeResourceName("Path", "PATH");
            DSGMlib.XDSAddLine("PATH " + NewName);
            byte argResourceID = (byte)DSGMlib.ResourceIDs.Path;
            DSGMlib.AddResourceNode(ref argResourceID, NewName, "PathNode", true);
        }

        private void AddScriptButton_Click(object sender, EventArgs e)
        {
            string NewName = DSGMlib.MakeResourceName("Script", "SCRIPT");
            File.CreateText(SessionsLib.SessionPath + "Scripts/" + NewName + ".dbas").Dispose();
            DSGMlib.XDSAddLine("SCRIPT " + NewName + ",1");
            byte argResourceID = (byte)DSGMlib.ResourceIDs.Script;
            DSGMlib.AddResourceNode(ref argResourceID, NewName, "ScriptNode", true);
        }

        public bool OpenWarn()
        {
            string TheText = "'" + DSGMlib.CacheProjectName + "'";

            if (DSGMlib.IsNewProject)
            {
                TheText = "your new Project";
            }

            DialogResult Answer = MessageBox.Show("Are you sure you want to open another Project?" + Constants.vbCrLf + Constants.vbCrLf + "You will lose any changes you have made to " + TheText + ".", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Answer == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OpenProjectButton_Click(object sender, EventArgs e)
        {
            if (DSGMlib.BeingUsed)
            {
                if (!OpenWarn())
                {
                    return;
                }
            }
            string Result = DSGMlib.OpenFile(string.Empty, Application.ProductName + " Projects|*.dsgm");
            if (Result.Length == 0)
                return;
            DSGMlib.OpenProject(Result);
        }

        public void LoadLastProject(bool Automatic)
        {
            // IsNewProject = False
            string LastPath = SettingsLib.GetSetting("LAST_PROJECT");
            if (Automatic)
            {
                if (File.Exists(LastPath))
                {
                    DSGMlib.OpenProject(LastPath);
                }
                return;
            }
            if (DSGMlib.BeingUsed)
            {
                if (LastPath == DSGMlib.ProjectPath)
                {
                    // Same Project - Reload job
                    DialogResult Result = MessageBox.Show("Do you want to reload the current Project?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (Result == DialogResult.Yes)
                    {
                        DSGMlib.CleanFresh(false);
                        DSGMlib.OpenProject(DSGMlib.ProjectPath);

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
                        DSGMlib.OpenProject(LastPath);
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
                DSGMlib.OpenProject(LastPath);
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
            string Directory = DSGMlib.ProjectPath;
            Directory = Directory.Substring(0, Directory.IndexOf("/"));
            string Result = string.Empty;
            if (DSGMlib.IsNewProject)
            {
                Result = DSGMlib.SaveFile(Directory, Application.ProductName + " Projects|*.dsgm", "New Project.dsgm");
            }
            else
            {
                Result = DSGMlib.SaveFile(Directory, Application.ProductName + " Projects|*.dsgm", DSGMlib.CacheProjectName + ".dsgm");
            }
            if (Result.Length == 0)
                return;
            DSGMlib.CleanInternalXDS();
            SaveButton.Enabled = false;
            SaveButtonTool.Enabled = false;
            File.WriteAllText(SessionsLib.SessionPath + "XDS.xds", DSGMlib.CurrentXDS);
            string MyBAT = "zip.exe a save.zip Sprites Backgrounds Sounds Scripts IncludeFiles NitroFSFiles XDS.xds" + Constants.vbCrLf + "exit";
            DSGMlib.RunBatchString(MyBAT, SessionsLib.SessionPath, true);
            // File.Delete(ProjectPath)
            DSGMlib.ProjectPath = Result;
            File.Copy(SessionsLib.SessionPath + "save.zip", DSGMlib.ProjectPath, true);
            File.Delete(SessionsLib.SessionPath + "save.bat");
            File.Delete(SessionsLib.SessionPath + "save.zip");
            SaveButton.Enabled = true;
            SaveButtonTool.Enabled = true;
            DSGMlib.IsNewProject = false;
            Text = DSGMlib.TitleDataWorks();
        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            Program.Forms.options_Form.ShowDialog();
        }

        private void ResourcesTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent is not null)
            {
                DSGMlib.OpenResource(e.Node.Text, (byte)e.Node.Parent.Index, false);
            }
        }

        private void GameSettingsButton_Click(object sender, EventArgs e)
        {
            Program.Forms.gameSettings_Form.ShowDialog();
        }

        private void TestGameButton_Click(object sender, EventArgs e)
        {
            if (!(bool)DSGMlib.CompileWrapper())
                return;

            Program.Forms.main_Form.compileForm.HasDoneIt = false;
            Program.Forms.main_Form.compileForm.ShowDialog();
            if (Program.Forms.main_Form.compileForm.Success)
            {
                DSGMlib.NOGBAShizzle();
            }
            else
            {
                DSGMlib.CompileFail();
            }
        }

        private void CompileGameButton_Click(object sender, EventArgs e)
        {
            if (!(bool)DSGMlib.CompileWrapper())
                return;

            Program.Forms.main_Form.compileForm.HasDoneIt = false;
            Program.Forms.main_Form.compileForm.ShowDialog();
            if (Program.Forms.main_Form.compileForm.Success)
            {
                {
                    var withBlock = Program.Forms.compiled_Form;
                    withBlock.Text = DSGMlib.CacheProjectName;
                    withBlock.MainLabel.Text = DSGMlib.CacheProjectName;
                    withBlock.SubLabel.Text = "Compiled by " + Environment.UserName + " at " + DSGMlib.GetTime();
                    withBlock.ShowDialog();
                }
            }
            else
            {
                DSGMlib.CompileFail();
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
            object argInstance = Program.Forms.actionEditor_Form;
            DSGMlib.ShowInternalForm(ref argInstance);
            Program.Forms.actionEditor_Form = (ActionEditor)argInstance;
        }

        private void VariableManagerButton_Click(object sender, EventArgs e)
        {
            Program.Forms.globalVariables_Form.ShowDialog();
        }

        public void ActuallyCleanUp()
        {
            foreach (string X in Directory.GetDirectories(Constants.AppDirectory))
            {
                if (X == SessionsLib.CompilePath.Substring(0, SessionsLib.CompilePath.Length - 1))
                {
                    continue;
                }

                try
                {
                    if (X.StartsWith(Constants.AppDirectory + "DSGMTemp"))
                        Directory.Delete(X, true);
                }
                catch
                {
                }
            }
            foreach (string X in Directory.GetDirectories(Constants.AppDirectory + "ProjectTemp"))
            {
                if (X == SessionsLib.SessionPath.Substring(0, SessionsLib.SessionPath.Length - 1))
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
            DSGMlib.MsgWarn("This will clean up all unused data created by the sessions system." + Constants.vbCrLf + Constants.vbCrLf + "Ensure you do not have other instances of the application open.");
            ActuallyCleanUp();
            DSGMlib.MsgInfo("The process completed successfully.");
        }

        private void ProjectStatisticsButton_Click(object sender, EventArgs e)
        {
            Program.Forms.statistics_Form.ShowDialog();
        }

        private void OpenCompileTempButton_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", SessionsLib.CompilePath);
        }

        private void OpenProjectTempButton_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", SessionsLib.SessionPath);
        }

        private void WebsiteButton_Click()
        {
            DSGMlib.URL(DSGMlib.Domain);
        }

        private void ForumButton_Click()
        {
            DSGMlib.URL(DSGMlib.Domain + "dsgmforum");
        }

        private void EditInternalXDSButton_Click(object sender, EventArgs e)
        {
            if (MdiChildren.Count() > 0)
            {
                DSGMlib.MsgWarn("You must close all open windows to continue.");
                return;
            }
            var Thing = new EditCode();
            Thing.CodeMode = (byte)DSGMlib.CodeMode.XDS;
            Thing.ImportExport = false;
            Thing.ReturnableCode = DSGMlib.CurrentXDS;
            Thing.StartPosition = FormStartPosition.CenterParent;
            Thing.Text = "Edit Internal XDS";
            Thing.ShowDialog();
            DSGMlib.CurrentXDS = Thing.MainTextBox.Text;
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
            object argInstance = Program.Forms.fontViewer_Form;
            DSGMlib.ShowInternalForm(ref argInstance);
            Program.Forms.fontViewer_Form = (FontViewer)argInstance;
            // FontEditor.ShowDialog()
        }

        private void GlobalArraysButton_Click(object sender, EventArgs e)
        {
            Program.Forms.globalArrays_Form.ShowDialog();
        }

        private void AboutDSGMButton_Click(object sender, EventArgs e)
        {
            Program.Forms.aboutDSG_Form.ShowDialog();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            bool BlankNew = true;
            if (DSGMlib.UpdateVersion > DSGMlib.IDVersion)
            {
                Program.Forms.dUpdate_Form.ShowDialog();
            }
            if (!Directory.Exists(Constants.AppDirectory + "devkitPro"))
            {
                //DSGMlib.MsgInfo("Thank you for installing " + Application.ProductName + "." + Constants.vbCrLf + Constants.vbCrLf + "The toolchain will now be installed to compile your games.");
                //Toolchain.RundevkitProUpdater();

                DSGMlib.MsgInfo("Thank you for installing " + Application.ProductName + "." + Constants.vbCrLf + Constants.vbCrLf + "The ds toolchain should of came pre configured but is missing, the application will now exit.");
                Application.Exit();
            }
            bool SkipAuto = false;
            var Args = new List<string>();
            foreach (string X in Program.Forms.CommandLineArgs)
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
                DSGMlib.MsgWarn("You can only open one Project with " + Application.ProductName + " at once.");
            }
            else if (Args.Count == 1)
            {
                if (File.Exists(Args[0]))
                    DSGMlib.OpenProject(Args[0]);
                BlankNew = false;
            }
            else if (!SkipAuto)
            {
                if ((int)Convert.ToByte(SettingsLib.GetSetting("OPEN_LAST_PROJECT_STARTUP")) == 1)
                {
                    LoadLastProject(true);
                    BlankNew = false;
                }
            }
            if (BlankNew)
            {
                DSGMlib.BeingUsed = true;
                string SessionName = string.Empty;
                for (byte Looper = 0; Looper <= 10; Looper++)
                {
                    SessionName = "NewProject" +  SessionsLib.MakeSessionName();
                    if (!Directory.Exists(Constants.AppDirectory + "ProjectTemp/" + SessionName))
                        break;
                }
                SessionsLib.FormSession(SessionName);
                SessionsLib.FormSessionFS();
                DSGMlib.IsNewProject = true;
                DSGMlib.ProjectPath = Constants.AppDirectory + "NewProjects/" + SessionName + ".dsgm";
                Text = DSGMlib.TitleDataWorks();
                GenerateShite("<New Project>");
                DSGMlib.RedoAllGraphics = true;
                DSGMlib.RedoSprites = true;
                DSGMlib.BGsToRedo.Clear();
                byte argResourceID = (byte)DSGMlib.ResourceIDs.Room;
                DSGMlib.AddResourceNode(ref argResourceID, "Room_1", "RoomNode", false);
                InternalSave();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DSGMlib.DeleteResource(ActiveMdiChild.Text, ActiveMdiChild.Name);
        }

        private void CompilesToNitroFSButton_Click(object sender, EventArgs e)
        {
            DSGMlib.RedoAllGraphics = true;
            DSGMlib.RedoSprites = true;
            DSGMlib.BGsToRedo.Clear();
            string ResourceName = ResourcesTreeView.SelectedNode.Text;
            if (File.Exists(SessionsLib.CompilePath + "nitrofiles/" + ResourceName + ".c"))
            {
                File.Delete(SessionsLib.CompilePath + "nitrofiles/" + ResourceName + ".c");
            }
            if (File.Exists(SessionsLib.CompilePath + "nitrofiles/" + ResourceName + "_Map.bin"))
            {
                File.Delete(SessionsLib.CompilePath + "nitrofiles/" + ResourceName + "_Map.bin");
            }
            if (File.Exists(SessionsLib.CompilePath + "nitrofiles/" + ResourceName + "_Tiles.bin"))
            {
                File.Delete(SessionsLib.CompilePath + "nitrofiles/" + ResourceName + "_Tiles.bin");
            }
            if (File.Exists(SessionsLib.CompilePath + "nitrofiles/" + ResourceName + "_Pal.bin"))
            {
                File.Delete(SessionsLib.CompilePath + "nitrofiles/" + ResourceName + "_Pal.bin");
            }
            string OldLine = DSGMlib.GetXDSLine(ResourcesTreeView.SelectedNode.Parent.Text.ToUpper().Substring(0, ResourcesTreeView.SelectedNode.Parent.Text.Length - 1) + " " + ResourcesTreeView.SelectedNode.Text);
            string NewLine = DSGMlib.GetXDSLine(ResourcesTreeView.SelectedNode.Parent.Text.ToUpper().Substring(0, ResourcesTreeView.SelectedNode.Parent.Text.Length - 1) + " " + ResourcesTreeView.SelectedNode.Text);
            if (ResourcesTreeView.SelectedNode.Parent.Text == "Sprites")
            {
                if (DSGMlib.iGet(NewLine, (byte)3, ",") == "NoNitro")
                {
                    NewLine = NewLine.Replace(",NoNitro", ",Nitro");
                }
                else
                {
                    NewLine = NewLine.Replace(",Nitro", ",NoNitro");
                }
                if (string.IsNullOrEmpty(DSGMlib.iGet(NewLine, (byte)3, ",")))
                {
                    NewLine += ",Nitro";
                }
            }
            if (ResourcesTreeView.SelectedNode.Parent.Text == "Backgrounds")
            {
                if (DSGMlib.iGet(NewLine, (byte)1, ",") == "NoNitro")
                {
                    NewLine = NewLine.Replace(",NoNitro", ",Nitro");
                }
                else
                {
                    NewLine = NewLine.Replace(",Nitro", ",NoNitro");
                }
                if (string.IsNullOrEmpty(DSGMlib.iGet(NewLine, (byte)1, ",")))
                {
                    NewLine += ",Nitro";
                }
            }
            DSGMlib.XDSChangeLine(OldLine, NewLine);
            // MsgBox(ResourcesTreeView.SelectedNode.Parent.Text.ToUpper.Substring(0, ResourcesTreeView.SelectedNode.Parent.Text.Length - 1) + " " + ResourcesTreeView.SelectedNode.Text)
        }

        private void ManualButton_Click(object sender, EventArgs e)
        {
            Program.Forms.helpViewer_Form.ShowDialog();
        }

        private void TutorialsButton_Click(object sender, EventArgs e)
        {
            DSGMlib.URL(DSGMlib.Domain + "dsgmforum/viewforum.php?f=6");
        }

        private void GlobalStructuresButton_Click(object sender, EventArgs e)
        {
            Program.Forms.globalStructures_Form.ShowDialog();
        }

        private void RunDevkitProUpdaterButton(object sender, EventArgs e)
        {
            //Toolchain.RundevkitProUpdater();
            DSGMlib.MsgWarn("The toolchain is now updated manually, please use that method instead.");
        }

        private void EditMenu_DropDownOpening(object sender, EventArgs e)
        {
            GoToLastFoundButton.Enabled = false;
            if (DSGMlib.LastResourceFound.Length > 0)
                GoToLastFoundButton.Enabled = true;
            bool OuiOui = ActiveMdiChild is not null;
            DeleteButton.Enabled = OuiOui;
            DuplicateButton.Enabled = OuiOui;
        }

        private void CompileChangeButton_Click(object sender, EventArgs e)
        {
            switch (((ToolStripMenuItem)sender).Name)
            {
                case "GraphicsChangeButton":
                    {
                        DSGMlib.RedoAllGraphics = true;
                        DSGMlib.RedoSprites = true;
                        DSGMlib.BGsToRedo.Clear();
                        break;
                    }
                case "SoundChangeButton":
                    {
                        DSGMlib.BuildSoundsRedoFromFile();
                        break;
                    }
            }
        }

        private void RunNOGBAButton_Click()
        {
            Process.Start(DSGMlib.FormNOGBAPath() + "/NO$GBA.exe");
        }

        private void ResRightClickMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TreeNode ToWorkFrom;
            do
            {
                try
                {
                    ToWorkFrom = ResourcesTreeView.SelectedNode.Parent;
                    if (ToWorkFrom == null)
                    {
                        break; // Exit the loop if there is no parent
                    }
                    ResourcesTreeView.BeginUpdate();

                    DeleteResourceRightClickButton.Enabled = true;
                    OpenResourceRightClickButton.Enabled = true;
                    DuplicateResourceRightClickButton.Enabled = true;
                    CompilesToNitroFSButton.Enabled = true;
                    CompilesToNitroFSButton.Image = Properties.Resources.DeleteIcon;

                    if (string.IsNullOrEmpty(ToWorkFrom.Text))
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    ToWorkFrom = ResourcesTreeView.SelectedNode;
                    DeleteResourceRightClickButton.Enabled = false;
                    OpenResourceRightClickButton.Enabled = false;
                    DuplicateResourceRightClickButton.Enabled = false;
                    CompilesToNitroFSButton.Enabled = false;
                    CompilesToNitroFSButton.Image = Properties.Resources.DeleteIcon;
                }
            }
            while (false);
            {
                //var withBlock = AddResourceRightClickButton;
                //withBlock.Image = Properties.Resources.PlusIcon;

                // Use null-conditional to safely get the parent or the node itself
                ToWorkFrom = ResourcesTreeView.SelectedNode?.Parent ?? ResourcesTreeView.SelectedNode;

                // Exit early if EVERYTHING is somehow null to prevent the switch crash
                if (ToWorkFrom == null) return;

                var withBlock = AddResourceRightClickButton;
                withBlock.Image = Properties.Resources.PlusIcon;

                // Use the null-conditional operator (?) to safely check length and text
                string nodeText = ToWorkFrom.Text ?? "";
                if (nodeText.Length > 0)
                {

                    switch (ToWorkFrom.Text.Substring(0, ToWorkFrom.Text.Length - 1))
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
            }
            {
                var withBlock1 = CompilesToNitroFSButton;
                if ((ToWorkFrom.Text.Substring(0, ToWorkFrom.Text.Length - 1) != "Sprite") && (ToWorkFrom.Text.Substring(0, ToWorkFrom.Text.Length - 1) != "Background"))
                {
                    withBlock1.Enabled = false;
                    if (ToWorkFrom.Text.Substring(0, ToWorkFrom.Text.Length - 1) != "Sound")
                    {
                        withBlock1.Image = Properties.Resources.DeleteIcon;
                    }
                    else
                    {
                        withBlock1.Image = Properties.Resources.AcceptIcon;
                    }
                }
                else
                {
                    if (ToWorkFrom.Text.Substring(0, ToWorkFrom.Text.Length - 1) == "Sprite")
                    {
                        if (DSGMlib.iGet(DSGMlib.GetXDSLine("SPRITE " + ResourcesTreeView.SelectedNode.Text), (byte)3, ",") == "Nitro")
                        {
                            withBlock1.Image = Properties.Resources.AcceptIcon;
                        }
                    }
                    if (ToWorkFrom.Text.Substring(0, ToWorkFrom.Text.Length - 1) == "Background")
                    {
                        if (DSGMlib.iGet(DSGMlib.GetXDSLine("BACKGROUND " + ResourcesTreeView.SelectedNode.Text), (byte)1, ",") == "Nitro")
                        {
                            withBlock1.Image = Properties.Resources.AcceptIcon;
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
            string Type = ResourcesTreeView.SelectedNode.Parent.Text;
            string TypeObject = Type.Substring(0, ResourcesTreeView.SelectedNode.Parent.Text.Length - 1);

            if (TypeObject == "Object")
            {
                Type = "DObject";
            }

            Type = Type.Replace(" ", string.Empty);

            string temp = ResourcesTreeView.SelectedNode.Text;

            DSGMlib.DeleteResource(temp, Type);
        }

        private void OpenResourceRightClickButton_Click(object sender, EventArgs e)
        {
            if (ResourcesTreeView.SelectedNode.Parent.Index != null)
            {
                DSGMlib.OpenResource(ResourcesTreeView.SelectedNode.Text, (byte)ResourcesTreeView.SelectedNode.Parent.Index, false);
            }
            else
            {
                throw new NullReferenceException("OpenResource-RightClick-Button *CLICK* was null");
            }
        }

        private void AddResourceRightClickButton_Click(object sender, EventArgs e)
        {
            // Take the resource type and strip "Add "(Count 4) from for example Sprite instead of "Add Sprite"
            string TheText = AddResourceRightClickButton.Text.Substring(4);
            switch (TheText)
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
                default:
                    {
                        throw new NotSupportedException("Switch did not have a case for desired object: " + TheText + Constants.vbNewLine + "New feature?");
                    }
            }
        }

        private void DuplicateResourceRightClickButton_Click(object sender, EventArgs e)
        {
            string TheName = ResourcesTreeView.SelectedNode.Text;
            DSGMlib.CopyResource(TheName, DSGMlib.GenerateDuplicateResourceName(TheName), (byte)ResourcesTreeView.SelectedNode.Parent.Index);
        }

        private void FindResourceButton_Click(object sender, EventArgs e)
        {
            string Result = DSGMlib.GetInput("Which resource are you looking for?", DSGMlib.LastResourceFound, (byte)DSGMlib.ValidateLevel.None);
            DSGMlib.FindResource(Result);
        }

        private void GoToLastFoundButton_Click(object sender, EventArgs e)
        {
            DSGMlib.FindResource(DSGMlib.LastResourceFound);
        }

        private void FindInScriptsButton_Click(object sender, EventArgs e)
        {
            string Result = DSGMlib.GetInput("Search term:", DSGMlib.LastScriptTermFound, (byte)DSGMlib.ValidateLevel.None);
            var Shower = new FoundInScripts();
            Shower.Term = Result;
            Shower.Text = "Searching Scripts for '" + Result + "'";
            object argInstance = (object)Shower;
            DSGMlib.ShowInternalForm(ref argInstance);
            Shower = (FoundInScripts)argInstance;
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
                        ResT = (byte)DSGMlib.ResourceIDs.Sprite;
                        break;
                    }
                case "Background":
                    {
                        ResT = (byte)DSGMlib.ResourceIDs.Background;
                        break;
                    }
                case "DObject":
                    {
                        ResT = (byte)DSGMlib.ResourceIDs.DObject;
                        break;
                    }
                case "Room":
                    {
                        ResT = (byte)DSGMlib.ResourceIDs.Room;
                        break;
                    }
                case "Path":
                    {
                        ResT = (byte)DSGMlib.ResourceIDs.Path;
                        break;
                    }
                case "Script":
                    {
                        ResT = (byte)DSGMlib.ResourceIDs.Script;
                        break;
                    }
                case "Sound":
                    {
                        ResT = (byte)DSGMlib.ResourceIDs.Sound;
                        break;
                    }
            }
            DSGMlib.CopyResource(TheName, DSGMlib.GenerateDuplicateResourceName(TheName), ResT);
        }

        private void WebsiteButton_Click(object sender, EventArgs e) => WebsiteButton_Click();
        private void ForumButton_Click(object sender, EventArgs e) => ForumButton_Click();
        private void RunNOGBAButton_Click(object sender, EventArgs e) => RunNOGBAButton_Click();

        // Private Sub InstallPluginButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InstallPluginButton.Click, InstallPluginToolStripMenuItem.Click
        // Dim FilePath As String = OpenFile(String.Empty, "DS Game Maker Plugins|*.dsgmp")
        // If FilePath.Length = 0 Then Exit Sub
        // Dim P As String = AppPath + "PluginInstall/"
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

        // File.Copy(P + "Executable.exe", AppPath + "Plugins/" + PName + ".exe")
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