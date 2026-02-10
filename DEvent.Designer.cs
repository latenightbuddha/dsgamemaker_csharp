using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class DEvent : Form
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
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(DEvent));
            DCancelButton = new Button();
            DCancelButton.Click += new EventHandler(DCancelButton_Click);
            StepEventButton = new Button();
            StepEventButton.Click += new EventHandler(EventButtons_Click);
            CollisionEventButton = new Button();
            CollisionEventButton.Click += new EventHandler(EventButtons_Click);
            TouchEventButton = new Button();
            TouchEventButton.Click += new EventHandler(EventButtons_Click);
            ButtonReleaseEventButton = new Button();
            ButtonReleaseEventButton.Click += new EventHandler(EventButtons_Click);
            ButtonHeldEventButton = new Button();
            ButtonHeldEventButton.Click += new EventHandler(EventButtons_Click);
            ButtonPressEventButton = new Button();
            ButtonPressEventButton.Click += new EventHandler(EventButtons_Click);
            CreateEventButton = new Button();
            CreateEventButton.Click += new EventHandler(EventButtons_Click);
            Dropper = new ContextMenuStrip(components);
            Dropper.ItemClicked += new ToolStripItemClickedEventHandler(EventDropper_ItemClicked);
            SuspendLayout();
            // 
            // DCancelButton
            // 
            DCancelButton.DialogResult = DialogResult.Cancel;
            DCancelButton.ImageAlign = ContentAlignment.MiddleLeft;
            DCancelButton.Location = new Point(12, 150);
            DCancelButton.Name = "DCancelButton";
            DCancelButton.Size = new Size(256, 28);
            DCancelButton.TabIndex = 2;
            DCancelButton.Text = "Cancel";
            DCancelButton.UseVisualStyleBackColor = true;
            // 
            // StepEventButton
            // 
            StepEventButton.Image = DS_Game_Maker.My.Resources.Resources.ClockIcon;
            StepEventButton.ImageAlign = ContentAlignment.MiddleLeft;
            StepEventButton.Location = new Point(12, 114);
            StepEventButton.Name = "StepEventButton";
            StepEventButton.Size = new Size(125, 28);
            StepEventButton.TabIndex = 7;
            StepEventButton.Text = "      Step";
            StepEventButton.TextAlign = ContentAlignment.MiddleLeft;
            StepEventButton.UseVisualStyleBackColor = true;
            // 
            // CollisionEventButton
            // 
            CollisionEventButton.Image = DS_Game_Maker.My.Resources.Resources.Collision;
            CollisionEventButton.ImageAlign = ContentAlignment.MiddleLeft;
            CollisionEventButton.Location = new Point(143, 80);
            CollisionEventButton.Name = "CollisionEventButton";
            CollisionEventButton.Size = new Size(125, 28);
            CollisionEventButton.TabIndex = 6;
            CollisionEventButton.Text = "      Collision";
            CollisionEventButton.TextAlign = ContentAlignment.MiddleLeft;
            CollisionEventButton.UseVisualStyleBackColor = true;
            // 
            // TouchEventButton
            // 
            TouchEventButton.Image = DS_Game_Maker.My.Resources.Resources.StylusIcon;
            TouchEventButton.ImageAlign = ContentAlignment.MiddleLeft;
            TouchEventButton.Location = new Point(12, 80);
            TouchEventButton.Name = "TouchEventButton";
            TouchEventButton.Size = new Size(125, 28);
            TouchEventButton.TabIndex = 5;
            TouchEventButton.Text = "      Touch";
            TouchEventButton.TextAlign = ContentAlignment.MiddleLeft;
            TouchEventButton.UseVisualStyleBackColor = true;
            // 
            // ButtonReleaseEventButton
            // 
            ButtonReleaseEventButton.Image = DS_Game_Maker.My.Resources.Resources.KeyDownIcon1;
            ButtonReleaseEventButton.ImageAlign = ContentAlignment.MiddleLeft;
            ButtonReleaseEventButton.Location = new Point(143, 46);
            ButtonReleaseEventButton.Name = "ButtonReleaseEventButton";
            ButtonReleaseEventButton.Size = new Size(125, 28);
            ButtonReleaseEventButton.TabIndex = 4;
            ButtonReleaseEventButton.Text = "      Button Released";
            ButtonReleaseEventButton.TextAlign = ContentAlignment.MiddleLeft;
            ButtonReleaseEventButton.UseVisualStyleBackColor = true;
            // 
            // ButtonHeldEventButton
            // 
            ButtonHeldEventButton.Image = DS_Game_Maker.My.Resources.Resources.KeyDownIcon;
            ButtonHeldEventButton.ImageAlign = ContentAlignment.MiddleLeft;
            ButtonHeldEventButton.Location = new Point(12, 46);
            ButtonHeldEventButton.Name = "ButtonHeldEventButton";
            ButtonHeldEventButton.Size = new Size(125, 28);
            ButtonHeldEventButton.TabIndex = 3;
            ButtonHeldEventButton.Text = "      Button Held";
            ButtonHeldEventButton.TextAlign = ContentAlignment.MiddleLeft;
            ButtonHeldEventButton.UseVisualStyleBackColor = true;
            // 
            // ButtonPressEventButton
            // 
            ButtonPressEventButton.Image = DS_Game_Maker.My.Resources.Resources.KeyIcon;
            ButtonPressEventButton.ImageAlign = ContentAlignment.MiddleLeft;
            ButtonPressEventButton.Location = new Point(143, 12);
            ButtonPressEventButton.Name = "ButtonPressEventButton";
            ButtonPressEventButton.Size = new Size(125, 28);
            ButtonPressEventButton.TabIndex = 1;
            ButtonPressEventButton.Text = "      Button Press";
            ButtonPressEventButton.TextAlign = ContentAlignment.MiddleLeft;
            ButtonPressEventButton.UseVisualStyleBackColor = true;
            // 
            // CreateEventButton
            // 
            CreateEventButton.Image = DS_Game_Maker.My.Resources.Resources.CreateEventIcon;
            CreateEventButton.ImageAlign = ContentAlignment.MiddleLeft;
            CreateEventButton.Location = new Point(12, 12);
            CreateEventButton.Name = "CreateEventButton";
            CreateEventButton.Size = new Size(125, 28);
            CreateEventButton.TabIndex = 0;
            CreateEventButton.Text = "      Create";
            CreateEventButton.TextAlign = ContentAlignment.MiddleLeft;
            CreateEventButton.UseVisualStyleBackColor = true;
            // 
            // Dropper
            // 
            Dropper.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Dropper.Name = "EventDropper";
            Dropper.RenderMode = ToolStripRenderMode.System;
            Dropper.Size = new Size(61, 4);
            // 
            // DEvent
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = DCancelButton;
            ClientSize = new Size(280, 190);
            Controls.Add(StepEventButton);
            Controls.Add(CollisionEventButton);
            Controls.Add(TouchEventButton);
            Controls.Add(ButtonReleaseEventButton);
            Controls.Add(ButtonHeldEventButton);
            Controls.Add(DCancelButton);
            Controls.Add(ButtonPressEventButton);
            Controls.Add(CreateEventButton);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DEvent";
            StartPosition = FormStartPosition.CenterParent;
            Load += new EventHandler(DEvent_Load);
            FormClosing += new FormClosingEventHandler(DEvent_FormClosing);
            ResumeLayout(false);

        }
        internal Button CreateEventButton;
        internal Button ButtonPressEventButton;
        internal Button DCancelButton;
        internal Button ButtonHeldEventButton;
        internal Button ButtonReleaseEventButton;
        internal Button TouchEventButton;
        internal Button CollisionEventButton;
        internal Button StepEventButton;
        internal ContextMenuStrip Dropper;
    }
}