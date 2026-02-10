using System;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic.CompilerServices;

namespace DS_Game_Maker
{
    public partial class GameSettings
    {
        public GameSettings()
        {
            InitializeComponent();
        }

        private void GameSettings_Load(object sender, EventArgs e)
        {
            StartingRoomDropper.Items.Clear();
            foreach (TreeNode X in DS_Game_Maker.My.MyProject.Forms.MainForm.ResourcesTreeView.Nodes[(int)DS_Game_Maker.DSGMlib.ResourceIDs.Room].Nodes)
                StartingRoomDropper.Items.Add(X.Text);
            StartingRoomDropper.Text = DS_Game_Maker.DSGMlib.GetXDSLine("BOOTROOM ").Substring(9);
            ProjectNameTextBox.Text = DS_Game_Maker.DSGMlib.GetXDSLine("PROJECTNAME ").Substring(12);
            Text2TextBox.Text = DS_Game_Maker.DSGMlib.GetXDSLine("TEXT2 ").Substring(6);
            Text3TextBox.Text = DS_Game_Maker.DSGMlib.GetXDSLine("TEXT3 ").Substring(6);
            if (DS_Game_Maker.DSGMlib.GetXDSLine("PROJECTLOGO ").Length > 12)
            {
                IconFileTextbox.Text = DS_Game_Maker.DSGMlib.GetXDSLine("PROJECTLOGO ").Substring(12);
            }
            ScoreDropper.Value = Convert.ToInt16(DS_Game_Maker.DSGMlib.GetXDSLine("SCORE ").Substring(6));
            LivesDropper.Value = Convert.ToInt16(DS_Game_Maker.DSGMlib.GetXDSLine("LIVES ").Substring(6));
            HealthDropper.Value = Convert.ToInt16(DS_Game_Maker.DSGMlib.GetXDSLine("HEALTH ").ToString().Substring(7));
            FATCallCheckBox.Checked = DS_Game_Maker.DSGMlib.GetXDSLine("FAT_CALL ").Substring(9) == "1" ? true : false;
            NitroFSCallCheckBox.Checked = DS_Game_Maker.DSGMlib.GetXDSLine("NITROFS_CALL ").Substring(13) == "1" ? true : false;
            MidPointCheckBox.Checked = DS_Game_Maker.DSGMlib.GetXDSLine("MIDPOINT_COLLISIONS ").Substring(20) == "1" ? true : false;
            IncludeWiFiLibChecker.Checked = DS_Game_Maker.DSGMlib.GetXDSLine("INCLUDE_WIFI_LIB ").Substring(17) == "1" ? true : false;

            IncludeFilesList.Items.Clear();
            NitroFSFilesList.Items.Clear();
            foreach (string P in DS_Game_Maker.DSGMlib.GetXDSFilter("INCLUDE "))
                IncludeFilesList.Items.Add(P.Substring(8));
            foreach (string P in DS_Game_Maker.DSGMlib.GetXDSFilter("NITROFS "))
                NitroFSFilesList.Items.Add(P.Substring(8));
        }

        private void DCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DOkayButton_Click(object sender, EventArgs e)
        {
            var Logo = DS_Game_Maker.DSGMlib.PathToImage(IconOpenFileDialog.FileName);
            if (!(Logo.Width == 32) & !(Logo.Height == 32))
            {
                DS_Game_Maker.DSGMlib.MsgError("Your project's icon must be 32 x 32!");
                return;
            }
            DS_Game_Maker.DSGMlib.XDSChangeLine(DS_Game_Maker.DSGMlib.GetXDSLine("BOOTROOM "), "BOOTROOM " + StartingRoomDropper.Text);
            DS_Game_Maker.DSGMlib.XDSChangeLine(DS_Game_Maker.DSGMlib.GetXDSLine("PROJECTNAME "), "PROJECTNAME " + ProjectNameTextBox.Text);
            DS_Game_Maker.DSGMlib.CacheProjectName = ProjectNameTextBox.Text;
            DS_Game_Maker.DSGMlib.XDSChangeLine(DS_Game_Maker.DSGMlib.GetXDSLine("TEXT2 "), "TEXT2 " + Text2TextBox.Text);
            DS_Game_Maker.DSGMlib.XDSChangeLine(DS_Game_Maker.DSGMlib.GetXDSLine("TEXT3 "), "TEXT3 " + Text3TextBox.Text);
            DS_Game_Maker.DSGMlib.XDSChangeLine(DS_Game_Maker.DSGMlib.GetXDSLine("SCORE "), "SCORE " + ScoreDropper.Value.ToString());
            DS_Game_Maker.DSGMlib.XDSChangeLine(DS_Game_Maker.DSGMlib.GetXDSLine("LIVES "), "LIVES " + LivesDropper.Value.ToString());
            DS_Game_Maker.DSGMlib.XDSChangeLine(DS_Game_Maker.DSGMlib.GetXDSLine("HEALTH "), "HEALTH " + HealthDropper.Value.ToString());
            DS_Game_Maker.DSGMlib.XDSChangeLine(DS_Game_Maker.DSGMlib.GetXDSLine("FAT_CALL "), "FAT_CALL " + (FATCallCheckBox.Checked ? "1" : "0"));
            DS_Game_Maker.DSGMlib.XDSChangeLine(DS_Game_Maker.DSGMlib.GetXDSLine("NITROFS_CALL "), "NITROFS_CALL " + (NitroFSCallCheckBox.Checked ? "1" : "0"));
            DS_Game_Maker.DSGMlib.XDSChangeLine(DS_Game_Maker.DSGMlib.GetXDSLine("MIDPOINT_COLLISIONS "), "MIDPOINT_COLLISIONS " + (MidPointCheckBox.Checked ? "1" : "0"));
            DS_Game_Maker.DSGMlib.XDSChangeLine(DS_Game_Maker.DSGMlib.GetXDSLine("INCLUDE_WIFI_LIB "), "INCLUDE_WIFI_LIB " + (IncludeWiFiLibChecker.Checked ? "1" : "0"));
            if (string.IsNullOrEmpty(DS_Game_Maker.DSGMlib.GetXDSLine("PROJECTLOGO ")))
            {
                DS_Game_Maker.DSGMlib.XDSAddLine("PROJECTLOGO " + IconFileTextbox.Text);
            }
            else
            {
                DS_Game_Maker.DSGMlib.XDSChangeLine(DS_Game_Maker.DSGMlib.GetXDSLine("PROJECTLOGO "), "PROJECTLOGO " + IconFileTextbox.Text);
            }
            Close();
        }

