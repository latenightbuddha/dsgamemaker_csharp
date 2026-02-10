using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    public partial class FoundInScripts
    {

        public List<string> ScriptNames = new List<string>();
        public List<short> ScriptLines = new List<short>();
        public List<short> ScriptPositions = new List<short>();
        public string Term = string.Empty;

        public FoundInScripts()
        {
            InitializeComponent();
        }

        private void FoundInScripts_Load(object sender, EventArgs e)
        {
            MainToolStrip.Renderer = new DS_Game_Maker.clsToolstripRenderer();
            if (Term.Length == 0)
                return;
            FindInScriptsDoIt();
        }

        public void FindInScriptsDoIt()
        {
            short TermLength = (short)Term.Length;
            MainListBox.Items.Clear();
            ScriptNames.Clear();
            ScriptLines.Clear();
            ScriptPositions.Clear();
            short DOn = 0;
            foreach (string X in DS_Game_Maker.DSGMlib.GetXDSFilter("SCRIPT "))
            {
                DOn = 0;
                string ScriptName = X.Substring(7);
                ScriptName = ScriptName.Substring(0, ScriptName.LastIndexOf(","));
                foreach (string ThisLine in DS_Game_Maker.DSGMlib.StringToLines(DS_Game_Maker.DSGMlib.PathToString(DS_Game_Maker.SessionsLib.SessionPath + @"Scripts\" + ScriptName + ".dbas")))
                {
                    short FullLength = (short)ThisLine.Length;
                    // MsgError("Line: " + ThisLine + vbcrlf + vbcrlf + "Length: " + FullLength.ToString)
                    if (TermLength > FullLength)
                    {
                        DOn = (short)(DOn + 1);
                        continue;
                    }
                    if ((ThisLine ?? "") == (Term ?? ""))
                    {
                        ScriptNames.Add(ScriptName);
                        ScriptLines.Add((short)(DOn + 1));
                        ScriptPositions.Add(0);
                        DOn = (short)(DOn + 1);
                        // MsgError("Term equal to line length")
                        continue;
                    }
                    // MsgError("Loop from 0 to " + (FullLength - TermLength).ToString)
                    for (short i = 0, loopTo = (short)(FullLength - TermLength); i <= loopTo; i++)
                    {
                        // MsgError(i.ToString + ": Observing: " + ThisLine.Substring(i))
                        if (ThisLine.Substring(i).StartsWith(Term))
                        {
                            DS_Game_Maker.DSGMlib.MsgError(i.ToString() + " - '" + ThisLine.Substring(i) + "' starts with '" + Term + "' so I'm addin' it");
                            ScriptNames.Add(ScriptName);
                            ScriptLines.Add((short)(DOn + 1));
                            ScriptPositions.Add(i);
                            // MsgError("Added line " + (DOn + 1).ToString)
                            // MsgError("Added pos " + i.ToString)
                        }
                    }
                    DOn = (short)(DOn + 1);
                }
            }
            for (short i = 0, loopTo1 = (short)(ScriptNames.Count - 1); i <= loopTo1; i++)
                MainListBox.Items.Add(ScriptNames[i]);
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            FindInScriptsDoIt();
        }

        private void MainListBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 22;
        }

        private void MainListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            short i = (short)e.Index;
            if (e.Index < 0)
                return;
            bool IsSelected = false;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                IsSelected = true;
            e.Graphics.FillRectangle(Brushes.White, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            if (IsSelected)
                e.Graphics.DrawImage(DS_Game_Maker.My.Resources.Resources.BarBG, new Rectangle(0, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            e.Graphics.DrawImageUnscaled(DS_Game_Maker.My.Resources.Resources.ScriptIcon, new Point(e.Bounds.X + 4, e.Bounds.Y + 3));
            e.Graphics.DrawString(ScriptNames[i] + ": Line " + ScriptLines[i].ToString() + ", Pos: " + ScriptPositions[i].ToString(), new Font("Tahoma", 8f), Brushes.Black, e.Bounds.X + 21, e.Bounds.Y + 4);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ScriptName = ScriptNames[MainListBox.SelectedIndex];
            bool HasDone = false;
            foreach (Form X in DS_Game_Maker.My.MyProject.Forms.MainForm.MdiChildren)
            {
                if (X.Name == "Script" & (X.Text ?? "") == (ScriptName ?? ""))
                {
                    ((DS_Game_Maker.Script)X).GoToLine((short)(ScriptLines[MainListBox.SelectedIndex] - 1), ScriptPositions[MainListBox.SelectedIndex], (short)Term.Length);
                    X.BringToFront();
                    HasDone = true;
                }
            }
            if (!HasDone)
            {
                var P = new DS_Game_Maker.Script();
                P.ScriptName = ScriptName;
                object argInstance = (object)P;
                DS_Game_Maker.DSGMlib.ShowInternalForm(ref argInstance);
                P = (DS_Game_Maker.Script)argInstance;
                P.GoToLine((short)(ScriptLines[MainListBox.SelectedIndex] - 1), ScriptPositions[MainListBox.SelectedIndex], (short)Term.Length);
            }
        }
    }
}