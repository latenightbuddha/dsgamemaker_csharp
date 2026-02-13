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
            foreach (TreeNode X in Program.Forms.main_Form.ResourcesTreeView.Nodes[(int)DSGMlib.ResourceIDs.Room].Nodes)
                StartingRoomDropper.Items.Add(X.Text);
            StartingRoomDropper.Text = DSGMlib.GetXDSLine("BOOTROOM ").Substring(9);
            ProjectNameTextBox.Text = DSGMlib.GetXDSLine("PROJECTNAME ").Substring(12);
            Text2TextBox.Text = DSGMlib.GetXDSLine("TEXT2 ").Substring(6);
            Text3TextBox.Text = DSGMlib.GetXDSLine("TEXT3 ").Substring(6);
            if (DSGMlib.GetXDSLine("PROJECTLOGO ").Length > 12)
            {
                IconFileTextbox.Text = DSGMlib.GetXDSLine("PROJECTLOGO ").Substring(12);
            }
            ScoreDropper.Value = Convert.ToInt16(DSGMlib.GetXDSLine("SCORE ").Substring(6));
            LivesDropper.Value = Convert.ToInt16(DSGMlib.GetXDSLine("LIVES ").Substring(6));
            HealthDropper.Value = Convert.ToInt16(DSGMlib.GetXDSLine("HEALTH ").ToString().Substring(7));
            FATCallCheckBox.Checked = DSGMlib.GetXDSLine("FAT_CALL ").Substring(9) == "1" ? true : false;
            NitroFSCallCheckBox.Checked = DSGMlib.GetXDSLine("NITROFS_CALL ").Substring(13) == "1" ? true : false;
            MidPointCheckBox.Checked = DSGMlib.GetXDSLine("MIDPOINT_COLLISIONS ").Substring(20) == "1" ? true : false;
            IncludeWiFiLibChecker.Checked = DSGMlib.GetXDSLine("INCLUDE_WIFI_LIB ").Substring(17) == "1" ? true : false;

            IncludeFilesList.Items.Clear();
            NitroFSFilesList.Items.Clear();
            foreach (string P in DSGMlib.GetXDSFilter("INCLUDE "))
                IncludeFilesList.Items.Add(P.Substring(8));
            foreach (string P in DSGMlib.GetXDSFilter("NITROFS "))
                NitroFSFilesList.Items.Add(P.Substring(8));
        }

        private void DCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DOkayButton_Click(object sender, EventArgs e)
        {
            var Logo = DSGMlib.PathToImage(IconOpenFileDialog.FileName);
            if (!(Logo.Width == 32) & !(Logo.Height == 32))
            {
                DSGMlib.MsgError("Your project's icon must be 32 x 32!");
                return;
            }
            DSGMlib.XDSChangeLine(DSGMlib.GetXDSLine("BOOTROOM "), "BOOTROOM " + StartingRoomDropper.Text);
            DSGMlib.XDSChangeLine(DSGMlib.GetXDSLine("PROJECTNAME "), "PROJECTNAME " + ProjectNameTextBox.Text);
            DSGMlib.CacheProjectName = ProjectNameTextBox.Text;
            DSGMlib.XDSChangeLine(DSGMlib.GetXDSLine("TEXT2 "), "TEXT2 " + Text2TextBox.Text);
            DSGMlib.XDSChangeLine(DSGMlib.GetXDSLine("TEXT3 "), "TEXT3 " + Text3TextBox.Text);
            DSGMlib.XDSChangeLine(DSGMlib.GetXDSLine("SCORE "), "SCORE " + ScoreDropper.Value.ToString());
            DSGMlib.XDSChangeLine(DSGMlib.GetXDSLine("LIVES "), "LIVES " + LivesDropper.Value.ToString());
            DSGMlib.XDSChangeLine(DSGMlib.GetXDSLine("HEALTH "), "HEALTH " + HealthDropper.Value.ToString());
            DSGMlib.XDSChangeLine(DSGMlib.GetXDSLine("FAT_CALL "), "FAT_CALL " + (FATCallCheckBox.Checked ? "1" : "0"));
            DSGMlib.XDSChangeLine(DSGMlib.GetXDSLine("NITROFS_CALL "), "NITROFS_CALL " + (NitroFSCallCheckBox.Checked ? "1" : "0"));
            DSGMlib.XDSChangeLine(DSGMlib.GetXDSLine("MIDPOINT_COLLISIONS "), "MIDPOINT_COLLISIONS " + (MidPointCheckBox.Checked ? "1" : "0"));
            DSGMlib.XDSChangeLine(DSGMlib.GetXDSLine("INCLUDE_WIFI_LIB "), "INCLUDE_WIFI_LIB " + (IncludeWiFiLibChecker.Checked ? "1" : "0"));
            if (string.IsNullOrEmpty(DSGMlib.GetXDSLine("PROJECTLOGO ")))
            {
                DSGMlib.XDSAddLine("PROJECTLOGO " + IconFileTextbox.Text);
            }
            else
            {
                DSGMlib.XDSChangeLine(DSGMlib.GetXDSLine("PROJECTLOGO "), "PROJECTLOGO " + IconFileTextbox.Text);
            }
            Close();
        }

        private void DeleteIncludeFileButton_Click(object sender, EventArgs e)
        {
            if (IncludeFilesList.SelectedIndices.Count == 0)
                return;
            string TheName = Conversions.ToString(IncludeFilesList.Items[IncludeFilesList.SelectedIndex]);
            System.IO.File.Delete(SessionsLib.SessionPath + "IncludeFiles/" + TheName);
            DSGMlib.XDSRemoveLine("INCLUDE " + TheName);
            IncludeFilesList.Items.RemoveAt(IncludeFilesList.SelectedIndex);
        }

        private void DeleteNitroFSFileButton_Click(object sender, EventArgs e)
        {
            if (NitroFSFilesList.SelectedIndices.Count == 0)
                return;
            string TheName = Conversions.ToString(NitroFSFilesList.Items[NitroFSFilesList.SelectedIndex]);
            System.IO.File.Delete(SessionsLib.SessionPath + "NitroFSFiles/" + TheName);
            DSGMlib.XDSRemoveLine("NITROFS " + TheName);
            NitroFSFilesList.Items.RemoveAt(NitroFSFilesList.SelectedIndex);
        }

        public void AddThing(string SysName)
        {
            string Result = DSGMlib.OpenFile(string.Empty, "All Files|*.*");
            if (Result.Length == 0)
                return;
            string ShortName = Result.Substring(Result.LastIndexOf("/") + 1);
            DSGMlib.XDSAddLine(SysName.ToUpper() + " " + ShortName);
            foreach (Control P in CodingTabPage.Controls)
            {
                if ((P.Name ?? "") == (SysName + "FilesList" ?? ""))
                {
                    ((ListBox)P).Items.Add(ShortName);
                }
            }
            System.IO.File.Copy(Result, SessionsLib.SessionPath + SysName + "Files/" + ShortName, true);
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
            DSGMlib.URL(Conversions.ToString(Operators.AddObject(SessionsLib.SessionPath + "IncludeFiles/", IncludeFilesList.Items[IncludeFilesList.SelectedIndex])));
        }

        private void EditNitroFSFileButton_Click(object sender, EventArgs e)
        {
            if (NitroFSFilesList.SelectedIndices.Count == 0)
                return;
            DSGMlib.MsgError(Conversions.ToString(Operators.AddObject(SessionsLib.SessionPath + "NitroFSFiles/", NitroFSFilesList.Items[NitroFSFilesList.SelectedIndex])));
            DSGMlib.URL(Conversions.ToString(Operators.AddObject(SessionsLib.SessionPath + "NitroFSFiles/", NitroFSFilesList.Items[NitroFSFilesList.SelectedIndex])));
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