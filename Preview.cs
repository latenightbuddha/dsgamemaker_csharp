using System;
using System.Drawing;

namespace DS_Game_Maker
{
    public partial class Preview
    {

        public Image TheImage;
        public Size ImageSize = new Size();
        public byte Speed;
        private byte FrameOn = 0;
        private byte FrameCount = 0;

        public Preview()
        {
            InitializeComponent();
        }

        public Bitmap GetFrame(short FrameNumber)
        {
            var Returnable = new Bitmap(ImageSize.Width, ImageSize.Height);
            var NewGFX = Graphics.FromImage(Returnable);
            NewGFX.DrawImage(TheImage, new Point(0, FrameNumber * ImageSize.Height * -1));
            NewGFX.Dispose();
            if ((int)Convert.ToByte(DS_Game_Maker.SettingsLib.GetSetting("TRANSPARENT_ANIMATIONS")) == 1)
                Returnable = (Bitmap)DS_Game_Maker.DSGMlib.MakeBMPTransparent(Returnable, Color.Magenta);
            return Returnable;
        }

        private void DCloseButton_Click(object sender, EventArgs e)
        {
            MainTimer.Enabled = false;
            Close();
        }

        private void Preview_Load(object sender, EventArgs e)
        {
            FrameOn = 0;
            byte WaitTime = (byte)Math.Round(60d / Speed);
            MainTimer.Interval = (int)Math.Round(WaitTime / 60d * 1000d);
            FrameCount = (byte)Math.Round(TheImage.Height / (double)ImageSize.Height);
            MainTimer.Enabled = true;
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            FrameOn = (byte)(FrameOn + 1);
            // MsgError("Showing " + FrameOn.ToString)
            if (FrameOn == FrameCount)
                FrameOn = 0;
            PreviewPanel.BackgroundImage = GetFrame(FrameOn);
        }
    }
}