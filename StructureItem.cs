using System;
using System.Linq;

namespace DS_Game_Maker
{
    public partial class StructureItem
    {

        public bool UseData = false;

        public string MemberName;
        public string MemberType;
        public string MemberValue;

        public StructureItem()
        {
            InitializeComponent();
        }

        private void StructureItem_Load(object sender, EventArgs e)
        {
            MainToolStrip.Renderer = new DS_Game_Maker.clsToolstripRenderer();
            TypeDropper.Items.Clear();
            for (byte X = 0, loopTo = (byte)(DS_Game_Maker.ScriptsLib.VariableTypes.Count - 1); X <= loopTo; X++)
                TypeDropper.Items.Add(DS_Game_Maker.ScriptsLib.VariableTypes[(int)X]);
            NameTextBox.Text = MemberName;
            TypeDropper.Text = MemberType;
            ValueTextBox.Text = MemberValue;
        }

        private void DAcceptButton_Click(object sender, EventArgs e)
        {
            UseData = true;
            MemberName = NameTextBox.Text;
            MemberType = TypeDropper.Text;
            MemberValue = ValueTextBox.Text;
            Close();
        }

        private void TextBoxs_TextChanged(object sender, EventArgs e)
        {
            DAcceptButton.Enabled = NameTextBox.Text.Length > 0 & TypeDropper.Text.Length > 0;
        }

        private void StructureItem_Shown(object sender, EventArgs e)
        {
            NameTextBox.Focus();
        }
    }
}