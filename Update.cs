using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace DS_Game_Maker
{
    public partial class DUpdate
    {

        private bool HasShown = false;

        public DUpdate()
        {
            InitializeComponent();
        }

        private void NeverMindButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InstallButton_Click(object sender, EventArgs e)
        {
            DSGMlib.URL(DSGMlib.Domain + "downloads/Install" + DSGMlib.UpdateVersion.ToString() + ".exe");
            DSGMlib.MsgInfo("Thanks for choosing to keep " + Application.ProductName + " up to date." + Constants.vbCrLf + Constants.vbCrLf + "The program will now exit so that the update may install.");
            Environment.Exit(0);
        }

        private void Update_Shown(object sender, EventArgs e)
        {
            if (HasShown)
                return;
            InstallButton.Focus();
            MainWebBrowser.Navigate(DSGMlib.Domain + "DSGM5RegClient/" + DSGMlib.UpdateVersion.ToString() + ".php");
            HasShown = true;
        }

        private void Update_Load(object sender, EventArgs e)
        {
            string VersionString = DSGMlib.UpdateVersion.ToString();
            TitleLabel.Text = "Version " + VersionString.Substring(0, 1) + "." + VersionString.Substring(1, 2) + " is Available";
        }

        private void MainWebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (e.Url.ToString().Contains("dsgmforum"))
            {
                DSGMlib.URL(e.Url.ToString());
                e.Cancel = true;
            }
        }

    }
}