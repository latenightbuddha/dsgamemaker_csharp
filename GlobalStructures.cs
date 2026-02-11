using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace DS_Game_Maker
{

    public partial class GlobalStructures
    {

        private List<string> Structures = new List<string>();
        private List<string> Names = new List<string>();
        private List<string> Types = new List<string>();
        private List<string> Values = new List<string>();
        // Dim CurrentName As String = String.Empty
        private bool AllowChangeOrQuit = true;
        private short OldIndex = 0;
        private bool SaveOnChange = false;

        public GlobalStructures()
        {
            InitializeComponent();
        }
        // Dim InUse As Boolean = False

        private void GlobalStructures_Load(object sender, EventArgs e)
        {
            SetEnablity(false);
            MembersList.Items.Clear();
            Structures.Clear();
            StructuresList.Items.Clear();
            Names.Clear();
            Types.Clear();
            Values.Clear();
            foreach (string X_ in DS_Game_Maker.DSGMlib.GetXDSFilter("STRUCT "))
            {
                string X = X_;
                X = X.Substring(7);
                Structures.Add(X);
                StructuresList.Items.Add(X);
                // For Each Y As String In GetXDSFilter("STRUCTMEMBER " + X + ",")
                // Y = Y.Substring(("STRUCTMEMBER " + X).Length + 1)
                // Names.Add(iGet(Y, 0, ","))
                // Types.Add(iGet(Y, 1, ","))
                // Values.Add(iGet(Y, 2, ","))
                // Next
            }
            NameTextBox.BackColor = Color.FromArgb(64, 64, 64);
            SaveOnChange = false;
            if (StructuresList.Items.Count > 0)
                StructuresList.SelectedIndex = 0;
            AllowChangeOrQuit = true;
            OldIndex = 0;
        }

        private void StructuresList_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 20;
        }

        private void StructuresList_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            bool IsSelected = false;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                IsSelected = true;
            e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            string FinalText = Structures[e.Index];
            var TF = new Font("Tahoma", 11f, FontStyle.Regular, GraphicsUnit.Pixel);
            if (IsSelected)
            {
                var x = new LinearGradientBrush(new Rectangle(0, e.Bounds.X, e.Bounds.Width, 20), Color.FromArgb(64, 64, 64), Color.Black, LinearGradientMode.Vertical);
                e.Graphics.FillRectangle(x, new Rectangle(0, e.Bounds.Y, e.Bounds.Width, 20));
                e.Graphics.DrawString(FinalText, TF, Brushes.White, 20f, e.Bounds.Y + 3);
            }
            else
            {
                e.Graphics.DrawString(FinalText, TF, Brushes.Black, 20f, e.Bounds.Y + 3);
            }
            e.Graphics.DrawImageUnscaled(Properties.Resources.StructureIcon, new Point(4, e.Bounds.Y + 4));
        }

        public void SetEnablity(bool Enabled)
        {
            MembersList.Enabled = Enabled;
            NameTextBox.Enabled = Enabled;
            AddMemberButton.Enabled = Enabled;
            EditMemberButton.Enabled = Enabled;
            DeleteMemberButton.Enabled = Enabled;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            short ToUse = 1;
            for (int i = 1; i <= 1000; i++)
            {
                if (!DS_Game_Maker.DSGMlib.DoesXDSLineExist("STRUCT Structure_" + i.ToString()))
                {
                    ToUse = (short)i;
                    break;
                }
            }
            string TheName = "Structure_" + ToUse.ToString();
            DS_Game_Maker.DSGMlib.XDSAddLine("STRUCT " + TheName);
            DS_Game_Maker.DSGMlib.XDSAddLine("STRUCTMEMBER " + TheName + ",Item_1,Integer,0");
            Structures.Add(TheName);
            SaveOnChange = StructuresList.SelectedIndices.Count == 1;
            StructuresList.Items.Add(TheName);
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (StructuresList.SelectedIndices.Count == 0)
                return;
            string CurrentName = Structures[OldIndex];
            Structures.RemoveAt(OldIndex);
            StructuresList.Items.RemoveAt(OldIndex);
            Names.Clear();
            Types.Clear();
            Values.Clear();
            MembersList.Items.Clear();
            NameTextBox.Text = string.Empty;
            DS_Game_Maker.DSGMlib.XDSRemoveLine("STRUCT " + CurrentName);
            DS_Game_Maker.DSGMlib.XDSRemoveFilter("STRUCTMEMBER " + CurrentName + ",");
            string Message = "You have just deleted '" + CurrentName + "'." + Constants.vbCrLf + Constants.vbCrLf;
            Message += "Be sure to update any logic that uses this Structure.";
            DS_Game_Maker.DSGMlib.MsgInfo(Message);
            SaveOnChange = false;
            if (StructuresList.Items.Count > 0)
            {
                StructuresList.SelectedIndex = 0;
            }
            else
            {
                SetEnablity(false);
            }
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (Structures.Count == 0)
                return;
            if (DS_Game_Maker.DSGMlib.ValidateSomething(NameTextBox.Text, (byte)DS_Game_Maker.DSGMlib.ValidateLevel.Tight))
            {
                NameTextBox.BackColor = Color.FromArgb(64, 64, 64);
                AllowChangeOrQuit = true;
            }
            else
            {
                NameTextBox.BackColor = Color.FromArgb(192, 0, 0);
                AllowChangeOrQuit = false;
            }
        }

        private void GlobalStructures_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowChangeOrQuit)
            {
                e.Cancel = true;
                return;
            }
            if (Structures.Count == 0)
                return;
            string OldName = Structures[OldIndex];
            // Structures(OldIndex) = NameTextBox.Text
            // StructuresList.Items(OldIndex) = NameTextBox.Text
            DS_Game_Maker.DSGMlib.XDSChangeLine("STRUCT " + OldName, "STRUCT " + NameTextBox.Text);
            DS_Game_Maker.DSGMlib.XDSRemoveFilter("STRUCTMEMBER " + OldName + ",");
            if (Names.Count > 0)
            {
                for (byte P = 0, loopTo = (byte)(Names.Count - 1); P <= loopTo; P++)
                {
                    string FS = "STRUCTMEMBER " + NameTextBox.Text + ",";
                    FS += Names[P] + ",";
                    FS += Types[P] + ",";
                    FS += Values[P].Replace(",", "<comma>");
                    DS_Game_Maker.DSGMlib.XDSAddLine(FS);
                }
            }

        }

        private void StructuresList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Structures.Count == 0)
                return;
            if (!AllowChangeOrQuit)
                return;
            short Position = (short)StructuresList.SelectedIndex;
            try
            {
                string X = Structures[Position];
            }
            catch
            {
                return;
            }
            if (SaveOnChange)
            {
                string OldName = Structures[OldIndex];
                Structures[OldIndex] = NameTextBox.Text;
                StructuresList.Items[OldIndex] = NameTextBox.Text;
                DS_Game_Maker.DSGMlib.XDSChangeLine("STRUCT " + OldName, "STRUCT " + NameTextBox.Text);
                DS_Game_Maker.DSGMlib.XDSRemoveFilter("STRUCTMEMBER " + OldName + ",");
                if (Names.Count > 0)
                {
                    for (byte P = 0, loopTo = (byte)(Names.Count - 1); P <= loopTo; P++)
                    {
                        string FS = "STRUCTMEMBER " + NameTextBox.Text + ",";
                        FS += Names[P] + ",";
                        FS += Types[P] + ",";
                        FS += Values[P].Replace(",", "<comma>");
                        DS_Game_Maker.DSGMlib.XDSAddLine(FS);
                    }
                }
                // For Each X As String In GetXDSFilter("STRUCTMEMBER " + OldName + ",")
                // Dim Tach As String = X.Substring(("STRUCTMEMBER ").Length + OldName.Length)
                // XDSChangeLine(X, "STRUCTMEMBER " + NameTextBox.Text + Tach)
                // Next
            }
            string CurrentName = Structures[Position];
            Names.Clear();
            Types.Clear();
            Values.Clear();
            MembersList.Items.Clear();
            foreach (string P_ in DS_Game_Maker.DSGMlib.GetXDSFilter("STRUCTMEMBER " + CurrentName + ","))
            {
                string P = P_;
                P = P.Substring(("STRUCTMEMBER " + CurrentName).Length + 1);
                string Name = P.Substring(0, P.IndexOf(","));
                Names.Add(Name);
                string Type = P.Substring(Name.Length + 1);
                Type = Type.Substring(0, Type.IndexOf(","));
                Types.Add(Type);
                string Value = P.Substring(P.LastIndexOf(",") + 1).Replace("<comma>", ",");
                Values.Add(Value);
                MembersList.Items.Add(string.Empty);
            }
            NameTextBox.Text = CurrentName;
            SetEnablity(true);
            NameTextBox.Focus();
            NameTextBox.SelectionStart = NameTextBox.Text.Length;
            NameTextBox.SelectionLength = 0;
            OldIndex = (short)StructuresList.SelectedIndex;
        }

        private void AddMemberButton_Click(object sender, EventArgs e)
        {
            short ToUse = 1;
            for (int i = 1; i <= 1000; i++)
            {
                if (!Names.Contains("Item_" + i.ToString()))
                {
                    ToUse = (short)i;
                    break;
                }
            }
            string TheName = "Item_" + ToUse.ToString();
            // XDSAddLine("STRUCTMEMBER " + CurrentName + "," + TheName + ",Item_1,Integer,0")
            Names.Add(TheName);
            Types.Add("Integer");
            Values.Add("0");
            MembersList.Items.Add(string.Empty);
        }

        private void EditMemberButton_Click(object sender, EventArgs e)
        {
            if (MembersList.SelectedIndices.Count == 0)
                return;
            byte ID = (byte)MembersList.SelectedIndex;
            {
                var withBlock = Program.Forms.structureItem_Form;
                withBlock.MemberName = Names[ID];
                withBlock.MemberType = Types[ID];
                withBlock.MemberValue = Values[ID];
                withBlock.ShowDialog();
                if (!withBlock.UseData)
                    return;
                Names[ID] = withBlock.MemberName;
                Types[ID] = withBlock.MemberType;
                Values[ID] = withBlock.MemberValue;
            }
            MembersList.Refresh();
        }

        private void DeleteMemberButton_Click(object sender, EventArgs e)
        {
            if (MembersList.SelectedIndices.Count == 0)
                return;
            byte TI = (byte)MembersList.SelectedIndex;
            Names.RemoveAt(TI);
            Types.RemoveAt(TI);
            Values.RemoveAt(TI);
            MembersList.Items.RemoveAt(TI);
        }

        private void MembersList_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 16;
        }

        private void MembersList_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            bool IsSelected = false;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                IsSelected = true;
            e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            string TheName = Names[e.Index];
            string TheType = Types[e.Index];
            string TheValue = Values[e.Index];
            var TF = new Font("Tahoma", 11f, FontStyle.Regular, GraphicsUnit.Pixel);
            if (IsSelected)
            {
                var x = new LinearGradientBrush(new Rectangle(0, e.Bounds.X, e.Bounds.Width, 16), Color.FromArgb(64, 64, 64), Color.Black, LinearGradientMode.Vertical);
                e.Graphics.FillRectangle(x, e.Bounds);
                e.Graphics.DrawString(TheName, TF, Brushes.White, 16f, e.Bounds.Y + 1);
                e.Graphics.DrawString(TheType, TF, Brushes.LightGray, 66f, e.Bounds.Y + 1);
                e.Graphics.DrawString(TheValue, TF, Brushes.LightGray, 138f, e.Bounds.Y + 1);
            }
            else
            {
                e.Graphics.DrawString(TheName, TF, Brushes.DarkGray, 16f, e.Bounds.Y + 1);
                e.Graphics.DrawString(TheType, TF, Brushes.DarkGray, 66f, e.Bounds.Y + 1);
                e.Graphics.DrawString(TheValue, TF, Brushes.DarkGray, 138f, e.Bounds.Y + 1);
            }
            e.Graphics.DrawImageUnscaled(Properties.Resources.ArgumentIcon, new Point(0, e.Bounds.Y));
        }

    }
}