using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace DS_Game_Maker
{
    public partial class HelpViewer
    {
        public HelpViewer()
        {
            InitializeComponent();
        }

        private void HelpViewer_Load(object sender, EventArgs e)
        {
            MainTreeView.Nodes.Clear();
            string FContent = DS_Game_Maker.DSGMlib.PathToString(Constants.AppDirectory + "help/topics.dat");
            foreach (string P in DS_Game_Maker.DSGMlib.StringToLines(FContent))
            {
                byte ID = Convert.ToByte(P.Substring(0, P.IndexOf(",")));
                string Name = P.Substring(P.IndexOf(",") + 1);
                MainTreeView.Nodes.Add(ID.ToString(), Name, 0, 0);
            }
            foreach (string P in DS_Game_Maker.DSGMlib.StringToLines(DS_Game_Maker.DSGMlib.PathToString(Constants.AppDirectory + "help/files.dat")))
            {
                byte ID = Convert.ToByte(P.Substring(0, P.IndexOf(",")));
                string Name = P.Substring(P.IndexOf(",") + 1);
                Name = Name.Substring(0, Name.LastIndexOf(","));
                MainTreeView.Nodes[ID].Nodes.Add(ID.ToString(), Name, 1, 1);
            }
            GoToPage("Introduction", "intro");
        }

        public void GoToPage(string Title, string FName)
        {
            string FString = DS_Game_Maker.DSGMlib.PathToString(Constants.AppDirectory + "Help/head.html").Replace("$TITLE$", Name) + Constants.vbCrLf + Constants.vbCrLf;
            if (System.IO.File.Exists(Constants.AppDirectory + "Help/" + FName + ".html"))
            {
                FString += DS_Game_Maker.DSGMlib.PathToString(Constants.AppDirectory + "Help/" + FName + ".html") + Constants.vbCrLf + Constants.vbCrLf;
            }
            else
            {
                FString += DS_Game_Maker.DSGMlib.PathToString(Constants.AppDirectory + "Help/todo.html").Replace("$TITLE$", Name) + Constants.vbCrLf + Constants.vbCrLf + Constants.vbCrLf + Constants.vbCrLf;
            }
            FString += DS_Game_Maker.DSGMlib.PathToString(Constants.AppDirectory + "Help/foot.html");
            System.IO.File.WriteAllText(Constants.AppDirectory + "Help/display.html", FString);
            DocBrowser.Navigate(Constants.AppDirectory + "Help/display.html");
        }

        private void MainTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent is null)
                return;
            string Name = e.Node.Text;
            byte ID = (byte)e.Node.Parent.Index;
            string FName = string.Empty;
            foreach (string P in DS_Game_Maker.DSGMlib.StringToLines(DS_Game_Maker.DSGMlib.PathToString(Constants.AppDirectory + "help/files.dat")))
            {
                if (P.StartsWith(ID.ToString() + "," + Name + ","))
                    FName = P.Substring(ID.ToString().Length + 1 + Name.Length + 1);
            }
            GoToPage(Name, FName);
        }

        private void DocBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (e.Url.ToString().Contains("&ext") | e.Url.ToString().Contains("?ext"))
            {
                e.Cancel = true;
                DS_Game_Maker.DSGMlib.URL(e.Url.ToString().Replace("&ext", string.Empty).Replace("?ext", string.Empty));
            }
        }
    }
}