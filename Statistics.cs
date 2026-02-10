using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace DS_Game_Maker
{
    public partial class Statistics
    {

        private short ProjectCoolness = 0;

        public Statistics()
        {
            InitializeComponent();
        }

        private void DAcceptButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Statistics_Load(object sender, EventArgs e)
        {
            MainToolStrip.Renderer = new DS_Game_Maker.clsToolstripRenderer();
            var ResSentences = new List<string>();
            var LogicSentences = new List<string>();
            string ToAdd = "You have " + DS_Game_Maker.DSGMlib.GetXDSFilter("ROOM ").Length.ToString() + " rooms, encompassing " + DS_Game_Maker.DSGMlib.GetXDSFilter("OBJECTPOS ").Length.ToString() + " instances";
            ToAdd += " of " + DS_Game_Maker.DSGMlib.GetXDSFilter("OBJECT ").Length.ToString() + " objects which are using " + DS_Game_Maker.DSGMlib.GetXDSFilter("SPRITE ").Length.ToString() + " sprites";
            ResSentences.Add(ToAdd);
            ToAdd = "There are also " + DS_Game_Maker.DSGMlib.GetXDSFilter("SCRIPT ").Length.ToString() + " scripts, " + DS_Game_Maker.DSGMlib.GetXDSFilter("PATH ").Length.ToString() + " paths,";
            ToAdd += " " + DS_Game_Maker.DSGMlib.GetXDSFilter("SOUND ").Length.ToString() + " sounds and " + DS_Game_Maker.DSGMlib.GetXDSFilter("BACKGROUND ").Length.ToString() + " backgrounds";
            ResSentences.Add(ToAdd);
            string ResourcesText = string.Empty;
            foreach (string X in ResSentences)
                ResourcesText += X + ". ";
            ResourcesText = ResourcesText.Substring(0, ResourcesText.Length - 1);
            // Efficient Defines
            short EventCount = (short)DS_Game_Maker.DSGMlib.GetXDSFilter("EVENT ").Length;
            short ObjectCount = (short)DS_Game_Maker.DSGMlib.GetXDSFilter("OBJECT ").Length;
            int ActionsCount = DS_Game_Maker.DSGMlib.GetXDSFilter("ACT ").Length;
            // Defines
            short EventAverage = 0;
            short ActionAverage = 0;
            short ActionAverage2 = 0;
            if (EventCount > 0 & ObjectCount > 0)
            {
                EventAverage = (short)Math.Round(EventCount / (double)ObjectCount);
                ActionAverage2 = (short)Math.Round(ObjectCount / (double)EventCount);
            }
            if (ActionsCount > 0 & EventCount > 0)
                ActionAverage = (short)Math.Round(ActionsCount / (double)EventCount);
            ToAdd = "In your " + DS_Game_Maker.DSGMlib.GetXDSFilter("OBJECT ").Length.ToString() + " objects there is a total of " + DS_Game_Maker.DSGMlib.GetXDSFilter("EVENT ").Length.ToString();
            ToAdd += " events (an average of " + EventAverage.ToString() + " events per object)";
            LogicSentences.Add(ToAdd);
            ToAdd = "There is a total of " + DS_Game_Maker.DSGMlib.GetXDSFilter("OBJECT ").Length.ToString() + " actions (phew!). That's an average of " + ActionAverage.ToString() + " actions per event, or ";
            ToAdd += ActionAverage2.ToString() + " actions per object";
            LogicSentences.Add(ToAdd);
            string LogicText = string.Empty;
            foreach (string X in LogicSentences)
                LogicText += X + ". ";
            LogicText = LogicText.Substring(0, LogicText.Length - 1);
            ResoucesLabel.Text = ResourcesText;
            LogicLabel.Text = LogicText;
        }

        private void CopytoClipboardButton_Click(object sender, EventArgs e)
        {
            string Returnable = "Resources:" + Constants.vbCrLf + Constants.vbCrLf;
            Returnable += ResoucesLabel.Text + Constants.vbCrLf + Constants.vbCrLf;
            Returnable += "Logic:" + Constants.vbCrLf + Constants.vbCrLf;
            Returnable += LogicLabel.Text + Constants.vbCrLf + Constants.vbCrLf;
            if (ProjectCoolness > 0)
            {
                Returnable += "Calculated Project Coolness was " + ProjectCoolness.ToString() + "%.";
            }
            else
            {
                Returnable += "Project Coolness was not calculated.";
            }
            Clipboard.SetText(Returnable);
        }

        private void CalculateUsageButton_Click(object sender, EventArgs e)
        {
            ProjectCoolness += Convert.ToInt16((double)DS_Game_Maker.DSGMlib.GetXDSFilter("ACT ").Length / 2d);
            ProjectCoolness += Convert.ToInt16(DS_Game_Maker.DSGMlib.GetXDSFilter("SCRIPT ").Length * 10);
            ProjectCoolness += Convert.ToInt16(DS_Game_Maker.DSGMlib.GetXDSFilter("EVENT ").Length * 2);
            ProjectCoolness += Convert.ToInt16(DS_Game_Maker.DSGMlib.GetXDSFilter("EVENT ").Length * 2);
            ProjectCoolness -= Convert.ToInt16(DS_Game_Maker.DSGMlib.GetXDSFilter("SPRITE ").Length * 5);
            if (ProjectCoolness > 1000)
                ProjectCoolness = (short)Math.Round(ProjectCoolness / 10d);
            if (ProjectCoolness > 11)
                ProjectCoolness = (short)Math.Round(ProjectCoolness / 10d);
            CoolBar.Value = ProjectCoolness;
        }
    }
}