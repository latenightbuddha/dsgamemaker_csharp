using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class DObject : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(DObject));
            MainToolStrip = new ToolStrip();
            DAcceptButton = new ToolStripButton();
            DAcceptButton.Click += new EventHandler(DAcceptButton_Click);
            SelectAllButton = new ToolStripButton();
            SelectAllButton.Click += new EventHandler(SelectAllButton_Click);
            ToolStripSeparator2 = new ToolStripSeparator();
            SelectManyButton = new ToolStripButton();
            SelectManyButton.Click += new EventHandler(SelectManyButton_Click);
            SelectOneButton = new ToolStripButton();
            SelectOneButton.Click += new EventHandler(SelectOneButton_Click);
            ObjectPropertiesPanel = new Panel();
            DeleteEventButton = new Button();
            DeleteEventButton.Click += new EventHandler(DeleteEventButton_Click);
            ChangeEventButton = new Button();
            ChangeEventButton.Click += new EventHandler(ChangeEventButton_Click);
            AddEventButton = new Button();
            AddEventButton.Click += new EventHandler(AddEventButton_Click);
            EventsListBox = new ListBox();
            EventsListBox.MeasureItem += new MeasureItemEventHandler(EventsListBox_MeasureItem);
            EventsListBox.DrawItem += new DrawItemEventHandler(EventsListBox_DrawItem);
            EventsListBox.SelectedIndexChanged += new EventHandler(EventsListBox_SelectedIndexChanged);
            EventRightClickMenu = new ContextMenuStrip(components);
            EventRightClickMenu.Opening += new System.ComponentModel.CancelEventHandler(EventRightClickMenu_Opening);
            AddEventRightClickButton = new ToolStripMenuItem();
            AddEventRightClickButton.Click += new EventHandler(AddEventButton_Click);
            RightClickSep4 = new ToolStripSeparator();
            ChangeEventRightClickButton = new ToolStripMenuItem();
            ChangeEventRightClickButton.Click += new EventHandler(ChangeEventButton_Click);
            RightClickSep5 = new ToolStripSeparator();
            DeleteEventRightClickButton = new ToolStripMenuItem();
            DeleteEventRightClickButton.Click += new EventHandler(DeleteEventButton_Click);
            RightClickSep6 = new ToolStripSeparator();
            ClearEventsButton = new ToolStripMenuItem();
            ClearEventsButton.Click += new EventHandler(ClearEventsButton_Click);
            SpriteGroupBox = new GroupBox();
            OpenSpriteButton = new Button();
            OpenSpriteButton.Click += new EventHandler(OpenSpriteButton_Click);
            FrameLabel = new Label();
            FrameRightButton = new Button();
            FrameRightButton.Click += new EventHandler(FrameRightButton_Click);
            FrameLeftButton = new Button();
            FrameLeftButton.Click += new EventHandler(FrameLeftButton_Click);
            SpriteDropper = new ComboBox();
            SpriteDropper.SelectedIndexChanged += new EventHandler(SpriteDropper_SelectedIndexChanged);
            SpritePanel = new Panel();
            NameLabel = new Label();
            NameTextBox = new TextBox();
            ActionsToAddTabControl = new TabControl();
            ActionsList = new ListBox();
            ActionsList.DrawItem += new DrawItemEventHandler(ActionsList_DrawItem);
            ActionsList.MeasureItem += new MeasureItemEventHandler(ActionsList_MeasureItem);
            ActionsList.MouseDoubleClick += new MouseEventHandler(ActionsList_MouseDoubleClick);
            ActionsList.MouseDown += new MouseEventHandler(ActionsList_MouseDown);
            ActionsList.MouseUp += new MouseEventHandler(ActionsList_MouseUp);
            ActionsList.MouseMove += new MouseEventHandler(ActionsList_MouseMove);
            ActionRightClickMenu = new ContextMenuStrip(components);
            ActionRightClickMenu.Opening += new System.ComponentModel.CancelEventHandler(ActionRightClickMenu_Opening);
            EditValuesRightClickButton = new ToolStripMenuItem();
            EditValuesRightClickButton.Click += new EventHandler(ActionsList_MouseDoubleClick);
            RightClickSep1 = new ToolStripSeparator();
            SelectOneRightClickButton = new ToolStripMenuItem();
            SelectOneRightClickButton.Click += new EventHandler(SelectOneButton_Click);
            SelectManyRightClickButton = new ToolStripMenuItem();
            SelectManyRightClickButton.Click += new EventHandler(SelectManyButton_Click);
            RightClickSep3 = new ToolStripSeparator();
            CutActionRightClickButton = new ToolStripMenuItem();
            CutActionRightClickButton.Click += new EventHandler(CutRightClickButton_Click);
            CopyActionRightClickButton = new ToolStripMenuItem();
            CopyActionRightClickButton.Click += new EventHandler(CopyActionRightClickButton_Click);
            PasteActionBelowRightClickButton = new ToolStripMenuItem();
            RightClickSep2 = new ToolStripSeparator();
            DeleteActionRightClickButton = new ToolStripMenuItem();
            DeleteActionRightClickButton.Click += new EventHandler(DeleteRightClickButton_Click);
            ClearActionsRightClickButton = new ToolStripMenuItem();
            ClearActionsRightClickButton.Click += new EventHandler(ClearRightClickButton_Click);
            ActionsPanel = new Panel();
            ActionInfoPanel = new Panel();
            RequiresProBanner = new Label();
            ArgumentsListLabel = new Label();
            ArgumentsHeaderLabel = new Label();
            ActionNameLabel = new Label();
            MainToolStrip.SuspendLayout();
            ObjectPropertiesPanel.SuspendLayout();
            EventRightClickMenu.SuspendLayout();
            SpriteGroupBox.SuspendLayout();
            ActionRightClickMenu.SuspendLayout();
            ActionsPanel.SuspendLayout();
            ActionInfoPanel.SuspendLayout();
            SuspendLayout();
            // 
            // MainToolStrip
            // 
            MainToolStrip.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainToolStrip.Items.AddRange(new ToolStripItem[] { DAcceptButton, SelectAllButton, ToolStripSeparator2, SelectManyButton, SelectOneButton });
            MainToolStrip.Location = new Point(0, 0);
            MainToolStrip.Name = "MainToolStrip";
            MainToolStrip.Size = new Size(708, 25);
            MainToolStrip.TabIndex = 0;
            MainToolStrip.Text = "ToolStrip1";
            // 
            // DAcceptButton
            // 
            DAcceptButton.Image = DS_Game_Maker.My.Resources.Resources.AcceptIcon;
            DAcceptButton.ImageTransparentColor = Color.Magenta;
            DAcceptButton.Name = "DAcceptButton";
            DAcceptButton.Size = new Size(60, 22);
            DAcceptButton.Text = "Accept";
            // 
            // SelectAllButton
            // 
            SelectAllButton.Alignment = ToolStripItemAlignment.Right;
            SelectAllButton.Image = DS_Game_Maker.My.Resources.Resources.CopyIcon;
            SelectAllButton.ImageTransparentColor = Color.Magenta;
            SelectAllButton.Name = "SelectAllButton";
            SelectAllButton.Size = new Size(70, 22);
            SelectAllButton.Text = "Select All";
            // 
            // ToolStripSeparator2
            // 
            ToolStripSeparator2.Alignment = ToolStripItemAlignment.Right;
            ToolStripSeparator2.Name = "ToolStripSeparator2";
            ToolStripSeparator2.Size = new Size(6, 25);
            // 
            // SelectManyButton
            // 
            SelectManyButton.Alignment = ToolStripItemAlignment.Right;
            SelectManyButton.Image = DS_Game_Maker.My.Resources.Resources.ListIcon;
            SelectManyButton.ImageTransparentColor = Color.Magenta;
            SelectManyButton.Name = "SelectManyButton";
            SelectManyButton.Size = new Size(85, 22);
            SelectManyButton.Text = "Select Many";
            // 
            // SelectOneButton
            // 
            SelectOneButton.Alignment = ToolStripItemAlignment.Right;
            SelectOneButton.Image = DS_Game_Maker.My.Resources.Resources.ListIcon;
            SelectOneButton.ImageTransparentColor = Color.Magenta;
            SelectOneButton.Name = "SelectOneButton";
            SelectOneButton.Size = new Size(79, 22);
            SelectOneButton.Text = "Select One";
            // 
            // ObjectPropertiesPanel
            // 
            ObjectPropertiesPanel.Controls.Add(DeleteEventButton);
            ObjectPropertiesPanel.Controls.Add(ChangeEventButton);
            ObjectPropertiesPanel.Controls.Add(AddEventButton);
            ObjectPropertiesPanel.Controls.Add(EventsListBox);
            ObjectPropertiesPanel.Controls.Add(SpriteGroupBox);
            ObjectPropertiesPanel.Controls.Add(NameLabel);
            ObjectPropertiesPanel.Controls.Add(NameTextBox);
            ObjectPropertiesPanel.Dock = DockStyle.Left;
            ObjectPropertiesPanel.Location = new Point(0, 25);
            ObjectPropertiesPanel.Name = "ObjectPropertiesPanel";
            ObjectPropertiesPanel.Size = new Size(210, 517);
            ObjectPropertiesPanel.TabIndex = 1;
            // 
            // DeleteEventButton
            // 
            DeleteEventButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            DeleteEventButton.Location = new Point(106, 477);
            DeleteEventButton.Name = "DeleteEventButton";
            DeleteEventButton.Size = new Size(92, 28);
            DeleteEventButton.TabIndex = 6;
            DeleteEventButton.Text = "Delete";
            DeleteEventButton.UseVisualStyleBackColor = true;
            // 
            // ChangeEventButton
            // 
            ChangeEventButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ChangeEventButton.Location = new Point(12, 477);
            ChangeEventButton.Name = "ChangeEventButton";
            ChangeEventButton.Size = new Size(92, 28);
            ChangeEventButton.TabIndex = 5;
            ChangeEventButton.Text = "Change";
            ChangeEventButton.UseVisualStyleBackColor = true;
            // 
            // AddEventButton
            // 
            AddEventButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            AddEventButton.Location = new Point(12, 446);
            AddEventButton.Name = "AddEventButton";
            AddEventButton.Size = new Size(186, 28);
            AddEventButton.TabIndex = 4;
            AddEventButton.Text = "Add Event";
            AddEventButton.UseVisualStyleBackColor = true;
            // 
            // EventsListBox
            // 
            EventsListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            EventsListBox.ContextMenuStrip = EventRightClickMenu;
            EventsListBox.DrawMode = DrawMode.OwnerDrawVariable;
            EventsListBox.FormattingEnabled = true;
            EventsListBox.Location = new Point(12, 150);
            EventsListBox.Name = "EventsListBox";
            EventsListBox.Size = new Size(186, 292);
            EventsListBox.TabIndex = 4;
            // 
            // EventRightClickMenu
            // 
            EventRightClickMenu.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            EventRightClickMenu.Items.AddRange(new ToolStripItem[] { AddEventRightClickButton, RightClickSep4, ChangeEventRightClickButton, RightClickSep5, DeleteEventRightClickButton, RightClickSep6, ClearEventsButton });
            EventRightClickMenu.Name = "ActionRightClickMenu";
            EventRightClickMenu.Size = new Size(125, 110);
            // 
            // AddEventRightClickButton
            // 
            AddEventRightClickButton.Image = DS_Game_Maker.My.Resources.Resources.PlusIcon;
            AddEventRightClickButton.Name = "AddEventRightClickButton";
            AddEventRightClickButton.Size = new Size(124, 22);
            AddEventRightClickButton.Text = "Add Event";
            // 
            // RightClickSep4
            // 
            RightClickSep4.Name = "RightClickSep4";
            RightClickSep4.Size = new Size(121, 6);
            // 
            // ChangeEventRightClickButton
            // 
            ChangeEventRightClickButton.Image = DS_Game_Maker.My.Resources.Resources.PencilIcon;
            ChangeEventRightClickButton.Name = "ChangeEventRightClickButton";
            ChangeEventRightClickButton.Size = new Size(124, 22);
            ChangeEventRightClickButton.Text = "Change";
            // 
            // RightClickSep5
            // 
            RightClickSep5.Name = "RightClickSep5";
            RightClickSep5.Size = new Size(121, 6);
            // 
            // DeleteEventRightClickButton
            // 
            DeleteEventRightClickButton.Image = DS_Game_Maker.My.Resources.Resources.DeleteIcon;
            DeleteEventRightClickButton.Name = "DeleteEventRightClickButton";
            DeleteEventRightClickButton.Size = new Size(124, 22);
            DeleteEventRightClickButton.Text = "Delete";
            // 
            // RightClickSep6
            // 
            RightClickSep6.Name = "RightClickSep6";
            RightClickSep6.Size = new Size(121, 6);
            // 
            // ClearEventsButton
            // 
            ClearEventsButton.Name = "ClearEventsButton";
            ClearEventsButton.Size = new Size(124, 22);
            ClearEventsButton.Text = "Clear";
            // 
            // SpriteGroupBox
            // 
            SpriteGroupBox.Controls.Add(OpenSpriteButton);
            SpriteGroupBox.Controls.Add(FrameLabel);
            SpriteGroupBox.Controls.Add(FrameRightButton);
            SpriteGroupBox.Controls.Add(FrameLeftButton);
            SpriteGroupBox.Controls.Add(SpriteDropper);
            SpriteGroupBox.Controls.Add(SpritePanel);
            SpriteGroupBox.Location = new Point(12, 53);
            SpriteGroupBox.Name = "SpriteGroupBox";
            SpriteGroupBox.Size = new Size(186, 91);
            SpriteGroupBox.TabIndex = 4;
            SpriteGroupBox.TabStop = false;
            SpriteGroupBox.Text = "Sprite";
            // 
            // OpenSpriteButton
            // 
            OpenSpriteButton.ImageAlign = ContentAlignment.MiddleLeft;
            OpenSpriteButton.Location = new Point(76, 63);
            OpenSpriteButton.Name = "OpenSpriteButton";
            OpenSpriteButton.Size = new Size(104, 22);
            OpenSpriteButton.TabIndex = 6;
            OpenSpriteButton.Text = "Open Sprite";
            OpenSpriteButton.UseVisualStyleBackColor = true;
            // 
            // FrameLabel
            // 
            FrameLabel.AutoSize = true;
            FrameLabel.Location = new Point(91, 45);
            FrameLabel.Name = "FrameLabel";
            FrameLabel.Size = new Size(41, 13);
            FrameLabel.TabIndex = 4;
            FrameLabel.Text = "Frame:";
            // 
            // FrameRightButton
            // 
            FrameRightButton.Image = DS_Game_Maker.My.Resources.Resources.RightArrowSmall;
            FrameRightButton.ImageAlign = ContentAlignment.MiddleRight;
            FrameRightButton.Location = new Point(158, 42);
            FrameRightButton.Name = "FrameRightButton";
            FrameRightButton.Size = new Size(22, 20);
            FrameRightButton.TabIndex = 5;
            FrameRightButton.UseVisualStyleBackColor = true;
            // 
            // FrameLeftButton
            // 
            FrameLeftButton.Image = DS_Game_Maker.My.Resources.Resources.LeftArrowSmall;
            FrameLeftButton.ImageAlign = ContentAlignment.MiddleLeft;
            FrameLeftButton.Location = new Point(134, 42);
            FrameLeftButton.Name = "FrameLeftButton";
            FrameLeftButton.Size = new Size(22, 20);
            FrameLeftButton.TabIndex = 4;
            FrameLeftButton.UseVisualStyleBackColor = true;
            // 
            // SpriteDropper
            // 
            SpriteDropper.FormattingEnabled = true;
            SpriteDropper.Location = new Point(76, 20);
            SpriteDropper.Name = "SpriteDropper";
            SpriteDropper.Size = new Size(104, 21);
            SpriteDropper.TabIndex = 4;
            // 
            // SpritePanel
            // 
            SpritePanel.Location = new Point(6, 20);
            SpritePanel.Name = "SpritePanel";
            SpritePanel.Size = new Size(64, 64);
            SpritePanel.TabIndex = 4;
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Location = new Point(9, 10);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(38, 13);
            NameLabel.TabIndex = 4;
            NameLabel.Text = "Name:";
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(12, 26);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(186, 21);
            NameTextBox.TabIndex = 3;
            // 
            // ActionsToAddTabControl
            // 
            ActionsToAddTabControl.Dock = DockStyle.Fill;
            ActionsToAddTabControl.Location = new Point(0, 0);
            ActionsToAddTabControl.Name = "ActionsToAddTabControl";
            ActionsToAddTabControl.SelectedIndex = 0;
            ActionsToAddTabControl.Size = new Size(328, 160);
            ActionsToAddTabControl.TabIndex = 2;
            // 
            // ActionsList
            // 
            ActionsList.ContextMenuStrip = ActionRightClickMenu;
            ActionsList.Dock = DockStyle.Fill;
            ActionsList.DrawMode = DrawMode.OwnerDrawVariable;
            ActionsList.FormattingEnabled = true;
            ActionsList.Location = new Point(210, 25);
            ActionsList.Name = "ActionsList";
            ActionsList.SelectionMode = SelectionMode.MultiExtended;
            ActionsList.Size = new Size(498, 357);
            ActionsList.TabIndex = 3;
            // 
            // ActionRightClickMenu
            // 
            ActionRightClickMenu.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            ActionRightClickMenu.Items.AddRange(new ToolStripItem[] { EditValuesRightClickButton, RightClickSep1, SelectOneRightClickButton, SelectManyRightClickButton, RightClickSep3, CutActionRightClickButton, CopyActionRightClickButton, PasteActionBelowRightClickButton, RightClickSep2, DeleteActionRightClickButton, ClearActionsRightClickButton });
            ActionRightClickMenu.Name = "ActionRightClickMenu";
            ActionRightClickMenu.Size = new Size(133, 198);
            // 
            // EditValuesRightClickButton
            // 
            EditValuesRightClickButton.Image = DS_Game_Maker.My.Resources.Resources.PencilIcon;
            EditValuesRightClickButton.Name = "EditValuesRightClickButton";
            EditValuesRightClickButton.Size = new Size(132, 22);
            EditValuesRightClickButton.Text = "Edit Values";
            // 
            // RightClickSep1
            // 
            RightClickSep1.Name = "RightClickSep1";
            RightClickSep1.Size = new Size(129, 6);
            // 
            // SelectOneRightClickButton
            // 
            SelectOneRightClickButton.Image = DS_Game_Maker.My.Resources.Resources.ListIcon;
            SelectOneRightClickButton.Name = "SelectOneRightClickButton";
            SelectOneRightClickButton.Size = new Size(132, 22);
            SelectOneRightClickButton.Text = "Select One";
            // 
            // SelectManyRightClickButton
            // 
            SelectManyRightClickButton.Image = DS_Game_Maker.My.Resources.Resources.ListIcon;
            SelectManyRightClickButton.Name = "SelectManyRightClickButton";
            SelectManyRightClickButton.Size = new Size(132, 22);
            SelectManyRightClickButton.Text = "Select Many";
            // 
            // RightClickSep3
            // 
            RightClickSep3.Name = "RightClickSep3";
            RightClickSep3.Size = new Size(129, 6);
            // 
            // CutActionRightClickButton
            // 
            CutActionRightClickButton.Name = "CutActionRightClickButton";
            CutActionRightClickButton.Size = new Size(132, 22);
            CutActionRightClickButton.Text = "Cut";
            // 
            // CopyActionRightClickButton
            // 
            CopyActionRightClickButton.Image = DS_Game_Maker.My.Resources.Resources.CopyIcon;
            CopyActionRightClickButton.Name = "CopyActionRightClickButton";
            CopyActionRightClickButton.Size = new Size(132, 22);
            CopyActionRightClickButton.Text = "Copy";
            // 
            // PasteActionBelowRightClickButton
            // 
            PasteActionBelowRightClickButton.Image = DS_Game_Maker.My.Resources.Resources.PasteIcon;
            PasteActionBelowRightClickButton.Name = "PasteActionBelowRightClickButton";
            PasteActionBelowRightClickButton.Size = new Size(132, 22);
            PasteActionBelowRightClickButton.Text = "Paste below";
            // 
            // RightClickSep2
            // 
            RightClickSep2.Name = "RightClickSep2";
            RightClickSep2.Size = new Size(129, 6);
            // 
            // DeleteActionRightClickButton
            // 
            DeleteActionRightClickButton.Image = DS_Game_Maker.My.Resources.Resources.DeleteIcon;
            DeleteActionRightClickButton.Name = "DeleteActionRightClickButton";
            DeleteActionRightClickButton.Size = new Size(132, 22);
            DeleteActionRightClickButton.Text = "Delete";
            // 
            // ClearActionsRightClickButton
            // 
            ClearActionsRightClickButton.Name = "ClearActionsRightClickButton";
            ClearActionsRightClickButton.Size = new Size(132, 22);
            ClearActionsRightClickButton.Text = "Clear";
            // 
            // ActionsPanel
            // 
            ActionsPanel.Controls.Add(ActionsToAddTabControl);
            ActionsPanel.Controls.Add(ActionInfoPanel);
            ActionsPanel.Dock = DockStyle.Bottom;
            ActionsPanel.Location = new Point(210, 382);
            ActionsPanel.Name = "ActionsPanel";
            ActionsPanel.Size = new Size(498, 160);
            ActionsPanel.TabIndex = 4;
            // 
            // ActionInfoPanel
            // 
            ActionInfoPanel.Controls.Add(RequiresProBanner);
            ActionInfoPanel.Controls.Add(ArgumentsListLabel);
            ActionInfoPanel.Controls.Add(ArgumentsHeaderLabel);
            ActionInfoPanel.Controls.Add(ActionNameLabel);
            ActionInfoPanel.Dock = DockStyle.Right;
            ActionInfoPanel.Location = new Point(328, 0);
            ActionInfoPanel.Name = "ActionInfoPanel";
            ActionInfoPanel.Size = new Size(170, 160);
            ActionInfoPanel.TabIndex = 5;
            // 
            // RequiresProBanner
            // 
            RequiresProBanner.BackColor = Color.FromArgb(192, 0, 0);
            RequiresProBanner.BorderStyle = BorderStyle.FixedSingle;
            RequiresProBanner.Font = new Font("Arial", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            RequiresProBanner.ForeColor = Color.White;
            RequiresProBanner.Location = new Point(4, 133);
            RequiresProBanner.Name = "RequiresProBanner";
            RequiresProBanner.Padding = new Padding(3);
            RequiresProBanner.Size = new Size(162, 23);
            RequiresProBanner.TabIndex = 3;
            RequiresProBanner.Text = "Requires PRO";
            // 
            // ArgumentsListLabel
            // 
            ArgumentsListLabel.BorderStyle = BorderStyle.Fixed3D;
            ArgumentsListLabel.Location = new Point(7, 58);
            ArgumentsListLabel.Name = "ArgumentsListLabel";
            ArgumentsListLabel.Padding = new Padding(0, 2, 4, 4);
            ArgumentsListLabel.Size = new Size(156, 68);
            ArgumentsListLabel.TabIndex = 2;
            ArgumentsListLabel.Text = "<No Arguments>";
            // 
            // ArgumentsHeaderLabel
            // 
            ArgumentsHeaderLabel.BackColor = Color.Gray;
            ArgumentsHeaderLabel.BorderStyle = BorderStyle.FixedSingle;
            ArgumentsHeaderLabel.Font = new Font("Arial", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            ArgumentsHeaderLabel.ForeColor = Color.White;
            ArgumentsHeaderLabel.Location = new Point(4, 36);
            ArgumentsHeaderLabel.Name = "ArgumentsHeaderLabel";
            ArgumentsHeaderLabel.Padding = new Padding(2);
            ArgumentsHeaderLabel.Size = new Size(162, 94);
            ArgumentsHeaderLabel.TabIndex = 1;
            ArgumentsHeaderLabel.Text = "Arguments:";
            // 
            // ActionNameLabel
            // 
            ActionNameLabel.BackColor = Color.FromArgb(64, 64, 64);
            ActionNameLabel.BorderStyle = BorderStyle.FixedSingle;
            ActionNameLabel.Font = new Font("Arial", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            ActionNameLabel.ForeColor = Color.White;
            ActionNameLabel.Location = new Point(4, 5);
            ActionNameLabel.Name = "ActionNameLabel";
            ActionNameLabel.Padding = new Padding(2, 4, 4, 4);
            ActionNameLabel.Size = new Size(162, 27);
            ActionNameLabel.TabIndex = 0;
            ActionNameLabel.Text = "...";
            // 
            // DObject
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(708, 542);
            Controls.Add(ActionsList);
            Controls.Add(ActionsPanel);
            Controls.Add(ObjectPropertiesPanel);
            Controls.Add(MainToolStrip);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(724, 580);
            Name = "DObject";
            MainToolStrip.ResumeLayout(false);
            MainToolStrip.PerformLayout();
            ObjectPropertiesPanel.ResumeLayout(false);
            ObjectPropertiesPanel.PerformLayout();
            EventRightClickMenu.ResumeLayout(false);
            SpriteGroupBox.ResumeLayout(false);
            SpriteGroupBox.PerformLayout();
            ActionRightClickMenu.ResumeLayout(false);
            ActionsPanel.ResumeLayout(false);
            ActionInfoPanel.ResumeLayout(false);
            Load += new EventHandler(DObject_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal ToolStrip MainToolStrip;
        internal ToolStripButton DAcceptButton;
        internal Panel ObjectPropertiesPanel;
        internal TabControl ActionsToAddTabControl;
        internal ListBox ActionsList;
        internal Label NameLabel;
        internal TextBox NameTextBox;
        internal GroupBox SpriteGroupBox;
        internal Panel SpritePanel;
        internal ComboBox SpriteDropper;
        internal Label FrameLabel;
        internal Button FrameRightButton;
        internal Button FrameLeftButton;
        internal Button ChangeEventButton;
        internal Button AddEventButton;
        internal ListBox EventsListBox;
        internal Button DeleteEventButton;
        internal ContextMenuStrip ActionRightClickMenu;
        internal ToolStripMenuItem EditValuesRightClickButton;
        internal ToolStripMenuItem DeleteActionRightClickButton;
        internal ToolStripSeparator RightClickSep1;
        internal ToolStripMenuItem CutActionRightClickButton;
        internal ToolStripMenuItem CopyActionRightClickButton;
        internal ToolStripMenuItem PasteActionBelowRightClickButton;
        internal ToolStripSeparator RightClickSep2;
        internal ToolStripSeparator RightClickSep3;
        internal ToolStripMenuItem ClearActionsRightClickButton;
        internal Button OpenSpriteButton;
        internal ContextMenuStrip EventRightClickMenu;
        internal ToolStripMenuItem AddEventRightClickButton;
        internal ToolStripSeparator RightClickSep4;
        internal ToolStripMenuItem DeleteEventRightClickButton;
        internal ToolStripSeparator RightClickSep6;
        internal ToolStripMenuItem ClearEventsButton;
        internal ToolStripMenuItem ChangeEventRightClickButton;
        internal ToolStripSeparator RightClickSep5;
        internal ToolStripButton SelectAllButton;
        internal Panel ActionsPanel;
        internal Panel ActionInfoPanel;
        internal Label ActionNameLabel;
        internal Label ArgumentsHeaderLabel;
        internal Label ArgumentsListLabel;
        internal Label RequiresProBanner;
        internal ToolStripButton SelectManyButton;
        internal ToolStripButton SelectOneButton;
        internal ToolStripMenuItem SelectOneRightClickButton;
        internal ToolStripMenuItem SelectManyRightClickButton;
        internal ToolStripSeparator ToolStripSeparator2;
    }
}