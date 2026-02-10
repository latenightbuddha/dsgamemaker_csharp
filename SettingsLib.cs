using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic;

namespace DS_Game_Maker
{
    static class SettingsLib
    {

        public static List<string> SettingNames = new List<string>();
        public static List<string> SettingValues = new List<string>();
        public static string SettingsPath = string.Empty;

        public static void LoadSettings()
        {
            SettingNames.Clear();
            SettingValues.Clear();
            foreach (string SettingLine in File.ReadAllLines(SettingsPath))
            {
                string SettingName = SettingLine.Substring(0, SettingLine.IndexOf(" "));
                string SettingValue = SettingLine.Substring(SettingLine.IndexOf(" ") + 1);
                SettingNames.Add(SettingName);
                SettingValues.Add(SettingValue);
            }
        }

        public static void SaveSettings()
        {
            string FinalString = string.Empty;
            for (byte SettingNo = 0, loopTo = (byte)(SettingNames.Count - 1); SettingNo <= loopTo; SettingNo++)
                FinalString += SettingNames[SettingNo] + " " + SettingValues[SettingNo] + Constants.vbCrLf;
            File.WriteAllText(SettingsPath, FinalString);
        }

        public static string GetSetting(string SettingName)
        {
            for (byte SettingNo = 0, loopTo = (byte)(SettingNames.Count - 1); SettingNo <= loopTo; SettingNo++)
            {
                if ((SettingNames[SettingNo] ?? "") == (SettingName ?? ""))
                    return SettingValues[SettingNo];
            }
            return string.Empty;
        }

        public static void SetSetting(string SettingName, string SettingValue)
        {
            var BackupValues = new List<string>();
            for (byte SettingNo = 0, loopTo = (byte)(SettingNames.Count - 1); SettingNo <= loopTo; SettingNo++)
            {
                if ((SettingNames[SettingNo] ?? "") == (SettingName ?? ""))
                {
                    BackupValues.Add(SettingValue);
                }
                else
                {
                    BackupValues.Add(SettingValues[SettingNo]);
                }
            }
            SettingValues.Clear();
            for (byte SettingNo = 0, loopTo1 = (byte)(BackupValues.Count - 1); SettingNo <= loopTo1; SettingNo++)
                SettingValues.Add(BackupValues[SettingNo]);
            BackupValues.Clear();
        }

    }
}