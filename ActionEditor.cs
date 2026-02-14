
namespace DS_Game_Maker
{

    public partial class ActionEditor
    {

        public string Editing = string.Empty;

        public ActionEditor()
        {
            InitializeComponent();
        }

        private void ActionEditor_Load(object sender, EventArgs e)
        {
            MainToolStrip.Renderer = new clsToolstripRenderer();
            SubToolStrip.Renderer = new clsToolstripRenderer();
            MainImageList.Images.Add("ActionIcon", Properties.Resources.ActionIcon); 
            ActionsTreeView.ImageList = MainImageList;
            TypeDropper.Items.Clear();
            ImageDropper.Items.Clear();
            for (byte X = 0; X <= 5; X++)
                TypeDropper.Items.Add(ScriptsLib.ActionTypeToString(X));
            foreach (string X in Directory.GetFiles(Constants.AppDirectory + "ActionIcons"))
            {
                string ToAdd = X.Substring(X.LastIndexOf("/") + 1);
                ToAdd = ToAdd.Substring(0, ToAdd.IndexOf("."));
                ImageDropper.Items.Add(ToAdd);
            }
            short ActionCount = 0;
            foreach (string X in Directory.GetFiles(Constants.AppDirectory + "Actions", "*.action"))
            {
                string ActionName = X.Substring(X.LastIndexOf("/") + 1);
                ActionName = ActionName.Substring(0, ActionName.LastIndexOf("."));
                ActionsTreeView.Nodes.Add(new TreeNode(ActionName, 0, 0));
                ActionCount = (short)(ActionCount + 1);
            }
            // If ActionCount = 0 Then MsgWarn("You haven't got any actions on your system.")
            ActionsTreeView_NodeMouseDoubleClick(new object(), new TreeNodeMouseClickEventArgs(ActionsTreeView.Nodes[0], MouseButtons.Left, 1, 0, 0));
            // If Not ActionCount = 0 Then ActionsTreeView.SelectedNode = ActionsTreeView.Nodes(0)
        }

        private void ActionsTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!File.Exists(Constants.AppDirectory + "Actions/" + e.Node.Text + ".action"))
                return;
            byte ActionType = 6;
            string ActionDisplay = string.Empty;
            var ArgNames = new List<string>();
            var ArgTypes = new List<string>();
            string FinalString = string.Empty;
            bool IndentSet = false;
            bool NoApplies = false;
            DontRequestApplicationChecker.Checked = false;
            ConditionalDisplayChecker.Checked = false;
            foreach (string ActLine_ in File.ReadAllLines(Constants.AppDirectory + "Actions/" + e.Node.Text + ".action"))
            {
                string ActLine = ActLine_;
                if (ActLine.StartsWith("TYPE "))
                {
                    ActionType = Convert.ToByte(ActLine.Substring(5));
                    continue;
                }
                if (ActLine.StartsWith("DISPLAY"))
                {
                    ActionDisplay = ActLine.Substring(8);
                    continue;
                }
                if (ActLine == "INDENT")
                {
                    IndentRadioButton.Checked = true;
                    IndentSet = true;
                    continue;
                }
                if (ActLine == "DEDENT")
                {
                    InvertRadioButton.Checked = true;
                    IndentSet = true;
                    continue;
                }
                if (ActLine == "NOAPPLIES")
                {
                    DontRequestApplicationChecker.Checked = true;
                    continue;
                }
                if (ActLine.StartsWith("CONDITION "))
                {
                    ActLine = ActLine.Substring("CONDITION".Length + 1);
                    ConditionalDisplayChecker.Checked = ActLine == "1";
                    continue;
                }
                if (ActLine.StartsWith("ICON "))
                    continue;
                if (ActLine.StartsWith("ARG "))
                {
                    ActLine = ActLine.Substring(4);
                    ArgNames.Add(ActLine.Substring(0, ActLine.IndexOf(",")));
                    ArgTypes.Add(ActLine.Substring(ActLine.IndexOf(",") + 1));
                    continue;
                }
                FinalString += ActLine + Constants.vbCrLf;
            }
            if (!IndentSet)
                GenericRadioButton.Checked = true;
            bool IsError = false;
            if (ActionType == 6)
                IsError = true;
            if (ActionDisplay.Length == 0)
                IsError = true;
            if (IsError)
            {
                DSGMlib.MsgError("'" + e.Node.Text + "' is not a valid action file.");
                return;
            }
            Editing = e.Node.Text;
            NameTextBox.Text = e.Node.Text;
            ListDisplayTextBox.Text = ActionDisplay;
            TypeDropper.Text = ScriptsLib.ActionTypeToString(ActionType);
            ImageDropper.Text = ActionsLib.ActionGetIconPath(e.Node.Text, false);
            ImageDropper.Text = ImageDropper.Text.Substring(0, ImageDropper.Text.LastIndexOf("."));
            ArgumentsListView.Items.Clear();
            if (ArgNames.Count > 0)
            {
                for (byte X = 0, loopTo = (byte)(ArgNames.Count - 1); X <= loopTo; X++)
                {
                    var ToAdd = new ListViewItem();
                    ToAdd.Text = ArgNames[X];
                    ToAdd.SubItems.Add(ScriptsLib.ArgumentTypeToString(Convert.ToByte(ArgTypes[X])));
                    ArgumentsListView.Items.Add(ToAdd);
                }
            }
            MainTextBox.Text = FinalString;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (Editing.Length == 0)
            {
                DSGMlib.MsgWarn("No action is selected.");
                return;
            }
            if (!File.Exists(Constants.AppDirectory + "ActionIcons/" + ImageDropper.Text + ".png"))
            {
                DSGMlib.MsgWarn("The action icon does not exist.");
                return;
            }
            // TODO LATER
            // Update references to this action. Scripts, and in Events.

