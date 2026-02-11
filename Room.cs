using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace DS_Game_Maker
{
    public partial class Room
    {

        public string RoomName;

        public struct AnObject
        {
            public bool InUse;
            public bool Screen;
            public string ObjectName;
            public Bitmap CacheImage;
            public short X;
            public short Y;
        }

        private short TopWidth;
        private short TopHeight;
        private bool TopScroll;
        public string TopBG = string.Empty;

        private short BottomWidth;
        private short BottomHeight;
        private bool BottomScroll;
        public string BottomBG = string.Empty;

        public AnObject[] Objects = new AnObject[129];
        private List<byte> SlotAppliesTo = new List<byte>();
        private List<byte> VisualAppliesTo = new List<byte>();

        private byte InstanceOn = 0;
        public string ObjectToPlot = string.Empty;

        public Room()
        {
            InitializeComponent();
        }

        public void ClearObjects()
        {
            foreach (AnObject X_ in Objects)
            {
                AnObject X = X_;
                X.InUse = false;
                X.X = 0;
                X.Y = 0;
                X.Screen = true;
            }
        }

        private byte SnapX = 16;
        private byte SnapY = 16;
        private bool ShowGrid = false;
        private bool SnapToGrid = false;
        private Color GridColor = Color.Black;
        private short TX = 0;
        private short TY = 0;
        private bool TS = false;

        public Bitmap GetBGImage(string BackgroundName)
        {
            return (Bitmap)DS_Game_Maker.DSGMlib.MakeBMPTransparent(DS_Game_Maker.DSGMlib.PathToImage(DS_Game_Maker.SessionsLib.SessionPath + @"Backgrounds\" + BackgroundName + ".png"), Color.Magenta);
        }

        public Bitmap MakeRoomImage(bool WhichScreen)
        {
            Bitmap Returnable;
            Graphics ReturnableGFX;
            if (WhichScreen)
            {
                Returnable = new Bitmap(TopWidth, TopHeight);
            }
            else
            {
                Returnable = new Bitmap(BottomWidth, BottomHeight);
            }
            ReturnableGFX = Graphics.FromImage(Returnable);
            ReturnableGFX.Clear(Color.Black);
            Size BGSize;
            if (WhichScreen & TopBG.Length > 0)
            {
                BGSize = GetBGImage(TopBG).Size;
                if (BGSize.Height == 192)
                    BGSize.Height = 256;
                // If BGSize.Width > 256 Then

                // NewWidth = 256
                // Else
                // 'Dim Remainder As Int16 = BGSize.Width Mod 256
                // End If
                short XRepeat = (short)Math.Round((TopWidth - TopWidth % 256) / 256d);
                short YRepeat = (short)Math.Round((TopHeight - TopHeight % 256) / 256d);
                for (byte X = 0, loopTo = (byte)XRepeat; X <= loopTo; X++)
                {
                    for (byte Y = 0, loopTo1 = (byte)YRepeat; Y <= loopTo1; Y++)
                        ReturnableGFX.DrawImage(GetBGImage(TopBG), new Point(X * 256, Y * 256));
                }
            }
            else if (!WhichScreen & BottomBG.Length > 0)
            {
                BGSize = GetBGImage(BottomBG).Size;
                if (BGSize.Height == 192)
                    BGSize.Height = 256;
                short XRepeat = (short)Math.Round((BottomWidth - BottomWidth % 256) / 256d);
                short YRepeat = (short)Math.Round((BottomHeight - BottomHeight % 256) / 256d);
                for (byte X = 0, loopTo2 = (byte)XRepeat; X <= loopTo2; X++)
                {
                    for (byte Y = 0, loopTo3 = (byte)YRepeat; Y <= loopTo3; Y++)
                        ReturnableGFX.DrawImage(GetBGImage(BottomBG), new Point(X * 256, Y * 256));
                }
            }
            // If WhichScreen And TopBG.Length > 0 Then

            // ReturnableGFX.DrawImage(GetBGImage(TopBG), New Point(0, 0))
            // ElseIf Not WhichScreen And BottomBG.Length > 0 Then
            // ReturnableGFX.DrawImage(GetBGImage(BottomBG), New Point(0, 0))
            // End If
            foreach (AnObject X in Objects)
            {
                if (!X.InUse)
                    continue;
                if (!(X.Screen == WhichScreen))
                    continue;
                ReturnableGFX.DrawImageUnscaled(X.CacheImage, new Point(X.X, X.Y));
            }
            var Screen = WhichScreen ? TopScreen : BottomScreen;
            if (ShowGrid)
            {
                for (short i = 0, loopTo4 = WhichScreen ? TopWidth : BottomWidth; i <= loopTo4; i++)
                {
                    if (!(i % SnapX == 0))
                        continue;
                    ReturnableGFX.DrawLine(new Pen(GridColor), i, 0, i, WhichScreen ? TopHeight : BottomHeight);
                    // ReturnableGFX.DrawLine(New Pen(GridColor), Screen.AutoScrollPosition.X + i, Screen.AutoScrollPosition.Y, Screen.AutoScrollPosition.X + i, If(WhichScreen, TopHeight, BottomHeight))
                }
                for (short i = 0, loopTo5 = WhichScreen ? TopHeight : BottomHeight; i <= loopTo5; i++)
                {
                    if (!(i % SnapY == 0))
                        continue;
                    ReturnableGFX.DrawLine(new Pen(GridColor), 0, i, WhichScreen ? TopWidth : BottomWidth, i);
                    // ReturnableGFX.DrawLine(New Pen(GridColor), Screen.AutoScrollPosition.X, Screen.AutoScrollPosition.Y + i, If(WhichScreen, TopWidth, BottomWidth), Screen.AutoScrollPosition.Y + i)
                }
            }
            ReturnableGFX.DrawRectangle(new Pen(Color.FromArgb(127, 255, 255, 255), 1f), new Rectangle(0, 0, 255, 191));
            // ReturnableGFX.DrawImage(TheBitmap, New Point(0, 0))
            // OriginalBitmapGFX.Dispose()
            return Returnable;
        }

        public void AddObjectToDropper(string ObjectName)
        {
            ObjectDropper.Items.Add(ObjectName);
        }

        public void AddBackgroundToDropper(string BackgroundName)
        {
            TopScreenBGDropper.Items.Add(BackgroundName);
            BottomScreenBGDropper.Items.Add(BackgroundName);
        }

        public void RemoveObjectFromDropper(string ObjectName)
        {
            short TheIndex = 0;
            short DOn = 0;
            foreach (string X in ObjectDropper.Items)
            {
                if ((X ?? "") == (ObjectName ?? ""))
                {
                    TheIndex = DOn;
                    break;
                }
                DOn = (short)(DOn + 1);
            }
            ObjectDropper.Items.RemoveAt(TheIndex);
        }

        public void RemoveBackground(string BackgroundName)
        {
            short TheIndex = 0;
            short DOn = 0;
            foreach (string X in TopScreenBGDropper.Items)
            {
                if ((X ?? "") == (BackgroundName ?? ""))
                {
                    TheIndex = DOn;
                    break;
                }
                DOn = (short)(DOn + 1);
            }
            TopScreenBGDropper.Items.RemoveAt(TheIndex);
            BottomScreenBGDropper.Items.RemoveAt(TheIndex);
            if ((TopBG ?? "") == (BackgroundName ?? ""))
            {
                TopBG = string.Empty;
                TopScreenBGDropper.Text = string.Empty;
                RefreshRoom(true);
            }
            if ((BottomBG ?? "") == (BackgroundName ?? ""))
            {
                BottomBG = string.Empty;
                BottomScreenBGDropper.Text = string.Empty;
                RefreshRoom(false);
            }
        }

        public void RenameBackground(string OldName, string NewName)
        {
            short TheIndex = 0;
            short DOn = 0;
            foreach (string X in TopScreenBGDropper.Items)
            {
                if ((X ?? "") == (OldName ?? ""))
                {
                    TheIndex = DOn;
                    break;
                }
                DOn = (short)(DOn + 1);
            }
            TopScreenBGDropper.Items[TheIndex] = NewName;
            BottomScreenBGDropper.Items[TheIndex] = NewName;
            if ((TopBG ?? "") == (OldName ?? ""))
            {
                TopBG = NewName;
                TopScreenBGDropper.Text = NewName;
            }
            if ((BottomBG ?? "") == (OldName ?? ""))
            {
                BottomBG = NewName;
                BottomScreenBGDropper.Text = NewName;
            }
        }

        public void RenameObjectDropper(string OldName, string NewName)
        {
            for (short X = 0, loopTo = (short)(ObjectDropper.Items.Count - 1); X <= loopTo; X++)
            {
                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ObjectDropper.Items[X], OldName, false)))
                {
                    ObjectDropper.Items[X] = NewName;
                    break;
                }
            }
        }

        public void RenameBackgroundDropper(string BackgroundName)
        {
            for (short X = 0, loopTo = (short)(TopScreenBGDropper.Items.Count - 1); X <= loopTo; X++)
            {
                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(TopScreenBGDropper.Items[X], BackgroundName, false)))
                {
                    TopScreenBGDropper.Items[X] = BackgroundName;
                    BottomScreenBGDropper.Items[X] = BackgroundName;
                    break;
                }
            }
        }

        public void RefreshRoom(bool WhichScreen)
        {
            if (WhichScreen)
            {
                TopScreen.Invalidate();
            }
            else
            {
                BottomScreen.Invalidate();
            }
        }

        private void Room_Load(object sender, EventArgs e)
        {
            SnapToGrid = DS_Game_Maker.SettingsLib.GetSetting("SNAP_OBJECTS") == "1";
            ShowGrid = DS_Game_Maker.SettingsLib.GetSetting("SHOW_GRID") == "1";

            UseRightClickMenuChecker.Checked = DS_Game_Maker.SettingsLib.GetSetting("RIGHT_CLICK") == "1";

            SnapX = Convert.ToByte(DS_Game_Maker.SettingsLib.GetSetting("SNAP_X"));
            SnapY = Convert.ToByte(DS_Game_Maker.SettingsLib.GetSetting("SNAP_Y"));

            string ColorString = DS_Game_Maker.SettingsLib.GetSetting("GRID_COLOR");
            byte R = Convert.ToByte(DS_Game_Maker.DSGMlib.iGet(ColorString, (byte)0, ","));
            byte G = Convert.ToByte(DS_Game_Maker.DSGMlib.iGet(ColorString, (byte)1, ","));
            byte B = Convert.ToByte(DS_Game_Maker.DSGMlib.iGet(ColorString, (byte)2, ","));

            ShowGridChecker.Checked = ShowGrid;
            SnapToGridChecker.Checked = SnapToGrid;

            SnapXTextBox.Text = SnapX.ToString();
            SnapYTextBox.Text = SnapY.ToString();

            GridColor = Color.FromArgb(R, G, B);

            ObjectRightClickMenu.Renderer = new DS_Game_Maker.clsMenuRenderer();
            string XDSLine = DS_Game_Maker.DSGMlib.GetXDSLine("ROOM " + RoomName + ",");
            XDSLine = XDSLine.Substring(6 + RoomName.Length);
            TopWidth = Convert.ToInt16(DS_Game_Maker.DSGMlib.iGet(XDSLine, (byte)0, ","));
            TopHeight = Convert.ToInt16(DS_Game_Maker.DSGMlib.iGet(XDSLine, (byte)1, ","));
            BottomWidth = Convert.ToInt16(DS_Game_Maker.DSGMlib.iGet(XDSLine, (byte)4, ","));
            // MsgError("Extracted BottomWidth is " + BottomWidth.ToString)
            BottomHeight = Convert.ToInt16(DS_Game_Maker.DSGMlib.iGet(XDSLine, (byte)5, ","));
            short MidWidth = TopWidth;
            if (BottomWidth > MidWidth)
                MidWidth = BottomWidth;
            if (MidWidth > 700)
                MidWidth = 700;
            short BackupWidth = (short)Width;
            short BackupHeight = (short)Height;
            // XXX
            // Dim DoTopScroll As Boolean = If(TopWidth > TopScreen.Width, True, False)
            // Dim DoBottomScroll As Boolean = If(BottomWidth > BottomScreen.Width, True, False)
            // If DoTopScroll Then Me.Height += 36
            // If DoBottomScroll Then Me.Height += 36
            MainToolStrip.Renderer = new DS_Game_Maker.clsToolstripRenderer();
            NameTextBox.Text = RoomName;
            Text = RoomName;
            TopScreenBGDropper.Items.Clear();
            TopScreenBGDropper.Items.Add(string.Empty);
            BottomScreenBGDropper.Items.Clear();
            BottomScreenBGDropper.Items.Add(string.Empty);
            foreach (string X_ in DS_Game_Maker.DSGMlib.GetXDSFilter("BACKGROUND "))
            {
                string X = X_;
                X = X.Substring(11);
                TopScreenBGDropper.Items.Add(DS_Game_Maker.DSGMlib.iGet(X, (byte)0, ","));
                BottomScreenBGDropper.Items.Add(DS_Game_Maker.DSGMlib.iGet(X, (byte)0, ","));
            }
            ObjectDropper.Items.Clear();
            foreach (string X_ in DS_Game_Maker.DSGMlib.GetXDSFilter("OBJECT "))
            {
                string X = X_;
                X = X.Substring(7);
                ObjectDropper.Items.Add(DS_Game_Maker.DSGMlib.iGet(X, (byte)0, ","));
            }
            TopScroll = DS_Game_Maker.DSGMlib.iGet(XDSLine, (byte)2, ",") == "1";
            TopBG = DS_Game_Maker.DSGMlib.iGet(XDSLine, (byte)3, ",");
            BottomWidthDropper.Value = BottomWidth;
            // MsgError(BottomHeight.ToString)
            BottomHeightDropper.Value = BottomHeight;
            TopWidthDropper.Value = TopWidth;
            TopHeightDropper.Value = TopHeight;
            TopScreenScrollChecker.Checked = TopScroll;
            BottomScroll = DS_Game_Maker.DSGMlib.iGet(XDSLine, (byte)6, ",") == "1";
            BottomBG = DS_Game_Maker.DSGMlib.iGet(XDSLine, (byte)7, ",");
            Width = MidWidth + 194;
            Height = (int)Math.Round((TopHeight + BottomHeight) / 2d + 259d);
            TopScreen.Width = 256 + (Width - BackupWidth);
            BottomScreen.Width = 256 + (Width - BackupWidth);
            BottomScreenScrollChecker.Checked = BottomScroll;
            TopScreenBGDropper.Text = TopBG;
            BottomScreenBGDropper.Text = BottomBG;
            ClearObjects();
            foreach (string TheLine_ in DS_Game_Maker.DSGMlib.GetXDSFilter("OBJECTPLOT "))
            {
                string TheLine = TheLine_;
                TheLine = TheLine.Substring(11);
                if (!((DS_Game_Maker.DSGMlib.iGet(TheLine, (byte)1, ",") ?? "") == (RoomName ?? "")))
                    continue;
                string ObjectName = DS_Game_Maker.DSGMlib.iGet(TheLine, (byte)0, ",");
                bool Screen = DS_Game_Maker.DSGMlib.iGet(TheLine, (byte)2, ",") == "1";
                short X = Convert.ToInt16(DS_Game_Maker.DSGMlib.iGet(TheLine, (byte)3, ","));
                short Y = Convert.ToInt16(DS_Game_Maker.DSGMlib.iGet(TheLine, (byte)4, ","));
                PlotObject(ObjectName, Screen, X, Y);
            }
            RefreshRoom(true);
            RefreshRoom(false);
            ObjectToPlot = string.Empty;
        }

        private void DAcceptButton_Click(object sender, EventArgs e)
        {
            string NewName = NameTextBox.Text;
            if (!((NewName ?? "") == (RoomName ?? "")))
            {
                if (DS_Game_Maker.DSGMlib.GUIResNameChecker(NewName))
                    return;
            }
            string OldLine = DS_Game_Maker.DSGMlib.GetXDSLine("ROOM " + RoomName + ",");
            string NewLine = "ROOM " + NewName + ",";
            NewLine += TopWidthDropper.Value.ToString() + "," + TopHeightDropper.Value.ToString() + "," + (TopScreenScrollChecker.Checked ? "1" : "0") + "," + TopBG + ",";
            NewLine += BottomWidthDropper.Value.ToString() + "," + BottomHeightDropper.Value.ToString() + "," + (BottomScreenScrollChecker.Checked ? "1" : "0") + "," + BottomBG;
            DS_Game_Maker.DSGMlib.XDSChangeLine(OldLine, NewLine);
            DS_Game_Maker.DSGMlib.UpdateArrayActionsName("Room", RoomName, NewName, false);
            DS_Game_Maker.DSGMlib.CurrentXDS = DS_Game_Maker.DSGMlib.UpdateActionsName(DS_Game_Maker.DSGMlib.CurrentXDS, "Room", RoomName, NewName, false);
            foreach (string X in DS_Game_Maker.DSGMlib.GetXDSFilter("OBJECTPLOT "))
            {
                if ((DS_Game_Maker.DSGMlib.iGet(X, (byte)1, ",") ?? "") == (RoomName ?? ""))
                    DS_Game_Maker.DSGMlib.XDSRemoveLine(X);
            }
            foreach (AnObject X in Objects)
            {
                if (!X.InUse)
                    continue;
                string TheLine = "OBJECTPLOT " + X.ObjectName + "," + NewName + ",";
                if (X.Screen)
                    TheLine += "1";
                else
                    TheLine += "0";
                TheLine += "," + X.X.ToString() + "," + X.Y.ToString();
                DS_Game_Maker.DSGMlib.XDSAddLine(TheLine);
            }
            if (!((NewName ?? "") == (RoomName ?? "")))
            {
                if ((DS_Game_Maker.DSGMlib.GetXDSLine("BOOTROOM ").Substring(9) ?? "") == (RoomName ?? ""))
                {
                    DS_Game_Maker.DSGMlib.XDSChangeLine("BOOTROOM " + RoomName, "BOOTROOM " + NewName);
                }
            }
            foreach (TreeNode X in Program.Forms.main_Form.ResourcesTreeView.Nodes[(int)DS_Game_Maker.DSGMlib.ResourceIDs.Room].Nodes)
            {
                if ((X.Text ?? "") == (RoomName ?? ""))
                    X.Text = NewName;
            }
            DS_Game_Maker.SettingsLib.SetSetting("SNAP_OBJECTS", SnapToGrid ? "1" : "0");
            DS_Game_Maker.SettingsLib.SetSetting("SHOW_GRID", ShowGrid ? "1" : "0");
            DS_Game_Maker.SettingsLib.SetSetting("RIGHT_CLICK", UseRightClickMenuChecker.Checked ? "1" : "0");
            Close();
        }

        public void PlotObject(string ObjectName, bool Screen, short X, short Y)
        {
            var ToAdd = new AnObject();
            ToAdd.InUse = true;
            ToAdd.X = X;
            ToAdd.Y = Y;
            ToAdd.Screen = Screen;
            ToAdd.ObjectName = ObjectName;
            ToAdd.CacheImage = DS_Game_Maker.DSGMlib.ObjectGetImage(ObjectName);
            Objects[InstanceOn] = ToAdd;
            InstanceOn = (byte)(InstanceOn + 1);
            RefreshRoom(Screen);
        }

        public void ScreenBGDropper_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Name.StartsWith("Top"))
            {
                TopBG = TopScreenBGDropper.Text;
                RefreshRoom(true);
            }
            else
            {
                BottomBG = BottomScreenBGDropper.Text;
                RefreshRoom(false);
            }
        }

        private void TopScreen_MouseClick(object sender, MouseEventArgs e)
        {
            byte PlottedCount = 0;
            foreach (AnObject Z in Objects)
            {
                if (Z.InUse)
                    PlottedCount = (byte)(PlottedCount + 1);
            }
            if (PlottedCount >= 128)
            {
                DS_Game_Maker.DSGMlib.MsgError("You can only plot 128 instances.");
                return;
            }
            short X = (short)e.Location.X;
            short Y = (short)e.Location.Y;
            X = (short)(X + (0 - TopScreen.AutoScrollPosition.X));
            Y = (short)(Y + (0 - TopScreen.AutoScrollPosition.Y));
            if (e.Button == MouseButtons.Right)
            {
                if (UseRightClickMenuChecker.Checked)
                {
                    TX = X;
                    TY = Y;
                    TS = true;
                    ObjectRightClickMenu.Show(TopScreen, e.Location);
                }
                else
                {
                    DeleteShizzle(X, Y, true);
                }
                return;
            }
            if (ObjectToPlot.Length == 0)
                return;
            if (SnapToGridChecker.Checked)
            {
                short NewX = default, NewY = default;
                for (short i = 1, loopTo = TopWidth; i <= loopTo; i++)
                {
                    if ((bool)DSGMlib.CanDivide(i, SnapX) == false)
                        continue;

                    if (X >= i)
                        NewX = i;
                }
                for (short i = 1, loopTo1 = TopHeight; i <= loopTo1; i++)
                {
                    if ((bool)DSGMlib.CanDivide(i, SnapY) == false)
                        continue;
                    if (Y >= i)
                        NewY = i;
                }
                PlotObject(ObjectToPlot, true, NewX, NewY);
            }
            else
            {
                PlotObject(ObjectToPlot, true, X, Y);
            }
        }

        public void DeleteShizzle(short X, short Y, bool WhichScreen)
        {
            var AppliesTo = new List<byte>();
            byte DOn = 0;
            foreach (AnObject Z in Objects)
            {
                if (Z.InUse)
                {
                    // MsgError("(" + DOn.ToString + ") name: " + Z.ObjectName + " at pos. " + Z.X.ToString + ", " + Z.Y.ToString)
                    if (Z.Screen == WhichScreen & X >= Z.X & Y >= Z.Y & X < Z.X + Z.CacheImage.Width & Y < Z.Y + Z.CacheImage.Height)
                    {
                        AppliesTo.Add(DOn);
                    }
                }
                DOn = (byte)(DOn + 1);
            }
            foreach (byte Z in AppliesTo)
            {
                Objects[Z].CacheImage = null;
                Objects[Z].InUse = false;
                Objects[Z].ObjectName = string.Empty;
                Objects[Z].X = 0;
                Objects[Z].Y = 0;
            }
            if (AppliesTo.Count > 0)
            {
                RefreshRoom(WhichScreen);
            }
        }

        private void BottomScreen_MouseClick(object sender, MouseEventArgs e)
        {
            short X = (short)e.Location.X;
            short Y = (short)e.Location.Y;
            X = (short)(X + (0 - BottomScreen.AutoScrollPosition.X));
            Y = (short)(Y + (0 - BottomScreen.AutoScrollPosition.Y));
            if (e.Button == MouseButtons.Right)
            {
                if (UseRightClickMenuChecker.Checked)
                {
                    TX = X;
                    TY = Y;
                    TS = false;
                    ObjectRightClickMenu.Show(BottomScreen, e.Location);
                }
                else
                {
                    DeleteShizzle(X, Y, false);
                }
                return;
            }
            if (ObjectToPlot.Length == 0)
                return;
            if (SnapToGridChecker.Checked)
            {
                short NewX = default, NewY = default;
                for (short i = 1, loopTo = BottomWidth; i <= loopTo; i++)
                {
                    if ((bool)DSGMlib.CanDivide(i, (short)SnapX) == false)
                        continue;
                    if (X >= i)
                        NewX = i;
                }
                for (short i = 1, loopTo1 = BottomHeight; i <= loopTo1; i++)
                {
                    if ((bool)DSGMlib.CanDivide(i, SnapY) == false)
                        continue;
                    if (Y >= i)
                        NewY = i;
                }
                PlotObject(ObjectToPlot, false, NewX, NewY);
            }
            else
            {
                PlotObject(ObjectToPlot, false, X, Y);
            }
        }

        private void ObjectDropper_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObjectToPlot = ((ComboBox)sender).Text;
        }

        private void Room_Resize(object sender, EventArgs e)
        {
            short TotalHeight = (short)(ClientRectangle.Height - MainToolStrip.Height);
            TopScreen.Height = (int)Math.Round(TotalHeight / 2d - 2d);
            BottomScreen.Location = new Point(BottomScreen.Location.X, (int)Math.Round(27d + TotalHeight / 2d));
            // If TotalHeight Mod 2 = 0 Then
            // BottomScreen.Location = New Point(BottomScreen.Location.X, (TotalHeight / 2) + 2)
            // Else
            // BottomScreen.Location = New Point(BottomScreen.Location.X, (TotalHeight / 2) + 1)
            // End If
            BottomScreen.Height = (int)Math.Round(TotalHeight / 2d - 1d);
            TopScreen.Width = 256 + (Width - 450);
            BottomScreen.Width = 256 + (Width - 450);
        }

        private void TopScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(MakeRoomImage(true), new Point(TopScreen.AutoScrollPosition.X, TopScreen.AutoScrollPosition.Y));
        }

        private void BottomScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(MakeRoomImage(false), new Point(BottomScreen.AutoScrollPosition.X, BottomScreen.AutoScrollPosition.Y));
        }

        private void TopWidthDropper_ValueChanged(object sender, EventArgs e)
        {
            Size = new Size((int)Math.Round(450m + TopWidthDropper.Value - 256m), (int)Math.Round(452m + TopHeightDropper.Value - 192m));
            TopScreen.AutoScrollMinSize = new Size((int)Math.Round(TopWidthDropper.Value), TopHeight);
            TopWidth = (short)Math.Round(TopWidthDropper.Value);
            TopScreen_Scroll();
        }

        private void TopHeightDropper_ValueChanged(object sender, EventArgs e)
        {
            Size = new Size((int)Math.Round(450m + TopWidthDropper.Value - 256m), (int)Math.Round(452m + TopHeightDropper.Value - 192m));
            TopScreen.AutoScrollMinSize = new Size(TopWidth, (int)Math.Round(TopHeightDropper.Value));
            TopHeight = (short)Math.Round(TopHeightDropper.Value);
            TopScreen_Scroll();
        }

        private void BottomWidthDropper_ValueChanged(object sender, EventArgs e)
        {
            Size = new Size((int)Math.Round(450m + BottomWidthDropper.Value - 256m), (int)Math.Round(452m + BottomHeightDropper.Value - 192m));
            BottomScreen.AutoScrollMinSize = new Size((int)Math.Round(BottomWidthDropper.Value), BottomHeight);
            BottomWidth = (short)Math.Round(BottomWidthDropper.Value);
            BottomScreen_Scroll();
        }

        private void BottomHeightDropper_ValueChanged(object sender, EventArgs e)
        {
            Size = new Size((int)Math.Round(450m + BottomWidthDropper.Value - 256m), (int)Math.Round(452m + BottomHeightDropper.Value - 192m));
            BottomScreen.AutoScrollMinSize = new Size(BottomWidth, (int)Math.Round(BottomHeightDropper.Value));
            BottomHeight = (short)Math.Round(BottomHeightDropper.Value);
            BottomScreen_Scroll();
        }

        private void TopScreen_Scroll()
        {
            TopScreen.Invalidate();
        }

        private void BottomScreen_Scroll()
        {
            BottomScreen.Invalidate();
        }

        private void Snappers_TextChanged(object sender, EventArgs e)
        {
            string TheValue = ((TextBox)sender).Text;
            if (!DS_Game_Maker.DSGMlib.IsNumeric(TheValue, System.Globalization.NumberStyles.Integer))
                return;
            if (Convert.ToInt16(TheValue) == 0 | Convert.ToInt16(TheValue) > 255)
                return;
            if (((dynamic)sender).name.ToString().Contains("X"))
            {
                SnapX = Convert.ToByte(TheValue);
                DS_Game_Maker.SettingsLib.SetSetting("SNAP_X", TheValue);
            }
            else
            {
                SnapY = Convert.ToByte(TheValue);
                DS_Game_Maker.SettingsLib.SetSetting("SNAP_Y", TheValue);
            }
            if (ShowGrid)
            {
                TopScreen.Refresh();
                BottomScreen.Refresh();
            }
        }

        private void SnapToGridChecker_CheckedChanged(object sender, EventArgs e)
        {
            SnapToGrid = SnapToGridChecker.Checked;
        }

        private void ShowGridChecker_CheckedChanged(object sender, EventArgs e)
        {
            ShowGrid = ShowGridChecker.Checked;
            TopScreen.Refresh();
            BottomScreen.Refresh();
        }

        private void GridColorButton_Click(object sender, EventArgs e)
        {
            // Dim DOn As Byte = 0
            // For Each Z As AnObject In Objects
            // If Z.InUse Then
            // MsgError("(" + DOn.ToString + ") name: " + Z.ObjectName + " at pos. " + Z.X.ToString + ", " + Z.Y.ToString)
            // DOn += 1
            // End If
            // Next
            GridColor = DS_Game_Maker.DSGMlib.SelectColor(GridColor);
            if (ShowGrid)
            {
                TopScreen.Refresh();
                BottomScreen.Refresh();
            }
            DS_Game_Maker.SettingsLib.SetSetting("GRID_COLOR", GridColor.R.ToString() + "," + GridColor.G.ToString() + "," + GridColor.B.ToString());
        }

        private void Screens_MouseMove(object sender, MouseEventArgs e)
        {
            bool WhichScreen = ((dynamic)sender).name.ToString().StartsWith("T") ? true : false;
            short X = (short)e.Location.X;
            short Y = (short)e.Location.Y;
            X = (short)(X + (0 - ((Panel)sender).AutoScrollPosition.X));
            Y = (short)(Y + (0 - ((Panel)sender).AutoScrollPosition.Y));
            short NewX = default, NewY = default;
            for (short i = 1, loopTo = BottomWidth; i <= loopTo; i++)
            {
                if ((bool)DSGMlib.CanDivide(i, SnapX) == false)
                    continue;
                if (X >= i)
                    NewX = i;
            }
            for (short i = 1, loopTo1 = BottomHeight; i <= loopTo1; i++)
            {
                if ((bool)DSGMlib.CanDivide(i, SnapY) == false)
                    continue;
                if (Y >= i)
                    NewY = i;
            }
            SlotAppliesTo.Clear();
            VisualAppliesTo.Clear();
            byte DOn = 0;
            byte AOn = 0;
            foreach (AnObject Z in Objects)
            {
                if (Z.InUse)
                {
                    // If UseRightClickMenuChecker.Checked = True Then MsgError("(" + DOn.ToString + ") name: " + Z.ObjectName + " at pos. " + Z.X.ToString + ", " + Z.Y.ToString)
                    if (Z.Screen == WhichScreen & X >= Z.X & Y >= Z.Y & X < Z.X + Z.CacheImage.Width & Y < Z.Y + Z.CacheImage.Height)
                    {
                        VisualAppliesTo.Add(AOn);
                        SlotAppliesTo.Add(DOn);
                    }
                    AOn = (byte)(AOn + 1);
                }
                DOn = (byte)(DOn + 1);
            }
            string FinalString = string.Empty;
            DOn = 0;
            foreach (byte Z in SlotAppliesTo)
            {
                FinalString += "[" + VisualAppliesTo[DOn].ToString() + "] " + Objects[Z].ObjectName + " at " + Objects[Z].X.ToString() + ", " + Objects[Z].Y.ToString() + Constants.vbCrLf;
                DOn = (byte)(DOn + 1);
            }
            ObjectInfoLabel.Text = FinalString;
            CursorPositionLabel.Text = "Cursor Position: " + X.ToString() + ", " + Y.ToString();
            CursorPositionSnapLabel.Text = "Cursor Position (snap): " + NewX.ToString() + ", " + NewY.ToString();
        }

        private void ObjectRightClickMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            OpenObjectButton.Text = "Open Object";
            DeleteObjectButton.Text = "Delete Instance";
            ObjectRightClickMenu.Enabled = false;
            SetCoOrdinatesButton.Enabled = false;
            DeleteObjectButton.Enabled = false;
            if (SlotAppliesTo.Count == 0)
                return;
            ObjectRightClickMenu.Enabled = true;
            DeleteObjectButton.Enabled = true;
            var ObjectsToOpen = new List<string>();
            foreach (byte X in SlotAppliesTo)
            {
                bool CanAdd = true;
                foreach (string Y in ObjectsToOpen)
                {
                    if ((Y ?? "") == (Objects[X].ObjectName ?? ""))
                        CanAdd = false;
                }
                if (CanAdd)
                    ObjectsToOpen.Add(Objects[X].ObjectName);
            }
            if (ObjectsToOpen.Count > 1)
            {
                OpenObjectButton.Text += "s";
            }
            if (SlotAppliesTo.Count > 1)
            {
                DeleteObjectButton.Text += "s";
            }
            else
            {
                SetCoOrdinatesButton.Enabled = true;
            }
        }

        private void DeleteObjectButton_Click(object sender, EventArgs e)
        {
            DeleteShizzle(TX, TY, TS);
        }

        private void OpenObjectButton_Click(object sender, EventArgs e)
        {
            var ObjectsToOpen = new List<string>();
            foreach (byte X in SlotAppliesTo)
            {
                bool CanAdd = true;
                foreach (string Y in ObjectsToOpen)
                {
                    if ((Y ?? "") == (Objects[X].ObjectName ?? ""))
                        CanAdd = false;
                }
                if (CanAdd)
                    ObjectsToOpen.Add(Objects[X].ObjectName);
            }
            foreach (string X in ObjectsToOpen)
            {
                bool DoShow = true;
                foreach (Form TheForm in Program.Forms.main_Form.MdiChildren)
                {
                    if ((TheForm.Text ?? "") == (X ?? ""))
                    {
                        TheForm.Focus();
                        DoShow = false;
                    }
                }
                if (DoShow)
                {
                    var ObjectForm = new DS_Game_Maker.DObject();
                    ObjectForm.ObjectName = X;
                    object argInstance = (object)ObjectForm;
                    DS_Game_Maker.DSGMlib.ShowInternalForm(ref argInstance);
                    ObjectForm = (DS_Game_Maker.DObject)argInstance;
                }
            }
        }

        private void SetCoOrdinatesButton_Click(object sender, EventArgs e)
        {
            short ID = SlotAppliesTo[0];
            Program.Forms.setCoOrdinates_Form.X = Objects[ID].X;
            Program.Forms.setCoOrdinates_Form.Y = Objects[ID].Y;
            Program.Forms.setCoOrdinates_Form.ShowDialog();

            // If Not SetCoOrdinates.ShowDialog() = Windows.Forms.DialogResult.OK Then Exit Sub
            Objects[ID].X = Program.Forms.setCoOrdinates_Form.X;
            Objects[ID].Y = Program.Forms.setCoOrdinates_Form.Y;
            RefreshRoom(Objects[ID].Screen);
        }

        private void TopScreen_Scroll(object sender, ScrollEventArgs e) => TopScreen_Scroll();
        private void BottomScreen_Scroll(object sender, ScrollEventArgs e) => BottomScreen_Scroll();
    }
}