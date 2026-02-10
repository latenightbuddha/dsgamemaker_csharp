using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class HelpViewer : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpViewer));
            SideTabControl = new TabControl();
            ContentsTab = new TabPage();
            MainTreeView = new TreeView();
            MainTreeView.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(MainTreeView_NodeMouseDoubleClick);
            MainImageList = new ImageList(components);
            IndexTab = new TabPage();
            ListBox1 = new ListBox();
            SearchBoxPanel = new Panel();
            SearchButton = new Button();
            SearchBox = new Panel();
            SearchBoxTB = new TextBox();
            DocBrowser = new WebBrowser();
            DocBrowser.Navigating += new WebBrowserNavigatingEventHandler(DocBrowser_Navigating);
            SideTabControl.SuspendLayout();
            ContentsTab.SuspendLayout();
            IndexTab.SuspendLayout();
            SearchBoxPanel.SuspendLayout();
            SearchBox.SuspendLayout();
            SuspendLayout();
            // 
            // SideTabControl
            // 
            SideTabControl.Controls.Add(ContentsTab);
            SideTabControl.Controls.Add(IndexTab);
            SideTabControl.Dock = DockStyle.Left;
            SideTabControl.Location = new Point(0, 0);
            SideTabControl.Margin = new Padding(0);
            SideTabControl.Name = "SideTabControl";
            SideTabControl.SelectedIndex = 0;
            SideTabControl.Size = new Size(256, 446);
            SideTabControl.TabIndex = 1;
            // 
            // ContentsTab
            // 
            ContentsTab.Controls.Add(MainTreeView);
            ContentsTab.Location = new Point(4, 22);
            ContentsTab.Name = "ContentsTab";
            ContentsTab.Padding = new Padding(3);
            ContentsTab.Size = new Size(248, 420);
            ContentsTab.TabIndex = 0;
            ContentsTab.Text = "Contents";
            ContentsTab.UseVisualStyleBackColor = true;
            // 
            // MainTreeView
            // 
            MainTreeView.BorderStyle = BorderStyle.None;
            MainTreeView.Dock = DockStyle.Fill;
            MainTreeView.ImageIndex = 0;
            MainTreeView.ImageList = MainImageList;
            MainTreeView.ItemHeight = 20;
            MainTreeView.Location = new Point(3, 3);
            MainTreeView.Name = "MainTreeView";
            MainTreeView.SelectedImageIndex = 0;
            MainTreeView.Size = new Size(242, 414);
            MainTreeView.TabIndex = 3;
            // 
            // MainImageList
            // 
            MainImageList.ImageStream = (ImageListStreamer)resources.GetObject("MainImageList.ImageStream");
            MainImageList.TransparentColor = Color.Transparent;
            MainImageList.Images.SetKeyName(0, "book.png");
            MainImageList.Images.SetKeyName(1, "document.png");
            // 
            // IndexTab
            // 
            IndexTab.Controls.Add(ListBox1);
            IndexTab.Controls.Add(SearchBoxPanel);
            IndexTab.Location = new Point(4, 22);
            IndexTab.Margin = new Padding(0);
            IndexTab.Name = "IndexTab";
            IndexTab.Size = new Size(232, 420);
            IndexTab.TabIndex = 1;
            IndexTab.Text = "Index";
            IndexTab.UseVisualStyleBackColor = true;
            // 
            // ListBox1
            // 
            ListBox1.Dock = DockStyle.Fill;
            ListBox1.DrawMode = DrawMode.OwnerDrawFixed;
            ListBox1.FormattingEnabled = true;
            ListBox1.Location = new Point(0, 39);
            ListBox1.Name = "ListBox1";
            ListBox1.Size = new Size(232, 381);
            ListBox1.TabIndex = 3;
            // 
            // SearchBoxPanel
            // 
            SearchBoxPanel.BackColor = Color.FromArgb(219, 219, 221);
            SearchBoxPanel.BackgroundImage = (Image)resources.GetObject("SearchBoxPanel.BackgroundImage");
            SearchBoxPanel.Controls.Add(SearchButton);
            SearchBoxPanel.Controls.Add(SearchBox);
            SearchBoxPanel.Dock = DockStyle.Top;
            SearchBoxPanel.Location = new Point(0, 0);
            SearchBoxPanel.Name = "SearchBoxPanel";
            SearchBoxPanel.Size = new Size(232, 39);
            SearchBoxPanel.TabIndex = 5;
            // 
            // SearchButton
            // 
            SearchButton.AccessibleName = "Search";
            SearchButton.Image = DS_Game_Maker.My.Resources.Resources.TestGameIcon;
            SearchButton.Location = new Point(176, 7);
            SearchButton.Name = "SearchButton";
            SearchButton.Size = new Size(26, 26);
            SearchButton.TabIndex = 3;
            SearchButton.UseVisualStyleBackColor = true;
            // 
            // SearchBox
            // 
            SearchBox.BackgroundImage = (Image)resources.GetObject("SearchBox.BackgroundImage");
            SearchBox.Controls.Add(SearchBoxTB);
            SearchBox.Location = new Point(8, 8);
            SearchBox.Name = "SearchBox";
            SearchBox.Size = new Size(166, 23);
            SearchBox.TabIndex = 4;
            // 
            // SearchBoxTB
            // 
            SearchBoxTB.BorderStyle = BorderStyle.None;
            SearchBoxTB.Font = new Font("Tahoma", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            SearchBoxTB.ForeColor = Color.Gray;
            SearchBoxTB.Location = new Point(21, 4);
            SearchBoxTB.Name = "SearchBoxTB";
            SearchBoxTB.Size = new Size(130, 15);
            SearchBoxTB.TabIndex = 3;
            SearchBoxTB.Text = "Hello, World";
            // 
            // DocBrowser
            // 
            DocBrowser.Dock = DockStyle.Fill;
            DocBrowser.Location = new Point(256, 0);
            DocBrowser.MinimumSize = new Size(20, 20);
            DocBrowser.Name = "DocBrowser";
            DocBrowser.Size = new Size(368, 446);
            DocBrowser.TabIndex = 2;
            // 
            // HelpViewer
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(225, 225, 225);
            ClientSize = new Size(624, 446);
            Controls.Add(DocBrowser);
            Controls.Add(SideTabControl);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "HelpViewer";
            Text = "DS Game Maker Help";
            WindowState = FormWindowState.Maximized;
            SideTabControl.ResumeLayout(false);
            ContentsTab.ResumeLayout(false);
            IndexTab.ResumeLayout(false);
            SearchBoxPanel.ResumeLayout(false);
            SearchBox.ResumeLayout(false);
            SearchBox.PerformLayout();
            Load += new EventHandler(HelpViewer_Load);
            ResumeLayout(false);

        }
        internal TabControl SideTabControl;
        internal TabPage ContentsTab;
        internal TabPage IndexTab;
        internal WebBrowser DocBrowser;
        internal TreeView MainTreeView;
        internal Panel SearchBoxPanel;
        internal TextBox SearchBoxTB;
        internal Panel SearchBox;
        internal Button SearchButton;
        internal ImageList MainImageList;
        internal ListBox ListBox1;
    }
}