            // TODO LATER
            // Loop through Object forms - update
            // Actions collection, action images, action displays.
            // Shit, action arguments
            // Repopulate the tab control

            // TODO LATER
            // actions in arguments - knock offs and add ons
            SaveButton.Enabled = false;
            bool NameChanged = true;
            if (Editing == NameTextBox.Text)
                NameChanged = false;
            bool ActionExists = false;
            if (NameTextBox.Text != Editing)
            {
                foreach (TreeNode Action in ActionsTreeView.Nodes)
                {
                    if (NameTextBox.Text == Action.Text)
                    {
                        ActionExists = true;
                        break;
                    }
                }
            }
            if (ActionExists)
            {
                DSGMlib.MsgError("There is already an action called '" + NameTextBox.Text + "'.");
                return;
            }
            string FinalString = "TYPE " + TypeDropper.SelectedIndex.ToString() + Constants.vbCrLf;
            FinalString += "DISPLAY " + ListDisplayTextBox.Text + Constants.vbCrLf;
            FinalString += "ICON " + ImageDropper.Text + ".png" + Constants.vbCrLf;
            FinalString += "CONDITION " + (ConditionalDisplayChecker.Checked ? "1" : "0") + Constants.vbCrLf;
            foreach (ListViewItem X in ArgumentsListView.Items)
                FinalString += "ARG " + X.Text + "," + ScriptsLib.ArgumentStringToType(X.SubItems[1].Text).ToString() + Constants.vbCrLf;
            foreach (ScintillaNET.Line X in MainTextBox.Lines)
                FinalString += X.Text;
            if (IndentRadioButton.Checked)
                FinalString += Constants.vbCrLf + "INDENT";
            if (InvertRadioButton.Checked)
                FinalString += Constants.vbCrLf + "DEDENT";
            if (DontRequestApplicationChecker.Checked)
                FinalString += Constants.vbCrLf + "NOAPPLIES";
            File.WriteAllText(Constants.AppDirectory + "Actions/" + Editing + ".action", FinalString);
            if (NameChanged)
                File.Move(Constants.AppDirectory + "Actions/" + Editing + ".action", Constants.AppDirectory + "Actions/" + NameTextBox.Text + ".action");
            foreach (TreeNode X in ActionsTreeView.Nodes)
            {
                if (X.Text == Editing)
                    X.Text = NameTextBox.Text;
            }
            foreach (string X in DSGMlib.GetXDSFilter("ACT "))
            {
                if ((DSGMlib.iGet(X, (byte)3, ",") ?? "") == (Editing ?? ""))
                {
                    string NewLine = DSGMlib.iGet(X, (byte)0, ",") + ",";
                    NewLine += DSGMlib.iGet(X, (byte)1, ",") + ",";
                    NewLine += DSGMlib.iGet(X, (byte)2, ",") + ",";
                    NewLine += NameTextBox.Text + "," + DSGMlib.iGet(X, (byte)4, ",") + "," + DSGMlib.iGet(X, (byte)5, ",");
                    DSGMlib.XDSChangeLine(X, NewLine);
                }
            }
            foreach (Form X in Program.Forms.main_Form.MdiChildren)
            {
                if (DSGMlib.GetXDSFilter("OBJECT " + X.Text + ",").Length == 0)
                    continue;
                for (short Y = 0, loopTo = (short)(((DObject)X).Actions.Count - 1); Y <= loopTo; Y++)
                {
                    if ((((DObject)X).Actions[(int)Y] ?? "") == (Editing ?? ""))
                    {
                        ((DObject)X).Actions[(int)Y] = NameTextBox.Text;
                        ((DObject)X).ActionImages[(int)Y] = ActionsLib.ActionGetIconPath(NameTextBox.Text, false);
                        ((DObject)X).ActionDisplays[(int)Y] = ActionsLib.ActionEquateDisplay(((DObject)X).Actions[(int)Y], ((DObject)X).ActionArguments[(int)Y]);
                    }
                } ((DObject)X).RepopulateLibrary();
                // For Each Y As Control In X.Controls
                // If Y.Name = "ActionsToAddTabControl" Then
                // DObject.PopulateActionsTabControl(DirectCast(Y, TabControl))
                // ElseIf Y.Name = "ActionsList" Then
                // DirectCast(Y, ListBox).Refresh()
                // End If
                // Next
            }
            Editing = NameTextBox.Text;
            SaveButton.Enabled = true;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            SaveButton_Click(new object(), new EventArgs());
            Close();
        }

        private void DeleteArgumentButton_Click(object sender, EventArgs e)
        {
            if (ArgumentsListView.SelectedItems.Count == 0)
                return;
            string TheName = ArgumentsListView.SelectedItems[0].Text;
            ArgumentsListView.SelectedItems[0].Remove();
            bool NeedsBinning = false;
            bool DisplayNeedsBinning = ListDisplayTextBox.Text.Contains("$" + TheName + "$") ? true : false;
            if (ListDisplayTextBox.Text.Contains("%" + TheName + "%"))
                DisplayNeedsBinning = true;
            foreach (ScintillaNET.Line X in MainTextBox.Lines)
            {
                if (X.Text.Contains("!" + TheName + "!"))
                    NeedsBinning = true;
            }
            bool BothMessages = NeedsBinning & DisplayNeedsBinning ? true : false;
            if (BothMessages)
            {
                DSGMlib.MsgWarn("You must removal all references to !" + TheName + "! and $" + TheName + "$.");
            }
            else
            {
                if (NeedsBinning)
                {
                    DSGMlib.MsgWarn("You must removal all references to !" + TheName + "!.");
                }
                if (DisplayNeedsBinning)
                {
                    DSGMlib.MsgWarn("You must removal all references to $" + TheName + "$ and %" + TheName + "%.");
                }
            }
        }

        private void AddArgumentButton_Click(object sender, EventArgs e)
        {
            if (Editing.Length == 0)
            {
                DSGMlib.MsgWarn("No action is selected.");
                return;
            }
            var ArgumentForm = new Argument();
            ArgumentForm.ArgumentName = string.Empty;
            ArgumentForm.ArgumentType = 0.ToString();
            ArgumentForm.Text = "Add Argument";
            ArgumentForm.IsAction = true;
            if (!(ArgumentForm.ShowDialog() == DialogResult.OK))
                return;
            if (ArgumentForm.ArgumentName.Length == 0)
                return;
            string NewName = ArgumentForm.ArgumentName;
            string NewType = ScriptsLib.ArgumentTypeToString(Convert.ToByte(ArgumentForm.ArgumentType));
            ArgumentForm.Dispose();
            if (!DSGMlib.ValidateSomething(NewName, (byte)DSGMlib.ValidateLevel.Loose))
            {
                DSGMlib.MsgWarn("You must enter a valid name for the new Argument.");
                return;
            }
            bool AlreadyDone = false;
            foreach (ListViewItem X in ArgumentsListView.Items)
            {
                if ((X.Text ?? "") == (NewName ?? ""))
                    AlreadyDone = true;
            }
            if (AlreadyDone)
            {
                DSGMlib.MsgError("There is already an Argument called '" + NewName + "'." + Constants.vbCrLf + Constants.vbCrLf + "You must choose another name.");
                return;
            }
            var Y = new ListViewItem();
            Y.Text = ArgumentForm.ArgumentName;
            Y.SubItems.Add(ScriptsLib.ArgumentTypeToString(Convert.ToByte(ArgumentForm.ArgumentType)));
            ArgumentsListView.Items.Add(Y);
        }

        private void EditArgumentButton_Click(object sender, EventArgs e)
        {
            if (ArgumentsListView.SelectedItems.Count == 0)
                return;
            var ArgumentForm = new Argument();
            ArgumentForm.ArgumentName = ArgumentsListView.SelectedItems[0].Text;
            ArgumentForm.ArgumentType = ScriptsLib.ArgumentStringToType(ArgumentsListView.SelectedItems[0].SubItems[1].Text).ToString();
            ArgumentForm.Text = "Edit Argument";
            ArgumentForm.IsAction = true;
            if (!(ArgumentForm.ShowDialog() == DialogResult.OK))
                return;
            if (ArgumentForm.ArgumentName.Length == 0)
                return;
            string NewName = ArgumentForm.ArgumentName;
            string NewType = ScriptsLib.ArgumentTypeToString(Convert.ToByte(ArgumentForm.ArgumentType));
            if (!DSGMlib.ValidateSomething(NewName, (byte)DSGMlib.ValidateLevel.Loose))
            {
                DSGMlib.MsgWarn("You must enter a valid name for the Argument.");
                return;
            }
            if (!((NewName ?? "") == (ArgumentsListView.SelectedItems[0].Text ?? "")))
            {
                bool AlreadyDone = false;
                foreach (ListViewItem X in ArgumentsListView.Items)
                {
                    if ((X.Text ?? "") == (NewName ?? ""))
                        AlreadyDone = true;
                }
                if (AlreadyDone)
                {
                    DSGMlib.MsgError("There is already an Argument called '" + NewName + "'." + Constants.vbCrLf + Constants.vbCrLf + "You must choose another name.");
                    return;
                }
            }
            ArgumentsListView.SelectedItems[0].Text = NewName;
            ArgumentsListView.SelectedItems[0].SubItems[1].Text = NewType;
            ArgumentForm.Dispose();
        }

        private void DeleteActionButton_Click(object sender, EventArgs e)
        {
            if (Editing.Length == 0)
            {
                DSGMlib.MsgWarn("No action is selected.");
                return;
            }
            string ActionName = ActionsTreeView.SelectedNode.Text;
           
            DialogResult Response = MessageBox.Show("Are you sure you want to delete '" + ActionName + "'?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Response == DialogResult.No)
            {
                return;
            }

            File.Delete(Constants.AppDirectory + "Actions/" + ActionName.Split('\\')[1] + ".action");
            ActionsTreeView.SelectedNode.Remove();
            if (ActionsTreeView.Nodes.Count == 0)
            {
                // Deleted the last action
                ClearStuff();
            }
            else
            {
                // Can select another!!
                ActionsTreeView.SelectedNode = ActionsTreeView.Nodes[0];
            }
        }

        public void ClearStuff()
        {
            NameTextBox.Text = string.Empty;
            ListDisplayTextBox.Text = string.Empty;
            TypeDropper.SelectedIndex = 0;
            ArgumentsListView.Items.Clear();
            Editing = string.Empty;
        }

        private void InsertArgumentButton_Click(object sender, EventArgs e)
        {
            if (ArgumentsListView.SelectedItems.Count == 0)
                return;
        }

        private void AddActionButton_Click(object sender, EventArgs e)
        {
            string Response = DSGMlib.GetInput("Please enter a name for the new action", string.Empty, (byte)DSGMlib.ValidateLevel.Loose);
            if (Response.Length == 0)
                return;
            string FinalString = "TYPE 5" + Constants.vbCrLf;
            FinalString += "DISPLAY " + Response + Constants.vbCrLf;
            File.WriteAllText(Constants.AppDirectory + "Actions/" + Response + ".action", FinalString);
            ActionsTreeView.Nodes.Add(new TreeNode(Response, 0, 0));
            foreach (TreeNode X in ActionsTreeView.Nodes)
            {
                if ((X.Text ?? "") == (Response ?? ""))
                    ActionsTreeView_NodeMouseDoubleClick(new object(), new TreeNodeMouseClickEventArgs(X, MouseButtons.Left, 1, 0, 0));
            }
        }
    }
}