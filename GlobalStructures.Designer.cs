using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class GlobalStructures : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(GlobalStructures));
            MembersLabel = new Label();
            TypeHeader = new Panel();
            MembersList = new ListBox();
            MembersList.MeasureItem += new MeasureItemEventHandler(MembersList_MeasureItem);
            MembersList.DrawItem += new DrawItemEventHandler(MembersList_DrawItem);
            NameTextBox = new TextBox();
            NameTextBox.TextChanged += new EventHandler(NameTextBox_TextChanged);
            AddButton = new Button();
            AddButton.Click += new EventHandler(AddButton_Click);
            NamePanel = new Panel();
            StructuresList = new ListBox();
            StructuresList.MeasureItem += new MeasureItemEventHandler(StructuresList_MeasureItem);
            StructuresList.DrawItem += new DrawItemEventHandler(StructuresList_DrawItem);
            StructuresList.SelectedIndexChanged += new EventHandler(StructuresList_SelectedIndexChanged);
            RemoveButton = new Button();
            RemoveButton.Click += new EventHandler(RemoveButton_Click);
            SidePanel = new Panel();
            NameLabel = new Label();
            TypeLabel = new Label();
            ValueLabel = new Label();
            AddMemberButton = new Button();
            AddMemberButton.Click += new EventHandler(AddMemberButton_Click);
            EditMemberButton = new Button();
            EditMemberButton.Click += new EventHandler(EditMemberButton_Click);
            DeleteMemberButton = new Button();
            DeleteMemberButton.Click += new EventHandler(DeleteMemberButton_Click);
            TypeHeader.SuspendLayout();
            NamePanel.SuspendLayout();
            SidePanel.SuspendLayout();
            SuspendLayout();
            // 
            // MembersLabel
            // 
            MembersLabel.AutoSize = true;
            MembersLabel.Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            MembersLabel.ForeColor = Color.White;
            MembersLabel.Location = new Point(3, 3);
            MembersLabel.Name = "MembersLabel";
            MembersLabel.Size = new Size(85, 14);
            MembersLabel.TabIndex = 4;
            MembersLabel.Text = "M e m b e r s :";
            // 
            // TypeHeader
            // 
            TypeHeader.BackColor = Color.Silver;
            TypeHeader.Controls.Add(MembersLabel);
            TypeHeader.Location = new Point(194, 37);
            TypeHeader.Name = "TypeHeader";
            TypeHeader.Size = new Size(212, 22);
            TypeHeader.TabIndex = 23;
            // 
            // MembersList
            // 
            MembersList.DrawMode = DrawMode.OwnerDrawVariable;
            MembersList.FormattingEnabled = true;
            MembersList.Location = new Point(194, 82);
            MembersList.Name = "MembersList";
            MembersList.Size = new Size(212, 108);
            MembersList.TabIndex = 22;
            // 
            // NameTextBox
            // 
            NameTextBox.BackColor = Color.FromArgb(64, 64, 64);
            NameTextBox.BorderStyle = BorderStyle.None;
            NameTextBox.Font = new Font("Arial", 12.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            NameTextBox.ForeColor = Color.White;
            NameTextBox.Location = new Point(7, 7);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(160, 19);
            NameTextBox.TabIndex = 15;
            // 
            // AddButton
            // 
            AddButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            AddButton.Location = new Point(3, 332);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(184, 30);
            AddButton.TabIndex = 11;
            AddButton.Text = "Add Global Structure";
            AddButton.UseVisualStyleBackColor = true;
            // 
            // NamePanel
            // 
            NamePanel.BackColor = Color.FromArgb(64, 64, 64);
            NamePanel.Controls.Add(NameTextBox);
            NamePanel.Dock = DockStyle.Top;
            NamePanel.Location = new Point(190, 0);
            NamePanel.Name = "NamePanel";
            NamePanel.Size = new Size(220, 33);
            NamePanel.TabIndex = 21;
            // 
            // StructuresList
            // 
            StructuresList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            StructuresList.DrawMode = DrawMode.OwnerDrawVariable;
            StructuresList.FormattingEnabled = true;
            StructuresList.Location = new Point(0, 0);
            StructuresList.Name = "StructuresList";
            StructuresList.Size = new Size(190, 329);
            StructuresList.TabIndex = 13;
            // 
            // RemoveButton
            // 
            RemoveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            RemoveButton.Location = new Point(3, 364);
            RemoveButton.Name = "RemoveButton";
            RemoveButton.Size = new Size(184, 30);
            RemoveButton.TabIndex = 12;
            RemoveButton.Text = "Remove";
            RemoveButton.UseVisualStyleBackColor = true;
            // 
            // SidePanel
            // 
            SidePanel.BackColor = Color.FromArgb(64, 64, 64);
            SidePanel.Controls.Add(StructuresList);
            SidePanel.Controls.Add(RemoveButton);
            SidePanel.Controls.Add(AddButton);
            SidePanel.Dock = DockStyle.Left;
            SidePanel.Location = new Point(0, 0);
            SidePanel.Name = "SidePanel";
            SidePanel.Size = new Size(190, 398);
            SidePanel.TabIndex = 20;
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.ForeColor = Color.FromArgb(64, 64, 64);
            NameLabel.Location = new Point(191, 66);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(34, 13);
            NameLabel.TabIndex = 24;
            NameLabel.Text = "Name";
            // 
            // TypeLabel
            // 
            TypeLabel.AutoSize = true;
            TypeLabel.ForeColor = Color.FromArgb(64, 64, 64);
            TypeLabel.Location = new Point(264, 66);
            TypeLabel.Name = "TypeLabel";
            TypeLabel.Size = new Size(31, 13);
            TypeLabel.TabIndex = 25;
            TypeLabel.Text = "Type";
            // 
            // ValueLabel
            // 
            ValueLabel.AutoSize = true;
            ValueLabel.ForeColor = Color.FromArgb(64, 64, 64);
            ValueLabel.Location = new Point(335, 66);
            ValueLabel.Name = "ValueLabel";
            ValueLabel.Size = new Size(33, 13);
            ValueLabel.TabIndex = 26;
            ValueLabel.Text = "Value";
            // 
            // AddMemberButton
            // 
            AddMemberButton.Location = new Point(194, 191);
            AddMemberButton.Name = "AddMemberButton";
            AddMemberButton.Size = new Size(60, 24);
            AddMemberButton.TabIndex = 27;
            AddMemberButton.Text = "Add";
            AddMemberButton.UseVisualStyleBackColor = true;
            // 
            // EditMemberButton
            // 
            EditMemberButton.Location = new Point(254, 191);
            EditMemberButton.Name = "EditMemberButton";
            EditMemberButton.Size = new Size(60, 24);
            EditMemberButton.TabIndex = 28;
            EditMemberButton.Text = "Edit";
            EditMemberButton.UseVisualStyleBackColor = true;
            // 
            // DeleteMemberButton
            // 
            DeleteMemberButton.Location = new Point(314, 191);
            DeleteMemberButton.Name = "DeleteMemberButton";
            DeleteMemberButton.Size = new Size(60, 24);
            DeleteMemberButton.TabIndex = 29;
            DeleteMemberButton.Text = "Delete";
            DeleteMemberButton.UseVisualStyleBackColor = true;
            // 
            // GlobalStructures
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(410, 398);
            Controls.Add(DeleteMemberButton);
            Controls.Add(EditMemberButton);
            Controls.Add(AddMemberButton);
            Controls.Add(ValueLabel);
            Controls.Add(TypeLabel);
            Controls.Add(NameLabel);
            Controls.Add(TypeHeader);
            Controls.Add(MembersList);
            Controls.Add(NamePanel);
            Controls.Add(SidePanel);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "GlobalStructures";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Global Structures";
            TypeHeader.ResumeLayout(false);
            TypeHeader.PerformLayout();
            NamePanel.ResumeLayout(false);
            NamePanel.PerformLayout();
            SidePanel.ResumeLayout(false);
            Load += new EventHandler(GlobalStructures_Load);
            FormClosing += new FormClosingEventHandler(GlobalStructures_FormClosing);
            ResumeLayout(false);
            PerformLayout();

        }
        internal Label MembersLabel;
        internal Panel TypeHeader;
        internal ListBox MembersList;
        internal TextBox NameTextBox;
        internal Button AddButton;
        internal Panel NamePanel;
        internal ListBox StructuresList;
        internal Button RemoveButton;
        internal Panel SidePanel;
        internal Label NameLabel;
        internal Label TypeLabel;
        internal Label ValueLabel;
        internal Button AddMemberButton;
        internal Button EditMemberButton;
        internal Button DeleteMemberButton;
    }
}