        private void DeleteIncludeFileButton_Click(object sender, EventArgs e)
        {
            if (IncludeFilesList.SelectedIndices.Count == 0)
                return;
            string TheName = Conversions.ToString(IncludeFilesList.Items[IncludeFilesList.SelectedIndex]);
            System.IO.File.Delete(DS_Game_Maker.SessionsLib.SessionPath + @"IncludeFiles\" + TheName);
            DS_Game_Maker.DSGMlib.XDSRemoveLine("INCLUDE " + TheName);
            IncludeFilesList.Items.RemoveAt(IncludeFilesList.SelectedIndex);
        }

        private void DeleteNitroFSFileButton_Click(object sender, EventArgs e)
        {
            if (NitroFSFilesList.SelectedIndices.Count == 0)
                return;
            string TheName = Conversions.ToString(NitroFSFilesList.Items[NitroFSFilesList.SelectedIndex]);
            System.IO.File.Delete(DS_Game_Maker.SessionsLib.SessionPath + @"NitroFSFiles\" + TheName);
            DS_Game_Maker.DSGMlib.XDSRemoveLine("NITROFS " + TheName);
            NitroFSFilesList.Items.RemoveAt(NitroFSFilesList.SelectedIndex);
        }

        public void AddThing(string SysName)
        {
            string Result = DS_Game_Maker.DSGMlib.OpenFile(string.Empty, "All Files|*.*");
            if (Result.Length == 0)
                return;
            string ShortName = Result.Substring(Result.LastIndexOf(@"\") + 1);
            DS_Game_Maker.DSGMlib.XDSAddLine(SysName.ToUpper() + " " + ShortName);
            foreach (Control P in CodingTabPage.Controls)
            {
                if ((P.Name ?? "") == (SysName + "FilesList" ?? ""))
                {
                    ((ListBox)P).Items.Add(ShortName);
                }
            }
            System.IO.File.Copy(Result, DS_Game_Maker.SessionsLib.SessionPath + SysName + @"Files\" + ShortName, true);
        }

        private void AddIncludeFileButton_Click(object sender, EventArgs e)
        {
            AddThing("Include");
        }

        private void AddNitroFSFileButton_Click(object sender, EventArgs e)
        {
            AddThing("NitroFS");
        }

        private void EditIncludeFileButton_Click(object sender, EventArgs e)
        {
            if (IncludeFilesList.SelectedIndices.Count == 0)
                return;
            DS_Game_Maker.DSGMlib.URL(Conversions.ToString(Operators.AddObject(DS_Game_Maker.SessionsLib.SessionPath + @"IncludeFiles\", IncludeFilesList.Items[IncludeFilesList.SelectedIndex])));
        }

        private void EditNitroFSFileButton_Click(object sender, EventArgs e)
        {
            if (NitroFSFilesList.SelectedIndices.Count == 0)
                return;
            DS_Game_Maker.DSGMlib.MsgError(Conversions.ToString(Operators.AddObject(DS_Game_Maker.SessionsLib.SessionPath + @"NitroFSFiles\", NitroFSFilesList.Items[NitroFSFilesList.SelectedIndex])));
            DS_Game_Maker.DSGMlib.URL(Conversions.ToString(Operators.AddObject(DS_Game_Maker.SessionsLib.SessionPath + @"NitroFSFiles\", NitroFSFilesList.Items[NitroFSFilesList.SelectedIndex])));
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            IconOpenFileDialog.ShowDialog();
        }

        private void IconOpenFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IconFileTextbox.Text = IconOpenFileDialog.FileName;
        }

    }
}