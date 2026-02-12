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
            MainToolStrip.Renderer = new clsToolstripRenderer();
            var ResSentences = new List<string>();
            var LogicSentences = new List<string>();
            string ToAdd = "You have " + DSGMlib.GetXDSFilter("ROOM ").Length.ToString() + " rooms, encompassing " + DSGMlib.GetXDSFilter("OBJECTPOS ").Length.ToString() + " instances";
            ToAdd += " of " + DSGMlib.GetXDSFilter("OBJECT ").Length.ToString() + " objects which are using " + DSGMlib.GetXDSFilter("SPRITE ").Length.ToString() + " sprites";
            ResSentences.Add(ToAdd);
            ToAdd = "There are also " + DSGMlib.GetXDSFilter("SCRIPT ").Length.ToString() + " scripts, " + DSGMlib.GetXDSFilter("PATH ").Length.ToString() + " paths,";
            ToAdd += " " + DSGMlib.GetXDSFilter("SOUND ").Length.ToString() + " sounds and " + DSGMlib.GetXDSFilter("BACKGROUND ").Length.ToString() + " backgrounds";
            ResSentences.Add(ToAdd);
            string ResourcesText = string.Empty;
            foreach (string X in ResSentences)
                ResourcesText += X + ". ";
            ResourcesText = ResourcesText.Substring(0, ResourcesText.Length - 1);
            // Efficient Defines
            short EventCount = (short)DSGMlib.GetXDSFilter("EVENT ").Length;
            short ObjectCount = (short)DSGMlib.GetXDSFilter("OBJECT ").Length;
            int ActionsCount = DSGMlib.GetXDSFilter("ACT ").Length;
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
            ToAdd = "In your " + DSGMlib.GetXDSFilter("OBJECT ").Length.ToString() + " objects there is a total of " + DSGMlib.GetXDSFilter("EVENT ").Length.ToString();
            ToAdd += " events (an average of " + EventAverage.ToString() + " events per object)";
            LogicSentences.Add(ToAdd);
            ToAdd = "There is a total of " + DSGMlib.GetXDSFilter("OBJECT ").Length.ToString() + " actions (phew!). That's an average of " + ActionAverage.ToString() + " actions per event, or ";
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
            ProjectCoolness += Convert.ToInt16((double)DSGMlib.GetXDSFilter("ACT ").Length / 2d);
            ProjectCoolness += Convert.ToInt16(DSGMlib.GetXDSFilter("SCRIPT ").Length * 10);
            ProjectCoolness += Convert.ToInt16(DSGMlib.GetXDSFilter("EVENT ").Length * 2);
            ProjectCoolness += Convert.ToInt16(DSGMlib.GetXDSFilter("EVENT ").Length * 2);
            ProjectCoolness -= Convert.ToInt16(DSGMlib.GetXDSFilter("SPRITE ").Length * 5);
            if (ProjectCoolness > 1000)
                ProjectCoolness = (short)Math.Round(ProjectCoolness / 10d);
            if (ProjectCoolness > 11)
                ProjectCoolness = (short)Math.Round(ProjectCoolness / 10d);
            CoolBar.Value = ProjectCoolness;
        }
    }
}