using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic;

namespace DS_Game_Maker
{
    public partial class Sprite
    {

        public string SpriteName;
        private string TheLine;
        private short TimesChanged = 0;
        public bool DataChanged = false;

        public Sprite()
        {
            InitializeComponent();
        }

        private void Sprite_Load(object sender, EventArgs e)
        {
            MainToolStrip.Renderer = new DS_Game_Maker.clsToolstripRenderer();
            Text = SpriteName;
            NameTextBox.Text = SpriteName;
            TheLine = DS_Game_Maker.DSGMlib.GetXDSLine("SPRITE " + SpriteName + ",");
            short ImageCount = 0;
            MainImageList.Images.Clear();
            foreach (string X_ in Directory.GetFiles(DS_Game_Maker.SessionsLib.SessionPath + "Sprites"))
            {
                string X = X_;
                X = X.Substring(X.LastIndexOf(@"\") + 1);
                X = X.Substring(0, X.LastIndexOf("."));
                X = X.Substring(X.IndexOf("_") + 1);

                string Y = X;
                if (X == SpriteName)
                {
                    if (ImageCount == 0)
                    {
                        // First image! Grab the size....
                        MainImageList.ImageSize = DS_Game_Maker.DSGMlib.PathToImage(Y).Size;
                    }
                    ImageCount = (short)(ImageCount + 1);
                }
            }
            for (short X = 0, loopTo = (short)(ImageCount - 1); X <= loopTo; X++)
            {
                string ThePath = DS_Game_Maker.SessionsLib.SessionPath + @"Sprites\" + X.ToString() + "_" + SpriteName + ".png";
                string Key = "Frame_" + X.ToString();
                MainImageList.Images.Add(Key, DS_Game_Maker.DSGMlib.PathToImage(ThePath));
                MainListView.Items.Add("Frame " + X.ToString(), X);
            }
        }

        private void PreviewButton_Click(object sender, EventArgs e)
        {
            if (FPSTextBox.Text.Length == 0)
            {
                DS_Game_Maker.DSGMlib.MsgWarn("Please enter a Speed.");
                return;
            }
            bool CanProceed = false;
            if (FPSTextBox.Text.Length == 2)
            {
                bool Okay1 = false;
                bool Okay2 = false;
                foreach (var X in DS_Game_Maker.DSGMlib.Numbers)
                {
                    if ((FPSTextBox.Text.Substring(0, 1) ?? "") == (X ?? ""))
                        Okay1 = true;
                    if ((FPSTextBox.Text.Substring(1) ?? "") == (X ?? ""))
                        Okay2 = true;
                }
                if (Okay1 & Okay2)
                    CanProceed = true;
            }
            else if (FPSTextBox.Text.Length == 1)
            {
                foreach (string X in DS_Game_Maker.DSGMlib.Numbers)
                {
                    if ((FPSTextBox.Text ?? "") == (X ?? ""))
                    {
                        CanProceed = true;
                        break;
                    }
                }
            }
            if (Convert.ToByte(FPSTextBox.Text) > 60)
                CanProceed = false;
            if (!CanProceed)
            {
                DS_Game_Maker.DSGMlib.MsgError("You must enter a valid value for FPS between 1 and 60.");
                return;
            }

            Program.Forms.preview_Form.ImageSize = MainImageList.ImageSize;
            Program.Forms.preview_Form.Speed = Convert.ToByte(FPSTextBox.Text);
            Program.Forms.preview_Form.TheImage = DSGMlib.GenerateDSSprite(SpriteName);
            Program.Forms.preview_Form.ShowDialog();
        }

        public bool IsSpriteSizeOkay(Size TheSize)
        {
            short TW = (short)TheSize.Width;
            short TH = (short)TheSize.Height;
            bool Alright = false;
            if (!(TW == 64) & !(TW == 32) & !(TW == 16) & !(TW == 8))
                return false;
            if (!(TH == 64) & !(TH == 32) & !(TH == 16) & !(TW == 8))
                return false;
            if (TW == 64 & TH == 8)
                return false;
            if (TW == 64 & TH == 16)
                return false;
            if (TW == 8 & TH == 64)
                return false;
            if (TW == 16 & TH == 64)
                return false;
            return true;
        }

        private void LoadFrameButton_Click(object sender, EventArgs e)
        {
            if (MainListView.SelectedIndices.Count == 0)
            {
                DS_Game_Maker.DSGMlib.MsgWarn("You must select a Frame into which to copy an existing image file.");
                return;
            }
            string Result = DS_Game_Maker.DSGMlib.OpenFile(string.Empty, DS_Game_Maker.DSGMlib.ImageFilter);
            if (Result.Length == 0)
                return;
            var NIS = DS_Game_Maker.DSGMlib.PathToImage(Result).Size;
            if (!this.IsSpriteSizeOkay(NIS))
            {
                Program.Forms.badSpriteSize_Form.ShowDialog();
                return;
            }
            short CW = (short)MainImageList.ImageSize.Width;
            short CH = (short)MainImageList.ImageSize.Height;
            if (NIS.Width == (int)CW & NIS.Height == (int)CH)
            {
                MainImageList.Images[MainListView.SelectedIndices[0]] = GetMagentaedBMP(Result);
                DataChanged = true;
                return;
            }
            if (NIS.Width > (int)CW | NIS.Height > (int)CH)
            {
                MainImageList.ImageSize = NIS;
                MainImageList.Images[MainListView.SelectedIndices[0]] = GetMagentaedBMP(Result);
                DataChanged = true;
                return;
            }
            if (NIS.Width < (int)CW | NIS.Height < (int)CH)
            {
                var NewImage = new Bitmap(NIS.Width, NIS.Height);
                var NewImageGFX = Graphics.FromImage(NewImage);
                NewImageGFX.Clear(Color.Magenta);
                NewImageGFX.DrawImage(GetMagentaedBMP(Result), new Point(0, 0));
                NewImageGFX.Dispose();
                MainImageList.Images[MainListView.SelectedIndices[0]] = NewImage;
            }
            DataChanged = true;
        }

        private void AddBlankFrameButton_Click(object sender, EventArgs e)
        {
            string TheKey = "Frame_" + MainImageList.Images.Count.ToString();
            var Newun = new Bitmap(MainImageList.ImageSize.Width, MainImageList.ImageSize.Height);
            var NewunGFX = Graphics.FromImage(Newun);
            NewunGFX.Clear(Color.Magenta);
            MainImageList.Images.Add(TheKey, Newun);
            NewunGFX.Dispose();
            Newun.Dispose();
            MainListView.Items.Add(TheKey.Replace("_", " "), MainImageList.Images.Count - 1);
            DataChanged = true;
        }

        private void AddFrameFromFileButton_Click(object sender, EventArgs e)
        {
            string Response = DS_Game_Maker.DSGMlib.OpenFile(string.Empty, DS_Game_Maker.DSGMlib.ImageFilter);
            if (Response.Length == 0)
                return;
            string TheKey = "Frame_" + MainImageList.Images.Count.ToString();
            var NIS = DS_Game_Maker.DSGMlib.PathToImage(Response).Size;
            short CW = (short)MainImageList.ImageSize.Width;
            short CH = (short)MainImageList.ImageSize.Height;
            if (NIS.Width == (int)CW & NIS.Height == (int)CH)
            {
                MainImageList.Images.Add(TheKey, GetMagentaedBMP(Response));
                MainListView.Items.Add(TheKey.Replace("_", " "), MainImageList.Images.Count - 1);
                DataChanged = true;
                return;
            }
            if (NIS.Width > (int)CW | NIS.Height > (int)CH)
            {
                MainImageList.ImageSize = NIS;
                MainImageList.Images.Add(TheKey, GetMagentaedBMP(Response));
                MainListView.Items.Add(TheKey.Replace("_", " "), MainImageList.Images.Count - 1);
                DataChanged = true;
                return;
            }
            if (NIS.Width < (int)CW | NIS.Height < (int)CH)
            {
                var NewImage = new Bitmap(NIS.Width, NIS.Height);
                var NewImageGFX = Graphics.FromImage(NewImage);
                NewImageGFX.Clear(Color.Magenta);
                NewImageGFX.DrawImage(GetMagentaedBMP(Response), new Point(0, 0));
                NewImageGFX.Dispose();
                MainImageList.Images.Add(TheKey, NewImage);
                MainListView.Items.Add(TheKey.Replace("_", " "), MainImageList.Images.Count - 1);
            }
            DataChanged = true;
        }

        private void EditFrameButton_Click(object sender, EventArgs e)
        {
            if (MainListView.SelectedIndices.Count == 0)
                return;
            if (MainListView.SelectedIndices.Count > 1)
            {
                DS_Game_Maker.DSGMlib.MsgWarn("You may only edit one Frame at once.");
                return;
            }
            short ID = (short)MainListView.SelectedIndices[0];
            string CopyPath = DS_Game_Maker.SessionsLib.SessionPath + "Sprite_Edit.png";
            MainImageList.Images[ID].Save(DS_Game_Maker.SessionsLib.SessionPath + "Sprite_Edit.png");
            if (DS_Game_Maker.DSGMlib.EditImage(CopyPath, SpriteName, false))
            {
                MainImageList.Images[ID] = DS_Game_Maker.DSGMlib.PathToImage(CopyPath);
                File.Delete(CopyPath);
                DataChanged = true;
            }
        }

        private void DeleteFrameButton_Click(object sender, EventArgs e)
        {
            if (MainListView.SelectedIndices.Count == 0)
                return;
            if (MainListView.Items.Count == 1)
            {
                DS_Game_Maker.DSGMlib.MsgWarn("A Sprite must have at least one Frame.");
                return;
            }
            if (MainListView.Items.Count == MainListView.SelectedIndices.Count)
            {
                DS_Game_Maker.DSGMlib.MsgWarn("You cannot delete all of the Frames at once: at least one frame must remain.");
                return;
            }
            short ID = (short)MainListView.SelectedIndices[0];
            // For Each ID As Byte In MainListView.SelectedIndices
            // IO.File.Delete(SessionPath + "Sprites\" + ID.ToString + "_" + SpriteName + ".png")
            // Next
            // Directory.CreateDirectory(SessionPath + "Sprites\" + SpriteName)
            // For Each X As String In Directory.GetFiles(SessionPath + "Sprites")
            // Dim Backup As String = X
            // X = X.Substring(X.LastIndexOf("\") + 1)
            // X = X.Substring(0, X.LastIndexOf("."))
            // Dim ID As Byte = Convert.ToByte(X.Substring(0, X.IndexOf("_")))
            // X = X.Substring(X.IndexOf("_") + 1)
            // If X = SpriteName Then
            // File.Move(Backup, SessionPath + "Sprites\" + SpriteName + "\" + ID.ToString + ".png")
            // End If
            // Next
            // Dim DOn As Byte = 0
            // For Each X As String In Directory.GetFiles(SessionPath + "Sprites\" + SpriteName)
            // IO.File.Move(X, SessionPath + "Sprites\" + DOn.ToString + "_" + SpriteName + ".png")
            // DOn += 1
            // Next
            // Directory.Delete(SessionPath + "Sprites\" + SpriteName)
            // SyncImages()
            MainImageList.Images.RemoveAt(ID);
            MainListView.Items.RemoveAt(ID);
            for (short i = 0, loopTo = (short)(MainListView.Items.Count - 1); i <= loopTo; i++)
                MainListView.Items[i].ImageIndex = i;
            DataChanged = true;
        }

        private void ExportDSSpriteStripButton_Click(object sender, EventArgs e)
        {
            Image ToSave = DS_Game_Maker.DSGMlib.GenerateDSSprite(SpriteName);
            string Response = DS_Game_Maker.DSGMlib.SaveFile(string.Empty, DS_Game_Maker.DSGMlib.ImageFilter, SpriteName + "_DS");
            if (Response.Length == 0)
                return;
            ToSave.Save(Response);
            ToSave.Dispose();
        }

        private void PasteButton_Click(object sender, EventArgs e)
        {
            try
            {
                string TheKey = "Frame_" + MainImageList.Images.Count.ToString();
                MainImageList.Images.Add(TheKey, Clipboard.GetImage());
                MainListView.Items.Add(TheKey.Replace("_", " "), MainImageList.Images.Count - 1);
                DataChanged = true;
            }
            catch (Exception ex)
            {
                DS_Game_Maker.DSGMlib.MsgWarn("There is no image on the clipboard." + Constants.vbCrLf + Constants.vbCrLf + "(" + ex.Message + ")");
            }
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            if (MainListView.SelectedIndices.Count == 0)
            {
                DS_Game_Maker.DSGMlib.MsgWarn("You must select a Frame to Copy.");
                return;
            }
            if (MainListView.SelectedIndices.Count > 1)
            {
                DS_Game_Maker.DSGMlib.MsgWarn("You may only copy on Frame at a time.");
                return;
            }
            Clipboard.SetImage(MainImageList.Images[MainListView.SelectedIndices[0]]);
        }

        private void CutButton_Click(object sender, EventArgs e)
        {
            if (MainListView.SelectedIndices.Count == 0)
            {
                DS_Game_Maker.DSGMlib.MsgWarn("You must select a Frame to Copy.");
                return;
            }
            if (MainListView.SelectedIndices.Count > 1)
            {
                DS_Game_Maker.DSGMlib.MsgWarn("You may only copy on Frame at a time.");
                return;
            }
            Clipboard.SetImage(MainImageList.Images[MainListView.SelectedIndices[0]]);
            DeleteFrameButton_Click(new object(), new EventArgs());
        }

        private void TransformButton_Click(object sender, EventArgs e)
        {
            if (MainListView.SelectedIndices.Count == 0)
            {
                for (short X = 0, loopTo = (short)(MainListView.Items.Count - 1); X <= loopTo; X++)
                    MainListView.SelectedIndices.Add(X);
                // MsgWarn("You must select a Frame to Transform.") : Exit Sub
            }

            Program.Forms.transformSprite_Form.Text = "Transform " + MainListView.SelectedIndices.Count.ToString() + " Frame";
            Program.Forms.transformSprite_Form.Text += MainListView.SelectedIndices.Count > 1 ? "s" : string.Empty;
            // TransformSprite.MainTabControl.TabPages(0).Text += If(MainListView.SelectedIndices.Count > 1, "s", String.Empty)
            Program.Forms.transformSprite_Form.ImagePaths.Clear();
            foreach (byte X in MainListView.SelectedIndices)
                Program.Forms.transformSprite_Form.ImagePaths.Add(DS_Game_Maker.SessionsLib.SessionPath + @"Sprites\" + X.ToString() + "_" + SpriteName + ".png");
            Program.Forms.transformSprite_Form.ShowDialog();
            DataChanged = true;
        }

        private void FromSheetButton_Click(object sender, EventArgs e)
        {
            byte MSResponse = (byte)MessageBox.Show("Importing from a Sheet will remove all existing frames." + Constants.vbCrLf + Constants.vbCrLf + "Would you like to Continue?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (!(MSResponse == (int)MsgBoxResult.Yes))
                return;
            string Response = DS_Game_Maker.DSGMlib.OpenFile(string.Empty, DS_Game_Maker.DSGMlib.ImageFilter);
            if (Response.Length == 0)
                return;
            string ThePath = DS_Game_Maker.SessionsLib.SessionPath + @"Sprites\" + SpriteName + "_Import";
            Directory.CreateDirectory(ThePath);


            Program.Forms.importSprite_Form.FileName = Response;
            Program.Forms.importSprite_Form.ToDirectory = ThePath;
            Program.Forms.importSprite_Form.ShowDialog();
            if ((int)Program.Forms.importSprite_Form.ImportedCount == 0)
                return;
            // For Each X As String In Directory.GetFiles(SessionPath + "Sprites")
            // Dim Backup As String = X
            // X = X.Substring(X.LastIndexOf("\") + 1)
            // X = X.Substring(0, X.LastIndexOf("."))
            // X = X.Substring(X.IndexOf("_") + 1)
            // If X = SpriteName Then File.Delete(Backup)
            // Next
            short ImageCount = 0;
            MainImageList.Images.Clear();
            MainListView.Items.Clear();
            foreach (string X in Directory.GetFiles(ThePath))
            {
                if (ImageCount == 0)
                    MainImageList.ImageSize = DS_Game_Maker.DSGMlib.PathToImage(X).Size;
                ImageCount = (short)(ImageCount + 1);
            }
            TimesChanged = 0;
            for (short X = 0, loopTo = (short)(ImageCount - 1); X <= loopTo; X++)
            {
                string TheKey = "Frame_" + MainImageList.Images.Count.ToString();
                MainImageList.Images.Add(TheKey, DS_Game_Maker.DSGMlib.PathToImage(ThePath + @"\" + X.ToString() + ".png"));
                MainListView.Items.Add(TheKey.Replace("_", " "), MainImageList.Images.Count - 1);
                File.Delete(ThePath + @"\" + X.ToString() + ".png");
                // File.Move(ThePath + "\" + X.ToString + ".png", SessionPath + "Sprites\" + X.ToString + "_" + SpriteName + ".png")
            }
            Directory.Delete(ThePath);
            DataChanged = true;
        }

        // Private Sub Droppers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        // If TimesChanged < 2 Then TimesChanged += 1 : Exit Sub
        // Dim NewWidth As Byte = Convert.ToByte(WidthDropper.Text)
        // Dim NewHeight As Byte = Convert.ToByte(HeightDropper.Text)
        // If NewWidth < CurrentSize.Width Or NewHeight < CurrentSize.Height Then
        // Dim Message As String = "You are about to reduce the size of the Sprite. This will permamently remove image data." + vbcrlf + vbcrlf + "Would you like to continue?"
        // Dim Response As Byte = MessageBox.Show(Message, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        // If Not Response = MsgBoxResult.Yes Then Exit Sub
        // End If
        // Dim ImageCount As Int16 = 0
        // Dim ThePath As String = SessionPath + "Sprites\"
        // For Each X As String In Directory.GetFiles(ThePath)
        // X = X.Substring(X.LastIndexOf("\") + 1)
        // X = X.Substring(0, X.LastIndexOf("."))
        // X = X.Substring(X.IndexOf("_") + 1)
        // If X = SpriteName Then ImageCount += 1
        // Next
        // For X As Int16 = 0 To ImageCount - 1
        // Dim Canvas As New Bitmap(NewWidth, NewHeight)
        // Dim CanvasGFX As Graphics = Graphics.FromImage(Canvas)
        // CanvasGFX.Clear(Color.Magenta)
        // CanvasGFX.DrawImage(PathToImage(ThePath + X.ToString + "_" + SpriteName + ".png"), New Point(0, 0))
        // CanvasGFX.Dispose()
        // Canvas.Save(ThePath + X.ToString + "_" + SpriteName + ".png")
        // Next
        // CurrentSize.Width = Convert.ToInt16(WidthDropper.Text)
        // CurrentSize.Height = Convert.ToInt16(HeightDropper.Text)
        // SyncImages()
        // DataChanged = True
        // End Sub

        private void DAcceptButton_Click(object sender, EventArgs e)
        {
            string NewName = NameTextBox.Text;
            if (NewName == "None")
            {
                DS_Game_Maker.DSGMlib.MsgWarn("'None' is not a valid name.");
                return;
            }
            if (!((SpriteName ?? "") == (NewName ?? "")))
            {
                if (DS_Game_Maker.DSGMlib.GUIResNameChecker(NewName))
                    return;
                if (File.Exists(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\" + SpriteName + ".png"))
                {
                    DS_Game_Maker.DSGMlib.SilentMoveFile(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\" + SpriteName + ".png", DS_Game_Maker.SessionsLib.CompilePath + @"gfx\" + NewName + ".png");
                }
                if (File.Exists(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\bin\" + SpriteName + "_Sprite.bin"))
                {
                    DS_Game_Maker.DSGMlib.SilentMoveFile(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\bin\" + SpriteName + "_Sprite.bin", DS_Game_Maker.SessionsLib.CompilePath + @"gfx\bin\" + NewName + "_Sprite.bin");
                }
                var BackupDate = File.GetLastWriteTime(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\dsgm_gfx.h");
                string FinalString = string.Empty;
                string ToAdd = string.Empty;
                foreach (string X in DS_Game_Maker.DSGMlib.StringToLines(DS_Game_Maker.DSGMlib.PathToString(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\dsgm_gfx.h")))
                {
                    ToAdd = X;
                    if (X.Contains(" char " + SpriteName + "_Sprite["))
                    {
                        ToAdd = X.Replace(" char " + SpriteName + "_Sprite[", " char " + NewName + "_Sprite[");
                    }
                    FinalString += ToAdd + Constants.vbCrLf;
                }
                File.WriteAllText(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\dsgm_gfx.h", FinalString);
                File.SetLastWriteTime(DS_Game_Maker.SessionsLib.CompilePath + @"gfx\dsgm_gfx.h", BackupDate);
            }
            if (File.Exists(DS_Game_Maker.SessionsLib.CompilePath + @"build\" + SpriteName + "_Sprite.h"))
            {
                File.Delete(DS_Game_Maker.SessionsLib.CompilePath + @"build\" + SpriteName + "_Sprite.h");
            }
            if (File.Exists(DS_Game_Maker.SessionsLib.CompilePath + @"build\" + SpriteName + "_Sprite.o"))
            {
                File.Delete(DS_Game_Maker.SessionsLib.CompilePath + @"build\" + SpriteName + "_Sprite.o");
            }
            string OldLine = DS_Game_Maker.DSGMlib.GetXDSLine("SPRITE " + SpriteName + ",");
            string NewLine = "SPRITE " + NewName + "," + MainImageList.ImageSize.Width.ToString() + "," + MainImageList.ImageSize.Height.ToString();
            DS_Game_Maker.DSGMlib.XDSChangeLine(OldLine, NewLine);
            var AffectedObjects = new List<string>();
            foreach (string X in DS_Game_Maker.DSGMlib.GetXDSFilter("OBJECT "))
            {
                if ((DS_Game_Maker.DSGMlib.iGet(X, (byte)1, ",") ?? "") == (SpriteName ?? ""))
                    AffectedObjects.Add(DS_Game_Maker.DSGMlib.iGet(X.Substring(7), (byte)0, ","));
            }
            foreach (string X in DS_Game_Maker.DSGMlib.GetXDSFilter("OBJECT "))
            {
                if (!((DS_Game_Maker.DSGMlib.iGet(X, (byte)1, ",") ?? "") == (SpriteName ?? "")))
                    continue;
                string ObjectName = DS_Game_Maker.DSGMlib.iGet(X.Substring(7), (byte)0, ",");
                string ObjectFrame = DS_Game_Maker.DSGMlib.iGet(X.Substring(7), (byte)2, ",");
                DS_Game_Maker.DSGMlib.XDSChangeLine(X, "OBJECT " + ObjectName + "," + NewName + "," + ObjectFrame);
            }
            foreach (string X_ in Directory.GetFiles(DS_Game_Maker.SessionsLib.SessionPath + "Sprites"))
            {
                string X = X_;
                string Backup = X;

                X = X.Substring(X.LastIndexOf(@"\") + 1);
                X = X.Substring(0, X.LastIndexOf("."));
                X = X.Substring(X.IndexOf("_") + 1);

                if (X == SpriteName)
                    File.Delete(Backup);
            }
            for (short X = 0, loopTo = (short)(MainImageList.Images.Count - 1); X <= loopTo; X++)
                MainImageList.Images[X].Save(DS_Game_Maker.SessionsLib.SessionPath + @"Sprites\" + X.ToString() + "_" + NewName + ".png");
            foreach (Form X in Program.Forms.main_Form.MdiChildren)
            {
                if (X.Name == "DObject")
                {
                    DS_Game_Maker.DObject DForm = (DS_Game_Maker.DObject)X;
                    // MsgError(DForm.SpriteDropper.Text)
                    DForm.ChangeSprite(SpriteName, NewName);
                }
                else if (X.Name == "Room")
                {
                    DS_Game_Maker.Room DForm = (DS_Game_Maker.Room)X;
                    byte TopAffected = 0;
                    byte BottomAffected = 0;
                    for (byte DOn = 0, loopTo1 = (byte)(DForm.Objects.Count() - 1); DOn <= loopTo1; DOn++)
                    {
                        if (AffectedObjects.Contains(DForm.Objects[(int)DOn].ObjectName))
                        {
                            if (DForm.Objects[(int)DOn].Screen)
                                TopAffected = (byte)(TopAffected + 1);
                            else
                                BottomAffected = (byte)(BottomAffected + 1);
                            DForm.Objects[(int)DOn].CacheImage = DS_Game_Maker.DSGMlib.ObjectGetImage(DForm.Objects[(int)DOn].ObjectName);
                        }
                    }
                    if (TopAffected > 0)
                        DForm.RefreshRoom(true);
                    if (BottomAffected > 0)
                        DForm.RefreshRoom(false);
                }
            }
            DS_Game_Maker.DSGMlib.UpdateArrayActionsName("Sprite", SpriteName, NewName, false);
            DS_Game_Maker.DSGMlib.CurrentXDS = DS_Game_Maker.DSGMlib.UpdateActionsName(DS_Game_Maker.DSGMlib.CurrentXDS, "Sprite", SpriteName, NewName, false);
            foreach (TreeNode X in Program.Forms.main_Form.ResourcesTreeView.Nodes[(int)DS_Game_Maker.DSGMlib.ResourceIDs.Sprite].Nodes)
            {
                if ((X.Text ?? "") == (SpriteName ?? ""))
                    X.Text = NewName;
            }
            DS_Game_Maker.DSGMlib.RedoSprites = DataChanged;
            Close();
        }

        public Bitmap GetMagentaedBMP(string Path)
        {
            Bitmap TMP = (Bitmap)DS_Game_Maker.DSGMlib.PathToImage(Path);
            for (byte x = 0, loopTo = (byte)(TMP.Width - 1); x <= loopTo; x++)
            {
                for (byte y = 0, loopTo1 = (byte)(TMP.Height - 1); y <= loopTo1; y++)
                {
                    if (TMP.GetPixel(x, y).A == 0)
                    {
                        TMP.SetPixel(x, y, Color.Magenta);
                    }
                }
            }
            var Drawer = new Bitmap(TMP.Width, TMP.Height);
            var DrawerGFX = Graphics.FromImage(Drawer);
            DrawerGFX.Clear(Color.Black);
            DrawerGFX.DrawImage(TMP, 0, 0);
            DrawerGFX.Dispose();
            return Drawer;
        }

        private void FromFileButton_Click(object sender, EventArgs e)
        {
            MainImageList.Images.Clear();
            MainListView.Items.Clear();
            string Result = DS_Game_Maker.DSGMlib.OpenFile(string.Empty, DS_Game_Maker.DSGMlib.ImageFilter);
            if (Result.Length == 0)
                return;
            Bitmap TMP = (Bitmap)DS_Game_Maker.DSGMlib.PathToImage(Result);
            if (!IsSpriteSizeOkay(TMP.Size))
            {
                Program.Forms.badSpriteSize_Form.ShowDialog();
                return;
            }
            MainImageList.ImageSize = TMP.Size;
            TMP.Dispose();
            MainImageList.Images.Add("Frame_0", GetMagentaedBMP(Result));
            MainListView.Items.Add("Frame 0", 0);
        }
    }
}