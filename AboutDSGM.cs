using System;

namespace DS_Game_Maker
{
    public partial class AboutDSGM
    {
        public AboutDSGM()
        {
            InitializeComponent();
        }

        private void WebAddressLabel_Click(object sender, EventArgs e)
        {
            DS_Game_Maker.DSGMlib.URL(DS_Game_Maker.DSGMlib.Domain);
        }

        private void DOkayButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutDSGM_Load(object sender, EventArgs e)
        {
            string VersionString = DS_Game_Maker.DSGMlib.IDVersion.ToString();
            VersionLabel.Text = "Version " + VersionString.Substring(0, 1) + "." + (VersionString.Substring(1).EndsWith("0") ? VersionString.Substring(1).Substring(0, 1) : VersionString.Substring(1)) + " (Release " + VersionString + ")";
        }
    }
}