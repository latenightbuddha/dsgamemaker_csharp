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
            MainToolStrip.Renderer = new clsToolstripRenderer();
            NameTextBox.Text = BackgroundName;
            Text = BackgroundName;
            RealPath = SessionsLib.SessionPath + @"Backgrounds\" + BackgroundName + ".png";
            TempPath = SessionsLib.SessionPath + @"Backgrounds\" + BackgroundName + "_Copy.png";
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
                if (DSGMlib.GUIResNameChecker(NewName))
                    return;
            }
            File.Delete(RealPath);
            File.Move(TempPath, RealPath);
            if (!((NewName ?? "") == (BackgroundName ?? "")))
            {
                DSGMlib.XDSChangeLine("BACKGROUND " + BackgroundName, "BACKGROUND " + NewName);
                DSGMlib.SilentMoveFile(RealPath, SessionsLib.SessionPath + @"Backgrounds\" + NewName + ".png");
                // File.Move(RealPath, SessionPath + "Backgrounds\" + NewName + ".png")
                foreach (Form X in Program.Forms.main_Form.MdiChildren)
                {
                    if (X.Name == "Room")
                    {
                        Room DForm = (Room)X;
                        DForm.RenameBackground(BackgroundName, NewName);
                    }
                    else if (X.Name == "DObject")
                    {
                        DObject DForm = (DObject)X;
                        DForm.MyXDSLines = DSGMlib.UpdateActionsName(DForm.MyXDSLines, "Background", BackgroundName, NewName, false);
                    }
                }
                if (DSGMlib.BGsToRedo.Contains(BackgroundName))
                {
                    byte P = 0;
                    foreach (string D in DSGMlib.BGsToRedo)
                    {
                        if ((D ?? "") == (BackgroundName ?? ""))
                            break;
                        P = (byte)(P + 1);
                    }
                    DSGMlib.BGsToRedo[(int)P] = NewName;
                }
                DSGMlib.UpdateArrayActionsName("Background", BackgroundName, NewName, false);
                DSGMlib.CurrentXDS = DSGMlib.UpdateActionsName(DSGMlib.CurrentXDS, "Background", BackgroundName, NewName, false);
                foreach (string X_ in DSGMlib.GetXDSFilter("ROOM "))
                {
                    string X = X_;
                    string Backup = X;
                    X = X.Substring(5);
                    bool TopChange = false;
                    bool BottomChange = false;
                    if ((DSGMlib.iGet(X, (byte)4, ",") ?? "") == (BackgroundName ?? ""))
                        TopChange = true;
                    if ((DSGMlib.iGet(X, (byte)8, ",") ?? "") == (BackgroundName ?? ""))
                        BottomChange = true;
                    if (TopChange & BottomChange)
                    {
                        string NewLine = "ROOM ";
                        for (byte I = 0; I <= 3; I++)
                            NewLine += DSGMlib.iGet(X, I, ",") + ",";
                        NewLine += NewName + ",";
                        for (byte I = 5; I <= 7; I++)
                            NewLine += DSGMlib.iGet(X, I, ",") + ",";
                        NewLine += NewName;
                        DSGMlib.XDSChangeLine(Backup, NewLine);
                    }
                    else
                    {
                        if (TopChange)
                        {
                            string NewLine = "ROOM ";
                            for (byte I = 0; I <= 3; I++)
                                NewLine += DSGMlib.iGet(X, I, ",") + ",";
                            NewLine += NewName + ",";
                            for (byte I = 5; I <= 8; I++)
                                NewLine += DSGMlib.iGet(X, I, ",") + ",";
                            NewLine = NewLine.Substring(0, NewLine.Length - 1);
                            DSGMlib.XDSChangeLine(Backup, NewLine);
                        }
                        if (BottomChange)
                        {
                            string NewLine = "ROOM ";
                            for (byte I = 0; I <= 7; I++)
                                NewLine += DSGMlib.iGet(X, I, ",") + ",";
                            NewLine += NewName;
                            DSGMlib.XDSChangeLine(Backup, NewLine);
                        }
                    }
                }
            }
            if (ImageChanged)
            {
                if (DSGMlib.BGsToRedo.Contains(BackgroundName))
                    DSGMlib.BGsToRedo.Remove(BackgroundName);
                DSGMlib.BGsToRedo.Add(NewName);
                foreach (Form X in Program.Forms.main_Form.MdiChildren)
                {
                    if (!(X.Name == "Room"))
                        continue;
                    if ((((Room)X).TopBG ?? "") == (NewName ?? ""))
                    {
                        ((Room)X).RefreshRoom(true);
                    }
                    if ((((Room)X).BottomBG ?? "") == (NewName ?? ""))
                    {
                        ((Room)X).RefreshRoom(false);
                    }
                }
                // Remove the old files (no use!!!!!)
                File.Delete(SessionsLib.CompilePath + @"gfx\" + BackgroundName + ".png");
                if (Directory.Exists(SessionsLib.CompilePath + @"gfx\bin"))
                {
                    File.Delete(SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + ".c");
                    File.Delete(SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + "_Tiles.bin");
                    File.Delete(SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + "_Map.bin");
                    File.Delete(SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + "_Pal.bin");
                }
            }
            if (!((BackgroundName ?? "") == (NewName ?? "")))
            {
                if (File.Exists(SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + ".c"))
                {
                    var BackupDate = File.GetLastWriteTime(SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + ".c");
                    // SilentMoveFile(CompilePath + "gfx\bin\" + BackgroundName + ".c", CompilePath + "gfx\bin\" + NewName + ".c")
                    File.Move(SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + ".c", SessionsLib.CompilePath + @"gfx\bin\" + NewName + ".c");
                    string ToWrite = string.Empty;
                    string ToPaste = string.Empty;
                    foreach (string X in DSGMlib.StringToLines(DSGMlib.PathToString(SessionsLib.CompilePath + @"gfx\bin\" + NewName + ".c")))
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
                    File.WriteAllText(SessionsLib.CompilePath + @"gfx\bin\" + NewName + ".c", ToWrite);
                    File.SetLastWriteTime(SessionsLib.CompilePath + @"gfx\bin\" + NewName + ".c", BackupDate);
                }
                if (File.Exists(SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + "_Map.bin"))
                {
                    DSGMlib.SilentMoveFile(SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + "_Map.bin", SessionsLib.CompilePath + @"gfx\bin\" + NewName + "_Map.bin");
                }
                if (File.Exists(SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + "_Tiles.bin"))
                {
                    DSGMlib.SilentMoveFile(SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + "_Tiles.bin", SessionsLib.CompilePath + @"gfx\bin\" + NewName + "_Tiles.bin");
                }
                if (File.Exists(SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + "_Pal.bin"))
                {
                    DSGMlib.SilentMoveFile(SessionsLib.CompilePath + @"gfx\bin\" + BackgroundName + "_Pal.bin", SessionsLib.CompilePath + @"gfx\bin\" + NewName + "_Pal.bin");
                }
                if (File.Exists(SessionsLib.CompilePath + @"gfx\" + BackgroundName + ".png"))
                {
                    DSGMlib.SilentMoveFile(SessionsLib.CompilePath + @"gfx\" + BackgroundName + ".png", SessionsLib.CompilePath + @"gfx\" + NewName + ".png");
                }
                string NewString = string.Empty;
                foreach (string X_ in DSGMlib.StringToLines(DSGMlib.PathToString(SessionsLib.CompilePath + @"gfx\dsgm_gfx.h")))
                {
                    string X = X_;
                    if ((X ?? "") == ("extern const PA_BgStruct " + BackgroundName + ";" ?? ""))
                        X = "extern const PA_BgStruct " + NewName + ";";
                    NewString += X + Constants.vbCrLf;
                }
                File.WriteAllText(SessionsLib.CompilePath + @"gfx\temp_gfx.h", NewString);
                File.Delete(SessionsLib.CompilePath + @"gfx\dsgm_gfx.h");
                DSGMlib.SilentMoveFile(SessionsLib.CompilePath + @"gfx\temp_gfx.h", SessionsLib.CompilePath + @"gfx\dsgm_gfx.h");
            }
            foreach (TreeNode X in Program.Forms.main_Form.ResourcesTreeView.Nodes[(int)DSGMlib.ResourceIDs.Background].Nodes)
            {
                if ((X.Text ?? "") == (BackgroundName ?? ""))
                    X.Text = NewName;
            }
            Close();
        }

        public void MakePreview()
        {
            PreviewPanel.BackgroundImage = DSGMlib.PathToImage(TempPath);
        }

        private void LoadfromFileButton_Click(object sender, EventArgs e)
        {
            string Result = DSGMlib.OpenFile(string.Empty, DSGMlib.ImageFilter);
            if (Result.Length == 0)
                return;
            var NewSize = DSGMlib.PathToImage(Result).Size;
            if (!(NewSize.Width % 256 == 0))
            {
                DSGMlib.MsgError("The width of the Background must be a multiple of 256 pixels.");
                return;
            }
            if (!(NewSize.Height % 256 == 0) & !(NewSize.Height == 192))
            {
                DSGMlib.MsgError("The height of the Background must be either 192 pixels high or a multiple of 256 pixels.");
                return;
            }
            File.Delete(TempPath);
            File.Copy(Result, TempPath, true);
            ImageChanged = true;
            MakePreview();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (!DSGMlib.EditImage(TempPath, BackgroundName, false))
                return;
            ImageChanged = true;
            MakePreview();
        }
    }
}