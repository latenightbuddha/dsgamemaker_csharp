using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Statistics : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Statistics));
            MainToolStrip = new ToolStrip();
            DAcceptButton = new ToolStripButton();
            DAcceptButton.Click += new EventHandler(DAcceptButton_Click);
            ToolStripSep1 = new ToolStripSeparator();
            CopytoClipboardButton = new ToolStripButton();
            CopytoClipboardButton.Click += new EventHandler(CopytoClipboardButton_Click);
            ResourcesHeaderLabel = new Label();
            ResoucesLabel = new Label();
            LogicLabel = new Label();
            LogicHeaderLabel = new Label();
            CoolBar = new ProgressBar();
            CalculateUsageButton = new Button();
            CalculateUsageButton.Click += new EventHandler(CalculateUsageButton_Click);
            UsageLabel = new Label();
            MainToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // MainToolStrip
            // 
            MainToolStrip.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainToolStrip.Items.AddRange(new ToolStripItem[] { DAcceptButton, ToolStripSep1, CopytoClipboardButton });
            MainToolStrip.Location = new Point(0, 0);
            MainToolStrip.Name = "MainToolStrip";
            MainToolStrip.Size = new Size(287, 25);
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
            // CopytoClipboardButton
            // 
            CopytoClipboardButton.Image = DS_Game_Maker.My.Resources.Resources.CopyIcon;
            CopytoClipboardButton.ImageTransparentColor = Color.Magenta;
            CopytoClipboardButton.Name = "CopytoClipboardButton";
            CopytoClipboardButton.Size = new Size(113, 22);
            CopytoClipboardButton.Text = "Copy to Clipboard";
            // 
            // ResourcesHeaderLabel
            // 
            ResourcesHeaderLabel.AutoSize = true;
            ResourcesHeaderLabel.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            ResourcesHeaderLabel.Location = new Point(9, 30);
            ResourcesHeaderLabel.Name = "ResourcesHeaderLabel";
            ResourcesHeaderLabel.Size = new Size(69, 13);
            ResourcesHeaderLabel.TabIndex = 1;
            ResourcesHeaderLabel.Text = "Resources:";
            // 
            // ResoucesLabel
            // 
            ResoucesLabel.BorderStyle = BorderStyle.Fixed3D;
            ResoucesLabel.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ResoucesLabel.Location = new Point(12, 47);
            ResoucesLabel.Name = "ResoucesLabel";
            ResoucesLabel.Size = new Size(263, 90);
            ResoucesLabel.TabIndex = 2;
            // 
            // LogicLabel
            // 
            LogicLabel.BorderStyle = BorderStyle.Fixed3D;
            LogicLabel.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            LogicLabel.Location = new Point(12, 159);
            LogicLabel.Name = "LogicLabel";
            LogicLabel.Size = new Size(263, 90);
            LogicLabel.TabIndex = 4;
            // 
            // LogicHeaderLabel
            // 
            LogicHeaderLabel.AutoSize = true;
            LogicHeaderLabel.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            LogicHeaderLabel.Location = new Point(9, 142);
            LogicHeaderLabel.Name = "LogicHeaderLabel";
            LogicHeaderLabel.Size = new Size(39, 13);
            LogicHeaderLabel.TabIndex = 3;
            LogicHeaderLabel.Text = "Logic:";
            // 
            // CoolBar
            // 
            CoolBar.Location = new Point(92, 274);
            CoolBar.Name = "CoolBar";
            CoolBar.Size = new Size(183, 22);
            CoolBar.TabIndex = 5;
            // 
            // CalculateUsageButton
            // 
            CalculateUsageButton.Location = new Point(12, 274);
            CalculateUsageButton.Name = "CalculateUsageButton";
            CalculateUsageButton.Size = new Size(74, 22);
            CalculateUsageButton.TabIndex = 6;
            CalculateUsageButton.Text = "Calculate";
            CalculateUsageButton.UseVisualStyleBackColor = true;
            // 
            // UsageLabel
            // 
            UsageLabel.AutoSize = true;
            UsageLabel.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            UsageLabel.Location = new Point(9, 256);
            UsageLabel.Name = "UsageLabel";
            UsageLabel.Size = new Size(190, 13);
            UsageLabel.TabIndex = 7;
            UsageLabel.Text = "Project Coolness (approximate):";
            // 
            // Statistics
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(287, 311);
            Controls.Add(UsageLabel);
            Controls.Add(CalculateUsageButton);
            Controls.Add(CoolBar);
            Controls.Add(LogicLabel);
            Controls.Add(LogicHeaderLabel);
            Controls.Add(ResoucesLabel);
            Controls.Add(ResourcesHeaderLabel);
            Controls.Add(MainToolStrip);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Statistics";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Project Statistics";
            MainToolStrip.ResumeLayout(false);
            MainToolStrip.PerformLayout();
            Load += new EventHandler(Statistics_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal ToolStrip MainToolStrip;
        internal ToolStripButton DAcceptButton;
        internal ToolStripButton CopytoClipboardButton;
        internal ToolStripSeparator ToolStripSep1;
        internal Label ResourcesHeaderLabel;
        internal Label ResoucesLabel;
        internal Label LogicLabel;
        internal Label LogicHeaderLabel;
        internal ProgressBar CoolBar;
        internal Button CalculateUsageButton;
        internal Label UsageLabel;
    }
}