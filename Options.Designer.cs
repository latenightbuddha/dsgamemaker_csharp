using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Options : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            OpenLastProjectCheckBox = new CheckBox();
            MainTabControl = new TabControl();
            GeneralTabPage = new TabPage();
            HideOldActionsChecker = new CheckBox();
            CloseNewslineCheckBox = new CheckBox();
            RoomsGroupBox = new GroupBox();
            DefaultRoomHeightTB = new TextBox();
            DefaultRoomWidthTB = new TextBox();
            DefaultRoomHeightLabel = new Label();
            DefaultRoomWidthLabel = new Label();
            TransparentAnimationsCheckBox = new CheckBox();
            CodeEditorGroupBox = new GroupBox();
            MatchBracesCheckBox = new CheckBox();
            HighlightCurrentLineCheckBox = new CheckBox();
            EditorsTabPage = new TabPage();
            ScriptEditorGroupBox = new GroupBox();
            ScriptEditorBrowseButton = new Button();
            ScriptEditorBrowseButton.Click += new EventHandler(ScriptEditorBowseButton_Click);
            ScriptEditorTextBox = new TextBox();
            UseExternalScriptEditorRadioButton = new RadioButton();
            UseInternalScriptEditorRadioButton = new RadioButton();
            SoundEditorGroupBox = new GroupBox();
            SoundEditorBrowseButton = new Button();
            SoundEditorBrowseButton.Click += new EventHandler(SoundEditorBowseButton_Click);
            SoundEditorTextBox = new TextBox();
            ImageEditorGroupBox = new GroupBox();
            ImageEditorBrowseButton = new Button();
            ImageEditorBrowseButton.Click += new EventHandler(ImageEditorBrowseButton_Click);
            ImageEditorTextBox = new TextBox();
            EmulatorTabPage = new TabPage();
            EmulatorInfoLabel = new Label();
            CustomEmulatorBrowseButton = new Button();
            CustomEmulatorBrowseButton.Click += new EventHandler(CustomEmulatorBrowseButton_Click);
            CustomEmulatorTextBox = new TextBox();
            RunCustomExecutableRadioButton = new RadioButton();
            UseNOGBARadioButton = new RadioButton();
            StartupTabPage = new TabPage();
            ShowNewsCheckBox = new CheckBox();
            DOkayButton = new Button();
            DOkayButton.Click += new EventHandler(DOkayButton_Click);
            ShrinkActionsListChecker = new CheckBox();
            MainTabControl.SuspendLayout();
            GeneralTabPage.SuspendLayout();
            RoomsGroupBox.SuspendLayout();
            CodeEditorGroupBox.SuspendLayout();
            EditorsTabPage.SuspendLayout();
            ScriptEditorGroupBox.SuspendLayout();
            SoundEditorGroupBox.SuspendLayout();
            ImageEditorGroupBox.SuspendLayout();
            EmulatorTabPage.SuspendLayout();
            StartupTabPage.SuspendLayout();
            SuspendLayout();
            // 
            // OpenLastProjectCheckBox
            // 
            OpenLastProjectCheckBox.AutoSize = true;
            OpenLastProjectCheckBox.Location = new Point(16, 16);
            OpenLastProjectCheckBox.Name = "OpenLastProjectCheckBox";
            OpenLastProjectCheckBox.Size = new Size(198, 17);
            OpenLastProjectCheckBox.TabIndex = 0;
            OpenLastProjectCheckBox.Text = "Open last loaded Project on Startup";
            OpenLastProjectCheckBox.UseVisualStyleBackColor = true;
            // 
            // MainTabControl
            // 
            MainTabControl.Controls.Add(GeneralTabPage);
            MainTabControl.Controls.Add(EditorsTabPage);
            MainTabControl.Controls.Add(EmulatorTabPage);
            MainTabControl.Controls.Add(StartupTabPage);
            MainTabControl.Dock = DockStyle.Top;
            MainTabControl.Location = new Point(0, 0);
            MainTabControl.Name = "MainTabControl";
            MainTabControl.SelectedIndex = 0;
            MainTabControl.Size = new Size(284, 309);
            MainTabControl.TabIndex = 1;
            // 
            // GeneralTabPage
            // 
            GeneralTabPage.Controls.Add(ShrinkActionsListChecker);
            GeneralTabPage.Controls.Add(HideOldActionsChecker);
            GeneralTabPage.Controls.Add(CloseNewslineCheckBox);
            GeneralTabPage.Controls.Add(RoomsGroupBox);
            GeneralTabPage.Controls.Add(TransparentAnimationsCheckBox);
            GeneralTabPage.Controls.Add(CodeEditorGroupBox);
            GeneralTabPage.Location = new Point(4, 22);
            GeneralTabPage.Name = "GeneralTabPage";
            GeneralTabPage.Padding = new Padding(3);
            GeneralTabPage.Size = new Size(276, 283);
            GeneralTabPage.TabIndex = 0;
            GeneralTabPage.Text = "General";
            GeneralTabPage.UseVisualStyleBackColor = true;
            // 
            // HideOldActionsChecker
            // 
            HideOldActionsChecker.AutoSize = true;
            HideOldActionsChecker.Location = new Point(16, 210);
            HideOldActionsChecker.Name = "HideOldActionsChecker";
            HideOldActionsChecker.Size = new Size(108, 17);
            HideOldActionsChecker.TabIndex = 4;
            HideOldActionsChecker.Text = "Hide 'Old' Actions";
            HideOldActionsChecker.UseVisualStyleBackColor = true;
            // 
            // CloseNewslineCheckBox
            // 
            CloseNewslineCheckBox.AutoSize = true;
            CloseNewslineCheckBox.Location = new Point(16, 16);
            CloseNewslineCheckBox.Name = "CloseNewslineCheckBox";
            CloseNewslineCheckBox.Size = new Size(213, 17);
            CloseNewslineCheckBox.TabIndex = 3;
            CloseNewslineCheckBox.Text = "Close Newsline when opening a Project";
            CloseNewslineCheckBox.UseVisualStyleBackColor = true;
            // 
            // RoomsGroupBox
            // 
            RoomsGroupBox.Controls.Add(DefaultRoomHeightTB);
            RoomsGroupBox.Controls.Add(DefaultRoomWidthTB);
            RoomsGroupBox.Controls.Add(DefaultRoomHeightLabel);
            RoomsGroupBox.Controls.Add(DefaultRoomWidthLabel);
            RoomsGroupBox.Location = new Point(8, 137);
            RoomsGroupBox.Name = "RoomsGroupBox";
            RoomsGroupBox.Size = new Size(260, 67);
            RoomsGroupBox.TabIndex = 2;
            RoomsGroupBox.TabStop = false;
            RoomsGroupBox.Text = "Rooms";
            // 
            // DefaultRoomHeightTB
            // 
            DefaultRoomHeightTB.Location = new Point(156, 37);
            DefaultRoomHeightTB.Name = "DefaultRoomHeightTB";
            DefaultRoomHeightTB.Size = new Size(52, 21);
            DefaultRoomHeightTB.TabIndex = 5;
            // 
            // DefaultRoomWidthTB
            // 
            DefaultRoomWidthTB.Location = new Point(156, 14);
            DefaultRoomWidthTB.Name = "DefaultRoomWidthTB";
            DefaultRoomWidthTB.Size = new Size(52, 21);
            DefaultRoomWidthTB.TabIndex = 3;
            // 
            // DefaultRoomHeightLabel
            // 
            DefaultRoomHeightLabel.AutoSize = true;
            DefaultRoomHeightLabel.Location = new Point(11, 40);
            DefaultRoomHeightLabel.Name = "DefaultRoomHeightLabel";
            DefaultRoomHeightLabel.Size = new Size(110, 13);
            DefaultRoomHeightLabel.TabIndex = 4;
            DefaultRoomHeightLabel.Text = "Default Room Height:";
            // 
            // DefaultRoomWidthLabel
            // 
            DefaultRoomWidthLabel.AutoSize = true;
            DefaultRoomWidthLabel.Location = new Point(10, 17);
            DefaultRoomWidthLabel.Name = "DefaultRoomWidthLabel";
            DefaultRoomWidthLabel.Size = new Size(107, 13);
            DefaultRoomWidthLabel.TabIndex = 3;
            DefaultRoomWidthLabel.Text = "Default Room Width:";
            // 
            // TransparentAnimationsCheckBox
            // 
            TransparentAnimationsCheckBox.AutoSize = true;
            TransparentAnimationsCheckBox.Location = new Point(16, 39);
            TransparentAnimationsCheckBox.Name = "TransparentAnimationsCheckBox";
            TransparentAnimationsCheckBox.Size = new Size(251, 17);
            TransparentAnimationsCheckBox.TabIndex = 2;
            TransparentAnimationsCheckBox.Text = "Use Transparency in Sprite Animation Previews";
            TransparentAnimationsCheckBox.UseVisualStyleBackColor = true;
            // 
            // CodeEditorGroupBox
            // 
            CodeEditorGroupBox.Controls.Add(MatchBracesCheckBox);
            CodeEditorGroupBox.Controls.Add(HighlightCurrentLineCheckBox);
            CodeEditorGroupBox.Location = new Point(8, 64);
            CodeEditorGroupBox.Name = "CodeEditorGroupBox";
            CodeEditorGroupBox.Size = new Size(260, 67);
            CodeEditorGroupBox.TabIndex = 1;
            CodeEditorGroupBox.TabStop = false;
            CodeEditorGroupBox.Text = "Code Editor && Viewer";
            // 
            // MatchBracesCheckBox
            // 
            MatchBracesCheckBox.AutoSize = true;
            MatchBracesCheckBox.Location = new Point(8, 43);
            MatchBracesCheckBox.Name = "MatchBracesCheckBox";
            MatchBracesCheckBox.Size = new Size(90, 17);
            MatchBracesCheckBox.TabIndex = 1;
            MatchBracesCheckBox.Text = "Match Braces";
            MatchBracesCheckBox.UseVisualStyleBackColor = true;
            // 
            // HighlightCurrentLineCheckBox
            // 
            HighlightCurrentLineCheckBox.AutoSize = true;
            HighlightCurrentLineCheckBox.Location = new Point(8, 20);
            HighlightCurrentLineCheckBox.Name = "HighlightCurrentLineCheckBox";
            HighlightCurrentLineCheckBox.Size = new Size(131, 17);
            HighlightCurrentLineCheckBox.TabIndex = 0;
            HighlightCurrentLineCheckBox.Text = "Highlight Working Line";
            HighlightCurrentLineCheckBox.UseVisualStyleBackColor = true;
            // 
            // EditorsTabPage
            // 
            EditorsTabPage.Controls.Add(ScriptEditorGroupBox);
            EditorsTabPage.Controls.Add(SoundEditorGroupBox);
            EditorsTabPage.Controls.Add(ImageEditorGroupBox);
            EditorsTabPage.Location = new Point(4, 22);
            EditorsTabPage.Name = "EditorsTabPage";
            EditorsTabPage.Padding = new Padding(3);
            EditorsTabPage.Size = new Size(276, 283);
            EditorsTabPage.TabIndex = 1;
            EditorsTabPage.Text = "Editors";
            EditorsTabPage.UseVisualStyleBackColor = true;
            // 
            // ScriptEditorGroupBox
            // 
            ScriptEditorGroupBox.Controls.Add(ScriptEditorBrowseButton);
            ScriptEditorGroupBox.Controls.Add(ScriptEditorTextBox);
            ScriptEditorGroupBox.Controls.Add(UseExternalScriptEditorRadioButton);
            ScriptEditorGroupBox.Controls.Add(UseInternalScriptEditorRadioButton);
            ScriptEditorGroupBox.Location = new Point(8, 130);
            ScriptEditorGroupBox.Name = "ScriptEditorGroupBox";
            ScriptEditorGroupBox.Size = new Size(260, 102);
            ScriptEditorGroupBox.TabIndex = 4;
            ScriptEditorGroupBox.TabStop = false;
            ScriptEditorGroupBox.Text = "Script Editor";
            // 
            // ScriptEditorBrowseButton
            // 
            ScriptEditorBrowseButton.Location = new Point(221, 70);
            ScriptEditorBrowseButton.Name = "ScriptEditorBrowseButton";
            ScriptEditorBrowseButton.Size = new Size(33, 23);
            ScriptEditorBrowseButton.TabIndex = 3;
            ScriptEditorBrowseButton.Text = "...";
            ScriptEditorBrowseButton.UseVisualStyleBackColor = true;
            // 
            // ScriptEditorTextBox
            // 
            ScriptEditorTextBox.Location = new Point(12, 71);
            ScriptEditorTextBox.Name = "ScriptEditorTextBox";
            ScriptEditorTextBox.Size = new Size(203, 21);
            ScriptEditorTextBox.TabIndex = 2;
            // 
            // UseExternalScriptEditorRadioButton
            // 
            UseExternalScriptEditorRadioButton.AutoSize = true;
            UseExternalScriptEditorRadioButton.Location = new Point(12, 46);
            UseExternalScriptEditorRadioButton.Name = "UseExternalScriptEditorRadioButton";
            UseExternalScriptEditorRadioButton.Size = new Size(117, 17);
            UseExternalScriptEditorRadioButton.TabIndex = 2;
            UseExternalScriptEditorRadioButton.TabStop = true;
            UseExternalScriptEditorRadioButton.Text = "Use External Editor";
            UseExternalScriptEditorRadioButton.UseVisualStyleBackColor = true;
            // 
            // UseInternalScriptEditorRadioButton
            // 
            UseInternalScriptEditorRadioButton.AutoSize = true;
            UseInternalScriptEditorRadioButton.Location = new Point(12, 20);
            UseInternalScriptEditorRadioButton.Name = "UseInternalScriptEditorRadioButton";
            UseInternalScriptEditorRadioButton.Size = new Size(115, 17);
            UseInternalScriptEditorRadioButton.TabIndex = 0;
            UseInternalScriptEditorRadioButton.TabStop = true;
            UseInternalScriptEditorRadioButton.Text = "Use Internal Editor";
            UseInternalScriptEditorRadioButton.UseVisualStyleBackColor = true;
            // 
            // SoundEditorGroupBox
            // 
            SoundEditorGroupBox.Controls.Add(SoundEditorBrowseButton);
            SoundEditorGroupBox.Controls.Add(SoundEditorTextBox);
            SoundEditorGroupBox.Location = new Point(8, 68);
            SoundEditorGroupBox.Name = "SoundEditorGroupBox";
            SoundEditorGroupBox.Size = new Size(260, 56);
            SoundEditorGroupBox.TabIndex = 1;
            SoundEditorGroupBox.TabStop = false;
            SoundEditorGroupBox.Text = "Sound Editor";
            // 
            // SoundEditorBrowseButton
            // 
            SoundEditorBrowseButton.Location = new Point(221, 19);
            SoundEditorBrowseButton.Name = "SoundEditorBrowseButton";
            SoundEditorBrowseButton.Size = new Size(33, 23);
            SoundEditorBrowseButton.TabIndex = 8;
            SoundEditorBrowseButton.Text = "...";
            SoundEditorBrowseButton.UseVisualStyleBackColor = true;
            // 
            // SoundEditorTextBox
            // 
            SoundEditorTextBox.Location = new Point(12, 20);
            SoundEditorTextBox.Name = "SoundEditorTextBox";
            SoundEditorTextBox.Size = new Size(203, 21);
            SoundEditorTextBox.TabIndex = 7;
            // 
            // ImageEditorGroupBox
            // 
            ImageEditorGroupBox.Controls.Add(ImageEditorBrowseButton);
            ImageEditorGroupBox.Controls.Add(ImageEditorTextBox);
            ImageEditorGroupBox.Location = new Point(8, 6);
            ImageEditorGroupBox.Name = "ImageEditorGroupBox";
            ImageEditorGroupBox.Size = new Size(260, 56);
            ImageEditorGroupBox.TabIndex = 0;
            ImageEditorGroupBox.TabStop = false;
            ImageEditorGroupBox.Text = "Image Editor";
            // 
            // ImageEditorBrowseButton
            // 
            ImageEditorBrowseButton.Location = new Point(221, 19);
            ImageEditorBrowseButton.Name = "ImageEditorBrowseButton";
            ImageEditorBrowseButton.Size = new Size(33, 21);
            ImageEditorBrowseButton.TabIndex = 3;
            ImageEditorBrowseButton.Text = "...";
            ImageEditorBrowseButton.UseVisualStyleBackColor = true;
            // 
            // ImageEditorTextBox
            // 
            ImageEditorTextBox.Location = new Point(12, 20);
            ImageEditorTextBox.Name = "ImageEditorTextBox";
            ImageEditorTextBox.Size = new Size(203, 21);
            ImageEditorTextBox.TabIndex = 2;
            // 
            // EmulatorTabPage
            // 
            EmulatorTabPage.Controls.Add(EmulatorInfoLabel);
            EmulatorTabPage.Controls.Add(CustomEmulatorBrowseButton);
            EmulatorTabPage.Controls.Add(CustomEmulatorTextBox);
            EmulatorTabPage.Controls.Add(RunCustomExecutableRadioButton);
            EmulatorTabPage.Controls.Add(UseNOGBARadioButton);
            EmulatorTabPage.Location = new Point(4, 22);
            EmulatorTabPage.Name = "EmulatorTabPage";
            EmulatorTabPage.Padding = new Padding(3);
            EmulatorTabPage.Size = new Size(276, 283);
            EmulatorTabPage.TabIndex = 2;
            EmulatorTabPage.Text = "Emulator";
            EmulatorTabPage.UseVisualStyleBackColor = true;
            // 
            // EmulatorInfoLabel
            // 
            EmulatorInfoLabel.BackColor = SystemColors.Control;
            EmulatorInfoLabel.BorderStyle = BorderStyle.Fixed3D;
            EmulatorInfoLabel.Location = new Point(18, 16);
            EmulatorInfoLabel.Name = "EmulatorInfoLabel";
            EmulatorInfoLabel.Padding = new Padding(4);
            EmulatorInfoLabel.Size = new Size(242, 44);
            EmulatorInfoLabel.TabIndex = 6;
            EmulatorInfoLabel.Text = "You can test your game accurately on your computer to save time with an Emulator." + "";
            EmulatorInfoLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CustomEmulatorBrowseButton
            // 
            CustomEmulatorBrowseButton.Location = new Point(227, 120);
            CustomEmulatorBrowseButton.Name = "CustomEmulatorBrowseButton";
            CustomEmulatorBrowseButton.Size = new Size(33, 23);
            CustomEmulatorBrowseButton.TabIndex = 5;
            CustomEmulatorBrowseButton.Text = "...";
            CustomEmulatorBrowseButton.UseVisualStyleBackColor = true;
            // 
            // CustomEmulatorTextBox
            // 
            CustomEmulatorTextBox.Location = new Point(18, 122);
            CustomEmulatorTextBox.Name = "CustomEmulatorTextBox";
            CustomEmulatorTextBox.Size = new Size(203, 21);
            CustomEmulatorTextBox.TabIndex = 4;
            // 
            // RunCustomExecutableRadioButton
            // 
            RunCustomExecutableRadioButton.AutoSize = true;
            RunCustomExecutableRadioButton.Location = new Point(18, 99);
            RunCustomExecutableRadioButton.Name = "RunCustomExecutableRadioButton";
            RunCustomExecutableRadioButton.Size = new Size(139, 17);
            RunCustomExecutableRadioButton.TabIndex = 2;
            RunCustomExecutableRadioButton.Text = "Run Custom Executable";
            RunCustomExecutableRadioButton.UseVisualStyleBackColor = true;
            // 
            // UseNOGBARadioButton
            // 
            UseNOGBARadioButton.AutoSize = true;
            UseNOGBARadioButton.Checked = true;
            UseNOGBARadioButton.Location = new Point(18, 74);
            UseNOGBARadioButton.Name = "UseNOGBARadioButton";
            UseNOGBARadioButton.Size = new Size(139, 17);
            UseNOGBARadioButton.TabIndex = 1;
            UseNOGBARadioButton.TabStop = true;
            UseNOGBARadioButton.Text = "Use NO$GBA (Included)";
            UseNOGBARadioButton.UseVisualStyleBackColor = true;
            // 
            // StartupTabPage
            // 
            StartupTabPage.Controls.Add(ShowNewsCheckBox);
            StartupTabPage.Controls.Add(OpenLastProjectCheckBox);
            StartupTabPage.Location = new Point(4, 22);
            StartupTabPage.Name = "StartupTabPage";
            StartupTabPage.Padding = new Padding(3);
            StartupTabPage.Size = new Size(276, 283);
            StartupTabPage.TabIndex = 3;
            StartupTabPage.Text = "Startup";
            StartupTabPage.UseVisualStyleBackColor = true;
            // 
            // ShowNewsCheckBox
            // 
            ShowNewsCheckBox.AutoSize = true;
            ShowNewsCheckBox.Location = new Point(16, 39);
            ShowNewsCheckBox.Name = "ShowNewsCheckBox";
            ShowNewsCheckBox.Size = new Size(182, 17);
            ShowNewsCheckBox.TabIndex = 1;
            ShowNewsCheckBox.Text = "Show DSGM Newsline on Startup";
            ShowNewsCheckBox.UseVisualStyleBackColor = true;
            // 
            // DOkayButton
            // 
            DOkayButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            DOkayButton.Location = new Point(184, 311);
            DOkayButton.Name = "DOkayButton";
            DOkayButton.Size = new Size(96, 28);
            DOkayButton.TabIndex = 2;
            DOkayButton.Text = "OK";
            DOkayButton.UseVisualStyleBackColor = true;
            // 
            // ShrinkActionsListChecker
            // 
            ShrinkActionsListChecker.AutoSize = true;
            ShrinkActionsListChecker.Location = new Point(16, 233);
            ShrinkActionsListChecker.Name = "ShrinkActionsListChecker";
            ShrinkActionsListChecker.Size = new Size(112, 17);
            ShrinkActionsListChecker.TabIndex = 5;
            ShrinkActionsListChecker.Text = "Shrink Actions List";
            ShrinkActionsListChecker.UseVisualStyleBackColor = true;
            // 
            // Options
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 344);
            Controls.Add(DOkayButton);
            Controls.Add(MainTabControl);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Options";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Options";
            MainTabControl.ResumeLayout(false);
            GeneralTabPage.ResumeLayout(false);
            GeneralTabPage.PerformLayout();
            RoomsGroupBox.ResumeLayout(false);
            RoomsGroupBox.PerformLayout();
            CodeEditorGroupBox.ResumeLayout(false);
            CodeEditorGroupBox.PerformLayout();
            EditorsTabPage.ResumeLayout(false);
            ScriptEditorGroupBox.ResumeLayout(false);
            ScriptEditorGroupBox.PerformLayout();
            SoundEditorGroupBox.ResumeLayout(false);
            SoundEditorGroupBox.PerformLayout();
            ImageEditorGroupBox.ResumeLayout(false);
            ImageEditorGroupBox.PerformLayout();
            EmulatorTabPage.ResumeLayout(false);
            EmulatorTabPage.PerformLayout();
            StartupTabPage.ResumeLayout(false);
            StartupTabPage.PerformLayout();
            Load += new EventHandler(Options_Load);
            ResumeLayout(false);

        }
        internal CheckBox OpenLastProjectCheckBox;
        internal TabControl MainTabControl;
        internal TabPage GeneralTabPage;
        internal TabPage EditorsTabPage;
        internal Button DOkayButton;
        internal GroupBox ImageEditorGroupBox;
        internal GroupBox SoundEditorGroupBox;
        internal Button ImageEditorBrowseButton;
        internal TextBox ImageEditorTextBox;
        internal Button SoundEditorBrowseButton;
        internal TextBox SoundEditorTextBox;
        internal GroupBox ScriptEditorGroupBox;
        internal Button ScriptEditorBrowseButton;
        internal TextBox ScriptEditorTextBox;
        internal RadioButton UseExternalScriptEditorRadioButton;
        internal RadioButton UseInternalScriptEditorRadioButton;
        internal CheckBox HighlightCurrentLineCheckBox;
        internal GroupBox CodeEditorGroupBox;
        internal CheckBox MatchBracesCheckBox;
        internal CheckBox TransparentAnimationsCheckBox;
        internal GroupBox RoomsGroupBox;
        internal Label DefaultRoomHeightLabel;
        internal Label DefaultRoomWidthLabel;
        internal TextBox DefaultRoomHeightTB;
        internal TextBox DefaultRoomWidthTB;
        internal TabPage EmulatorTabPage;
        internal RadioButton UseNOGBARadioButton;
        internal RadioButton RunCustomExecutableRadioButton;
        internal Button CustomEmulatorBrowseButton;
        internal TextBox CustomEmulatorTextBox;
        internal Label EmulatorInfoLabel;
        internal TabPage StartupTabPage;
        internal CheckBox ShowNewsCheckBox;
        internal CheckBox CloseNewslineCheckBox;
        internal CheckBox HideOldActionsChecker;
        internal CheckBox ShrinkActionsListChecker;
    }
}