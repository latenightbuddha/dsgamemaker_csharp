using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class ActionEditor : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(ActionEditor));
            ActionsTreeView = new TreeView();
            ActionsTreeView.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(ActionsTreeView_NodeMouseDoubleClick);
            MainImageList = new ImageList(components);
            NameLabel = new Label();
            NameTextBox = new TextBox();
            MainToolStrip = new ToolStrip();
            CloseButton = new ToolStripButton();
            CloseButton.Click += new EventHandler(CloseButton_Click);
            MainToolStripSep1 = new ToolStripSeparator();
            AddActionButton = new ToolStripButton();
            AddActionButton.Click += new EventHandler(AddActionButton_Click);
            DeleteActionButton = new ToolStripButton();
            DeleteActionButton.Click += new EventHandler(DeleteActionButton_Click);
            FormattedDisplayLabel = new Label();
            ListDisplayTextBox = new TextBox();
            TypeDropper = new ComboBox();
            MainTextBox = new ScintillaNet.Scintilla();
            ActionPropertiesPanel = new Panel();
            ConditionalDisplayChecker = new CheckBox();
            DontRequestApplicationChecker = new CheckBox();
            ImageDropper = new ComboBox();
            IndentationGroupBox = new GroupBox();
            InvertRadioButton = new RadioButton();
            IndentRadioButton = new RadioButton();
            GenericRadioButton = new RadioButton();
            SubToolStrip = new ToolStrip();
            SaveButton = new ToolStripButton();
            SaveButton.Click += new EventHandler(SaveButton_Click);
            SubToolStripSep1 = new ToolStripSeparator();
            AddArgumentButton = new ToolStripButton();
            AddArgumentButton.Click += new EventHandler(AddArgumentButton_Click);
            EditArgumentButton = new ToolStripButton();
            EditArgumentButton.Click += new EventHandler(EditArgumentButton_Click);
            DeleteArgumentButton = new ToolStripButton();
            DeleteArgumentButton.Click += new EventHandler(DeleteArgumentButton_Click);
            InsertArgumentButton = new ToolStripButton();
            InsertArgumentButton.Click += new EventHandler(InsertArgumentButton_Click);
            ArgumentsLabel = new Label();
            ArgumentsListView = new ListView();
            NameColumn = new ColumnHeader();
            TypeColumn = new ColumnHeader();
            MainToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MainTextBox).BeginInit();
            ActionPropertiesPanel.SuspendLayout();
            IndentationGroupBox.SuspendLayout();
            SubToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // ActionsTreeView
            // 
            ActionsTreeView.BorderStyle = BorderStyle.FixedSingle;
            ActionsTreeView.Dock = DockStyle.Left;
            ActionsTreeView.Indent = 16;
            ActionsTreeView.ItemHeight = 19;
            ActionsTreeView.Location = new Point(0, 25);
            ActionsTreeView.Name = "ActionsTreeView";
            ActionsTreeView.Size = new Size(174, 457);
            ActionsTreeView.TabIndex = 0;
            // 
            // MainImageList
            // 
            MainImageList.ColorDepth = ColorDepth.Depth32Bit;
            MainImageList.ImageSize = new Size(16, 16);
            MainImageList.TransparentColor = Color.Transparent;
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Location = new Point(6, 40);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(112, 13);
            NameLabel.TabIndex = 3;
            NameLabel.Text = "Name, Type && Image:";
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(6, 56);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(170, 21);
            NameTextBox.TabIndex = 4;
            // 
            // MainToolStrip
            // 
            MainToolStrip.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainToolStrip.Items.AddRange(new ToolStripItem[] { CloseButton, MainToolStripSep1, AddActionButton, DeleteActionButton });
            MainToolStrip.Location = new Point(0, 0);
            MainToolStrip.Name = "MainToolStrip";
            MainToolStrip.Size = new Size(734, 25);
            MainToolStrip.TabIndex = 5;
            MainToolStrip.Text = "ToolStrip1";
            // 
            // CloseButton
            // 
            CloseButton.Image = Properties.Resources.AcceptIcon;
            CloseButton.ImageTransparentColor = Color.Magenta;
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(53, 22);
            CloseButton.Text = "Close";
            // 
            // MainToolStripSep1
            // 
            MainToolStripSep1.Name = "MainToolStripSep1";
            MainToolStripSep1.Size = new Size(6, 25);
            // 
            // AddActionButton
            // 
            AddActionButton.Image = Properties.Resources.PlusIcon;
            AddActionButton.ImageTransparentColor = Color.Magenta;
            AddActionButton.Name = "AddActionButton";
            AddActionButton.Size = new Size(79, 22);
            AddActionButton.Text = "Add Action";
            // 
            // DeleteActionButton
            // 
            DeleteActionButton.Image = Properties.Resources.DeleteIcon;
            DeleteActionButton.ImageTransparentColor = Color.Magenta;
            DeleteActionButton.Name = "DeleteActionButton";
            DeleteActionButton.Size = new Size(58, 22);
            DeleteActionButton.Text = "Delete";
            DeleteActionButton.ToolTipText = "Delete Action";
            // 
            // FormattedDisplayLabel
            // 
            FormattedDisplayLabel.AutoSize = true;
            FormattedDisplayLabel.Location = new Point(6, 114);
            FormattedDisplayLabel.Name = "FormattedDisplayLabel";
            FormattedDisplayLabel.Size = new Size(64, 13);
            FormattedDisplayLabel.TabIndex = 9;
            FormattedDisplayLabel.Text = "List Display:";
            // 
            // ListDisplayTextBox
            // 
            ListDisplayTextBox.Location = new Point(6, 130);
            ListDisplayTextBox.Name = "ListDisplayTextBox";
            ListDisplayTextBox.Size = new Size(170, 21);
            ListDisplayTextBox.TabIndex = 8;
            // 
            // TypeDropper
            // 
            TypeDropper.FormattingEnabled = true;
            TypeDropper.Location = new Point(6, 81);
            TypeDropper.Name = "TypeDropper";
            TypeDropper.Size = new Size(58, 21);
            TypeDropper.TabIndex = 7;
            // 
            // MainTextBox
            // 
            MainTextBox.ConfigurationManager.Language = "cpp";
            MainTextBox.Dock = DockStyle.Fill;
            MainTextBox.IsBraceMatching = true;
            MainTextBox.Location = new Point(174, 25);
            MainTextBox.Margins.Margin0.Width = 20;
            MainTextBox.Name = "MainTextBox";
            MainTextBox.Scrolling.HorizontalWidth = 1000;
            MainTextBox.Size = new Size(379, 457);
            MainTextBox.Styles.BraceBad.FontName = "Verdana";
            MainTextBox.Styles.BraceLight.FontName = "Verdana";
            MainTextBox.Styles.ControlChar.FontName = "Verdana";
            MainTextBox.Styles.Default.FontName = "Verdana";
            MainTextBox.Styles.IndentGuide.FontName = "Verdana";
            MainTextBox.Styles.LastPredefined.FontName = "Verdana";
            MainTextBox.Styles.LineNumber.FontName = "Verdana";
            MainTextBox.Styles.Max.FontName = "Verdana";
            MainTextBox.TabIndex = 7;
            // 
            // ActionPropertiesPanel
            // 
            ActionPropertiesPanel.BackColor = Color.FromArgb(224, 224, 224);
            ActionPropertiesPanel.BorderStyle = BorderStyle.FixedSingle;
            ActionPropertiesPanel.Controls.Add(ConditionalDisplayChecker);
            ActionPropertiesPanel.Controls.Add(DontRequestApplicationChecker);
            ActionPropertiesPanel.Controls.Add(ImageDropper);
            ActionPropertiesPanel.Controls.Add(IndentationGroupBox);
            ActionPropertiesPanel.Controls.Add(SubToolStrip);
            ActionPropertiesPanel.Controls.Add(ArgumentsLabel);
            ActionPropertiesPanel.Controls.Add(ListDisplayTextBox);
            ActionPropertiesPanel.Controls.Add(FormattedDisplayLabel);
            ActionPropertiesPanel.Controls.Add(ArgumentsListView);
            ActionPropertiesPanel.Controls.Add(TypeDropper);
            ActionPropertiesPanel.Controls.Add(NameTextBox);
            ActionPropertiesPanel.Controls.Add(NameLabel);
            ActionPropertiesPanel.Dock = DockStyle.Right;
            ActionPropertiesPanel.Location = new Point(553, 25);
            ActionPropertiesPanel.Name = "ActionPropertiesPanel";
            ActionPropertiesPanel.Size = new Size(181, 457);
            ActionPropertiesPanel.TabIndex = 8;
            // 
            // ConditionalDisplayChecker
            // 
            ConditionalDisplayChecker.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ConditionalDisplayChecker.AutoSize = true;
            ConditionalDisplayChecker.Location = new Point(10, 331);
            ConditionalDisplayChecker.Name = "ConditionalDisplayChecker";
            ConditionalDisplayChecker.Size = new Size(116, 17);
            ConditionalDisplayChecker.TabIndex = 16;
            ConditionalDisplayChecker.Text = "Conditional Display";
            ConditionalDisplayChecker.UseVisualStyleBackColor = true;
            // 
            // DontRequestApplicationChecker
            // 
            DontRequestApplicationChecker.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DontRequestApplicationChecker.AutoSize = true;
            DontRequestApplicationChecker.Location = new Point(10, 348);
            DontRequestApplicationChecker.Name = "DontRequestApplicationChecker";
            DontRequestApplicationChecker.Size = new Size(149, 17);
            DontRequestApplicationChecker.TabIndex = 15;
            DontRequestApplicationChecker.Text = "Don't Request Application";
            DontRequestApplicationChecker.UseVisualStyleBackColor = true;
            // 
            // ImageDropper
            // 
            ImageDropper.FormattingEnabled = true;
            ImageDropper.Location = new Point(65, 81);
            ImageDropper.Name = "ImageDropper";
            ImageDropper.Size = new Size(111, 21);
            ImageDropper.TabIndex = 14;
            // 
            // IndentationGroupBox
            // 
            IndentationGroupBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            IndentationGroupBox.Controls.Add(InvertRadioButton);
            IndentationGroupBox.Controls.Add(IndentRadioButton);
            IndentationGroupBox.Controls.Add(GenericRadioButton);
            IndentationGroupBox.Location = new Point(6, 368);
            IndentationGroupBox.Name = "IndentationGroupBox";
            IndentationGroupBox.Size = new Size(170, 83);
            IndentationGroupBox.TabIndex = 9;
            IndentationGroupBox.TabStop = false;
            IndentationGroupBox.Text = "Indentation";
            // 
            // InvertRadioButton
            // 
            InvertRadioButton.AutoSize = true;
            InvertRadioButton.Location = new Point(11, 60);
            InvertRadioButton.Name = "InvertRadioButton";
            InvertRadioButton.Size = new Size(52, 17);
            InvertRadioButton.TabIndex = 16;
            InvertRadioButton.TabStop = true;
            InvertRadioButton.Text = "Invert";
            InvertRadioButton.UseVisualStyleBackColor = true;
            // 
            // IndentRadioButton
            // 
            IndentRadioButton.AutoSize = true;
            IndentRadioButton.Location = new Point(11, 40);
            IndentRadioButton.Name = "IndentRadioButton";
            IndentRadioButton.Size = new Size(55, 17);
            IndentRadioButton.TabIndex = 15;
            IndentRadioButton.TabStop = true;
            IndentRadioButton.Text = "Indent";
            IndentRadioButton.UseVisualStyleBackColor = true;
            // 
            // GenericRadioButton
            // 
            GenericRadioButton.AutoSize = true;
            GenericRadioButton.Location = new Point(11, 20);
            GenericRadioButton.Name = "GenericRadioButton";
            GenericRadioButton.Size = new Size(62, 17);
            GenericRadioButton.TabIndex = 14;
            GenericRadioButton.TabStop = true;
            GenericRadioButton.Text = "Generic";
            GenericRadioButton.UseVisualStyleBackColor = true;
            // 
            // SubToolStrip
            // 
            SubToolStrip.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            SubToolStrip.Items.AddRange(new ToolStripItem[] { SaveButton, SubToolStripSep1, AddArgumentButton, EditArgumentButton, DeleteArgumentButton, InsertArgumentButton });
            SubToolStrip.Location = new Point(0, 0);
            SubToolStrip.Name = "SubToolStrip";
            SubToolStrip.Size = new Size(179, 25);
            SubToolStrip.TabIndex = 13;
            SubToolStrip.Text = "ToolStrip1";
            // 
            // SaveButton
            // 
            SaveButton.Image = Properties.Resources.SaveIcon;
            SaveButton.ImageTransparentColor = Color.Magenta;
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(51, 22);
            SaveButton.Text = "Save";
            // 
            // SubToolStripSep1
            // 
            SubToolStripSep1.Name = "SubToolStripSep1";
            SubToolStripSep1.Size = new Size(6, 25);
            // 
            // AddArgumentButton
            // 
            AddArgumentButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            AddArgumentButton.Image = Properties.Resources.PlusIcon;
            AddArgumentButton.ImageTransparentColor = Color.Magenta;
            AddArgumentButton.Name = "AddArgumentButton";
            AddArgumentButton.Size = new Size(23, 22);
            AddArgumentButton.Text = "Add Argument";
            // 
            // EditArgumentButton
            // 
            EditArgumentButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            EditArgumentButton.Image = Properties.Resources.PencilIcon;
            EditArgumentButton.ImageTransparentColor = Color.Magenta;
            EditArgumentButton.Name = "EditArgumentButton";
            EditArgumentButton.Size = new Size(23, 22);
            EditArgumentButton.Text = "Edit Argument";
            // 
            // DeleteArgumentButton
            // 
            DeleteArgumentButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            DeleteArgumentButton.Image = Properties.Resources.DeleteIcon;
            DeleteArgumentButton.ImageTransparentColor = Color.Magenta;
            DeleteArgumentButton.Name = "DeleteArgumentButton";
            DeleteArgumentButton.Size = new Size(23, 22);
            DeleteArgumentButton.Text = "Delete Argument";
            // 
            // InsertArgumentButton
            // 
            InsertArgumentButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            InsertArgumentButton.Image = Properties.Resources.InsertIcon;
            InsertArgumentButton.ImageTransparentColor = Color.Magenta;
            InsertArgumentButton.Name = "InsertArgumentButton";
            InsertArgumentButton.Size = new Size(23, 22);
            InsertArgumentButton.Text = "Insert Argument into Code";
            // 
            // ArgumentsLabel
            // 
            ArgumentsLabel.AutoSize = true;
            ArgumentsLabel.Location = new Point(6, 158);
            ArgumentsLabel.Name = "ArgumentsLabel";
            ArgumentsLabel.Size = new Size(63, 13);
            ArgumentsLabel.TabIndex = 12;
            ArgumentsLabel.Text = "Arguments:";
            // 
            // ArgumentsListView
            // 
            ArgumentsListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            ArgumentsListView.Columns.AddRange(new ColumnHeader[] { NameColumn, TypeColumn });
            ArgumentsListView.Location = new Point(6, 174);
            ArgumentsListView.MultiSelect = false;
            ArgumentsListView.Name = "ArgumentsListView";
            ArgumentsListView.Size = new Size(170, 155);
            ArgumentsListView.TabIndex = 11;
            ArgumentsListView.UseCompatibleStateImageBehavior = false;
            ArgumentsListView.View = View.Details;
            // 
            // NameColumn
            // 
            NameColumn.Text = "Name";
            NameColumn.Width = 83;
            // 
            // TypeColumn
            // 
            TypeColumn.Text = "Type";
            TypeColumn.Width = 82;
            // 
            // ActionEditor
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(734, 482);
            Controls.Add(MainTextBox);
            Controls.Add(ActionsTreeView);
            Controls.Add(ActionPropertiesPanel);
            Controls.Add(MainToolStrip);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ActionEditor";
            Text = "Action Editor";
            MainToolStrip.ResumeLayout(false);
            MainToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)MainTextBox).EndInit();
            ActionPropertiesPanel.ResumeLayout(false);
            ActionPropertiesPanel.PerformLayout();
            IndentationGroupBox.ResumeLayout(false);
            IndentationGroupBox.PerformLayout();
            SubToolStrip.ResumeLayout(false);
            SubToolStrip.PerformLayout();
            Load += new EventHandler(ActionEditor_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal TreeView ActionsTreeView;
        internal Label NameLabel;
        internal TextBox NameTextBox;
        internal ToolStrip MainToolStrip;
        internal ToolStripButton AddActionButton;
        internal ToolStripButton DeleteActionButton;
        internal ComboBox TypeDropper;
        internal ScintillaNet.Scintilla MainTextBox;
        internal ImageList MainImageList;
        internal Label FormattedDisplayLabel;
        internal TextBox ListDisplayTextBox;
        internal Panel ActionPropertiesPanel;
        internal ListView ArgumentsListView;
        internal Label ArgumentsLabel;
        internal ColumnHeader NameColumn;
        internal ColumnHeader TypeColumn;
        internal ToolStripSeparator MainToolStripSep1;
        internal ToolStripButton CloseButton;
        internal ToolStrip SubToolStrip;
        internal ToolStripButton SaveButton;
        internal ToolStripSeparator SubToolStripSep1;
        internal ToolStripButton AddArgumentButton;
        internal ToolStripButton EditArgumentButton;
        internal ToolStripButton DeleteArgumentButton;
        internal ToolStripButton InsertArgumentButton;
        internal RadioButton InvertRadioButton;
        internal RadioButton IndentRadioButton;
        internal RadioButton GenericRadioButton;
        internal GroupBox IndentationGroupBox;
        internal ComboBox ImageDropper;
        internal CheckBox DontRequestApplicationChecker;
        internal CheckBox ConditionalDisplayChecker;
    }
}