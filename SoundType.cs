using System;

namespace DS_Game_Maker
{
    public partial class SoundType
    {

        public bool IsSoundEffect;

        public SoundType()
        {
            InitializeComponent();
        }

        private void SoundEffectRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SoundEffectRadioButton.Checked)
            {
                InfoLabel.Text = "A WAV sound file that can play along side other effects and on top of background sound (MP3).";
            }
            else
            {
                InfoLabel.Text = "A streamed MP3 sound file that plays underneath sound effects.";
            }
        }

        private void DOkayButton_Click(object sender, EventArgs e)
        {
            IsSoundEffect = SoundEffectRadioButton.Checked;
            Close();
        }

        private void SoundType_Load(object sender, EventArgs e)
        {
            SoundEffectRadioButton.Checked = true;
        }
    }
}