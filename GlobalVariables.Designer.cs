using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class GlobalVariables : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(GlobalVariables));
            TypeLabel = new Label();
            ValueTextBox = new TextBox();
            ValueTextBox.TextChanged += new EventHandler(ValueTextBox_TextChanged);
            AddButton = new Button();
            AddButton.Click += new EventHandler(AddButton_Click);
            SidePanel = new Panel();
            VariablesList = new ListBox();
            VariablesList.SelectedIndexChanged += new EventHandler(VariablesList_SelectedIndexChanged);
            VariablesList.MeasureItem += new MeasureItemEventHandler(VariablesList_MeasureItem);
            VariablesList.DrawItem += new DrawItemEventHandler(VariablesList_DrawItem);
            RemoveButton = new Button();
            RemoveButton.Click += new EventHandler(RemoveButton_Click);
            NamePanel = new Panel();
            NameTextBox = new TextBox();
            NameTextBox.TextChanged += new EventHandler(NameTextBox_TextChanged);
            TypeList = new ListBox();
            TypeList.SelectedIndexChanged += new EventHandler(TypeList_SelectedIndexChanged);
            TypeList.MeasureItem += new MeasureItemEventHandler(TypeList_MeasureItem);
            TypeList.DrawItem += new DrawItemEventHandler(TypeList_DrawItem);
            TypeHeader = new Panel();
            TypeDescriptionLabel = new Label();
            TypeTitleLabel = new Label();
            ValueHeader = new Panel();
            ValueLabel = new Label();
            Panel1 = new Panel();
            SidePanel.SuspendLayout();
            NamePanel.SuspendLayout();
            TypeHeader.SuspendLayout();
            ValueHeader.SuspendLayout();
            Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // TypeLabel
            // 
            TypeLabel.AutoSize = true;
            TypeLabel.Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            TypeLabel.ForeColor = Color.White;
            TypeLabel.Location = new Point(3, 3);
            TypeLabel.Name = "TypeLabel";
            TypeLabel.Size = new Size(49, 14);
            TypeLabel.TabIndex = 4;
            TypeLabel.Text = "T y p e :";
            // 
            // ValueTextBox
            // 
            ValueTextBox.BorderStyle = BorderStyle.None;
            ValueTextBox.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ValueTextBox.Location = new Point(5, 7);
            ValueTextBox.Name = "ValueTextBox";
            ValueTextBox.Size = new Size(43, 15);
            ValueTextBox.TabIndex = 9;
            // 
            // AddButton
            // 
            AddButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            AddButton.Location = new Point(3, 332);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(184, 30);
            AddButton.TabIndex = 11;
            AddButton.Text = "Add Global Variable";
            AddButton.UseVisualStyleBackColor = true;
            // 
            // SidePanel
            // 
            SidePanel.BackColor = Color.FromArgb(64, 64, 64);
            SidePanel.Controls.Add(VariablesList);
            SidePanel.Controls.Add(RemoveButton);
            SidePanel.Controls.Add(AddButton);
            SidePanel.Dock = DockStyle.Left;
            SidePanel.Location = new Point(0, 0);
            SidePanel.Name = "SidePanel";
            SidePanel.Size = new Size(190, 398);
            SidePanel.TabIndex = 12;
            // 
            // VariablesList
            // 
            VariablesList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            VariablesList.DrawMode = DrawMode.OwnerDrawVariable;
            VariablesList.FormattingEnabled = true;
            VariablesList.Location = new Point(0, 0);
            VariablesList.Name = "VariablesList";
            VariablesList.Size = new Size(190, 329);
            VariablesList.TabIndex = 13;
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
            // NamePanel
            // 
            NamePanel.BackColor = Color.FromArgb(64, 64, 64);
            NamePanel.Controls.Add(NameTextBox);
            NamePanel.Dock = DockStyle.Top;
            NamePanel.Location = new Point(190, 0);
            NamePanel.Name = "NamePanel";
            NamePanel.Size = new Size(220, 33);
            NamePanel.TabIndex = 14;
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
            // TypeList
            // 
            TypeList.DrawMode = DrawMode.OwnerDrawVariable;
            TypeList.FormattingEnabled = true;
            TypeList.Location = new Point(195, 64);
            TypeList.Name = "TypeList";
            TypeList.Size = new Size(105, 147);
            TypeList.TabIndex = 15;
            // 
            // TypeHeader
            // 
            TypeHeader.BackColor = Color.Silver;
            TypeHeader.Controls.Add(TypeLabel);
            TypeHeader.Location = new Point(194, 37);
            TypeHeader.Name = "TypeHeader";
            TypeHeader.Size = new Size(212, 22);
            TypeHeader.TabIndex = 16;
            // 
            // TypeDescriptionLabel
            // 
            TypeDescriptionLabel.Location = new Point(303, 85);
            TypeDescriptionLabel.Name = "TypeDescriptionLabel";
            TypeDescriptionLabel.Size = new Size(99, 126);
            TypeDescriptionLabel.TabIndex = 17;
            TypeDescriptionLabel.Text = "To see a description ...";
            // 
            // TypeTitleLabel
            // 
            TypeTitleLabel.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            TypeTitleLabel.Location = new Point(303, 68);
            TypeTitleLabel.Name = "TypeTitleLabel";
            TypeTitleLabel.Size = new Size(99, 18);
            TypeTitleLabel.TabIndex = 18;
            TypeTitleLabel.Text = "Select a Type";
            // 
            // ValueHeader
            // 
            ValueHeader.BackColor = Color.Silver;
            ValueHeader.Controls.Add(ValueLabel);
            ValueHeader.Location = new Point(194, 224);
            ValueHeader.Name = "ValueHeader";
            ValueHeader.Size = new Size(212, 22);
            ValueHeader.TabIndex = 17;
            // 
            // ValueLabel
            // 
            ValueLabel.AutoSize = true;
            ValueLabel.Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            ValueLabel.ForeColor = Color.White;
            ValueLabel.Location = new Point(3, 3);
            ValueLabel.Name = "ValueLabel";
            ValueLabel.Size = new Size(56, 14);
            ValueLabel.TabIndex = 4;
            ValueLabel.Text = "V a l u e :";
            // 
            // Panel1
            // 
            Panel1.BackColor = Color.White;
            Panel1.BorderStyle = BorderStyle.FixedSingle;
            Panel1.Controls.Add(ValueTextBox);
            Panel1.Location = new Point(349, 220);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(53, 31);
            Panel1.TabIndex = 19;
            // 
            // GlobalVariables
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(410, 398);
            Controls.Add(Panel1);
            Controls.Add(ValueHeader);
            Controls.Add(TypeTitleLabel);
            Controls.Add(TypeDescriptionLabel);
            Controls.Add(TypeHeader);
            Controls.Add(TypeList);
            Controls.Add(NamePanel);
            Controls.Add(SidePanel);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "GlobalVariables";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Global Variables";
            SidePanel.ResumeLayout(false);
            NamePanel.ResumeLayout(false);
            NamePanel.PerformLayout();
            TypeHeader.ResumeLayout(false);
            TypeHeader.PerformLayout();
            ValueHeader.ResumeLayout(false);
            ValueHeader.PerformLayout();
            Panel1.ResumeLayout(false);
            Panel1.PerformLayout();
            Load += new EventHandler(Globals_Load);
            FormClosing += new FormClosingEventHandler(GlobalVariables_FormClosing);
            ResumeLayout(false);

        }
        internal Label TypeLabel;
        internal TextBox ValueTextBox;
        internal Button AddButton;
        internal Panel SidePanel;
        internal Button RemoveButton;
        internal ListBox VariablesList;
        internal Panel NamePanel;
        internal TextBox NameTextBox;
        internal ListBox TypeList;
        internal Panel TypeHeader;
        internal Label TypeDescriptionLabel;
        internal Label TypeTitleLabel;
        internal Panel ValueHeader;
        internal Label ValueLabel;
        internal Panel Panel1;
    }
}