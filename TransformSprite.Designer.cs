using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class TransformSprite : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(TransformSprite));
            MainTabControl = new TabControl();
            FlipAndRotateTabPage = new TabPage();
            HorizontalRadioButton = new RadioButton();
            VerticalRadioButton = new RadioButton();
            ReplaceColorTab = new TabPage();
            WithThisLabel = new Label();
            ReplaceColorButton = new Button();
            ReplaceColorButton.Click += new EventHandler(ReplaceButtonColor_Click);
            ReplaceColorLabel = new Label();
            OriginalColorButton = new Button();
            OriginalColorButton.Click += new EventHandler(OriginalColorButton_Click);
            SubPanel = new Panel();
            DOkayButton = new Button();
            DOkayButton.Click += new EventHandler(DOkayButton_Click);
            FlipGroupBox = new GroupBox();
            NoneRadioButton = new RadioButton();
            BothRadioButton = new RadioButton();
            RotationLabel = new Label();
            RotationDropper = new ComboBox();
            DegreesLabel = new Label();
            MainTabControl.SuspendLayout();
            FlipAndRotateTabPage.SuspendLayout();
            ReplaceColorTab.SuspendLayout();
            SubPanel.SuspendLayout();
            FlipGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // MainTabControl
            // 
            MainTabControl.Controls.Add(FlipAndRotateTabPage);
            MainTabControl.Controls.Add(ReplaceColorTab);
            MainTabControl.Dock = DockStyle.Fill;
            MainTabControl.Location = new Point(0, 0);
            MainTabControl.Name = "MainTabControl";
            MainTabControl.SelectedIndex = 0;
            MainTabControl.Size = new Size(284, 220);
            MainTabControl.TabIndex = 0;
            // 
            // FlipAndRotateTabPage
            // 
            FlipAndRotateTabPage.Controls.Add(DegreesLabel);
            FlipAndRotateTabPage.Controls.Add(RotationDropper);
            FlipAndRotateTabPage.Controls.Add(RotationLabel);
            FlipAndRotateTabPage.Controls.Add(FlipGroupBox);
            FlipAndRotateTabPage.Location = new Point(4, 22);
            FlipAndRotateTabPage.Name = "FlipAndRotateTabPage";
            FlipAndRotateTabPage.Padding = new Padding(3);
            FlipAndRotateTabPage.Size = new Size(276, 194);
            FlipAndRotateTabPage.TabIndex = 0;
            FlipAndRotateTabPage.Text = "Flip && Rotate";
            FlipAndRotateTabPage.UseVisualStyleBackColor = true;
            // 
            // HorizontalRadioButton
            // 
            HorizontalRadioButton.AutoSize = true;
            HorizontalRadioButton.Location = new Point(12, 43);
            HorizontalRadioButton.Name = "HorizontalRadioButton";
            HorizontalRadioButton.Size = new Size(73, 17);
            HorizontalRadioButton.TabIndex = 1;
            HorizontalRadioButton.Text = "Horizontal";
            HorizontalRadioButton.UseVisualStyleBackColor = true;
            // 
            // VerticalRadioButton
            // 
            VerticalRadioButton.AutoSize = true;
            VerticalRadioButton.Location = new Point(12, 66);
            VerticalRadioButton.Name = "VerticalRadioButton";
            VerticalRadioButton.Size = new Size(60, 17);
            VerticalRadioButton.TabIndex = 0;
            VerticalRadioButton.Text = "Vertical";
            VerticalRadioButton.UseVisualStyleBackColor = true;
            // 
            // ReplaceColorTab
            // 
            ReplaceColorTab.Controls.Add(WithThisLabel);
            ReplaceColorTab.Controls.Add(ReplaceColorButton);
            ReplaceColorTab.Controls.Add(ReplaceColorLabel);
            ReplaceColorTab.Controls.Add(OriginalColorButton);
            ReplaceColorTab.Location = new Point(4, 22);
            ReplaceColorTab.Name = "ReplaceColorTab";
            ReplaceColorTab.Padding = new Padding(3);
            ReplaceColorTab.Size = new Size(276, 194);
            ReplaceColorTab.TabIndex = 2;
            ReplaceColorTab.Text = "Replace Color";
            ReplaceColorTab.UseVisualStyleBackColor = true;
            // 
            // WithThisLabel
            // 
            WithThisLabel.AutoSize = true;
            WithThisLabel.Location = new Point(69, 87);
            WithThisLabel.Name = "WithThisLabel";
            WithThisLabel.Size = new Size(51, 13);
            WithThisLabel.TabIndex = 3;
            WithThisLabel.Text = "With This";
            // 
            // ReplaceColorButton
            // 
            ReplaceColorButton.BackColor = Color.White;
            ReplaceColorButton.Location = new Point(11, 78);
            ReplaceColorButton.Name = "ReplaceColorButton";
            ReplaceColorButton.Size = new Size(52, 52);
            ReplaceColorButton.TabIndex = 2;
            ReplaceColorButton.UseVisualStyleBackColor = false;
            // 
            // ReplaceColorLabel
            // 
            ReplaceColorLabel.AutoSize = true;
            ReplaceColorLabel.Location = new Point(69, 29);
            ReplaceColorLabel.Name = "ReplaceColorLabel";
            ReplaceColorLabel.Size = new Size(93, 13);
            ReplaceColorLabel.TabIndex = 1;
            ReplaceColorLabel.Text = "Replace this Color";
            // 
            // OriginalColorButton
            // 
            OriginalColorButton.BackColor = Color.Black;
            OriginalColorButton.Location = new Point(11, 20);
            OriginalColorButton.Name = "OriginalColorButton";
            OriginalColorButton.Size = new Size(52, 52);
            OriginalColorButton.TabIndex = 0;
            OriginalColorButton.UseVisualStyleBackColor = false;
            // 
            // SubPanel
            // 
            SubPanel.Controls.Add(DOkayButton);
            SubPanel.Dock = DockStyle.Bottom;
            SubPanel.Location = new Point(0, 220);
            SubPanel.Name = "SubPanel";
            SubPanel.Size = new Size(284, 42);
            SubPanel.TabIndex = 1;
            // 
            // DOkayButton
            // 
            DOkayButton.Location = new Point(190, 6);
            DOkayButton.Name = "DOkayButton";
            DOkayButton.Size = new Size(84, 26);
            DOkayButton.TabIndex = 0;
            DOkayButton.Text = "Okay";
            DOkayButton.UseVisualStyleBackColor = true;
            // 
            // FlipGroupBox
            // 
            FlipGroupBox.Controls.Add(BothRadioButton);
            FlipGroupBox.Controls.Add(NoneRadioButton);
            FlipGroupBox.Controls.Add(HorizontalRadioButton);
            FlipGroupBox.Controls.Add(VerticalRadioButton);
            FlipGroupBox.Location = new Point(8, 6);
            FlipGroupBox.Name = "FlipGroupBox";
            FlipGroupBox.Size = new Size(262, 123);
            FlipGroupBox.TabIndex = 2;
            FlipGroupBox.TabStop = false;
            FlipGroupBox.Text = "Flip";
            // 
            // NoneRadioButton
            // 
            NoneRadioButton.AutoSize = true;
            NoneRadioButton.Checked = true;
            NoneRadioButton.Location = new Point(12, 20);
            NoneRadioButton.Name = "NoneRadioButton";
            NoneRadioButton.Size = new Size(50, 17);
            NoneRadioButton.TabIndex = 2;
            NoneRadioButton.Text = "None";
            NoneRadioButton.UseVisualStyleBackColor = true;
            // 
            // BothRadioButton
            // 
            BothRadioButton.AutoSize = true;
            BothRadioButton.Location = new Point(12, 89);
            BothRadioButton.Name = "BothRadioButton";
            BothRadioButton.Size = new Size(146, 17);
            BothRadioButton.TabIndex = 3;
            BothRadioButton.Text = "Both Horizontal && Vertical";
            BothRadioButton.UseVisualStyleBackColor = true;
            // 
            // RotationLabel
            // 
            RotationLabel.AutoSize = true;
            RotationLabel.Location = new Point(91, 141);
            RotationLabel.Name = "RotationLabel";
            RotationLabel.Size = new Size(59, 13);
            RotationLabel.TabIndex = 3;
            RotationLabel.Text = "Rotate By:";
            // 
            // RotationDropper
            // 
            RotationDropper.FormattingEnabled = true;
            RotationDropper.Items.AddRange(new object[] { "0", "90", "180", "270" });
            RotationDropper.Location = new Point(172, 138);
            RotationDropper.Name = "RotationDropper";
            RotationDropper.Size = new Size(44, 21);
            RotationDropper.TabIndex = 4;
            RotationDropper.Text = "0";
            // 
            // DegreesLabel
            // 
            DegreesLabel.AutoSize = true;
            DegreesLabel.Location = new Point(222, 141);
            DegreesLabel.Name = "DegreesLabel";
            DegreesLabel.Size = new Size(46, 13);
            DegreesLabel.TabIndex = 5;
            DegreesLabel.Text = "degrees";
            // 
            // TransformSprite
            // 
            AcceptButton = DOkayButton;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 262);
            Controls.Add(MainTabControl);
            Controls.Add(SubPanel);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TransformSprite";
            StartPosition = FormStartPosition.CenterParent;
            MainTabControl.ResumeLayout(false);
            FlipAndRotateTabPage.ResumeLayout(false);
            FlipAndRotateTabPage.PerformLayout();
            ReplaceColorTab.ResumeLayout(false);
            ReplaceColorTab.PerformLayout();
            SubPanel.ResumeLayout(false);
            FlipGroupBox.ResumeLayout(false);
            FlipGroupBox.PerformLayout();
            Load += new EventHandler(TransformSprite_Load);
            ResumeLayout(false);

        }
        internal TabControl MainTabControl;
        internal TabPage FlipAndRotateTabPage;
        internal Panel SubPanel;
        internal Button DOkayButton;
        internal RadioButton HorizontalRadioButton;
        internal RadioButton VerticalRadioButton;
        internal TabPage ReplaceColorTab;
        internal Label WithThisLabel;
        internal Button ReplaceColorButton;
        internal Label ReplaceColorLabel;
        internal Button OriginalColorButton;
        internal GroupBox FlipGroupBox;
        internal RadioButton BothRadioButton;
        internal RadioButton NoneRadioButton;
        internal ComboBox RotationDropper;
        internal Label RotationLabel;
        internal Label DegreesLabel;
    }
}