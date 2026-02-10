using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    public partial class DEvent
    {

        public string MainClass;
        public string SubClass;
        public bool UseData;
        private string OwnedBy;

        public DEvent()
        {
            InitializeComponent();
        }

        private void DCancelButton_Click()
        {
            MainClass = "NoData";
            SubClass = "NoData";
            Close();
        }

        private void DEvent_Load(object sender, EventArgs e)
        {
            Dropper.Renderer = new DS_Game_Maker.clsMenuRenderer();
            UseData = false;
        }

        public void EquateDropper()
        {
            Dropper.Items.Clear();
            var NewItems = new List<string>();
            switch (DS_Game_Maker.ScriptsLib.MainClassStringToType(MainClass))
            {
                case (byte)2:
                case (byte)3:
                case (byte)4:
                    {
                        NewItems.Add("A");
                        NewItems.Add("B");
                        NewItems.Add("X");
                        NewItems.Add("Y");
                        NewItems.Add("L");
                        NewItems.Add("R");
                        NewItems.Add("Up");
                        NewItems.Add("Down");
                        NewItems.Add("Left");
                        NewItems.Add("Right");
                        NewItems.Add("Start");
                        NewItems.Add("Select");
                        break;
                    }
                case (byte)5:
                    {
                        NewItems.Add("New Press");
                        NewItems.Add("Double Press");
                        NewItems.Add("Held");
                        NewItems.Add("Released");
                        break;
                    }
                case (byte)6:
                    {
                        foreach (string X_ in DS_Game_Maker.DSGMlib.GetXDSFilter("OBJECT "))
                        {
                            string X = X_;
                            X = X.Substring(7);
                            string ObjectName = X.Substring(0, X.IndexOf(","));
                            // If ObjectName = OwnedBy Then Continue For
                            NewItems.Add(ObjectName);
                        }

                        break;
                    }
            }
            foreach (string X in NewItems)
                Dropper.Items.Add(X);
        }

        private void EventButtons_Click(object sender, EventArgs e)
        {
            MainClass = ((Button)sender).Text.Substring(6);
            byte X = DS_Game_Maker.ScriptsLib.MainClassStringToType(MainClass);
            if (X == 1 | X == 7)
            {
                SubClass = "NoData";
                UseData = true;
                Close();
                return;
            }
            var PT = ((Button)sender).Location;
            EquateDropper();
            Dropper.Show((Control)sender, 0, ((Control)sender).Height + 2);
        }

        private void EventDropper_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            SubClass = e.ClickedItem.Text;
            UseData = true;
            Close();
        }

        private void DEvent_FormClosing(object sender, FormClosingEventArgs e)
        {
            // MainClass = "NoData"
            // SubClass = "NoData"
        }

        private void DCancelButton_Click(object sender, EventArgs e) => DCancelButton_Click();
    }
}