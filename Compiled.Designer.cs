using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Compiled : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Compiled));
            Panel1 = new Panel();
            SubLabel = new Label();
            MainLabel = new Label();
            CloseButton = new Button();
            CloseButton.Click += new EventHandler(CloseButton_Click);
            SavetoKitButton = new Button();
            SavetoKitButton.Click += new EventHandler(SavetoKitButton_Click);
            OpenCompileFolderButton = new Button();
            OpenCompileFolderButton.Click += new EventHandler(OpenCompileFolderButton_Click);
            SaveNDSFileButton = new Button();
            SaveNDSFileButton.Click += new EventHandler(SaveNDSFileButton_Click);
            PlayButton = new Button();
            PlayButton.Click += new EventHandler(PlayButton_Click);
            Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // Panel1
            // 
            Panel1.BackColor = Color.FromArgb(64, 64, 64);
            Panel1.Controls.Add(SubLabel);
            Panel1.Controls.Add(MainLabel);
            Panel1.Dock = DockStyle.Top;
            Panel1.Location = new Point(0, 0);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(284, 54);
            Panel1.TabIndex = 0;
            // 
            // SubLabel
            // 
            SubLabel.AutoSize = true;
            SubLabel.Font = new Font("Arial", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            SubLabel.ForeColor = Color.Silver;
            SubLabel.Location = new Point(8, 27);
            SubLabel.Name = "SubLabel";
            SubLabel.Size = new Size(16, 15);
            SubLabel.TabIndex = 2;
            SubLabel.Text = "...";
            // 
            // MainLabel
            // 
            MainLabel.AutoSize = true;
            MainLabel.Font = new Font("Arial", 12.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            MainLabel.ForeColor = Color.White;
            MainLabel.Location = new Point(6, 9);
            MainLabel.Name = "MainLabel";
            MainLabel.Size = new Size(21, 19);
            MainLabel.TabIndex = 1;
            MainLabel.Text = "...";
            // 
            // CloseButton
            // 
            CloseButton.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            CloseButton.Location = new Point(179, 268);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(98, 28);
            CloseButton.TabIndex = 1;
            CloseButton.Text = "Close";
            CloseButton.UseVisualStyleBackColor = true;
            // 
            // SavetoKitButton
            // 
            SavetoKitButton.Font = new Font("Arial", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            SavetoKitButton.Image = DS_Game_Maker.My.Resources.Resources.BigHBIcon;
            SavetoKitButton.ImageAlign = ContentAlignment.MiddleLeft;
            SavetoKitButton.Location = new Point(8, 114);
            SavetoKitButton.Name = "SavetoKitButton";
            SavetoKitButton.Size = new Size(269, 48);
            SavetoKitButton.TabIndex = 5;
            SavetoKitButton.Text = "           Save to Homebrew Kit";
            SavetoKitButton.TextAlign = ContentAlignment.MiddleLeft;
            SavetoKitButton.UseVisualStyleBackColor = true;
            // 
            // OpenCompileFolderButton
            // 
            OpenCompileFolderButton.Font = new Font("Arial", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            OpenCompileFolderButton.Image = DS_Game_Maker.My.Resources.Resources.BigFolderIcon;
            OpenCompileFolderButton.ImageAlign = ContentAlignment.MiddleLeft;
            OpenCompileFolderButton.Location = new Point(8, 214);
            OpenCompileFolderButton.Name = "OpenCompileFolderButton";
            OpenCompileFolderButton.Size = new Size(269, 48);
            OpenCompileFolderButton.TabIndex = 4;
            OpenCompileFolderButton.Text = "           Open Compile Folder";
            OpenCompileFolderButton.TextAlign = ContentAlignment.MiddleLeft;
            OpenCompileFolderButton.UseVisualStyleBackColor = true;
            // 
            // SaveNDSFileButton
            // 
            SaveNDSFileButton.Font = new Font("Arial", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            SaveNDSFileButton.Image = DS_Game_Maker.My.Resources.Resources.BigSaveIcon;
            SaveNDSFileButton.ImageAlign = ContentAlignment.MiddleLeft;
            SaveNDSFileButton.Location = new Point(8, 164);
            SaveNDSFileButton.Name = "SaveNDSFileButton";
            SaveNDSFileButton.Size = new Size(269, 48);
            SaveNDSFileButton.TabIndex = 3;
            SaveNDSFileButton.Text = "           Save NDS File";
            SaveNDSFileButton.TextAlign = ContentAlignment.MiddleLeft;
            SaveNDSFileButton.UseVisualStyleBackColor = true;
            // 
            // PlayButton
            // 
            PlayButton.Font = new Font("Arial", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            PlayButton.Image = DS_Game_Maker.My.Resources.Resources.BigPlayButton;
            PlayButton.ImageAlign = ContentAlignment.MiddleLeft;
            PlayButton.Location = new Point(8, 64);
            PlayButton.Name = "PlayButton";
            PlayButton.Size = new Size(269, 48);
            PlayButton.TabIndex = 2;
            PlayButton.Text = "           Play Game";
            PlayButton.TextAlign = ContentAlignment.MiddleLeft;
            PlayButton.UseVisualStyleBackColor = true;
            // 
            // Compiled
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(228, 228, 228);
            ClientSize = new Size(284, 304);
            Controls.Add(SavetoKitButton);
            Controls.Add(OpenCompileFolderButton);
            Controls.Add(SaveNDSFileButton);
            Controls.Add(PlayButton);
            Controls.Add(CloseButton);
            Controls.Add(Panel1);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Compiled";
            StartPosition = FormStartPosition.CenterParent;
            Panel1.ResumeLayout(false);
            Panel1.PerformLayout();
            ResumeLayout(false);

        }
        internal Panel Panel1;
        internal Label MainLabel;
        internal Label SubLabel;
        internal Button CloseButton;
        internal Button PlayButton;
        internal Button SaveNDSFileButton;
        internal Button OpenCompileFolderButton;
        internal Button SavetoKitButton;
    }
}