using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class GlobalArrays : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(GlobalArrays));
            ArraysList = new TreeView();
            ArraysList.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(VariablesList_NodeMouseDoubleClick);
            NameTextBox = new TextBox();
            NameLabel = new Label();
            SaveButton = new Button();
            SaveButton.Click += new EventHandler(SaveButton_Click);
            DeleteButton = new ToolStripButton();
            DeleteButton.Click += new EventHandler(DeleteButton_Click);
            TypeLabel = new Label();
            AddButton = new ToolStripButton();
            AddButton.Click += new EventHandler(AddButton_Click);
            CloseButton = new ToolStripButton();
            CloseButton.Click += new EventHandler(CloseButton_Click);
            MainToolStrip = new ToolStrip();
            ToolStripSep1 = new ToolStripSeparator();
            TypeDropper = new ComboBox();
            MainGroupBox = new GroupBox();
            DeleteValueButton = new Button();
            DeleteValueButton.Click += new EventHandler(DeleteValueButton_Click);
            EditValueButton = new Button();
            EditValueButton.Click += new EventHandler(EditValueButton_Click);
            ValuesListBox = new ListBox();
            ValuesLabel = new Label();
            AddValueButton = new Button();
            AddValueButton.Click += new EventHandler(AddValueButton_Click);
            MainToolStrip.SuspendLayout();
            MainGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // ArraysList
            // 
            ArraysList.Dock = DockStyle.Left;
            ArraysList.Indent = 16;
            ArraysList.ItemHeight = 19;
            ArraysList.Location = new Point(0, 25);
            ArraysList.Name = "ArraysList";
            ArraysList.Size = new Size(180, 373);
            ArraysList.TabIndex = 11;
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(69, 24);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(120, 21);
            NameTextBox.TabIndex = 3;
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Location = new Point(22, 27);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(38, 13);
            NameLabel.TabIndex = 2;
            NameLabel.Text = "Name:";
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(192, 264);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(206, 28);
            SaveButton.TabIndex = 13;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            // 
            // DeleteButton
            // 
            DeleteButton.Image = DS_Game_Maker.My.Resources.Resources.DeleteIcon;
            DeleteButton.ImageTransparentColor = Color.Magenta;
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(58, 22);
            DeleteButton.Text = "Delete";
            // 
            // TypeLabel
            // 
            TypeLabel.AutoSize = true;
            TypeLabel.Location = new Point(25, 54);
            TypeLabel.Name = "TypeLabel";
            TypeLabel.Size = new Size(35, 13);
            TypeLabel.TabIndex = 4;
            TypeLabel.Text = "Type:";
            // 
            // AddButton
            // 
            AddButton.Image = DS_Game_Maker.My.Resources.Resources.PlusIcon;
            AddButton.ImageTransparentColor = Color.Magenta;
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(46, 22);
            AddButton.Text = "Add";
            // 
            // CloseButton
            // 
            CloseButton.Image = DS_Game_Maker.My.Resources.Resources.AcceptIcon;
            CloseButton.ImageTransparentColor = Color.Magenta;
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(53, 22);
            CloseButton.Text = "Close";
            // 
            // MainToolStrip
            // 
            MainToolStrip.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainToolStrip.Items.AddRange(new ToolStripItem[] { CloseButton, ToolStripSep1, AddButton, DeleteButton });
            MainToolStrip.Location = new Point(0, 0);
            MainToolStrip.Name = "MainToolStrip";
            MainToolStrip.Size = new Size(410, 25);
            MainToolStrip.TabIndex = 12;
            MainToolStrip.Text = "ToolStrip1";
            // 
            // ToolStripSep1
            // 
            ToolStripSep1.Name = "ToolStripSep1";
            ToolStripSep1.Size = new Size(6, 25);
            // 
            // TypeDropper
            // 
            TypeDropper.FormattingEnabled = true;
            TypeDropper.Items.AddRange(new object[] { "Number", "TrueFalse" });
            TypeDropper.Location = new Point(69, 51);
            TypeDropper.Name = "TypeDropper";
            TypeDropper.Size = new Size(120, 21);
            TypeDropper.TabIndex = 5;
            // 
            // MainGroupBox
            // 
            MainGroupBox.Controls.Add(DeleteValueButton);
            MainGroupBox.Controls.Add(EditValueButton);
            MainGroupBox.Controls.Add(ValuesListBox);
            MainGroupBox.Controls.Add(ValuesLabel);
            MainGroupBox.Controls.Add(AddValueButton);
            MainGroupBox.Controls.Add(NameTextBox);
            MainGroupBox.Controls.Add(NameLabel);
            MainGroupBox.Controls.Add(TypeLabel);
            MainGroupBox.Controls.Add(TypeDropper);
            MainGroupBox.Location = new Point(192, 35);
            MainGroupBox.Name = "MainGroupBox";
            MainGroupBox.Size = new Size(206, 223);
            MainGroupBox.TabIndex = 14;
            MainGroupBox.TabStop = false;
            MainGroupBox.Text = "<No Array Selected>";
            // 
            // DeleteValueButton
            // 
            DeleteValueButton.Location = new Point(6, 190);
            DeleteValueButton.Name = "DeleteValueButton";
            DeleteValueButton.Size = new Size(60, 22);
            DeleteValueButton.TabIndex = 17;
            DeleteValueButton.Text = "Delete";
            DeleteValueButton.UseVisualStyleBackColor = true;
            // 
            // EditValueButton
            // 
            EditValueButton.Location = new Point(6, 168);
            EditValueButton.Name = "EditValueButton";
            EditValueButton.Size = new Size(60, 22);
            EditValueButton.TabIndex = 16;
            EditValueButton.Text = "Edit";
            EditValueButton.UseVisualStyleBackColor = true;
            // 
            // ValuesListBox
            // 
            ValuesListBox.FormattingEnabled = true;
            ValuesListBox.Location = new Point(69, 78);
            ValuesListBox.Name = "ValuesListBox";
            ValuesListBox.Size = new Size(120, 134);
            ValuesListBox.TabIndex = 15;
            // 
            // ValuesLabel
            // 
            ValuesLabel.AutoSize = true;
            ValuesLabel.Location = new Point(18, 81);
            ValuesLabel.Name = "ValuesLabel";
            ValuesLabel.Size = new Size(42, 13);
            ValuesLabel.TabIndex = 6;
            ValuesLabel.Text = "Values:";
            // 
            // AddValueButton
            // 
            AddValueButton.Location = new Point(6, 146);
            AddValueButton.Name = "AddValueButton";
            AddValueButton.Size = new Size(60, 22);
            AddValueButton.TabIndex = 15;
            AddValueButton.Text = "Add";
            AddValueButton.UseVisualStyleBackColor = true;
            // 
            // GlobalArrays
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(410, 398);
            Controls.Add(ArraysList);
            Controls.Add(SaveButton);
            Controls.Add(MainToolStrip);
            Controls.Add(MainGroupBox);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "GlobalArrays";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Global Arrays";
            MainToolStrip.ResumeLayout(false);
            MainToolStrip.PerformLayout();
            MainGroupBox.ResumeLayout(false);
            MainGroupBox.PerformLayout();
            Load += new EventHandler(Globals_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal TreeView ArraysList;
        internal TextBox NameTextBox;
        internal Label NameLabel;
        internal Button SaveButton;
        internal ToolStripButton DeleteButton;
        internal Label TypeLabel;
        internal ToolStripButton AddButton;
        internal ToolStripButton CloseButton;
        internal ToolStrip MainToolStrip;
        internal ToolStripSeparator ToolStripSep1;
        internal ComboBox TypeDropper;
        internal GroupBox MainGroupBox;
        internal ListBox ValuesListBox;
        internal Label ValuesLabel;
        internal Button DeleteValueButton;
        internal Button EditValueButton;
        internal Button AddValueButton;
    }
}