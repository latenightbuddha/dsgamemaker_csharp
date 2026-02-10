using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace DS_Game_Maker
{

    public partial class GlobalVariables
    {

        private List<string> Variables = new List<string>();
        private List<string> MyVariableTypes = new List<string>();
        private List<string> VariableValues = new List<string>();
        private string CurrentName = string.Empty;
        private bool AllowClose = true;

        public GlobalVariables()
        {
            InitializeComponent();
        }

        private void Globals_Load(object sender, EventArgs e)
        {
            SetEnablity(false);
            TypeList.Items.Clear();
            foreach (string X in DS_Game_Maker.ScriptsLib.VariableTypes)
                TypeList.Items.Add(X);
            foreach (string P in DS_Game_Maker.DSGMlib.GetXDSFilter("STRUCT "))
                TypeList.Items.Add(P.Substring(7));
            Variables.Clear();
            MyVariableTypes.Clear();
            VariableValues.Clear();
            foreach (string X_ in DS_Game_Maker.DSGMlib.GetXDSFilter("GLOBAL "))
            {
                string X = X_;
                X = X.Substring(7);
                Variables.Add(DS_Game_Maker.DSGMlib.iGet(X, (byte)0, ","));
                MyVariableTypes.Add(DS_Game_Maker.DSGMlib.iGet(X, (byte)1, ","));
                VariableValues.Add(DS_Game_Maker.DSGMlib.iGet(X, (byte)2, ","));
            }
            VariablesList.Items.Clear();
            if (Variables.Count > 0)
            {
                for (short i = 0, loopTo = (short)(Variables.Count - 1); i <= loopTo; i++)
                    VariablesList.Items.Add(Variables[i]);
            }
            NameTextBox.BackColor = Color.FromArgb(64, 64, 64);
            ValueTextBox.Text = string.Empty;
            if (VariablesList.Items.Count > 0)
                VariablesList.SelectedIndex = 0;
            AllowClose = true;
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (CurrentName.Length == 0)
                return;
            short DOn = 0;
            for (short i = 0, loopTo = (short)(VariablesList.Items.Count - 1); i <= loopTo; i++)
            {
                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(VariablesList.Items[i], CurrentName, false)))
                {
                    DOn = i;
                    break;
                }
            }
            // For Each X As String In VariablesList.Items
            // If X = CurrentName Then Exit For
            // DOn += 1
            // Next
            VariablesList.Items.RemoveAt(DOn);
            Variables.RemoveAt(DOn);
            MyVariableTypes.RemoveAt(DOn);
            VariableValues.RemoveAt(DOn);
            NameTextBox.Text = string.Empty;
            ValueTextBox.Text = string.Empty;
            string Message = "You have just deleted '" + CurrentName + "'." + Constants.vbCrLf + Constants.vbCrLf;
            if (VariablesList.Items.Count > 0)
            {
                VariablesList.SelectedIndex = 0;
            }
            else
            {
                SetEnablity(false);
            }
            Message += "Be sure to update any logic that uses this Variable.";
            DS_Game_Maker.DSGMlib.MsgInfo(Message);
            CurrentName = string.Empty;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            short ToUse = 1;
            for (int i = 1; i <= 100; i++)
            {
                if (!Variables.Contains("Variable_" + i.ToString()))
                {
                    ToUse = (short)i;
                    break;
                }
            }
            Variables.Add("Variable_" + ToUse.ToString());
            MyVariableTypes.Add("Integer");
            VariableValues.Add("0");
            VariablesList.Items.Add("Variable_" + ToUse.ToString());
            VariablesList.SelectedIndex = VariablesList.Items.Count - 1;
        }

        private void VariablesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Variables.Count == 0)
                return;
            short Position = (short)VariablesList.SelectedIndex;
            try
            {
                string X = MyVariableTypes[Position];
            }
            catch
            {
                return;
            }
            TypeList.Text = MyVariableTypes[Position];
            ValueTextBox.Text = VariableValues[Position];
            CurrentName = Variables[Position];
            NameTextBox.Text = CurrentName;
            SetEnablity(true);
            NameTextBox.Focus();
            NameTextBox.SelectionStart = NameTextBox.Text.Length;
            NameTextBox.SelectionLength = 0;
        }

        private void TypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Variables.Count == 0)
                return;
            string Returnable = "<No description>";
            TypeTitleLabel.Text = TypeList.Text;
            switch (TypeList.Text ?? "")
            {
                case "Integer":
                    {
                        Returnable = "An integer is a numerical value that does not have a decimal point.";
                        break;
                    }
                case "Boolean":
                    {
                        Returnable = "A boolean holds one of two values: true or false.";
                        break;
                    }
                case "Signed Byte":
                    {
                        Returnable = "A numerical value ranging from -127 to 127.";
                        break;
                    }
                case "Unsigned Byte":
                    {
                        Returnable = "A numerical value ranging from 0 to 255.";
                        break;
                    }
                case "Float":
                    {
                        Returnable = "A float is a numerical value which may possess a decimal point.";
                        break;
                    }
                case "String":
                    {
                        Returnable = "A word or phrase; a linear sequence of symbols (characters).";
                        break;
                    }
            }
            TypeDescriptionLabel.Text = Returnable;
            MyVariableTypes[VariablesList.SelectedIndex] = TypeList.Text;
        }

        public void SetEnablity(bool Enabled)
        {
            TypeList.Enabled = Enabled;
            ValueTextBox.Enabled = Enabled;
            NameTextBox.Enabled = Enabled;
            TypeTitleLabel.Enabled = Enabled;
            TypeDescriptionLabel.Enabled = Enabled;
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (Variables.Count == 0)
                return;
            if (DS_Game_Maker.DSGMlib.ValidateSomething(NameTextBox.Text, (byte)DS_Game_Maker.DSGMlib.ValidateLevel.Tight))
            {
                NameTextBox.BackColor = Color.FromArgb(64, 64, 64);
                Variables[VariablesList.SelectedIndex] = NameTextBox.Text;
                VariablesList.Items[VariablesList.SelectedIndex] = NameTextBox.Text;
                AllowClose = true;
            }
            else
            {
                NameTextBox.BackColor = Color.FromArgb(192, 0, 0);
                AllowClose = false;
            }
        }

        private void ValueTextBox_TextChanged(object sender, EventArgs e)
        {
            if (Variables.Count == 0)
                return;
            try
            {
                VariableValues[VariablesList.SelectedIndex] = ValueTextBox.Text;
            }
            catch
            {
            }
        }

        private void VariablesList_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 20;
        }

        private void VariablesList_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            bool IsSelected = false;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                IsSelected = true;
            e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            string FinalText = Variables[e.Index];
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
            e.Graphics.DrawImageUnscaled(DS_Game_Maker.My.Resources.Resources.VariableIcon, new Point(4, e.Bounds.Y + 4));
        }

        private void TypeList_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 16;
        }

        private void TypeList_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            bool IsSelected = false;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                IsSelected = true;
            e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            string FinalText = Conversions.ToString(TypeList.Items[e.Index]);
            if (IsSelected)
            {
                var x = new LinearGradientBrush(new Rectangle(0, e.Bounds.X, e.Bounds.Width, e.Bounds.Height), Color.FromArgb(192, 192, 192), Color.FromArgb(80, 80, 80), LinearGradientMode.Vertical);
                e.Graphics.FillRectangle(x, new Rectangle(0, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
                e.Graphics.DrawString(FinalText, new Font("Tahoma", 11f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.White, 3f, e.Bounds.Y + 1);
            }
            else
            {
                e.Graphics.DrawString(FinalText, new Font("Tahoma", 11f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Black, 3f, e.Bounds.Y + 1);
            }
        }

        private void GlobalVariables_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
                return;
            }
            DS_Game_Maker.DSGMlib.XDSRemoveFilter("GLOBAL ");
            for (short i = 0, loopTo = (short)(Variables.Count - 1); i <= loopTo; i++)
            {
                string NewLine = "GLOBAL " + Variables[i] + ",";
                NewLine += MyVariableTypes[i] + ",";
                NewLine += VariableValues[i];
                DS_Game_Maker.DSGMlib.XDSAddLine(NewLine);
            }
        }
    }
}