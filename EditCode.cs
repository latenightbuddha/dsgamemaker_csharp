using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace DS_Game_Maker
{
    public partial class EditCode
    {

        public string ReturnableCode = string.Empty;
        public byte CodeMode = 0;
        public bool ImportExport = false;

        public EditCode()
        {
            InitializeComponent();
        }

        private void EditCode_Load(object sender, EventArgs e)
        {
            MainToolStrip.Renderer = new DS_Game_Maker.clsToolstripRenderer();
            LoadInButton.Enabled = ImportExport;
            SaveOutButton.Enabled = ImportExport;
            string NewCode = ReturnableCode;
            if (!(CodeMode == 1))
            {
                NewCode = NewCode.Replace("<br|>", Constants.vbCrLf).Replace("<com>", ",").Replace("<sem>", ";");
            }
            MainTextBox.Text = NewCode;
            UpdateLineStats();
        }

        public void UpdateLineStats()
        {
            InfoLabel.Text = "Ln " + MainTextBox.Caret.LineNumber.ToString() + " : ";
            InfoLabel.Text += MainTextBox.Lines.Count.ToString() + "   Col " + MainTextBox.GetColumn(MainTextBox.CurrentPos).ToString();
            InfoLabel.Text += "   Sel " + MainTextBox.Selection.Start.ToString();
        }

        private void MainTextBox_KeyDown(object sender, EventArgs e)
        {
            UpdateLineStats();
        }

        private void DAcceptButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            ReturnableCode = MainTextBox.Text.Replace(Constants.vbCrLf, "<br|>").Replace(",", "<com>").Replace(";", "<sem>");
            DialogResult = DialogResult.OK;
            Close();
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
            foreach (string X in DS_Game_Maker.DSGMlib.StringToLines(Content))
            {
                if (X.StartsWith("SCRIPTARG "))
                    continue;
                FinalContent += X;
            }
            // If FinalContent.Length > 0 Then FinalContent = FinalContent.Substring(0, FinalContent.Length - 1)
            MainTextBox.Text = FinalContent;
        }

        private void SaveOutButton_Click(object sender, EventArgs e)
        {
            string Response = DS_Game_Maker.DSGMlib.SaveFile(string.Empty, "Dynamic Basic Files|*.dbas", "Expoted Code.dbas");
            if (Response.Length == 0)
                return;
            string ToWrite = MainTextBox.Text;
            // If ToWrite.Length > 0 Then ToWrite = ToWrite.Substring(0, ToWrite.Length - 1)
            System.IO.File.WriteAllText(Response, ToWrite);
        }

        private void MainTextBox_CharAdded(object sender, ScintillaNET.CharAddedEventArgs e)
        {
            if (!(e.Ch == '\r'))
                return;
            ScintillaNET.Scintilla argTheControl = (ScintillaNET.Scintilla)sender;
            DS_Game_Maker.DSGMlib.IntelliSense(ref argTheControl);
            sender = argTheControl;
        }

    }
}