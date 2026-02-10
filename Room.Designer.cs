using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Room : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Room));
            NameLabel = new Label();
            NameTextBox = new TextBox();
            TopHeightLabel = new Label();
            TopWidthLabel = new Label();
            ObjectsTabControl = new TabControl();
            GeneralTab = new TabPage();
            BottomScreenGroupBox = new GroupBox();
            BottomScreenBGDropper = new ComboBox();
            BottomScreenBGDropper.SelectedIndexChanged += new EventHandler(ScreenBGDropper_SelectedIndexChanged);
            BottomHeightDropper = new NumericUpDown();
            BottomHeightDropper.ValueChanged += new EventHandler(BottomHeightDropper_ValueChanged);
            BottomWidthDropper = new NumericUpDown();
            BottomWidthDropper.ValueChanged += new EventHandler(BottomWidthDropper_ValueChanged);
            BottomScreenScrollChecker = new CheckBox();
            BottomWidthLabel = new Label();
            BottomHeightLabel = new Label();
            TopScreenGroupBox = new GroupBox();
            TopHeightDropper = new NumericUpDown();
            TopHeightDropper.ValueChanged += new EventHandler(TopHeightDropper_ValueChanged);
            TopWidthDropper = new NumericUpDown();
            TopWidthDropper.ValueChanged += new EventHandler(TopWidthDropper_ValueChanged);
            TopScreenBGDropper = new ComboBox();
            TopScreenBGDropper.SelectedIndexChanged += new EventHandler(ScreenBGDropper_SelectedIndexChanged);
            TopScreenScrollChecker = new CheckBox();
            DesignTab = new TabPage();
            CursorPositionSnapLabel = new Label();
            CursorPositionLabel = new Label();
            UseRightClickMenuChecker = new CheckBox();
            GroupBox1 = new GroupBox();
            ObjectInfoLabel = new Label();
            ObjectDropper = new ComboBox();
            ObjectDropper.SelectedIndexChanged += new EventHandler(ObjectDropper_SelectedIndexChanged);
            PlotWithLeftClickLabel = new Label();
            SnappingGroupBox = new GroupBox();
            GridColorButton = new Button();
            GridColorButton.Click += new EventHandler(GridColorButton_Click);
            SnapYTextBox = new TextBox();
            SnapYTextBox.TextChanged += new EventHandler(Snappers_TextChanged);
            SnapYLabel = new Label();
            SnapXTextBox = new TextBox();
            SnapXTextBox.TextChanged += new EventHandler(Snappers_TextChanged);
            SnapToGridChecker = new CheckBox();
            SnapToGridChecker.CheckedChanged += new EventHandler(SnapToGridChecker_CheckedChanged);
            SnapXLabel = new Label();
            ShowGridChecker = new CheckBox();
            ShowGridChecker.CheckedChanged += new EventHandler(ShowGridChecker_CheckedChanged);
            TopScreen = new Panel();
            TopScreen.MouseClick += new MouseEventHandler(TopScreen_MouseClick);
            TopScreen.Paint += new PaintEventHandler(TopScreen_Paint);
            TopScreen.Scroll += new ScrollEventHandler(TopScreen_Scroll);
            TopScreen.MouseMove += new MouseEventHandler(Screens_MouseMove);
            BottomScreen = new Panel();
            BottomScreen.MouseClick += new MouseEventHandler(BottomScreen_MouseClick);
            BottomScreen.Paint += new PaintEventHandler(BottomScreen_Paint);
            BottomScreen.Scroll += new ScrollEventHandler(BottomScreen_Scroll);
            BottomScreen.MouseMove += new MouseEventHandler(Screens_MouseMove);
            MainToolStrip = new ToolStrip();
            DAcceptButton = new ToolStripButton();
            DAcceptButton.Click += new EventHandler(DAcceptButton_Click);
            ObjectRightClickMenu = new ContextMenuStrip(components);
            ObjectRightClickMenu.Opening += new System.ComponentModel.CancelEventHandler(ObjectRightClickMenu_Opening);
            DeleteObjectButton = new ToolStripMenuItem();
            DeleteObjectButton.Click += new EventHandler(DeleteObjectButton_Click);
            SetCoOrdinatesButton = new ToolStripMenuItem();
            SetCoOrdinatesButton.Click += new EventHandler(SetCoOrdinatesButton_Click);
            RightClickSep1 = new ToolStripSeparator();
            OpenObjectButton = new ToolStripMenuItem();
            OpenObjectButton.Click += new EventHandler(OpenObjectButton_Click);
            ObjectsTabControl.SuspendLayout();
            GeneralTab.SuspendLayout();
            BottomScreenGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BottomHeightDropper).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BottomWidthDropper).BeginInit();
            TopScreenGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TopHeightDropper).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TopWidthDropper).BeginInit();
            DesignTab.SuspendLayout();
            GroupBox1.SuspendLayout();
            SnappingGroupBox.SuspendLayout();
            MainToolStrip.SuspendLayout();
            ObjectRightClickMenu.SuspendLayout();
            SuspendLayout();
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Location = new Point(6, 8);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(38, 13);
            NameLabel.TabIndex = 3;
            NameLabel.Text = "Name:";
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(7, 24);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(153, 21);
            NameTextBox.TabIndex = 0;
            // 
            // TopHeightLabel
            // 
            TopHeightLabel.AutoSize = true;
            TopHeightLabel.Location = new Point(29, 43);
            TopHeightLabel.Name = "TopHeightLabel";
            TopHeightLabel.Size = new Size(42, 13);
            TopHeightLabel.TabIndex = 4;
            TopHeightLabel.Text = "Height:";
            // 
            // TopWidthLabel
            // 
            TopWidthLabel.AutoSize = true;
            TopWidthLabel.BackColor = Color.Transparent;
            TopWidthLabel.Location = new Point(29, 20);
            TopWidthLabel.Name = "TopWidthLabel";
            TopWidthLabel.Size = new Size(39, 13);
            TopWidthLabel.TabIndex = 3;
            TopWidthLabel.Text = "Width:";
            // 
            // ObjectsTabControl
            // 
            ObjectsTabControl.Controls.Add(GeneralTab);
            ObjectsTabControl.Controls.Add(DesignTab);
            ObjectsTabControl.Dock = DockStyle.Left;
            ObjectsTabControl.Location = new Point(0, 25);
            ObjectsTabControl.Name = "ObjectsTabControl";
            ObjectsTabControl.SelectedIndex = 0;
            ObjectsTabControl.Size = new Size(177, 389);
            ObjectsTabControl.TabIndex = 1;
            // 
            // GeneralTab
            // 
            GeneralTab.Controls.Add(BottomScreenGroupBox);
            GeneralTab.Controls.Add(TopScreenGroupBox);
            GeneralTab.Controls.Add(NameTextBox);
            GeneralTab.Controls.Add(NameLabel);
            GeneralTab.Location = new Point(4, 22);
            GeneralTab.Name = "GeneralTab";
            GeneralTab.Padding = new Padding(3);
            GeneralTab.Size = new Size(169, 363);
            GeneralTab.TabIndex = 0;
            GeneralTab.Text = "General";
            GeneralTab.UseVisualStyleBackColor = true;
            // 
            // BottomScreenGroupBox
            // 
            BottomScreenGroupBox.Controls.Add(BottomScreenBGDropper);
            BottomScreenGroupBox.Controls.Add(BottomHeightDropper);
            BottomScreenGroupBox.Controls.Add(BottomWidthDropper);
            BottomScreenGroupBox.Controls.Add(BottomScreenScrollChecker);
            BottomScreenGroupBox.Controls.Add(BottomWidthLabel);
            BottomScreenGroupBox.Controls.Add(BottomHeightLabel);
            BottomScreenGroupBox.Location = new Point(7, 176);
            BottomScreenGroupBox.Name = "BottomScreenGroupBox";
            BottomScreenGroupBox.Size = new Size(153, 119);
            BottomScreenGroupBox.TabIndex = 2;
            BottomScreenGroupBox.TabStop = false;
            BottomScreenGroupBox.Text = "Bottom Screen";
            // 
            // BottomScreenBGDropper
            // 
            BottomScreenBGDropper.FormattingEnabled = true;
            BottomScreenBGDropper.Location = new Point(9, 68);
            BottomScreenBGDropper.Name = "BottomScreenBGDropper";
            BottomScreenBGDropper.Size = new Size(135, 21);
            BottomScreenBGDropper.TabIndex = 7;
            // 
            // BottomHeightDropper
            // 
            BottomHeightDropper.Location = new Point(104, 41);
            BottomHeightDropper.Maximum = new decimal(new int[] { 4096, 0, 0, 0 });
            BottomHeightDropper.Minimum = new decimal(new int[] { 192, 0, 0, 0 });
            BottomHeightDropper.Name = "BottomHeightDropper";
            BottomHeightDropper.Size = new Size(40, 21);
            BottomHeightDropper.TabIndex = 1;
            BottomHeightDropper.Value = new decimal(new int[] { 192, 0, 0, 0 });
            // 
            // BottomWidthDropper
            // 
            BottomWidthDropper.Location = new Point(104, 18);
            BottomWidthDropper.Maximum = new decimal(new int[] { 4096, 0, 0, 0 });
            BottomWidthDropper.Minimum = new decimal(new int[] { 256, 0, 0, 0 });
            BottomWidthDropper.Name = "BottomWidthDropper";
            BottomWidthDropper.Size = new Size(40, 21);
            BottomWidthDropper.TabIndex = 0;
            BottomWidthDropper.Value = new decimal(new int[] { 256, 0, 0, 0 });
            // 
            // BottomScreenScrollChecker
            // 
            BottomScreenScrollChecker.AutoSize = true;
            BottomScreenScrollChecker.Location = new Point(9, 95);
            BottomScreenScrollChecker.Name = "BottomScreenScrollChecker";
            BottomScreenScrollChecker.Size = new Size(130, 17);
            BottomScreenScrollChecker.TabIndex = 2;
            BottomScreenScrollChecker.Text = "Scroll BG with Camera";
            BottomScreenScrollChecker.UseVisualStyleBackColor = true;
            // 
            // BottomWidthLabel
            // 
            BottomWidthLabel.AutoSize = true;
            BottomWidthLabel.BackColor = Color.Transparent;
            BottomWidthLabel.Location = new Point(29, 20);
            BottomWidthLabel.Name = "BottomWidthLabel";
            BottomWidthLabel.Size = new Size(39, 13);
            BottomWidthLabel.TabIndex = 3;
            BottomWidthLabel.Text = "Width:";
            // 
            // BottomHeightLabel
            // 
            BottomHeightLabel.AutoSize = true;
            BottomHeightLabel.Location = new Point(29, 43);
            BottomHeightLabel.Name = "BottomHeightLabel";
            BottomHeightLabel.Size = new Size(42, 13);
            BottomHeightLabel.TabIndex = 4;
            BottomHeightLabel.Text = "Height:";
            // 
            // TopScreenGroupBox
            // 
            TopScreenGroupBox.Controls.Add(TopHeightDropper);
            TopScreenGroupBox.Controls.Add(TopWidthDropper);
            TopScreenGroupBox.Controls.Add(TopScreenBGDropper);
            TopScreenGroupBox.Controls.Add(TopScreenScrollChecker);
            TopScreenGroupBox.Controls.Add(TopWidthLabel);
            TopScreenGroupBox.Controls.Add(TopHeightLabel);
            TopScreenGroupBox.Location = new Point(7, 51);
            TopScreenGroupBox.Name = "TopScreenGroupBox";
            TopScreenGroupBox.Size = new Size(153, 119);
            TopScreenGroupBox.TabIndex = 1;
            TopScreenGroupBox.TabStop = false;
            TopScreenGroupBox.Text = "Top Screen";
            // 
            // TopHeightDropper
            // 
            TopHeightDropper.Location = new Point(104, 41);
            TopHeightDropper.Maximum = new decimal(new int[] { 4096, 0, 0, 0 });
            TopHeightDropper.Minimum = new decimal(new int[] { 192, 0, 0, 0 });
            TopHeightDropper.Name = "TopHeightDropper";
            TopHeightDropper.Size = new Size(40, 21);
            TopHeightDropper.TabIndex = 2;
            TopHeightDropper.Value = new decimal(new int[] { 192, 0, 0, 0 });
            // 
            // TopWidthDropper
            // 
            TopWidthDropper.Location = new Point(104, 18);
            TopWidthDropper.Maximum = new decimal(new int[] { 4096, 0, 0, 0 });
            TopWidthDropper.Minimum = new decimal(new int[] { 256, 0, 0, 0 });
            TopWidthDropper.Name = "TopWidthDropper";
            TopWidthDropper.Size = new Size(40, 21);
            TopWidthDropper.TabIndex = 1;
            TopWidthDropper.Value = new decimal(new int[] { 256, 0, 0, 0 });
            // 
            // TopScreenBGDropper
            // 
            TopScreenBGDropper.FormattingEnabled = true;
            TopScreenBGDropper.Location = new Point(9, 68);
            TopScreenBGDropper.Name = "TopScreenBGDropper";
            TopScreenBGDropper.Size = new Size(135, 21);
            TopScreenBGDropper.TabIndex = 4;
            // 
            // TopScreenScrollChecker
            // 
            TopScreenScrollChecker.AutoSize = true;
            TopScreenScrollChecker.Location = new Point(9, 95);
            TopScreenScrollChecker.Name = "TopScreenScrollChecker";
            TopScreenScrollChecker.Size = new Size(130, 17);
            TopScreenScrollChecker.TabIndex = 0;
            TopScreenScrollChecker.Text = "Scroll BG with Camera";
            TopScreenScrollChecker.UseVisualStyleBackColor = true;
            // 
            // DesignTab
            // 
            DesignTab.Controls.Add(CursorPositionSnapLabel);
            DesignTab.Controls.Add(CursorPositionLabel);
            DesignTab.Controls.Add(UseRightClickMenuChecker);
            DesignTab.Controls.Add(GroupBox1);
            DesignTab.Controls.Add(ObjectDropper);
            DesignTab.Controls.Add(PlotWithLeftClickLabel);
            DesignTab.Controls.Add(SnappingGroupBox);
            DesignTab.Location = new Point(4, 22);
            DesignTab.Name = "DesignTab";
            DesignTab.Padding = new Padding(3);
            DesignTab.Size = new Size(169, 363);
            DesignTab.TabIndex = 3;
            DesignTab.Text = "Design";
            DesignTab.UseVisualStyleBackColor = true;
            // 
            // CursorPositionSnapLabel
            // 
            CursorPositionSnapLabel.AutoSize = true;
            CursorPositionSnapLabel.BackColor = Color.Transparent;
            CursorPositionSnapLabel.Location = new Point(8, 321);
            CursorPositionSnapLabel.Name = "CursorPositionSnapLabel";
            CursorPositionSnapLabel.Size = new Size(117, 13);
            CursorPositionSnapLabel.TabIndex = 6;
            CursorPositionSnapLabel.Text = "Cursor Position (snap):";
            // 
            // CursorPositionLabel
            // 
            CursorPositionLabel.AutoSize = true;
            CursorPositionLabel.BackColor = Color.Transparent;
            CursorPositionLabel.Location = new Point(42, 308);
            CursorPositionLabel.Name = "CursorPositionLabel";
            CursorPositionLabel.Size = new Size(83, 13);
            CursorPositionLabel.TabIndex = 5;
            CursorPositionLabel.Text = "Cursor Position:";
            // 
            // UseRightClickMenuChecker
            // 
            UseRightClickMenuChecker.AutoSize = true;
            UseRightClickMenuChecker.CheckAlign = ContentAlignment.MiddleRight;
            UseRightClickMenuChecker.Location = new Point(37, 341);
            UseRightClickMenuChecker.Name = "UseRightClickMenuChecker";
            UseRightClickMenuChecker.Size = new Size(126, 17);
            UseRightClickMenuChecker.TabIndex = 0;
            UseRightClickMenuChecker.Text = "Use Right-Click Menu";
            UseRightClickMenuChecker.UseVisualStyleBackColor = true;
            // 
            // GroupBox1
            // 
            GroupBox1.Controls.Add(ObjectInfoLabel);
            GroupBox1.Location = new Point(8, 214);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(155, 91);
            GroupBox1.TabIndex = 4;
            GroupBox1.TabStop = false;
            GroupBox1.Text = "Object(s) under Mouse";
            // 
            // ObjectInfoLabel
            // 
            ObjectInfoLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            ObjectInfoLabel.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ObjectInfoLabel.Location = new Point(7, 17);
            ObjectInfoLabel.Name = "ObjectInfoLabel";
            ObjectInfoLabel.Size = new Size(142, 64);
            ObjectInfoLabel.TabIndex = 0;
            ObjectInfoLabel.Text = "...";
            // 
            // ObjectDropper
            // 
            ObjectDropper.FormattingEnabled = true;
            ObjectDropper.Location = new Point(8, 28);
            ObjectDropper.Name = "ObjectDropper";
            ObjectDropper.Size = new Size(155, 21);
            ObjectDropper.TabIndex = 2;
            // 
            // PlotWithLeftClickLabel
            // 
            PlotWithLeftClickLabel.AutoSize = true;
            PlotWithLeftClickLabel.Location = new Point(8, 12);
            PlotWithLeftClickLabel.Name = "PlotWithLeftClickLabel";
            PlotWithLeftClickLabel.Size = new Size(77, 13);
            PlotWithLeftClickLabel.TabIndex = 1;
            PlotWithLeftClickLabel.Text = "Object to Plot:";
            // 
            // SnappingGroupBox
            // 
            SnappingGroupBox.Controls.Add(GridColorButton);
            SnappingGroupBox.Controls.Add(SnapYTextBox);
            SnappingGroupBox.Controls.Add(SnapYLabel);
            SnappingGroupBox.Controls.Add(SnapXTextBox);
            SnappingGroupBox.Controls.Add(SnapToGridChecker);
            SnappingGroupBox.Controls.Add(SnapXLabel);
            SnappingGroupBox.Controls.Add(ShowGridChecker);
            SnappingGroupBox.Location = new Point(8, 64);
            SnappingGroupBox.Name = "SnappingGroupBox";
            SnappingGroupBox.Size = new Size(155, 145);
            SnappingGroupBox.TabIndex = 0;
            SnappingGroupBox.TabStop = false;
            SnappingGroupBox.Text = "Snap Settings";
            // 
            // GridColorButton
            // 
            GridColorButton.Image = DS_Game_Maker.My.Resources.Resources.color;
            GridColorButton.ImageAlign = ContentAlignment.MiddleLeft;
            GridColorButton.Location = new Point(50, 109);
            GridColorButton.Name = "GridColorButton";
            GridColorButton.Size = new Size(99, 28);
            GridColorButton.TabIndex = 0;
            GridColorButton.Text = "      Set Grid Color";
            GridColorButton.TextAlign = ContentAlignment.MiddleLeft;
            GridColorButton.UseVisualStyleBackColor = true;
            // 
            // SnapYTextBox
            // 
            SnapYTextBox.Location = new Point(97, 82);
            SnapYTextBox.Name = "SnapYTextBox";
            SnapYTextBox.Size = new Size(52, 21);
            SnapYTextBox.TabIndex = 3;
            // 
            // SnapYLabel
            // 
            SnapYLabel.AutoSize = true;
            SnapYLabel.Location = new Point(46, 85);
            SnapYLabel.Name = "SnapYLabel";
            SnapYLabel.Size = new Size(44, 13);
            SnapYLabel.TabIndex = 2;
            SnapYLabel.Text = "Snap Y:";
            // 
            // SnapXTextBox
            // 
            SnapXTextBox.Location = new Point(97, 57);
            SnapXTextBox.Name = "SnapXTextBox";
            SnapXTextBox.Size = new Size(52, 21);
            SnapXTextBox.TabIndex = 0;
            // 
            // SnapToGridChecker
            // 
            SnapToGridChecker.AutoSize = true;
            SnapToGridChecker.Location = new Point(10, 20);
            SnapToGridChecker.Name = "SnapToGridChecker";
            SnapToGridChecker.Size = new Size(125, 17);
            SnapToGridChecker.TabIndex = 0;
            SnapToGridChecker.Text = "Snap Objects to Grid";
            SnapToGridChecker.UseVisualStyleBackColor = true;
            // 
            // SnapXLabel
            // 
            SnapXLabel.AutoSize = true;
            SnapXLabel.Location = new Point(46, 60);
            SnapXLabel.Name = "SnapXLabel";
            SnapXLabel.Size = new Size(44, 13);
            SnapXLabel.TabIndex = 0;
            SnapXLabel.Text = "Snap X:";
            // 
            // ShowGridChecker
            // 
            ShowGridChecker.AutoSize = true;
            ShowGridChecker.Location = new Point(10, 40);
            ShowGridChecker.Name = "ShowGridChecker";
            ShowGridChecker.Size = new Size(74, 17);
            ShowGridChecker.TabIndex = 1;
            ShowGridChecker.Text = "Show Grid";
            ShowGridChecker.UseVisualStyleBackColor = true;
            // 
            // TopScreen
            // 
            TopScreen.BackColor = Color.Black;
            TopScreen.Location = new Point(178, 25);
            TopScreen.Name = "TopScreen";
            TopScreen.Size = new Size(588, 192);
            TopScreen.TabIndex = 1;
            // 
            // BottomScreen
            // 
            BottomScreen.BackColor = Color.Black;
            BottomScreen.Location = new Point(178, 221);
            BottomScreen.Name = "BottomScreen";
            BottomScreen.Size = new Size(588, 192);
            BottomScreen.TabIndex = 2;
            // 
            // MainToolStrip
            // 
            MainToolStrip.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainToolStrip.Items.AddRange(new ToolStripItem[] { DAcceptButton });
            MainToolStrip.Location = new Point(0, 0);
            MainToolStrip.Name = "MainToolStrip";
            MainToolStrip.Size = new Size(434, 25);
            MainToolStrip.TabIndex = 0;
            // 
            // DAcceptButton
            // 
            DAcceptButton.Image = DS_Game_Maker.My.Resources.Resources.AcceptIcon;
            DAcceptButton.ImageTransparentColor = Color.Magenta;
            DAcceptButton.Name = "DAcceptButton";
            DAcceptButton.Size = new Size(60, 22);
            DAcceptButton.Text = "Accept";
            // 
            // ObjectRightClickMenu
            // 
            ObjectRightClickMenu.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ObjectRightClickMenu.Items.AddRange(new ToolStripItem[] { DeleteObjectButton, OpenObjectButton, RightClickSep1, SetCoOrdinatesButton });
            ObjectRightClickMenu.Name = "ObjectRightClickMenu";
            ObjectRightClickMenu.Size = new Size(166, 98);
            // 
            // DeleteObjectButton
            // 
            DeleteObjectButton.Image = DS_Game_Maker.My.Resources.Resources.DeleteIcon;
            DeleteObjectButton.Name = "DeleteObjectButton";
            DeleteObjectButton.Size = new Size(165, 22);
            DeleteObjectButton.Text = "Delete";
            // 
            // SetCoOrdinatesButton
            // 
            SetCoOrdinatesButton.Name = "SetCoOrdinatesButton";
            SetCoOrdinatesButton.Size = new Size(165, 22);
            SetCoOrdinatesButton.Text = "Move to Position...";
            // 
            // RightClickSep1
            // 
            RightClickSep1.Name = "RightClickSep1";
            RightClickSep1.Size = new Size(162, 6);
            // 
            // OpenObjectButton
            // 
            OpenObjectButton.Image = DS_Game_Maker.My.Resources.Resources.OpenObjectIcon;
            OpenObjectButton.Name = "OpenObjectButton";
            OpenObjectButton.Size = new Size(165, 22);
            OpenObjectButton.Text = "Open Object";
            // 
            // Room
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(224, 224, 224);
            ClientSize = new Size(434, 414);
            Controls.Add(ObjectsTabControl);
            Controls.Add(BottomScreen);
            Controls.Add(MainToolStrip);
            Controls.Add(TopScreen);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(450, 451);
            Name = "Room";
            ObjectsTabControl.ResumeLayout(false);
            GeneralTab.ResumeLayout(false);
            GeneralTab.PerformLayout();
            BottomScreenGroupBox.ResumeLayout(false);
            BottomScreenGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)BottomHeightDropper).EndInit();
            ((System.ComponentModel.ISupportInitialize)BottomWidthDropper).EndInit();
            TopScreenGroupBox.ResumeLayout(false);
            TopScreenGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TopHeightDropper).EndInit();
            ((System.ComponentModel.ISupportInitialize)TopWidthDropper).EndInit();
            DesignTab.ResumeLayout(false);
            DesignTab.PerformLayout();
            GroupBox1.ResumeLayout(false);
            SnappingGroupBox.ResumeLayout(false);
            SnappingGroupBox.PerformLayout();
            MainToolStrip.ResumeLayout(false);
            MainToolStrip.PerformLayout();
            ObjectRightClickMenu.ResumeLayout(false);
            Load += new EventHandler(Room_Load);
            Resize += new EventHandler(Room_Resize);
            ResumeLayout(false);
            PerformLayout();

        }
        internal Label NameLabel;
        internal TextBox NameTextBox;
        internal Label TopHeightLabel;
        internal Label TopWidthLabel;
        internal TabControl ObjectsTabControl;
        internal TabPage GeneralTab;
        internal ToolStrip MainToolStrip;
        internal ToolStripButton DAcceptButton;
        internal GroupBox TopScreenGroupBox;
        internal GroupBox BottomScreenGroupBox;
        internal Label BottomWidthLabel;
        internal Label BottomHeightLabel;
        internal CheckBox TopScreenScrollChecker;
        internal CheckBox BottomScreenScrollChecker;
        internal ComboBox ObjectDropper;
        internal Label PlotWithLeftClickLabel;
        internal NumericUpDown BottomHeightDropper;
        internal NumericUpDown BottomWidthDropper;
        internal NumericUpDown TopHeightDropper;
        internal NumericUpDown TopWidthDropper;
        internal ComboBox BottomScreenBGDropper;
        internal ComboBox TopScreenBGDropper;
        private Panel TopScreen;
        private Panel BottomScreen;
        internal TabPage DesignTab;
        internal CheckBox ShowGridChecker;
        internal CheckBox SnapToGridChecker;
        internal Label SnapXLabel;
        internal GroupBox SnappingGroupBox;
        internal TextBox SnapYTextBox;
        internal Label SnapYLabel;
        internal TextBox SnapXTextBox;
        internal Button GridColorButton;
        internal GroupBox GroupBox1;
        internal Label ObjectInfoLabel;
        internal CheckBox UseRightClickMenuChecker;
        internal ContextMenuStrip ObjectRightClickMenu;
        internal ToolStripMenuItem DeleteObjectButton;
        internal ToolStripSeparator RightClickSep1;
        internal ToolStripMenuItem OpenObjectButton;
        internal ToolStripMenuItem SetCoOrdinatesButton;
        internal Label CursorPositionSnapLabel;
        internal Label CursorPositionLabel;
    }
}