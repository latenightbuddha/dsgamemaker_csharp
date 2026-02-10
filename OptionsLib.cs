using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic; // Install-Package Microsoft.VisualBasic

internal static partial class OptionsLib
{

    public static List<string> OptionNames = new List<string>();
    public static List<string> OptionValues = new List<string>();
    public static string OptionPath = string.Empty;

    public static void LoadOptions()
    {
        OptionNames.Clear();
        OptionValues.Clear();
        foreach (string OL in File.ReadAllLines(OptionPath))
        {
            string OName = OL.Substring(0, OL.IndexOf(" "));
            string OValue = OL.Substring(OL.IndexOf(" ") + 1);
            OptionNames.Add(OName);
            OptionValues.Add(OValue);
        }
    }

    public static void SaveOptions()
    {
        string FinalString = string.Empty;
        for (ushort I = 0, loopTo = (ushort)(OptionNames.Count - 1); I <= loopTo; I++)
            FinalString += OptionNames[I] + " " + OptionValues[I] + Constants.vbCrLf;
        File.WriteAllText(OptionPath, FinalString);
    }

    public static string GetOption(string SettingName)
    {
        for (byte SettingNo = 0, loopTo = (byte)(OptionNames.Count - 1); SettingNo <= loopTo; SettingNo++)
        {
            if ((OptionNames[SettingNo] ?? "") == (SettingName ?? ""))
                return OptionValues[SettingNo];
        }
        return string.Empty;
    }

    public static void SetOption(string OptionName, string OptionValue)
    {
        var BackupValues = new List<string>();
        for (byte SettingNo = 0, loopTo = (byte)(OptionNames.Count - 1); SettingNo <= loopTo; SettingNo++)
        {
            if ((OptionNames[SettingNo] ?? "") == (OptionName ?? ""))
            {
                BackupValues.Add(OptionValue);
            }
            else
            {
                BackupValues.Add(OptionValues[SettingNo]);
            }
        }
        OptionValues.Clear();
        for (ushort I = 0, loopTo1 = (ushort)(BackupValues.Count - 1); I <= loopTo1; I++)
            OptionValues.Add(BackupValues[I]);
        BackupValues.Clear();
    }

    public static void PatchOption(string OName, string OValue)
    {
        bool DoTheAdd = true;
        string FS = string.Empty;
        foreach (string OL in File.ReadAllLines(OptionPath))
        {
            if (OL.StartsWith(OName + " "))
                DoTheAdd = false;
            FS += OL + Constants.vbCrLf;
        }
        if (DoTheAdd)
        {
            FS += OName + " " + OValue;
            File.WriteAllText(OptionPath, FS);
        }
    }


}