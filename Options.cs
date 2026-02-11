using System;
using System.Linq;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    public partial class Options
    {
        public Options()
        {
            InitializeComponent();
        }

        private void DOkayButton_Click(object sender, EventArgs e)
        {
            if (DefaultRoomWidthTB.Text.Length == 0 | DefaultRoomHeightTB.Text.Length == 0)
            {
                DS_Game_Maker.DSGMlib.MsgWarn("You must enter values for the Default Room Width and Height.");
                if (DefaultRoomHeightTB.Text.Length == 0)
                    DefaultRoomHeightTB.Focus();
                if (DefaultRoomWidthTB.Text.Length == 0)
                    DefaultRoomWidthTB.Focus();
                return;
            }
            bool IsNumber = true;

            foreach (char X in DefaultRoomWidthTB.Text.ToArray())
            {
                if (DSGMlib.Numbers.Contains(X.ToString()) == false)
                {
                    IsNumber = false;
                }

            }

            if (!IsNumber)
            {
                DS_Game_Maker.DSGMlib.MsgWarn("Default Room Width must be a number.");
                DefaultRoomWidthTB.Focus();
                return;
            }

            IsNumber = true;

            foreach (char X in DefaultRoomHeightTB.Text.ToArray())
            {
                if (DSGMlib.Numbers.Contains(X.ToString()) == false)
                {
                    IsNumber = false;
                }
            }

            if (!IsNumber)
            {
                DS_Game_Maker.DSGMlib.MsgWarn("Default Room Height must be a number.");
                DefaultRoomHeightTB.Focus();
                return;
            }

            if (Convert.ToInt16(DefaultRoomWidthTB.Text) < 256 | Convert.ToInt16(DefaultRoomWidthTB.Text) > 4096)
            {
                DS_Game_Maker.DSGMlib.MsgWarn("Default Room Width must be between 256 and 4096.");
                DefaultRoomWidthTB.Focus();
                return;
            }
            if (Convert.ToInt16(DefaultRoomHeightTB.Text) < 192 | Convert.ToInt16(DefaultRoomWidthTB.Text) > 4096)
            {
                DS_Game_Maker.DSGMlib.MsgWarn("Default Room Height must be between 192 and 4096.");
                DefaultRoomHeightTB.Focus();
                return;
            }
            DS_Game_Maker.SettingsLib.SetSetting("CLOSE_NEWS", CloseNewslineCheckBox.Checked ? "1" : "0");
            DS_Game_Maker.SettingsLib.SetSetting("OPEN_LAST_PROJECT_STARTUP", OpenLastProjectCheckBox.Checked ? "1" : "0");
            DS_Game_Maker.SettingsLib.SetSetting("SHOW_NEWS", ShowNewsCheckBox.Checked ? "1" : "0");
            DS_Game_Maker.SettingsLib.SetSetting("TRANSPARENT_ANIMATIONS", TransparentAnimationsCheckBox.Checked ? "1" : "0");
            DS_Game_Maker.SettingsLib.SetSetting("USE_NOGBA", UseNOGBARadioButton.Checked ? "1" : "0");
            DS_Game_Maker.SettingsLib.SetSetting("IMAGE_EDITOR_PATH", ImageEditorTextBox.Text);
            DS_Game_Maker.SettingsLib.SetSetting("SOUND_EDITOR_PATH", SoundEditorTextBox.Text);
            DS_Game_Maker.SettingsLib.SetSetting("USE_EXTERNAL_SCRIPT_EDITOR", UseExternalScriptEditorRadioButton.Checked ? "1" : "0");
            DS_Game_Maker.SettingsLib.SetSetting("HIGHLIGHT_CURRENT_LINE", HighlightCurrentLineCheckBox.Checked ? "1" : "0");
            DS_Game_Maker.SettingsLib.SetSetting("MATCH_BRACES", MatchBracesCheckBox.Checked ? "1" : "0");
            DS_Game_Maker.SettingsLib.SetSetting("HIDE_OLD_ACTIONS", HideOldActionsChecker.Checked ? "1" : "0");
            DS_Game_Maker.SettingsLib.SetSetting("SHRINK_ACTIONS_LIST", ShrinkActionsListChecker.Checked ? "1" : "0");
            DS_Game_Maker.SettingsLib.SetSetting("SCRIPT_EDITOR_PATH", ScriptEditorTextBox.Text);
            DS_Game_Maker.SettingsLib.SetSetting("DEFAULT_ROOM_WIDTH", DefaultRoomWidthTB.Text);
            DS_Game_Maker.SettingsLib.SetSetting("DEFAULT_ROOM_HEIGHT", DefaultRoomHeightTB.Text);
            DS_Game_Maker.SettingsLib.SetSetting("EMULATOR_PATH", CustomEmulatorTextBox.Text);
            DS_Game_Maker.SettingsLib.SaveSettings();
            foreach (Form X in Program.Forms.main_Form.MdiChildren)
            {
                string TheText = X.Text;
                if (TheText.StartsWith("Outputted C Preview for "))
                {
                    TheText = TheText.Substring(24);
                }
                if (!DS_Game_Maker.DSGMlib.DoesXDSLineExist("SCRIPT " + TheText))
                    continue;
                foreach (Control SC in X.Controls)
                {
                    if (SC.Name == "MainTextBox")
                    {
                        ((ScintillaNet.Scintilla)SC).Caret.HighlightCurrentLine = HighlightCurrentLineCheckBox.Checked;
                        ((ScintillaNet.Scintilla)SC).MatchBraces = MatchBracesCheckBox.Checked;
                    }
                }
            }
            Close();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            CloseNewslineCheckBox.Checked = DS_Game_Maker.SettingsLib.GetSetting("CLOSE_NEWS") == "1";
            OpenLastProjectCheckBox.Checked = DS_Game_Maker.SettingsLib.GetSetting("OPEN_LAST_PROJECT_STARTUP") == "1";
            ShowNewsCheckBox.Checked = DS_Game_Maker.SettingsLib.GetSetting("SHOW_NEWS") == "1";
            HighlightCurrentLineCheckBox.Checked = DS_Game_Maker.SettingsLib.GetSetting("HIGHLIGHT_CURRENT_LINE") == "1";
            MatchBracesCheckBox.Checked = DS_Game_Maker.SettingsLib.GetSetting("MATCH_BRACES") == "1";
            TransparentAnimationsCheckBox.Checked = DS_Game_Maker.SettingsLib.GetSetting("TRANSPARENT_ANIMATIONS") == "1";
            UseNOGBARadioButton.Checked = DS_Game_Maker.SettingsLib.GetSetting("USE_NOGBA") == "1";
            ImageEditorTextBox.Text = DS_Game_Maker.SettingsLib.GetSetting("IMAGE_EDITOR_PATH");
            SoundEditorTextBox.Text = DS_Game_Maker.SettingsLib.GetSetting("SOUND_EDITOR_PATH");
            CustomEmulatorTextBox.Text = DS_Game_Maker.SettingsLib.GetSetting("EMULATOR_PATH");
            if (DS_Game_Maker.SettingsLib.GetSetting("USE_EXTERNAL_SCRIPT_EDITOR") == "1")
            {
                UseExternalScriptEditorRadioButton.Checked = true;
                UseInternalScriptEditorRadioButton.Checked = false;
            }
            else
            {
                UseExternalScriptEditorRadioButton.Checked = false;
                UseInternalScriptEditorRadioButton.Checked = true;
            }
            ScriptEditorTextBox.Text = DS_Game_Maker.SettingsLib.GetSetting("SCRIPT_EDITOR_PATH");
            DefaultRoomWidthTB.Text = DS_Game_Maker.SettingsLib.GetSetting("DEFAULT_ROOM_WIDTH");
            DefaultRoomHeightTB.Text = DS_Game_Maker.SettingsLib.GetSetting("DEFAULT_ROOM_HEIGHT");
            HideOldActionsChecker.Checked = DS_Game_Maker.SettingsLib.GetSetting("HIDE_OLD_ACTIONS") == "1";
            ShrinkActionsListChecker.Checked = DS_Game_Maker.SettingsLib.GetSetting("SHRINK_ACTIONS_LIST") == "1";
        }

        private void ImageEditorBrowseButton_Click(object sender, EventArgs e)
        {
            ImageEditorTextBox.Text = DS_Game_Maker.DSGMlib.OpenFile(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Executables|*.exe");
        }

        private void SoundEditorBowseButton_Click(object sender, EventArgs e)
        {
            SoundEditorTextBox.Text = DS_Game_Maker.DSGMlib.OpenFile(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Executables|*.exe");
        }

        private void ScriptEditorBowseButton_Click(object sender, EventArgs e)
        {
            ScriptEditorTextBox.Text = DS_Game_Maker.DSGMlib.OpenFile(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Executables|*.exe");
        }

        private void CustomEmulatorBrowseButton_Click(object sender, EventArgs e)
        {
            CustomEmulatorTextBox.Text = DS_Game_Maker.DSGMlib.OpenFile(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Executables|*.exe");
        }
    }
}