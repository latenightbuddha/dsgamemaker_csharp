using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic;

namespace DS_Game_Maker
{

    public partial class Sound
    {

        public string SoundName;
        private bool SoundType;
        private string XDSLine;
        private string OldPath;
        private string NewPath;
        private string SoundTypeString;
        private SoundPlayer P = new SoundPlayer();
        private Process Proc;
        private ProcessStartInfo SProc;

        public Sound()
        {
            InitializeComponent();
        }

        private void Sound_Load(object sender, EventArgs e)
        {
            MainToolStrip.Renderer = new DS_Game_Maker.clsToolstripRenderer();
            Text = SoundName;
            NameTextBox.Text = SoundName;
            XDSLine = DS_Game_Maker.DSGMlib.GetXDSLine("SOUND " + SoundName + ",");
            if (DS_Game_Maker.DSGMlib.iGet(XDSLine, (byte)1, ",") == "1")
                SoundType = true;
            else
                SoundType = false;
            SoundTypeString = "." + (SoundType ? "mp3" : "wav");
            OldPath = DS_Game_Maker.SessionsLib.SessionPath + @"Sounds\" + SoundName + SoundTypeString;
            NewPath = OldPath;
            InfoLabel.Text = SoundType ? "Background Sound" : "Sound Effect";
            if (!SoundType)
            {
                P.SoundLocation = NewPath;
            }
            else
            {
                Proc = new Process();
                SProc = new ProcessStartInfo("wmplayer", "\"" + NewPath + "\"");
                Proc.StartInfo = SProc;
            }
        }

        private void DAcceptButton_Click(object sender, EventArgs e)
        {
            string NewName = NameTextBox.Text;
            if (!((NewName ?? "") == (SoundName ?? "")))
            {
                if (DS_Game_Maker.DSGMlib.GUIResNameChecker(NewName))
                    return;
            }
            if (!SoundType)
                P.Stop();
            try
            {
                Proc.Dispose();
            }
            catch
            {
            }
            P.Dispose();
            if (!((NewName ?? "") == (SoundName ?? "")))
            {
                string NewLine = "SOUND " + NewName + "," + (SoundType ? "1" : "0");
                DS_Game_Maker.DSGMlib.XDSChangeLine(XDSLine, NewLine);
            }
            if (!((OldPath ?? "") == (NewPath ?? "")))
            {
                File.Delete(OldPath);
                File.Copy(NewPath, OldPath);
            }
            string SoundTypeString = "." + (SoundType ? "mp3" : "wav");
            if (!((NewName ?? "") == (SoundName ?? "")))
            {
                File.Move(DS_Game_Maker.SessionsLib.SessionPath + @"Sounds\" + SoundName + SoundTypeString, DS_Game_Maker.SessionsLib.SessionPath + @"Sounds\" + NewName + SoundTypeString);
                if (SoundType)
                {
                    if (File.Exists(DS_Game_Maker.SessionsLib.CompilePath + @"nitrofiles\" + SoundName + ".mp3"))
                    {
                        File.Move(DS_Game_Maker.SessionsLib.CompilePath + @"nitrofiles\" + SoundName + ".mp3", DS_Game_Maker.SessionsLib.CompilePath + @"nitrofiles\" + NewName + ".mp3");
                    }
                }
                else if (File.Exists(DS_Game_Maker.SessionsLib.CompilePath + @"data\" + SoundName + ".raw"))
                {
                    File.Move(DS_Game_Maker.SessionsLib.CompilePath + @"data\" + SoundName + ".raw", DS_Game_Maker.SessionsLib.CompilePath + @"nitrofiles\" + NewName + ".raw");
                }
                if (DS_Game_Maker.DSGMlib.SoundsToRedo.Contains(SoundName))
                {
                    DS_Game_Maker.DSGMlib.SoundsToRedo.Remove(SoundName);
                    DS_Game_Maker.DSGMlib.SoundsToRedo.Add(NewName);
                }
            }
            foreach (TreeNode X in Program.Forms.main_Form.ResourcesTreeView.Nodes[(int)DS_Game_Maker.DSGMlib.ResourceIDs.Sound].Nodes)
            {
                if ((X.Text ?? "") == (SoundName ?? ""))
                    X.Text = NewName;
            }
            Close();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            string Filter = (SoundType ? "MP3" : "WAV") + " Files|*." + (SoundType ? "mp3" : "wav");
            string Result = DS_Game_Maker.DSGMlib.OpenFile(string.Empty, Filter);
            if (Result.Length == 0)
                return;
            NewPath = Result;
            if (!SoundType)
                P.SoundLocation = Result;
            DS_Game_Maker.DSGMlib.AddSoundToRedo(SoundName);
            if (SoundType)
            {
                SProc.Arguments = "\"" + NewPath + "\"";
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            string FinalEXE = DS_Game_Maker.SettingsLib.GetSetting("SOUND_EDITOR_PATH");
            if (FinalEXE.Length == 0)
            {
                DS_Game_Maker.DSGMlib.MsgWarn("No Sound Editor has been defined. See 'Options'.");
                return;
            }
            if (!File.Exists(FinalEXE))
            {
                DS_Game_Maker.DSGMlib.MsgWarn("Your Sound Editor EXE is not present. See 'Options'.");
                return;
            }
            string ThePath = DS_Game_Maker.SessionsLib.SessionPath + @"Sounds\" + SoundName + SoundTypeString;
            string CopyPath = DS_Game_Maker.SessionsLib.SessionPath + @"Sounds\" + SoundName + "_Copy" + SoundTypeString;
            try
            {
                if (File.Exists(CopyPath))
                    File.Delete(CopyPath);
                File.Copy(ThePath, CopyPath);
            }
            catch (Exception ex)
            {
                DS_Game_Maker.DSGMlib.MsgError("This sound cannot be edited because its file is locked." + Constants.vbCrLf + Constants.vbCrLf + "(" + ex.Message + ")");
                return;
            }
            if (!DS_Game_Maker.DSGMlib.EditSound(CopyPath, SoundName))
            {
                File.Delete(CopyPath);
                return;
            }
            File.Delete(ThePath);
            File.Move(CopyPath, ThePath);
            DS_Game_Maker.DSGMlib.AddSoundToRedo(SoundName);
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            if (!SoundType)
            {
                P.Play();
            }
            else
            {
                Proc.Start();
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            if (!SoundType)
            {
                P.Stop();
            }
            else
            {
                try
                {
                    Proc.Kill();
                }
                catch
                {
                }
            }
        }

    }
}