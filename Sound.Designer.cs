using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Sound : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Sound));
            MainToolStrip = new ToolStrip();
            DAcceptButton = new ToolStripButton();
            DAcceptButton.Click += new EventHandler(DAcceptButton_Click);
            ToolStripSeparator = new ToolStripSeparator();
            EditButton = new ToolStripButton();
            EditButton.Click += new EventHandler(EditButton_Click);
            LoadButton = new ToolStripButton();
            LoadButton.Click += new EventHandler(LoadButton_Click);
            IconPictureBox = new PictureBox();
            NameTextBox = new TextBox();
            InfoLabel = new Label();
            PlayButton = new Panel();
            PlayButton.Click += new EventHandler(PlayButton_Click);
            StopButton = new Panel();
            StopButton.Click += new EventHandler(StopButton_Click);
            MainToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)IconPictureBox).BeginInit();
            SuspendLayout();
            // 
            // MainToolStrip
            // 
            MainToolStrip.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainToolStrip.Items.AddRange(new ToolStripItem[] { DAcceptButton, ToolStripSeparator, EditButton, LoadButton });
            MainToolStrip.Location = new Point(0, 0);
            MainToolStrip.Name = "MainToolStrip";
            MainToolStrip.Size = new Size(235, 25);
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
            // ToolStripSeparator
            // 
            ToolStripSeparator.Name = "ToolStripSeparator";
            ToolStripSeparator.Size = new Size(6, 25);
            // 
            // EditButton
            // 
            EditButton.Image = DS_Game_Maker.My.Resources.Resources.PencilIcon;
            EditButton.ImageTransparentColor = Color.Magenta;
            EditButton.Name = "EditButton";
            EditButton.Size = new Size(45, 22);
            EditButton.Text = "Edit";
            // 
            // LoadButton
            // 
            LoadButton.Image = DS_Game_Maker.My.Resources.Resources.OpenIcon;
            LoadButton.ImageTransparentColor = Color.Magenta;
            LoadButton.Name = "LoadButton";
            LoadButton.Size = new Size(94, 22);
            LoadButton.Text = "Load from File";
            // 
            // IconPictureBox
            // 
            IconPictureBox.Image = DS_Game_Maker.My.Resources.Resources.BigMusicIcon1;
            IconPictureBox.Location = new Point(4, 27);
            IconPictureBox.Name = "IconPictureBox";
            IconPictureBox.Size = new Size(84, 84);
            IconPictureBox.TabIndex = 1;
            IconPictureBox.TabStop = false;
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(94, 29);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(136, 21);
            NameTextBox.TabIndex = 3;
            // 
            // InfoLabel
            // 
            InfoLabel.AutoSize = true;
            InfoLabel.Location = new Point(94, 53);
            InfoLabel.Name = "InfoLabel";
            InfoLabel.Size = new Size(19, 13);
            InfoLabel.TabIndex = 6;
            InfoLabel.Text = "...";
            // 
            // PlayButton
            // 
            PlayButton.BackgroundImage = DS_Game_Maker.My.Resources.Resources.PlayButtonIcon;
            PlayButton.Location = new Point(161, 79);
            PlayButton.Name = "PlayButton";
            PlayButton.Size = new Size(32, 32);
            PlayButton.TabIndex = 6;
            // 
            // StopButton
            // 
            StopButton.BackgroundImage = DS_Game_Maker.My.Resources.Resources.StopButtonIcon;
            StopButton.Location = new Point(197, 79);
            StopButton.Name = "StopButton";
            StopButton.Size = new Size(32, 32);
            StopButton.TabIndex = 7;
            // 
            // Sound
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(235, 116);
            Controls.Add(PlayButton);
            Controls.Add(StopButton);
            Controls.Add(MainToolStrip);
            Controls.Add(IconPictureBox);
            Controls.Add(InfoLabel);
            Controls.Add(NameTextBox);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(251, 323);
            MinimizeBox = false;
            MinimumSize = new Size(251, 50);
            Name = "Sound";
            MainToolStrip.ResumeLayout(false);
            MainToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)IconPictureBox).EndInit();
            Load += new EventHandler(Sound_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal ToolStrip MainToolStrip;
        internal ToolStripButton DAcceptButton;
        internal PictureBox IconPictureBox;
        internal TextBox NameTextBox;
        internal ToolStripButton EditButton;
        internal ToolStripButton LoadButton;
        internal ToolStripSeparator ToolStripSeparator;
        internal Label InfoLabel;
        internal Panel PlayButton;
        internal Panel StopButton;
    }
}