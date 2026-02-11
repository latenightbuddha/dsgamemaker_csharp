using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic;

namespace DS_Game_Maker
{

    public partial class Script
    {

        public string ScriptName;
        private string ScriptContent;
        private bool DoIt = false;
        private bool CanInsert = false;
        private string WhatsDone = string.Empty;

        private List<string> ArgumentNames = new List<string>();
        private List<string> ArgumentTypes = new List<string>();

        public Script()
        {
            InitializeComponent();
        }

        public void SaveChanges()
        {
            if (!((ScriptName ?? "") == (NameTextBox.Text ?? "")))
            {
                File.Move(DS_Game_Maker.SessionsLib.SessionPath + @"Scripts\" + ScriptName + ".dbas", DS_Game_Maker.SessionsLib.SessionPath + @"Scripts\" + NameTextBox.Text + ".dbas");
            }
            DS_Game_Maker.DSGMlib.XDSChangeLine(DS_Game_Maker.DSGMlib.GetXDSLine("SCRIPT " + ScriptName + ","), "SCRIPT " + NameTextBox.Text + "," + (ParseDBASChecker.Checked ? "1" : "0"));
            DS_Game_Maker.DSGMlib.XDSRemoveFilter("SCRIPTARG " + ScriptName + ",");
            if (ArgumentNames.Count > 0)
            {
                for (byte P = 0, loopTo = (byte)(ArgumentNames.Count - 1); P <= loopTo; P++)
                    DS_Game_Maker.DSGMlib.XDSAddLine("SCRIPTARG " + NameTextBox.Text + "," + ArgumentNames[P] + "," + ArgumentTypes[P]);
            }
            File.WriteAllText(DS_Game_Maker.SessionsLib.SessionPath + @"Scripts\" + NameTextBox.Text + ".dbas", MainTextBox.Text);
            foreach (TreeNode X in Program.Forms.main_Form.ResourcesTreeView.Nodes[(int)DS_Game_Maker.DSGMlib.ResourceIDs.Script].Nodes)
            {
                if ((X.Text ?? "") == (ScriptName ?? ""))
                    X.Text = NameTextBox.Text;
            }
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            if (MainTextBox.UndoRedo.CanUndo)
                MainTextBox.UndoRedo.Undo();
        }

        private void RedoButton_Click(object sender, EventArgs e)
        {
            if (MainTextBox.UndoRedo.CanRedo)
                MainTextBox.UndoRedo.Redo();
        }

        public void GoToLine(short LineNumber, short Position, short SelLength)
        {
            if (LineNumber >= MainTextBox.Lines.Count)
                return;
            MainTextBox.Caret.LineNumber = LineNumber;
            MainTextBox.Selection.Start += Position;
            MainTextBox.Selection.Length = SelLength;
        }

        private void Script_Load(object sender, EventArgs e)
        {
            // With MainTextBox.AutoComplete
            // .List.Clear()
            // .ListString = "Stylus_X,Stylus_Y"
            // .ListSeparator = ","
            // '.AutoHide = True
            // '.AutomaticLengthEntered = True
            // '.CancelAtStart = True
            // '.DropRestOfWord = True
            // '.FillUpCharacters = String.Empty
            // '.ImageSeparator = Nothing
            // .IsCaseSensitive = True
            // '.SingleLineAccept = False
            // '.StopCharacters = String.Empty
            // End With
            MainToolStrip.Renderer = new DS_Game_Maker.clsToolstripRenderer();
            MainTextBox.AcceptsTab = true;
            MainTextBox.Caret.HighlightCurrentLine = (int)Convert.ToByte(DS_Game_Maker.SettingsLib.GetSetting("HIGHLIGHT_CURRENT_LINE")) == 1;
            // MsgError("""" + ScriptName + """")
            ScriptContent = DS_Game_Maker.DSGMlib.PathToString(DS_Game_Maker.SessionsLib.SessionPath + @"Scripts\" + ScriptName + ".dbas");
            MainTextBox.Text = ScriptContent;
            Text = ScriptName;
            NameTextBox.Text = ScriptName;
            TidyUp();
            string XDSLine = DS_Game_Maker.DSGMlib.GetXDSLine("SCRIPT " + ScriptName + ",");
            XDSLine = XDSLine.Substring(XDSLine.LastIndexOf(",") + 1);
            ParseDBASChecker.Checked = XDSLine == "1";
            foreach (string X in DS_Game_Maker.DSGMlib.GetXDSFilter("SCRIPTARG " + ScriptName + ","))
            {
                string TheName = DS_Game_Maker.DSGMlib.iGet(X, (byte)1, ",");
                string TheType = DS_Game_Maker.DSGMlib.iGet(X, (byte)2, ",");
                ArgumentNames.Add(TheName);
                ArgumentTypes.Add(TheType);
            }
            ArrayToControl();
            UpdateLineStats();
        }

        private void DAcceptButton_Click(object sender, EventArgs e)
        {
            // TODO LATER - after renaming, update references to this in:
            // Other scripts, actions in events.
            string NewName = NameTextBox.Text;
            if (!((ScriptName ?? "") == (NewName ?? "")))
            {
                if (DS_Game_Maker.DSGMlib.GUIResNameChecker(NameTextBox.Text))
                    return;
            }
            SaveChanges();
            Close();
        }

        public void UpdateLineStats()
        {
            StatsLabel.Text = "Ln " + MainTextBox.Caret.LineNumber.ToString() + " : ";
            StatsLabel.Text += MainTextBox.Lines.Count.ToString() + "   Col " + MainTextBox.GetColumn(MainTextBox.CurrentPos).ToString();
            StatsLabel.Text += "   Sel " + MainTextBox.Selection.Start.ToString();
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (DoIt)
            {
                string NewText = ((TextBox)sender).Text;
                NewText = DS_Game_Maker.DSGMlib.ResurrectResourceName(NewText);
                short LengthDifference = (short)(((TextBox)sender).Text.Length - NewText.Length);
                byte BackupCaret = (byte)NameTextBox.SelectionStart;
                ((TextBox)sender).Text = NewText;
                NameTextBox.SelectionStart = BackupCaret - LengthDifference;
            }
            else
            {
                DoIt = true;
            }
        }

        private void MainTextBox_LineStatCaller(object sender, EventArgs e)
        {
            UpdateLineStats();
        }

        private void MainTextBox_KeyUp(object sender, EventArgs e)
        {
            // Dim TheLine As String = MainTextBox.Lines.Current.Text.Replace(vbcrlf, String.Empty)
            // If TheLine.Length > 0 Then
            // For i As Byte = 0 To 200
            // If TheLine(0).ToString = " " Then TheLine = TheLine.Substring(1) Else Exit For
            // Next
            // Else
            // WhatsDone = String.Empty
            // UpdateFunctionAssist()
            // Exit Sub
            // End If
            // If TheLine.StartsWith("If Not ") Then TheLine = TheLine.Substring(7)
            // If TheLine.StartsWith("If ") Then TheLine = TheLine.Substring(3)
            // If TheLine.Contains("(") Then
            // 'Bracketed off, so remember what they did.
            // WhatsDone = TheLine.Substring(0, TheLine.LastIndexOf("("))
            // UpdateFunctionAssist()
            // End If
            // FunctionsList.Items.Clear()
            // For Each X As String In FunctionNames
            // If TheLine.Length > 0 Then
            // If X.StartsWith(TheLine) Then FunctionsList.Items.Add(X)
            // Else
            // FunctionsList.Items.Add(X)
            // End If
            // Next
            // If FunctionsList.Items.Count > 0 Then
            // FunctionsList.SelectedIndex = 0
            // Else
            // FunctionDescriptionLabel.Text = String.Empty
            // End If
        }

        public void TidyUp()
        {
            ArgumentNames.Clear();
            ArgumentTypes.Clear();
            ArgumentsList.Items.Clear();
        }

        public void ArrayToControl()
        {
            ArgumentsList.Items.Clear();
            if (ArgumentNames.Count > 0)
            {
                for (byte P = 0, loopTo = (byte)(ArgumentNames.Count - 1); P <= loopTo; P++)
                    ArgumentsList.Items.Add(P);
            }
        }

        private void LoadInButton_Click(object sender, EventArgs e)
        {
            byte MsgResponse = (byte)MessageBox.Show("Importing a Script will erase and replace the current code." + Constants.vbCrLf + Constants.vbCrLf + "Would you like to Continue?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (!(MsgResponse == (int)MsgBoxResult.Yes))
                return;
            string Response = DS_Game_Maker.DSGMlib.OpenFile(string.Empty, "Dynamic Basic Files|*.dbas");
            if (Response.Length == 0)
                return;
            string Content = DS_Game_Maker.DSGMlib.PathToString(Response);
            string FinalContent = string.Empty;
            TidyUp();
            foreach (string X_ in DS_Game_Maker.DSGMlib.StringToLines(Content))
            {
                string X = X_;
                if (X.StartsWith("SCRIPTARG "))
                {
                    X = X.Substring(10);
                    string TheName = X.Substring(0, X.IndexOf(","));
                    string TheType = X.Substring(X.IndexOf(",") + 1);
                    ArgumentNames.Add(TheName);
                    ArgumentTypes.Add(TheType);
                }
                else
                {
                    FinalContent += X + Constants.vbCrLf;
                }
            }
            ArrayToControl();
            // If FinalContent.Length > 0 Then FinalContent = FinalContent.Substring(0, FinalContent.Length - 1)
            MainTextBox.Text = FinalContent;
        }

        private void SaveOutButton_Click(object sender, EventArgs e)
        {
            string Response = DS_Game_Maker.DSGMlib.SaveFile(string.Empty, "Dynamic Basic Files|*.dbas", ScriptName + ".dbas");
            if (Response.Length == 0)
                return;
            string ToWrite = MainTextBox.Text + Constants.vbCrLf;
            for (byte P = 0, loopTo = (byte)(ArgumentNames.Count - 1); P <= loopTo; P++)
                ToWrite += "SCRIPTARG " + ArgumentNames[P] + "," + ArgumentTypes[P] + Constants.vbCrLf;
            if (ToWrite.Length > 0)
                ToWrite = ToWrite.Substring(0, ToWrite.Length - 1);
            File.WriteAllText(Response, ToWrite);
        }

        // Private Sub PreviewCButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        // Dim ArgumentsString As String = String.Empty
        // Dim ArgumentTypesString As String = String.Empty
        // If ArgumentNames.Count > 0 Then
        // For P As Byte = 0 To ArgumentNames.Count - 1
        // ArgumentsString += ArgumentNames(P) + ","
        // ArgumentTypesString += ArgumentTypes(P) + ","
        // Next
        // ArgumentsString = ArgumentsString.Substring(0, ArgumentsString.Length - 1)
        // ArgumentTypesString = ArgumentTypesString.Substring(0, ArgumentTypesString.Length - 1)
        // End If
        // For Each X As Form In MainForm.MdiChildren
        // If Not X.Text = "Outputted C Preview for " + ScriptName Then Continue For
        // For Each SC As Control In X.Controls
        // If Not SC.Name = "MainTextBox" Then Continue For
        // If ParseDBASChecker.Checked Then
        // DirectCast(SC, ScintillaNet.Scintilla).Text = ScriptParseFromContent(ScriptName, MainTextBox.Text, ArgumentsString, ArgumentTypesString, True, False)
        // Else
        // DirectCast(SC, ScintillaNet.Scintilla).Text = ScriptParseFromContent(ScriptName, MainTextBox.Text, ArgumentsString, ArgumentTypesString, True, False)
        // End If
        // Next
        // X.Focus()
        // Exit Sub
        // Next
        // Dim CodeForm As New EditCode
        // With CodeForm
        // .Text = "Outputted C Preview for " + ScriptName
        // .ReturnableCode = ScriptParseFromContent(ScriptName, MainTextBox.Text, ArgumentsString, ArgumentTypesString, True, False)
        // .StartPosition = FormStartPosition.WindowsDefaultLocation
        // .CodeMode = CodeMode.C
        // End With
        // ShowInternalForm(CodeForm)
        // End Sub

        private void InsertIntoCodeButton_Click(object sender, EventArgs e)
        {
            if (ArgumentsList.SelectedIndices.Count == 0)
                return;
            // Dim BackupPosition = MainTextBox.Caret.Position + ArgumentsListBox.Text.Length
            MainTextBox.InsertText(ArgumentNames[ArgumentsList.SelectedIndex]);
            MainTextBox.Focus();
            // MainTextBox.Caret.Position = BackupPosition
        }

        private void AddArgumentButton_Click(object sender, EventArgs e)
        {
            var ArgumentForm = new DS_Game_Maker.Argument();
            ArgumentForm.ArgumentName = string.Empty;
            ArgumentForm.ArgumentType = "Integer";
            ArgumentForm.Text = "Add Argument";
            ArgumentForm.IsAction = false;
            if (!(ArgumentForm.ShowDialog() == DialogResult.OK))
                return;
            if (ArgumentForm.ArgumentName.Length == 0)
                return;
            string NewName = ArgumentForm.ArgumentName;
            string NewType = ArgumentForm.ArgumentType;
            ArgumentForm.Dispose();
            if (!DS_Game_Maker.DSGMlib.ValidateSomething(NewName, (byte)DS_Game_Maker.DSGMlib.ValidateLevel.Tight))
            {
                DS_Game_Maker.DSGMlib.MsgWarn("You must enter a valid name for the new Argument.");
                return;
            }
            if (!DS_Game_Maker.DSGMlib.ValidateSomething(NewName, (byte)DS_Game_Maker.DSGMlib.ValidateLevel.NumberStart))
            {
                DS_Game_Maker.DSGMlib.MsgWarn("An Argument name may not start with a number.");
                return;
            }
            bool AlreadyDone = false;
            foreach (string X in ArgumentNames)
            {
                if ((X ?? "") == (NewName ?? ""))
                    AlreadyDone = true;
            }
            if (AlreadyDone)
            {
                DS_Game_Maker.DSGMlib.MsgError("There is already an Argument called '" + NewName + "'." + Constants.vbCrLf + Constants.vbCrLf + "You must choose another name.");
                return;
            }
            ArgumentNames.Add(NewName);
            ArgumentTypes.Add(NewType);
            ArrayToControl();
        }

        private void EditArgumentButton_Click(object sender, EventArgs e)
        {
            if (ArgumentsList.SelectedIndices.Count == 0)
                return;
            byte ID = (byte)ArgumentsList.SelectedIndex;
            var ArgumentForm = new DS_Game_Maker.Argument();
            ArgumentForm.ArgumentName = ArgumentNames[ID];
            ArgumentForm.ArgumentType = ArgumentTypes[ID];
            ArgumentForm.Text = "Edit Argument";
            ArgumentForm.IsAction = false;
            if (!(ArgumentForm.ShowDialog() == DialogResult.OK))
                return;
            if (ArgumentForm.ArgumentName.Length == 0)
                return;
            string NewName = ArgumentForm.ArgumentName;
            string NewType = ArgumentForm.ArgumentType;
            ArgumentForm.Dispose();
            if (!DS_Game_Maker.DSGMlib.ValidateSomething(NewName, (byte)DS_Game_Maker.DSGMlib.ValidateLevel.Tight))
            {
                DS_Game_Maker.DSGMlib.MsgWarn("You must enter a valid name for the Argument.");
                return;
            }
            if (!((NewName ?? "") == (ArgumentNames[ID] ?? "")))
            {
                bool AlreadyDone = false;
                foreach (string X in ArgumentNames)
                {
                    if ((X ?? "") == (NewName ?? ""))
                        AlreadyDone = true;
                }
                if (AlreadyDone)
                {
                    DS_Game_Maker.DSGMlib.MsgError("There is already an Argument called '" + NewName + "'." + Constants.vbCrLf + Constants.vbCrLf + "You must choose another name.");
                    return;
                }
            }
            ArgumentNames[ID] = NewName;
            ArgumentTypes[ID] = NewType;
            ArgumentsList.Refresh();
            DS_Game_Maker.DSGMlib.MsgInfo(Application.ProductName + " cannot update your code to use the new Argument name." + Constants.vbCrLf + Constants.vbCrLf + "You must do this yourself.");
        }

        private void DeleteArgumentButton_Click(object sender, EventArgs e)
        {
            if (ArgumentsList.SelectedIndices.Count == 0)
                return;
            ArgumentNames.RemoveAt(ArgumentsList.SelectedIndex);
            ArgumentTypes.RemoveAt(ArgumentsList.SelectedIndex);
            ArrayToControl();
            // UX?
            // MsgInfo("You must update your code so that it no longer uses or references the deleted argument.")
        }

        private void MainTextBox_CharAdded(object sender, ScintillaNet.CharAddedEventArgs e)
        {
            if (!(e.Ch == '\r'))
                return;
            ScintillaNet.Scintilla argTheControl = (ScintillaNet.Scintilla)sender;
            DS_Game_Maker.DSGMlib.IntelliSense(ref argTheControl);
            sender = argTheControl;
            // Dim pos As Int32 = MainTextBox.NativeInterface.GetCurrentPos()
            // Dim length As Int32 = pos - MainTextBox.NativeInterface.WordStartPosition(pos, True)
            // MainTextBox.AutoComplete.Show(length)
        }

        private void ArgumentsList_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            bool IsSelected = false;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                IsSelected = true;
            e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            string TheName = ArgumentNames[e.Index];
            string TheType = ArgumentTypes[e.Index];
            var TF = new Font("Tahoma", 11f, FontStyle.Regular, GraphicsUnit.Pixel);
            SizeF localMeasureString() { int argcharactersFitted = TheType.Length; int arglinesFilled = 1; var ret = e.Graphics.MeasureString(TheType, TF, e.Bounds.Size, StringFormat.GenericDefault, out argcharactersFitted, out arglinesFilled); return ret; }

            byte TW = (byte)Math.Round(localMeasureString().Width);
            if (IsSelected)
            {
                var x = new LinearGradientBrush(new Rectangle(0, e.Bounds.X, e.Bounds.Width, 16), Color.FromArgb(64, 64, 64), Color.Black, LinearGradientMode.Vertical);
                e.Graphics.FillRectangle(x, e.Bounds);
                e.Graphics.DrawString(TheName, TF, Brushes.White, 16f, e.Bounds.Y + 1);
                e.Graphics.DrawString(TheType, TF, Brushes.LightGray, e.Bounds.Width - TW - 3, e.Bounds.Y + 1);
            }
            else
            {
                e.Graphics.DrawString(TheName, TF, Brushes.Black, 16f, e.Bounds.Y + 1);
                e.Graphics.DrawString(TheType, TF, Brushes.LightGray, e.Bounds.Width - TW - 3, e.Bounds.Y + 1);
            }
            e.Graphics.DrawImageUnscaled(Properties.Resources.ArgumentIcon, new Point(0, e.Bounds.Y));
        }

        private void ArgumentsList_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 16;
        }

    }
}