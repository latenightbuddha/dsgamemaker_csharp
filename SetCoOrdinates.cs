using System;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace DS_Game_Maker
{

    public partial class SetCoOrdinates
    {

        public short X;
        public short Y;

        public SetCoOrdinates()
        {
            InitializeComponent();
        }

        private void DAcceptButton_Click(object sender, EventArgs e)
        {
            X = Conversions.ToShort(XTextBox.Text.ToString());
            Y = Conversions.ToShort(YTextBox.Text.ToString());
            Close();
        }

        private void SetCoOrdinates_Load(object sender, EventArgs e)
        {
            XTextBox.Text = X.ToString();
            YTextBox.Text = Y.ToString();
        }

        private void TextBoxs_TextChanged(object sender, EventArgs e)
        {
            bool Enabled = true;
            if (XTextBox.Text.Length == 0 | YTextBox.Text.Length == 0)
                return;
            byte XCount = 0;
            byte YCount = 0;
            foreach (string X in DS_Game_Maker.DSGMlib.Numbers)
                XCount = (byte)(XCount + DS_Game_Maker.DSGMlib.HowManyChar(XTextBox.Text, X));
            foreach (string Y in DS_Game_Maker.DSGMlib.Numbers)
                YCount = (byte)(YCount + DS_Game_Maker.DSGMlib.HowManyChar(YTextBox.Text, Y));
            if (!(XCount == XTextBox.Text.Length))
                Enabled = false;
            if (!(YCount == YTextBox.Text.Length))
                Enabled = false;
            DAcceptButton.Enabled = Enabled;
        }

        private void SetCoOrdinates_Shown(object sender, EventArgs e)
        {
            XTextBox.Focus();
        }

        private void XTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                DAcceptButton_Click(new object(), new EventArgs());
            }
        }
    }
}