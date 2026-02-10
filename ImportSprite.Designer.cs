using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class ImportSprite : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportSprite));
            ImportSettingsPanel = new Panel();
            ImagesPerColumnDropper = new NumericUpDown();
            ImagesPerColumnDropper.ValueChanged += new EventHandler(ImagesPerColumnDropper_ValueChanged);
            ImagesPerColumnLabel = new Label();
            DAcceptButton = new Button();
            DAcceptButton.Click += new EventHandler(DAcceptButton_Click);
            ImagesPerRowDropper = new NumericUpDown();
            ImagesPerRowDropper.ValueChanged += new EventHandler(ImagesPerRowDropper_ValueChanged);
            FrameHeightDropper = new ComboBox();
            FrameHeightDropper.SelectedIndexChanged += new EventHandler(Droppers_ValuesChanged);
            FrameHeightLabel = new Label();
            FrameWidthDropper = new ComboBox();
            FrameWidthDropper.SelectedIndexChanged += new EventHandler(Droppers_ValuesChanged);
            FrameWidthLabel = new Label();
            ImagesPerRowLabel = new Label();
            PreviewPanel = new Panel();
            ImportSettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ImagesPerColumnDropper).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ImagesPerRowDropper).BeginInit();
            SuspendLayout();
            // 
            // ImportSettingsPanel
            // 
            ImportSettingsPanel.Controls.Add(ImagesPerColumnDropper);
            ImportSettingsPanel.Controls.Add(ImagesPerColumnLabel);
            ImportSettingsPanel.Controls.Add(DAcceptButton);
            ImportSettingsPanel.Controls.Add(ImagesPerRowDropper);
            ImportSettingsPanel.Controls.Add(FrameHeightDropper);
            ImportSettingsPanel.Controls.Add(FrameHeightLabel);
            ImportSettingsPanel.Controls.Add(FrameWidthDropper);
            ImportSettingsPanel.Controls.Add(FrameWidthLabel);
            ImportSettingsPanel.Controls.Add(ImagesPerRowLabel);
            ImportSettingsPanel.Dock = DockStyle.Left;
            ImportSettingsPanel.Location = new Point(0, 0);
            ImportSettingsPanel.Name = "ImportSettingsPanel";
            ImportSettingsPanel.Size = new Size(177, 442);
            ImportSettingsPanel.TabIndex = 0;
            // 
            // ImagesPerColumnDropper
            // 
            ImagesPerColumnDropper.Location = new Point(117, 93);
            ImagesPerColumnDropper.Name = "ImagesPerColumnDropper";
            ImagesPerColumnDropper.Size = new Size(47, 21);
            ImagesPerColumnDropper.TabIndex = 6;
            ImagesPerColumnDropper.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // ImagesPerColumnLabel
            // 
            ImagesPerColumnLabel.AutoSize = true;
            ImagesPerColumnLabel.Location = new Point(8, 95);
            ImagesPerColumnLabel.Name = "ImagesPerColumnLabel";
            ImagesPerColumnLabel.Size = new Size(103, 13);
            ImagesPerColumnLabel.TabIndex = 7;
            ImagesPerColumnLabel.Text = "Images Per Column:";
            // 
            // DAcceptButton
            // 
            DAcceptButton.Image = DS_Game_Maker.My.Resources.Resources.AcceptIcon;
            DAcceptButton.ImageAlign = ContentAlignment.MiddleLeft;
            DAcceptButton.Location = new Point(12, 120);
            DAcceptButton.Name = "DAcceptButton";
            DAcceptButton.Size = new Size(152, 26);
            DAcceptButton.TabIndex = 5;
            DAcceptButton.Text = "Accept";
            DAcceptButton.UseVisualStyleBackColor = true;
            // 
            // ImagesPerRowDropper
            // 
            ImagesPerRowDropper.Location = new Point(117, 66);
            ImagesPerRowDropper.Name = "ImagesPerRowDropper";
            ImagesPerRowDropper.Size = new Size(47, 21);
            ImagesPerRowDropper.TabIndex = 1;
            ImagesPerRowDropper.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // FrameHeightDropper
            // 
            FrameHeightDropper.FormattingEnabled = true;
            FrameHeightDropper.Items.AddRange(new object[] { "16", "32", "64" });
            FrameHeightDropper.Location = new Point(117, 39);
            FrameHeightDropper.Name = "FrameHeightDropper";
            FrameHeightDropper.Size = new Size(47, 21);
            FrameHeightDropper.TabIndex = 4;
            FrameHeightDropper.Text = "32";
            // 
            // FrameHeightLabel
            // 
            FrameHeightLabel.AutoSize = true;
            FrameHeightLabel.Location = new Point(36, 42);
            FrameHeightLabel.Name = "FrameHeightLabel";
            FrameHeightLabel.Size = new Size(75, 13);
            FrameHeightLabel.TabIndex = 3;
            FrameHeightLabel.Text = "Frame Height:";
            // 
            // FrameWidthDropper
            // 
            FrameWidthDropper.FormattingEnabled = true;
            FrameWidthDropper.Items.AddRange(new object[] { "16", "32", "64" });
            FrameWidthDropper.Location = new Point(117, 12);
            FrameWidthDropper.Name = "FrameWidthDropper";
            FrameWidthDropper.Size = new Size(47, 21);
            FrameWidthDropper.TabIndex = 1;
            FrameWidthDropper.Text = "32";
            // 
            // FrameWidthLabel
            // 
            FrameWidthLabel.AutoSize = true;
            FrameWidthLabel.Location = new Point(39, 15);
            FrameWidthLabel.Name = "FrameWidthLabel";
            FrameWidthLabel.Size = new Size(72, 13);
            FrameWidthLabel.TabIndex = 1;
            FrameWidthLabel.Text = "Frame Width:";
            // 
            // ImagesPerRowLabel
            // 
            ImagesPerRowLabel.AutoSize = true;
            ImagesPerRowLabel.Location = new Point(22, 68);
            ImagesPerRowLabel.Name = "ImagesPerRowLabel";
            ImagesPerRowLabel.Size = new Size(89, 13);
            ImagesPerRowLabel.TabIndex = 2;
            ImagesPerRowLabel.Text = "Images Per Row:";
            // 
            // PreviewPanel
            // 
            PreviewPanel.BackColor = Color.White;
            PreviewPanel.BackgroundImageLayout = ImageLayout.None;
            PreviewPanel.Dock = DockStyle.Fill;
            PreviewPanel.Location = new Point(177, 0);
            PreviewPanel.Name = "PreviewPanel";
            PreviewPanel.Size = new Size(447, 442);
            PreviewPanel.TabIndex = 1;
            // 
            // ImportSprite
            // 
            AcceptButton = DAcceptButton;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(624, 442);
            Controls.Add(PreviewPanel);
            Controls.Add(ImportSettingsPanel);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ImportSprite";
            Text = "Import Sprite";
            ImportSettingsPanel.ResumeLayout(false);
            ImportSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ImagesPerColumnDropper).EndInit();
            ((System.ComponentModel.ISupportInitialize)ImagesPerRowDropper).EndInit();
            Load += new EventHandler(ImportSprite_Load);
            Activated += new EventHandler(ImportSprite_Activated);
            ResumeLayout(false);

        }
        internal Panel ImportSettingsPanel;
        internal NumericUpDown ImagesPerRowDropper;
        internal Label ImagesPerRowLabel;
        internal ComboBox FrameWidthDropper;
        internal Label FrameWidthLabel;
        internal ComboBox FrameHeightDropper;
        internal Label FrameHeightLabel;
        internal Button DAcceptButton;
        internal Panel PreviewPanel;
        internal NumericUpDown ImagesPerColumnDropper;
        internal Label ImagesPerColumnLabel;
    }
}