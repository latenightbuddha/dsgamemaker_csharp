using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace DS_Game_Maker
{

    public partial class DObject
    {

        public string ObjectName;
        private string SpriteName;
        private short Frame;
        private short FrameLimit;
        public string MyXDSLines;
        private List<string> DEventMainClasses = new List<string>();
        private List<string> DEventSubClasses = new List<string>();

        public List<string> Actions = new List<string>();
        public List<string> ActionImages = new List<string>();
        public List<string> ActionArguments = new List<string>();
        public List<string> ActionDisplays = new List<string>();
        public List<string> ActionAppliesTos = new List<string>();

        private short SelectedEvent = 0;
        private List<sbyte> CurrentIndents = new List<sbyte>();

        private bool DragFromBottom;
        private bool DragInternal;
        private short DraggingInternal;
        private Panel DraggingFromBottom;
        private bool MouseInBox = false;

        private bool SaveOnNextChange = false;
        private bool ThinList = true;

        public DObject()
        {
            InitializeComponent();
        }

        public void ActionMouseDown(object sender, MouseEventArgs e)
        {
            DragFromBottom = true;
            DraggingFromBottom = (Panel)sender;
            Cursor = Cursors.NoMove2D;
            // DirectCast(sender, Panel).DoDragDrop(DirectCast(sender, Panel).Tag, DragDropEffects.Move)
        }

        public void ActionMouseEnter(object sender, EventArgs e)
        {
            string ActionName = Conversions.ToString(((Panel)sender).Tag);
            ActionNameLabel.Text = ActionName;
            ArgumentsListLabel.Text = string.Empty;
            byte ArgumentCount = 0;
            foreach (string X_ in File.ReadAllLines(DS_Game_Maker.DSGMlib.AppPath + @"Actions\" + ActionName + ".action"))
            {
                string X = X_;
                if (X.StartsWith("ARG "))
                {
                    X = X.Substring(4);
                    string ArgumentName = DS_Game_Maker.DSGMlib.iGet(X, (byte)0, ",");
                    ArgumentsListLabel.Text += ArgumentName + Constants.vbCrLf;
                    ArgumentCount = (byte)(ArgumentCount + 1);
                }
            }
            if (ArgumentCount == 0)
                ArgumentsListLabel.Text = "<No Arguments>";
            bool RequiresPro = false;
            foreach (string X in DS_Game_Maker.DSGMlib.ProActions)
            {
                if ((X ?? "") == (ActionName ?? ""))
                    RequiresPro = true;
            }
            RequiresProBanner.Visible = RequiresPro;
            if (RequiresPro)
            {
                ArgumentsHeaderLabel.Height = 94;
                ArgumentsListLabel.Height = 68;
            }
            else
            {
                ArgumentsHeaderLabel.Height = 120;
                ArgumentsListLabel.Height = 94;
            }
        }

        public void ActionMouseUp(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Default;
            string ActionName = Conversions.ToString(DraggingFromBottom.Tag);
            if (SelectedEvent == 100)
            {
                DS_Game_Maker.DSGMlib.MsgWarn("You must add an Event, to which to add Actions.");
                return;
            }
            if (!DragFromBottom)
                return;
            // MsgError(Location.X.ToString)
            // MsgError(Location.Y.ToString)
            // Dim MDIID As Byte = 10
            // Dim DOn As Byte = 0
            // MsgError(ObjectName)
            // For Each X As Form In MainForm.MdiChildren
            // If X.Text = ObjectName Then MDIID = DOn
            // DOn += 1
            // Next
            // MsgError(MDIID.ToString)
            // If Not ActionsList.PointToClient(Cursor.Position).X > 0 Then Exit Sub
            // MsgError(Cursor.Position.X.ToString)
            // MsgError(Me.PointToScreen(ActionsList.Location).X.ToString)
            short TheX = (short)ActionsList.PointToClient(Cursor.Position).X; // - Location.X
            short TheY = (short)ActionsList.PointToClient(Cursor.Position).Y; // - Location.Y
            if (TheX < 0)
                return;
            if (TheX > ActionsList.Width)
                return;
            if (TheY < 0)
                return;
            if (TheY > ActionsList.Height)
                return;
            byte ArgCount = 0;
            bool NoAppliesTo = false;
            foreach (string X in File.ReadAllLines(DS_Game_Maker.DSGMlib.AppPath + @"Actions\" + ActionName + ".action"))
            {
                if (X.StartsWith("ARG "))
                    ArgCount = (byte)(ArgCount + 1);
                if (X == "NOAPPLIES")
                    NoAppliesTo = true;
            }
            bool Code = ActionName == "Execute Code";
            bool JustAdd = !(ActionsList.SelectedIndices.Count == 1);
            short Position = (short)(ActionsList.SelectedIndex + 1);
            if (NoAppliesTo & ArgCount == 0)
            {
                if (JustAdd)
                {
                    Actions.Add(ActionName);
                    ActionImages.Add(DS_Game_Maker.ActionsLib.ActionGetIconPath(ActionName, false));
                    ActionArguments.Add(string.Empty);
                    ActionDisplays.Add(DS_Game_Maker.ActionsLib.ActionEquateDisplay(ActionName, string.Empty));
                    ActionAppliesTos.Add("this");
                    GenerateIndentIndices();
                    ActionsList.Items.Add(string.Empty);
                    ActionsList.SelectedItems.Clear();
                    ActionsList.SelectedIndex = ActionsList.Items.Count - 1;
                }
                else
                {
                    Actions.Insert(Position, ActionName);
                    ActionImages.Insert((int)Position, DS_Game_Maker.ActionsLib.ActionGetIconPath(ActionName, false));
                    ActionArguments.Insert(Position, string.Empty);
                    ActionDisplays.Insert((int)Position, DS_Game_Maker.ActionsLib.ActionEquateDisplay(ActionName, string.Empty));
                    ActionAppliesTos.Insert(Position, "this");
                    GenerateIndentIndices();
                    ActionsList.Items.Insert(Position, string.Empty);
                    ActionsList.SelectedItems.Clear();
                    ActionsList.SelectedIndex = Position;
                }
                DragFromBottom = false;
                return;
            }
            if (!Code)
            {
                Program.Forms.action_Form.ActionName = ActionName;
                Program.Forms.action_Form.AppliesTo = "this";
                Program.Forms.action_Form.ArgumentString = string.Empty;
            }
            if (ArgCount > 1)
            {
                string MyShizzle = string.Empty;
                for (byte X = 0, loopTo = (byte)(ArgCount - 2); X <= loopTo; X++)
                    MyShizzle += ";";
                Program.Forms.action_Form.ArgumentString = MyShizzle;
            }
            if (Code)
            {
                Program.Forms.editCode_Form.ReturnableCode = string.Empty;
                Program.Forms.editCode_Form.ImportExport = true;
                if (!(Program.Forms.editCode_Form.ShowDialog() == DialogResult.OK))
                    return;
            }
            else
            {
                Program.Forms.action_Form.AppliesToGroupBox.Enabled = true;
                if (NoAppliesTo)
                {
                    Program.Forms.action_Form.AppliesToGroupBox.Enabled = false;
                }
                Program.Forms.action_Form.ShowDialog();
                if (!Program.Forms.action_Form.UseData)
                {
                    Cursor = Cursors.Default;
                    return;
                }
            }
            if (JustAdd)
            {
                Actions.Add(ActionName);
                ActionImages.Add(DS_Game_Maker.ActionsLib.ActionGetIconPath(ActionName, false));
            }
            else
            {
                Actions.Insert(Position, ActionName);
                ActionImages.Insert((int)Position, DS_Game_Maker.ActionsLib.ActionGetIconPath(ActionName, false));
            }
            if (Code)
            {
                if (JustAdd)
                {
                    ActionArguments.Add(Program.Forms.editCode_Form.ReturnableCode);
                    ActionDisplays.Add(DS_Game_Maker.ActionsLib.ActionEquateDisplay(ActionName, Program.Forms.editCode_Form.ReturnableCode));
                    ActionAppliesTos.Add("this");
                }
                else
                {
                    ActionArguments.Insert((int)Position, Program.Forms.editCode_Form.ReturnableCode);
                    ActionDisplays.Insert((int)Position, DS_Game_Maker.ActionsLib.ActionEquateDisplay(ActionName, Program.Forms.editCode_Form.ReturnableCode));
                    ActionAppliesTos.Insert(Position, "this");
                }
            }
            else if (JustAdd)
            {
                ActionArguments.Add(Program.Forms.action_Form.ArgumentString);
                ActionDisplays.Add(DS_Game_Maker.ActionsLib.ActionEquateDisplay(ActionName, Program.Forms.action_Form.ArgumentString));
                ActionAppliesTos.Add(Program.Forms.action_Form.AppliesTo);
            }
            else
            {
                ActionArguments.Insert((int)Position, Program.Forms.action_Form.ArgumentString);
                ActionDisplays.Insert((int)Position, DS_Game_Maker.ActionsLib.ActionEquateDisplay(ActionName, Program.Forms.action_Form.ArgumentString));
                ActionAppliesTos.Insert((int)Position, Program.Forms.action_Form.AppliesTo);
            }
            GenerateIndentIndices();
            ActionsList.SelectedItems.Clear();
            if (JustAdd)
            {
                ActionsList.Items.Add(string.Empty);
                ActionsList.SelectedIndex = ActionsList.Items.Count - 1;
            }
            else
            {
                ActionsList.Items.Insert(Position, string.Empty);
                ActionsList.SelectedIndex = Position;
            }

            // MsgError((ActionsList.PointToClient(Cursor.Position).X).ToString + " v.s. " + ActionsList.Location.X.ToString + " and " + ActionsList.Width.ToString)
            // MsgError(TheY)
            // If ActionsList.PointToClient(Cursor.Position).X > ActionsList.Width Then Exit Sub
            // If Not ActionsList.PointToClient(Cursor.Position).Y > 0 Then Exit Sub
            // If ActionsList.PointToClient(Cursor.Position).Y > ActionsList.Height Then Exit Sub
            DragFromBottom = false;
        }

        public void GenerateIndentIndices()
        {
            CurrentIndents.Clear();
            if (Actions.Count == 0)
                return;
            byte RunningIndent = 0;
            for (short X = 0, loopTo = (short)(Actions.Count - 1); X <= loopTo; X++)
            {
                string ActionName = Actions[X];
                byte IndentChange = 0;
                foreach (string Y in File.ReadAllLines(DS_Game_Maker.DSGMlib.AppPath + @"Actions\" + ActionName + ".action"))
                {
                    if (Y == "INDENT")
                    {
                        IndentChange = 2;
                        break;
                    }
                    if (Y == "DEDENT")
                    {
                        IndentChange = 1;
                        break;
                    }
                }
                if (IndentChange == 2)
                    RunningIndent = (byte)(RunningIndent + 1);
                CurrentIndents.Add((sbyte)RunningIndent);
                if (RunningIndent > 0)
                {
                    if (IndentChange == 1)
                        RunningIndent = (byte)(RunningIndent - 1);
                }
            }
            // Dim IndentChange As Byte = 0
            // Dim CurrentIndent As Byte = 1
            // Dim Thingy As Byte = 0
            // For X As Int16 = 0 To Actions.Count - 1
            // IndentChange = 0
            // Dim ActionName As String = Actions(X)
            // For Each Y As String In IO.File.ReadAllLines(AppPath + "Actions\" + ActionName + ".action")
            // If Y = "INDENT" Then IndentChange = 2 : Exit For
            // If Y = "DEDENT" Then IndentChange = 1 : Exit For
            // Next
            // If IndentChange = 1 Then
            // If CurrentIndent > 1 Then
            // For i = 0 To CurrentIndent - 2
            // Thingy += 1
            // Next
            // CurrentIndent -= 1
            // End If
            // Else
            // If CurrentIndent > 0 Then
            // For i = 0 To CurrentIndent - 1
            // Thingy += 1
            // Next
            // End If
            // End If
            // CurrentIndents.Add(Thingy)
            // If IndentChange = 2 Then CurrentIndent += 1
            // Next
        }

        public void RenderSprite()
        {
            var Final = new Bitmap(64, 64);
            var FinalGFX = Graphics.FromImage(Final);
            FinalGFX.Clear(Color.White);
            string ImagePath;
            if (SpriteDropper.Text == "None")
            {
                ImagePath = DS_Game_Maker.DSGMlib.AppPath + @"Resources\NoSprite.png";
            }
            else
            {
                ImagePath = DS_Game_Maker.SessionsLib.SessionPath + @"Sprites\" + Frame.ToString() + "_" + SpriteDropper.Text + ".png";
            }
            var Drawable = DS_Game_Maker.DSGMlib.PathToImage(ImagePath);
            Drawable = DS_Game_Maker.DSGMlib.MakeBMPTransparent(Drawable, Color.Magenta);
            FinalGFX.DrawImage(Drawable, new Point((int)Math.Round(32d - (double)Drawable.Width / 2d), (int)Math.Round(32d - (double)Drawable.Height / 2d)));
            SpritePanel.BackgroundImage = Final;
        }

        public void PopulateActionsTabControl(ref TabControl RAppliesTo)
        {
            bool Hide = DS_Game_Maker.SettingsLib.GetSetting("HIDE_OLD_ACTIONS") == "1";
            var BannedActions = new List<string>();
            BannedActions.Add("load collision map");
            BannedActions.Add("if position free");
            BannedActions.Add("if position occupied");
            BannedActions.Add("palib");
            BannedActions.Add("run c script");
            BannedActions.Add("enable guitar controller");
            BannedActions.Add("set color");
            byte BackupIndex = 100;
            try
            {
                BackupIndex = (byte)RAppliesTo.SelectedIndex;
            }
            catch
            {
            }
            foreach (TabPage X in RAppliesTo.TabPages)
            {
                try
                {
                    if (X.Controls.Count > 0)
                    {
                        for (int Y = 0, loopTo = X.Controls.Count - 1; Y <= loopTo; Y++)
                        {
                            X.Controls[Y].MouseDown -= ActionMouseDown;
                            X.Controls[Y].MouseEnter -= ActionMouseEnter;
                            X.Controls[Y].MouseUp -= ActionMouseUp;
                            X.Controls.RemoveAt(Y);
                        }
                    }
                }
                catch
                {
                }
                RAppliesTo.TabPages.Remove(X);
            }
            RAppliesTo.TabPages.Clear();
            for (byte X = 0; X <= 5; X++)
            {
                var Y = new TabPage();
                Y.Text = DS_Game_Maker.ScriptsLib.ActionTypeToString(X);
                Y.Name = DS_Game_Maker.ScriptsLib.ActionTypeToString(X) + "TabPage";

                Y.AutoScroll = true;
                Y.SetAutoScrollMargin(8, 8);

                var Actions = new List<string>();
                foreach (string Z in Directory.GetFiles(DS_Game_Maker.DSGMlib.AppPath + "Actions"))
                {
                    string ActionName = Z.Substring(Z.LastIndexOf(@"\") + 1);
                    ActionName = ActionName.Substring(0, ActionName.LastIndexOf("."));
                    if (Hide)
                    {
                        if (BannedActions.Contains(ActionName.ToLower()))
                            continue;
                    }
                    byte ActionType = 100;
                    foreach (string I in File.ReadAllLines(Z))
                    {
                        if (I.StartsWith("TYPE "))
                            ActionType = Convert.ToByte(I.Substring(5));
                    }
                    if (ActionType == 100)
                        continue; // Bad action
                    if (!(ActionType == X))
                        continue;
                    Actions.Add(ActionName);
                }
                short DOn = 0;
                short XOn = 0;
                short YOn = 0;
                foreach (string Z in Actions)
                {
                    var NewPanel = new Panel();
                    NewPanel.Size = new Size(32, 32);
                    NewPanel.Tag = Z;
                    NewPanel.Name = Z.Replace(" ", "_") + "ActionPanel";
                    NewPanel.Location = new Point(10 + XOn * 32 + XOn * 10, 10 + YOn * 32 + YOn * 10);
                    // MsgError(Z + " at " + NewPanel.Location.ToString)
                    NewPanel.BackgroundImage = DS_Game_Maker.ActionsLib.ActionGetIcon(Z);
                    Y.Controls.Add(NewPanel);
                    // AddHandler NewPanel.Click, AddressOf ActionPanelClicked
                    NewPanel.MouseDown += ActionMouseDown;
                    NewPanel.MouseEnter += ActionMouseEnter;
                    NewPanel.MouseUp += ActionMouseUp;
                    if (XOn == 6)
                    {
                        YOn = (short)(YOn + 1);
                        XOn = 0;
                    }
                    else
                    {
                        XOn = (short)(XOn + 1);
                    }
                    DOn = (short)(DOn + 1);
                }
                RAppliesTo.TabPages.Add(Y);
            }
            if (!(BackupIndex == 100))
                RAppliesTo.SelectedIndex = BackupIndex;
        }

        public void ChangeSprite(string OldName, string NewName)
        {
            for (byte DOn = 0, loopTo = (byte)(SpriteDropper.Items.Count - 1); DOn <= loopTo; DOn++)
            {
                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(SpriteDropper.Items[DOn], OldName, false)))
                    SpriteDropper.Items[DOn] = NewName;
            }
            if ((SpriteDropper.Text ?? "") == (OldName ?? ""))
                SpriteDropper.Text = NewName;
        }

        private void DObject_Load(object sender, EventArgs e)
        {
            SelectedEvent = 100;
            ThinList = DS_Game_Maker.SettingsLib.GetSetting("SHRINK_ACTIONS_LIST") == "1";
            MainToolStrip.Renderer = new DS_Game_Maker.clsToolstripRenderer();
            ActionRightClickMenu.Renderer = new DS_Game_Maker.clsMenuRenderer();
            EventRightClickMenu.Renderer = new DS_Game_Maker.clsMenuRenderer();
            Text = ObjectName;
            var argRAppliesTo = ActionsToAddTabControl;
            PopulateActionsTabControl(ref argRAppliesTo);
            ActionsToAddTabControl = argRAppliesTo;
            NameTextBox.Text = ObjectName;
            string XDSLine = DS_Game_Maker.DSGMlib.GetXDSLine("OBJECT " + ObjectName + ",");
            MyXDSLines = string.Empty;
            // MyXDSLines += XDSLine
            EventsListBox.Items.Clear();
            foreach (string X in DS_Game_Maker.DSGMlib.GetXDSFilter("EVENT " + ObjectName + ","))
            {
                MyXDSLines += X + Constants.vbCrLf;
                DEventMainClasses.Add(DS_Game_Maker.ScriptsLib.MainClassTypeToString(Conversions.ToByte(DS_Game_Maker.DSGMlib.iGet(X, (byte)1, ","))));
                DEventSubClasses.Add(DS_Game_Maker.DSGMlib.iGet(X, (byte)2, ","));
            }
            foreach (string X in DS_Game_Maker.DSGMlib.GetXDSFilter("ACT " + ObjectName + ","))
                MyXDSLines += X + Constants.vbCrLf;
            foreach (string X in DEventSubClasses)
                EventsListBox.Items.Add(X);
            SpriteName = DS_Game_Maker.DSGMlib.iGet(XDSLine, (byte)1, ",");
            // MsgError(iget(XDSLine, 1, ","))
            Frame = Convert.ToInt16(DS_Game_Maker.DSGMlib.iGet(XDSLine, (byte)2, ","));
            SpriteDropper.Items.Clear();
            SpriteDropper.Items.Add("None");
            foreach (string X in DS_Game_Maker.DSGMlib.GetXDSFilter("SPRITE "))
                SpriteDropper.Items.Add(DS_Game_Maker.DSGMlib.iGet(X.Substring(7), (byte)0, ","));
            SpriteDropper.Text = SpriteName;
            if (EventsListBox.Items.Count > 0)
            {
                SaveOnNextChange = false;
                SelectedEvent = 0;
                EventsListBox.SelectedIndex = 0;
            }
            ArgumentsHeaderLabel.Height = 120;
            ArgumentsListLabel.Height = 94;
        }

        private void DAcceptButton_Click(object sender, EventArgs e)
        {
            if (MyXDSLines.Length > 0)
                SaveCurrentData();
            string NewName = NameTextBox.Text;
            if (!((ObjectName ?? "") == (NewName ?? "")))
            {
                if (DS_Game_Maker.DSGMlib.GUIResNameChecker(NameTextBox.Text))
                    return;
            }
            if (NewName == "NoData")
            {
                DS_Game_Maker.DSGMlib.MsgWarn("'NoData' is not a valid name. You must choose another name.");
                return;
            }
            if (NewName == "this")
            {
                DS_Game_Maker.DSGMlib.MsgWarn("'this' is not a valid name. You must choose another name.");
                return;
            }
            string OldLine = DS_Game_Maker.DSGMlib.GetXDSLine("OBJECT " + ObjectName + ",");
            string NewLine = "OBJECT " + NewName + "," + SpriteDropper.Text + "," + Frame.ToString();
            DS_Game_Maker.DSGMlib.XDSChangeLine(OldLine, NewLine);
            DS_Game_Maker.DSGMlib.XDSRemoveFilter("EVENT " + ObjectName + ",");
            DS_Game_Maker.DSGMlib.XDSRemoveFilter("ACT " + ObjectName + ",");
            foreach (string X in DS_Game_Maker.DSGMlib.GetXDSFilter("OBJECTPLOT " + ObjectName + ","))
                DS_Game_Maker.DSGMlib.XDSChangeLine(X, "OBJECTPLOT " + NewName + X.Substring(X.IndexOf(",")));
            string FinalString = string.Empty;
            if ((NewName ?? "") == (ObjectName ?? ""))
            {
                FinalString = MyXDSLines;
            }
            else if (MyXDSLines.Length > 0)
            {
                foreach (string X_ in DS_Game_Maker.DSGMlib.StringToLines(MyXDSLines))
                {
                    string X = X_;
                    if (X.StartsWith("EVENT "))
                    {
                        X = "EVENT " + NewName + X.Substring(X.IndexOf(","));
                    }
                    if (X.StartsWith("ACT "))
                    {
                        X = "ACT " + NewName + X.Substring(X.IndexOf(","));
                    }
                    FinalString += X + Constants.vbCrLf;
                }
            }
            // FinalString = UpdateActionsName(FinalString, "Object", ObjectName, NewName, True)
            DS_Game_Maker.DSGMlib.CurrentXDS += Constants.vbCrLf + FinalString + Constants.vbCrLf;
            foreach (Form X in Program.Forms.main_Form.MdiChildren)
            {
                if (X.Name == "Room")
                {
                    DS_Game_Maker.Room DForm = (DS_Game_Maker.Room)X;
                    DForm.RenameObjectDropper(ObjectName, NewName);
                    for (byte DOn = 0, loopTo = (byte)(DForm.Objects.Length - 1); DOn <= loopTo; DOn++)
                    {
                        if (DForm.Objects[(int)DOn].InUse & (DForm.Objects[(int)DOn].ObjectName ?? "") == (ObjectName ?? ""))
                            DForm.Objects[(int)DOn].ObjectName = NewName;
                    }
                }
            }
            foreach (string X in DS_Game_Maker.DSGMlib.GetXDSFilter("ACT "))
            {
                short SubPoint = (short)(DS_Game_Maker.DSGMlib.iGet(X, (byte)0, ",").Length + 1 + DS_Game_Maker.DSGMlib.iGet(X, (byte)1, ",").Length);
                short SubPoint2 = (short)(SubPoint + 1 + DS_Game_Maker.DSGMlib.iGet(X, (byte)2, ",").Length + 1);
                if (DS_Game_Maker.DSGMlib.iGet(X, (byte)1, ",") == "6")
                {
                    if ((DS_Game_Maker.DSGMlib.iGet(X, (byte)2, ",") ?? "") == (ObjectName ?? ""))
                    {
                        DS_Game_Maker.DSGMlib.XDSChangeLine(X, X.Substring(0, SubPoint) + "," + NewName + "," + X.Substring(SubPoint2));
                    }
                }
            }
            foreach (string X in DS_Game_Maker.DSGMlib.GetXDSFilter("EVENT "))
            {
                if (DS_Game_Maker.DSGMlib.iGet(X, (byte)1, ",") == "6" & (DS_Game_Maker.DSGMlib.iGet(X, (byte)2, ",") ?? "") == (ObjectName ?? ""))
                    DS_Game_Maker.DSGMlib.XDSChangeLine(X, DS_Game_Maker.DSGMlib.iGet(X, (byte)0, ",") + ",6," + NewName);
            }
            DS_Game_Maker.DSGMlib.CurrentXDS = DS_Game_Maker.DSGMlib.UpdateActionsName(DS_Game_Maker.DSGMlib.CurrentXDS, "Object", ObjectName, NewName, true);
            foreach (Form X in Program.Forms.main_Form.MdiChildren)
            {
                if (!DS_Game_Maker.DSGMlib.IsObject(X.Text))
                    continue;
                DObject DForm = (DObject)X;
                if (DForm.MyXDSLines.Length == 0)
                    continue;
                string LF = string.Empty;
                foreach (string Y_ in DS_Game_Maker.DSGMlib.StringToLines(DForm.MyXDSLines))
                {
                    string Y = Y_;
                    if (Y.StartsWith("EVENT "))
                    {
                        if (DS_Game_Maker.DSGMlib.iGet(Y, (byte)1, ",") == "6" & (DS_Game_Maker.DSGMlib.iGet(Y, (byte)2, ",") ?? "") == (ObjectName ?? ""))
                            Y = DS_Game_Maker.DSGMlib.iGet(Y, (byte)0, ",") + ",6," + NewName;
                    }
                    if (Y.StartsWith("ACT "))
                    {
                        short SubPoint = (short)(DS_Game_Maker.DSGMlib.iGet(Y, (byte)0, ",").Length + 1 + DS_Game_Maker.DSGMlib.iGet(Y, (byte)1, ",").Length);
                        short SubPoint2 = (short)(SubPoint + 1 + DS_Game_Maker.DSGMlib.iGet(Y, (byte)2, ",").Length + 1);
                        if (DS_Game_Maker.DSGMlib.iGet(Y, (byte)1, ",") == "6" & (DS_Game_Maker.DSGMlib.iGet(Y, (byte)2, ",") ?? "") == (ObjectName ?? ""))
                        {
                            Y = Y.Substring(0, SubPoint) + "," + NewName + "," + Y.Substring(SubPoint2);
                        }
                    }
                    LF += Y + Constants.vbCrLf;
                }
                DForm.MyXDSLines = LF;
            }
            foreach (Form X in Program.Forms.main_Form.MdiChildren)
            {
                if (!(X.Name == "DObject"))
                    continue;
                DObject DForm = (DObject)X;
                DForm.MyXDSLines = DS_Game_Maker.DSGMlib.UpdateActionsName(DForm.MyXDSLines, "Object", ObjectName, NewName, true);
            }
            DS_Game_Maker.DSGMlib.UpdateArrayActionsName("Object", ObjectName, NewName, true);
            foreach (Form X in Program.Forms.main_Form.MdiChildren)
            {
                if (!(X.Name == "DObject"))
                    continue;
                if (((DObject)X).DEventMainClasses.Count == 0)
                    continue;
                // MsgError("processing form " + X.Text)
                for (byte Y = 0, loopTo1 = (byte)(((DObject)X).DEventMainClasses.Count - 1); Y <= loopTo1; Y++)
                {
                    // MsgError("mainclass " + Y.ToString + " is " + DirectCast(X, DObject).DEventMainClasses(Y))
                    // MsgError("subclass " + Y.ToString + " is " + DirectCast(X, DObject).DEventSubClasses(Y))
                    if (((DObject)X).DEventMainClasses[Y] == "Collision" & (((DObject)X).DEventSubClasses[Y] ?? "") == (ObjectName ?? ""))
                    {
                        ((DObject)X).DEventSubClasses[Y] = NewName;
                    }
                }
                foreach (Control Y in X.Controls)
                {
                    if (!(Y.Name == "ObjectPropertiesPanel"))
                        continue;
                    foreach (Control Z in Y.Controls)
                    {
                        if (Z.Name == "EventsListBox")
                            ((ListBox)Z).Invalidate();
                    }
                }
            }
            foreach (TreeNode X in Program.Forms.main_Form.ResourcesTreeView.Nodes[(int)DS_Game_Maker.DSGMlib.ResourceIDs.DObject].Nodes)
            {
                if ((X.Text ?? "") == (ObjectName ?? ""))
                    X.Text = NewName;
            }
            Close();
        }

        public void AddSprite(string SpriteName)
        {
            SpriteDropper.Items.Add(SpriteName);
        }

        public string GetSpriteName()
        {
            return SpriteDropper.Text;
        }

        public void DeleteSprite()
        {
            SpriteDropper.Text = "None";
        }

        private void SpriteDropper_SelectedIndexChanged(object sender, EventArgs e)
        {
            OpenSpriteButton.Enabled = true;
            short ImageCount = 0;
            if (SpriteDropper.Text == "None")
            {
                ImageCount = 1;
                OpenSpriteButton.Enabled = false;
            }
            else
            {
                foreach (string X_ in Directory.GetFiles(DS_Game_Maker.SessionsLib.SessionPath + "Sprites"))
                {
                    string X = X_;
                    X = X.Substring(X.LastIndexOf(@"\") + 1);
                    X = X.Substring(0, X.LastIndexOf("."));
                    X = X.Substring(X.IndexOf("_") + 1);
                    if ((X ?? "") == (SpriteDropper.Text ?? ""))
                        ImageCount = (short)(ImageCount + 1);
                }
            }
            FrameLimit = ImageCount;
            if (!(Frame <= FrameLimit - 1))
                Frame = 0;
            FrameLeftButton.Enabled = true;
            FrameRightButton.Enabled = true;
            if (FrameLimit == 1)
                FrameRightButton.Enabled = false;
            if (Frame == 0)
                FrameLeftButton.Enabled = false;
            if (Frame == FrameLimit - 1)
                FrameRightButton.Enabled = false;
            RenderSprite();
        }

        private void FrameLeftButton_Click(object sender, EventArgs e)
        {
            Frame = (short)(Frame - 1);
            FrameRightButton.Enabled = true;
            RenderSprite();
            if (Frame == 0)
                FrameLeftButton.Enabled = false;
        }

        private void FrameRightButton_Click(object sender, EventArgs e)
        {
            Frame = (short)(Frame + 1);
            FrameLeftButton.Enabled = true;
            RenderSprite();
            if (Frame == FrameLimit - 1)
                FrameRightButton.Enabled = false;
        }

        private void AddEventButton_Click(object sender, EventArgs e)
        {
            Program.Forms.dEvent_Form.Text = "Add Event";
            Program.Forms.dEvent_Form.ShowDialog();
            if (!Program.Forms.dEvent_Form.UseData)
                return;
            string MainClass = Program.Forms.dEvent_Form.MainClass;
            string SubClass = Program.Forms.dEvent_Form.SubClass;
            if (MainClass == "NoData")
                return;
            for (short X = 0, loopTo = (short)(DEventMainClasses.Count - 1); X <= loopTo; X++)
            {
                if ((DEventMainClasses[X] ?? "") == (MainClass ?? "") & (DEventSubClasses[X] ?? "") == (SubClass ?? ""))
                {
                    SaveOnNextChange = true;
                    EventsListBox.SelectedIndex = X - 1;
                    return;
                }
            }
            string NewLine = "EVENT " + NameTextBox.Text + "," + DS_Game_Maker.ScriptsLib.MainClassStringToType(MainClass).ToString() + "," + SubClass;
            MyXDSLines += NewLine + Constants.vbCrLf;
            if (DEventMainClasses.Count == 0)
            {
                SaveOnNextChange = false;
            }
            else
            {
                SaveOnNextChange = true;
            }
            DEventMainClasses.Add(MainClass);
            DEventSubClasses.Add(SubClass);
            EventsListBox.Items.Add(SubClass);
            EventsListBox.SelectedIndex = EventsListBox.Items.Count - 1;
        }

        private void EventsListBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 24;
        }

        private void EventsListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            bool IsSelected = false;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                IsSelected = true;
            var Drawable = new Bitmap(16, 16);
            string FinalText = string.Empty;
            switch (DS_Game_Maker.ScriptsLib.MainClassStringToType(DEventMainClasses[e.Index]))
            {
                case (byte)0:
                    {
                        Drawable = Properties.Resources.QuestionIcon;
                        FinalText = "<Unkown>";
                        break;
                    }
                case (byte)1:
                    {
                        Drawable = Properties.Resources.CreateEventIcon;
                        FinalText = "Create";
                        break;
                    }
                case (byte)2:
                case (byte)3:
                case (byte)4:
                    {
                        Drawable = Properties.Resources.KeyIcon;
                        FinalText = DEventSubClasses[e.Index] + " " + DEventMainClasses[e.Index];
                        break;
                    }
                case (byte)5:
                    {
                        Drawable = Properties.Resources.StylusIcon;
                        FinalText += "Touch (" + DEventSubClasses[e.Index] + ")";
                        break;
                    }
                case (byte)6:
                    {
                        Drawable = Properties.Resources.Collision;
                        string TheWith = DEventSubClasses[e.Index];
                        if (TheWith == "NoData")
                            TheWith = "<Unknown>";
                        FinalText += "Collision with " + TheWith;
                        break;
                    }
                case (byte)7:
                    {
                        Drawable = Properties.Resources.ClockIcon;
                        FinalText = "Step";
                        break;
                    }
                case (byte)8:
                    {
                        Drawable = Properties.Resources.OtherIcon;
                        FinalText += DEventSubClasses[e.Index];
                        break;
                    }
            }
            e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            if (IsSelected)
                e.Graphics.DrawImage(Properties.Resources.BarBGSmall, e.Bounds);
            if (IsSelected)
                e.Graphics.DrawImageUnscaled(Properties.Resources.RoundedBlock, new Point(2, e.Bounds.Y + 2));
            e.Graphics.DrawImage(Drawable, new Point(4, e.Bounds.Y + 4));
            // If IsSelected Then
            // e.Graphics.DrawString(FinalText, New Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Black, 24, 5 + e.Bounds.Y)
            // e.Graphics.DrawString(FinalText, New Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.White, 24, 4 + e.Bounds.Y)
            // Else
            // e.Graphics.DrawString(FinalText, New Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Black, 24, 4 + e.Bounds.Y)
            // End If
            if (IsSelected)
            {
                e.Graphics.DrawString(FinalText, new Font("Tahoma", 11f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.White, 24f, 5 + e.Bounds.Y);
                e.Graphics.DrawString(FinalText, new Font("Tahoma", 11f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Black, 24f, 4 + e.Bounds.Y);
            }
            else
            {
                e.Graphics.DrawString(FinalText, new Font("Tahoma", 11f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Black, 24f, 4 + e.Bounds.Y);
            }
        }

        public void DeleteEvent(short TheIndex)
        {
            SaveOnNextChange = false;
            short FI = 0;
            if (EventsListBox.Items.Count > 1)
            {
                if (TheIndex == 0)
                    FI = 1;
                else
                    FI = 0;
                EventsListBox.SelectedIndex = FI;
            }
            // SelectedEvent = FI
            else
            {
                SelectedEvent = 100;
            }
            string FinalString = string.Empty;
            foreach (string X in DS_Game_Maker.DSGMlib.StringToLines(MyXDSLines))
            {
                if (X.StartsWith("EVENT " + ObjectName + "," + DS_Game_Maker.ScriptsLib.MainClassStringToType(DEventMainClasses[TheIndex]).ToString() + "," + DEventSubClasses[TheIndex]))
                {
                    continue;
                }
                if (X.StartsWith("ACT " + ObjectName + "," + DS_Game_Maker.ScriptsLib.MainClassStringToType(DEventMainClasses[TheIndex]).ToString() + "," + DEventSubClasses[TheIndex] + ","))
                {
                    continue;
                }
                FinalString += X + Constants.vbCrLf;
            }
            MyXDSLines = FinalString;
            EventsListBox.Items.RemoveAt(TheIndex);
            if (EventsListBox.Items.Count > 0)
                SelectedEvent = (short)EventsListBox.SelectedIndex;
            DEventMainClasses.RemoveAt(TheIndex);
            DEventSubClasses.RemoveAt(TheIndex);
            // If EventsListBox.SelectedIndex = TheIndex Then
            // ActionsList.Items.Clear()
            // Actions.Clear()
            // ActionDisplays.Clear()
            // ActionAppliesTos.Clear()
            // ActionArguments.Clear()
            // ActionImages.Clear()
            // End If
            // EventsListBox.Items.RemoveAt(TheIndex)
            // DEventMainClasses.RemoveAt(TheIndex)
            // DEventSubClasses.RemoveAt(TheIndex)
        }

        private void DeleteEventButton_Click(object sender, EventArgs e)
        {
            // MsgError(MyXDSLines)
            // Exit Sub
            if (EventsListBox.SelectedIndices.Count == 0)
                return;
            byte Response = (byte)MessageBox.Show("Are you sure you want to remove the Event and all of its Actions?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (!(Response == (int)MsgBoxResult.Yes))
                return;
            DeleteEvent((short)EventsListBox.SelectedIndices[0]);
        }

        private void ChangeEventButton_Click(object sender, EventArgs e)
        {
            if (EventsListBox.SelectedIndices.Count == 0)
                return;
            Program.Forms.dEvent_Form.ShowDialog();
            if (Program.Forms.dEvent_Form.UseData == false)
                return;
            short I = (short)EventsListBox.SelectedIndex;
            byte OldMC = DS_Game_Maker.ScriptsLib.MainClassStringToType(DEventMainClasses[I]);
            string OldSC = DEventSubClasses[I];
            byte MC = DS_Game_Maker.ScriptsLib.MainClassStringToType(Program.Forms.dEvent_Form.MainClass);
            string SC = Program.Forms.dEvent_Form.SubClass;
            if (MC == OldMC & (SC ?? "") == (OldSC ?? ""))
                return;
            if (DEventMainClasses.Contains(Program.Forms.dEvent_Form.MainClass) & DEventSubClasses.Contains(Program.Forms.dEvent_Form.SubClass))
            {
                DS_Game_Maker.DSGMlib.MsgWarn("That event is already in-use.");
                return;
            }
            string FinalString = string.Empty;
            foreach (string P_ in DS_Game_Maker.DSGMlib.StringToLines(MyXDSLines))
            {
                string P = P_;
                if (P.Length == 0)
                    continue;
                if (P.StartsWith("EVENT ") & P.EndsWith("," + OldMC.ToString() + "," + OldSC))
                {
                    P = P.Substring(0, P.Length - ("," + OldMC.ToString() + "," + OldSC).Length);
                    P += "," + MC.ToString() + "," + SC;
                }
                if (P.StartsWith("ACT " + ObjectName + "," + OldMC.ToString() + "," + OldSC + ","))
                {
                    P = P.Substring(("ACT " + ObjectName + "," + MC.ToString() + "," + SC + ",").Length);
                    P = "ACT " + ObjectName + "," + MC.ToString() + "," + SC + "," + P;
                }
                FinalString += P + Constants.vbCrLf;
            }
            MyXDSLines = FinalString;
            DEventMainClasses[I] = Program.Forms.dEvent_Form.MainClass;
            DEventSubClasses[I] = SC;
            EventsListBox.Refresh();
            // MsgError(Actions.Count.ToString)
            // MsgError(ActionArguments.Count.ToString)
            // MsgError(ActionAppliesTos.Count.ToString)
            // MsgError(ActionDisplays.Count.ToString)
            // MsgError(ActionImages.Count.ToString)
            // For i As Byte = 0 To Actions.Count - 1
            // MsgError(Actions(i))
            // MsgError(ActionArguments(i))
            // MsgError(ActionAppliesTos(i))
            // Next
            // MsgError(MyXDSLines)
            // For Each X As String In ActionAppliesTos
            // MsgError(X)
            // Next
            // MsgError(FS)
            // Actions(DraggingInternal) = Actions(DraggingInternal - 1)
            // Exit Sub
            // DEvent.Text = "Change Event"
            // MsgError(MyXDSLines)
        }

        private void ActionsList_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            bool IsSelected = false;
            byte ThinNum = (byte)(ThinList ? 24 : 36);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                IsSelected = true;
            short MyX = (short)(CurrentIndents[e.Index] * (ThinList ? 16 : 24));
            e.Graphics.FillRectangle(Brushes.White, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            if (IsSelected)
                e.Graphics.DrawImage(Properties.Resources.BarBG, new Rectangle(0, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            string ThingString = ActionAppliesTos[e.Index];
            string ArgString = string.Empty;
            string NiceArgs = ArgumentsMakeAttractive(ActionArguments[e.Index], true);
            if (ThinList)
            {
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                e.Graphics.DrawImage(DS_Game_Maker.ActionsLib.ActionGetIcon(Actions[e.Index]), new Rectangle(MyX + 2, e.Bounds.Y + 2, 20, 20));
                e.Graphics.DrawString(ActionDisplays[e.Index], new Font("Tahoma", 11f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.White, MyX + ThinNum, e.Bounds.Y + 5 + 1);
                e.Graphics.DrawString(ActionDisplays[e.Index], new Font("Tahoma", 11f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Black, MyX + ThinNum, e.Bounds.Y + 5);
                return;
            }
            else
            {
                e.Graphics.DrawImageUnscaled(DS_Game_Maker.ActionsLib.ActionGetIcon(Actions[e.Index]), new Point(MyX + 2, e.Bounds.Y + 2));
                if (NiceArgs.Length > 0)
                {
                    if (ThingString == "this")
                    {
                        if (Actions[e.Index] == "Execute Code")
                        {
                            ArgString = NiceArgs;
                        }
                        else
                        {
                            ArgString = "Arguments of " + NiceArgs;
                        }
                    }
                    else if (DS_Game_Maker.DSGMlib.IsObject(ThingString))
                    {
                        if (Actions[e.Index] == "Execute Code")
                        {
                            ArgString = "Applies to instances of " + ThingString + ": " + NiceArgs;
                        }
                        else
                        {
                            ArgString = "Applies to instances of " + ThingString + " with Args. " + NiceArgs;
                        }
                    }
                    else if (Actions[e.Index] == "Execute Code")
                    {
                        ArgString = "Applies to instance IDs " + ThingString + ": " + NiceArgs;
                    }
                    else
                    {
                        ArgString = "Applies to instance IDs " + ThingString + " with Args. " + NiceArgs;
                    }
                    // Else
                    // If IsObject(ThingString) Then
                    // ArgString = "Applies to instances of " + ThingString
                    // Else
                    // ArgString = "Applies to instance IDs " + ThingString
                    // End If
                }
                short TheY = (short)(e.Bounds.Y + (NiceArgs.Length > 0 ? 5 : 10));
                if (IsSelected)
                {
                    e.Graphics.DrawString(ArgString, new Font("Tahoma", 11f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.White, MyX + ThinNum, (float)(e.Bounds.Y + ThinNum / 2d + 1d));
                    e.Graphics.DrawString(ArgString, new Font("Tahoma", 11f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Gray, MyX + ThinNum, e.Bounds.Y + 18);
                    e.Graphics.DrawString(ActionDisplays[e.Index], new Font("Tahoma", 11f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.White, MyX + ThinNum, e.Bounds.Y + (NiceArgs.Length > 0 ? 5 : 10) + 1);
                    e.Graphics.DrawString(ActionDisplays[e.Index], new Font("Tahoma", 11f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Black, MyX + ThinNum, e.Bounds.Y + (NiceArgs.Length > 0 ? 5 : 10));
                    e.Graphics.DrawImageUnscaled(Properties.Resources.Corners, new Point(-7, e.Bounds.Y - 6));
                    e.Graphics.DrawImageUnscaled(Properties.Resources.Corners, new Point(-7, e.Bounds.Y + ThinNum - 6));
                    e.Graphics.DrawImageUnscaled(Properties.Resources.Corners, new Point(e.Bounds.Width - 7, e.Bounds.Y - 7));
                    e.Graphics.DrawImageUnscaled(Properties.Resources.Corners, new Point(e.Bounds.Width - 7, e.Bounds.Y + 29));
                }
                else
                {
                    e.Graphics.DrawString(ArgString, new Font("Tahoma", 11f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.LightGray, MyX + ThinNum, (float)(e.Bounds.Y + ThinNum / 2d));
                    e.Graphics.DrawString(ActionDisplays[e.Index], new Font("Tahoma", 11f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Black, MyX + ThinNum, e.Bounds.Y + (NiceArgs.Length > 0 ? 5 : 10));
                }
            }
        }

        private void ActionsList_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = ThinList ? 24 : 36;
        }

        public string ArgumentsMakeAttractive(string InputArguments, bool HideSymbols)
        {
            string Returnable = InputArguments;
            Returnable = Returnable.Replace(";", ", ").Replace("<com>", ",").Replace("<sem>", ";");
            if (HideSymbols)
                Returnable = Returnable.Replace("<br|>", " .. ");
            return Returnable;
        }

        public void SaveCurrentData()
        {
            // MsgError(SelectedEvent.ToString)
            // MsgError("Saved Current Data")
            if (EventsListBox.SelectedIndices.Count == 0)
                return;
            // If DEventMainClasses.Count = 0 Then Exit Sub
            // MsgError("SaveCurrentData 2")
            string FinalString = string.Empty;
            // MsgError("filter is ACT " + ObjectName + "," + DEventMainClasses(TempIndex) + "," + DEventSubClasses(TempIndex) + ",")
            // MsgError("filter is ACT " + ObjectName + "," + MainClassStringToType(DEventMainClasses(SelectedEvent)).ToString + "," + DEventSubClasses(SelectedEvent) + ",")
            // MsgError(MainClassStringToType(DEventMainClasses(SelectedEvent)).ToString)
            foreach (string X in DS_Game_Maker.DSGMlib.StringToLines(MyXDSLines))
            {
                if (X.Length == 0)
                    continue;
                // MsgError(X + vbcrlf + vbcrlf + "against" + vbcrlf + vbcrlf + "ACT " + ObjectName + "," + MainClassStringToType(DEventMainClasses(SelectedEvent)).ToString + "," + DEventSubClasses(SelectedEvent) + ",")
                if (!X.StartsWith("ACT " + ObjectName + "," + DS_Game_Maker.ScriptsLib.MainClassStringToType(DEventMainClasses[SelectedEvent]).ToString() + "," + DEventSubClasses[SelectedEvent] + ","))
                {
                    FinalString += X + Constants.vbCrLf;
                }
            }
            // MsgError(FinalString)
            // MsgError("new index is " + EventsListBox.SelectedIndex.ToString)
            // Dim TempIndex As Byte = EventsListBox.SelectedIndex
            for (short X = 0, loopTo = (short)(Actions.Count - 1); X <= loopTo; X++)
            {
                string TheNewLine = "ACT " + ObjectName + "," + DS_Game_Maker.ScriptsLib.MainClassStringToType(DEventMainClasses[SelectedEvent]).ToString() + "," + DEventSubClasses[SelectedEvent] + ",";
                TheNewLine += Actions[X] + "," + ActionArguments[X] + "," + ActionAppliesTos[X];
                FinalString += TheNewLine + Constants.vbCrLf;
            }
            MyXDSLines = FinalString;
        }

        private void EventsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // MsgBox(If(SaveDataOnEventChange, "true", "false"))
            if (EventsListBox.Items.Count == 0)
                return;
            if (EventsListBox.SelectedIndex < 0)
                return;
            if (EventsListBox.SelectedIndex > DEventMainClasses.Count - 1)
                return;
            // If SelectedEvent = 100 Then Exit Sub
            if (SaveOnNextChange)
                SaveCurrentData();
            SaveOnNextChange = true;
            Actions.Clear();
            ActionArguments.Clear();
            ActionDisplays.Clear();
            ActionAppliesTos.Clear();
            ActionImages.Clear();
            ActionsList.Items.Clear();
            // GenerateIndentIndices()
            byte MainClass = DS_Game_Maker.ScriptsLib.MainClassStringToType(DEventMainClasses[EventsListBox.SelectedIndex]);
            string SubClass = DEventSubClasses[EventsListBox.SelectedIndex];
            // MsgError("Mainclass is " + MainClass.ToString)
            // MsgError("Subclass is " + SubClass.ToString)
            // MsgError("Gonna work on:" + vbcrlf + vbcrlf + MyXDSLines)
            short TempCount = 0;
            if (MyXDSLines.Length > 0)
            {
                foreach (string X_ in DS_Game_Maker.DSGMlib.StringToLines(MyXDSLines))
                {
                    string X = X_;
                    if (X.Length == 0)
                        continue;
                    if (!X.StartsWith("ACT "))
                        continue;
                    X = Conversions.ToString(DS_Game_Maker.DSGMlib.SillyFixMe(X));
                    // MsgError(X(X.Length - 1))
                    if (!((DS_Game_Maker.DSGMlib.iGet(X, (byte)1, ",") ?? "") == (MainClass.ToString() ?? "")) | !((DS_Game_Maker.DSGMlib.iGet(X, (byte)2, ",") ?? "") == (SubClass ?? "")))
                        continue;
                    // MsgError("On X at " + X)
                    string ActionName = DS_Game_Maker.DSGMlib.iGet(X, (byte)3, ",");
                    // MsgError("ActionName is " + ActionName)
                    string ActionArgs = DS_Game_Maker.DSGMlib.iGet(X, (byte)4, ",");
                    // ActionArgs = ActionArgs.Replace("<com>", ",")
                    // MsgError("Action Arguments are " + ActionArgs.ToString)
                    Actions.Add(ActionName);
                    ActionArguments.Add(ActionArgs);
                    string AppliesTo = DS_Game_Maker.DSGMlib.iGet(X, (byte)5, ",");
                    // MsgError(ActionEquateDisplay(ActionName, ActionArgs))
                    ActionDisplays.Add(DS_Game_Maker.ActionsLib.ActionEquateDisplay(ActionName, ActionArgs));
                    ActionAppliesTos.Add(AppliesTo);
                    ActionImages.Add(DS_Game_Maker.ActionsLib.ActionGetIconPath(ActionName, false));
                    // MsgError(ActionGetIconPath(ActionName, False))
                    TempCount = (short)(TempCount + 1);
                }
            }
            GenerateIndentIndices();
            if (TempCount > 0)
            {
                for (int X = 0, loopTo = TempCount - 1; X <= loopTo; X++)
                    ActionsList.Items.Add(string.Empty);
            }
            // Hmm
            // SaveDataOnEventChange = False
            SelectedEvent = (short)EventsListBox.SelectedIndex;
            // MsgError(SelectedEvent)
        }

        private void OpenSpriteButton_Click(object sender, EventArgs e)
        {
            foreach (Form TheForm in MdiChildren)
            {
                if ((TheForm.Text ?? "") == (SpriteDropper.Text ?? ""))
                {
                    TheForm.Focus();
                    return;
                }
            }
            var SpriteForm = new DS_Game_Maker.Sprite();
            SpriteForm.SpriteName = SpriteDropper.Text;
            object argInstance = (object)SpriteForm;
            DS_Game_Maker.DSGMlib.ShowInternalForm(ref argInstance);
            SpriteForm = (DS_Game_Maker.Sprite)argInstance;
        }

        public void EditAction()
        {
            if (ActionsList.SelectedIndices.Count == 0)
                return;
            if (ActionsList.SelectedIndices.Count > 1)
            {
                DS_Game_Maker.DSGMlib.MsgWarn("You may only edit one Action at once.");
                return;
            }
            short EditingIndex = (short)ActionsList.SelectedIndices[0];
            if (Actions[EditingIndex] == "Execute Code")
            {
                Program.Forms.editCode_Form.ReturnableCode = ActionArguments[EditingIndex];
                Program.Forms.editCode_Form.CodeMode = (byte)DS_Game_Maker.DSGMlib.CodeMode.DBAS;
                Program.Forms.editCode_Form.ImportExport = true;
                if (Program.Forms.editCode_Form.ShowDialog() == DialogResult.OK)
                {
                    ActionArguments[EditingIndex] = Program.Forms.editCode_Form.ReturnableCode;
                }
            }
            else
            {
                // MsgError("Previous to Opening the dialogue")
                // MsgError("AppliesTo: """ + ActionAppliesTos(EditingIndex) + """")
                {
                    var withBlock = Program.Forms.action_Form;
                    withBlock.ArgumentString = ActionArguments[EditingIndex];
                    withBlock.ActionName = Actions[EditingIndex];
                    withBlock.AppliesTo = ActionAppliesTos[EditingIndex];
                    withBlock.ShowDialog();
                    if (!withBlock.UseData)
                        return;
                    ActionAppliesTos[EditingIndex] = withBlock.AppliesTo;
                    ActionArguments[EditingIndex] = withBlock.ArgumentString;
                    ActionDisplays[EditingIndex] = DS_Game_Maker.ActionsLib.ActionEquateDisplay(withBlock.ActionName, withBlock.ArgumentString);
                }
                ActionsList.Invalidate();
            }
        }

        public bool ShouldAllowDialog(string ActionName)
        {
            bool NoAppliesTo = false;
            byte ArgCount = 0;
            foreach (string X in File.ReadAllLines(DS_Game_Maker.DSGMlib.AppPath + @"Actions\" + Actions[ActionsList.SelectedIndices[0]] + ".action"))
            {
                if (X == "NOAPPLIES")
                    NoAppliesTo = true;
                if (X.StartsWith("ARG "))
                    ArgCount = (byte)(ArgCount + 1);
            }
            if (ArgCount == 0 & NoAppliesTo)
                return false;
            return true;
        }

        private void ActionsList_MouseDoubleClick()
        {
            if (ActionsList.SelectedIndices.Count == 0)
                return;
            if (ShouldAllowDialog(Actions[ActionsList.SelectedIndices[0]]))
                EditAction();
        }

        private void DeleteRightClickButton_Click()
        {
            while (ActionsList.SelectedIndices.Count > 0)
            {
                short TheIndex = (short)ActionsList.SelectedIndices[0];
                Actions.RemoveAt(TheIndex);
                ActionDisplays.RemoveAt(TheIndex);
                ActionArguments.RemoveAt(TheIndex);
                ActionAppliesTos.RemoveAt(TheIndex);
                ActionImages.RemoveAt(TheIndex);
                ActionsList.Items.RemoveAt(TheIndex);
            }
            // ActionsList.Items.RemoveAt(ActionsList.SelectedIndex)
            GenerateIndentIndices();
        }

        private void ActionRightClickMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool HasActions = ActionsList.Items.Count > 0 ? true : false;
            DeleteActionRightClickButton.Enabled = HasActions;
            CutActionRightClickButton.Enabled = HasActions;
            CopyActionRightClickButton.Enabled = HasActions;
            ClearActionsRightClickButton.Enabled = HasActions;
            if (ActionsList.Items.Count > 0)
            {
                ActionsList.SelectedIndex = ActionsList.IndexFromPoint(ActionsList.PointToClient(MousePosition));
            }
            bool CanPaste = Clipboard.ContainsText();
            if (CanPaste) // Attempt to disprove paste ability
            {
                int DOn = 0;
                var TheItems = new List<string>();
                foreach (string X in DS_Game_Maker.DSGMlib.StringToLines(Clipboard.GetText()))
                {
                    if (X.Length > 0)
                    {
                        if (DOn == 0 & !(X == "DSGMACTS"))
                        {
                            CanPaste = false;
                            break;
                        }
                        if (DOn > 0)
                        {
                            TheItems.Add(X);
                        }
                        DOn += 1;
                    }
                }
                if (TheItems.Count == 0)
                    CanPaste = false;
            }
            EditValuesRightClickButton.Enabled = ActionsList.SelectedIndices.Count == 1 ? true : false;
            PasteActionBelowRightClickButton.Enabled = CanPaste;
            if (ActionsList.Items.Count > 0)
            {
                if (!ShouldAllowDialog(Actions[ActionsList.SelectedIndex]))
                {
                    EditValuesRightClickButton.Enabled = false;
                }
            }
        }

        private void ActionsList_MouseDown(object sender, MouseEventArgs e)
        {
            if (!(ActionsList.SelectionMode == SelectionMode.One))
                return;
            DragInternal = true;
            DraggingInternal = (short)ActionsList.SelectedIndex;
            Cursor = Cursors.NoMove2D;
        }

        private void ActionsList_MouseUp(object sender, MouseEventArgs e)
        {
            if (!(ActionsList.SelectionMode == SelectionMode.One))
                return;
            DragInternal = false;
            GenerateIndentIndices();
            ActionsList.Invalidate();
            Cursor = Cursors.Default;
        }

        private void ActionsList_MouseMove(object sender, MouseEventArgs e)
        {
            if (!(ActionsList.SelectionMode == SelectionMode.One))
                return;
            if (!DragInternal)
                return;
            if (DraggingInternal == ActionsList.SelectedIndex)
                return;
            if (DraggingInternal > ActionsList.SelectedIndex)
            {
                string TextForCurrent = Actions[DraggingInternal - 1];
                string TextForAbove = Actions[DraggingInternal];
                Actions[DraggingInternal] = TextForCurrent;
                Actions[DraggingInternal - 1] = TextForAbove;
                string ImageForCurrent = ActionImages[DraggingInternal - 1];
                string ImageForAbove = ActionImages[DraggingInternal];
                ActionImages[DraggingInternal] = ImageForCurrent;
                ActionImages[DraggingInternal - 1] = ImageForAbove;
                string ArgumentForCurrent = ActionArguments[DraggingInternal - 1];
                string ArgumentForAbove = ActionArguments[DraggingInternal];
                ActionArguments[DraggingInternal] = ArgumentForCurrent;
                ActionArguments[DraggingInternal - 1] = ArgumentForAbove;
                string DisplayForCurrent = ActionDisplays[DraggingInternal - 1];
                string DisplayForAbove = ActionDisplays[DraggingInternal];
                ActionDisplays[DraggingInternal] = DisplayForCurrent;
                ActionDisplays[DraggingInternal - 1] = DisplayForAbove;
                string ApplyForCurrent = ActionAppliesTos[DraggingInternal - 1];
                string ApplyForAbove = ActionAppliesTos[DraggingInternal];
                ActionAppliesTos[DraggingInternal] = ApplyForCurrent;
                ActionAppliesTos[DraggingInternal - 1] = ApplyForAbove;
                DraggingInternal = (short)(DraggingInternal - 1);
            }
            else
            {
                string TextForCurrent = Actions[DraggingInternal + 1];
                string TextForAbove = Actions[DraggingInternal];
                Actions[DraggingInternal] = TextForCurrent;
                Actions[DraggingInternal + 1] = TextForAbove;
                string ImageForCurrent = ActionImages[DraggingInternal + 1];
                string ImageForAbove = ActionImages[DraggingInternal];
                ActionImages[DraggingInternal] = ImageForCurrent;
                ActionImages[DraggingInternal + 1] = ImageForAbove;
                string ArgumentForCurrent = ActionArguments[DraggingInternal + 1];
                string ArgumentForAbove = ActionArguments[DraggingInternal];
                ActionArguments[DraggingInternal] = ArgumentForCurrent;
                ActionArguments[DraggingInternal + 1] = ArgumentForAbove;
                string DisplayForCurrent = ActionDisplays[DraggingInternal + 1];
                string DisplayForAbove = ActionDisplays[DraggingInternal];
                ActionDisplays[DraggingInternal] = DisplayForCurrent;
                ActionDisplays[DraggingInternal + 1] = DisplayForAbove;
                string ApplyForCurrent = ActionAppliesTos[DraggingInternal + 1];
                string ApplyForAbove = ActionAppliesTos[DraggingInternal];
                ActionAppliesTos[DraggingInternal] = ApplyForCurrent;
                ActionAppliesTos[DraggingInternal + 1] = ApplyForAbove;
                DraggingInternal = (short)(DraggingInternal + 1);
            }
            ActionsList.Invalidate();
        }

        // Function GenerateForumLine(ByVal RowID As Int16) As String
        // Dim Returnable As String = Actions(RowID) + " " + ArgumentsMakeAttractive(ActionArguments(RowID))
        // Dim ApplyTo As String = ActionAppliesTos(RowID)
        // If Not ApplyTo = "this" Then
        // If IsObject(ApplyTo) Then
        // ApplyTo = "instances of " + ApplyTo
        // Else
        // ApplyTo = "instances IDs " + ApplyTo
        // End If
        // Returnable += " (applying to " + ApplyTo + ")"
        // End If
        // Return Returnable
        // End Function

        public void RepopulateLibrary()
        {
            var argRAppliesTo = ActionsToAddTabControl;
            PopulateActionsTabControl(ref argRAppliesTo);
            ActionsToAddTabControl = argRAppliesTo;
        }

        // Private Sub CopyAllForForumButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        // Dim FinalString As String = "Actions for " + DEventMainClasses(SelectedEvent) + " Event on " + ObjectName + vbcrlf + vbcrlf
        // For X As Int16 = 0 To ActionsList.Items.Count - 1
        // FinalString += GenerateForumLine(X) + vbcrlf
        // Next
        // Clipboard.SetText(FinalString)
        // End Sub

        // Private Sub ActionsDropper_DropDownOpening(ByVal sender As System.Object, ByVal e As System.EventArgs)
        // For X As Byte = 0 To ActionsDropper.DropDownItems.Count - 1
        // ActionsDropper.DropDownItems(X).Enabled = True
        // Next
        // If ActionsList.Items.Count = 0 Then
        // CopyAllForForumButton.Enabled = False
        // 'FUTURE ADDITION??
        // End If
        // End Sub

        private void ClearRightClickButton_Click(object sender, EventArgs e)
        {
            byte Response = (byte)MessageBox.Show("Are you sure you want to clear the list of Actions?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (!(Response == (int)MsgBoxResult.Yes))
                return;
            Actions.Clear();
            ActionImages.Clear();
            ActionDisplays.Clear();
            ActionAppliesTos.Clear();
            ActionArguments.Clear();
            ActionsList.Items.Clear();
            GenerateIndentIndices();
        }

        private void CutRightClickButton_Click(object sender, EventArgs e)
        {
            CopyActionRightClickButton_Click();
            DeleteRightClickButton_Click();
        }

        private void EventRightClickMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!(EventsListBox.Items.Count == 0))
                EventsListBox.SelectedIndex = EventsListBox.IndexFromPoint(EventsListBox.PointToClient(MousePosition));
            for (byte X = 0, loopTo = (byte)(EventRightClickMenu.Items.Count - 1); X <= loopTo; X++)
                EventRightClickMenu.Items[X].Enabled = true;
            if (EventsListBox.Items.Count == 0)
            {
                ChangeEventRightClickButton.Enabled = false;
                DeleteEventRightClickButton.Enabled = false;
                ClearEventsButton.Enabled = false;
                return;
            }
        }

        private void ClearEventsButton_Click(object sender, EventArgs e)
        {
            byte Response = (byte)MessageBox.Show("Are you sure you want to clear the list of Events?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (!(Response == (int)MsgBoxResult.Yes))
                return;
            EventsListBox.Items.Clear();
            DEventMainClasses.Clear();
            DEventSubClasses.Clear();
            Actions.Clear();
            ActionImages.Clear();
            ActionDisplays.Clear();
            ActionAppliesTos.Clear();
            ActionArguments.Clear();
            ActionsList.Items.Clear();
            GenerateIndentIndices();
            MyXDSLines = string.Empty;
        }

        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            for (short i = 0, loopTo = (short)(ActionsList.Items.Count - 1); i <= loopTo; i++)
                ActionsList.SelectedIndices.Add(i);
            // Dim FinalString As String = "DSGMALL" + vbcrlf
            // For i As Int16 = 0 To Actions.Count - 1
            // FinalString += Actions(i) + ","
            // FinalString += ActionArguments(i) + ","
            // FinalString += ActionAppliesTos(i) + vbcrlf
            // Next
            // Clipboard.SetText(FinalString)
        }

        private void CopyActionRightClickButton_Click()
        {
            string FinalString = "DSGMACTS" + Constants.vbCrLf;
            foreach (short X in ActionsList.SelectedIndices)
                FinalString += Actions[X] + "," + ActionArguments[X] + "," + ActionAppliesTos[X] + Constants.vbCrLf;
            FinalString = FinalString.Substring(0, FinalString.Length - 1);
            Clipboard.SetText(FinalString);
        }

        // Private Sub PasteAtBottomButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        // If Not Clipboard.ContainsText Then Exit Sub
        // If Not Clipboard.GetText.StartsWith("DSGMALL") Then Exit Sub
        // For Each X As String In StringToLines(Clipboard.GetText)
        // If X.StartsWith("DSGMALL") Then Continue For
        // If X.Length = 0 Then Continue For
        // Dim ActionName As String = iget(X, 0, ",")
        // Dim ActionArgs As String = iget(X, 1, ",")
        // Dim ActionApply As String = iget(X, 2, ",")
        // Actions.Add(ActionName)
        // ActionArguments.Add(ActionArgs)
        // ActionAppliesTos.Add(ActionApply)
        // ActionDisplays.Add(ActionEquateDisplay(ActionName, ActionArgs))
        // ActionImages.Add(ActionGetIconPath(ActionName, False))
        // ActionsList.Items.Add(String.Empty)
        // Next
        // GenerateIndentIndices()
        // End Sub

        private void SelectOneButton_Click(object sender, EventArgs e)
        {
            ActionsList.SelectionMode = SelectionMode.One;
        }

        private void SelectManyButton_Click(object sender, EventArgs e)
        {
            ActionsList.SelectionMode = SelectionMode.MultiExtended;
        }

        private void ActionsList_MouseDoubleClick(object sender, MouseEventArgs e) => ActionsList_MouseDoubleClick();
        private void ActionsList_MouseDoubleClick(object sender, EventArgs e) => ActionsList_MouseDoubleClick();
        private void DeleteRightClickButton_Click(object sender, EventArgs e) => DeleteRightClickButton_Click();
        private void CopyActionRightClickButton_Click(object sender, EventArgs e) => CopyActionRightClickButton_Click();

    }
}