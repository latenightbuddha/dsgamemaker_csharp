using System;

namespace DS_Game_Maker
{
    public partial class SpriteSize
    {

        public string SpritePath = string.Empty;

        public SpriteSize()
        {
            InitializeComponent();
        }

        private void DOkayButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OpenEditorButton_Click(object sender, EventArgs e)
        {
            string X = SpritePath;
            X = X.Substring(X.LastIndexOf(@"\") + 1);
            X = X.Substring(0, X.IndexOf("."));
            DS_Game_Maker.DSGMlib.EditImage(SpritePath, X, false);
            Close();
        }

    }
}