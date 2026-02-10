using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Sprite : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Sprite));
            PreviewAnimationGroupBox = new GroupBox();
            FPSTextBox = new TextBox();
            FPSLabel = new Label();
            PreviewButton = new Button();
            PreviewButton.Click += new EventHandler(PreviewButton_Click);
            NameLabel = new Label();
            NameTextBox = new TextBox();
            SidePanel = new Panel();
            MainToolStrip = new ToolStrip();
            DAcceptButton = new ToolStripButton();
            DAcceptButton.Click += new EventHandler(DAcceptButton_Click);
            ToolSep1 = new ToolStripSeparator();
            LoadFromTLabel = new ToolStripLabel();
            FromFileButton = new ToolStripButton();
            FromFileButton.Click += new EventHandler(FromFileButton_Click);
            FromSheetButton = new ToolStripButton();
            FromSheetButton.Click += new EventHandler(FromSheetButton_Click);
            ToolSep2 = new ToolStripSeparator();
            FramesTLabel = new ToolStripLabel();
            AddBlankFrameButton = new ToolStripButton();
            AddBlankFrameButton.Click += new EventHandler(AddBlankFrameButton_Click);
            DeleteFrameButton = new ToolStripButton();
            DeleteFrameButton.Click += new EventHandler(DeleteFrameButton_Click);
            EditFrameButton = new ToolStripButton();
            EditFrameButton.Click += new EventHandler(EditFrameButton_Click);
            ToolSep3 = new ToolStripSeparator();
            LoadFrameButton = new ToolStripButton();
            LoadFrameButton.Click += new EventHandler(LoadFrameButton_Click);
            AddFrameFromFileButton = new ToolStripButton();
            AddFrameFromFileButton.Click += new EventHandler(AddFrameFromFileButton_Click);
            ExportDSSpriteStripButton = new ToolStripButton();
            ExportDSSpriteStripButton.Click += new EventHandler(ExportDSSpriteStripButton_Click);
            ToolSep4 = new ToolStripSeparator();
            CutButton = new ToolStripButton();
            CutButton.Click += new EventHandler(CutButton_Click);
            CopyButton = new ToolStripButton();
            CopyButton.Click += new EventHandler(CopyButton_Click);
            PasteButton = new ToolStripButton();
            PasteButton.Click += new EventHandler(PasteButton_Click);
            ToolSep5 = new ToolStripSeparator();
            TransformButton = new ToolStripButton();
            TransformButton.Click += new EventHandler(TransformButton_Click);
            MainImageList = new ImageList(components);
            MainListView = new ListView();
            PreviewAnimationGroupBox.SuspendLayout();
            SidePanel.SuspendLayout();
            MainToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // PreviewAnimationGroupBox
            // 
            PreviewAnimationGroupBox.Controls.Add(FPSTextBox);
            PreviewAnimationGroupBox.Controls.Add(FPSLabel);
            PreviewAnimationGroupBox.Controls.Add(PreviewButton);
            PreviewAnimationGroupBox.Location = new Point(12, 52);
            PreviewAnimationGroupBox.Name = "PreviewAnimationGroupBox";
            PreviewAnimationGroupBox.Size = new Size(153, 69);
            PreviewAnimationGroupBox.TabIndex = 0;
            PreviewAnimationGroupBox.TabStop = false;
            PreviewAnimationGroupBox.Text = "Preview Animation";
            // 
            // FPSTextBox
            // 
            FPSTextBox.Location = new Point(9, 37);
            FPSTextBox.MaxLength = 2;
            FPSTextBox.Name = "FPSTextBox";
            FPSTextBox.Size = new Size(40, 21);
            FPSTextBox.TabIndex = 7;
            FPSTextBox.Text = "5";
            // 
            // FPSLabel
            // 
            FPSLabel.AutoSize = true;
            FPSLabel.Location = new Point(6, 18);
            FPSLabel.Name = "FPSLabel";
            FPSLabel.Size = new Size(103, 13);
            FPSLabel.TabIndex = 7;
            FPSLabel.Text = "Frames Per Second:";
            // 
            // PreviewButton
            // 
            PreviewButton.Location = new Point(55, 36);
            PreviewButton.Name = "PreviewButton";
            PreviewButton.Size = new Size(92, 22);
            PreviewButton.TabIndex = 7;
            PreviewButton.Text = "Preview";
            PreviewButton.UseVisualStyleBackColor = true;
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Location = new Point(12, 9);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(38, 13);
            NameLabel.TabIndex = 1;
            NameLabel.Text = "Name:";
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(12, 25);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(153, 21);
            NameTextBox.TabIndex = 2;
            // 
            // SidePanel
            // 
            SidePanel.BackColor = Color.FromArgb(240, 240, 240);
            SidePanel.Controls.Add(NameLabel);
            SidePanel.Controls.Add(NameTextBox);
            SidePanel.Controls.Add(PreviewAnimationGroupBox);
            SidePanel.Dock = DockStyle.Left;
            SidePanel.Location = new Point(0, 25);
            SidePanel.Name = "SidePanel";
            SidePanel.Size = new Size(177, 288);
            SidePanel.TabIndex = 4;
            // 
            // MainToolStrip
            // 
            MainToolStrip.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainToolStrip.Items.AddRange(new ToolStripItem[] { DAcceptButton, ToolSep1, LoadFromTLabel, FromFileButton, FromSheetButton, ToolSep2, FramesTLabel, AddBlankFrameButton, EditFrameButton, DeleteFrameButton, ToolSep3, LoadFrameButton, AddFrameFromFileButton, ExportDSSpriteStripButton, ToolSep4, CutButton, CopyButton, PasteButton, ToolSep5, TransformButton });
            MainToolStrip.Location = new Point(0, 0);
            MainToolStrip.Name = "MainToolStrip";
            MainToolStrip.Size = new Size(612, 25);
            MainToolStrip.TabIndex = 6;
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
            // ToolSep1
            // 
            ToolSep1.Name = "ToolSep1";
            ToolSep1.Size = new Size(6, 25);
            // 
            // LoadFromTLabel
            // 
            LoadFromTLabel.Name = "LoadFromTLabel";
            LoadFromTLabel.Size = new Size(59, 22);
            LoadFromTLabel.Text = "Load from:";
            // 
            // FromFileButton
            // 
            FromFileButton.Image = DS_Game_Maker.My.Resources.Resources.OpenIcon;
            FromFileButton.ImageTransparentColor = Color.Magenta;
            FromFileButton.Name = "FromFileButton";
            FromFileButton.Size = new Size(43, 22);
            FromFileButton.Text = "File";
            FromFileButton.ToolTipText = "Import from File...";
            // 
            // FromSheetButton
            // 
            FromSheetButton.Image = DS_Game_Maker.My.Resources.Resources.InsertIcon;
            FromSheetButton.ImageTransparentColor = Color.Magenta;
            FromSheetButton.Name = "FromSheetButton";
            FromSheetButton.Size = new Size(55, 22);
            FromSheetButton.Text = "Sheet";
            FromSheetButton.ToolTipText = "Import from Sheet...";
            // 
            // ToolSep2
            // 
            ToolSep2.Name = "ToolSep2";
            ToolSep2.Size = new Size(6, 25);
            // 
            // FramesTLabel
            // 
            FramesTLabel.Name = "FramesTLabel";
            FramesTLabel.Size = new Size(46, 22);
            FramesTLabel.Text = "Frames:";
            // 
            // AddBlankFrameButton
            // 
            AddBlankFrameButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            AddBlankFrameButton.Image = DS_Game_Maker.My.Resources.Resources.PlusIcon;
            AddBlankFrameButton.ImageTransparentColor = Color.Magenta;
            AddBlankFrameButton.Name = "AddBlankFrameButton";
            AddBlankFrameButton.Size = new Size(23, 22);
            AddBlankFrameButton.Text = "Add";
            AddBlankFrameButton.ToolTipText = "Add Frame";
            // 
            // DeleteFrameButton
            // 
            DeleteFrameButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            DeleteFrameButton.Image = DS_Game_Maker.My.Resources.Resources.DeleteIcon;
            DeleteFrameButton.ImageTransparentColor = Color.Magenta;
            DeleteFrameButton.Name = "DeleteFrameButton";
            DeleteFrameButton.Size = new Size(23, 22);
            DeleteFrameButton.Text = "Delete";
            DeleteFrameButton.ToolTipText = "Delete Frame";
            // 
            // EditFrameButton
            // 
            EditFrameButton.Image = DS_Game_Maker.My.Resources.Resources.PencilIcon;
            EditFrameButton.ImageTransparentColor = Color.Magenta;
            EditFrameButton.Name = "EditFrameButton";
            EditFrameButton.Size = new Size(45, 22);
            EditFrameButton.Text = "Edit";
            EditFrameButton.ToolTipText = "Edit Frame...";
            // 
            // ToolSep3
            // 
            ToolSep3.Name = "ToolSep3";
            ToolSep3.Size = new Size(6, 25);
            // 
            // LoadFrameButton
            // 
            LoadFrameButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            LoadFrameButton.Image = DS_Game_Maker.My.Resources.Resources.OpenIcon;
            LoadFrameButton.ImageTransparentColor = Color.Magenta;
            LoadFrameButton.Name = "LoadFrameButton";
            LoadFrameButton.Size = new Size(23, 22);
            LoadFrameButton.Text = "Load Frame from File";
            LoadFrameButton.ToolTipText = "Load Frame from File...";
            // 
            // AddFrameFromFileButton
            // 
            AddFrameFromFileButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            AddFrameFromFileButton.Image = DS_Game_Maker.My.Resources.Resources.AddFolderIcon;
            AddFrameFromFileButton.ImageTransparentColor = Color.Magenta;
            AddFrameFromFileButton.Name = "AddFrameFromFileButton";
            AddFrameFromFileButton.Size = new Size(23, 22);
            AddFrameFromFileButton.Text = "Add Frame From File";
            AddFrameFromFileButton.ToolTipText = "Add Frame From File...";
            // 
            // ExportDSSpriteStripButton
            // 
            ExportDSSpriteStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ExportDSSpriteStripButton.Image = DS_Game_Maker.My.Resources.Resources.ExportSpriteStripButton;
            ExportDSSpriteStripButton.ImageTransparentColor = Color.Magenta;
            ExportDSSpriteStripButton.Name = "ExportDSSpriteStripButton";
            ExportDSSpriteStripButton.Size = new Size(23, 22);
            ExportDSSpriteStripButton.Text = "Export as DS Compatible Strip";
            ExportDSSpriteStripButton.ToolTipText = "Export as DS Compatible Strip...";
            // 
            // ToolSep4
            // 
            ToolSep4.Name = "ToolSep4";
            ToolSep4.Size = new Size(6, 25);
            // 
            // CutButton
            // 
            CutButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            CutButton.Image = DS_Game_Maker.My.Resources.Resources.CutIcon;
            CutButton.ImageTransparentColor = Color.Magenta;
            CutButton.Name = "CutButton";
            CutButton.Size = new Size(23, 22);
            CutButton.Text = "Cut Frame";
            // 
            // CopyButton
            // 
            CopyButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            CopyButton.Image = DS_Game_Maker.My.Resources.Resources.CopyIcon;
            CopyButton.ImageTransparentColor = Color.Magenta;
            CopyButton.Name = "CopyButton";
            CopyButton.Size = new Size(23, 22);
            CopyButton.Text = "Copy Frame";
            // 
            // PasteButton
            // 
            PasteButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            PasteButton.Image = DS_Game_Maker.My.Resources.Resources.PasteIcon;
            PasteButton.ImageTransparentColor = Color.Magenta;
            PasteButton.Name = "PasteButton";
            PasteButton.Size = new Size(23, 22);
            PasteButton.Text = "Paste Frame";
            // 
            // ToolSep5
            // 
            ToolSep5.Name = "ToolSep5";
            ToolSep5.Size = new Size(6, 25);
            // 
            // TransformButton
            // 
            TransformButton.Enabled = false;
            TransformButton.Image = DS_Game_Maker.My.Resources.Resources.TransformIcon;
            TransformButton.ImageTransparentColor = Color.Magenta;
            TransformButton.Name = "TransformButton";
            TransformButton.Size = new Size(76, 22);
            TransformButton.Text = "Transform";
            TransformButton.ToolTipText = "Transform Frame(s)...";
            // 
            // MainImageList
            // 
            MainImageList.ColorDepth = ColorDepth.Depth32Bit;
            MainImageList.ImageSize = new Size(16, 16);
            MainImageList.TransparentColor = Color.Transparent;
            // 
            // MainListView
            // 
            MainListView.Dock = DockStyle.Fill;
            MainListView.LargeImageList = MainImageList;
            MainListView.Location = new Point(177, 25);
            MainListView.Name = "MainListView";
            MainListView.Size = new Size(435, 288);
            MainListView.TabIndex = 3;
            MainListView.UseCompatibleStateImageBehavior = false;
            // 
            // Sprite
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(612, 313);
            Controls.Add(MainListView);
            Controls.Add(SidePanel);
            Controls.Add(MainToolStrip);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(620, 340);
            Name = "Sprite";
            PreviewAnimationGroupBox.ResumeLayout(false);
            PreviewAnimationGroupBox.PerformLayout();
            SidePanel.ResumeLayout(false);
            SidePanel.PerformLayout();
            MainToolStrip.ResumeLayout(false);
            MainToolStrip.PerformLayout();
            Load += new EventHandler(Sprite_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal GroupBox PreviewAnimationGroupBox;
        internal Label NameLabel;
        internal TextBox NameTextBox;
        internal Panel SidePanel;
        internal ToolStrip MainToolStrip;
        internal ToolStripButton AddBlankFrameButton;
        internal ToolStripButton DeleteFrameButton;
        internal ToolStripButton EditFrameButton;
        internal Label FPSLabel;
        internal Button PreviewButton;
        internal TextBox FPSTextBox;
        internal ImageList MainImageList;
        internal ToolStripButton LoadFrameButton;
        internal ToolStripSeparator ToolSep3;
        internal ListView MainListView;
        internal ToolStripButton FromSheetButton;
        internal ToolStripButton AddFrameFromFileButton;
        internal ToolStripButton ExportDSSpriteStripButton;
        internal ToolStripSeparator ToolSep4;
        internal ToolStripButton CutButton;
        internal ToolStripButton CopyButton;
        internal ToolStripButton PasteButton;
        internal ToolStripSeparator ToolSep5;
        internal ToolStripButton TransformButton;
        internal ToolStripButton DAcceptButton;
        internal ToolStripSeparator ToolSep1;
        internal ToolStripButton FromFileButton;
        internal ToolStripSeparator ToolSep2;
        internal ToolStripLabel FramesTLabel;
        internal ToolStripLabel LoadFromTLabel;
    }
}