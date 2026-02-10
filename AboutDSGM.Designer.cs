using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class AboutDSGM : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDSGM));
            DSGMTopPanel = new Panel();
            MainPanel = new Panel();
            Label1 = new Label();
            AdditionalCreditsLabel = new Label();
            WebAddressLabel = new Label();
            WebAddressLabel.Click += new EventHandler(WebAddressLabel_Click);
            WrittenByLabel = new Label();
            VersionLabel = new Label();
            DOkayButton = new Button();
            DOkayButton.Click += new EventHandler(DOkayButton_Click);
            MainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // DSGMTopPanel
            // 
            DSGMTopPanel.BackgroundImage = DS_Game_Maker.Properties.Resources.DSGMTopBanner;
            DSGMTopPanel.Location = new Point(0, 0);
            DSGMTopPanel.Name = "DSGMTopPanel";
            DSGMTopPanel.Size = new Size(338, 130);
            DSGMTopPanel.TabIndex = 0;
            // 
            // MainPanel
            // 
            MainPanel.BackColor = Color.FromArgb(64, 64, 64);
            MainPanel.Controls.Add(Label1);
            MainPanel.Controls.Add(AdditionalCreditsLabel);
            MainPanel.Controls.Add(WebAddressLabel);
            MainPanel.Controls.Add(WrittenByLabel);
            MainPanel.Location = new Point(0, 130);
            MainPanel.Name = "MainPanel";
            MainPanel.Size = new Size(338, 235);
            MainPanel.TabIndex = 1;
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Font = new Font("Tahoma", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label1.ForeColor = Color.White;
            Label1.Location = new Point(8, 35);
            Label1.Name = "Label1";
            Label1.Size = new Size(193, 14);
            Label1.TabIndex = 4;
            Label1.Text = "With some love from Chris Ertl";
            // 
            // AdditionalCreditsLabel
            // 
            AdditionalCreditsLabel.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            AdditionalCreditsLabel.ForeColor = Color.Silver;
            AdditionalCreditsLabel.Location = new Point(8, 60);
            AdditionalCreditsLabel.Name = "AdditionalCreditsLabel";
            AdditionalCreditsLabel.Size = new Size(314, 67);
            AdditionalCreditsLabel.TabIndex = 3;
            AdditionalCreditsLabel.Text = "Additional components and assistance contributed by Dave J Murphy, Michael Noland" + ", Jason Rogers, Gregory Potdevin, Chris Rickard, Nick Thissen, Robert Dixon, Dav" + "e Tabba, Cilein Kearns and Baron Khan.";

            // 
            // WebAddressLabel
            // 
            WebAddressLabel.AutoSize = true;
            WebAddressLabel.Cursor = Cursors.Hand;
            WebAddressLabel.Font = new Font("Tahoma", 9.0f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            WebAddressLabel.ForeColor = Color.White;
            WebAddressLabel.Location = new Point(8, 207);
            WebAddressLabel.Name = "WebAddressLabel";
            WebAddressLabel.Size = new Size(58, 14);
            WebAddressLabel.TabIndex = 2;
            WebAddressLabel.Text = "dsgm.co";
            // 
            // WrittenByLabel
            // 
            WrittenByLabel.AutoSize = true;
            WrittenByLabel.Font = new Font("Tahoma", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            WrittenByLabel.ForeColor = Color.White;
            WrittenByLabel.Location = new Point(8, 12);
            WrittenByLabel.Name = "WrittenByLabel";
            WrittenByLabel.Size = new Size(158, 14);
            WrittenByLabel.TabIndex = 0;
            WrittenByLabel.Text = "Written by James Garner";
            // 
            // VersionLabel
            // 
            VersionLabel.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            VersionLabel.ForeColor = Color.Gray;
            VersionLabel.Location = new Point(6, 375);
            VersionLabel.Name = "VersionLabel";
            VersionLabel.Size = new Size(200, 18);
            VersionLabel.TabIndex = 3;
            VersionLabel.Text = "...";
            // 
            // DOkayButton
            // 
            DOkayButton.Location = new Point(234, 369);
            DOkayButton.Name = "DOkayButton";
            DOkayButton.Size = new Size(100, 28);
            DOkayButton.TabIndex = 2;
            DOkayButton.Text = "OK";
            DOkayButton.UseVisualStyleBackColor = true;
            // 
            // AboutDSGM
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(338, 402);
            Controls.Add(VersionLabel);
            Controls.Add(DOkayButton);
            Controls.Add(MainPanel);
            Controls.Add(DSGMTopPanel);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutDSGM";
            StartPosition = FormStartPosition.CenterParent;
            Text = "About DS Game Maker";
            MainPanel.ResumeLayout(false);
            MainPanel.PerformLayout();
            Load += new EventHandler(AboutDSGM_Load);
            ResumeLayout(false);

        }
        internal Panel DSGMTopPanel;
        internal Panel MainPanel;
        internal Button DOkayButton;
        internal Label WrittenByLabel;
        internal Label WebAddressLabel;
        internal Label VersionLabel;
        internal Label AdditionalCreditsLabel;
        internal Label Label1;
    }
}