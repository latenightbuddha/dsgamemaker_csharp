using System;
using System.Collections.Generic;
using System.Drawing;

namespace DS_Game_Maker
{
    public partial class TransformSprite
    {

        public List<string> ImagePaths = new List<string>();

        public TransformSprite()
        {
            InitializeComponent();
        }

        private void OriginalColorButton_Click(object sender, EventArgs e)
        {
            OriginalColorButton.BackColor = DS_Game_Maker.DSGMlib.SelectColor(OriginalColorButton.BackColor);
        }

        private void ReplaceButtonColor_Click(object sender, EventArgs e)
        {
            ReplaceColorButton.BackColor = DS_Game_Maker.DSGMlib.SelectColor(ReplaceColorButton.BackColor);
        }

        private void DOkayButton_Click(object sender, EventArgs e)
        {
            foreach (string X in ImagePaths)
            {
                Bitmap TheImage = (Bitmap)DS_Game_Maker.DSGMlib.PathToImage(X);
                if (MainTabControl.SelectedIndex == 1)
                {
                    var TheSize = DS_Game_Maker.DSGMlib.PathToImage(X).Size;
                    TheImage = new Bitmap(TheSize.Width, TheSize.Height);
                    var GFX = Graphics.FromImage(TheImage);
                    GFX.Clear(ReplaceColorButton.BackColor);
                    GFX.DrawImage(DS_Game_Maker.DSGMlib.MakeBMPTransparent(DS_Game_Maker.DSGMlib.PathToImage(X), OriginalColorButton.BackColor), new Point(0, 0));
                    GFX.Dispose();
                    TheImage.Save(X);
                    continue;
                }
                if (NoneRadioButton.Checked)
                {
                    if (RotationDropper.Text == "90")
                    {
                        TheImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    }
                    else if (RotationDropper.Text == "180")
                    {
                        TheImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    }
                    else if (RotationDropper.Text == "270")
                    {
                        TheImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    }
                    else if (RotationDropper.Text == "0")
                    {
                        TheImage.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                    }
                }
                else if (BothRadioButton.Checked)
                {
                    if (RotationDropper.Text == "90")
                    {
                        TheImage.RotateFlip(RotateFlipType.Rotate90FlipXY);
                    }
                    else if (RotationDropper.Text == "180")
                    {
                        TheImage.RotateFlip(RotateFlipType.Rotate180FlipXY);
                    }
                    else if (RotationDropper.Text == "270")
                    {
                        TheImage.RotateFlip(RotateFlipType.Rotate270FlipXY);
                    }
                    else if (RotationDropper.Text == "0")
                    {
                        TheImage.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                    }
                }
                else if (HorizontalRadioButton.Checked)
                {
                    if (RotationDropper.Text == "90")
                    {
                        TheImage.RotateFlip(RotateFlipType.Rotate90FlipX);
                    }
                    else if (RotationDropper.Text == "180")
                    {
                        TheImage.RotateFlip(RotateFlipType.Rotate180FlipX);
                    }
                    else if (RotationDropper.Text == "270")
                    {
                        TheImage.RotateFlip(RotateFlipType.Rotate270FlipX);
                    }
                    else if (RotationDropper.Text == "0")
                    {
                        TheImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    }
                }
                else if (VerticalRadioButton.Checked)
                {
                    if (RotationDropper.Text == "90")
                    {
                        TheImage.RotateFlip(RotateFlipType.Rotate90FlipY);
                    }
                    else if (RotationDropper.Text == "180")
                    {
                        TheImage.RotateFlip(RotateFlipType.Rotate180FlipY);
                    }
                    else if (RotationDropper.Text == "270")
                    {
                        TheImage.RotateFlip(RotateFlipType.Rotate270FlipY);
                    }
                    else if (RotationDropper.Text == "0")
                    {
                        TheImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    }
                }
                TheImage.Save(X);
            }
            Close();
        }

        private void TransformSprite_Load(object sender, EventArgs e)
        {

        }
    }
}