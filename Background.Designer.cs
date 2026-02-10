using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Background : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Background));
            MainToolStrip = new ToolStrip();
            DAcceptButton = new ToolStripButton();
            DAcceptButton.Click += new EventHandler(DAcceptButton_Click);
            ToolStripSep1 = new ToolStripSeparator();
            EditButton = new ToolStripButton();
            EditButton.Click += new EventHandler(EditButton_Click);
            LoadfromFileButton = new ToolStripButton();
            LoadfromFileButton.Click += new EventHandler(LoadfromFileButton_Click);
            SubPanel = new Panel();
            NameTextBox = new TextBox();
            NameLabel = new Label();
            PreviewPanel = new Panel();
            MainToolStrip.SuspendLayout();
            SubPanel.SuspendLayout();
            SuspendLayout();
            // 
            // MainToolStrip
            // 
            MainToolStrip.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainToolStrip.Items.AddRange(new ToolStripItem[] { DAcceptButton, ToolStripSep1, EditButton, LoadfromFileButton });
            MainToolStrip.Location = new Point(0, 0);
            MainToolStrip.Name = "MainToolStrip";
            MainToolStrip.Size = new Size(256, 25);
            MainToolStrip.TabIndex = 0;
            MainToolStrip.Text = "ToolStrip1";
            // 
            // DAcceptButton
            // 
            DAcceptButton.Image = DS_Game_Maker.My.Resources.Resources.AcceptIcon;
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
            // EditButton
            // 
            EditButton.Image = DS_Game_Maker.My.Resources.Resources.PencilIcon;
            EditButton.ImageTransparentColor = Color.Magenta;
            EditButton.Name = "EditButton";
            EditButton.Size = new Size(45, 22);
            EditButton.Text = "Edit";
            EditButton.ToolTipText = "Edit...";
            // 
            // LoadfromFileButton
            // 
            LoadfromFileButton.Image = DS_Game_Maker.My.Resources.Resources.FolderIcon;
            LoadfromFileButton.ImageTransparentColor = Color.Magenta;
            LoadfromFileButton.Name = "LoadfromFileButton";
            LoadfromFileButton.Size = new Size(94, 22);
            LoadfromFileButton.Text = "Load from File";
            LoadfromFileButton.ToolTipText = "Load from File...";
            // 
            // SubPanel
            // 
            SubPanel.BackColor = Color.Silver;
            SubPanel.Controls.Add(NameTextBox);
            SubPanel.Controls.Add(NameLabel);
            SubPanel.Dock = DockStyle.Bottom;
            SubPanel.Location = new Point(0, 217);
            SubPanel.Name = "SubPanel";
            SubPanel.Size = new Size(256, 28);
            SubPanel.TabIndex = 1;
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(49, 3);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(124, 21);
            NameTextBox.TabIndex = 2;
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Location = new Point(5, 7);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(38, 13);
            NameLabel.TabIndex = 2;
            NameLabel.Text = "Name:";
            // 
            // PreviewPanel
            // 
            PreviewPanel.BackgroundImageLayout = ImageLayout.None;
            PreviewPanel.Dock = DockStyle.Fill;
            PreviewPanel.Location = new Point(0, 25);
            PreviewPanel.Name = "PreviewPanel";
            PreviewPanel.Size = new Size(256, 192);
            PreviewPanel.TabIndex = 2;
            // 
            // Background
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(256, 245);
            Controls.Add(PreviewPanel);
            Controls.Add(SubPanel);
            Controls.Add(MainToolStrip);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(272, 283);
            Name = "Background";
            MainToolStrip.ResumeLayout(false);
            MainToolStrip.PerformLayout();
            SubPanel.ResumeLayout(false);
            SubPanel.PerformLayout();
            Load += new EventHandler(Background_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal ToolStrip MainToolStrip;
        internal ToolStripButton DAcceptButton;
        internal ToolStripSeparator ToolStripSep1;
        internal ToolStripButton EditButton;
        internal ToolStripButton LoadfromFileButton;
        internal Panel SubPanel;
        internal Label NameLabel;
        internal TextBox NameTextBox;
        internal Panel PreviewPanel;
    }
}