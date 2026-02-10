using System;
using System.Drawing;

namespace DS_Game_Maker
{
    public partial class ImportSprite
    {

        public string FileName = string.Empty;
        public string ToDirectory;
        public bool DidIt = false;
        public short ImportedCount = 0;
        public short FrameWidth = 32;
        public short FrameHeight = 32;
        private bool CustomHRow = false;
        private bool CustomVRow = false;

        public ImportSprite()
        {
            InitializeComponent();
        }

        public Bitmap RenderImage()
        {
            var TheSize = DS_Game_Maker.DSGMlib.PathToImage(FileName).Size;
            var Returnable = new Bitmap(TheSize.Width, TheSize.Height);
            var RGFX = Graphics.FromImage(Returnable);
            RGFX.DrawImage(DS_Game_Maker.DSGMlib.PathToImage(FileName), new Point(0, 0));
            bool XStartWhite = true;
            for (short Y = 0, loopTo = (short)Math.Round(ImagesPerColumnDropper.Value - 1m); Y <= loopTo; Y++)
            {
                var ThePen = XStartWhite ? Pens.LightGray : Pens.Black;
                bool DrawInWhite = XStartWhite;
                for (short X = 0, loopTo1 = (short)Math.Round(ImagesPerRowDropper.Value - 1m); X <= loopTo1; X++)
                {
                    if (DrawInWhite)
                        ThePen = Pens.LightGray;
                    else
                        ThePen = Pens.Black;
                    if (DrawInWhite)
                        DrawInWhite = false;
                    else
                        DrawInWhite = true;
                    var TP = new Point(X * FrameWidth, Y * FrameHeight);
                    RGFX.DrawRectangle(ThePen, new Rectangle(TP.X, TP.Y, FrameWidth - 1, FrameHeight - 1));
                }
                if (XStartWhite == true)
                    XStartWhite = false;
                else
                    XStartWhite = true;
            }
            return Returnable;
        }

        public short EquateHRow()
        {
            if (FileName.Length == 0)
                return 1;
            short HRow = (short)DS_Game_Maker.DSGMlib.PathToImage(FileName).Width;
            if (HRow % FrameWidth == 0)
            {
                return (short)Math.Round(HRow / (double)FrameWidth);
            }
            else
            {
                HRow = (short)(HRow + ((int)FrameWidth - DS_Game_Maker.DSGMlib.PathToImage(FileName).Width % (int)FrameWidth));
                HRow = (short)Math.Round(HRow / (double)FrameWidth);
            }
            return HRow;
        }

        public short EquateVRow()
        {
            if (FileName.Length == 0)
                return 1;
            short VRow = (short)DS_Game_Maker.DSGMlib.PathToImage(FileName).Height;
            if (VRow % FrameHeight == 0)
            {
                return (short)Math.Round(VRow / (double)FrameHeight);
            }
            else
            {
                VRow = (short)(VRow + (short)(FrameHeight - (short)(VRow % FrameHeight)));
                VRow = (short)Math.Round(VRow / (double)FrameHeight);
            }
            return VRow;
        }

        private void ImportSprite_Load(object sender, EventArgs e)
        {
            ImportedCount = 0;
            CustomHRow = false;
            CustomVRow = false;
        }

        public void RefreshPreview()
        {
            if (FileName.Length == 0)
                return;
            PreviewPanel.BackgroundImage = RenderImage();
        }

        private void DAcceptButton_Click(object sender, EventArgs e)
        {
            // TODO LATER Rows beyond the image width = bad frames!
            // Limit on selector, or ignore extra value?
            Bitmap TheImage = (Bitmap)DS_Game_Maker.DSGMlib.PathToImage(FileName);
            short DOn = 0;
            for (short Y = 0, loopTo = (short)Math.Round(ImagesPerColumnDropper.Value - 1m); Y <= loopTo; Y++)
            {
                for (short X = 0, loopTo1 = (short)Math.Round(ImagesPerRowDropper.Value - 1m); X <= loopTo1; X++)
                {
                    var LittleImage = new Bitmap(FrameWidth, FrameHeight);
                    var RGFX = Graphics.FromImage(LittleImage);
                    var TP = new Point(-1 * X * FrameWidth, -1 * Y * FrameHeight);
                    RGFX.DrawImage(TheImage, TP);
                    RGFX.Dispose();
                    LittleImage.Save(ToDirectory + @"\" + DOn.ToString() + ".png");
                    DOn = (short)(DOn + 1);
                }
            }
            DidIt = true;
            ImportedCount = DOn;
            Close();
        }

        private void ImagesPerRowDropper_ValueChanged(object sender, EventArgs e)
        {
            CustomHRow = true;
            RefreshPreview();
        }

        private void Droppers_ValuesChanged(object sender, EventArgs e)
        {
            if (FrameWidthDropper.Text.Length > 0)
                FrameWidth = Convert.ToByte(FrameWidthDropper.Text);
            if (FrameHeightDropper.Text.Length > 0)
                FrameHeight = Convert.ToByte(FrameHeightDropper.Text);
            if (FrameWidthDropper.Text.Length > 0 & FrameHeightDropper.Text.Length > 0)
            {
                if (!CustomHRow)
                    ImagesPerRowDropper.Value = EquateHRow();
                if (!CustomVRow)
                    ImagesPerColumnDropper.Value = EquateVRow();
            }
            RefreshPreview();
        }

        private void ImportSprite_Activated(object sender, EventArgs e)
        {
            ImagesPerRowDropper.Value = EquateHRow();
            ImagesPerColumnDropper.Value = EquateVRow();
            RefreshPreview();
        }

        private void ImagesPerColumnDropper_ValueChanged(object sender, EventArgs e)
        {
            CustomVRow = true;
            RefreshPreview();
        }
    }
}