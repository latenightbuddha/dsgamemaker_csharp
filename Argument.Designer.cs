using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Argument : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Argument));
            DOkayButton = new Button();
            DOkayButton.Click += new EventHandler(DOkayButton_Click);
            NameLabel = new Label();
            NameTextBox = new TextBox();
            TypeLabel = new Label();
            TypeDropper = new ComboBox();
            SuspendLayout();
            // 
            // DOkayButton
            // 
            DOkayButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            DOkayButton.Location = new Point(114, 113);
            DOkayButton.Name = "DOkayButton";
            DOkayButton.Size = new Size(100, 28);
            DOkayButton.TabIndex = 0;
            DOkayButton.Text = "OK";
            DOkayButton.UseVisualStyleBackColor = true;
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Location = new Point(12, 10);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(38, 13);
            NameLabel.TabIndex = 1;
            NameLabel.Text = "Name:";
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(12, 26);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(202, 21);
            NameTextBox.TabIndex = 2;
            // 
            // TypeLabel
            // 
            TypeLabel.AutoSize = true;
            TypeLabel.Location = new Point(12, 61);
            TypeLabel.Name = "TypeLabel";
            TypeLabel.Size = new Size(35, 13);
            TypeLabel.TabIndex = 3;
            TypeLabel.Text = "Type:";
            // 
            // TypeDropper
            // 
            TypeDropper.FormattingEnabled = true;
            TypeDropper.Location = new Point(12, 77);
            TypeDropper.Name = "TypeDropper";
            TypeDropper.Size = new Size(202, 21);
            TypeDropper.TabIndex = 4;
            // 
            // Argument
            // 
            AcceptButton = DOkayButton;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(226, 153);
            Controls.Add(TypeDropper);
            Controls.Add(TypeLabel);
            Controls.Add(NameTextBox);
            Controls.Add(NameLabel);
            Controls.Add(DOkayButton);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Argument";
            StartPosition = FormStartPosition.CenterParent;
            Load += new EventHandler(Argument_Load);
            Activated += new EventHandler(Argument_Activated);
            ResumeLayout(false);
            PerformLayout();

        }
        internal Button DOkayButton;
        internal Label NameLabel;
        internal TextBox NameTextBox;
        internal Label TypeLabel;
        internal ComboBox TypeDropper;
    }
}