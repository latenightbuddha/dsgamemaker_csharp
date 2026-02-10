using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class FontViewer : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(FontViewer));
            NamePanel = new Panel();
            RebuildCacheButton = new Button();
            RebuildCacheButton.Click += new EventHandler(RebuildCacheButton_Click);
            RebuildCacheButton.MouseEnter += new EventHandler(RebuildCacheButton_MouseEnter);
            RebuildCacheButton.MouseLeave += new EventHandler(RebuildCacheButton_MouseLeave);
            PreviousButton = new Button();
            PreviousButton.Click += new EventHandler(PreviousButton_Click);
            PreviousButton.MouseEnter += new EventHandler(RebuildCacheButton_MouseEnter);
            PreviousButton.MouseLeave += new EventHandler(RebuildCacheButton_MouseLeave);
            NextButton = new Button();
            NextButton.Click += new EventHandler(NextButton_Click);
            NextButton.MouseEnter += new EventHandler(RebuildCacheButton_MouseEnter);
            NextButton.MouseLeave += new EventHandler(RebuildCacheButton_MouseLeave);
            NameLabel = new Label();
            MainImagePanel = new Panel();
            MainStatusStrip = new StatusStrip();
            MainInfoLabel = new ToolStripStatusLabel();
            NamePanel.SuspendLayout();
            MainStatusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // NamePanel
            // 
            NamePanel.BorderStyle = BorderStyle.FixedSingle;
            NamePanel.Controls.Add(RebuildCacheButton);
            NamePanel.Controls.Add(PreviousButton);
            NamePanel.Controls.Add(NextButton);
            NamePanel.Controls.Add(NameLabel);
            NamePanel.Dock = DockStyle.Top;
            NamePanel.Location = new Point(0, 0);
            NamePanel.Name = "NamePanel";
            NamePanel.Size = new Size(280, 28);
            NamePanel.TabIndex = 0;
            // 
            // RebuildCacheButton
            // 
            RebuildCacheButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            RebuildCacheButton.Image = DS_Game_Maker.My.Resources.Resources.OpenLastIcon;
            RebuildCacheButton.Location = new Point(205, 1);
            RebuildCacheButton.Name = "RebuildCacheButton";
            RebuildCacheButton.Size = new Size(24, 24);
            RebuildCacheButton.TabIndex = 3;
            RebuildCacheButton.UseVisualStyleBackColor = true;
            // 
            // PreviousButton
            // 
            PreviousButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            PreviousButton.Image = DS_Game_Maker.My.Resources.Resources.ArrowUp;
            PreviousButton.Location = new Point(229, 1);
            PreviousButton.Name = "PreviousButton";
            PreviousButton.Size = new Size(24, 24);
            PreviousButton.TabIndex = 2;
            PreviousButton.UseVisualStyleBackColor = true;
            // 
            // NextButton
            // 
            NextButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            NextButton.Image = DS_Game_Maker.My.Resources.Resources.ArrowDown;
            NextButton.Location = new Point(253, 1);
            NextButton.Name = "NextButton";
            NextButton.Size = new Size(24, 24);
            NextButton.TabIndex = 1;
            NextButton.UseVisualStyleBackColor = true;
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Font = new Font("Arial", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            NameLabel.Location = new Point(3, 5);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(20, 18);
            NameLabel.TabIndex = 1;
            NameLabel.Text = "...";
            // 
            // MainImagePanel
            // 
            MainImagePanel.BackColor = Color.White;
            MainImagePanel.BackgroundImageLayout = ImageLayout.Center;
            MainImagePanel.Dock = DockStyle.Fill;
            MainImagePanel.Location = new Point(0, 28);
            MainImagePanel.Name = "MainImagePanel";
            MainImagePanel.Size = new Size(280, 104);
            MainImagePanel.TabIndex = 1;
            // 
            // MainStatusStrip
            // 
            MainStatusStrip.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainStatusStrip.Items.AddRange(new ToolStripItem[] { MainInfoLabel });
            MainStatusStrip.Location = new Point(0, 132);
            MainStatusStrip.Name = "MainStatusStrip";
            MainStatusStrip.Size = new Size(280, 22);
            MainStatusStrip.SizingGrip = false;
            MainStatusStrip.TabIndex = 2;
            MainStatusStrip.Text = "StatusStrip1";
            // 
            // MainInfoLabel
            // 
            MainInfoLabel.Name = "MainInfoLabel";
            MainInfoLabel.Size = new Size(19, 17);
            MainInfoLabel.Text = "...";
            // 
            // FontViewer
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(280, 154);
            Controls.Add(MainImagePanel);
            Controls.Add(NamePanel);
            Controls.Add(MainStatusStrip);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(296, 192);
            MinimizeBox = false;
            MinimumSize = new Size(296, 192);
            Name = "FontViewer";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "Font Viewer";
            NamePanel.ResumeLayout(false);
            NamePanel.PerformLayout();
            MainStatusStrip.ResumeLayout(false);
            MainStatusStrip.PerformLayout();
            Load += new EventHandler(FontEditor_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal Panel NamePanel;
        internal Label NameLabel;
        internal Button NextButton;
        internal Button PreviousButton;
        internal Panel MainImagePanel;
        internal Button RebuildCacheButton;
        internal StatusStrip MainStatusStrip;
        internal ToolStripStatusLabel MainInfoLabel;
    }
}