using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace DS_Game_Maker
{

    public partial class Compiled
    {
        public Compiled()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OpenCompileFolderButton_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", DS_Game_Maker.SessionsLib.CompilePath);
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            DS_Game_Maker.DSGMlib.NOGBAShizzle();
        }

        private void SaveNDSFileButton_Click(object sender, EventArgs e)
        {
            string Response = DS_Game_Maker.DSGMlib.SaveFile(string.Empty, "NDS Binaries|*.nds", DS_Game_Maker.DSGMlib.CacheProjectName + ".nds");
            if (Response.Length == 0)
                return;
            File.Copy(DS_Game_Maker.SessionsLib.CompilePath + DS_Game_Maker.SessionsLib.CompileName + ".nds", Response, true);
        }

        private void SavetoKitButton_Click(object sender, EventArgs e)
        {
            string HBKitDrive = string.Empty;
            foreach (string X in Directory.GetLogicalDrives())
            {
                if (File.Exists(X + "_DS_MENU.DAT") | File.Exists(X + "Redant.DAT"))
                {
                    HBKitDrive = X;
                    break;
                }
            }
            if (HBKitDrive.Length == 0)
            {
                DS_Game_Maker.DSGMlib.MsgWarn("There is no USB Reader for a DS Homebrew Kit connected.");
                return;
            }
            File.Copy(DS_Game_Maker.SessionsLib.CompilePath + DS_Game_Maker.SessionsLib.CompileName + ".nds", HBKitDrive + DS_Game_Maker.DSGMlib.CacheProjectName + ".nds", true);
            byte Response = (byte)MessageBox.Show("'" + DS_Game_Maker.DSGMlib.CacheProjectName + "' was copied successfully." + Constants.vbCrLf + Constants.vbCrLf + "Safely disconnect USB Reader now?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (!(Response == (int)MsgBoxResult.Yes))
                return;
            DS_Game_Maker.DSGMlib.RunBatchString("EjectMedia " + HBKitDrive + Constants.vbCrLf + "RemoveDrive " + HBKitDrive, DS_Game_Maker.DSGMlib.AppPath, false);
        }
    }
}