using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class SoundType : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(SoundType));
            SoundEffectRadioButton = new RadioButton();
            SoundEffectRadioButton.CheckedChanged += new EventHandler(SoundEffectRadioButton_CheckedChanged);
            DOkayButton = new Button();
            DOkayButton.Click += new EventHandler(DOkayButton_Click);
            BackgroundSoundRadioButton = new RadioButton();
            BackgroundSoundRadioButton.CheckedChanged += new EventHandler(SoundEffectRadioButton_CheckedChanged);
            InfoLabel = new Label();
            SuspendLayout();
            // 
            // SoundEffectRadioButton
            // 
            SoundEffectRadioButton.AutoSize = true;
            SoundEffectRadioButton.Location = new Point(12, 12);
            SoundEffectRadioButton.Name = "SoundEffectRadioButton";
            SoundEffectRadioButton.Size = new Size(87, 17);
            SoundEffectRadioButton.TabIndex = 0;
            SoundEffectRadioButton.Text = "Sound Effect";
            SoundEffectRadioButton.UseVisualStyleBackColor = true;
            // 
            // DOkayButton
            // 
            DOkayButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            DOkayButton.Location = new Point(161, 132);
            DOkayButton.Name = "DOkayButton";
            DOkayButton.Size = new Size(106, 28);
            DOkayButton.TabIndex = 1;
            DOkayButton.Text = "Okay";
            DOkayButton.UseVisualStyleBackColor = true;
            // 
            // BackgroundSoundRadioButton
            // 
            BackgroundSoundRadioButton.AutoSize = true;
            BackgroundSoundRadioButton.Location = new Point(12, 35);
            BackgroundSoundRadioButton.Name = "BackgroundSoundRadioButton";
            BackgroundSoundRadioButton.Size = new Size(114, 17);
            BackgroundSoundRadioButton.TabIndex = 2;
            BackgroundSoundRadioButton.Text = "Background Sound";
            BackgroundSoundRadioButton.UseVisualStyleBackColor = true;
            // 
            // InfoLabel
            // 
            InfoLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            InfoLabel.BorderStyle = BorderStyle.Fixed3D;
            InfoLabel.Location = new Point(12, 67);
            InfoLabel.Name = "InfoLabel";
            InfoLabel.Padding = new Padding(4);
            InfoLabel.Size = new Size(255, 62);
            InfoLabel.TabIndex = 3;
            InfoLabel.Text = "Label1";
            // 
            // SoundType
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(279, 172);
            Controls.Add(InfoLabel);
            Controls.Add(BackgroundSoundRadioButton);
            Controls.Add(DOkayButton);
            Controls.Add(SoundEffectRadioButton);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(295, 210);
            MinimizeBox = false;
            MinimumSize = new Size(295, 210);
            Name = "SoundType";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Select Sound Type";
            Load += new EventHandler(SoundType_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal RadioButton SoundEffectRadioButton;
        internal Button DOkayButton;
        internal RadioButton BackgroundSoundRadioButton;
        internal Label InfoLabel;
    }
}