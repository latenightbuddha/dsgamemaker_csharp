using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace DS_Game_Maker
{
    public partial class Argument
    {

        public string ArgumentName = string.Empty;
        public string ArgumentType = string.Empty;
        public bool IsAction;

        public Argument()
        {
            InitializeComponent();
        }

        private void Argument_Load(object sender, EventArgs e)
        {
            TypeDropper.Items.Clear();
            if (IsAction)
            {
                for (byte X = 1; X <= 16; X++)
                    TypeDropper.Items.Add(DS_Game_Maker.ScriptsLib.ArgumentTypeToString(X));
                TypeDropper.Text = DS_Game_Maker.ScriptsLib.ArgumentTypeToString(Conversions.ToByte(ArgumentType));
            }
            else
            {
                for (byte X = 0, loopTo = (byte)(DS_Game_Maker.ScriptsLib.VariableTypes.Count - 1); X <= loopTo; X++)
                    TypeDropper.Items.Add(DS_Game_Maker.ScriptsLib.VariableTypes[(int)X]);
                TypeDropper.Text = ArgumentType;
            }
            NameTextBox.Text = ArgumentName;
        }

        private void DOkayButton_Click(object sender, EventArgs e)
        {
            ArgumentName = NameTextBox.Text;
            if (IsAction)
            {
                ArgumentType = DS_Game_Maker.ScriptsLib.ArgumentStringToType(TypeDropper.Text).ToString();
            }
            else
            {
                ArgumentType = TypeDropper.Text;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Argument_Activated(object sender, EventArgs e)
        {
            NameTextBox.Focus();
        }
    }
}