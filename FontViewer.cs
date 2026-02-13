using System;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;

namespace DS_Game_Maker
{
    public partial class FontViewer
    {

        private byte CurrentFont = 0;
        private byte FontCount = 0;

        public FontViewer()
        {
            InitializeComponent();
        }

        private void RebuildCacheButton_Click(object sender, EventArgs e)
        {
            DSGMlib.RebuildFontCache();
            InitialDisplayShizzle();
            CurrentFont = 0;
            DisplayShizzle();
        }

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            NextButton.Enabled = true;
            if (CurrentFont == 1)
                PreviousButton.Enabled = false;
            CurrentFont = (byte)(CurrentFont - 1);
            DisplayShizzle();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            PreviousButton.Enabled = true;
            if (CurrentFont == FontCount - 2)
                NextButton.Enabled = false;
            CurrentFont = (byte)(CurrentFont + 1);
            DisplayShizzle();
        }

        private void RebuildCacheButton_MouseEnter(object sender, EventArgs e)
        {
            switch (((dynamic)sender).Name)
            {
                case var @case when Operators.ConditionalCompareObjectEqual(@case, "RebuildCacheButton", false):
                    {
                        MainInfoLabel.Text = "Rebuild Font Cache";
                        break;
                    }
                case var case1 when Operators.ConditionalCompareObjectEqual(case1, "PreviousButton", false):
                    {
                        MainInfoLabel.Text = "Previous Font";
                        break;
                    }
                case var case2 when Operators.ConditionalCompareObjectEqual(case2, "NextButton", false):
                    {
                        MainInfoLabel.Text = "Next Font";
                        break;
                    }
            }
        }

        public void DisplayShizzle()
        {
            NameLabel.Text = DSGMlib.Fonts[(int)CurrentFont];
            MainImagePanel.BackgroundImage = DSGMlib.PathToImage(Constants.AppDirectory + "Fonts/" + DSGMlib.Fonts[(int)CurrentFont] + ".png");
        }

        private void RebuildCacheButton_MouseLeave(object sender, EventArgs e)
        {
            MainInfoLabel.Text = string.Empty;
        }

        public void InitialDisplayShizzle()
        {
            NameLabel.Text = DSGMlib.Fonts[(int)CurrentFont];
            PreviousButton.Enabled = false;
            NextButton.Enabled = false;
            if (DSGMlib.Fonts.Count == 1)
                return;
            NextButton.Enabled = true;
            DisplayShizzle();
        }

        private void FontEditor_Load(object sender, EventArgs e)
        {
            DSGMlib.RebuildFontCache();
            FontCount = (byte)DSGMlib.Fonts.Count;
            if (FontCount == 0)
            {
                DSGMlib.MsgError("There are no Fonts on this machine.");
                Close();
                return;
            }
            CurrentFont = 0;
            InitialDisplayShizzle();
        }
    }
}