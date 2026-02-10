using System;

namespace DS_Game_Maker
{
    public partial class InputBoxForm
    {

        public string Descriptor;
        public string TheInput;
        public byte Validation = 0;

        public InputBoxForm()
        {
            InitializeComponent();
        }

        private void InputBoxForm_Load(object sender, EventArgs e)
        {
            DescriptionLabel.Text = Descriptor;
            MainTextBox.Text = TheInput;
            if (MainTextBox.Text.Length == 0)
                DOkayButton.Enabled = false;
        }

        private void InputBoxForm_Activated(object sender, EventArgs e)
        {
            MainTextBox.Focus();
        }

        private void DCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DOkayButton_Click(object sender, EventArgs e)
        {
            TheInput = MainTextBox.Text;
            Close();
        }

        private void MainTextBox_TextChanged(object sender, EventArgs e)
        {
            DOkayButton.Enabled = DS_Game_Maker.DSGMlib.ValidateSomething(MainTextBox.Text, Validation);
        }
    }
}