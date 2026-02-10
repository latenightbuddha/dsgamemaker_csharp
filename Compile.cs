using System;

namespace DS_Game_Maker
{
    public partial class Compile
    {

        public bool HasDoneIt;
        public bool Success;

        public Compile()
        {
            InitializeComponent();
        }

        public void CustomPerformStep(string Stage)
        {
            System.Threading.Thread.Sleep(100);
            // ProgressLabel.Text = Stage
            MainProgressBar.PerformStep();
            // ProgressLabel.Invalidate()
            MainProgressBar.Invalidate();
            System.Threading.Thread.Sleep(100);
        }

        private void Compile_Shown(object sender, EventArgs e)
        {
            DS_Game_Maker.DSGMlib.CompileWrapper();
            Success = DS_Game_Maker.DSGMlib.CompileGame();
            Close();
        }

    }
}