using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class SetCoOrdinates : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(SetCoOrdinates));
            XLabel = new Label();
            YLabel = new Label();
            XTextBox = new TextBox();
            XTextBox.TextChanged += new EventHandler(TextBoxs_TextChanged);
            XTextBox.KeyPress += new KeyPressEventHandler(XTextBox_KeyPress);
            YTextBox = new TextBox();
            YTextBox.TextChanged += new EventHandler(TextBoxs_TextChanged);
            YTextBox.KeyPress += new KeyPressEventHandler(XTextBox_KeyPress);
            DAcceptButton = new Button();
            DAcceptButton.Click += new EventHandler(DAcceptButton_Click);
            SuspendLayout();
            // 
            // XLabel
            // 
            XLabel.AutoSize = true;
            XLabel.Location = new Point(12, 11);
            XLabel.Name = "XLabel";
            XLabel.Size = new Size(17, 13);
            XLabel.TabIndex = 3;
            XLabel.Text = "X:";
            // 
            // YLabel
            // 
            YLabel.AutoSize = true;
            YLabel.Location = new Point(12, 36);
            YLabel.Name = "YLabel";
            YLabel.Size = new Size(17, 13);
            YLabel.TabIndex = 4;
            YLabel.Text = "Y:";
            // 
            // XTextBox
            // 
            XTextBox.Location = new Point(100, 8);
            XTextBox.Name = "XTextBox";
            XTextBox.Size = new Size(76, 21);
            XTextBox.TabIndex = 0;
            // 
            // YTextBox
            // 
            YTextBox.Location = new Point(100, 33);
            YTextBox.Name = "YTextBox";
            YTextBox.Size = new Size(76, 21);
            YTextBox.TabIndex = 1;
            // 
            // DAcceptButton
            // 
            DAcceptButton.Image = DS_Game_Maker.My.Resources.Resources.AcceptIcon;
            DAcceptButton.ImageAlign = ContentAlignment.MiddleLeft;
            DAcceptButton.Location = new Point(12, 62);
            DAcceptButton.Name = "DAcceptButton";
            DAcceptButton.Size = new Size(164, 30);
            DAcceptButton.TabIndex = 2;
            DAcceptButton.Text = "Accept";
            DAcceptButton.UseVisualStyleBackColor = true;
            // 
            // SetCoOrdinates
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(188, 104);
            Controls.Add(DAcceptButton);
            Controls.Add(YTextBox);
            Controls.Add(XTextBox);
            Controls.Add(YLabel);
            Controls.Add(XLabel);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SetCoOrdinates";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Set Co-ordinates";
            Load += new EventHandler(SetCoOrdinates_Load);
            Shown += new EventHandler(SetCoOrdinates_Shown);
            ResumeLayout(false);
            PerformLayout();

        }
        internal Label XLabel;
        internal Label YLabel;
        internal TextBox XTextBox;
        internal TextBox YTextBox;
        internal Button DAcceptButton;
    }
}