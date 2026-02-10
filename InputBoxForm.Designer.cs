using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class InputBoxForm : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(InputBoxForm));
            DescriptionLabel = new Label();
            MainTextBox = new TextBox();
            MainTextBox.TextChanged += new EventHandler(MainTextBox_TextChanged);
            DOkayButton = new Button();
            DOkayButton.Click += new EventHandler(DOkayButton_Click);
            DCancelButton = new Button();
            DCancelButton.Click += new EventHandler(DCancelButton_Click);
            SuspendLayout();
            // 
            // DescriptionLabel
            // 
            DescriptionLabel.AutoSize = true;
            DescriptionLabel.Location = new Point(12, 9);
            DescriptionLabel.Name = "DescriptionLabel";
            DescriptionLabel.Size = new Size(85, 13);
            DescriptionLabel.TabIndex = 0;
            DescriptionLabel.Text = "DescriptionLabel";
            // 
            // MainTextBox
            // 
            MainTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MainTextBox.Location = new Point(12, 81);
            MainTextBox.Name = "MainTextBox";
            MainTextBox.Size = new Size(320, 21);
            MainTextBox.TabIndex = 1;
            // 
            // DOkayButton
            // 
            DOkayButton.DialogResult = DialogResult.Cancel;
            DOkayButton.Location = new Point(248, 12);
            DOkayButton.Name = "DOkayButton";
            DOkayButton.Size = new Size(84, 26);
            DOkayButton.TabIndex = 2;
            DOkayButton.Text = "OK";
            DOkayButton.UseVisualStyleBackColor = true;
            // 
            // DCancelButton
            // 
            DCancelButton.DialogResult = DialogResult.Cancel;
            DCancelButton.Location = new Point(248, 40);
            DCancelButton.Name = "DCancelButton";
            DCancelButton.Size = new Size(84, 26);
            DCancelButton.TabIndex = 3;
            DCancelButton.Text = "Cancel";
            DCancelButton.UseVisualStyleBackColor = true;
            // 
            // InputBoxForm
            // 
            AcceptButton = DOkayButton;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = DCancelButton;
            ClientSize = new Size(344, 114);
            Controls.Add(DCancelButton);
            Controls.Add(DOkayButton);
            Controls.Add(MainTextBox);
            Controls.Add(DescriptionLabel);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InputBoxForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "DS Game Maker";
            Load += new EventHandler(InputBoxForm_Load);
            Activated += new EventHandler(InputBoxForm_Activated);
            ResumeLayout(false);
            PerformLayout();

        }
        internal Label DescriptionLabel;
        internal TextBox MainTextBox;
        internal Button DOkayButton;
        internal Button DCancelButton;
    }
}