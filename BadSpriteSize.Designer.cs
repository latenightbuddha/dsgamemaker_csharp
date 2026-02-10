using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class BadSpriteSize : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(BadSpriteSize));
            ExplanationLabel = new Label();
            Panel1 = new Panel();
            DAcceptButton = new Button();
            DAcceptButton.Click += new EventHandler(DAcceptButton_Click);
            SuspendLayout();
            // 
            // ExplanationLabel
            // 
            ExplanationLabel.Location = new Point(13, 9);
            ExplanationLabel.Name = "ExplanationLabel";
            ExplanationLabel.Size = new Size(196, 57);
            ExplanationLabel.TabIndex = 1;
            ExplanationLabel.Text = "The sprite you have tried to load is not correctly sized to be used on the DS. Yo" + "u will need to resize it to one of the sizes below using a paint program.";
            // 
            // Panel1
            // 
            Panel1.BackgroundImage = DS_Game_Maker.My.Resources.Resources.SpriteSizesTable1;
            Panel1.Location = new Point(13, 80);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(196, 94);
            Panel1.TabIndex = 2;
            // 
            // DAcceptButton
            // 
            DAcceptButton.Image = DS_Game_Maker.My.Resources.Resources.AcceptIcon;
            DAcceptButton.ImageAlign = ContentAlignment.MiddleLeft;
            DAcceptButton.Location = new Point(134, 180);
            DAcceptButton.Name = "DAcceptButton";
            DAcceptButton.Size = new Size(76, 28);
            DAcceptButton.TabIndex = 3;
            DAcceptButton.Text = "      Accept";
            DAcceptButton.TextAlign = ContentAlignment.MiddleLeft;
            DAcceptButton.UseVisualStyleBackColor = true;
            // 
            // BadSpriteSize
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(222, 220);
            Controls.Add(DAcceptButton);
            Controls.Add(Panel1);
            Controls.Add(ExplanationLabel);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BadSpriteSize";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Invalid Sprite Size";
            ResumeLayout(false);

        }
        internal Label ExplanationLabel;
        internal Panel Panel1;
        internal Button DAcceptButton;
    }
}