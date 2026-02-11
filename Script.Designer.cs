using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Script : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Script));
            MainToolStrip = new ToolStrip();
            DAcceptButton = new ToolStripButton();
            DAcceptButton.Click += new EventHandler(DAcceptButton_Click);
            ToolSep1 = new ToolStripSeparator();
            UndoButton = new ToolStripButton();
            UndoButton.Click += new EventHandler(UndoButton_Click);
            RedoButton = new ToolStripButton();
            RedoButton.Click += new EventHandler(RedoButton_Click);
            ToolSep3 = new ToolStripSeparator();
            LoadInButton = new ToolStripButton();
            LoadInButton.Click += new EventHandler(LoadInButton_Click);
            SaveOutButton = new ToolStripButton();
            SaveOutButton.Click += new EventHandler(SaveOutButton_Click);
            MainStatusStrip = new StatusStrip();
            StatsLabel = new ToolStripStatusLabel();
            SpacesLabel = new ToolStripStatusLabel();
            FunctionLabel = new ToolStripStatusLabel();
            MainTextBox = new ScintillaNet.Scintilla();
            MainTextBox.KeyDown += new KeyEventHandler(MainTextBox_LineStatCaller);
            MainTextBox.MouseClick += new MouseEventHandler(MainTextBox_LineStatCaller);
            MainTextBox.TextChanged += new EventHandler<EventArgs>(MainTextBox_LineStatCaller);
            MainTextBox.KeyUp += new KeyEventHandler(MainTextBox_KeyUp);
            MainTextBox.CharAdded += new EventHandler<ScintillaNet.CharAddedEventArgs>(MainTextBox_CharAdded);
            SidePanel = new Panel();
            ParseDBASChecker = new CheckBox();
            ArgumentsList = new ListBox();
            ArgumentsList.DrawItem += new DrawItemEventHandler(ArgumentsList_DrawItem);
            ArgumentsList.MeasureItem += new MeasureItemEventHandler(ArgumentsList_MeasureItem);
            EditArgumentButton = new Button();
            EditArgumentButton.Click += new EventHandler(EditArgumentButton_Click);
            AddArgumentButton = new Button();
            AddArgumentButton.Click += new EventHandler(AddArgumentButton_Click);
            DeleteArgumentButton = new Button();
            DeleteArgumentButton.Click += new EventHandler(DeleteArgumentButton_Click);
            InsertIntoCodeButton = new Button();
            InsertIntoCodeButton.Click += new EventHandler(InsertIntoCodeButton_Click);
            ArgumentsLabel = new Label();
            NameLabel = new Label();
            NameTextBox = new TextBox();
            NameTextBox.TextChanged += new EventHandler(NameTextBox_TextChanged);
            MainToolStrip.SuspendLayout();
            MainStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MainTextBox).BeginInit();
            SidePanel.SuspendLayout();
            SuspendLayout();
            // 
            // MainToolStrip
            // 
            MainToolStrip.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainToolStrip.Items.AddRange(new ToolStripItem[] { DAcceptButton, ToolSep1, UndoButton, RedoButton, ToolSep3, LoadInButton, SaveOutButton });
            MainToolStrip.Location = new Point(0, 0);
            MainToolStrip.Name = "MainToolStrip";
            MainToolStrip.Size = new Size(544, 25);
            MainToolStrip.TabIndex = 0;
            MainToolStrip.Text = "ToolStrip1";
            // 
            // DAcceptButton
            // 
            DAcceptButton.Image = Properties.Resources.AcceptIcon;
            DAcceptButton.ImageTransparentColor = Color.Magenta;
            DAcceptButton.Name = "DAcceptButton";
            DAcceptButton.Size = new Size(60, 22);
            DAcceptButton.Text = "Accept";
            // 
            // ToolSep1
            // 
            ToolSep1.Name = "ToolSep1";
            ToolSep1.Size = new Size(6, 25);
            // 
            // UndoButton
            // 
            UndoButton.Image = Properties.Resources.UndoIcon;
            UndoButton.ImageTransparentColor = Color.Magenta;
            UndoButton.Name = "UndoButton";
            UndoButton.Size = new Size(55, 22);
            UndoButton.Text = " Undo";
            // 
            // RedoButton
            // 
            RedoButton.Image = Properties.Resources.RedoIcon;
            RedoButton.ImageTransparentColor = Color.Magenta;
            RedoButton.Name = "RedoButton";
            RedoButton.Size = new Size(55, 22);
            RedoButton.Text = "Redo ";
            // 
            // ToolSep3
            // 
            ToolSep3.Name = "ToolSep3";
            ToolSep3.Size = new Size(6, 25);
            // 
            // LoadInButton
            // 
            LoadInButton.Image = Properties.Resources.OpenIcon;
            LoadInButton.ImageTransparentColor = Color.Magenta;
            LoadInButton.Name = "LoadInButton";
            LoadInButton.Size = new Size(75, 22);
            LoadInButton.Text = "Load In...";
            // 
            // SaveOutButton
            // 
            SaveOutButton.Image = Properties.Resources.SaveIcon;
            SaveOutButton.ImageTransparentColor = Color.Magenta;
            SaveOutButton.Name = "SaveOutButton";
            SaveOutButton.Size = new Size(84, 22);
            SaveOutButton.Text = "Save Out...";
            // 
            // MainStatusStrip
            // 
            MainStatusStrip.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainStatusStrip.Items.AddRange(new ToolStripItem[] { StatsLabel, SpacesLabel, FunctionLabel });
            MainStatusStrip.Location = new Point(0, 477);
            MainStatusStrip.Name = "MainStatusStrip";
            MainStatusStrip.Size = new Size(544, 26);
            MainStatusStrip.TabIndex = 1;
            MainStatusStrip.Text = "StatusStrip1";
            // 
            // StatsLabel
            // 
            StatsLabel.Name = "StatsLabel";
            StatsLabel.Padding = new Padding(4);
            StatsLabel.Size = new Size(19, 21);
            StatsLabel.Text = "-";
            // 
            // SpacesLabel
            // 
            SpacesLabel.Name = "SpacesLabel";
            SpacesLabel.Padding = new Padding(4);
            SpacesLabel.Size = new Size(21, 21);
            SpacesLabel.Text = "  ";
            // 
            // FunctionLabel
            // 
            FunctionLabel.AutoSize = false;
            FunctionLabel.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            FunctionLabel.Name = "FunctionLabel";
            FunctionLabel.Padding = new Padding(4);
            FunctionLabel.Size = new Size(300, 21);
            FunctionLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // MainTextBox
            // 
            MainTextBox.ConfigurationManager.Language = "vbscript";
            MainTextBox.Dock = DockStyle.Fill;
            MainTextBox.IsBraceMatching = true;
            MainTextBox.Location = new Point(196, 25);
            MainTextBox.Margins.Margin0.Width = 20;
            MainTextBox.Name = "MainTextBox";
            MainTextBox.Scrolling.HorizontalWidth = 1000;
            MainTextBox.Size = new Size(348, 452);
            MainTextBox.Styles.BraceBad.FontName = "Verdana";
            MainTextBox.Styles.BraceLight.FontName = "Verdana";
            MainTextBox.Styles.ControlChar.FontName = "Verdana";
            MainTextBox.Styles.Default.FontName = "Verdana";
            MainTextBox.Styles.IndentGuide.FontName = "Verdana";
            MainTextBox.Styles.LastPredefined.FontName = "Verdana";
            MainTextBox.Styles.LineNumber.FontName = "Verdana";
            MainTextBox.Styles.Max.FontName = "Verdana";
            MainTextBox.TabIndex = 4;
            // 
            // SidePanel
            // 
            SidePanel.BackColor = Color.FromArgb(219, 219, 219);
            SidePanel.Controls.Add(ParseDBASChecker);
            SidePanel.Controls.Add(ArgumentsList);
            SidePanel.Controls.Add(EditArgumentButton);
            SidePanel.Controls.Add(AddArgumentButton);
            SidePanel.Controls.Add(DeleteArgumentButton);
            SidePanel.Controls.Add(InsertIntoCodeButton);
            SidePanel.Controls.Add(ArgumentsLabel);
            SidePanel.Controls.Add(NameLabel);
            SidePanel.Controls.Add(NameTextBox);
            SidePanel.Dock = DockStyle.Left;
            SidePanel.Location = new Point(0, 25);
            SidePanel.Name = "SidePanel";
            SidePanel.Size = new Size(196, 452);
            SidePanel.TabIndex = 5;
            // 
            // ParseDBASChecker
            // 
            ParseDBASChecker.AutoSize = true;
            ParseDBASChecker.CheckAlign = ContentAlignment.MiddleRight;
            ParseDBASChecker.Location = new Point(96, 259);
            ParseDBASChecker.Name = "ParseDBASChecker";
            ParseDBASChecker.Size = new Size(87, 17);
            ParseDBASChecker.TabIndex = 6;
            ParseDBASChecker.Text = "Parse DBAS?";
            ParseDBASChecker.UseVisualStyleBackColor = true;
            // 
            // ArgumentsList
            // 
            ArgumentsList.DrawMode = DrawMode.OwnerDrawFixed;
            ArgumentsList.FormattingEnabled = true;
            ArgumentsList.ItemHeight = 16;
            ArgumentsList.Location = new Point(13, 78);
            ArgumentsList.Name = "ArgumentsList";
            ArgumentsList.Size = new Size(170, 148);
            ArgumentsList.TabIndex = 6;
            // 
            // EditArgumentButton
            // 
            EditArgumentButton.Image = Properties.Resources.PencilIcon;
            EditArgumentButton.Location = new Point(44, 227);
            EditArgumentButton.Name = "EditArgumentButton";
            EditArgumentButton.Size = new Size(30, 26);
            EditArgumentButton.TabIndex = 14;
            EditArgumentButton.UseVisualStyleBackColor = true;
            // 
            // AddArgumentButton
            // 
            AddArgumentButton.Image = Properties.Resources.PlusIcon;
            AddArgumentButton.Location = new Point(13, 227);
            AddArgumentButton.Name = "AddArgumentButton";
            AddArgumentButton.Size = new Size(30, 26);
            AddArgumentButton.TabIndex = 13;
            AddArgumentButton.UseVisualStyleBackColor = true;
            // 
            // DeleteArgumentButton
            // 
            DeleteArgumentButton.Image = Properties.Resources.DeleteIcon;
            DeleteArgumentButton.Location = new Point(75, 227);
            DeleteArgumentButton.Name = "DeleteArgumentButton";
            DeleteArgumentButton.Size = new Size(30, 26);
            DeleteArgumentButton.TabIndex = 6;
            DeleteArgumentButton.UseVisualStyleBackColor = true;
            // 
            // InsertIntoCodeButton
            // 
            InsertIntoCodeButton.Image = Properties.Resources.ArrowFadeRightIcon;
            InsertIntoCodeButton.ImageAlign = ContentAlignment.MiddleRight;
            InsertIntoCodeButton.Location = new Point(108, 227);
            InsertIntoCodeButton.Name = "InsertIntoCodeButton";
            InsertIntoCodeButton.Size = new Size(76, 26);
            InsertIntoCodeButton.TabIndex = 8;
            InsertIntoCodeButton.Text = "    Insert";
            InsertIntoCodeButton.TextAlign = ContentAlignment.MiddleLeft;
            InsertIntoCodeButton.UseVisualStyleBackColor = true;
            // 
            // ArgumentsLabel
            // 
            ArgumentsLabel.AutoSize = true;
            ArgumentsLabel.Location = new Point(10, 62);
            ArgumentsLabel.Name = "ArgumentsLabel";
            ArgumentsLabel.Size = new Size(63, 13);
            ArgumentsLabel.TabIndex = 4;
            ArgumentsLabel.Text = "Arguments:";
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Location = new Point(10, 13);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(38, 13);
            NameLabel.TabIndex = 2;
            NameLabel.Text = "Name:";
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(13, 29);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(170, 21);
            NameTextBox.TabIndex = 1;
            // 
            // Script
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(544, 503);
            Controls.Add(MainTextBox);
            Controls.Add(SidePanel);
            Controls.Add(MainStatusStrip);
            Controls.Add(MainToolStrip);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(560, 350);
            Name = "Script";
            MainToolStrip.ResumeLayout(false);
            MainToolStrip.PerformLayout();
            MainStatusStrip.ResumeLayout(false);
            MainStatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)MainTextBox).EndInit();
            SidePanel.ResumeLayout(false);
            SidePanel.PerformLayout();
            Load += new EventHandler(Script_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal ToolStrip MainToolStrip;
        internal ToolStripButton DAcceptButton;
        internal StatusStrip MainStatusStrip;
        internal ToolStripStatusLabel StatsLabel;
        internal ToolStripSeparator ToolSep1;
        internal ToolStripButton LoadInButton;
        internal ToolStripButton SaveOutButton;
        internal ScintillaNet.Scintilla MainTextBox;
        internal Panel SidePanel;
        internal TextBox NameTextBox;
        internal Label NameLabel;
        internal ToolStripSeparator ToolSep3;
        internal Label ArgumentsLabel;
        internal Button InsertIntoCodeButton;
        internal Button EditArgumentButton;
        internal Button AddArgumentButton;
        internal Button DeleteArgumentButton;
        internal ToolStripStatusLabel FunctionLabel;
        internal ToolStripStatusLabel SpacesLabel;
        internal ListBox ArgumentsList;
        internal CheckBox ParseDBASChecker;
        internal ToolStripButton UndoButton;
        internal ToolStripButton RedoButton;
    }
}