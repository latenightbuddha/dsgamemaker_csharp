using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class MainForm : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components is not null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            DMainMenuStrip = new MenuStrip();
            FileMenu = new ToolStripMenuItem();
            NewProjectButton = new ToolStripMenuItem();
            NewProjectButton.Click += new EventHandler(NewProject_Click);
            OpenProjectButton = new ToolStripMenuItem();
            OpenProjectButton.Click += new EventHandler(OpenProjectButton_Click);
            OpenLastProjectButton = new ToolStripMenuItem();
            OpenLastProjectButton.Click += new EventHandler(OpenLastProjectButton_Click);
            MenuSep1 = new ToolStripSeparator();
            SaveButton = new ToolStripMenuItem();
            SaveButton.Click += new EventHandler(SaveButton_Click);
            SaveAsButton = new ToolStripMenuItem();
            SaveAsButton.Click += new EventHandler(SaveAsButton_Click);
            MenuSep2 = new ToolStripSeparator();
            TestGameButton = new ToolStripMenuItem();
            TestGameButton.Click += new EventHandler(TestGameButton_Click);
            CompileGameButton = new ToolStripMenuItem();
            CompileGameButton.Click += new EventHandler(CompileGameButton_Click);
            EditMenu = new ToolStripMenuItem();
            EditMenu.DropDownOpening += new EventHandler(EditMenu_DropDownOpening);
            DuplicateButton = new ToolStripMenuItem();
            DuplicateButton.Click += new EventHandler(DuplicateButton_Click);
            DeleteButton = new ToolStripMenuItem();
            DeleteButton.Click += new EventHandler(DeleteButton_Click);
            MenuSep4 = new ToolStripSeparator();
            FindInScriptsButton = new ToolStripMenuItem();
            FindInScriptsButton.Click += new EventHandler(FindInScriptsButton_Click);
            MenuSep41 = new ToolStripSeparator();
            FindResourceButton = new ToolStripMenuItem();
            FindResourceButton.Click += new EventHandler(FindResourceButton_Click);
            GoToLastFoundButton = new ToolStripMenuItem();
            GoToLastFoundButton.Click += new EventHandler(GoToLastFoundButton_Click);
            ResourcesMenu = new ToolStripMenuItem();
            AddSpriteButton = new ToolStripMenuItem();
            AddSpriteButton.Click += new EventHandler(AddSpriteButton_Click);
            AddObjectButton = new ToolStripMenuItem();
            AddObjectButton.Click += new EventHandler(AddObjectButton_Click);
            AddBackgroundButton = new ToolStripMenuItem();
            AddBackgroundButton.Click += new EventHandler(AddBackgroundButton_Click);
            AddSoundButton = new ToolStripMenuItem();
            AddSoundButton.Click += new EventHandler(AddSoundButton_Click);
            AddRoomButton = new ToolStripMenuItem();
            AddRoomButton.Click += new EventHandler(AddRoomButton_Click);
            MenuSep5 = new ToolStripSeparator();
            AddPathButton = new ToolStripMenuItem();
            AddPathButton.Click += new EventHandler(AddPathButton_Click);
            AddScriptButton = new ToolStripMenuItem();
            AddScriptButton.Click += new EventHandler(AddScriptButton_Click);
            ToolsMenu = new ToolStripMenuItem();
            GameSettingsButton = new ToolStripMenuItem();
            GameSettingsButton.Click += new EventHandler(GameSettingsButton_Click);
            GlobalVariablesButton = new ToolStripMenuItem();
            GlobalVariablesButton.Click += new EventHandler(VariableManagerButton_Click);
            GlobalArraysButton = new ToolStripMenuItem();
            GlobalArraysButton.Click += new EventHandler(GlobalArraysButton_Click);
            GlobalStructuresButton = new ToolStripMenuItem();
            GlobalStructuresButton.Click += new EventHandler(GlobalStructuresButton_Click);
            OptionsButton = new ToolStripMenuItem();
            OptionsButton.Click += new EventHandler(OptionsButton_Click);
            MenuSep7 = new ToolStripSeparator();
            ActionEditorButton = new ToolStripMenuItem();
            ActionEditorButton.Click += new EventHandler(ActionEditorButton_Click);
            FontViewerButton = new ToolStripMenuItem();
            FontViewerButton.Click += new EventHandler(FontViewerButton_Click);
            MenuSep8 = new ToolStripSeparator();
            RunNOGBAButton = new ToolStripMenuItem();
            RunNOGBAButton.Click += new EventHandler(RunNOGBAButton_Click);
            AdvancedButton = new ToolStripMenuItem();
            CleanUpButton = new ToolStripMenuItem();
            CleanUpButton.Click += new EventHandler(CleanUpButton_Click);
            EditInternalXDSButton = new ToolStripMenuItem();
            EditInternalXDSButton.Click += new EventHandler(EditInternalXDSButton_Click);
            MenuSep9 = new ToolStripSeparator();
            OpenCompileTempButton = new ToolStripMenuItem();
            OpenCompileTempButton.Click += new EventHandler(OpenCompileTempButton_Click);
            OpenProjectTempButton = new ToolStripMenuItem();
            OpenProjectTempButton.Click += new EventHandler(OpenProjectTempButton_Click);
            MenuSep10 = new ToolStripSeparator();
            GraphicsChangeButton = new ToolStripMenuItem();
            GraphicsChangeButton.Click += new EventHandler(CompileChangeButton_Click);
            SoundChangeButton = new ToolStripMenuItem();
            SoundChangeButton.Click += new EventHandler(CompileChangeButton_Click);
            WindowMenu = new ToolStripMenuItem();
            HelpMenu = new ToolStripMenuItem();
            HelpContentsButton = new ToolStripMenuItem();
            HelpContentsButton.Click += new EventHandler(ManualButton_Click);
            OnlineTutorialsButton = new ToolStripMenuItem();
            OnlineTutorialsButton.Click += new EventHandler(TutorialsButton_Click);
            MenuSep11 = new ToolStripSeparator();
            NewsButton = new ToolStripMenuItem();
            WebsiteButton = new ToolStripMenuItem();
            WebsiteButton.Click += new EventHandler(WebsiteButton_Click);
            ForumButton = new ToolStripMenuItem();
            ForumButton.Click += new EventHandler(ForumButton_Click);
            MenuSep13 = new ToolStripSeparator();
            ReinstallToolchainButton = new ToolStripMenuItem();
            ReinstallToolchainButton.Click += new EventHandler(RunDevkitProUpdaterButton);
            AboutDSGMButton = new ToolStripMenuItem();
            AboutDSGMButton.Click += new EventHandler(AboutDSGMButton_Click);
            MainToolStrip = new ToolStrip();
            NewProjectButtonTool = new ToolStripButton();
            NewProjectButtonTool.Click += new EventHandler(NewProject_Click);
            OpenProjectButtonTool = new ToolStripButton();
            OpenProjectButtonTool.Click += new EventHandler(OpenProjectButton_Click);
            OpenLastProjectButtonTool = new ToolStripButton();
            OpenLastProjectButtonTool.Click += new EventHandler(OpenLastProjectButton_Click);
            SaveButtonTool = new ToolStripButton();
            SaveButtonTool.Click += new EventHandler(SaveButton_Click);
            SaveAsButtonTool = new ToolStripButton();
            SaveAsButtonTool.Click += new EventHandler(SaveAsButton_Click);
            ToolSep1 = new ToolStripSeparator();
            TestGameButtonTool = new ToolStripButton();
            TestGameButtonTool.Click += new EventHandler(TestGameButton_Click);
            CompileGameButtonTool = new ToolStripButton();
            CompileGameButtonTool.Click += new EventHandler(CompileGameButton_Click);
            OptionsButtonTool = new ToolStripButton();
            OptionsButtonTool.Click += new EventHandler(OptionsButton_Click);
            ToolSep3 = new ToolStripSeparator();
            AddSpriteButtonTool = new ToolStripButton();
            AddSpriteButtonTool.Click += new EventHandler(AddSpriteButton_Click);
            AddObjectButtonTool = new ToolStripButton();
            AddObjectButtonTool.Click += new EventHandler(AddObjectButton_Click);
            AddBackgroundButtonTool = new ToolStripButton();
            AddBackgroundButtonTool.Click += new EventHandler(AddBackgroundButton_Click);
            AddSoundButtonTool = new ToolStripButton();
            AddSoundButtonTool.Click += new EventHandler(AddSoundButton_Click);
            AddRoomButtonTool = new ToolStripButton();
            AddRoomButtonTool.Click += new EventHandler(AddRoomButton_Click);
            AddPathButtonTool = new ToolStripButton();
            AddPathButtonTool.Click += new EventHandler(AddPathButton_Click);
            AddScriptButtonTool = new ToolStripButton();
            AddScriptButtonTool.Click += new EventHandler(AddScriptButton_Click);
            ToolSep4 = new ToolStripSeparator();
            GameSettingsButtonTool = new ToolStripButton();
            GameSettingsButtonTool.Click += new EventHandler(GameSettingsButton_Click);
            GlobalVariablesButtonTool = new ToolStripButton();
            GlobalVariablesButtonTool.Click += new EventHandler(VariableManagerButton_Click);
            GlobalArraysButtonTool = new ToolStripButton();
            GlobalArraysButtonTool.Click += new EventHandler(GlobalArraysButton_Click);
            GlobalStructuresButtonTool = new ToolStripButton();
            GlobalStructuresButtonTool.Click += new EventHandler(GlobalStructuresButton_Click);
            MainImageList = new ImageList(components);
            ResourcesTreeView = new TreeView();
            ResourcesTreeView.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(ResourcesTreeView_NodeMouseDoubleClick);
            ResourcesTreeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(ResourcesTreeView_NodeMouseClick);
            ResRightClickMenu = new ContextMenuStrip(components);
            ResRightClickMenu.Opening += new System.ComponentModel.CancelEventHandler(ResRightClickMenu_Opening);
            OpenResourceRightClickButton = new ToolStripMenuItem();
            OpenResourceRightClickButton.Click += new EventHandler(OpenResourceRightClickButton_Click);
            RightClickSeparator = new ToolStripSeparator();
            AddResourceRightClickButton = new ToolStripMenuItem();
            AddResourceRightClickButton.Click += new EventHandler(AddResourceRightClickButton_Click);
            DuplicateResourceRightClickButton = new ToolStripMenuItem();
            DuplicateResourceRightClickButton.Click += new EventHandler(DuplicateResourceRightClickButton_Click);
            DeleteResourceRightClickButton = new ToolStripMenuItem();
            DeleteResourceRightClickButton.Click += new EventHandler(DeleteResourceRightClickButton_Click);
            ToolStripSeparator1 = new ToolStripSeparator();
            CompilesToNitroFSButton = new ToolStripMenuItem();
            CompilesToNitroFSButton.Click += new EventHandler(CompilesToNitroFSButton_Click);
            Splitter1 = new Splitter();
            DMainMenuStrip.SuspendLayout();
            MainToolStrip.SuspendLayout();
            ResRightClickMenu.SuspendLayout();
            SuspendLayout();
            // 
            // DMainMenuStrip
            // 
            DMainMenuStrip.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            DMainMenuStrip.Items.AddRange(new ToolStripItem[] { FileMenu, EditMenu, ResourcesMenu, ToolsMenu, WindowMenu, HelpMenu });
            DMainMenuStrip.Location = new Point(0, 0);
            DMainMenuStrip.MdiWindowListItem = WindowMenu;
            DMainMenuStrip.Name = "DMainMenuStrip";
            DMainMenuStrip.Size = new Size(724, 24);
            DMainMenuStrip.TabIndex = 0;
            DMainMenuStrip.Text = "MenuStrip1";
            // 
            // FileMenu
            // 
            FileMenu.DropDownItems.AddRange(new ToolStripItem[] { NewProjectButton, OpenProjectButton, OpenLastProjectButton, MenuSep1, SaveButton, SaveAsButton, MenuSep2, TestGameButton, CompileGameButton });
            FileMenu.Name = "FileMenu";
            FileMenu.Size = new Size(35, 20);
            FileMenu.Text = "File";
            // 
            // NewProjectButton
            // 
            NewProjectButton.Image = Properties.Resources.NewIcon;
            NewProjectButton.Name = "NewProjectButton";
            NewProjectButton.ShortcutKeys = Keys.Control | Keys.N;
            NewProjectButton.Size = new Size(230, 22);
            NewProjectButton.Text = "New Project...";
            // 
            // OpenProjectButton
            // 
            OpenProjectButton.Image = Properties.Resources.OpenIcon;
            OpenProjectButton.Name = "OpenProjectButton";
            OpenProjectButton.ShortcutKeys = Keys.Control | Keys.O;
            OpenProjectButton.Size = new Size(230, 22);
            OpenProjectButton.Text = "Open...";
            // 
            // OpenLastProjectButton
            // 
            OpenLastProjectButton.Name = "OpenLastProjectButton";
            OpenLastProjectButton.ShortcutKeys = Keys.Control | Keys.Shift | Keys.O;
            OpenLastProjectButton.Size = new Size(230, 22);
            OpenLastProjectButton.Text = "Open Last Project";
            // 
            // MenuSep1
            // 
            MenuSep1.Name = "MenuSep1";
            MenuSep1.Size = new Size(227, 6);
            // 
            // SaveButton
            // 
            SaveButton.Image = Properties.Resources.SaveIcon;
            SaveButton.Name = "SaveButton";
            SaveButton.ShortcutKeys = Keys.Control | Keys.S;
            SaveButton.Size = new Size(230, 22);
            SaveButton.Text = "Save";
            // 
            // SaveAsButton
            // 
            SaveAsButton.Image = Properties.Resources.SaveAsIcon;
            SaveAsButton.Name = "SaveAsButton";
            SaveAsButton.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            SaveAsButton.Size = new Size(230, 22);
            SaveAsButton.Text = "Save As...";
            // 
            // MenuSep2
            // 
            MenuSep2.Name = "MenuSep2";
            MenuSep2.Size = new Size(227, 6);
            // 
            // TestGameButton
            // 
            TestGameButton.Image = Properties.Resources.TestGameIcon;
            TestGameButton.Name = "TestGameButton";
            TestGameButton.ShortcutKeys = Keys.F5;
            TestGameButton.Size = new Size(230, 22);
            TestGameButton.Text = "Test Game";
            // 
            // CompileGameButton
            // 
            CompileGameButton.Image = Properties.Resources.TestGameSaveIcon;
            CompileGameButton.Name = "CompileGameButton";
            CompileGameButton.ShortcutKeys = Keys.F6;
            CompileGameButton.Size = new Size(230, 22);
            CompileGameButton.Text = "Compile Game...";
            // 
            // EditMenu
            // 
            EditMenu.DropDownItems.AddRange(new ToolStripItem[] { DuplicateButton, DeleteButton, MenuSep4, FindInScriptsButton, MenuSep41, FindResourceButton, GoToLastFoundButton });
            EditMenu.Name = "EditMenu";
            EditMenu.Size = new Size(37, 20);
            EditMenu.Text = "Edit";
            // 
            // DuplicateButton
            // 
            DuplicateButton.Image = Properties.Resources.CopyIcon;
            DuplicateButton.Name = "DuplicateButton";
            DuplicateButton.Size = new Size(224, 22);
            DuplicateButton.Text = "Duplicate";
            // 
            // DeleteButton
            // 
            DeleteButton.Image = Properties.Resources.DeleteIcon;
            DeleteButton.Name = "DeleteButton";
            DeleteButton.ShortcutKeys = Keys.Control | Keys.Delete;
            DeleteButton.Size = new Size(224, 22);
            DeleteButton.Text = "Delete";
            // 
            // MenuSep4
            // 
            MenuSep4.Name = "MenuSep4";
            MenuSep4.Size = new Size(221, 6);
            // 
            // FindInScriptsButton
            // 
            FindInScriptsButton.Name = "FindInScriptsButton";
            FindInScriptsButton.ShortcutKeys = Keys.Control | Keys.F;
            FindInScriptsButton.Size = new Size(224, 22);
            FindInScriptsButton.Text = "Find in Scripts";
            // 
            // MenuSep41
            // 
            MenuSep41.Name = "MenuSep41";
            MenuSep41.Size = new Size(221, 6);
            // 
            // FindResourceButton
            // 
            FindResourceButton.Name = "FindResourceButton";
            FindResourceButton.ShortcutKeys = Keys.Control | Keys.Shift | Keys.F;
            FindResourceButton.Size = new Size(224, 22);
            FindResourceButton.Text = "Find Resource";
            // 
            // GoToLastFoundButton
            // 
            GoToLastFoundButton.Name = "GoToLastFoundButton";
            GoToLastFoundButton.ShortcutKeys = Keys.Shift | Keys.F3;
            GoToLastFoundButton.Size = new Size(224, 22);
            GoToLastFoundButton.Text = "Last Found Resource";
            // 
            // ResourcesMenu
            // 
            ResourcesMenu.DropDownItems.AddRange(new ToolStripItem[] { AddSpriteButton, AddObjectButton, AddBackgroundButton, AddSoundButton, AddRoomButton, MenuSep5, AddPathButton, AddScriptButton });
            ResourcesMenu.Name = "ResourcesMenu";
            ResourcesMenu.Size = new Size(69, 20);
            ResourcesMenu.Text = "Resources";
            // 
            // AddSpriteButton
            // 
            AddSpriteButton.Image = Properties.Resources.SpriteAddIcon;
            AddSpriteButton.Name = "AddSpriteButton";
            AddSpriteButton.Size = new Size(152, 22);
            AddSpriteButton.Text = "Add Sprite";
            // 
            // AddObjectButton
            // 
            AddObjectButton.Image = Properties.Resources.ObjectAddIcon;
            AddObjectButton.Name = "AddObjectButton";
            AddObjectButton.Size = new Size(152, 22);
            AddObjectButton.Text = "Add Object";
            // 
            // AddBackgroundButton
            // 
            AddBackgroundButton.Image = Properties.Resources.BackgroundAddIcon;
            AddBackgroundButton.Name = "AddBackgroundButton";
            AddBackgroundButton.Size = new Size(152, 22);
            AddBackgroundButton.Text = "Add Background";
            // 
            // AddSoundButton
            // 
            AddSoundButton.Image = Properties.Resources.SoundAddIcon;
            AddSoundButton.Name = "AddSoundButton";
            AddSoundButton.Size = new Size(152, 22);
            AddSoundButton.Text = "Add Sound";
            // 
            // AddRoomButton
            // 
            AddRoomButton.Image = Properties.Resources.RoomAddIcon;
            AddRoomButton.Name = "AddRoomButton";
            AddRoomButton.Size = new Size(152, 22);
            AddRoomButton.Text = "Add Room";
            // 
            // MenuSep5
            // 
            MenuSep5.Name = "MenuSep5";
            MenuSep5.Size = new Size(149, 6);
            // 
            // AddPathButton
            // 
            AddPathButton.Image = Properties.Resources.PathAddIcon;
            AddPathButton.Name = "AddPathButton";
            AddPathButton.Size = new Size(152, 22);
            AddPathButton.Text = "Add Path";
            AddPathButton.Visible = false;
            // 
            // AddScriptButton
            // 
            AddScriptButton.Image = Properties.Resources.ScriptAddIcon;
            AddScriptButton.Name = "AddScriptButton";
            AddScriptButton.Size = new Size(152, 22);
            AddScriptButton.Text = "Add Script";
            // 
            // ToolsMenu
            // 
            ToolsMenu.DropDownItems.AddRange(new ToolStripItem[] { GameSettingsButton, GlobalVariablesButton, GlobalArraysButton, GlobalStructuresButton, OptionsButton, MenuSep7, ActionEditorButton, FontViewerButton, MenuSep8, RunNOGBAButton, AdvancedButton });
            ToolsMenu.Name = "ToolsMenu";
            ToolsMenu.Size = new Size(44, 20);
            ToolsMenu.Text = "Tools";
            // 
            // GameSettingsButton
            // 
            GameSettingsButton.Image = Properties.Resources.GameSettingsIcon;
            GameSettingsButton.Name = "GameSettingsButton";
            GameSettingsButton.Size = new Size(168, 22);
            GameSettingsButton.Text = "Game Settings...";
            // 
            // GlobalVariablesButton
            // 
            GlobalVariablesButton.Image = Properties.Resources.VariableManagerIcon;
            GlobalVariablesButton.Name = "GlobalVariablesButton";
            GlobalVariablesButton.Size = new Size(168, 22);
            GlobalVariablesButton.Text = "Global Variables...";
            // 
            // GlobalArraysButton
            // 
            GlobalArraysButton.Image = Properties.Resources.ArrayIcon;
            GlobalArraysButton.Name = "GlobalArraysButton";
            GlobalArraysButton.Size = new Size(168, 22);
            GlobalArraysButton.Text = "Global Arrays...";
            // 
            // GlobalStructuresButton
            // 
            GlobalStructuresButton.Image = Properties.Resources.StructureIcon;
            GlobalStructuresButton.Name = "GlobalStructuresButton";
            GlobalStructuresButton.Size = new Size(168, 22);
            GlobalStructuresButton.Text = "Global Structures...";
            // 
            // OptionsButton
            // 
            OptionsButton.Image = Properties.Resources.OptionsIcon;
            OptionsButton.Name = "OptionsButton";
            OptionsButton.ShortcutKeys = Keys.Control | Keys.P;
            OptionsButton.Size = new Size(168, 22);
            OptionsButton.Text = "Options...";
            // 
            // MenuSep7
            // 
            MenuSep7.Name = "MenuSep7";
            MenuSep7.Size = new Size(165, 6);
            // 
            // ActionEditorButton
            // 
            ActionEditorButton.Image = Properties.Resources.ActionEditIcon;
            ActionEditorButton.Name = "ActionEditorButton";
            ActionEditorButton.Size = new Size(168, 22);
            ActionEditorButton.Text = "Action Editor";
            // 
            // FontViewerButton
            // 
            FontViewerButton.Name = "FontViewerButton";
            FontViewerButton.Size = new Size(168, 22);
            FontViewerButton.Text = "Font Viewer";
            // 
            // MenuSep8
            // 
            MenuSep8.Name = "MenuSep8";
            MenuSep8.Size = new Size(165, 6);
            // 
            // RunNOGBAButton
            // 
            RunNOGBAButton.Name = "RunNOGBAButton";
            RunNOGBAButton.Size = new Size(168, 22);
            RunNOGBAButton.Text = "Run NO$GBA";
            // 
            // AdvancedButton
            // 
            AdvancedButton.DropDownItems.AddRange(new ToolStripItem[] { CleanUpButton, EditInternalXDSButton, MenuSep9, OpenCompileTempButton, OpenProjectTempButton, MenuSep10, GraphicsChangeButton, SoundChangeButton });
            AdvancedButton.Name = "AdvancedButton";
            AdvancedButton.Size = new Size(168, 22);
            AdvancedButton.Text = "Advanced";
            // 
            // CleanUpButton
            // 
            CleanUpButton.Name = "CleanUpButton";
            CleanUpButton.Size = new Size(191, 22);
            CleanUpButton.Text = "Clean Up";
            // 
            // EditInternalXDSButton
            // 
            EditInternalXDSButton.Name = "EditInternalXDSButton";
            EditInternalXDSButton.ShortcutKeys = Keys.Control | Keys.I;
            EditInternalXDSButton.Size = new Size(191, 22);
            EditInternalXDSButton.Text = "Edit Internal XDS";
            // 
            // MenuSep9
            // 
            MenuSep9.Name = "MenuSep9";
            MenuSep9.Size = new Size(188, 6);
            // 
            // OpenCompileTempButton
            // 
            OpenCompileTempButton.Name = "OpenCompileTempButton";
            OpenCompileTempButton.Size = new Size(191, 22);
            OpenCompileTempButton.Text = "Open Compile Temp";
            // 
            // OpenProjectTempButton
            // 
            OpenProjectTempButton.Name = "OpenProjectTempButton";
            OpenProjectTempButton.Size = new Size(191, 22);
            OpenProjectTempButton.Text = "Open Project Temp";
            // 
            // MenuSep10
            // 
            MenuSep10.Name = "MenuSep10";
            MenuSep10.Size = new Size(188, 6);
            // 
            // GraphicsChangeButton
            // 
            GraphicsChangeButton.Name = "GraphicsChangeButton";
            GraphicsChangeButton.Size = new Size(191, 22);
            GraphicsChangeButton.Text = "Signify Graphics Change";
            // 
            // SoundChangeButton
            // 
            SoundChangeButton.Name = "SoundChangeButton";
            SoundChangeButton.Size = new Size(191, 22);
            SoundChangeButton.Text = "Signify Sound Change";
            // 
            // WindowMenu
            // 
            WindowMenu.Name = "WindowMenu";
            WindowMenu.Size = new Size(57, 20);
            WindowMenu.Text = "Window";
            // 
            // HelpMenu
            // 
            HelpMenu.DropDownItems.AddRange(new ToolStripItem[] { HelpContentsButton, OnlineTutorialsButton, MenuSep11, NewsButton, WebsiteButton, ForumButton, MenuSep13, ReinstallToolchainButton, AboutDSGMButton });
            HelpMenu.Name = "HelpMenu";
            HelpMenu.Size = new Size(40, 20);
            HelpMenu.Text = "Help";
            // 
            // HelpContentsButton
            // 
            HelpContentsButton.Image = Properties.Resources.QuestionIcon;
            HelpContentsButton.Name = "HelpContentsButton";
            HelpContentsButton.ShortcutKeys = Keys.F1;
            HelpContentsButton.Size = new Size(181, 22);
            HelpContentsButton.Text = "Contents";
            // 
            // OnlineTutorialsButton
            // 
            OnlineTutorialsButton.Name = "OnlineTutorialsButton";
            OnlineTutorialsButton.Size = new Size(181, 22);
            OnlineTutorialsButton.Text = "Online Tutorials";
            // 
            // MenuSep11
            // 
            MenuSep11.Name = "MenuSep11";
            MenuSep11.Size = new Size(178, 6);
            // 
            // NewsButton
            // 
            NewsButton.Image = (Image)resources.GetObject("NewsButton.Image");
            NewsButton.Name = "NewsButton";
            NewsButton.Size = new Size(181, 22);
            NewsButton.Text = "News...";
            // 
            // WebsiteButton
            // 
            WebsiteButton.Image = Properties.Resources.InternetIcon;
            WebsiteButton.Name = "WebsiteButton";
            WebsiteButton.Size = new Size(181, 22);
            WebsiteButton.Text = "DSGameMaker.com";
            // 
            // ForumButton
            // 
            ForumButton.Name = "ForumButton";
            ForumButton.Size = new Size(181, 22);
            ForumButton.Text = "Discussion Forum";
            // 
            // MenuSep13
            // 
            MenuSep13.Name = "MenuSep13";
            MenuSep13.Size = new Size(178, 6);
            // 
            // ReinstallToolchainButton
            // 
            ReinstallToolchainButton.Name = "ReinstallToolchainButton";
            ReinstallToolchainButton.Size = new Size(181, 22);
            ReinstallToolchainButton.Text = "Reinstall Toolchain";
            // 
            // AboutDSGMButton
            // 
            AboutDSGMButton.Name = "AboutDSGMButton";
            AboutDSGMButton.Size = new Size(181, 22);
            AboutDSGMButton.Text = "About DS Game Maker";
            // 
            // MainToolStrip
            // 
            MainToolStrip.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainToolStrip.Items.AddRange(new ToolStripItem[] { NewProjectButtonTool, OpenProjectButtonTool, OpenLastProjectButtonTool, SaveButtonTool, SaveAsButtonTool, ToolSep1, TestGameButtonTool, CompileGameButtonTool, OptionsButtonTool, ToolSep3, AddSpriteButtonTool, AddObjectButtonTool, AddBackgroundButtonTool, AddSoundButtonTool, AddRoomButtonTool, AddPathButtonTool, AddScriptButtonTool, ToolSep4, GameSettingsButtonTool, GlobalVariablesButtonTool, GlobalArraysButtonTool, GlobalStructuresButtonTool });
            MainToolStrip.Location = new Point(0, 24);
            MainToolStrip.Name = "MainToolStrip";
            MainToolStrip.Size = new Size(724, 25);
            MainToolStrip.TabIndex = 1;
            // 
            // NewProjectButtonTool
            // 
            NewProjectButtonTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            NewProjectButtonTool.Image = Properties.Resources.NewIcon;
            NewProjectButtonTool.ImageTransparentColor = Color.Magenta;
            NewProjectButtonTool.Name = "NewProjectButtonTool";
            NewProjectButtonTool.Size = new Size(23, 22);
            NewProjectButtonTool.Text = "New Project...";
            // 
            // OpenProjectButtonTool
            // 
            OpenProjectButtonTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            OpenProjectButtonTool.Image = Properties.Resources.OpenIcon;
            OpenProjectButtonTool.ImageTransparentColor = Color.Magenta;
            OpenProjectButtonTool.Name = "OpenProjectButtonTool";
            OpenProjectButtonTool.Size = new Size(23, 22);
            OpenProjectButtonTool.Text = "Open...";
            // 
            // OpenLastProjectButtonTool
            // 
            OpenLastProjectButtonTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            OpenLastProjectButtonTool.Image = Properties.Resources.OpenLastIcon;
            OpenLastProjectButtonTool.ImageTransparentColor = Color.Magenta;
            OpenLastProjectButtonTool.Name = "OpenLastProjectButtonTool";
            OpenLastProjectButtonTool.Size = new Size(23, 22);
            OpenLastProjectButtonTool.Text = "Open Last Project";
            // 
            // SaveButtonTool
            // 
            SaveButtonTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            SaveButtonTool.Image = Properties.Resources.SaveIcon;
            SaveButtonTool.ImageTransparentColor = Color.Magenta;
            SaveButtonTool.Name = "SaveButtonTool";
            SaveButtonTool.Size = new Size(23, 22);
            SaveButtonTool.Text = "Save";
            // 
            // SaveAsButtonTool
            // 
            SaveAsButtonTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            SaveAsButtonTool.Image = Properties.Resources.SaveAsIcon;
            SaveAsButtonTool.ImageTransparentColor = Color.Magenta;
            SaveAsButtonTool.Name = "SaveAsButtonTool";
            SaveAsButtonTool.Size = new Size(23, 22);
            SaveAsButtonTool.Text = "Save As...";
            // 
            // ToolSep1
            // 
            ToolSep1.Name = "ToolSep1";
            ToolSep1.Size = new Size(6, 25);
            // 
            // TestGameButtonTool
            // 
            TestGameButtonTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TestGameButtonTool.Image = Properties.Resources.TestGameIcon;
            TestGameButtonTool.ImageTransparentColor = Color.Magenta;
            TestGameButtonTool.Name = "TestGameButtonTool";
            TestGameButtonTool.Size = new Size(23, 22);
            TestGameButtonTool.Text = "Test Game";
            // 
            // CompileGameButtonTool
            // 
            CompileGameButtonTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            CompileGameButtonTool.Image = Properties.Resources.TestGameSaveIcon;
            CompileGameButtonTool.ImageTransparentColor = Color.Magenta;
            CompileGameButtonTool.Name = "CompileGameButtonTool";
            CompileGameButtonTool.Size = new Size(23, 22);
            CompileGameButtonTool.Text = "Compile Game...";
            // 
            // OptionsButtonTool
            // 
            OptionsButtonTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            OptionsButtonTool.Image = Properties.Resources.OptionsIcon;
            OptionsButtonTool.ImageTransparentColor = Color.Magenta;
            OptionsButtonTool.Name = "OptionsButtonTool";
            OptionsButtonTool.Size = new Size(23, 22);
            OptionsButtonTool.Text = "Options...";
            // 
            // ToolSep3
            // 
            ToolSep3.Name = "ToolSep3";
            ToolSep3.Size = new Size(6, 25);
            // 
            // AddSpriteButtonTool
            // 
            AddSpriteButtonTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            AddSpriteButtonTool.Image = Properties.Resources.SpriteAddIcon;
            AddSpriteButtonTool.ImageTransparentColor = Color.Magenta;
            AddSpriteButtonTool.Name = "AddSpriteButtonTool";
            AddSpriteButtonTool.Size = new Size(23, 22);
            AddSpriteButtonTool.Text = "Add Sprite";
            // 
            // AddObjectButtonTool
            // 
            AddObjectButtonTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            AddObjectButtonTool.Image = Properties.Resources.ObjectAddIcon;
            AddObjectButtonTool.ImageTransparentColor = Color.Magenta;
            AddObjectButtonTool.Name = "AddObjectButtonTool";
            AddObjectButtonTool.Size = new Size(23, 22);
            AddObjectButtonTool.Text = "Add Object";
            // 
            // AddBackgroundButtonTool
            // 
            AddBackgroundButtonTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            AddBackgroundButtonTool.Image = Properties.Resources.BackgroundAddIcon;
            AddBackgroundButtonTool.ImageTransparentColor = Color.Magenta;
            AddBackgroundButtonTool.Name = "AddBackgroundButtonTool";
            AddBackgroundButtonTool.Size = new Size(23, 22);
            AddBackgroundButtonTool.Text = "Add Background";
            // 
            // AddSoundButtonTool
            // 
            AddSoundButtonTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            AddSoundButtonTool.Image = Properties.Resources.SoundAddIcon;
            AddSoundButtonTool.ImageTransparentColor = Color.Magenta;
            AddSoundButtonTool.Name = "AddSoundButtonTool";
            AddSoundButtonTool.Size = new Size(23, 22);
            AddSoundButtonTool.Text = "Add Sound";
            // 
            // AddRoomButtonTool
            // 
            AddRoomButtonTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            AddRoomButtonTool.Image = Properties.Resources.RoomAddIcon;
            AddRoomButtonTool.ImageTransparentColor = Color.Magenta;
            AddRoomButtonTool.Name = "AddRoomButtonTool";
            AddRoomButtonTool.Size = new Size(23, 22);
            AddRoomButtonTool.Text = "Add Room";
            // 
            // AddPathButtonTool
            // 
            AddPathButtonTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            AddPathButtonTool.Image = Properties.Resources.PathAddIcon;
            AddPathButtonTool.ImageTransparentColor = Color.Magenta;
            AddPathButtonTool.Name = "AddPathButtonTool";
            AddPathButtonTool.Size = new Size(23, 22);
            AddPathButtonTool.Text = "Add Path";
            AddPathButtonTool.Visible = false;
            // 
            // AddScriptButtonTool
            // 
            AddScriptButtonTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            AddScriptButtonTool.Image = Properties.Resources.ScriptAddIcon;
            AddScriptButtonTool.ImageTransparentColor = Color.Magenta;
            AddScriptButtonTool.Name = "AddScriptButtonTool";
            AddScriptButtonTool.Size = new Size(23, 22);
            AddScriptButtonTool.Text = "Add Script";
            // 
            // ToolSep4
            // 
            ToolSep4.Name = "ToolSep4";
            ToolSep4.Size = new Size(6, 25);
            // 
            // GameSettingsButtonTool
            // 
            GameSettingsButtonTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            GameSettingsButtonTool.Image = Properties.Resources.GameSettingsIcon;
            GameSettingsButtonTool.ImageTransparentColor = Color.Magenta;
            GameSettingsButtonTool.Name = "GameSettingsButtonTool";
            GameSettingsButtonTool.Size = new Size(23, 22);
            GameSettingsButtonTool.Text = "Game Settings...";
            // 
            // GlobalVariablesButtonTool
            // 
            GlobalVariablesButtonTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            GlobalVariablesButtonTool.Image = Properties.Resources.VariableManagerIcon;
            GlobalVariablesButtonTool.ImageTransparentColor = Color.Magenta;
            GlobalVariablesButtonTool.Name = "GlobalVariablesButtonTool";
            GlobalVariablesButtonTool.Size = new Size(23, 22);
            GlobalVariablesButtonTool.Text = "Global Variables...";
            // 
            // GlobalArraysButtonTool
            // 
            GlobalArraysButtonTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            GlobalArraysButtonTool.Image = Properties.Resources.ArrayIcon;
            GlobalArraysButtonTool.ImageTransparentColor = Color.Magenta;
            GlobalArraysButtonTool.Name = "GlobalArraysButtonTool";
            GlobalArraysButtonTool.Size = new Size(23, 22);
            GlobalArraysButtonTool.Text = "Global Arrays...";
            // 
            // GlobalStructuresButtonTool
            // 
            GlobalStructuresButtonTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            GlobalStructuresButtonTool.Image = Properties.Resources.StructureIcon;
            GlobalStructuresButtonTool.ImageTransparentColor = Color.Magenta;
            GlobalStructuresButtonTool.Name = "GlobalStructuresButtonTool";
            GlobalStructuresButtonTool.Size = new Size(23, 22);
            GlobalStructuresButtonTool.Text = "Global Structures...";
            // 
            // MainImageList
            // 
            MainImageList.ColorDepth = ColorDepth.Depth32Bit;
            MainImageList.ImageSize = new Size(16, 16);
            MainImageList.TransparentColor = Color.Transparent;
            // 
            // ResourcesTreeView
            // 
            ResourcesTreeView.BorderStyle = BorderStyle.None;
            ResourcesTreeView.ContextMenuStrip = ResRightClickMenu;
            ResourcesTreeView.Dock = DockStyle.Left;
            ResourcesTreeView.ImageIndex = 0;
            ResourcesTreeView.ImageList = MainImageList;
            ResourcesTreeView.Indent = 16;
            ResourcesTreeView.ItemHeight = 19;
            ResourcesTreeView.Location = new Point(0, 49);
            ResourcesTreeView.Name = "ResourcesTreeView";
            ResourcesTreeView.SelectedImageIndex = 0;
            ResourcesTreeView.Size = new Size(192, 493);
            ResourcesTreeView.TabIndex = 5;
            // 
            // ResRightClickMenu
            // 
            ResRightClickMenu.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ResRightClickMenu.Items.AddRange(new ToolStripItem[] { OpenResourceRightClickButton, RightClickSeparator, AddResourceRightClickButton, DuplicateResourceRightClickButton, DeleteResourceRightClickButton, ToolStripSeparator1, CompilesToNitroFSButton });
            ResRightClickMenu.Name = "ResRightClickMenu";
            ResRightClickMenu.Size = new Size(168, 126);
            // 
            // OpenResourceRightClickButton
            // 
            OpenResourceRightClickButton.Enabled = false;
            OpenResourceRightClickButton.Name = "OpenResourceRightClickButton";
            OpenResourceRightClickButton.Size = new Size(167, 22);
            OpenResourceRightClickButton.Text = "Open";
            // 
            // RightClickSeparator
            // 
            RightClickSeparator.Name = "RightClickSeparator";
            RightClickSeparator.Size = new Size(164, 6);
            // 
            // AddResourceRightClickButton
            // 
            AddResourceRightClickButton.Name = "AddResourceRightClickButton";
            AddResourceRightClickButton.Size = new Size(167, 22);
            AddResourceRightClickButton.Text = "Add X";
            // 
            // DuplicateResourceRightClickButton
            // 
            DuplicateResourceRightClickButton.Enabled = false;
            DuplicateResourceRightClickButton.Image = Properties.Resources.CopyIcon;
            DuplicateResourceRightClickButton.Name = "DuplicateResourceRightClickButton";
            DuplicateResourceRightClickButton.Size = new Size(167, 22);
            DuplicateResourceRightClickButton.Text = "Duplicate";
            // 
            // DeleteResourceRightClickButton
            // 
            DeleteResourceRightClickButton.Enabled = false;
            DeleteResourceRightClickButton.Image = Properties.Resources.DeleteIcon;
            DeleteResourceRightClickButton.Name = "DeleteResourceRightClickButton";
            DeleteResourceRightClickButton.Size = new Size(167, 22);
            DeleteResourceRightClickButton.Text = "Delete";
            // 
            // ToolStripSeparator1
            // 
            ToolStripSeparator1.Name = "ToolStripSeparator1";
            ToolStripSeparator1.Size = new Size(164, 6);
            // 
            // CompilesToNitroFSButton
            // 
            CompilesToNitroFSButton.Image = Properties.Resources.DeleteIcon;
            CompilesToNitroFSButton.Name = "CompilesToNitroFSButton";
            CompilesToNitroFSButton.Size = new Size(167, 22);
            CompilesToNitroFSButton.Text = "Compiles to NitroFS";
            // 
            // Splitter1
            // 
            Splitter1.Location = new Point(192, 49);
            Splitter1.Name = "Splitter1";
            Splitter1.Size = new Size(3, 493);
            Splitter1.TabIndex = 6;
            Splitter1.TabStop = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(724, 542);
            Controls.Add(Splitter1);
            Controls.Add(ResourcesTreeView);
            Controls.Add(MainToolStrip);
            Controls.Add(DMainMenuStrip);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            IsMdiContainer = true;
            MainMenuStrip = DMainMenuStrip;
            Name = "MainForm";
            Text = "Loading...";
            WindowState = FormWindowState.Maximized;
            DMainMenuStrip.ResumeLayout(false);
            DMainMenuStrip.PerformLayout();
            MainToolStrip.ResumeLayout(false);
            MainToolStrip.PerformLayout();
            ResRightClickMenu.ResumeLayout(false);
            Load += new EventHandler(MainForm_Load);
            FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
            Shown += new EventHandler(MainForm_Shown);
            ResumeLayout(false);
            PerformLayout();

        }
        internal MenuStrip DMainMenuStrip;
        internal ToolStripMenuItem FileMenu;
        internal ToolStripMenuItem EditMenu;
        internal ToolStrip MainToolStrip;
        internal ToolStripMenuItem NewProjectButton;
        internal ToolStripMenuItem OpenProjectButton;
        internal ToolStripMenuItem OpenLastProjectButton;
        internal ToolStripSeparator MenuSep1;
        internal ToolStripMenuItem SaveButton;
        internal ToolStripMenuItem SaveAsButton;
        internal ToolStripSeparator MenuSep2;
        internal ToolStripMenuItem TestGameButton;
        internal ToolStripMenuItem CompileGameButton;
        internal ToolStripMenuItem DeleteButton;
        internal ToolStripMenuItem ResourcesMenu;
        internal ToolStripMenuItem AddSpriteButton;
        internal ToolStripMenuItem AddBackgroundButton;
        internal ToolStripMenuItem AddSoundButton;
        internal ToolStripMenuItem AddObjectButton;
        internal ToolStripMenuItem AddRoomButton;
        internal ToolStripMenuItem ToolsMenu;
        internal ToolStripMenuItem GlobalVariablesButton;
        internal ToolStripMenuItem HelpMenu;
        internal ToolStripButton NewProjectButtonTool;
        internal ToolStripButton OpenProjectButtonTool;
        internal ToolStripButton OpenLastProjectButtonTool;
        internal ToolStripButton SaveButtonTool;
        internal ToolStripButton SaveAsButtonTool;
        internal ToolStripMenuItem GameSettingsButton;
        internal ToolStripSeparator MenuSep7;
        internal ToolStripMenuItem OptionsButton;
        internal ToolStripSeparator ToolSep1;
        internal ToolStripSeparator MenuSep5;
        internal ToolStripMenuItem AddPathButton;
        internal ToolStripMenuItem AddScriptButton;
        internal ToolStripButton TestGameButtonTool;
        internal ToolStripButton CompileGameButtonTool;
        internal ToolStripButton OptionsButtonTool;
        internal ToolStripSeparator ToolSep3;
        internal ToolStripButton AddSpriteButtonTool;
        internal ToolStripButton AddObjectButtonTool;
        internal ToolStripButton AddBackgroundButtonTool;
        internal ToolStripButton AddSoundButtonTool;
        internal ToolStripButton AddRoomButtonTool;
        internal ToolStripButton AddPathButtonTool;
        internal ToolStripButton AddScriptButtonTool;
        internal ToolStripSeparator ToolSep4;
        internal ToolStripButton GameSettingsButtonTool;
        internal ToolStripButton GlobalVariablesButtonTool;
        internal ToolStripMenuItem HelpContentsButton;
        internal ToolStripMenuItem OnlineTutorialsButton;
        internal ToolStripSeparator MenuSep11;
        internal ToolStripMenuItem WebsiteButton;
        internal ToolStripMenuItem ForumButton;
        internal ToolStripMenuItem WindowMenu;
        internal ImageList MainImageList;
        internal TreeView ResourcesTreeView;
        internal Splitter Splitter1;
        internal ToolStripMenuItem ActionEditorButton;
        internal ToolStripMenuItem FontViewerButton;
        internal ToolStripSeparator MenuSep8;
        internal ToolStripMenuItem AdvancedButton;
        internal ToolStripMenuItem CleanUpButton;
        internal ToolStripMenuItem EditInternalXDSButton;
        internal ToolStripMenuItem OpenCompileTempButton;
        internal ContextMenuStrip ResRightClickMenu;
        internal ToolStripMenuItem AddResourceRightClickButton;
        internal ToolStripMenuItem DeleteResourceRightClickButton;
        internal ToolStripMenuItem OpenResourceRightClickButton;
        internal ToolStripSeparator RightClickSeparator;
        internal ToolStripMenuItem GlobalArraysButton;
        internal ToolStripButton GlobalArraysButtonTool;
        internal ToolStripSeparator MenuSep13;
        internal ToolStripMenuItem AboutDSGMButton;
        internal ToolStripMenuItem GlobalStructuresButton;
        internal ToolStripButton GlobalStructuresButtonTool;
        internal ToolStripMenuItem NewsButton;
        internal ToolStripMenuItem ReinstallToolchainButton;
        internal ToolStripSeparator MenuSep10;
        internal ToolStripMenuItem GraphicsChangeButton;
        internal ToolStripMenuItem SoundChangeButton;
        internal ToolStripMenuItem RunNOGBAButton;
        internal ToolStripMenuItem DuplicateResourceRightClickButton;
        internal ToolStripMenuItem DuplicateButton;
        internal ToolStripSeparator MenuSep4;
        internal ToolStripMenuItem FindResourceButton;
        internal ToolStripMenuItem GoToLastFoundButton;
        internal ToolStripMenuItem FindInScriptsButton;
        internal ToolStripSeparator MenuSep41;
        internal ToolStripSeparator MenuSep9;
        internal ToolStripMenuItem OpenProjectTempButton;
        internal ToolStripSeparator ToolStripSeparator1;
        internal ToolStripMenuItem CompilesToNitroFSButton;

    }
}