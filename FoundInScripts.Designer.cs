using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class FoundInScripts : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(FoundInScripts));
            MainToolStrip = new ToolStrip();
            CloseButton = new ToolStripButton();
            CloseButton.Click += new EventHandler(CloseButton_Click);
            ToolStripSep1 = new ToolStripSeparator();
            RefreshButton = new ToolStripButton();
            RefreshButton.Click += new EventHandler(RefreshButton_Click);
            MainListBox = new ListBox();
            MainListBox.MeasureItem += new MeasureItemEventHandler(MainListBox_MeasureItem);
            MainListBox.DrawItem += new DrawItemEventHandler(MainListBox_DrawItem);
            MainListBox.SelectedIndexChanged += new EventHandler(MainListBox_SelectedIndexChanged);
            MainToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // MainToolStrip
            // 
            MainToolStrip.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainToolStrip.Items.AddRange(new ToolStripItem[] { CloseButton, ToolStripSep1, RefreshButton });
            MainToolStrip.Location = new Point(0, 0);
            MainToolStrip.Name = "MainToolStrip";
            MainToolStrip.Size = new Size(544, 25);
            MainToolStrip.TabIndex = 0;
            // 
            // CloseButton
            // 
            CloseButton.Image = DS_Game_Maker.My.Resources.Resources.AcceptIcon;
            CloseButton.ImageTransparentColor = Color.Magenta;
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(53, 22);
            CloseButton.Text = "Close";
            // 
            // ToolStripSep1
            // 
            ToolStripSep1.Name = "ToolStripSep1";
            ToolStripSep1.Size = new Size(6, 25);
            // 
            // RefreshButton
            // 
            RefreshButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            RefreshButton.Image = (Image)resources.GetObject("RefreshButton.Image");
            RefreshButton.ImageTransparentColor = Color.Magenta;
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(49, 22);
            RefreshButton.Text = "Refresh";
            // 
            // MainListBox
            // 
            MainListBox.BorderStyle = BorderStyle.None;
            MainListBox.Dock = DockStyle.Fill;
            MainListBox.DrawMode = DrawMode.OwnerDrawVariable;
            MainListBox.FormattingEnabled = true;
            MainListBox.Location = new Point(0, 25);
            MainListBox.Name = "MainListBox";
            MainListBox.Size = new Size(544, 357);
            MainListBox.TabIndex = 1;
            // 
            // FoundInScripts
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(544, 382);
            Controls.Add(MainListBox);
            Controls.Add(MainToolStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FoundInScripts";
            MainToolStrip.ResumeLayout(false);
            MainToolStrip.PerformLayout();
            Load += new EventHandler(FoundInScripts_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal ToolStrip MainToolStrip;
        internal ToolStripButton CloseButton;
        internal ToolStripSeparator ToolStripSep1;
        internal ToolStripButton RefreshButton;
        internal ListBox MainListBox;
    }
}