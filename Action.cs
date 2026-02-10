using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace DS_Game_Maker
{
    public partial class Action
    {

        public bool UseData = false;
        public string ActionName;
        public string ArgumentString;
        public string AppliesTo;

        public Action()
        {
            InitializeComponent();
        }

        public void InstancesBugger()
        {
            DS_Game_Maker.DSGMlib.MsgWarn("The instance numbers must be separated by single spaces. You may also the IRandom function." + Constants.vbCrLf + Constants.vbCrLf + "For more information, see the manual.");
            InstancesTextBox.Focus();
        }

        private void DOkayButton_Click(object sender, EventArgs e)
        {
            if (ControlsPanel.Visible)
            {
                bool IsOneEmpty = false;
                byte FirstEmpty = 100;
                byte DOn = 0;
                foreach (Control X in ControlsPanel.Controls)
                {
                    if (X.Name.StartsWith("Unrestricted"))
                    {
                        DOn = (byte)(DOn + 1);
                        continue;
                    }
                    if (X.Text.Length == 0)
                    {
                        if (!IsOneEmpty)
                            FirstEmpty = DOn;
                        IsOneEmpty = true;
                    }
                    DOn = (byte)(DOn + 1);
                }
                if (IsOneEmpty)
                {
                    DS_Game_Maker.DSGMlib.MsgWarn("You must put something in every Argument box or selector.");
                    ControlsPanel.Controls[DOn - 1].Focus();
                    return;
                }
            }
            ArgumentString = string.Empty;
            if (ThisRadioButton.Checked)
            {
                AppliesTo = "this";
            }
            else if (InstancesOfRadioButton.Checked)
            {
                AppliesTo = InstancesOfDropper.Text;
            }
            else if (InstancesRadioButton.Checked)
            {
                AppliesTo = InstancesTextBox.Text.Replace(",", "<comma>");
                // If AppliesTo.Length = 0 Or Not AppliesTo.Contains(" ") Then
                // InstancesBugger()
                // Exit Sub
                // End If
                // Dim OverallValid As Boolean = True
                // For X As Byte = 0 To HowManyChar(AppliesTo, " ")
                // If iGet(AppliesTo, X, " ").ToString.StartsWith("IRandom(") Then Continue For
                // Dim AllNumCharsAlright As Boolean = True
                // For Each Y As String In iGet(AppliesTo, X, " ")
                // Dim IsANumber As Boolean = False
                // For Each C As String In Numbers
                // If Y = C Then IsANumber = True
                // Next
                // If Not IsANumber Then AllNumCharsAlright = False
                // Next
                // If AllNumCharsAlright Then
                // Dim ThatNumber As Int16 = Convert.ToInt16(iGet(AppliesTo, X, " "))
                // If ThatNumber > 255 Then OverallValid = False
                // Else
                // OverallValid = False
                // End If
                // Next
                // If Not OverallValid Then
                // InstancesBugger()
                // Exit Sub
                // End If
            }
            UseData = true;
            if (ControlsPanel.Visible)
            {
                foreach (Control X in ControlsPanel.Controls)
                {
                    string TheText = X.Text;
                    TheText = TheText.Replace(",", "<com>");
                    if (X.Name.StartsWith("Screen"))
                    {
                        if (TheText == "Top Screen")
                            TheText = "1";
                        if (TheText == "Bottom Screen")
                            TheText = "0";
                    }
                    else if (X.Name.StartsWith("TrueFalse"))
                    {
                        if (TheText == "True")
                            TheText = "1";
                        if (TheText == "False")
                            TheText = "0";
                    }
                    else if (X.Name.StartsWith("Comparative"))
                    {
                        TheText = DS_Game_Maker.ScriptsLib.StringToComparative(TheText);
                    }
                    ArgumentString += TheText + ";";
                }
                ArgumentString = ArgumentString.Substring(0, ArgumentString.Length - 1);
            }
            // MsgError(ArgumentString)
            Close();
        }

        // Private Sub DCancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        // Me.Close()
        // End Sub

        private void Action_Load(object sender, EventArgs e)
        {
            UseData = false;
            InstancesTextBox.Text = string.Empty;
            InstancesOfDropper.Text = string.Empty;
            Text = ActionName;
            byte DOn = 0;
            LabelsPanel.Controls.Clear();
            ControlsPanel.Controls.Clear();
            foreach (string X_ in File.ReadAllLines(DS_Game_Maker.DSGMlib.AppPath + @"Actions\" + ActionName + ".action"))
            {
                string X = X_;
                if (X_.StartsWith("ARG "))
                {
                    X = X.Substring(4);
                    string ArgumentName = X.Substring(0, X.IndexOf(","));
                    byte ArgumentType = Convert.ToByte(X.Substring(ArgumentName.Length + 1));
                    var NewLabel = new Label();
                    NewLabel.Text = ArgumentName;
                    NewLabel.Location = new Point(1, 8 + DOn * 24);
                    LabelsPanel.Controls.Add(NewLabel);
                    Control InputControl;
                    string TheContent = DS_Game_Maker.DSGMlib.iGet(ArgumentString, DOn, ";").ToString().Replace("<com>", ",");
                    if (ArgumentType == 0 | ArgumentType == 12)
                    {
                        InputControl = new TextBox();
                        ((TextBox)InputControl).Location = new Point(5, 5 + DOn * 24);
                        ((TextBox)InputControl).Width = 123;
                        if (ArgumentType == 0)
                            InputControl.Name = "Generic" + DOn.ToString();
                        if (ArgumentType == 12)
                            InputControl.Name = "Unrestricted" + DOn.ToString();
                    }
                    else
                    {
                        InputControl = new ComboBox();
                        ((ComboBox)InputControl).Location = new Point(5, 5 + DOn * 24);
                        ((ComboBox)InputControl).Width = 123;
                    }
                    if (ArgumentType == 1)
                    {
                        if (TheContent == "1")
                            TheContent = "Top Screen";
                        if (TheContent == "0")
                            TheContent = "Bottom Screen";
                        ((ComboBox)InputControl).Items.Add("Top Screen");
                        ((ComboBox)InputControl).Items.Add("Bottom Screen");
                        InputControl.Name = "Screen" + DOn.ToString();
                    }
                    else if (ArgumentType == 2)
                    {
                        if (TheContent == "1")
                            TheContent = "True";
                        if (TheContent == "0")
                            TheContent = "False";
                        ((ComboBox)InputControl).Items.Add("True");
                        ((ComboBox)InputControl).Items.Add("False");
                        InputControl.Name = "TrueFalse" + DOn.ToString();
                    }
                    else if (ArgumentType == 3)
                    {
                        foreach (string Y_ in DS_Game_Maker.DSGMlib.GetXDSFilter("GLOBAL "))
                        {
                            string Y = Y_;
                            Y = Y.Substring(7);
                            string VariableName = Y.Substring(0, Y.IndexOf(","));
                            ((ComboBox)InputControl).Items.Add(VariableName);
                        }
                        InputControl.Name = "Variable" + DOn.ToString();
                    }
                    else if (ArgumentType == 4)
                    {
                        foreach (string Y_ in DS_Game_Maker.DSGMlib.GetXDSFilter("OBJECT "))
                        {
                            string Y = Y_;
                            Y = Y.Substring(7);
                            string ObjectName = Y.Substring(0, Y.IndexOf(","));
                            ((ComboBox)InputControl).Items.Add(ObjectName);
                        }
                        InputControl.Name = "Object" + DOn.ToString();
                    }
                    else if (ArgumentType == 5)
                    {
                        foreach (string Y in DS_Game_Maker.DSGMlib.GetXDSFilter("BACKGROUND "))
                            ((ComboBox)InputControl).Items.Add(Y.Substring(11));
                        InputControl.Name = "Background" + DOn.ToString();
                    }
                    else if (ArgumentType == 6)
                    {
                        foreach (string Y_ in DS_Game_Maker.DSGMlib.GetXDSFilter("SOUND "))
                        {
                            string Y = Y_;
                            Y = Y.Substring(6);
                            string SoundName = Y.Substring(0, Y.IndexOf(","));
                            ((ComboBox)InputControl).Items.Add(SoundName);
                        }
                    }
                    else if (ArgumentType == 7)
                    {
                        foreach (string Y_ in DS_Game_Maker.DSGMlib.GetXDSFilter("ROOM "))
                        {
                            string Y = Y_;
                            Y = Y.Substring(5);
                            string RoomName = Y.Substring(0, Y.IndexOf(","));
                            ((ComboBox)InputControl).Items.Add(RoomName);
                        }
                        InputControl.Name = "Room" + DOn.ToString();
                    }
                    else if (ArgumentType == 8)
                    {
                        foreach (string Y in DS_Game_Maker.DSGMlib.GetXDSFilter("PATH "))
                            ((ComboBox)InputControl).Items.Add(Y.Substring(5));
                        InputControl.Name = "Path" + DOn.ToString();
                    }
                    else if (ArgumentType == 9)
                    {
                        foreach (string Y in DS_Game_Maker.DSGMlib.GetXDSFilter("SCRIPT "))
                        {
                            string ScriptName = Y.Substring(7);
                            ScriptName = ScriptName.Substring(0, ScriptName.LastIndexOf(","));
                            ((ComboBox)InputControl).Items.Add(ScriptName);
                        }
                        InputControl.Name = "Script" + DOn.ToString();
                    }
                    else if (ArgumentType == 10)
                    {
                        ((ComboBox)InputControl).Items.Add("Less than");
                        ((ComboBox)InputControl).Items.Add("Equal to");
                        ((ComboBox)InputControl).Items.Add("Greater than");
                        ((ComboBox)InputControl).Items.Add("Less than or Equal to");
                        ((ComboBox)InputControl).Items.Add("Greater than or Equal to");
                        ((ComboBox)InputControl).Items.Add("Not Equal to");
                        TheContent = DS_Game_Maker.ScriptsLib.ComparativeToString(TheContent);
                        InputControl.Name = "Comparative" + DOn.ToString();
                    }
                    else if (ArgumentType == 11)
                    {
                        foreach (string F in DS_Game_Maker.DSGMlib.Fonts)
                            ((ComboBox)InputControl).Items.Add(F);
                    }
                    // 12 Unrestrictive = no difference
                    else if (ArgumentType == 13)
                    {
                        foreach (string Y in DS_Game_Maker.DSGMlib.GetXDSFilter("SPRITE "))
                        {
                            string SpriteName = Y;
                            SpriteName = SpriteName.Substring(7);
                            SpriteName = SpriteName.Substring(0, SpriteName.IndexOf(","));
                            ((ComboBox)InputControl).Items.Add(SpriteName);
                        }
                        InputControl.Name = "Sprite" + DOn.ToString();
                    }
                    else if (ArgumentType == 14)
                    {
                        // For compatibility only, folks
                        InputControl.Name = "CScript" + DOn.ToString();
                    }
                    else if (ArgumentType == 15)
                    {
                        foreach (string Y in DS_Game_Maker.DSGMlib.GetXDSFilter("ARRAY "))
                        {
                            string ArrayName = Y;
                            ArrayName = ArrayName.Substring(6);
                            ArrayName = ArrayName.Substring(0, ArrayName.IndexOf(","));
                            ((ComboBox)InputControl).Items.Add(ArrayName);
                        }
                        InputControl.Name = "Array" + DOn.ToString();
                    }
                    else if (ArgumentType == 16)
                    {
                        foreach (string Y_ in DS_Game_Maker.DSGMlib.GetXDSFilter("STRUCTURE "))
                        {
                            string Y = Y_;
                            Y = Y.Substring(10);
                            string StructureName = Y.Substring(0, Y.IndexOf(","));
                            ((ComboBox)InputControl).Items.Add(StructureName);
                        }
                        InputControl.Name = "Structure" + DOn.ToString();
                    }
                    if (ArgumentType == 0 | ArgumentType == 12)
                    {
                        ((TextBox)InputControl).Text = TheContent;
                    }
                    else
                    {
                        ((ComboBox)InputControl).Text = TheContent;
                    }
                    ControlsPanel.Controls.Add(InputControl);
                    DOn = (byte)(DOn + 1);
                }
            }
            if (DOn == 0)
            {
                Height = 180;
                LabelsPanel.Visible = false;
                ControlsPanel.Visible = false;
            }
            else
            {
                Height = 350;
                LabelsPanel.Visible = true;
                ControlsPanel.Visible = true;
            }
            InstancesOfDropper.Items.Clear();
            foreach (string X_ in DS_Game_Maker.DSGMlib.GetXDSFilter("OBJECT "))
            {
                string X = X_;
                X = X.Substring(7);
                InstancesOfDropper.Items.Add(X.Substring(0, X.IndexOf(",")));
            }
            bool CheckedSomething = false;
            if (AppliesTo == "this")
            {
                ThisRadioButton.Checked = true;
                CheckedSomething = true;
            }
            else if (DS_Game_Maker.DSGMlib.IsObject(AppliesTo))
            {
                InstancesOfRadioButton.Checked = true;
                InstancesOfDropper.Text = AppliesTo;
                CheckedSomething = true;
            }
            else
            {
                InstancesTextBox.Text = AppliesTo;
                InstancesRadioButton.Checked = true;
                CheckedSomething = true;
            }
            if (!CheckedSomething)
                ThisRadioButton.Checked = true;
        }

        private void Action_Activated(object sender, EventArgs e)
        {
            if (ControlsPanel.Visible)
            {
                ControlsPanel.Height = ControlsPanel.Controls[ControlsPanel.Controls.Count - 1].Location.Y + 30;
                LabelsPanel.Height = ControlsPanel.Height;
                Height = ControlsPanel.Location.Y + ControlsPanel.Height + 68;
                ControlsPanel.Controls[0].Focus();
            }
        }

        private void ThisRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            InstancesOfDropper.Enabled = InstancesOfRadioButton.Checked;
            InstancesTextBox.Enabled = InstancesRadioButton.Checked;
        }

    }
}