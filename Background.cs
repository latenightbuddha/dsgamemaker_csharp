using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic;

namespace DS_Game_Maker
{

    public partial class Background
    {

        public string BackgroundName;
        private string RealPath;
        private string TempPath;
        private bool ImageChanged;

        public Background()
        {
            InitializeComponent();
        }

        private void Background_Load(object sender, EventArgs e)
        {
            MainToolStrip.Renderer = new DS_Game_Maker.clsToolstripRenderer();
            NameTextBox.Text = BackgroundName;
            Text = BackgroundName;
            RealPath = DS_Game_Maker.SessionsLib.SessionPath + @"Backgrounds\" + BackgroundName + ".png";
            TempPath = DS_Game_Maker.SessionsLib.SessionPath + @"Backgrounds\" + BackgroundName + "_Copy.png";
            File.Delete(TempPath);
            File.Copy(RealPath, TempPath);
            ImageChanged = false;
            MakePreview();
            // Me.Width = 300
            // Me.Height = 300
            // Dim BGWidth As Int16 = PreviewPanel.BackgroundImage.Width
            // Dim BGHeight As Int16 = PreviewPanel.BackgroundImage.Height
            // Me.Width = 24 + BGWidth
            // Me.Height = 100 + BGHeight
        }

        private void DAcceptButton_Click(object sender, EventArgs e)
        {
            string NewName = NameTextBox.Text;
            if (!((BackgroundName ?? "") == (NewName ?? "")))
            {
                if (DS_Game_Maker.DSGMlib.GUIResNameChecker(NewName))
                    return;
            }
            File.Delete(RealPath);
            File.Move(TempPath, RealPath);
            if (!((NewName ?? "") == (BackgroundName ?? "")))
            {
                DS_Game_Maker.DSGMlib.XDSChangeLine("BACKGROUND " + BackgroundName, "BACKGROUND " + NewName);
                DS_Game_Maker.DSGMlib.SilentMoveFile(RealPath, DS_Game_Maker.SessionsLib.SessionPath + @"Backgrounds\" + NewName + ".png");
                // File.Move(RealPath, SessionPath + "Backgrounds\" + NewName + ".png")
                foreach (Form X in Program.Forms.main_Form.MdiChildren)
                {
                    if (X.Name == "Room")
                    {
                        DS_Game_Maker.Room DForm = (DS_Game_Maker.Room)X;
                        DForm.RenameBackground(BackgroundName, NewName);
                    }
                    else if (X.Name == "DObject")
                    {
                        DS_Game_Maker.DObject DForm = (DS_Game_Maker.DObject)X;
                        DForm.MyXDSLines = DS_Game_Maker.DSGMlib.UpdateActionsName(DForm.MyXDSLines, "Background", BackgroundName, NewName, false);
                    }
                }
                if (DS_Game_Maker.DSGMlib.BGsToRedo.Contains(BackgroundName))
                {
                    byte P = 0;
                    foreach (string D in DS_Game_Maker.DSGMlib.BGsToRedo)
                    {
                        if ((D ?? "") == (BackgroundName ?? ""))
                            break;
                        P = (byte)(P + 1);
                    }
                    DS_Game_Maker.DSGMlib.BGsToRedo[(int)P] = NewName;
                }
                DS_Game_Maker.DSGMlib.UpdateArrayActionsName("Background", BackgroundName, NewName, false);
                DS_Game_Maker.DSGMlib.CurrentXDS = DS_Game_Maker.DSGMlib.UpdateActionsName(DS_Game_Maker.DSGMlib.CurrentXDS, "Background", BackgroundName, NewName, false);
                foreach (string X_ in DS_Game_Maker.DSGMlib.GetXDSFilter("ROOM "))
                {
                    string X = X_;
                    string Backup = X;
                    X = X.Substring(5);
                    bool TopChange = false;
                    bool BottomChange = false;
                    if ((DS_Game_Maker.DSGMlib.iGet(X, (byte)4, ",") ?? "") == (BackgroundName ?? ""))
                        TopChange = true;
                    if ((DS_Game_Maker.DSGMlib.iGet(X, (byte)8, ",") ?? "") == (BackgroundName ?? ""))
                        BottomChange = true;
                    if (TopChange & BottomChange)
                    {
                        string NewLine = "ROOM ";
                        for (byte I = 0; I <= 3; I++)
                            NewLine += DS_Game_Maker.DSGMlib.iGet(X, I, ",") + ",";
                        NewLine += NewName + ",";
                        for (byte I = 5; I <= 7; I++)
                            NewLine += DS_Game_Maker.DSGMlib.iGet(X, I, ",") + ",";
                        NewLine += NewName;
                        DS_Game_Maker.DSGMlib.XDSChangeLine(Backup, NewLine);
                    }
                    else
                    {
                        if (TopChange)
                        {
                            string NewLine = "ROOM ";
                            for (byte I = 0; I <= 3; I++)
                                NewLine += DS_Game_Maker.DSGMlib.iGet(X, I, ",") + ",";
                            NewLine += NewName + ",";
                            for (byte I = 5; I <= 8; I++)
                                NewLine += DS_Game_Maker.DSGMlib.iGet(X, I, ",") + ",";
                            NewLine = NewLine.Substring(0, NewLine.Length - 1);
                            DS_Game_Maker.DSGMlib.XDSChangeLine(Backup, NewLine);
                        }
                        if (BottomChange)
                        {
                            string NewLine = "ROOM ";
                            for (byte I = 0; I <= 7; I++)
                                NewLine += DS_Game_Maker.DSGMlib.iGet(X, I, ",") + ",";
                            NewLine += NewName;
                            DS_Game_Maker.DSGMlib.XDSChangeLine(Backup, NewLine);
                        }
                    }
                }
            }
            if (ImageChanged)
            {
                if (DS_Game_Maker.DSGMlib.BGsToRedo.Contains(BackgroundName))
                    DS_Game_Maker.DSGMlib.BGsToRedo.Remove(BackgroundName);
                DS_Game_Maker.DSGMlib.BGsToRedo.Add(NewName);
                foreach (Form X in Program.Forms.main_Form.MdiChildren)
                {
                    if (!(X.Name == "Room"))
                        continue;
                    if ((((DS_Game_Maker.Room)X).TopBG ?? "") == (NewName ?? ""))
                    {
                        ((DS_Game_Maker.Room)X).RefreshRoom(true);
                    }
                    if ((((DS_Game_Maker.Room)X).BottomBG ?? "") == (NewName ?? ""))
                    {
                        ((DS_Game_Maker.Room)X).RefreshRoom(false);
                    }
                }
                // Remove the old files (no use!!!!!)
                File.Delete(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\" + BackgroundName + ".png");
                if (Directory.Exists(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\bin"))
                {
                    File.Delete(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + ".c");
                    File.Delete(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + "_Tiles.bin");
                    File.Delete(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + "_Map.bin");
                    File.Delete(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + "_Pal.bin");
                }
            }
            if (!((BackgroundName ?? "") == (NewName ?? "")))
            {
                if (File.Exists(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + ".c"))
                {
                    var BackupDate = File.GetLastWriteTime(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + ".c");
                    // SilentMoveFile(CompilePath + "gfx\bin\" + BackgroundName + ".c", CompilePath + "gfx\bin\" + NewName + ".c")
                    File.Move(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + ".c", DS_Game_Maker.SessionsLib.CompilePath + @"gfx\bin\" + NewName + ".c");
                    string ToWrite = string.Empty;
                    string ToPaste = string.Empty;
                    foreach (string X in DS_Game_Maker.DSGMlib.StringToLines(DS_Game_Maker.DSGMlib.PathToString(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\bin\" + NewName + ".c")))
                    {
                        ToPaste = X;
                        switch (X ?? "")
                        {
                            case var @case when @case == ("extern const char " + BackgroundName + "_Tiles[];" ?? ""):
                                {
                                    ToPaste = "extern const char " + NewName + "_Tiles[];";
                                    break;
                                }
                            case var case1 when case1 == ("extern const char " + BackgroundName + "_Map[];" ?? ""):
                                {
                                    ToPaste = "extern const char " + NewName + "_Map[];";
                                    break;
                                }
                            case var case2 when case2 == ("extern const char " + BackgroundName + "_Pal[];" ?? ""):
                                {
                                    ToPaste = "extern const char " + NewName + "_Pal[];";
                                    break;
                                }
                            case var case3 when case3 == ("const PA_BgStruct " + BackgroundName + " = {" ?? ""):
                                {
                                    ToPaste = "const PA_BgStruct " + NewName + " = {";
                                    break;
                                }
                            case var case4 when case4 == ("	" + BackgroundName + "_Tiles," ?? ""):
                                {
                                    ToPaste = "	" + NewName + "_Tiles,";
                                    break;
                                }
                            case var case5 when case5 == ("	" + BackgroundName + "_Map," ?? ""):
                                {
                                    ToPaste = "	" + NewName + "_Map,";
                                    break;
                                }
                            case var case6 when case6 == ("	{" + BackgroundName + "_Pal}," ?? ""):
                                {
                                    ToPaste = "	{" + NewName + "_Pal},";
                                    break;
                                }
                        }
                        ToWrite += ToPaste + Constants.vbCrLf;
                    }
                    File.WriteAllText(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\bin\" + NewName + ".c", ToWrite);
                    File.SetLastWriteTime(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\bin\" + NewName + ".c", BackupDate);
                }
                if (File.Exists(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + "_Map.bin"))
                {
                    DS_Game_Maker.DSGMlib.SilentMoveFile(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + "_Map.bin", DS_Game_Maker.SessionsLib.CompilePath + @"gfx\bin\" + NewName + "_Map.bin");
                }
                if (File.Exists(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + "_Tiles.bin"))
                {
                    DS_Game_Maker.DSGMlib.SilentMoveFile(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + "_Tiles.bin", DS_Game_Maker.SessionsLib.CompilePath + @"gfx\bin\" + NewName + "_Tiles.bin");
                }
                if (File.Exists(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + "_Pal.bin"))
                {
                    DS_Game_Maker.DSGMlib.SilentMoveFile(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + "_Pal.bin", DS_Game_Maker.SessionsLib.CompilePath + @"gfx\bin\" + NewName + "_Pal.bin");
                }
                if (File.Exists(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\" + BackgroundName + ".png"))
                {
                    DS_Game_Maker.DSGMlib.SilentMoveFile(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\" + BackgroundName + ".png", DS_Game_Maker.SessionsLib.CompilePath + @"gfx\" + NewName + ".png");
                }
                string NewString = string.Empty;
                foreach (string X_ in DS_Game_Maker.DSGMlib.StringToLines(DS_Game_Maker.DSGMlib.PathToString(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\dsgm_gfx.h")))
                {
                    string X = X_;
                    if ((X ?? "") == ("extern const PA_BgStruct " + BackgroundName + ";" ?? ""))
                        X = "extern const PA_BgStruct " + NewName + ";";
                    NewString += X + Constants.vbCrLf;
                }
                File.WriteAllText(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\temp_gfx.h", NewString);
                File.Delete(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\dsgm_gfx.h");
                DS_Game_Maker.DSGMlib.SilentMoveFile(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\temp_gfx.h", DS_Game_Maker.SessionsLib.CompilePath + @"gfx\dsgm_gfx.h");
            }
            foreach (TreeNode X in Program.Forms.main_Form.ResourcesTreeView.Nodes[(int)DS_Game_Maker.DSGMlib.ResourceIDs.Background].Nodes)
            {
                if ((X.Text ?? "") == (BackgroundName ?? ""))
                    X.Text = NewName;
            }
            Close();
        }

        public void MakePreview()
        {
            PreviewPanel.BackgroundImage = DS_Game_Maker.DSGMlib.PathToImage(TempPath);
        }

        private void LoadfromFileButton_Click(object sender, EventArgs e)
        {
            string Result = DS_Game_Maker.DSGMlib.OpenFile(string.Empty, DS_Game_Maker.DSGMlib.ImageFilter);
            if (Result.Length == 0)
                return;
            var NewSize = DS_Game_Maker.DSGMlib.PathToImage(Result).Size;
            if (!(NewSize.Width % 256 == 0))
            {
                DS_Game_Maker.DSGMlib.MsgError("The width of the Background must be a multiple of 256 pixels.");
                return;
            }
            if (!(NewSize.Height % 256 == 0) & !(NewSize.Height == 192))
            {
                DS_Game_Maker.DSGMlib.MsgError("The height of the Background must be either 192 pixels high or a multiple of 256 pixels.");
                return;
            }
            File.Delete(TempPath);
            File.Copy(Result, TempPath, true);
            ImageChanged = true;
            MakePreview();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (!DS_Game_Maker.DSGMlib.EditImage(TempPath, BackgroundName, false))
                return;
            ImageChanged = true;
            MakePreview();
        }
    }
}