using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class FormAction : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(DS_Game_Maker.Action));
            SubPanel = new Panel();
            DOkayButton = new Button();
            AppliesToGroupBox = new GroupBox();
            InstancesTextBox = new TextBox();
            InstancesOfDropper = new ComboBox();
            InstancesRadioButton = new RadioButton();
            InstancesOfRadioButton = new RadioButton();
            ThisRadioButton = new RadioButton();
            LabelsPanel = new Panel();
            ControlsPanel = new Panel();
            SubPanel.SuspendLayout();
            AppliesToGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // SubPanel
            // 
            SubPanel.Controls.Add(DOkayButton);
            SubPanel.Dock = DockStyle.Bottom;
            SubPanel.Location = new Point(0, 272);
            SubPanel.Name = "SubPanel";
            SubPanel.Size = new Size(259, 40);
            SubPanel.TabIndex = 0;
            // 
            // DOkayButton
            // 
            DOkayButton.ImageAlign = ContentAlignment.MiddleLeft;
            DOkayButton.Location = new Point(148, 5);
            DOkayButton.Name = "DOkayButton";
            DOkayButton.Size = new Size(104, 26);
            DOkayButton.TabIndex = 1;
            DOkayButton.Text = "Okay";
            DOkayButton.UseVisualStyleBackColor = true;
            // 
            // AppliesToGroupBox
            // 
            AppliesToGroupBox.Controls.Add(InstancesTextBox);
            AppliesToGroupBox.Controls.Add(InstancesOfDropper);
            AppliesToGroupBox.Controls.Add(InstancesRadioButton);
            AppliesToGroupBox.Controls.Add(InstancesOfRadioButton);
            AppliesToGroupBox.Controls.Add(ThisRadioButton);
            AppliesToGroupBox.Location = new Point(7, 7);
            AppliesToGroupBox.Name = "AppliesToGroupBox";
            AppliesToGroupBox.Size = new Size(245, 90);
            AppliesToGroupBox.TabIndex = 1;
            AppliesToGroupBox.TabStop = false;
            AppliesToGroupBox.Text = "Applies To";
            // 
            // InstancesTextBox
            // 
            InstancesTextBox.Location = new Point(120, 62);
            InstancesTextBox.Name = "InstancesTextBox";
            InstancesTextBox.Size = new Size(119, 21);
            InstancesTextBox.TabIndex = 0;
            // 
            // InstancesOfDropper
            // 
            InstancesOfDropper.FormattingEnabled = true;
            InstancesOfDropper.Location = new Point(120, 39);
            InstancesOfDropper.Name = "InstancesOfDropper";
            InstancesOfDropper.Size = new Size(119, 21);
            InstancesOfDropper.TabIndex = 2;
            // 
            // InstancesRadioButton
            // 
            InstancesRadioButton.AutoSize = true;
            InstancesRadioButton.Location = new Point(11, 63);
            InstancesRadioButton.Name = "InstancesRadioButton";
            InstancesRadioButton.Size = new Size(76, 17);
            InstancesRadioButton.TabIndex = 4;
            InstancesRadioButton.TabStop = true;
            InstancesRadioButton.Text = "Instances:";
            InstancesRadioButton.UseVisualStyleBackColor = true;
            // 
            // InstancesOfRadioButton
            // 
            InstancesOfRadioButton.AutoSize = true;
            InstancesOfRadioButton.Location = new Point(11, 40);
            InstancesOfRadioButton.Name = "InstancesOfRadioButton";
            InstancesOfRadioButton.Size = new Size(89, 17);
            InstancesOfRadioButton.TabIndex = 3;
            InstancesOfRadioButton.TabStop = true;
            InstancesOfRadioButton.Text = "Instances of:";
            InstancesOfRadioButton.UseVisualStyleBackColor = true;
            // 
            // ThisRadioButton
            // 
            ThisRadioButton.AutoSize = true;
            ThisRadioButton.Location = new Point(11, 18);
            ThisRadioButton.Name = "ThisRadioButton";
            ThisRadioButton.Size = new Size(44, 17);
            ThisRadioButton.TabIndex = 2;
            ThisRadioButton.TabStop = true;
            ThisRadioButton.Text = "This";
            ThisRadioButton.UseVisualStyleBackColor = true;
            // 
            // LabelsPanel
            // 
            LabelsPanel.Location = new Point(7, 108);
            LabelsPanel.Name = "LabelsPanel";
            LabelsPanel.Size = new Size(112, 158);
            LabelsPanel.TabIndex = 2;
            // 
            // ControlsPanel
            // 
            ControlsPanel.Location = new Point(122, 108);
            ControlsPanel.Name = "ControlsPanel";
            ControlsPanel.Size = new Size(128, 158);
            ControlsPanel.TabIndex = 3;
            // 
            // Action
            // 
            AcceptButton = DOkayButton;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(259, 312);
            Controls.Add(ControlsPanel);
            Controls.Add(LabelsPanel);
            Controls.Add(AppliesToGroupBox);
            Controls.Add(SubPanel);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Action";
            StartPosition = FormStartPosition.CenterParent;
            SubPanel.ResumeLayout(false);
            AppliesToGroupBox.ResumeLayout(false);
            AppliesToGroupBox.PerformLayout();
            ResumeLayout(false);

        }
        internal Panel SubPanel;
        internal Button DOkayButton;
        internal GroupBox AppliesToGroupBox;
        internal RadioButton InstancesRadioButton;
        internal RadioButton InstancesOfRadioButton;
        internal RadioButton ThisRadioButton;
        internal ComboBox InstancesOfDropper;
        internal Panel LabelsPanel;
        internal Panel ControlsPanel;
        internal TextBox InstancesTextBox;
    }
}