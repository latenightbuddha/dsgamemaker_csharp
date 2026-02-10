using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class DUpdate : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(DUpdate));
            MainTopPanel = new Panel();
            InfoLabel = new Label();
            TitleLabel = new Label();
            InstallButton = new Button();
            InstallButton.Click += new EventHandler(InstallButton_Click);
            NeverMindButton = new Button();
            NeverMindButton.Click += new EventHandler(NeverMindButton_Click);
            MainWebBrowser = new WebBrowser();
            MainWebBrowser.Navigating += new WebBrowserNavigatingEventHandler(MainWebBrowser_Navigating);
            UpdateIconPanel = new Panel();
            MainTopPanel.SuspendLayout();
            SuspendLayout();
            // 
            // MainTopPanel
            // 
            MainTopPanel.BackColor = Color.White;
            MainTopPanel.Controls.Add(InfoLabel);
            MainTopPanel.Controls.Add(TitleLabel);
            MainTopPanel.Controls.Add(UpdateIconPanel);
            MainTopPanel.Dock = DockStyle.Top;
            MainTopPanel.Location = new Point(0, 0);
            MainTopPanel.Name = "MainTopPanel";
            MainTopPanel.Size = new Size(362, 108);
            MainTopPanel.TabIndex = 0;
            // 
            // InfoLabel
            // 
            InfoLabel.AutoSize = true;
            InfoLabel.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            InfoLabel.ForeColor = Color.Gray;
            InfoLabel.Location = new Point(110, 44);
            InfoLabel.Name = "InfoLabel";
            InfoLabel.Size = new Size(192, 14);
            InfoLabel.TabIndex = 3;
            InfoLabel.Text = "Available for immediate installation";
            // 
            // TitleLabel
            // 
            TitleLabel.AutoSize = true;
            TitleLabel.Font = new Font("Trebuchet MS", 14.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            TitleLabel.Location = new Point(108, 20);
            TitleLabel.Name = "TitleLabel";
            TitleLabel.Size = new Size(31, 24);
            TitleLabel.TabIndex = 2;
            TitleLabel.Text = "...";
            // 
            // InstallButton
            // 
            InstallButton.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            InstallButton.Location = new Point(272, 366);
            InstallButton.Name = "InstallButton";
            InstallButton.Size = new Size(84, 28);
            InstallButton.TabIndex = 1;
            InstallButton.Text = "Install";
            InstallButton.UseVisualStyleBackColor = true;
            // 
            // NeverMindButton
            // 
            NeverMindButton.Location = new Point(186, 366);
            NeverMindButton.Name = "NeverMindButton";
            NeverMindButton.Size = new Size(84, 28);
            NeverMindButton.TabIndex = 2;
            NeverMindButton.Text = "Never Mind";
            NeverMindButton.UseVisualStyleBackColor = true;
            // 
            // MainWebBrowser
            // 
            MainWebBrowser.Location = new Point(6, 114);
            MainWebBrowser.MinimumSize = new Size(20, 20);
            MainWebBrowser.Name = "MainWebBrowser";
            MainWebBrowser.Size = new Size(350, 246);
            MainWebBrowser.TabIndex = 3;
            // 
            // UpdateIconPanel
            // 
            UpdateIconPanel.BackgroundImage = DS_Game_Maker.My.Resources.Resources.UpdateBigIcon;
            UpdateIconPanel.Location = new Point(6, 6);
            UpdateIconPanel.Name = "UpdateIconPanel";
            UpdateIconPanel.Size = new Size(96, 96);
            UpdateIconPanel.TabIndex = 1;
            // 
            // DUpdate
            // 
            AcceptButton = InstallButton;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(362, 399);
            Controls.Add(MainWebBrowser);
            Controls.Add(NeverMindButton);
            Controls.Add(InstallButton);
            Controls.Add(MainTopPanel);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DUpdate";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Update";
            MainTopPanel.ResumeLayout(false);
            MainTopPanel.PerformLayout();
            Shown += new EventHandler(Update_Shown);
            Load += new EventHandler(Update_Load);
            ResumeLayout(false);

        }
        internal Panel MainTopPanel;
        internal Panel UpdateIconPanel;
        internal Label TitleLabel;
        internal Label InfoLabel;
        internal Button InstallButton;
        internal Button NeverMindButton;
        internal WebBrowser MainWebBrowser;
    }
}