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
                DSGMlib.MsgWarn("You must enter values for the Default Room Width and Height.");
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
                DSGMlib.MsgWarn("Default Room Width must be a number.");
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
                DSGMlib.MsgWarn("Default Room Height must be a number.");
                DefaultRoomHeightTB.Focus();
                return;
            }

            if (Convert.ToInt16(DefaultRoomWidthTB.Text) < 256 | Convert.ToInt16(DefaultRoomWidthTB.Text) > 4096)
            {
                DSGMlib.MsgWarn("Default Room Width must be between 256 and 4096.");
                DefaultRoomWidthTB.Focus();
                return;
            }
            if (Convert.ToInt16(DefaultRoomHeightTB.Text) < 192 | Convert.ToInt16(DefaultRoomWidthTB.Text) > 4096)
            {
                DSGMlib.MsgWarn("Default Room Height must be between 192 and 4096.");
                DefaultRoomHeightTB.Focus();
                return;
            }
            SettingsLib.SetSetting("CLOSE_NEWS", CloseNewslineCheckBox.Checked ? "1" : "0");
            SettingsLib.SetSetting("OPEN_LAST_PROJECT_STARTUP", OpenLastProjectCheckBox.Checked ? "1" : "0");
            SettingsLib.SetSetting("SHOW_NEWS", ShowNewsCheckBox.Checked ? "1" : "0");
            SettingsLib.SetSetting("TRANSPARENT_ANIMATIONS", TransparentAnimationsCheckBox.Checked ? "1" : "0");
            SettingsLib.SetSetting("USE_NOGBA", UseNOGBARadioButton.Checked ? "1" : "0");
            SettingsLib.SetSetting("IMAGE_EDITOR_PATH", ImageEditorTextBox.Text);
            SettingsLib.SetSetting("SOUND_EDITOR_PATH", SoundEditorTextBox.Text);
            SettingsLib.SetSetting("USE_EXTERNAL_SCRIPT_EDITOR", UseExternalScriptEditorRadioButton.Checked ? "1" : "0");
            SettingsLib.SetSetting("HIGHLIGHT_CURRENT_LINE", HighlightCurrentLineCheckBox.Checked ? "1" : "0");
            SettingsLib.SetSetting("MATCH_BRACES", MatchBracesCheckBox.Checked ? "1" : "0");
            SettingsLib.SetSetting("HIDE_OLD_ACTIONS", HideOldActionsChecker.Checked ? "1" : "0");
            SettingsLib.SetSetting("SHRINK_ACTIONS_LIST", ShrinkActionsListChecker.Checked ? "1" : "0");
            SettingsLib.SetSetting("SCRIPT_EDITOR_PATH", ScriptEditorTextBox.Text);
            SettingsLib.SetSetting("DEFAULT_ROOM_WIDTH", DefaultRoomWidthTB.Text);
            SettingsLib.SetSetting("DEFAULT_ROOM_HEIGHT", DefaultRoomHeightTB.Text);
            SettingsLib.SetSetting("EMULATOR_PATH", CustomEmulatorTextBox.Text);
            SettingsLib.SaveSettings();
            foreach (Form X in Program.Forms.main_Form.MdiChildren)
            {
                string TheText = X.Text;
                if (TheText.StartsWith("Outputted C Preview for "))
                {
                    TheText = TheText.Substring(24);
                }
                if (!DSGMlib.DoesXDSLineExist("SCRIPT " + TheText))
                    continue;
                foreach (Control SC in X.Controls)
                {
                    if (SC.Name == "MainTextBox")
                    {
                        //((ScintillaNet.Scintilla)SC).Caret.HighlightCurrentLine = HighlightCurrentLineCheckBox.Checked;
                        //((ScintillaNet.Scintilla)SC).MatchBraces = MatchBracesCheckBox.Checked;
                    }
                }
            }
            Close();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            CloseNewslineCheckBox.Checked = SettingsLib.GetSetting("CLOSE_NEWS") == "1";
            OpenLastProjectCheckBox.Checked = SettingsLib.GetSetting("OPEN_LAST_PROJECT_STARTUP") == "1";
            ShowNewsCheckBox.Checked = SettingsLib.GetSetting("SHOW_NEWS") == "1";
            HighlightCurrentLineCheckBox.Checked = SettingsLib.GetSetting("HIGHLIGHT_CURRENT_LINE") == "1";
            MatchBracesCheckBox.Checked = SettingsLib.GetSetting("MATCH_BRACES") == "1";
            TransparentAnimationsCheckBox.Checked = SettingsLib.GetSetting("TRANSPARENT_ANIMATIONS") == "1";
            UseNOGBARadioButton.Checked = SettingsLib.GetSetting("USE_NOGBA") == "1";
            ImageEditorTextBox.Text = SettingsLib.GetSetting("IMAGE_EDITOR_PATH");
            SoundEditorTextBox.Text = SettingsLib.GetSetting("SOUND_EDITOR_PATH");
            CustomEmulatorTextBox.Text = SettingsLib.GetSetting("EMULATOR_PATH");
            if (SettingsLib.GetSetting("USE_EXTERNAL_SCRIPT_EDITOR") == "1")
            {
                UseExternalScriptEditorRadioButton.Checked = true;
                UseInternalScriptEditorRadioButton.Checked = false;
            }
            else
            {
                UseExternalScriptEditorRadioButton.Checked = false;
                UseInternalScriptEditorRadioButton.Checked = true;
            }
            ScriptEditorTextBox.Text = SettingsLib.GetSetting("SCRIPT_EDITOR_PATH");
            DefaultRoomWidthTB.Text = SettingsLib.GetSetting("DEFAULT_ROOM_WIDTH");
            DefaultRoomHeightTB.Text = SettingsLib.GetSetting("DEFAULT_ROOM_HEIGHT");
            HideOldActionsChecker.Checked = SettingsLib.GetSetting("HIDE_OLD_ACTIONS") == "1";
            ShrinkActionsListChecker.Checked = SettingsLib.GetSetting("SHRINK_ACTIONS_LIST") == "1";
        }

        private void ImageEditorBrowseButton_Click(object sender, EventArgs e)
        {
            ImageEditorTextBox.Text = DSGMlib.OpenFile(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Executables|*.exe");
        }

        private void SoundEditorBowseButton_Click(object sender, EventArgs e)
        {
            SoundEditorTextBox.Text = DSGMlib.OpenFile(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Executables|*.exe");
        }

        private void ScriptEditorBowseButton_Click(object sender, EventArgs e)
        {
            ScriptEditorTextBox.Text = DSGMlib.OpenFile(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Executables|*.exe");
        }

        private void CustomEmulatorBrowseButton_Click(object sender, EventArgs e)
        {
            CustomEmulatorTextBox.Text = DSGMlib.OpenFile(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Executables|*.exe");
        }
    }
}