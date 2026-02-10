using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace DS_Game_Maker
{
    public partial class GlobalArrays
    {

        private byte SN = 200;

        public GlobalArrays()
        {
            InitializeComponent();
        }

        // Number 0
        // TrueFalse 1

        public void ClearShizzle()
        {
            SN = 200;
            NameTextBox.Text = string.Empty;
            ValuesListBox.Items.Clear();
            TypeDropper.Text = "Number";
            MainGroupBox.Text = "<No Array Selected>";
            SaveButton.Enabled = false;
            AddValueButton.Enabled = false;
            EditValueButton.Enabled = false;
            DeleteValueButton.Enabled = false;
        }

        private void Globals_Load(object sender, EventArgs e)
        {
            MainToolStrip.Renderer = new DS_Game_Maker.clsToolstripRenderer();
            var TMPList = new ImageList();
            TMPList.ImageSize = new Size(16, 16);
            TMPList.ColorDepth = ColorDepth.Depth32Bit;
            TMPList.Images.Add(DS_Game_Maker.My.Resources.Resources.ArrayIcon);
            ArraysList.ImageList = TMPList;
            ArraysList.Nodes.Clear();
            foreach (string X_ in DS_Game_Maker.DSGMlib.GetXDSFilter("ARRAY "))
            {
                string X = X_;
                X = X.Substring(6);
                string ArrayName = DS_Game_Maker.DSGMlib.iGet(X, (byte)0, ",");
                ArraysList.Nodes.Add(string.Empty, ArrayName, 0);
            }
            ClearShizzle();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (SN == 200)
                return;
            string ArrayName = ArraysList.Nodes[SN].Text;
            DS_Game_Maker.DSGMlib.XDSRemoveLine(DS_Game_Maker.DSGMlib.GetXDSLine("ARRAY " + ArrayName + ","));
            ArraysList.Nodes.Remove(ArraysList.Nodes[SN]);
            string Message = "You have just deleted '" + ArrayName + "'." + Constants.vbCrLf + Constants.vbCrLf;
            Message += "Be sure to update any logic that uses this Array.";
            DS_Game_Maker.DSGMlib.MsgInfo(Message);
            ClearShizzle();
        }

        private void VariablesList_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SN = (byte)e.Node.Index;
            NameTextBox.Text = e.Node.Text;
            MainGroupBox.Text = e.Node.Text;
            string XDSLine = DS_Game_Maker.DSGMlib.GetXDSLine("ARRAY " + e.Node.Text + ",");
            byte Type = Convert.ToByte(DS_Game_Maker.DSGMlib.iGet(XDSLine, (byte)1, ","));
            TypeDropper.Text = "Number";
            if (Type == 1)
                TypeDropper.Text = "TrueFalse";
            string ValuesString = DS_Game_Maker.DSGMlib.iGet(XDSLine, (byte)2, ",");
            ValuesListBox.Items.Clear();
            if (ValuesString.Length > 0)
            {
                for (short i = 0, loopTo = DS_Game_Maker.DSGMlib.HowManyChar(ValuesString, ";"); i <= loopTo; i++)
                {
                    string Value = DS_Game_Maker.DSGMlib.iGet(ValuesString, (byte)i, ";");
                    ValuesListBox.Items.Add(Value);
                }
            }
            AddValueButton.Enabled = true;
            EditValueButton.Enabled = true;
            DeleteValueButton.Enabled = true;
            SaveButton.Enabled = true;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // If SN = 200 Then MsgWarn("No Variable is selected.") : Exit Sub
            if (!DS_Game_Maker.DSGMlib.ValidateSomething(NameTextBox.Text, (byte)DS_Game_Maker.DSGMlib.ValidateLevel.Tight))
            {
                DS_Game_Maker.DSGMlib.MsgWarn("You must provide a valid name.");
                return;
            }
            if (TypeDropper.Text.Length == 0)
            {
                DS_Game_Maker.DSGMlib.MsgWarn("You must select a Type.");
                return;
            }
            if (!(TypeDropper.Text == "TrueFalse") & !(TypeDropper.Text == "Number"))
            {
                DS_Game_Maker.DSGMlib.MsgWarn("A Variable can only be a 'TrueFalse' or a 'Number'.");
                return;
            }
            if (TypeDropper.Text == "TrueFalse")
            {
                bool AllTrueFalse = true;
                // Dim ThisTime As Boolean = False
                foreach (string X in ValuesListBox.Items)
                {
                    if (X.ToLower() == "true" | X.ToLower() == "false")
                    {
                    }
                    else
                    {
                        AllTrueFalse = false;
                    }
                }
                if (!AllTrueFalse)
                {
                    DS_Game_Maker.DSGMlib.MsgWarn("With a TrueFalse Array, all Values must be 'true' or 'false'.");
                    return;
                }
            }
            byte TheType = (byte)(TypeDropper.Text == "TrueFalse" ? 1 : 0);
            string ValuesString = string.Empty;
            foreach (string X in ValuesListBox.Items)
                ValuesString += X + ";";
            if (ValuesString.Length > 0)
                ValuesString = ValuesString.Substring(0, ValuesString.Length - 1);
            DS_Game_Maker.DSGMlib.XDSChangeLine(DS_Game_Maker.DSGMlib.GetXDSLine("ARRAY " + ArraysList.Nodes[SN].Text + ","), "ARRAY " + NameTextBox.Text + "," + TheType.ToString() + "," + ValuesString);
            ArraysList.SelectedNode.Text = NameTextBox.Text;
            MainGroupBox.Text = NameTextBox.Text;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            short ToUse = 1;
            for (int i = 1; i <= 1000; i++)
            {
                if (DS_Game_Maker.DSGMlib.GetXDSFilter("ARRAY Array_" + i.ToString() + ",").Length == 0)
                {
                    ToUse = (short)i;
                    break;
                }
            }
            DS_Game_Maker.DSGMlib.XDSAddLine("ARRAY Array_" + ToUse.ToString() + ",0,");
            ArraysList.Nodes.Add(string.Empty, "Array_" + ToUse.ToString(), 0);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DeleteValueButton_Click(object sender, EventArgs e)
        {
            if (ValuesListBox.SelectedIndices.Count == 0)
                return;
            ValuesListBox.Items.RemoveAt(ValuesListBox.SelectedIndices[0]);
        }

        private void AddValueButton_Click(object sender, EventArgs e)
        {
            if (TypeDropper.SelectedIndex == 0)
                ValuesListBox.Items.Add("0");
            else
                ValuesListBox.Items.Add("true");
        }

        private void EditValueButton_Click(object sender, EventArgs e)
        {
            if (ValuesListBox.SelectedIndices.Count == 0)
                return;
            string Response = DS_Game_Maker.DSGMlib.GetInput("Please enter a new Value", ValuesListBox.Text, (byte)DS_Game_Maker.DSGMlib.ValidateLevel.None);
            if (Response.Length == 0)
                return;
            ValuesListBox.Items[ValuesListBox.SelectedIndices[0]] = Response;
        }
    }
}