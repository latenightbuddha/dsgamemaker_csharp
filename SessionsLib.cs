using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace DS_Game_Maker
{
    static class SessionsLib
    {

        public static string Session = string.Empty;
        public static string SessionPath = string.Empty;
        public static string CompileName = string.Empty;
        public static string CompilePath = string.Empty;

        public static object MakeSessionName()
        {
            byte ProcessCount = 0;
            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName.StartsWith(Application.ProductName))
                    ProcessCount = (byte)(ProcessCount + 1);
            }
            string Returnable = string.Empty;
            Returnable += DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            Returnable += ProcessCount.ToString();
            return Returnable;
        }

        public static void FormSession(string SessionName)
        {
            Session = SessionName;
            SessionPath = DS_Game_Maker.DSGMlib.AppPath + @"ProjectTemp\" + Session + @"\";
            CompileName = "DSGMTemp" + Session;
            CompilePath = DS_Game_Maker.DSGMlib.CDrive + CompileName + @"\";
        }

        public static void FormSessionFS()
        {
            Directory.CreateDirectory(SessionPath);
            Directory.CreateDirectory(SessionPath + "Sprites");
            Directory.CreateDirectory(SessionPath + "Backgrounds");
            Directory.CreateDirectory(SessionPath + "Sounds");
            Directory.CreateDirectory(SessionPath + "Scripts");
            Directory.CreateDirectory(SessionPath + "NitroFSFiles");
            Directory.CreateDirectory(SessionPath + "IncludeFiles");
            File.CreateText(SessionPath + "XDS.xds").Dispose();
            Directory.CreateDirectory(CompilePath);
            Directory.CreateDirectory(CompilePath + "data");
            Directory.CreateDirectory(CompilePath + "nitrofiles");
            Directory.CreateDirectory(CompilePath + "gfx");
            File.WriteAllBytes(CompilePath + @"gfx\PAGfx.exe", DS_Game_Maker.My.Resources.Resources.PAGfx);
            Directory.CreateDirectory(CompilePath + "include");
            File.WriteAllBytes(CompilePath + @"include\ActionWorks.h", DS_Game_Maker.My.Resources.Resources.ActionWorks);
            File.WriteAllBytes(CompilePath + @"include\ExtraDBAS.h", DS_Game_Maker.My.Resources.Resources.ExtraDBAS);
            File.WriteAllText(CompilePath + @"include\ActionWorks.h", DS_Game_Maker.DSGMlib.PathToString(CompilePath + @"include\ActionWorks.h") + Constants.vbCrLf + DS_Game_Maker.DSGMlib.PathToString(CompilePath + @"include\ExtraDBAS.h"));
            File.Delete(CompilePath + @"include\ExtraDBAS.h");
            Directory.CreateDirectory(CompilePath + "source");
            string Writeable = "make" + Constants.vbCrLf + "pause";
            File.WriteAllText(CompilePath + "build.bat", Writeable);
            Writeable = "make clean";
            File.WriteAllText(CompilePath + "clean.bat", Writeable);
            File.CreateText(CompilePath + @"source\main.c").Dispose();
            File.CreateText(CompilePath + @"gfx\dsgm_gfx.h").Dispose();

            File.Copy(DS_Game_Maker.DSGMlib.AppPath + "logo.bmp", CompilePath + "logo.bmp");

        }

    }
}