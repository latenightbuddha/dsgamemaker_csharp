using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class SpriteSize : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(SpriteSize));
            TablePanel = new Panel();
            MainInfoLabel = new Label();
            DOkayButton = new Button();
            DOkayButton.Click += new EventHandler(DOkayButton_Click);
            OpenEditorButton = new Button();
            OpenEditorButton.Click += new EventHandler(OpenEditorButton_Click);
            SuspendLayout();
            // 
            // TablePanel
            // 
            TablePanel.BackgroundImage = DS_Game_Maker.Properties.Resources.SpriteSizesTable;
            TablePanel.Location = new Point(46, 94);
            TablePanel.Name = "TablePanel";
            TablePanel.Size = new Size(151, 70);
            TablePanel.TabIndex = 0;
            // 
            // MainInfoLabel
            // 
            MainInfoLabel.Location = new Point(12, 9);
            MainInfoLabel.Name = "MainInfoLabel";
            MainInfoLabel.Size = new Size(218, 82);
            MainInfoLabel.TabIndex = 1;
            MainInfoLabel.Text = "This Frame was not correctly sized for the DS. It must be one of the below sizes." + "" + '\r' + '\n' + '\r' + '\n' + "Click 'Open Editor' to open up your image editor so that you can correct the" + " problem.";

            // 
            // DOkayButton
            // 
            DOkayButton.Anchor = AnchorStyles.None;
            DOkayButton.Location = new Point(124, 185);
            DOkayButton.Name = "DOkayButton";
            DOkayButton.Size = new Size(105, 28);
            DOkayButton.TabIndex = 2;
            DOkayButton.Text = "OK";
            DOkayButton.UseVisualStyleBackColor = true;
            // 
            // OpenEditorButton
            // 
            OpenEditorButton.Anchor = AnchorStyles.None;
            OpenEditorButton.Location = new Point(15, 185);
            OpenEditorButton.Name = "OpenEditorButton";
            OpenEditorButton.Size = new Size(105, 28);
            OpenEditorButton.TabIndex = 3;
            OpenEditorButton.Text = "Open Editor";
            OpenEditorButton.UseVisualStyleBackColor = true;
            // 
            // SpriteSize
            // 
            AcceptButton = DOkayButton;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(242, 225);
            Controls.Add(OpenEditorButton);
            Controls.Add(DOkayButton);
            Controls.Add(MainInfoLabel);
            Controls.Add(TablePanel);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SpriteSize";
            Text = "Invalid Frame Size";
            ResumeLayout(false);

        }
        internal Panel TablePanel;
        internal Label MainInfoLabel;
        internal Button DOkayButton;
        internal Button OpenEditorButton;
    }
}