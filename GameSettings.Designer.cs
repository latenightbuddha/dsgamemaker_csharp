using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class GameSettings : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(GameSettings));
            MainTabControl = new TabControl();
            GeneralTabPage = new TabPage();
            MidPointCheckBox = new CheckBox();
            Label1 = new Label();
            NitroFSCallCheckBox = new CheckBox();
            FATCallCheckBox = new CheckBox();
            StartingRoomDropper = new ComboBox();
            StartingRoomLabel = new Label();
            ProjectInfoTabPage = new TabPage();
            BrowseButton = new Button();
            BrowseButton.Click += new EventHandler(BrowseButton_Click);
            IconFileTextbox = new TextBox();
            Label2 = new Label();
            ProjectInfoInfoLabel = new Label();
            Text3TextBox = new TextBox();
            Text3Label = new Label();
            Text2TextBox = new TextBox();
            Text2Label = new Label();
            ProjectNameTextBox = new TextBox();
            ProjectNameLabel = new Label();
            ScoringTabPage = new TabPage();
            HealthDropper = new NumericUpDown();
            ScoreDropper = new NumericUpDown();
            LivesDropper = new NumericUpDown();
            HealthLabel = new Label();
            LivesLabel = new Label();
            ScoreLabel = new Label();
            ScoringInfoLabel = new Label();
            CodingTabPage = new TabPage();
            NitroFSFilesControlPanel = new Panel();
            DeleteNitroFSFileButton = new Button();
            DeleteNitroFSFileButton.Click += new EventHandler(DeleteNitroFSFileButton_Click);
            EditNitroFSFileButton = new Button();
            EditNitroFSFileButton.Click += new EventHandler(EditNitroFSFileButton_Click);
            AddNitroFSFileButton = new Button();
            AddNitroFSFileButton.Click += new EventHandler(AddNitroFSFileButton_Click);
            IncludeFilesControlPanel = new Panel();
            DeleteIncludeFileButton = new Button();
            DeleteIncludeFileButton.Click += new EventHandler(DeleteIncludeFileButton_Click);
            EditIncludeFileButton = new Button();
            EditIncludeFileButton.Click += new EventHandler(EditIncludeFileButton_Click);
            AddIncludeFileButton = new Button();
            AddIncludeFileButton.Click += new EventHandler(AddIncludeFileButton_Click);
            NitroFSFilesLabel = new Label();
            NitroFSFilesList = new ListBox();
            IncludeFilesLabel = new Label();
            IncludeFilesList = new ListBox();
            IncludeWiFiLibChecker = new CheckBox();
            DCancelButton = new Button();
            DCancelButton.Click += new EventHandler(DCancelButton_Click);
            DOkayButton = new Button();
            DOkayButton.Click += new EventHandler(DOkayButton_Click);
            IconOpenFileDialog = new OpenFileDialog();
            IconOpenFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(IconOpenFileDialog_FileOk);
            MainTabControl.SuspendLayout();
            GeneralTabPage.SuspendLayout();
            ProjectInfoTabPage.SuspendLayout();
            ScoringTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)HealthDropper).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ScoreDropper).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LivesDropper).BeginInit();
            CodingTabPage.SuspendLayout();
            NitroFSFilesControlPanel.SuspendLayout();
            IncludeFilesControlPanel.SuspendLayout();
            SuspendLayout();
            // 
            // MainTabControl
            // 
            MainTabControl.Controls.Add(GeneralTabPage);
            MainTabControl.Controls.Add(ProjectInfoTabPage);
            MainTabControl.Controls.Add(ScoringTabPage);
            MainTabControl.Controls.Add(CodingTabPage);
            MainTabControl.Location = new Point(0, 0);
            MainTabControl.Name = "MainTabControl";
            MainTabControl.SelectedIndex = 0;
            MainTabControl.Size = new Size(284, 262);
            MainTabControl.TabIndex = 0;
            // 
            // GeneralTabPage
            // 
            GeneralTabPage.Controls.Add(MidPointCheckBox);
            GeneralTabPage.Controls.Add(Label1);
            GeneralTabPage.Controls.Add(NitroFSCallCheckBox);
            GeneralTabPage.Controls.Add(FATCallCheckBox);
            GeneralTabPage.Controls.Add(StartingRoomDropper);
            GeneralTabPage.Controls.Add(StartingRoomLabel);
            GeneralTabPage.Location = new Point(4, 22);
            GeneralTabPage.Name = "GeneralTabPage";
            GeneralTabPage.Padding = new Padding(3);
            GeneralTabPage.Size = new Size(276, 236);
            GeneralTabPage.TabIndex = 0;
            GeneralTabPage.Text = "General";
            GeneralTabPage.UseVisualStyleBackColor = true;
            // 
            // MidPointCheckBox
            // 
            MidPointCheckBox.AutoSize = true;
            MidPointCheckBox.Location = new Point(20, 104);
            MidPointCheckBox.Name = "MidPointCheckBox";
            MidPointCheckBox.Size = new Size(133, 17);
            MidPointCheckBox.TabIndex = 8;
            MidPointCheckBox.Text = "Use Midpoint Collisions";
            MidPointCheckBox.UseVisualStyleBackColor = true;
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Font = new Font("Tahoma", 8.25f, FontStyle.Italic, GraphicsUnit.Point, 0);
            Label1.Location = new Point(34, 88);
            Label1.Name = "Label1";
            Label1.Size = new Size(151, 13);
            Label1.TabIndex = 7;
            Label1.Text = "Enables Sound and Filesystem";
            // 
            // NitroFSCallCheckBox
            // 
            NitroFSCallCheckBox.AutoSize = true;
            NitroFSCallCheckBox.Location = new Point(20, 69);
            NitroFSCallCheckBox.Name = "NitroFSCallCheckBox";
            NitroFSCallCheckBox.Size = new Size(162, 17);
            NitroFSCallCheckBox.TabIndex = 3;
            NitroFSCallCheckBox.Text = "Perform NitroFS Initialization";
            NitroFSCallCheckBox.UseVisualStyleBackColor = true;
            // 
            // FATCallCheckBox
            // 
            FATCallCheckBox.AutoSize = true;
            FATCallCheckBox.Location = new Point(20, 46);
            FATCallCheckBox.Name = "FATCallCheckBox";
            FATCallCheckBox.Size = new Size(185, 17);
            FATCallCheckBox.TabIndex = 2;
            FATCallCheckBox.Text = "Perform Generic FAT Initialization";
            FATCallCheckBox.UseVisualStyleBackColor = true;
            // 
            // StartingRoomDropper
            // 
            StartingRoomDropper.FormattingEnabled = true;
            StartingRoomDropper.Location = new Point(119, 17);
            StartingRoomDropper.Name = "StartingRoomDropper";
            StartingRoomDropper.Size = new Size(112, 21);
            StartingRoomDropper.TabIndex = 1;
            // 
            // StartingRoomLabel
            // 
            StartingRoomLabel.AutoSize = true;
            StartingRoomLabel.Location = new Point(20, 20);
            StartingRoomLabel.Name = "StartingRoomLabel";
            StartingRoomLabel.Size = new Size(79, 13);
            StartingRoomLabel.TabIndex = 0;
            StartingRoomLabel.Text = "Starting Room:";
            // 
            // ProjectInfoTabPage
            // 
            ProjectInfoTabPage.Controls.Add(BrowseButton);
            ProjectInfoTabPage.Controls.Add(IconFileTextbox);
            ProjectInfoTabPage.Controls.Add(Label2);
            ProjectInfoTabPage.Controls.Add(ProjectInfoInfoLabel);
            ProjectInfoTabPage.Controls.Add(Text3TextBox);
            ProjectInfoTabPage.Controls.Add(Text3Label);
            ProjectInfoTabPage.Controls.Add(Text2TextBox);
            ProjectInfoTabPage.Controls.Add(Text2Label);
            ProjectInfoTabPage.Controls.Add(ProjectNameTextBox);
            ProjectInfoTabPage.Controls.Add(ProjectNameLabel);
            ProjectInfoTabPage.Location = new Point(4, 22);
            ProjectInfoTabPage.Name = "ProjectInfoTabPage";
            ProjectInfoTabPage.Padding = new Padding(3);
            ProjectInfoTabPage.Size = new Size(276, 236);
            ProjectInfoTabPage.TabIndex = 2;
            ProjectInfoTabPage.Text = "Project Info.";
            ProjectInfoTabPage.UseVisualStyleBackColor = true;
            // 
            // BrowseButton
            // 
            BrowseButton.Image = DS_Game_Maker.My.Resources.Resources.FolderIcon;
            BrowseButton.Location = new Point(232, 183);
            BrowseButton.Name = "BrowseButton";
            BrowseButton.Size = new Size(24, 24);
            BrowseButton.TabIndex = 15;
            BrowseButton.UseVisualStyleBackColor = true;
            // 
            // IconFileTextbox
            // 
            IconFileTextbox.Location = new Point(119, 183);
            IconFileTextbox.Name = "IconFileTextbox";
            IconFileTextbox.Size = new Size(107, 21);
            IconFileTextbox.TabIndex = 14;
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.Location = new Point(20, 186);
            Label2.Name = "Label2";
            Label2.Size = new Size(32, 13);
            Label2.TabIndex = 13;
            Label2.Text = "Icon:";
            // 
            // ProjectInfoInfoLabel
            // 
            ProjectInfoInfoLabel.BackColor = SystemColors.Control;
            ProjectInfoInfoLabel.BorderStyle = BorderStyle.Fixed3D;
            ProjectInfoInfoLabel.Location = new Point(20, 20);
            ProjectInfoInfoLabel.Name = "ProjectInfoInfoLabel";
            ProjectInfoInfoLabel.Padding = new Padding(8);
            ProjectInfoInfoLabel.Size = new Size(236, 72);
            ProjectInfoInfoLabel.TabIndex = 12;
            ProjectInfoInfoLabel.Text = "A game can have 3 lines of description texts that appear on the DS Homebrew Kit o" + "r other Flashcart. The Project Name is equivelant to the first thereof.";
            ProjectInfoInfoLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Text3TextBox
            // 
            Text3TextBox.Location = new Point(119, 156);
            Text3TextBox.MaxLength = 24;
            Text3TextBox.Name = "Text3TextBox";
            Text3TextBox.Size = new Size(112, 21);
            Text3TextBox.TabIndex = 11;
            // 
            // Text3Label
            // 
            Text3Label.AutoSize = true;
            Text3Label.Location = new Point(20, 159);
            Text3Label.Name = "Text3Label";
            Text3Label.Size = new Size(42, 13);
            Text3Label.TabIndex = 10;
            Text3Label.Text = "Text 3:";
            // 
            // Text2TextBox
            // 
            Text2TextBox.Location = new Point(119, 129);
            Text2TextBox.MaxLength = 24;
            Text2TextBox.Name = "Text2TextBox";
            Text2TextBox.Size = new Size(112, 21);
            Text2TextBox.TabIndex = 9;
            // 
            // Text2Label
            // 
            Text2Label.AutoSize = true;
            Text2Label.Location = new Point(20, 132);
            Text2Label.Name = "Text2Label";
            Text2Label.Size = new Size(42, 13);
            Text2Label.TabIndex = 8;
            Text2Label.Text = "Text 2:";
            // 
            // ProjectNameTextBox
            // 
            ProjectNameTextBox.Location = new Point(119, 102);
            ProjectNameTextBox.MaxLength = 24;
            ProjectNameTextBox.Name = "ProjectNameTextBox";
            ProjectNameTextBox.Size = new Size(112, 21);
            ProjectNameTextBox.TabIndex = 7;
            // 
            // ProjectNameLabel
            // 
            ProjectNameLabel.AutoSize = true;
            ProjectNameLabel.Location = new Point(20, 105);
            ProjectNameLabel.Name = "ProjectNameLabel";
            ProjectNameLabel.Size = new Size(75, 13);
            ProjectNameLabel.TabIndex = 6;
            ProjectNameLabel.Text = "Project Name:";
            // 
            // ScoringTabPage
            // 
            ScoringTabPage.Controls.Add(HealthDropper);
            ScoringTabPage.Controls.Add(ScoreDropper);
            ScoringTabPage.Controls.Add(LivesDropper);
            ScoringTabPage.Controls.Add(HealthLabel);
            ScoringTabPage.Controls.Add(LivesLabel);
            ScoringTabPage.Controls.Add(ScoreLabel);
            ScoringTabPage.Controls.Add(ScoringInfoLabel);
            ScoringTabPage.Location = new Point(4, 22);
            ScoringTabPage.Name = "ScoringTabPage";
            ScoringTabPage.Padding = new Padding(3);
            ScoringTabPage.Size = new Size(276, 236);
            ScoringTabPage.TabIndex = 1;
            ScoringTabPage.Text = "Scoring";
            ScoringTabPage.UseVisualStyleBackColor = true;
            // 
            // HealthDropper
            // 
            HealthDropper.Location = new Point(119, 144);
            HealthDropper.Maximum = new decimal(new int[] { 8192, 0, 0, 0 });
            HealthDropper.Name = "HealthDropper";
            HealthDropper.Size = new Size(60, 21);
            HealthDropper.TabIndex = 20;
            HealthDropper.TextAlign = HorizontalAlignment.Center;
            // 
            // ScoreDropper
            // 
            ScoreDropper.Location = new Point(119, 90);
            ScoreDropper.Maximum = new decimal(new int[] { 8192, 0, 0, 0 });
            ScoreDropper.Name = "ScoreDropper";
            ScoreDropper.Size = new Size(60, 21);
            ScoreDropper.TabIndex = 19;
            ScoreDropper.TextAlign = HorizontalAlignment.Center;
            // 
            // LivesDropper
            // 
            LivesDropper.Location = new Point(119, 117);
            LivesDropper.Maximum = new decimal(new int[] { 8192, 0, 0, 0 });
            LivesDropper.Name = "LivesDropper";
            LivesDropper.Size = new Size(60, 21);
            LivesDropper.TabIndex = 18;
            LivesDropper.TextAlign = HorizontalAlignment.Center;
            // 
            // HealthLabel
            // 
            HealthLabel.AutoSize = true;
            HealthLabel.Location = new Point(20, 146);
            HealthLabel.Name = "HealthLabel";
            HealthLabel.Size = new Size(42, 13);
            HealthLabel.TabIndex = 16;
            HealthLabel.Text = "Health:";
            // 
            // LivesLabel
            // 
            LivesLabel.AutoSize = true;
            LivesLabel.Location = new Point(20, 119);
            LivesLabel.Name = "LivesLabel";
            LivesLabel.Size = new Size(35, 13);
            LivesLabel.TabIndex = 14;
            LivesLabel.Text = "Lives:";
            // 
            // ScoreLabel
            // 
            ScoreLabel.AutoSize = true;
            ScoreLabel.Location = new Point(20, 92);
            ScoreLabel.Name = "ScoreLabel";
            ScoreLabel.Size = new Size(38, 13);
            ScoreLabel.TabIndex = 12;
            ScoreLabel.Text = "Score:";
            // 
            // ScoringInfoLabel
            // 
            ScoringInfoLabel.BackColor = SystemColors.Control;
            ScoringInfoLabel.BorderStyle = BorderStyle.Fixed3D;
            ScoringInfoLabel.Location = new Point(20, 20);
            ScoringInfoLabel.Name = "ScoringInfoLabel";
            ScoringInfoLabel.Padding = new Padding(8);
            ScoringInfoLabel.Size = new Size(236, 57);
            ScoringInfoLabel.TabIndex = 0;
            ScoringInfoLabel.Text = "Here you may set the default values (values when the Game starts) of the built-in" + " lives, health and score variables.";
            ScoringInfoLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CodingTabPage
            // 
            CodingTabPage.Controls.Add(NitroFSFilesControlPanel);
            CodingTabPage.Controls.Add(IncludeFilesControlPanel);
            CodingTabPage.Controls.Add(NitroFSFilesLabel);
            CodingTabPage.Controls.Add(NitroFSFilesList);
            CodingTabPage.Controls.Add(IncludeFilesLabel);
            CodingTabPage.Controls.Add(IncludeFilesList);
            CodingTabPage.Controls.Add(IncludeWiFiLibChecker);
            CodingTabPage.Location = new Point(4, 22);
            CodingTabPage.Name = "CodingTabPage";
            CodingTabPage.Padding = new Padding(3);
            CodingTabPage.Size = new Size(276, 236);
            CodingTabPage.TabIndex = 3;
            CodingTabPage.Text = "Coding";
            CodingTabPage.UseVisualStyleBackColor = true;
            // 
            // NitroFSFilesControlPanel
            // 
            NitroFSFilesControlPanel.BackColor = Color.FromArgb(224, 224, 224);
            NitroFSFilesControlPanel.Controls.Add(DeleteNitroFSFileButton);
            NitroFSFilesControlPanel.Controls.Add(EditNitroFSFileButton);
            NitroFSFilesControlPanel.Controls.Add(AddNitroFSFileButton);
            NitroFSFilesControlPanel.Location = new Point(143, 173);
            NitroFSFilesControlPanel.Name = "NitroFSFilesControlPanel";
            NitroFSFilesControlPanel.Size = new Size(113, 30);
            NitroFSFilesControlPanel.TabIndex = 7;
            // 
            // DeleteNitroFSFileButton
            // 
            DeleteNitroFSFileButton.AccessibleName = "Delete NitroFS File";
            DeleteNitroFSFileButton.Image = DS_Game_Maker.My.Resources.Resources.DeleteIcon;
            DeleteNitroFSFileButton.Location = new Point(62, 2);
            DeleteNitroFSFileButton.Name = "DeleteNitroFSFileButton";
            DeleteNitroFSFileButton.Size = new Size(30, 26);
            DeleteNitroFSFileButton.TabIndex = 10;
            DeleteNitroFSFileButton.UseVisualStyleBackColor = true;
            // 
            // EditNitroFSFileButton
            // 
            EditNitroFSFileButton.AccessibleName = "Edit NitroFS File";
            EditNitroFSFileButton.Image = DS_Game_Maker.My.Resources.Resources.PencilIcon;
            EditNitroFSFileButton.Location = new Point(32, 2);
            EditNitroFSFileButton.Name = "EditNitroFSFileButton";
            EditNitroFSFileButton.Size = new Size(30, 26);
            EditNitroFSFileButton.TabIndex = 9;
            EditNitroFSFileButton.UseVisualStyleBackColor = true;
            // 
            // AddNitroFSFileButton
            // 
            AddNitroFSFileButton.AccessibleName = "Add NitroFS File";
            AddNitroFSFileButton.Image = DS_Game_Maker.My.Resources.Resources.PlusIcon;
            AddNitroFSFileButton.Location = new Point(2, 2);
            AddNitroFSFileButton.Name = "AddNitroFSFileButton";
            AddNitroFSFileButton.Size = new Size(30, 26);
            AddNitroFSFileButton.TabIndex = 8;
            AddNitroFSFileButton.UseVisualStyleBackColor = true;
            // 
            // IncludeFilesControlPanel
            // 
            IncludeFilesControlPanel.BackColor = Color.FromArgb(224, 224, 224);
            IncludeFilesControlPanel.Controls.Add(DeleteIncludeFileButton);
            IncludeFilesControlPanel.Controls.Add(EditIncludeFileButton);
            IncludeFilesControlPanel.Controls.Add(AddIncludeFileButton);
            IncludeFilesControlPanel.Location = new Point(20, 173);
            IncludeFilesControlPanel.Name = "IncludeFilesControlPanel";
            IncludeFilesControlPanel.Size = new Size(113, 30);
            IncludeFilesControlPanel.TabIndex = 6;
            // 
            // DeleteIncludeFileButton
            // 
            DeleteIncludeFileButton.AccessibleName = "Delete Include File";
            DeleteIncludeFileButton.Image = DS_Game_Maker.My.Resources.Resources.DeleteIcon;
            DeleteIncludeFileButton.Location = new Point(62, 2);
            DeleteIncludeFileButton.Name = "DeleteIncludeFileButton";
            DeleteIncludeFileButton.Size = new Size(30, 26);
            DeleteIncludeFileButton.TabIndex = 7;
            DeleteIncludeFileButton.UseVisualStyleBackColor = true;
            // 
            // EditIncludeFileButton
            // 
            EditIncludeFileButton.AccessibleName = "Edit Include File";
            EditIncludeFileButton.Image = DS_Game_Maker.My.Resources.Resources.PencilIcon;
            EditIncludeFileButton.Location = new Point(32, 2);
            EditIncludeFileButton.Name = "EditIncludeFileButton";
            EditIncludeFileButton.Size = new Size(30, 26);
            EditIncludeFileButton.TabIndex = 6;
            EditIncludeFileButton.UseVisualStyleBackColor = true;
            // 
            // AddIncludeFileButton
            // 
            AddIncludeFileButton.AccessibleName = "Add Include File";
            AddIncludeFileButton.Image = DS_Game_Maker.My.Resources.Resources.PlusIcon;
            AddIncludeFileButton.Location = new Point(2, 2);
            AddIncludeFileButton.Name = "AddIncludeFileButton";
            AddIncludeFileButton.Size = new Size(30, 26);
            AddIncludeFileButton.TabIndex = 5;
            AddIncludeFileButton.UseVisualStyleBackColor = true;
            // 
            // NitroFSFilesLabel
            // 
            NitroFSFilesLabel.AutoSize = true;
            NitroFSFilesLabel.Location = new Point(140, 46);
            NitroFSFilesLabel.Name = "NitroFSFilesLabel";
            NitroFSFilesLabel.Size = new Size(70, 13);
            NitroFSFilesLabel.TabIndex = 4;
            NitroFSFilesLabel.Text = "NitroFS Files:";
            // 
            // NitroFSFilesList
            // 
            NitroFSFilesList.FormattingEnabled = true;
            NitroFSFilesList.Location = new Point(143, 62);
            NitroFSFilesList.Name = "NitroFSFilesList";
            NitroFSFilesList.Size = new Size(113, 108);
            NitroFSFilesList.TabIndex = 3;
            // 
            // IncludeFilesLabel
            // 
            IncludeFilesLabel.AutoSize = true;
            IncludeFilesLabel.Location = new Point(17, 46);
            IncludeFilesLabel.Name = "IncludeFilesLabel";
            IncludeFilesLabel.Size = new Size(70, 13);
            IncludeFilesLabel.TabIndex = 2;
            IncludeFilesLabel.Text = "Include Files:";
            // 
            // IncludeFilesList
            // 
            IncludeFilesList.FormattingEnabled = true;
            IncludeFilesList.Location = new Point(20, 62);
            IncludeFilesList.Name = "IncludeFilesList";
            IncludeFilesList.Size = new Size(113, 108);
            IncludeFilesList.TabIndex = 1;
            // 
            // IncludeWiFiLibChecker
            // 
            IncludeWiFiLibChecker.AutoSize = true;
            IncludeWiFiLibChecker.Location = new Point(20, 20);
            IncludeWiFiLibChecker.Name = "IncludeWiFiLibChecker";
            IncludeWiFiLibChecker.Size = new Size(102, 17);
            IncludeWiFiLibChecker.TabIndex = 0;
            IncludeWiFiLibChecker.Text = "Include WiFi Lib";
            IncludeWiFiLibChecker.UseVisualStyleBackColor = true;
            // 
            // DCancelButton
            // 
            DCancelButton.DialogResult = DialogResult.Cancel;
            DCancelButton.Location = new Point(201, 267);
            DCancelButton.Name = "DCancelButton";
            DCancelButton.Size = new Size(75, 28);
            DCancelButton.TabIndex = 1;
            DCancelButton.Text = "Cancel";
            DCancelButton.UseVisualStyleBackColor = true;
            // 
            // DOkayButton
            // 
            DOkayButton.Location = new Point(123, 267);
            DOkayButton.Name = "DOkayButton";
            DOkayButton.Size = new Size(75, 28);
            DOkayButton.TabIndex = 2;
            DOkayButton.Text = "OK";
            DOkayButton.UseVisualStyleBackColor = true;
            // 
            // IconOpenFileDialog
            // 
            IconOpenFileDialog.Filter = "BMP Images|*.bmp";
            IconOpenFileDialog.Title = "Select your project's icon";
            // 
            // GameSettings
            // 
            AcceptButton = DOkayButton;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = DCancelButton;
            ClientSize = new Size(284, 302);
            Controls.Add(DOkayButton);
            Controls.Add(DCancelButton);
            Controls.Add(MainTabControl);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(290, 330);
            MinimizeBox = false;
            Name = "GameSettings";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Game Settings";
            MainTabControl.ResumeLayout(false);
            GeneralTabPage.ResumeLayout(false);
            GeneralTabPage.PerformLayout();
            ProjectInfoTabPage.ResumeLayout(false);
            ProjectInfoTabPage.PerformLayout();
            ScoringTabPage.ResumeLayout(false);
            ScoringTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)HealthDropper).EndInit();
            ((System.ComponentModel.ISupportInitialize)ScoreDropper).EndInit();
            ((System.ComponentModel.ISupportInitialize)LivesDropper).EndInit();
            CodingTabPage.ResumeLayout(false);
            CodingTabPage.PerformLayout();
            NitroFSFilesControlPanel.ResumeLayout(false);
            IncludeFilesControlPanel.ResumeLayout(false);
            Load += new EventHandler(GameSettings_Load);
            ResumeLayout(false);

        }
        internal TabControl MainTabControl;
        internal TabPage GeneralTabPage;
        internal TabPage ScoringTabPage;
        internal TabPage ProjectInfoTabPage;
        internal TextBox Text3TextBox;
        internal Label Text3Label;
        internal TextBox Text2TextBox;
        internal Label Text2Label;
        internal TextBox ProjectNameTextBox;
        internal Label ProjectNameLabel;
        internal Button DCancelButton;
        internal Button DOkayButton;
        internal Label StartingRoomLabel;
        internal ComboBox StartingRoomDropper;
        internal Label ScoringInfoLabel;
        internal Label HealthLabel;
        internal Label LivesLabel;
        internal Label ScoreLabel;
        internal NumericUpDown HealthDropper;
        internal NumericUpDown ScoreDropper;
        internal NumericUpDown LivesDropper;
        internal Label ProjectInfoInfoLabel;
        internal CheckBox NitroFSCallCheckBox;
        internal CheckBox FATCallCheckBox;
        internal Label Label1;
        internal CheckBox MidPointCheckBox;
        internal TabPage CodingTabPage;
        internal CheckBox IncludeWiFiLibChecker;
        internal Label IncludeFilesLabel;
        internal ListBox IncludeFilesList;
        internal Label NitroFSFilesLabel;
        internal ListBox NitroFSFilesList;
        internal Button AddIncludeFileButton;
        internal Panel NitroFSFilesControlPanel;
        internal Panel IncludeFilesControlPanel;
        internal Button DeleteIncludeFileButton;
        internal Button EditIncludeFileButton;
        internal Button DeleteNitroFSFileButton;
        internal Button EditNitroFSFileButton;
        internal Button AddNitroFSFileButton;
        internal Button BrowseButton;
        internal TextBox IconFileTextbox;
        internal Label Label2;
        internal OpenFileDialog IconOpenFileDialog;
    }
}