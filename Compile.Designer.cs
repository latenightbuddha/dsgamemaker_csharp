using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Compile : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Compile));
            ProgressLabel = new Label();
            MainProgressBar = new ProgressBar();
            SuspendLayout();
            // 
            // ProgressLabel
            // 
            ProgressLabel.AutoSize = true;
            ProgressLabel.Location = new Point(11, 9);
            ProgressLabel.Name = "ProgressLabel";
            ProgressLabel.Size = new Size(0, 13);
            ProgressLabel.TabIndex = 0;
            // 
            // MainProgressBar
            // 
            MainProgressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MainProgressBar.Location = new Point(12, 9);
            MainProgressBar.Name = "MainProgressBar";
            MainProgressBar.Size = new Size(280, 17);
            MainProgressBar.Step = 25;
            MainProgressBar.TabIndex = 1;
            // 
            // Compile
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(304, 36);
            Controls.Add(MainProgressBar);
            Controls.Add(ProgressLabel);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Compile";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Creating Game...";
            Shown += new EventHandler(Compile_Shown);
            ResumeLayout(false);
            PerformLayout();

        }
        internal Label ProgressLabel;
        internal ProgressBar MainProgressBar;
    }
}