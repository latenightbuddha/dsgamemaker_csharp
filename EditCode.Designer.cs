using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class EditCode : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(EditCode));
            MainTextBox = new ScintillaNET.Scintilla();
            MainTextBox.KeyDown += new KeyEventHandler(MainTextBox_KeyDown);
            MainTextBox.MouseClick += new MouseEventHandler(MainTextBox_KeyDown);
            MainTextBox.TextChanged += new EventHandler(MainTextBox_KeyDown);
            MainTextBox.CharAdded += new EventHandler<ScintillaNET.CharAddedEventArgs>(MainTextBox_CharAdded);
            MainToolStrip = new ToolStrip();
            DAcceptButton = new ToolStripButton();
            DAcceptButton.Click += new EventHandler(DAcceptButton_Click);
            ToolStripSep1 = new ToolStripSeparator();
            UndoButton = new ToolStripButton();
            UndoButton.Click += new EventHandler(UndoButton_Click);
            RedoButton = new ToolStripButton();
            RedoButton.Click += new EventHandler(RedoButton_Click);
            ToolStripSeparator1 = new ToolStripSeparator();
            LoadInButton = new ToolStripButton();
            LoadInButton.Click += new EventHandler(LoadInButton_Click);
            SaveOutButton = new ToolStripButton();
            SaveOutButton.Click += new EventHandler(SaveOutButton_Click);
            MainStatusStrip = new StatusStrip();
            InfoLabel = new ToolStripStatusLabel();
            //((System.ComponentModel.ISupportInitialize)MainTextBox).BeginInit();
            MainToolStrip.SuspendLayout();
            MainStatusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // MainTextBox
            // 
            //MainTextBox.ConfigurationManager.Language = "vbscript";
            MainTextBox.Dock = DockStyle.Fill;
            //MainTextBox.IsBraceMatching = true;
            MainTextBox.Location = new Point(0, 25);
            MainTextBox.Margins.Left = 0;
            //MainTextBox.Margins.Margin0.Width = 32;
            MainTextBox.Margins.Right = 0;
            MainTextBox.Name = "MainTextBox";
            //MainTextBox.Scrolling.HorizontalWidth = -1;
            MainTextBox.Size = new Size(464, 275);
            //MainTextBox.Styles.BraceBad.FontName = "Verdana";
            //MainTextBox.Styles.BraceLight.FontName = "Verdana";
            //MainTextBox.Styles.CallTip.FontName = "Tahoma";
            //MainTextBox.Styles.CallTip.Size = 8.25f;
            //MainTextBox.Styles.ControlChar.FontName = "Verdana";
            //MainTextBox.Styles.Default.FontName = "Verdana";
            //MainTextBox.Styles.IndentGuide.FontName = "Verdana";
            //MainTextBox.Styles.LastPredefined.FontName = "Verdana";
            //MainTextBox.Styles.LineNumber.FontName = "Verdana";
            //MainTextBox.Styles.Max.FontName = "Verdana";
            MainTextBox.TabIndex = 6;
            // 
            // MainToolStrip
            // 
            MainToolStrip.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainToolStrip.Items.AddRange(new ToolStripItem[] { DAcceptButton, ToolStripSep1, UndoButton, RedoButton, ToolStripSeparator1, LoadInButton, SaveOutButton });
            MainToolStrip.Location = new Point(0, 0);
            MainToolStrip.Name = "MainToolStrip";
            MainToolStrip.Size = new Size(464, 25);
            MainToolStrip.TabIndex = 7;
            // 
            // DAcceptButton
            // 
            DAcceptButton.Image = Properties.Resources.AcceptIcon;
            DAcceptButton.ImageTransparentColor = Color.Magenta;
            DAcceptButton.Name = "DAcceptButton";
            DAcceptButton.Size = new Size(60, 22);
            DAcceptButton.Text = "Accept";
            // 
            // ToolStripSep1
            // 
            ToolStripSep1.Name = "ToolStripSep1";
            ToolStripSep1.Size = new Size(6, 25);
            // 
            // UndoButton
            // 
            UndoButton.Image = Properties.Resources.UndoIcon;
            UndoButton.ImageTransparentColor = Color.Magenta;
            UndoButton.Name = "UndoButton";
            UndoButton.Size = new Size(55, 22);
            UndoButton.Text = " Undo";
            UndoButton.ToolTipText = "Undo";
            // 
            // RedoButton
            // 
            RedoButton.Image = Properties.Resources.RedoIcon;
            RedoButton.ImageTransparentColor = Color.Magenta;
            RedoButton.Name = "RedoButton";
            RedoButton.Size = new Size(55, 22);
            RedoButton.Text = "Redo ";
            RedoButton.TextImageRelation = TextImageRelation.TextBeforeImage;
            RedoButton.ToolTipText = "Redo";
            // 
            // ToolStripSeparator1
            // 
            ToolStripSeparator1.Name = "ToolStripSeparator1";
            ToolStripSeparator1.Size = new Size(6, 25);
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
            MainStatusStrip.Items.AddRange(new ToolStripItem[] { InfoLabel });
            MainStatusStrip.Location = new Point(0, 300);
            MainStatusStrip.Name = "MainStatusStrip";
            MainStatusStrip.Size = new Size(464, 22);
            MainStatusStrip.TabIndex = 8;
            MainStatusStrip.Text = "StatusStrip1";
            // 
            // InfoLabel
            // 
            InfoLabel.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            InfoLabel.Name = "InfoLabel";
            InfoLabel.Size = new Size(0, 17);
            // 
            // EditCode
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(464, 322);
            Controls.Add(MainTextBox);
            Controls.Add(MainStatusStrip);
            Controls.Add(MainToolStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "EditCode";
            Text = "Code Editor";
            //((System.ComponentModel.ISupportInitialize)MainTextBox).EndInit();
            MainToolStrip.ResumeLayout(false);
            MainToolStrip.PerformLayout();
            MainStatusStrip.ResumeLayout(false);
            MainStatusStrip.PerformLayout();
            Load += new EventHandler(EditCode_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal ScintillaNET.Scintilla MainTextBox;
        internal ToolStrip MainToolStrip;
        internal StatusStrip MainStatusStrip;
        internal ToolStripButton DAcceptButton;
        internal ToolStripSeparator ToolStripSep1;
        internal ToolStripButton UndoButton;
        internal ToolStripButton RedoButton;
        internal ToolStripStatusLabel InfoLabel;
        internal ToolStripSeparator ToolStripSeparator1;
        internal ToolStripButton LoadInButton;
        internal ToolStripButton SaveOutButton;
    }
}