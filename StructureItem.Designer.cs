using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class StructureItem : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(StructureItem));
            MainToolStrip = new ToolStrip();
            DAcceptButton = new ToolStripButton();
            DAcceptButton.Click += new EventHandler(DAcceptButton_Click);
            TypeDropper = new ComboBox();
            TypeDropper.TextChanged += new EventHandler(TextBoxs_TextChanged);
            TypeLabel = new Label();
            NameTextBox = new TextBox();
            NameTextBox.TextChanged += new EventHandler(TextBoxs_TextChanged);
            NameLabel = new Label();
            ValueTextBox = new TextBox();
            ValueLabel = new Label();
            ToolStripContainer = new Panel();
            MainToolStrip.SuspendLayout();
            ToolStripContainer.SuspendLayout();
            SuspendLayout();
            // 
            // MainToolStrip
            // 
            MainToolStrip.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainToolStrip.Items.AddRange(new ToolStripItem[] { DAcceptButton });
            MainToolStrip.Location = new Point(0, 0);
            MainToolStrip.Name = "MainToolStrip";
            MainToolStrip.Size = new Size(153, 25);
            MainToolStrip.TabIndex = 0;
            MainToolStrip.Text = "ToolStrip1";
            // 
            // DAcceptButton
            // 
            DAcceptButton.BackgroundImageLayout = ImageLayout.None;
            DAcceptButton.Image = DS_Game_Maker.My.Resources.Resources.AcceptIcon;
            DAcceptButton.ImageTransparentColor = Color.Magenta;
            DAcceptButton.Name = "DAcceptButton";
            DAcceptButton.Size = new Size(60, 22);
            DAcceptButton.Text = "Accept";
            // 
            // TypeDropper
            // 
            TypeDropper.FormattingEnabled = true;
            TypeDropper.Location = new Point(11, 89);
            TypeDropper.Name = "TypeDropper";
            TypeDropper.Size = new Size(129, 21);
            TypeDropper.TabIndex = 8;
            // 
            // TypeLabel
            // 
            TypeLabel.AutoSize = true;
            TypeLabel.ForeColor = Color.White;
            TypeLabel.Location = new Point(9, 73);
            TypeLabel.Name = "TypeLabel";
            TypeLabel.Size = new Size(35, 13);
            TypeLabel.TabIndex = 7;
            TypeLabel.Text = "Type:";
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(11, 49);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(129, 21);
            NameTextBox.TabIndex = 6;
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.ForeColor = Color.White;
            NameLabel.Location = new Point(9, 33);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(38, 13);
            NameLabel.TabIndex = 5;
            NameLabel.Text = "Name:";
            // 
            // ValueTextBox
            // 
            ValueTextBox.Location = new Point(12, 129);
            ValueTextBox.Name = "ValueTextBox";
            ValueTextBox.Size = new Size(129, 21);
            ValueTextBox.TabIndex = 10;
            // 
            // ValueLabel
            // 
            ValueLabel.AutoSize = true;
            ValueLabel.ForeColor = Color.White;
            ValueLabel.Location = new Point(9, 113);
            ValueLabel.Name = "ValueLabel";
            ValueLabel.Size = new Size(37, 13);
            ValueLabel.TabIndex = 9;
            ValueLabel.Text = "Value:";
            // 
            // ToolStripContainer
            // 
            ToolStripContainer.BackColor = Color.Silver;
            ToolStripContainer.Controls.Add(MainToolStrip);
            ToolStripContainer.Dock = DockStyle.Top;
            ToolStripContainer.Location = new Point(0, 0);
            ToolStripContainer.Name = "ToolStripContainer";
            ToolStripContainer.Size = new Size(153, 25);
            ToolStripContainer.TabIndex = 11;
            // 
            // StructureItem
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(153, 162);
            Controls.Add(ToolStripContainer);
            Controls.Add(ValueTextBox);
            Controls.Add(ValueLabel);
            Controls.Add(TypeDropper);
            Controls.Add(TypeLabel);
            Controls.Add(NameTextBox);
            Controls.Add(NameLabel);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StructureItem";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit Member";
            MainToolStrip.ResumeLayout(false);
            MainToolStrip.PerformLayout();
            ToolStripContainer.ResumeLayout(false);
            ToolStripContainer.PerformLayout();
            Load += new EventHandler(StructureItem_Load);
            Shown += new EventHandler(StructureItem_Shown);
            ResumeLayout(false);
            PerformLayout();

        }
        internal ToolStrip MainToolStrip;
        internal ToolStripButton DAcceptButton;
        internal ComboBox TypeDropper;
        internal Label TypeLabel;
        internal TextBox NameTextBox;
        internal Label NameLabel;
        internal TextBox ValueTextBox;
        internal Label ValueLabel;
        internal Panel ToolStripContainer;
    }
}