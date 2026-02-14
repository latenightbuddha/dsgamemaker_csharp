using System.ComponentModel.Design;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace DS_Game_Maker
{

    static class DSGMlib
    {

        public static string[] ResourceTypes = new string[7];

        public static short IDVersion = 530;
        public static string Domain = "https://ewaygames.com/dsgm/";
        public static short UpdateVersion = 0;

        public static Bitmap ActionBG = new Bitmap(32, 32);
        public static Bitmap ActionConditionalBG = new Bitmap(32, 32);

        public static string ProjectPath = string.Empty;

        public static string CacheProjectName = string.Empty;
        public static bool BeingUsed = false;
        public static string CurrentXDS = string.Empty;

        public static List<string> FontNames = new List<string>();

		public static string[] BannedChars = new string[] { " ", ",", ".", ";", "/", @"\", "!", "\"", "(", ")", "£", "$", "%", "^", "&", "*", "{", "}", "[", "]", "@", "#", "'", "~", "<", ">", "?", "+", "=", "-", "|", "¬", "`" };
		public static string[] Numbers = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        public static string ImageFilter = "Graphics|*.png; *.gif; *.bmp|PNG Images|*.png|GIF Images|*.gif|Bitmap Images|*.bmp";

        public static string LoadDefaultFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public static string SaveDefaultFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public static bool RedoAllGraphics = false;
        public static bool RedoSprites = false;
        public static List<string> BGsToRedo = new List<string>();
        public static List<string> FontsUsedLastTime = new List<string>();
        public static List<string> SoundsToRedo = new List<string>();

        public static bool IsNewProject = false;

        public static string[] ProActions = new string[] { "Enable Rotation", "Set Angle", "Disable Rotation" };
        // Public ProActions() As String = {}

        public static string LastResourceFound = string.Empty;
        public static string LastScriptTermFound = string.Empty;

        public static void FindResource(string ResourceName)
        {
            LastResourceFound = ResourceName;
            if (GetXDSFilter("SPRITE " + ResourceName + ",").Length > 0)
            {
                OpenResource(ResourceName, (byte)ResourceIDs.Sprite, false);
                return;
            }
            else if (DoesXDSLineExist("BACKGROUND " + ResourceName))
            {
                OpenResource(ResourceName, (byte)ResourceIDs.Background, false);
                return;
            }
            else if (GetXDSFilter("SCRIPT " + ResourceName + ",").Length > 0)
            {
                OpenResource(ResourceName, (byte)ResourceIDs.Script, false);
                return;
            }
            else if (DoesXDSLineExist("PATH " + ResourceName))
            {
                OpenResource(ResourceName, (byte)ResourceIDs.Path, false);
                return;
            }
            else if (GetXDSFilter("ROOM " + ResourceName + ",").Length > 0)
            {
                OpenResource(ResourceName, (byte)ResourceIDs.Room, false);
                return;
            }
            else if (GetXDSFilter("OBJECT " + ResourceName + ",").Length > 0)
            {
                OpenResource(ResourceName, (byte)ResourceIDs.DObject, false);
                return;
            }
            else if (GetXDSFilter("SOUND " + ResourceName + ",").Length > 0)
            {
                OpenResource(ResourceName, (byte)ResourceIDs.Sound, false);
                return;
            }
            MsgInfo("There is no Resource named '" + ResourceName + "'.");
        }

        public static bool IsObject(string ThingyName)
        {
            if (GetXDSFilter("OBJECT " + ThingyName + ",").Length > 0)
                return true;
            else
                return false;
        }

        public static bool IsBG(string ThingyName)
        {
            return DoesXDSLineExist("BACKGROUND " + ThingyName);
        }

        public static bool IsRoom(string ThingyName)
        {
            if (GetXDSFilter("ROOM " + ThingyName + ",").Length > 0)
                return true;
            else
                return false;
        }

        public static string ResurrectResourceName(string ResourceName)
        {
            string Returnable = ResourceName;
            foreach (string BannedChar in BannedChars)
                Returnable = Returnable.Replace(BannedChar, string.Empty);
            return Returnable;
        }

        public static Bitmap GenerateDSSprite(string TheSpriteName)
        {
            string Folder = SessionsLib.SessionPath + "Sprites/";
            byte ImageCount = 0;
            var Images = new List<string>();
            foreach (string X_ in Directory.GetFiles(Folder))
            {
                string X = X_;
                X = X.Substring(X.LastIndexOf("/") + 1);
                X = X.Substring(0, X.IndexOf(".png"));
                X = X.Substring(X.IndexOf("_") + 1);
                if (X == TheSpriteName)
                    ImageCount = (byte)(ImageCount + 1);
            }
            for (int X = 0, loopTo = ImageCount - 1; X <= loopTo; X++)
                Images.Add(Folder + X.ToString() + "_" + TheSpriteName + ".png");
            var TheSize = PathToImage(Images[0]).Size;
            var Returnable = new Bitmap(TheSize.Width, TheSize.Height * Images.Count);
            var TempGFX = Graphics.FromImage(Returnable);
            short DOn = 0;
            foreach (string X in Images)
            {
                TempGFX.DrawImage(PathToImage(X), new Point(0, DOn * TheSize.Height));
                DOn = (short)(DOn + 1);
            }
            TempGFX.Dispose();
            return Returnable;
        }

        public static object MakeSpaces(byte HowMany)
        {
            string Returnable = string.Empty;
            if (HowMany == 0)
                return Returnable;
            for (byte X = 0, loopTo = (byte)(HowMany - 1); X <= loopTo; X++)
                Returnable += " ";
            return Returnable;
        }

        public static List<string> Fonts = new List<string>();

        public static void RebuildFontCache()
        {
            Fonts.Clear();
            foreach (string X in Directory.GetFiles(Constants.AppDirectory + "Fonts"))
            {
                if (!X.EndsWith(".png"))
                    continue;
                string FontName = X.Substring(X.LastIndexOf("/") + 1);
                FontName = FontName.Substring(0, FontName.Length - 4);
                Fonts.Add(FontName);
            }
        }

        public static void OpenResource(string ResourceName, byte ResourceType, bool SpriteDataChanged)
        {
            foreach (Form TheForm in Program.Forms.main_Form.MdiChildren)
            {
                if (TheForm.Text == ResourceName)
                {
                    TheForm.Focus();
                    return;
                }
            }
            switch (ResourceType)
            {
                case (byte)ResourceIDs.Sprite:
                    {
                        var SpriteForm = new Sprite();
                        SpriteForm.SpriteName = ResourceName;
                        SpriteForm.DataChanged = SpriteDataChanged;
                        object argInstance = (object)SpriteForm;
                        ShowInternalForm(ref argInstance);
                        SpriteForm = (Sprite)argInstance;
                        break;
                    }
                case (byte)ResourceIDs.DObject:
                    {
                        var ObjectForm = new DObject();
                        ObjectForm.ObjectName = ResourceName;
                        object argInstance1 = (object)ObjectForm;
                        ShowInternalForm(ref argInstance1);
                        ObjectForm = (DObject)argInstance1;
                        break;
                    }
                case (byte)ResourceIDs.Background:
                    {
                        var BGForm = new Background();
                        BGForm.BackgroundName = ResourceName;
                        object argInstance2 = (object)BGForm;
                        ShowInternalForm(ref argInstance2);
                        BGForm = (Background)argInstance2;
                        break;
                    }
                case (byte)ResourceIDs.Sound:
                    {
                        var SoundForm = new Sound();
                        SoundForm.SoundName = ResourceName;
                        object argInstance3 = (object)SoundForm;
                        ShowInternalForm(ref argInstance3);
                        SoundForm = (Sound)argInstance3;
                        break;
                    }
                case (byte)ResourceIDs.Room:
                    {
                        var RoomForm = new Room();
                        RoomForm.RoomName = ResourceName;
                        object argInstance4 = (object)RoomForm;
                        ShowInternalForm(ref argInstance4);
                        RoomForm = (Room)argInstance4;
                        break;
                    }
                case (byte)ResourceIDs.Script:
                    {
                        var ScriptForm = new Script();
                        ScriptForm.ScriptName = ResourceName;
                        object argInstance5 = (object)ScriptForm;
                        ShowInternalForm(ref argInstance5);
                        ScriptForm = (Script)argInstance5;
                        break;
                    }
            }
        }

        public static object CompileWrapper()
        {
            if (!Directory.Exists(Constants.AppDirectory + "devkitPro/devkitARM"))
            {
                MsgError("The development toolchain was not found." + Constants.vbCrLf + Constants.vbCrLf + "Please restart " + Application.ProductName + ".");
                return false;
            }
            return true;
        }

        public static void CompileFail()
        {
            MsgError("Your game was not successfully compiled and could therefore not be run." + Constants.vbCrLf + Constants.vbCrLf + "Look for logic errors in your game and try again.");
        }

        public static object SillyFixMe(string Input)
        {
            if (Input.Length > 5 & Input.Substring(0, Input.Length - 5).StartsWith("this"))
            {
                Input = Input.Substring(0, Input.Length - 1);
            }
            else if (char.IsNumber(Input[Input.Length - 1]) == false & char.IsLetter(Input[Input.Length - 1]) == false)
            {
                Input = Input.Substring(0, Input.Length - 1);
            }

            return Input;
        }

        public static object GetTime()
        {
            string Returnable = DateTime.Now.Hour.ToString() + ":";
            if (DateTime.Now.Minute == 0)
                Returnable += "00";
            if (DateTime.Now.Minute > 0 & DateTime.Now.Minute < 10)
                Returnable += "0" + DateTime.Now.Minute.ToString();
            if (DateTime.Now.Minute >= 10)
                Returnable += DateTime.Now.Minute.ToString();
            return Returnable;
        }

        public static bool CompileGame()
        {
            Program.Forms.main_Form.compileForm.CustomPerformStep("Cleaning Temporary Data");
            File.Delete(SessionsLib.CompilePath + "DSGMTemp" + SessionsLib.Session + ".nds");
            foreach (Process TheProcess in Process.GetProcesses())
            {
                if (TheProcess.ProcessName.ToLower() == "pagfx" | TheProcess.ProcessName.ToLower() == "make")
                {
                    TheProcess.Kill();
                }
            }
            var FontsUsedThisTime = new List<string>();
            FontsUsedThisTime.Add("Default");
            foreach (string X_ in GetXDSFilter("ACT "))
            {
                string X = X_;
                if (!X.Contains(",Change Font,"))
                    continue;
                X = X.Substring(X.IndexOf(";") + 1);
                X = X.Substring(0, X.LastIndexOf(","));
                if (!FontsUsedThisTime.Contains(X))
                    FontsUsedThisTime.Add(X);
            }
            if (!Directory.Exists(SessionsLib.CompilePath + "gfx/bin"))
            {
                Directory.CreateDirectory(SessionsLib.CompilePath + "gfx/bin");
            }
            // Remove fonts not used this time
            foreach (string X in FontsUsedLastTime)
            {
                if (!FontsUsedThisTime.Contains(X))
                {
                    File.Delete(SessionsLib.CompilePath + "gfx/bin/" + X + ".c");
                    File.Delete(SessionsLib.CompilePath + "gfx/bin/" + X + "_Map.bin");
                    File.Delete(SessionsLib.CompilePath + "gfx/bin/" + X + "_Tiles.bin");
                    File.Delete(SessionsLib.CompilePath + "gfx/bin/" + X + "_Pal.bin");
                }
            }
            string FontH = string.Empty;
            FontH += "#pragma once" + Constants.vbCrLf;
            FontH += "#include <PA_BgStruct.h>" + Constants.vbCrLf;
            // FontH += "#ifdef __cplusplus" + vbcrlf
            // FontH += "  extern ""C"" {" + vbcrlf
            // FontH += "#endif" + vbcrlf
            // MsgError("fonts used last time")
            // For Each Y As String In FontsUsedLastTime
            // MsgError(Y)
            // Next
            foreach (string X in FontsUsedThisTime)
            {
                // If there's a font in this time's that's not in last times...
                if (!FontsUsedLastTime.Contains(X))
                {
                    File.Copy(Constants.AppDirectory + "CompiledBINs/" + X + ".c", SessionsLib.CompilePath + "gfx/bin/" + X + ".c", true);
                    File.Copy(Constants.AppDirectory + "CompiledBINs/" + X + "_Map.bin", SessionsLib.CompilePath + "gfx/bin/" + X + "_Map.bin", true);
                    File.Copy(Constants.AppDirectory + "CompiledBINs/" + X + "_Tiles.bin", SessionsLib.CompilePath + "gfx/bin/" + X + "_Tiles.bin", true);
                    File.Copy(Constants.AppDirectory + "CompiledBINs/" + X + "_Pal.bin", SessionsLib.CompilePath + "gfx/bin/" + X + "_Pal.bin", true);
                }
                FontH += "extern const PA_BgStruct " + X + ";" + Constants.vbCrLf;
            }
            // FontH += "#ifdef __cplusplus" + vbcrlf
            // FontH += "  }" + vbcrlf
            // FontH += "#endif"
            File.WriteAllText(SessionsLib.CompilePath + "gfx/custom_gfx.h", FontH);
            FileInfo[] MyFiles;
            if (RedoAllGraphics)
            {
                MyFiles = new DirectoryInfo(SessionsLib.CompilePath + "gfx").GetFiles();
                foreach (FileInfo dra in MyFiles)
                {
                    if (dra.Name.ToLower() == "pagfx.exe" | dra.Name.ToLower() == "custom_gfx.h")
                        continue;
                    File.Delete(dra.FullName);
                }
            }
            MyFiles = new DirectoryInfo(SessionsLib.CompilePath + "data").GetFiles();
            foreach (FileInfo TheFile in MyFiles)
            {
                if (TheFile.Name.EndsWith(".raw"))
                    continue;
                File.Delete(TheFile.FullName);
            }
            MyFiles = new DirectoryInfo(SessionsLib.CompilePath + "nitrofiles").GetFiles();
            foreach (FileInfo TheFile in MyFiles)
            {
                if (TheFile.FullName.EndsWith(".mp3"))
                {
                    bool IsMyMP3 = false;
                    string RName = TheFile.FullName;
                    RName = RName.Substring(RName.LastIndexOf("/") + 1);
                    RName = RName.Substring(0, RName.LastIndexOf("."));
                    if (IsAlreadyResource(RName).Length > 0)
                        IsMyMP3 = true;
                    if (!IsMyMP3)
                        File.Delete(TheFile.FullName);
                }
                else
                {
                    File.Delete(TheFile.FullName);
                }
            }
            MyFiles = new DirectoryInfo(SessionsLib.CompilePath + "include").GetFiles();
            foreach (FileInfo TheFile in MyFiles)
            {
                // File.Delete(TheFile.FullName)
                if (!(TheFile.Name == "ActionWorks.h"))
                    File.Delete(TheFile.FullName);
            }
            if (GetXDSLine("SAVE ").EndsWith("1"))
            {
                var ff = new byte[32769];
                for (short i = 0; i <= 32767; i++)
                    ff[i] = 0;
                File.WriteAllBytes(SessionsLib.CompilePath + "nitrofiles/SaveData.dat", ff);
            }

            if (GetXDSLine("PROJECTLOGO ").Length < 12)
            {
                if (File.Exists(SessionsLib.CompilePath + "logo.bmp"))
                {
                    File.Delete(SessionsLib.CompilePath + "logo.bmp");
                }
                File.Copy(Constants.AppDirectory + "logo.bmp", SessionsLib.CompilePath + "logo.bmp");
            }
            else
            {
                if (File.Exists(SessionsLib.CompilePath + "logo.bmp"))
                {
                    File.Delete(SessionsLib.CompilePath + "logo.bmp");
                }
                File.Copy(GetXDSLine("PROJECTLOGO ").Substring(12), SessionsLib.CompilePath + "logo.bmp");
            }

            string BootRoom = GetXDSLine("BOOTROOM ").Substring(9);
            string FinalString = string.Empty;
            FinalString += "#include <PA9.h>" + Constants.vbCrLf;
            FinalString += "#include <dirent.h>" + Constants.vbCrLf;
            FinalString += "#include <filesystem.h>" + Constants.vbCrLf;
            FinalString += "#include <unistd.h>" + Constants.vbCrLf;

            // FinalString += "#include ""NitroGraphics.h""" + vbCrLf

            File.WriteAllBytes(SessionsLib.CompilePath + "include/NitroGraphics.h", Properties.Resources.NitroGraphics);
            if (GetXDSLine("INCLUDE_WIFI_LIB ").Substring(17) == "1")
            {
                FinalString += "#include \"ky_geturl.h\"" + Constants.vbCrLf;
                if (!File.Exists(SessionsLib.CompilePath + "source/ky_geturl.h"))
                {
                    File.WriteAllBytes(SessionsLib.CompilePath + "source/ky_geturl.h", Properties.Resources.WifiLibH);
                }
                if (!File.Exists(SessionsLib.CompilePath + "source/ky_geturl.c"))
                {
                    File.WriteAllBytes(SessionsLib.CompilePath + "source/ky_geturl.c", Properties.Resources.WifiLibC);
                }
            }
            else
            {
                if (File.Exists(SessionsLib.CompilePath + "source/ky_geturl.h"))
                {
                    File.Delete(SessionsLib.CompilePath + "source/ky_geturl.h");
                }
                if (File.Exists(SessionsLib.CompilePath + "source/ky_geturl.c"))
                {
                    File.Delete(SessionsLib.CompilePath + "source/ky_geturl.c");
                }
            }
            FinalString += "#include \"dsgm_gfx.h\"" + Constants.vbCrLf;
            FinalString += "#include \"GameWorks.h\"" + Constants.vbCrLf;
            Program.Forms.main_Form.compileForm.CustomPerformStep("Converting Sounds");
            string RAWString = string.Empty;
            string MP3String = string.Empty;
            bool DoRAW = false;
            bool DoMP3 = false;
            foreach (string X in SoundsToRedo)
            {
                if (iGet(GetXDSLine("SOUND " + X + ","), 1, ",") == "1")
                {
                    MP3String += "mp3enc -b 64 \"" + X + "_enc.mp3\" \"" + X + "\".mp3" + Constants.vbCrLf;
                    File.Copy(SessionsLib.SessionPath + "Sounds/" + X + ".mp3", SessionsLib.CompilePath + "nitrofiles/" + X + "_enc.mp3");
                    DoMP3 = true;
                }
                else
                {
                    RAWString += "sox \"" + X + "\".wav -r 11025 -c 1 -e signed -b 8 \"" + X + "\".raw" + Constants.vbCrLf;
                    File.Copy(SessionsLib.SessionPath + "Sounds/" + X + ".wav", SessionsLib.CompilePath + "data/" + X + ".wav");
                    DoRAW = true;
                }
            }
            if (DoRAW)
            {
                File.Copy(Constants.AppDirectory + "sox.exe", SessionsLib.CompilePath + "data/sox.exe");
                File.Copy(Constants.AppDirectory + "libgomp-1.dll", SessionsLib.CompilePath + "data/libgomp-1.dll");
                File.Copy(Constants.AppDirectory + "pthreadgc2.dll", SessionsLib.CompilePath + "data/pthreadgc2.dll");
                File.Copy(Constants.AppDirectory + "zlib1.dll", SessionsLib.CompilePath + "data/zlib1.dll");
                DSGMlib.RunBatchString(RAWString, SessionsLib.CompilePath + "data", false);
                File.Delete(SessionsLib.CompilePath + "data/sox.exe");
                File.Delete(SessionsLib.CompilePath + "data/libgomp-1.dll");
                File.Delete(SessionsLib.CompilePath + "data/pthreadgc2.dll");
                File.Delete(SessionsLib.CompilePath + "data/zlib1.dll");
                foreach (string X in SoundsToRedo)
                {
                    if (iGet(GetXDSLine("SOUND " + X + ","), 1, ",") == "1")
                        continue;
                    File.Delete(SessionsLib.CompilePath + "data/" + X + ".wav");
                }
            }
            if (DoMP3)
            {
                File.Copy(Constants.AppDirectory + "mp3enc.exe", SessionsLib.CompilePath + "nitrofiles/mp3enc.exe");
                DSGMlib.RunBatchString(MP3String, SessionsLib.CompilePath + "nitrofiles", false);
                foreach (string X in SoundsToRedo)
                {
                    if (iGet(GetXDSLine("SOUND " + X + ","), 1, ",") == "0")
                        continue;
                    File.Delete(SessionsLib.CompilePath + "nitrofiles/" + X + "_enc.mp3");
                }
                File.Delete(SessionsLib.CompilePath + "nitrofiles/mp3enc.exe");
            }
            FinalString += Constants.vbCrLf;
            foreach (string X in GetXDSFilter("NITROFS "))
            {
                string FileName = X.Substring(8);
                File.Copy(SessionsLib.SessionPath + "NitroFSFiles/" + FileName, SessionsLib.CompilePath + "nitrofiles/" + FileName, true);
            }
            // FinalString += "s16 score = " + GetXDSLine("SCORE ").ToString.Substring(6) + ";" + vbcrlf
            // FinalString += "s16 health = " + GetXDSLine("HEALTH ").ToString.Substring(7) + ";" + vbcrlf
            // FinalString += "s16 lives = " + GetXDSLine("LIVES ").ToString.Substring(6) + ";" + vbcrlf
            // If GetXDSFilter("GLOBAL ").Length > 0 Then
            // FinalString += vbcrlf
            // End If
            // If GetXDSFilter("GLOBAL ").Length > 0 Then
            // FinalString += vbcrlf
            // End If
            // FinalString += "bool DSGM_CreateObject(u8 ObjectID, u8 InstanceID, bool Screen, s16 X, s16 Y);" + vbcrlf
            // FinalString += "bool DSGM_SetObjectSprite(u8 InstanceID, u8 SpriteID, bool DeleteOld);" + vbcrlf
            // FinalString += "bool DSGM_SwitchRoomByIndex(u8 RoomIndex);" + vbcrlf
            // FinalString += vbcrlf + "int main (int argc, char ** argv) {" + vbcrlf
            FinalString += Constants.vbCrLf + "int main (void) {" + Constants.vbCrLf;
            FinalString += "  RoomCount = " + GetXDSFilter("ROOM ").Length.ToString() + ";" + Constants.vbCrLf;
            FinalString += "  score = " + GetXDSLine("SCORE ").Substring(6) + ";" + Constants.vbCrLf;
            FinalString += "  lives = " + GetXDSLine("LIVES ").Substring(6) + ";" + Constants.vbCrLf;
            FinalString += "  health = " + GetXDSLine("HEALTH ").Substring(7) + ";" + Constants.vbCrLf;
            foreach (string XDSLine_ in GetXDSFilter("GLOBAL "))
            {
                string XDSLine = XDSLine_;
                XDSLine = XDSLine.Substring(7);
                string VariableType = iGet(XDSLine, 1, ",");
                if (!(VariableType == "String"))
                    continue;
                string VariableName = iGet(XDSLine, 0, ",");
                string VariableValue = iGet(XDSLine, 2, ",");
                FinalString += "  strcpy(" + VariableName + ", \"" + VariableValue + "\");" + Constants.vbCrLf;
            }
            FinalString += "  CurrentRoom = Room_Get_Index(\"" + GetXDSLine("BOOTROOM ").Substring(9) + "\");" + Constants.vbCrLf;
            FinalString += "  swiWaitForVBlank();" + Constants.vbCrLf;
            FinalString += "  PA_InitFifo();" + Constants.vbCrLf;
            FinalString += "  PA_Init();" + Constants.vbCrLf;
            // FinalString += "  PA_Init2D();" + vbcrlf
            FinalString += "  DSGM_Init_PAlib();" + Constants.vbCrLf;
            FinalString += "  Reset_Alarms();" + Constants.vbCrLf;
            if (GetXDSLine("NITROFS_CALL ").Substring(13) == "1")
            {
                FinalString += "  nitroFSInit(NULL);" + Constants.vbCrLf;
                FinalString += "  chdir(\"nitro:/\");" + Constants.vbCrLf;
            }
            if (GetXDSLine("FAT_CALL ").Substring(9) == "1")
            {
                FinalString += "  fatInitDefault();" + Constants.vbCrLf;
            }
            if (XDSCountLines("SOUND ") > 0)
            {
                FinalString += "  DSGM_Init_Sound();" + Constants.vbCrLf;
            }
            FinalString += "  " + BootRoom + "();" + Constants.vbCrLf;
            FinalString += "  return 0;" + Constants.vbCrLf;
            FinalString += "}" + Constants.vbCrLf;
            string PAini = "#TranspColor Magenta";
            PAini += Constants.vbCrLf + "#Backgrounds : " + Constants.vbCrLf;
            short DOn = 0;
            if (RedoAllGraphics)
            {
                foreach (string XDSLine_ in GetXDSFilter("BACKGROUND "))
                {
                    string XDSLine = XDSLine_;
                    XDSLine = XDSLine.Substring(11);
                    string BackgroundName = iGet(XDSLine, 0, ",");
                    var BGImage = DSGMlib.PathToImage(SessionsLib.SessionPath + "Backgrounds/" + BackgroundName + ".png");
                    PAini += BackgroundName + ".png ";
                    if (BGImage.Width > 256 & BGImage.Height > 256)
                    {
                        PAini += "LargeMap";
                    }
                    else
                    {
                        PAini += "EasyBG";
                    }
                    File.Copy(SessionsLib.SessionPath + "Backgrounds/" + BackgroundName + ".png", SessionsLib.CompilePath + "gfx/" + BackgroundName + ".png");
                    PAini += Constants.vbCrLf;
                }
            }
            else
            {
                foreach (string X in BGsToRedo)
                {
                    // Dim IsBG As Boolean = DoesXDSLineExist("BACKGROUND " + X)
                    // If Not IsBG Then Continue For
                    var BGImage = DSGMlib.PathToImage(SessionsLib.SessionPath + "Backgrounds/" + X + ".png");
                    PAini += X + ".png ";
                    if (BGImage.Width > 256 & BGImage.Height > 256)
                    {
                        PAini += "LargeMap";
                    }
                    else
                    {
                        PAini += "EasyBG";
                    }
                    PAini += Constants.vbCrLf;
                    File.Copy(SessionsLib.SessionPath + "Backgrounds/" + X + ".png", SessionsLib.CompilePath + "gfx/" + X + ".png", true);
                    BGImage.Dispose();
                    DOn = (short)(DOn + 1);
                }
            }
            // FinalString += "u8 ObjectGetPallet(char *ObjectName) {" + vbcrlf
            // For Each X As String In GetXDSFilter("OBJECT ")
            // X = X.Substring(7)
            // Dim ObjectName As String = iget(X, 0, ",")
            // Dim SpriteName As String = iget(GetXDSLine("OBJECT " + ObjectName + ","), 1, ",")
            // Dim PalletNumber As Byte = 0
            // For Each PalletString As String In PalletNumbers
            // If PalletString.StartsWith(SpriteName + ",") Then
            // PalletNumber = Convert.ToByte(PalletString.Substring(PalletString.IndexOf(",") + 1))
            // End If
            // Next
            // FinalString += " if (strcmp(ObjectName, """ + ObjectName + """) == 0) return " + PalletNumber.ToString + ";" + vbcrlf
            // Next
            // FinalString += " return 0;" + vbcrlf
            // FinalString += "}" + vbcrlf
            // Dim EventsHeaderString As String = String.Empty
            // For Each XDSLine As String In GetXDSFilter("EVENT ")
            // DOn = 0
            // XDSLine = XDSLine.Substring(6)
            // Dim ObjectName As String = iget(XDSLine, 0, ",")
            // Dim MainClass As String = iget(XDSLine, 1, ",")
            // Dim StringMainClass As String = MainClassTypeToString(MainClass).Replace(" ", String.Empty)
            // Dim SubClass As String = iget(XDSLine, 2, ",")
            // Dim StringSubClass As String = SubClass.Replace(" ", String.Empty)
            // If StringSubClass = "NoData" Then StringSubClass = String.Empty
            // EventsHeaderString += "void " + ObjectName + StringMainClass + StringSubClass + "_Event(u8 DAppliesTo);" + vbcrlf
            // Next
            PAini += Constants.vbCrLf + "#Sprites : " + Constants.vbCrLf;
            var PalletNumbers = new Dictionary<string, string>();
            var PalletNames = new Dictionary<string, string>();
            var PalletNumbers_Nitro = new Dictionary<string, string>();
            var PalletNames_Nitro = new Dictionary<string, string>();
            short AllColors = 0;
            byte PalletOn = 0;
            short AllColors_Nitro = 0;
            byte PalletOn_Nitro = 0;
            bool FirstRun = true;
            foreach (string XDSLine_ in GetXDSFilter("SPRITE "))
            {
                string XDSLine = XDSLine_;
                XDSLine = XDSLine.Substring(7);
                string SpriteName = iGet(XDSLine, 0, ",");
                var TheImage = GenerateDSSprite(SpriteName);
                string SpriteNameExtension = SpriteName + ".png";
                short CurrentColors = ImageCountColors(TheImage);
                if (iGet(GetXDSLine("SPRITE " + XDSLine), 3, ",") == "Nitro")
                {
                    if (AllColors_Nitro + CurrentColors >= 256)
                    {
                        AllColors_Nitro = 0;
                        PalletOn_Nitro = (byte)(PalletOn_Nitro + 1);
                    }
                    else
                    {
                        AllColors_Nitro += CurrentColors;
                    }
                    PalletNumbers_Nitro.Add(SpriteName , PalletOn_Nitro.ToString());
                }
                else
                {
                    if (AllColors + CurrentColors >= 256)
                    {
                        AllColors = 0;
                        PalletOn = (byte)(PalletOn + 1);
                    }
                    else
                    {
                        AllColors += CurrentColors;
                    }
                    PalletNumbers.Add(SpriteName , PalletOn.ToString());
                }
                if (RedoSprites)
                {
                    TheImage.Save(SessionsLib.CompilePath + "gfx/" + SpriteNameExtension);
                    if (iGet(GetXDSLine("SPRITE " + XDSLine), 3, ",") == "Nitro")
                    {
                        PAini += SpriteNameExtension + " 256Colors " + "NitroPal" + PalletOn_Nitro.ToString() + Constants.vbCrLf;
                    }
                    else
                    {
                        PAini += SpriteNameExtension + " 256Colors " + "DSGMPal" + PalletOn.ToString() + Constants.vbCrLf;
                    }
                }
                if (iGet(GetXDSLine("SPRITE " + XDSLine), 3, ",") == "Nitro")
                {
                    if (FirstRun)
                    {
                        PalletNames_Nitro.Add(SpriteName, PalletOn_Nitro.ToString());
                        FirstRun = false;
                        continue;
                    }
                    if (AllColors == 0)
                        PalletNames_Nitro.Add(SpriteName, PalletOn_Nitro.ToString());
                }
                else
                {
                    if (FirstRun)
                    {
                        PalletNames.Add(SpriteName, PalletOn.ToString());
                        FirstRun = false;
                        continue;
                    }
                    if (AllColors == 0)
                        PalletNames.Add(SpriteName, PalletOn.ToString());
                }
            }
            File.WriteAllText(SessionsLib.CompilePath + "gfx/PAGfx.ini", PAini);
            string EventsString = string.Empty;
            foreach (string XDSLine_ in GetXDSFilter("EVENT "))
            {
                string XDSLine = XDSLine_;
                DOn = 0;
                XDSLine = XDSLine.Substring(6);
                string ObjectName = iGet(XDSLine, 0, ",");
                string MainClass = iGet(XDSLine, 1, ",");
                string StringMainClass = ScriptsLib.MainClassTypeToString(Convert.ToByte(MainClass)).Replace(" ", string.Empty);
                string SubClass = iGet(XDSLine, 2, ",");
                string StringSubClass = SubClass.Replace(" ", string.Empty);
                if (StringSubClass == "NoData")
                    StringSubClass = string.Empty;
                if (StringSubClass == "<Unknown>")
                    continue;
                EventsString += "void " + ObjectName + StringMainClass + StringSubClass + "_Event(u8 DAppliesTo) {" + Constants.vbCrLf;
                byte CurrentIndent = 1;
                byte IndentOrDedent = 0;
                byte Added = 0;
                foreach (string Y_ in GetXDSFilter("ACT " + ObjectName + "," + MainClass + "," + SubClass + ","))
                {
                    string Y = Y_;
                    Y = (string)SillyFixMe(Y);
                    Y = Y.Substring(("ACT " + ObjectName + "," + MainClass + "," + SubClass + ",").Length);
                    string ActionName = iGet(Y, 0, ",");
                    if (!File.Exists(Constants.AppDirectory + "Actions/" + ActionName + ".action"))
                        continue;
                    IndentOrDedent = 0;
                    Added = 0;
                    bool NeedsAppliesToVar = false;
                    if (!(ActionName == "Execute Code"))
                    {
                        foreach (string X in File.ReadAllLines(Constants.AppDirectory + "Actions/" + ActionName.Split('\\')[1] + ".action"))
                        {
                            if (X == "INDENT")
                                IndentOrDedent = 1;
                            if (X == "DEDENT")
                                IndentOrDedent = 2;
                            if (X.Contains("AppliesTo"))
                                NeedsAppliesToVar = true;
                        }
                    }
                    string Arguments = iGet(Y, 1, ",");
                    string AppliesToString = iGet(Y, 2, ",");
                    if (AppliesToString == "<Unknown>" | Arguments.Contains("<Unknown>"))
                        continue;
                    byte ArgumentCount = (byte)(HowManyChar(Arguments, ";") + 1);
                    var InputtedArgumentValues = new List<string>();
                    for (byte X = 0, loopTo = (byte)(ArgumentCount - 1); X <= loopTo; X++)
                        InputtedArgumentValues.Add(iGet(Arguments, X, ";").ToString().Replace("<com>", ","));
                    // For Each X As String In File.ReadAllLines(AppPath + "Actions/" + ActionName + ".action")
                    // If X = "NOAPPLIES" Then ActionSaysIgnoreApplies = True : Exit For
                    // Next
                    if (!(ActionName == "Execute Code"))
                    {
                        foreach (string X in ScriptsLib.ApplyFinders)
                        {
                            if (Arguments.Contains(X))
                                NeedsAppliesToVar = true;
                        }
                    }
                    // If Not IgnoreApplies Then NeedsAppliesToVar = True
                    string TempLine = string.Empty;
                    // If ApplicationUsed Then
                    string ToReplace = "AppliesTo";
                    if (ActionName == "Execute Code")
                        ToReplace = "DAppliesTo";
                    for (byte X = 0, loopTo1 = (byte)(InputtedArgumentValues.Count - 1); X <= loopTo1; X++)
                    {
                        foreach (string D in ScriptsLib.ApplyFinders)
                        {
                            string NoBrackets = D;
                            NoBrackets = NoBrackets.Substring(1);
                            NoBrackets = NoBrackets.Substring(0, NoBrackets.Length - 1);
                            InputtedArgumentValues[X] = InputtedArgumentValues[X].Replace(D, "Instances[" + ToReplace + "]." + NoBrackets);
                        }
                        // InputtedArgumentValues.Item(X) = InputtedArgumentValues(X).Replace("[Me]", "DAppliesTo")
                    }
                    // End If
                    if (NeedsAppliesToVar)
                    {
                        if (AppliesToString == "this")
                        {
                            EventsString = EventsString + MakeSpaces((byte)(CurrentIndent * 2)) + "u16 AppliesTo" + DOn.ToString() + " = DAppliesTo;" + Constants.vbCrLf;
                            //EventsString = Conversions.ToString(EventsString + Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(MakeSpaces((byte)(CurrentIndent * 2)), "u16 AppliesTo"), DOn.ToString()), " = DAppliesTo;"), Constants.vbCrLf));
                            Added = 0;
                        }
                        else if (IsObject(AppliesToString))
                        {
                            EventsString = EventsString + MakeSpaces((byte)(CurrentIndent * 2)) + "u16 AppliesTo" + DOn.ToString() + " = 0;" + Constants.vbCrLf;
                            EventsString = EventsString + MakeSpaces((byte)(CurrentIndent * 2)) + "for (AppliesTo" + DOn.ToString() + " = 0; AppliesTo" + DOn.ToString() + " < 256; AppliesTo" + DOn.ToString() + "++) {" + Constants.vbCrLf;
                            EventsString = EventsString + MakeSpaces((byte)(CurrentIndent * 2)) + "  if (Instances[AppliesTo" + DOn.ToString() + "].InUse && Instances[AppliesTo" + DOn.ToString() + "].EName == " + AppliesToString + ") {" + Constants.vbCrLf;

                            //EventsString = Conversions.ToString(EventsString + Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(MakeSpaces((byte)(CurrentIndent * 2)), "u16 AppliesTo"), DOn.ToString()), " = 0;"), Constants.vbCrLf));
                            //EventsString = Conversions.ToString(EventsString + Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(MakeSpaces((byte)(CurrentIndent * 2)), "for (AppliesTo"), DOn.ToString()), " = 0; AppliesTo"), DOn.ToString()), " < 256; AppliesTo"), DOn.ToString()), "++) {"), Constants.vbCrLf));
                            //EventsString = Conversions.ToString(EventsString + Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(MakeSpaces((byte)(CurrentIndent * 2)), "  if (Instances[AppliesTo"), DOn.ToString()), "].InUse && Instances[AppliesTo"), DOn.ToString()), "].EName == "), AppliesToString), ") {"), Constants.vbCrLf));
                            CurrentIndent = (byte)(CurrentIndent + 2);
                            Added = 2;
                        }
                        else
                        {
                            TempLine = "u8 ArrayAppliesTo" + DOn.ToString() + "[] = {";
                            byte LoopTo = 0;
                            for (byte I = 0, loopTo2 = (byte)HowManyChar(AppliesToString, " "); I <= loopTo2; I++)
                            {
                                TempLine += iGet(AppliesToString, I, " ") + ", ";
                                LoopTo = (byte)(LoopTo + 1);
                            }
                            TempLine = TempLine.Substring(0, TempLine.Length - 2);
                            TempLine += "};";

                            EventsString = EventsString + MakeSpaces((byte)(CurrentIndent * 2)) + TempLine + Constants.vbCrLf;
                            EventsString = EventsString + MakeSpaces((byte)(CurrentIndent * 2)) + "u16 AppliesTo" + DOn.ToString() + " = 0;" + Constants.vbCrLf;

                            TempLine = MakeSpaces((byte)(CurrentIndent * 2)) + "for (AppliesTo" + DOn.ToString() + " = 0; AppliesTo" + DOn.ToString() + " < " + LoopTo.ToString() + "; AppliesTo" + DOn.ToString() + "++) {";

                            //EventsString = Conversions.ToString(EventsString + Operators.AddObject(Operators.AddObject(MakeSpaces((byte)(CurrentIndent * 2)), TempLine), Constants.vbCrLf));
                            //EventsString = Conversions.ToString(EventsString + Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(MakeSpaces((byte)(CurrentIndent * 2)), "u16 AppliesTo"), DOn.ToString()), " = 0;"), Constants.vbCrLf));
                            //TempLine = Conversions.ToString(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(MakeSpaces((byte)(CurrentIndent * 2)), "for (AppliesTo"), DOn.ToString()), " = 0; AppliesTo"), DOn.ToString()), " < "), LoopTo.ToString()), "; AppliesTo"), DOn.ToString()), "++) {"));
                            EventsString += TempLine + Constants.vbCrLf;
                            CurrentIndent = (byte)(CurrentIndent + 1);
                            Added = 1;
                        }
                    }
                    // For Each X As String In File.ReadAllLines(AppPath + "Actions/" + ActionName + ".action")
                    // If X = "INDENT" Then
                    // CurrentIndent += 1
                    // End If
                    // If X = "DEDENT" Then
                    // If CurrentIndent > 0 Then CurrentIndent -= 1
                    // End If
                    // Next
                    if (IndentOrDedent == 1)
                        CurrentIndent = (byte)(CurrentIndent + 1);
                    if (IndentOrDedent == 2)
                    {
                        if (CurrentIndent > 0)
                            CurrentIndent = (byte)(CurrentIndent - 1);
                    }
                    if (ActionName == "Execute Code")
                    {
                        string DBASCode = Arguments;
                        // Dim BRCount As Int16 = HowManyChar(OriginalCode, "<br|>")
                        // For i As Int16 = 0 To BRCount

                        // Next
                        DBASCode = DBASCode.Replace("<br|>", Constants.vbCrLf).Replace("<com>", ",").Replace("<sem>", ";");
                        string CCode = ScriptsLib.ScriptParseFromContent("Temp", DBASCode, string.Empty, string.Empty, false, true, false);
                        foreach (string X in StringToLines(CCode))
                        {
                            EventsString = EventsString + MakeSpaces((byte)(CurrentIndent * 2)) + X + Constants.vbCrLf;
                            //EventsString = Conversions.ToString(EventsString + Operators.AddObject(Operators.AddObject(MakeSpaces((byte)(CurrentIndent * 2)), X), Constants.vbCrLf));
                        }
                    }
                    else
                    {
                        foreach (string X_ in File.ReadAllLines(Constants.AppDirectory + "Actions/" + ActionName.Split('\\')[1] + ".action"))
                        {
                            string X = X_;

                            if (X.Length == 0)
                                continue;
                            if (X.StartsWith("ARG "))
                                continue;
                            if (X.StartsWith("DISPLAY "))
                                continue;
                            if (X.StartsWith("TYPE "))
                                continue;
                            if (X.StartsWith("ICON "))
                                continue;
                            if (X.StartsWith("CONDITION "))
                                continue;
                            if (X == "INDENT")
                                continue;
                            if (X == "DEDENT")
                                continue;
                            if (X == "NOAPPLIES")
                                continue;
                            for (byte i = 0; i <= 200; i++)
                            {
                                if (X[0].ToString() == " ")
                                    X = X.Substring(1);
                                else
                                    break;
                            }
                            for (int FOn = 0, loopTo3 = ArgumentCount - 1; FOn <= loopTo3; FOn++)
                                X = X.Replace("!" + (FOn + 1).ToString() + "!", InputtedArgumentValues[FOn]);
                            if (NeedsAppliesToVar)
                            {
                                if (IsObject(AppliesToString) | AppliesToString == "this")
                                {
                                    X = X.Replace("AppliesTo", "DX" + DOn.ToString());
                                }
                                else
                                {
                                    X = X.Replace("AppliesTo", "ArrayDX" + DOn.ToString() + "[DX" + DOn.ToString() + "]");
                                }
                                X = X.Replace("DX", "AppliesTo");
                            }
                            X = X.Replace("[Me]", "DAppliesTo");
                            X = MakeSpaces((byte)(CurrentIndent * 2)) + X;
                            //X = Conversions.ToString(Operators.AddObject(MakeSpaces((byte)(CurrentIndent * 2)), X));
                            EventsString += X + Constants.vbCrLf;
                        }
                    }
                    if (IndentOrDedent == 0)
                    {
                        CurrentIndent -= Added;
                        if (Added == 1)
                        {
                            EventsString = EventsString + MakeSpaces((byte)(CurrentIndent * 2)) + "}" + Constants.vbCrLf;
                            //EventsString = Conversions.ToString(EventsString + Operators.AddObject(Operators.AddObject(MakeSpaces((byte)(CurrentIndent * 2)), "}"), Constants.vbCrLf));
                        }
                        else if (Added == 2)
                        {
                            EventsString = EventsString + MakeSpaces((byte)(CurrentIndent * 2)) + "  }" + Constants.vbCrLf;
                            EventsString = EventsString + MakeSpaces((byte)(CurrentIndent * 2)) + "}" + Constants.vbCrLf;
                            //EventsString = Conversions.ToString(EventsString + Operators.AddObject(Operators.AddObject(MakeSpaces((byte)(CurrentIndent * 2)), "  }"), Constants.vbCrLf));
                            //EventsString = Conversions.ToString(EventsString + Operators.AddObject(Operators.AddObject(MakeSpaces((byte)(CurrentIndent * 2)), "}"), Constants.vbCrLf));
                        }
                    }
                    DOn = (short)(DOn + 1);
                }
                for (int i = CurrentIndent - 1; i >= 0; i -= 1)
                {
                    EventsString = EventsString + MakeSpaces((byte)(i * 2)) + "}" + Constants.vbCrLf;
                    //EventsString = Conversions.ToString(EventsString + Operators.AddObject(Operators.AddObject(MakeSpaces((byte)(i * 2)), "}"), Constants.vbCrLf));
                }
            }
            // File.WriteAllText(CompilePath + "source\Events.c", EventsString)
            DOn = 0;
            // Twas here, Marvolo!
            foreach (string X_ in GetXDSFilter("ROOM "))
            {
                string X = X_;
                X = X.Substring(5);
                string RoomName = iGet(X, 0, ",");
                short TopWidth = Convert.ToInt16(iGet(X, 1, ","));
                short TopHeight = Convert.ToInt16(iGet(X, 2, ","));
                bool TopScroll = iGet(X, 3, ",") == "1" ? true : false;
                string TopBG = iGet(X, 4, ",");
                short BottomWidth = Convert.ToInt16(iGet(X, 5, ","));
                short BottomHeight = Convert.ToInt16(iGet(X, 6, ","));
                bool BottomScroll = iGet(X, 7, ",") == "1" ? true : false;
                string BottomBG = iGet(X, 8, ",");
                FinalString += "bool " + RoomName + "(void) {" + Constants.vbCrLf;
                FinalString += "  PA_ResetSpriteSys();" + Constants.vbCrLf;
                FinalString += "  PA_ResetBgSys();" + Constants.vbCrLf;
                if (iGet(GetXDSLine("BACKGROUND " + TopBG), 1, ",") == "Nitro")
                {
                    if (TopBG.Length > 0)
                        FinalString += "  FAT_LoadBackground(1, 2, \"" + TopBG + "\");" + Constants.vbCrLf;
                    if (BottomBG.Length > 0)
                        FinalString += "  FAT_LoadBackground(0, 2, \"" + BottomBG + "\");" + Constants.vbCrLf;
                }
                else
                {
                    if (TopBG.Length > 0)
                        FinalString += "  PA_LoadBackground(1, 2, &" + TopBG + ");" + Constants.vbCrLf;
                    if (BottomBG.Length > 0)
                        FinalString += "  PA_LoadBackground(0, 2, &" + BottomBG + ");" + Constants.vbCrLf;
                }
                var MyPalletLines = new Dictionary<string, string>();
                string MyNewLine = string.Empty;
                foreach (var p in PalletNames)
                {
                    byte PalletNo = Convert.ToByte(p.Key.Substring(p.Value.IndexOf(",") + 1));
                    if (File.ReadAllText(SessionsLib.CompilePath + "gfx/PAGfx.ini").Contains("DSGMPal" + PalletNo.ToString()))
                    {
                        MyNewLine = "  PA_LoadSpritePal(1, " + PalletNo.ToString() + ", (void*)DSGMPal" + PalletNo.ToString() + "_Pal);";
                        MyNewLine += " PA_LoadSpritePal(0, " + PalletNo.ToString() + ", (void*)DSGMPal" + PalletNo.ToString() + "_Pal);";
                    }
                    bool AlreadyDone = false;
                    foreach (var MyLine in MyPalletLines)
                    {
                        if (MyLine.Value == MyNewLine)
                        {
                            AlreadyDone = true;
                            break;
                        }
                    }
                    if (!AlreadyDone)
                    {
                        MyPalletLines.Add("", MyNewLine);
                    }
                }

                foreach (var MyLine in MyPalletLines)
                { 
                    FinalString += MyLine.Value + Constants.vbCrLf; 
                }

                FinalString += "  DSGM_Setup_Room(" + TopWidth.ToString() + ", ";
                FinalString += TopHeight.ToString() + ", ";
                FinalString += BottomWidth.ToString() + ", ";
                FinalString += BottomHeight.ToString() + ", 0, 0, 0, 0);" + Constants.vbCrLf;
                FinalString += "  PA_LoadText(1, 0, &Default); PA_LoadText(0, 0, &Default);" + Constants.vbCrLf;
                DOn = 0;

                foreach (string Y_ in GetXDSFilter("OBJECTPLOT "))
                {
                    string Y = Y_;
                    Y = Y.Substring(11);

                    if (iGet(Y, 1, ",") != RoomName)
                    {
                        continue;
                    }

                    string ObjectName = iGet(Y, 0, ",");
                    // FinalString += "  // " + ObjectName + vbcrlf
                    string ObjectLine = GetXDSLine("OBJECT " + ObjectName + ",");
                    bool Screen = iGet(Y, 2, ",") == "1" ? true : false;
                    short DX = Convert.ToInt16(iGet(Y, 3, ","));
                    short DY = Convert.ToInt16(iGet(Y, 4, ","));
                    string SpriteName = iGet(ObjectLine, 1, ",");
                    string SpriteLine = GetXDSLine("SPRITE " + SpriteName + ",");
                    // Dim DefaultFrame As Int16 = Convert.ToInt16(iget(ObjectLine, 2, ","))
                    FinalString += "  Create_Object(" + ObjectName + ", " + DOn.ToString() + ", " + (Screen ? "true" : "false") + ", " + DX.ToString() + ", " + DY.ToString() + ");" + Constants.vbCrLf;
                    // Dim SW As Byte = Convert.ToByte(iget(SpriteLine, 1, ","))
                    // Dim SH As Byte = Convert.ToByte(iget(SpriteLine, 2, ","))
                    // FinalString += "  Instances[" + DOn.ToString + "].ObjectID = ObjectToID(""" + ObjectName + """);" + vbcrlf
                    // FinalString += "  Instances[" + DOn.ToString + "].InUse = true; Instances[" + DOn.ToString + "].Screen = " + If(Screen = 1, "true", "false") + ";" + vbcrlf
                    // FinalString += "  Instances[" + DOn.ToString + "].X = " + DX.ToString + "; Instances[" + DOn.ToString + "].Y = " + DY.ToString + ";" + vbcrlf
                    // FinalString += "  Instances[" + DOn.ToString + "].Width = " + SW.ToString + "; Instances[" + DOn.ToString + "].Height = " + SH.ToString + ";" + vbcrlf
                    // FinalString += "  Instances[" + DOn.ToString + "].Frame = " + DefaultFrame.ToString + ";" + vbcrlf
                    // If DefaultFrame > 0 Then FinalString += "  Instances[" + DOn.ToString + "].FrameChanged = true;" + vbcrlf
                    // Dim PalletNumber As Byte = 0
                    // For Each PalletString As String In PalletNumbers
                    // If PalletString.StartsWith(SpriteName + ",") Then
                    // PalletNumber = Convert.ToByte(PalletString.Substring(PalletString.IndexOf(",") + 1))
                    // End If
                    // Next
                    // FinalString += "  PA_CreateSprite(" + Screen.ToString + ", " + DOn.ToString + ", (void*)" + SpriteName + "_Sprite, OBJ_SIZE_" + SW.ToString + "X" + SH.ToString + ", 1, " + PalletNumber.ToString + ", 256, 192);" + vbcrlf
                    // If Not DefaultFrame = 0 Then FinalString += "  PA_SetSpriteAnim(" + Screen + ", " + DOn.ToString + ", " + DefaultFrame.ToString + ");" + vbcrlf
                    // If DoesXDSLineExist("EVENT " + ObjectName + ",1,NoData") Then FinalString += "  " + ObjectName + "Create_Event(" + DOn.ToString + ");" + vbcrlf
                    DOn = (short)(DOn + 1);
                }
                FinalString += "  DSGM_Complete_Room();" + Constants.vbCrLf;
                FinalString += "  while(true) {" + Constants.vbCrLf;
                DOn = 0;
                bool HasSuchEvents = false;
                foreach (string Y in GetXDSFilter("EVENT "))
                {
                    if (iGet(Y, 1, ",") == "7")
                    {
                        HasSuchEvents = true;
                        break;
                    }
                }
                if (HasSuchEvents)
                {
                    // FinalString += "    // Step Events" + vbcrlf
                    FinalString += "    for (DSGMPL = 0; DSGMPL <= 127; DSGMPL++) {" + Constants.vbCrLf;
                    FinalString += "      if (Instances[DSGMPL].InUse) {" + Constants.vbCrLf;
                    foreach (string Y_ in GetXDSFilter("OBJECT "))
                    {
                        string Y = Y_;
                        Y = Y.Substring(7);
                        string ObjectName = iGet(Y, 0, ",");
                        if (DoesXDSLineExist("EVENT " + ObjectName + ",7,NoData"))
                        {
                            FinalString += "        if (Instances[DSGMPL].EName == " + ObjectName + ") " + ObjectName + "Step_Event(DSGMPL);" + Constants.vbCrLf;
                        }
                        DOn = (short)(DOn + 1);
                    }
                    FinalString += "      }" + Constants.vbCrLf;
                    FinalString += "    }" + Constants.vbCrLf;
                }
                DOn = 0;
                HasSuchEvents = false;
                foreach (string Y in GetXDSFilter("EVENT "))
                {
                    if (iGet(Y, 1, ",") == "5")
                    {
                        HasSuchEvents = true;
                        break;
                    }
                }
                if (HasSuchEvents)
                {
                    // FinalString += "    // Touch (Stylus) Events" + vbcrlf
                    FinalString += "    for (DSGMPL = 0; DSGMPL <= 127; DSGMPL++) {" + Constants.vbCrLf;
                    FinalString += "      if (Instances[DSGMPL].InUse) {" + Constants.vbCrLf;
                    foreach (string Y in GetXDSFilter("OBJECT "))
                    {
                        string ObjectName = iGet(Y.Substring(7), 0, ",");
                        if (DoesXDSLineExist("EVENT " + ObjectName + ",5,New Press"))
                        {
                            FinalString += "        if (Instances[DSGMPL].EName == " + ObjectName + " && Stylus.Newpress && PA_SpriteTouched(DSGMPL)) " + ObjectName + "TouchNewPress_Event(DSGMPL);" + Constants.vbCrLf;
                        }
                        if (DoesXDSLineExist("EVENT " + ObjectName + ",5,Double Press"))
                        {
                            FinalString += "        if (Instances[DSGMPL].EName == " + ObjectName + " && Stylus.DblClick && PA_SpriteTouched(DSGMPL)) " + ObjectName + "TouchDoublePress_Event(DSGMPL);" + Constants.vbCrLf;
                        }
                        if (DoesXDSLineExist("EVENT " + ObjectName + ",5,Held"))
                        {
                            FinalString += "        if (Instances[DSGMPL].EName == " + ObjectName + " && Stylus.Held && PA_SpriteTouched(DSGMPL)) " + ObjectName + "TouchHeld_Event(DSGMPL);" + Constants.vbCrLf;
                        }
                        if (DoesXDSLineExist("EVENT " + ObjectName + ",5,Released"))
                        {
                            FinalString += "        if (Instances[DSGMPL].EName == " + ObjectName + " && Stylus.Released && PA_SpriteTouched(DSGMPL)) " + ObjectName + "TouchReleased_Event(DSGMPL);" + Constants.vbCrLf;
                        }
                        DOn = (short)(DOn + 1);
                    }
                    FinalString += "      }" + Constants.vbCrLf;
                    FinalString += "    }" + Constants.vbCrLf;
                }
                DOn = 0;
                HasSuchEvents = false;
                foreach (string Y in GetXDSFilter("EVENT "))
                {
                    if (iGet(Y, 1, ",") == "6")
                    {
                        HasSuchEvents = true;
                        break;
                    }
                }
                if (HasSuchEvents)
                {
                    // FinalString += "    // Collision Events" + vbcrlf
                    FinalString += "    for (DSGMPL = 0; DSGMPL <= 127; DSGMPL++) {" + Constants.vbCrLf;
                    foreach (string Y_ in GetXDSFilter("OBJECT "))
                    {
                        string Y = Y_;
                        Y = Y.Substring(7);
                        string ObjectName = iGet(Y, 0, ",");
                        if (GetXDSFilter("EVENT " + ObjectName + ",6,").Length > 0)
                        {
                            FinalString += "      if (Instances[DSGMPL].InUse && Instances[DSGMPL].EName == " + ObjectName + ") {" + Constants.vbCrLf;
                            foreach (string Z in GetXDSFilter("EVENT " + ObjectName + ",6,"))
                            {
                                string CollidingObject = Z.Substring(Z.LastIndexOf(",") + 1);
                                FinalString += "        for (DSGMSL = 0; DSGMSL <= 127; DSGMSL++) {" + Constants.vbCrLf;
                                FinalString += "          if (Instances[DSGMSL].InUse && Instances[DSGMSL].EName == " + CollidingObject + ") {" + Constants.vbCrLf;
                                FinalString += "            if (" + (GetXDSLine("MIDPOINT_COLLISIONS ").Substring(20) == "1" ? "Sprite_Collision_Mid(" : "Sprite_Collision(") + "DSGMPL, DSGMSL)) " + ObjectName + "Collision" + CollidingObject + "_Event(DSGMPL);" + Constants.vbCrLf;
                                FinalString += "          }" + Constants.vbCrLf;
                                FinalString += "        }" + Constants.vbCrLf;
                            }
                            FinalString += "      }" + Constants.vbCrLf;
                        }
                        DOn = (short)(DOn + 1);
                    }
                    FinalString += "    }" + Constants.vbCrLf;
                }
                DOn = 0;
                HasSuchEvents = false;
                foreach (string Y in GetXDSFilter("EVENT "))
                {
                    if (iGet(Y, 1, ",") == "2")
                        HasSuchEvents = true;
                    if (iGet(Y, 1, ",") == "3")
                        HasSuchEvents = true;
                    if (iGet(Y, 1, ",") == "4")
                        HasSuchEvents = true;
                }
                if (HasSuchEvents)
                {
                    // FinalString += "    // Button Events" + vbcrlf
                    FinalString += "    for (DSGMPL = 0; DSGMPL <= 127; DSGMPL++) {" + Constants.vbCrLf;
                    FinalString += "      if(Instances[DSGMPL].InUse) {" + Constants.vbCrLf;
                    foreach (string Y_ in GetXDSFilter("OBJECT "))
                    {
                        string Y = Y_;
                        Y = Y.Substring(7);
                        string ObjectName = iGet(Y, 0, ",");
                        if (GetXDSFilter("EVENT " + ObjectName + ",2,").Length > 0)
                        {
                            FinalString += "        if (Instances[DSGMPL].EName == " + ObjectName + ") {" + Constants.vbCrLf;
                            foreach (string Z in GetXDSFilter("EVENT " + ObjectName + ",2,"))
                            {
                                string TC = Z.Substring(Z.LastIndexOf(",") + 1);
                                FinalString += "          if(Pad.Newpress." + TC + ") " + ObjectName + "ButtonPress" + TC + "_Event(DSGMPL);" + Constants.vbCrLf;
                            }
                            FinalString += "        }" + Constants.vbCrLf;
                        }
                        if (GetXDSFilter("EVENT " + ObjectName + ",3,").Length > 0)
                        {
                            FinalString += "        if (Instances[DSGMPL].EName == " + ObjectName + ") {" + Constants.vbCrLf;
                            foreach (string Z in GetXDSFilter("EVENT " + ObjectName + ",3,"))
                            {
                                string TC = Z.Substring(Z.LastIndexOf(",") + 1);
                                FinalString += "          if(Pad.Held." + TC + ") " + ObjectName + "ButtonHeld" + TC + "_Event(DSGMPL);" + Constants.vbCrLf;
                            }
                            FinalString += "        }" + Constants.vbCrLf;
                        }
                        if (GetXDSFilter("EVENT " + ObjectName + ",4,").Length > 0)
                        {
                            FinalString += "        if (Instances[DSGMPL].EName == " + ObjectName + ") {" + Constants.vbCrLf;
                            foreach (string Z in GetXDSFilter("EVENT " + ObjectName + ",4,"))
                            {
                                string TC = Z.Substring(Z.LastIndexOf(",") + 1);
                                FinalString += "          if(Pad.Released." + TC + ") " + ObjectName + "ButtonReleased" + TC + "_Event(DSGMPL);" + Constants.vbCrLf;
                            }
                            FinalString += "        }" + Constants.vbCrLf;
                        }
                        DOn = (short)(DOn + 1);
                    }
                    FinalString += "      }" + Constants.vbCrLf;
                    FinalString += "    }" + Constants.vbCrLf;
                }
                FinalString += "    Frames += 1;" + Constants.vbCrLf;
                FinalString += "    RoomFrames += 1;" + Constants.vbCrLf;
                FinalString += "    if (Frames % 60 == 0) Seconds += 1;" + Constants.vbCrLf;
                FinalString += "    if (Frames % 60 == 0) RoomSeconds += 1;" + Constants.vbCrLf;
                // FinalString += "    for (DSGMPL = 0; DSGMPL <= 127; DSGMPL++) {" + vbcrlf
                // FinalString += "      if (Instances[DSGMPL].InUse && Instances[DSGMPL].FrameChanged) {" + vbcrlf
                // FinalString += "        PA_SetSpriteAnim(Instances[DSGMPL].Screen, DSGMPL, Instances[DSGMPL].Frame);" + vbcrlf
                // FinalString += "        Instances[DSGMPL].FrameChanged = false;" + vbcrlf
                // FinalString += "      }" + vbcrlf
                // FinalString += "    }" + vbcrlf
                // FinalString += "    for (DSGMPL = 0; DSGMPL < 255; DSGMPL++) {" + vbcrlf
                // FinalString += "      if (Instances[DSGMPL].InUse) Instances[DSGMPL].FrameChanged = false;" + vbcrlf
                // FinalString += "    }" + vbcrlf
                // FinalString += "    Frames += 1;" + vbcrlf
                // FinalString += "    RoomFrames += 1;" + vbcrlf
                // FinalString += "    if (Frames % 60 == 0) Seconds += 1;" + vbcrlf
                // FinalString += "    if (Frames % 60 == 0) RoomSeconds += 1;" + vbcrlf
                FinalString += "    DSGM_ObjectsSync();" + Constants.vbCrLf;
                FinalString += "    DSGM_AlarmsSync();" + Constants.vbCrLf;
                FinalString += "    PA_WaitForVBL();" + Constants.vbCrLf;
                if (TopScroll)
                    FinalString += "    PA_EasyBgScrollXY(1, 2, RoomData.TopX, RoomData.TopY);" + Constants.vbCrLf;
                if (BottomScroll)
                    FinalString += "    PA_EasyBgScrollXY(0, 2, RoomData.BottomX, RoomData.BottomY);" + Constants.vbCrLf;
                FinalString += "  }" + Constants.vbCrLf;
                FinalString += "  return true;" + Constants.vbCrLf;
                FinalString += "}" + Constants.vbCrLf;
            }
            DOn = 0;
            // Twas here 2, Marvolo!
            // FinalString += "  return true;" + vbcrlf
            // FinalString += "}" + vbcrlf + vbcrlf
            // File.WriteAllText(CompilePath + "gfx/PAGfx.ini", PAini)
            File.WriteAllText(SessionsLib.CompilePath + "source/main.c", FinalString);
            string DefsString = string.Empty;
            foreach (string XDSLine in GetXDSFilter("ROOM "))
                DefsString += "bool " + iGet(XDSLine.Substring(5), 0, ",") + "();" + Constants.vbCrLf;
            foreach (string XDSLine in GetXDSFilter("SCRIPT "))
            {
                string ScriptName = XDSLine.Substring(7);
                ScriptName = ScriptName.Substring(0, ScriptName.LastIndexOf(","));
                DefsString += "int " + ScriptName + "(";
                string ArgumentString = string.Empty;
                foreach (string YXDSLine in GetXDSFilter("SCRIPTARG " + ScriptName + ","))
                {
                    string ArgumentName = iGet(YXDSLine, 1, ",");
                    string ArgumentType = ScriptsLib.GenerateCType(iGet(YXDSLine, 2, ","));
                    ArgumentString += ArgumentType + " ";
                    if (ArgumentType == "char")
                        ArgumentString += "*";
                    ArgumentString += ArgumentName + ", ";
                }
                if (ArgumentString.Length == 0)
                {
                    DefsString += "void";
                }
                else
                {
                    ArgumentString = ArgumentString.Substring(0, ArgumentString.Length - 2);
                    DefsString += ArgumentString;
                }
                DefsString += ");" + Constants.vbCrLf;
            }
            // fsdds()
            File.WriteAllText(SessionsLib.CompilePath + "include/Defines.h", DefsString);
            // File.WriteAllBytes(CompilePath + "include\ActionWorks.h", My.Resources.ActionWorks)
            string GameString = string.Empty;
            GameString += "#include \"dsgm_gfx.h\"" + Constants.vbCrLf;
            GameString += "#include \"custom_gfx.h\"" + Constants.vbCrLf;
            GameString += "#include \"Defines.h\"" + Constants.vbCrLf;
            GameString += "#include \"ActionWorks.h\"" + Constants.vbCrLf + Constants.vbCrLf;
            foreach (string X in GetXDSFilter("INCLUDE "))
            {
                string FileName = X.Substring(8);
                GameString += "#include \"" + FileName + "\"" + Constants.vbCrLf;
                File.Copy(SessionsLib.SessionPath + "IncludeFiles/" + FileName, SessionsLib.CompilePath + "include/" + FileName);
            }
            foreach (string X_ in GetXDSFilter("SOUND "))
            {
                string X = X_;
                if (iGet(X, 1, ",") == "1")
                    continue;
                X = X.Substring(6);
                string SoundName = iGet(X, 0, ",");
                if (File.Exists(SessionsLib.CompilePath + "data/" + SoundName + ".raw"))
                {
                    GameString += "#include \"" + SoundName + ".h\"" + Constants.vbCrLf;
                }
                else
                {
                    GameString += "// Error converting " + SoundName + " with SOX! Sorry folks, use a proper WAV next time." + Constants.vbCrLf;
                }
            }
            GameString += Constants.vbCrLf;
            foreach (string XDSLine in GetXDSFilter("STRUCT "))
            {
                string StructureName = XDSLine.Substring(7);
                GameString += "typedef struct {" + Constants.vbCrLf;
                foreach (string Y_ in GetXDSFilter("STRUCTMEMBER " + StructureName + ","))
                {
                    string Y = Y_;
                    Y = Y.Substring(("STRUCTMEMBER " + StructureName).Length + 1);
                    string ItemName = Y.Substring(0, Y.IndexOf(","));
                    string ItemType = Y.Substring(ItemName.Length + 1);
                    ItemType = ItemType.Substring(0, ItemType.IndexOf(","));
                    ItemType = ScriptsLib.GenerateCType(ItemType);
                    string ItemValue = Y.Substring(Y.LastIndexOf(",") + 1).Replace("<comma>", ",");
                    GameString += "  " + ItemType + " " + (ItemType == "char" ? "*" : string.Empty) + ItemName + ";" + Constants.vbCrLf;
                }
                GameString += "} " + StructureName + ";" + Constants.vbCrLf;
                // GameString += StructureName + "Struct " + StructureName + ";" + vbcrlf
            }
            GameString += Constants.vbCrLf;
            foreach (string XDSLine_ in GetXDSFilter("GLOBAL "))
            {
                string XDSLine = XDSLine_;
                string TempString = string.Empty;
                XDSLine = XDSLine.Substring(7);
                string VariableName = iGet(XDSLine, 0, ",");
                string VariableType = iGet(XDSLine, 1, ",");
                string CVariableType = ScriptsLib.GenerateCType(VariableType);
                // If RealVariableType = "pie" Then RealVariableType = VariableType
                string VariableValue = iGet(XDSLine, 2, ",");
                TempString = CVariableType + " " + VariableName;
                if (CVariableType.ToLower() == "char")
                {
                    TempString += "[128]";
                }
                else if (!DoesXDSLineExist("STRUCT " + VariableType))
                {
                    if (VariableValue.Length > 0)
                        TempString += " = " + VariableValue;
                }
                GameString += TempString + ";" + Constants.vbCrLf;
            }
            GameString += Constants.vbCrLf;
            foreach (string XDSLine_ in GetXDSFilter("ARRAY "))
            {
                string XDSLine = XDSLine_;
                XDSLine = XDSLine.Substring(6);
                string VariableName = iGet(XDSLine, 0, ",");
                byte VariableType = Convert.ToByte(iGet(XDSLine, 1, ","));
                string RealVariableType = string.Empty;
                if (VariableType == 0)
                    RealVariableType = "s32";
                if (VariableType == 1)
                    RealVariableType = "bool";
                string ValuesString = iGet(XDSLine, 2, ",");
                ValuesString = ValuesString.Replace(";", ", ");
                GameString += RealVariableType + " " + VariableName + "[] = {" + ValuesString + "};" + Constants.vbCrLf;
            }
            GameString += Constants.vbCrLf;
            if (GetXDSFilter("OBJECT ").Length > 0)
            {
                GameString += "enum ObjectEnums { ";
                byte ELooper = 1;
                foreach (string P_ in GetXDSFilter("OBJECT "))
                {
                    string P = P_;
                    P = P.Substring(7);
                    P = P.Substring(0, P.IndexOf(","));
                    GameString += P + " = " + ELooper.ToString() + ", ";
                    ELooper = (byte)(ELooper + 1);
                }
                GameString += " };" + Constants.vbCrLf;
            }
            GameString += "void Set_Sprite(u8 InstanceID, char *SpriteName, bool DeleteOld);" + Constants.vbCrLf;
            GameString += "void Create_Object(u8 ObjectEnum, u8 InstanceID, bool Screen, s16 X, s16 Y);" + Constants.vbCrLf;
            GameString += "u8 Sprite_Get_ID(char *SpriteName);" + Constants.vbCrLf;
            GameString += "u8 Room_Get_Index(char *RoomName);" + Constants.vbCrLf;
            GameString += "void Goto_Room_Backend(u8 RoomIndex);" + Constants.vbCrLf;
            GameString += "u8 Count_Instances(u8 ObjectEnum);" + Constants.vbCrLf;
            GameString += "void Goto_Next_Room(void);" + Constants.vbCrLf + Constants.vbCrLf;
            GameString += EventsString + Constants.vbCrLf;
            foreach (string XDSLine_ in GetXDSFilter("SCRIPT "))
            {
                string XDSLine = XDSLine_;
                XDSLine = XDSLine.Substring(7);
                bool C = XDSLine.EndsWith(",0");
                XDSLine = XDSLine.Substring(0, XDSLine.LastIndexOf(","));
                GameString += Constants.vbCrLf + ScriptsLib.ScriptParse(XDSLine, C) + Constants.vbCrLf;
            }
            GameString += Constants.vbCrLf;
            // GameString += "s16 score = " + GetXDSLine("SCORE ").ToString.Substring(6) + ";" + vbcrlf
            // GameString += "s16 lives = " + GetXDSLine("LIVES ").ToString.Substring(6) + ";" + vbcrlf
            // GameString += "s16 health = " + GetXDSLine("HEALTH ").ToString.Substring(7) + ";" + vbcrlf
            // GameString += "u8 RoomCount = " + GetXDSFilter("ROOM ").Length.ToString + ";" + vbcrlf
            // GameString += "u8 CurrentRoom = 0;" + vbcrlf + vbcrlf
            DOn = 0;
            GameString += "void Set_Sprite(u8 InstanceID, char *SpriteName, bool DeleteOld) {" + Constants.vbCrLf;
            GameString += "  Instances[InstanceID].HasSprite = true;" + Constants.vbCrLf;
            GameString += "  if (DeleteOld) PA_DeleteSprite(Instances[InstanceID].Screen, InstanceID);" + Constants.vbCrLf;
            if (GetXDSFilter("SPRITE ").Length > 0)
            {
                GameString += "  switch(Sprite_Get_ID(SpriteName)) {" + Constants.vbCrLf;
                foreach (string X_ in GetXDSFilter("SPRITE "))
                {
                    string X = X_;
                    X = X.Substring(7);
                    string SpriteName = iGet(X, 0, ",");
                    string SW = iGet(X, 1, ",");
                    string SH = iGet(X, 2, ",");
                    byte PalletNumber = 0;
                    byte PalletNumber_Nitro = 0;
                    if (iGet(X, 3, ",") == "Nitro")
                    {
                        foreach (var PalletString_Nitro in PalletNumbers_Nitro)
                        {
                            if (PalletString_Nitro.Key.StartsWith(SpriteName + ","))
                            {
                                PalletNumber_Nitro = Convert.ToByte(PalletString_Nitro.Key.Substring(PalletString_Nitro.Value.IndexOf(",") + 1));
                            }
                        }
                        PalletNumber = (byte)PalletNumbers.Count;
                    }
                    else
                    {
                        foreach (var PalletString in PalletNumbers)
                        {
                            if (PalletString.Key.StartsWith(SpriteName + ","))
                            {
                                PalletNumber = Convert.ToByte(PalletString.Key.Substring(PalletString.Value.IndexOf(",") + 1));
                            }
                        }
                    }
                    GameString += "    case " + DOn.ToString() + ":" + Constants.vbCrLf;
                    GameString += "      Instances[InstanceID].Width = " + SW + "; Instances[InstanceID].Height = " + SH + ";" + Constants.vbCrLf;

                    if (iGet(X, 3, ",") == "Nitro")
                    {
                        // Fix palette
                        // Nitro + DSGM
                        GameString += "      FAT_BasicCreateSprite(Instances[InstanceID].Screen, InstanceID, " + (PalletNumber_Nitro + PalletNumber).ToString() + ", \"" + SpriteName + "_Sprite.bin\", \"" + "NitroPal" + PalletNumber_Nitro.ToString() + "_Pal.bin\", OBJ_SIZE_" + SW + "X" + SH + ", 256, 192);" + Constants.vbCrLf;
                    }
                    else
                    {
                        GameString += "      PA_CreateSprite(Instances[InstanceID].Screen, InstanceID, (void*)" + SpriteName + "_Sprite, " + "OBJ_SIZE_" + SW + "X" + SH + ", 1, " + PalletNumber.ToString() + ", 256, 192);" + Constants.vbCrLf;
                    }

                    GameString += "      break;" + Constants.vbCrLf;
                    DOn = (short)(DOn + 1);
                }
                GameString += "  }" + Constants.vbCrLf;
            }
            // FinalString += "  return true;" + vbcrlf
            GameString += "}" + Constants.vbCrLf + Constants.vbCrLf;
            DOn = 0;
            GameString += "void Create_Object(u8 ObjectEnum, u8 InstanceID, bool Screen, s16 X, s16 Y) {" + Constants.vbCrLf;
            // GameString += "  strcpy(Instances[InstanceID].Name, ObjectName);" + vbCrLf
            GameString += "  Instances[InstanceID].EName = ObjectEnum;";
            GameString += "  Instances[InstanceID].InUse = true; Instances[InstanceID].Screen = Screen;" + Constants.vbCrLf;
            GameString += "  Instances[InstanceID].OriginalX = X; Instances[InstanceID].OriginalY = Y;" + Constants.vbCrLf;
            GameString += "  Instances[InstanceID].X = X; Instances[InstanceID].Y = Y;" + Constants.vbCrLf;
            GameString += "  Instances[InstanceID].VX = 0; Instances[InstanceID].VY = 0;" + Constants.vbCrLf;
            if (GetXDSFilter("OBJECT ").Length > 0)
            {
                foreach (string X_ in GetXDSFilter("OBJECT "))
                {
                    string X = X_;
                    X = X.Substring(7);
                    string ObjectName = iGet(X, 0, ",");
                    short ObjectFrame = Convert.ToInt16(iGet(X, 2, ","));
                    string SpriteName = iGet(GetXDSLine("OBJECT " + ObjectName + ","), 1, ",");
                    if (DOn == 0)
                    {
                        GameString += "  if (ObjectEnum == " + ObjectName + ") {" + Constants.vbCrLf;
                    }
                    else
                    {
                        GameString += "  } else if (ObjectEnum == " + ObjectName + ") {" + Constants.vbCrLf;
                    }
                    if (!(SpriteName == "None"))
                    {
                        string SpriteLine = GetXDSLine("SPRITE " + SpriteName + ",");
                        byte SW = Convert.ToByte(iGet(SpriteLine, 1, ","));
                        byte SH = Convert.ToByte(iGet(SpriteLine, 2, ","));
                        GameString += "     Instances[InstanceID].HasSprite = true;" + Constants.vbCrLf;
                        GameString += "     Set_Sprite(InstanceID, \"" + SpriteName + "\", false);" + Constants.vbCrLf;
                        if (ObjectFrame > 0)
                            GameString += "     Set_Frame(InstanceID, " + ObjectFrame.ToString() + ");" + Constants.vbCrLf;
                    }
                    else
                    {
                        GameString += "     Instances[InstanceID].HasSprite = false;" + Constants.vbCrLf;
                        // FinalString += "     PA_CreateSprite(Screen, InstanceID, (void*)" + SpriteName + "_Sprite, OBJ_SIZE_" + SW.ToString + "X" + SH.ToString + ", 1, " + PalletNumber.ToString + ", 256, 192);" + vbcrlf
                    }
                    // FinalString += "     #ifdef " + ObjectName + "CreateExists" + vbcrlf
                    if (DoesXDSLineExist("EVENT " + ObjectName + ",1,NoData"))
                    {
                        GameString += "     " + ObjectName + "Create_Event(InstanceID);" + Constants.vbCrLf;
                    }
                    // FinalString += "       " + ObjectName + "Create_Event(" + DOn.ToString + ");" + vbcrlf
                    // FinalString += "     #endif" + vbcrlf
                    // GameString += "     break;" + vbcrlf
                    DOn = (short)(DOn + 1);
                }
                GameString += "  }" + Constants.vbCrLf + Constants.vbCrLf;
            }
            // GameString += "  return true;" + vbcrlf
            GameString += "}" + Constants.vbCrLf;
            DOn = 0;
            GameString += "u8 Sprite_Get_ID(char *SpriteName) {" + Constants.vbCrLf;
            foreach (string X_ in GetXDSFilter("SPRITE "))
            {
                string X = X_;
                X = X.Substring(7);
                string SpriteName = iGet(X, 0, ",");
                GameString += " if (strcmp(SpriteName, \"" + SpriteName + "\") == 0) return " + DOn.ToString() + ";" + Constants.vbCrLf;
                DOn = (short)(DOn + 1);
            }
            GameString += " return 0;" + Constants.vbCrLf;
            GameString += "}" + Constants.vbCrLf + Constants.vbCrLf;
            DOn = 0;
            GameString += "u8 Room_Get_Index(char *RoomName) {" + Constants.vbCrLf;
            foreach (string X_ in GetXDSFilter("ROOM "))
            {
                string X = X_;
                X = X.Substring(5);
                string RoomName = iGet(X, 0, ",");
                GameString += " if (strcmp(RoomName, \"" + RoomName + "\") == 0) return " + DOn.ToString() + ";" + Constants.vbCrLf;
                DOn = (short)(DOn + 1);
            }
            GameString += " return 0;" + Constants.vbCrLf;
            GameString += "}" + Constants.vbCrLf + Constants.vbCrLf;
            DOn = 0;
            GameString += "void Goto_Room_Backend(u8 RoomIndex) {" + Constants.vbCrLf;
            GameString += "  RoomFrames = 0;" + Constants.vbCrLf;
            GameString += "  RoomSeconds = 0;" + Constants.vbCrLf;
            foreach (string X_ in GetXDSFilter("ROOM "))
            {
                string X = X_;
                X = X.Substring(5);
                string RoomName = iGet(X, 0, ",");
                GameString += "  if (RoomIndex == " + DOn.ToString() + ") " + RoomName + "();" + Constants.vbCrLf;
                DOn = (short)(DOn + 1);
            }
            GameString += "}" + Constants.vbCrLf + Constants.vbCrLf;
            GameString += "void Goto_Next_Room(void) {" + Constants.vbCrLf;
            GameString += " if (CurrentRoom < RoomCount) {" + Constants.vbCrLf;
            GameString += "  CurrentRoom += 1;" + Constants.vbCrLf;
            GameString += "  Goto_Room_Backend(CurrentRoom);" + Constants.vbCrLf;
            GameString += " }" + Constants.vbCrLf;
            GameString += "}" + Constants.vbCrLf + Constants.vbCrLf;
            // GameString += "void Goto_Room(char *RoomName) {" + vbcrlf
            // GameString += "  Goto_Room_Backend(Room_Get_Index(RoomName));" + vbcrlf
            // GameString += "}" + vbcrlf
            File.WriteAllText(SessionsLib.CompilePath + "include/GameWorks.h", GameString);
            string MF = "ARM7_SELECTED = ARM7_MP3_DSWIFI" + Constants.vbCrLf;
            MF += "USE_NITROFS  = YES" + Constants.vbCrLf;
            MF += "NITRODATA   := nitrofiles" + Constants.vbCrLf;
            MF += "TEXT1        := " + GetXDSLine("PROJECTNAME ").ToString().Substring(12) + Constants.vbCrLf;
            MF += "TEXT2        := " + GetXDSLine("TEXT2 ").ToString().Substring(6) + Constants.vbCrLf;
            MF += "TEXT3        := " + GetXDSLine("TEXT3 ").ToString().Substring(6) + Constants.vbCrLf;
            MF += "TARGET       := $(shell basename $(CURDIR))" + Constants.vbCrLf;
            MF += "BUILD        := build" + Constants.vbCrLf;
            MF += "SOURCES      := source data gfx/bin" + Constants.vbCrLf;
            MF += "INCLUDES     := include build data gfx" + Constants.vbCrLf;
            MF += "RELEASEPATH  := " + Constants.vbCrLf;
            MF += "MAKEFILE_VER := ver2" + Constants.vbCrLf;
            MF += "include " + Constants.AppDirectory + "devkitPro/PAlib/lib/PA_Makefile" + Constants.vbCrLf;
            File.WriteAllText(SessionsLib.CompilePath + "Makefile", MF);
            Program.Forms.main_Form.compileForm.CustomPerformStep("Processing Graphics");
            var MyProcess = new Process();
            var MyInfo = new ProcessStartInfo();
            if (RedoAllGraphics | BGsToRedo.Count > 0 | RedoSprites)
            {
                MyInfo.FileName = SessionsLib.CompilePath + "gfx/PAGfx.exe";
                MyInfo.WorkingDirectory = SessionsLib.CompilePath + "gfx";
                MyProcess.StartInfo = MyInfo;
                MyProcess.Start();
                MyProcess.WaitForExit();
            }
            File.Delete(SessionsLib.CompilePath + "gfx/PAGfx.txt");
            File.Delete(SessionsLib.CompilePath + "gfx/all_gfx.h");
            // Make a hacky GFX file ...
            string DSGMH = string.Empty;
            DSGMH += "#pragma once" + Constants.vbCrLf;
            DSGMH += "#include <PA_BgStruct.h>" + Constants.vbCrLf + Constants.vbCrLf;
            DSGMH += "//Sprites:" + Constants.vbCrLf;
            foreach (string X_ in GetXDSFilter("SPRITE "))
            {
                string X = X_;
                X = X.Substring(7);
                if (iGet(GetXDSLine("SPRITE " + X), 3, ",") == "Nitro")
                {
                    if (File.Exists(SessionsLib.CompilePath + "gfx/bin/" + iGet(X, 0, ",") + "_Sprite.bin"))
                    {
                        if (File.Exists(SessionsLib.CompilePath + "nitrofiles/" + iGet(X, 0, ",") + "_Sprite.bin"))
                            File.Delete(SessionsLib.CompilePath + "nitrofiles/" + iGet(X, 0, ",") + "_Sprite.bin");
                        File.Move(SessionsLib.CompilePath + "gfx/bin/" + iGet(X, 0, ",") + "_Sprite.bin", SessionsLib.CompilePath + "nitrofiles/" + iGet(X, 0, ",") + "_Sprite.bin");
                    }
                    byte PalletNumber = 0;
                    foreach (var PalletString in PalletNumbers)
                    {
                        if (PalletString.Key.StartsWith(X + ","))
                        {
                            PalletNumber = Convert.ToByte(PalletString.Key.Substring(PalletString.Value.IndexOf(",") + 1));
                        }
                    }

                    if (File.Exists(SessionsLib.CompilePath + "gfx/bin/" + "NitroPal" + PalletNumber.ToString() + "_Pal.bin"))
                    {
                        if (File.Exists(SessionsLib.CompilePath + "nitrofiles/" + "NitroPal" + PalletNumber.ToString() + "_Pal.bin"))
                            File.Delete(SessionsLib.CompilePath + "nitrofiles/" + "NitroPal" + PalletNumber.ToString() + "_Pal.bin");
                        File.Move(SessionsLib.CompilePath + "gfx/bin/" + "NitroPal" + PalletNumber.ToString() + "_Pal.bin", SessionsLib.CompilePath + "nitrofiles/" + "NitroPal" + PalletNumber.ToString() + "_Pal.bin");
                    }
                }
                else
                {
                    string SpriteName = X.Substring(0, X.IndexOf(","));
                    var TheSize = GenerateDSSprite(SpriteName).Size;
                    long Timez = TheSize.Width * TheSize.Height;
                    DSGMH += "  extern const unsigned char " + SpriteName + "_Sprite[" + Timez.ToString() + "] _GFX_ALIGN;" + Constants.vbCrLf;
                }
            }
            DSGMH += Constants.vbCrLf + "//Backgrounds:" + Constants.vbCrLf;
            foreach (string X_ in GetXDSFilter("BACKGROUND "))
            {
                string X = X_;
                X = X.Substring(11);
                if (iGet(GetXDSLine("BACKGROUND " + X), 1, ",") == "Nitro")
                {

                    if (File.Exists(SessionsLib.CompilePath + "gfx/bin/" + iGet(X, 0, ",") + "_Map.bin"))
                    {
                        if (File.Exists(SessionsLib.CompilePath + "nitrofiles/" + iGet(X, 0, ",") + "_Map.bin"))
                            File.Delete(SessionsLib.CompilePath + "nitrofiles/" + iGet(X, 0, ",") + "_Map.bin");
                        File.Move(SessionsLib.CompilePath + "gfx/bin/" + iGet(X, 0, ",") + "_Map.bin", SessionsLib.CompilePath + "nitrofiles/" + iGet(X, 0, ",") + "_Map.bin");
                    }
                    if (File.Exists(SessionsLib.CompilePath + "gfx/bin/" + iGet(X, 0, ",") + "_Tiles.bin"))
                    {
                        if (File.Exists(SessionsLib.CompilePath + "nitrofiles/" + iGet(X, 0, ",") + "_Tiles.bin"))
                            File.Delete(SessionsLib.CompilePath + "nitrofiles/" + iGet(X, 0, ",") + "_Tiles.bin");
                        File.Move(SessionsLib.CompilePath + "gfx/bin/" + iGet(X, 0, ",") + "_Tiles.bin", SessionsLib.CompilePath + "nitrofiles/" + iGet(X, 0, ",") + "_Tiles.bin");
                    }
                    if (File.Exists(SessionsLib.CompilePath + "gfx/bin/" + iGet(X, 0, ",") + "_Pal.bin"))
                    {
                        if (File.Exists(SessionsLib.CompilePath + "nitrofiles/" + iGet(X, 0, ",") + "_Pal.bin"))
                            File.Delete(SessionsLib.CompilePath + "nitrofiles/" + iGet(X, 0, ",") + "_Pal.bin");
                        File.Move(SessionsLib.CompilePath + "gfx/bin/" + iGet(X, 0, ",") + "_Pal.bin", SessionsLib.CompilePath + "nitrofiles/" + iGet(X, 0, ",") + "_Pal.bin");
                    }
                    if (File.Exists(SessionsLib.CompilePath + "gfx/bin/" + iGet(X, 0, ",") + ".c"))
                    {
                        if (File.Exists(SessionsLib.CompilePath + "nitrofiles/" + iGet(X, 0, ",") + ".c"))
                            File.Delete(SessionsLib.CompilePath + "nitrofiles/" + iGet(X, 0, ",") + ".c");
                        File.Move(SessionsLib.CompilePath + "gfx/bin/" + iGet(X, 0, ",") + ".c", SessionsLib.CompilePath + "nitrofiles/" + iGet(X, 0, ",") + ".c");
                    }
                }
                else
                {
                    DSGMH += "  extern const PA_BgStruct " + X + ";" + Constants.vbCrLf;
                }
            }
            if (PalletNames.Count > 0)
            {
                DSGMH += Constants.vbCrLf + "//Pallets:" + Constants.vbCrLf;
                for (byte i = 0, loopTo4 = (byte)(PalletNames.Count - 1); i <= loopTo4; i++)// BRB!
                {
                    DSGMH += "  extern const unsigned short DSGMPal" + i.ToString() + "_Pal[256] _GFX_ALIGN;" + Constants.vbCrLf;
                }
            }
            File.WriteAllText(SessionsLib.CompilePath + "gfx/dsgm_gfx.h", DSGMH);
            Program.Forms.main_Form.compileForm.CustomPerformStep("Compiling Game");
            MyInfo.FileName = SessionsLib.CompilePath + "build.bat";
            MyInfo.WorkingDirectory = SessionsLib.CompilePath;
            MyProcess.StartInfo = MyInfo;
            MyProcess.Start();
            MyProcess.WaitForExit();
            if (File.Exists(SessionsLib.CompilePath + SessionsLib.CompileName + ".nds"))
            {
                RedoAllGraphics = false;
                RedoSprites = false;
                BGsToRedo.Clear();
                SoundsToRedo.Clear();
                FontsUsedLastTime.Clear();
                foreach (string X in FontsUsedThisTime)
                    FontsUsedLastTime.Add(X);
                return true;
            }
            return false;
        }

        public static string FormNOGBAPath()
        {
            string Returnable = Constants.AppDirectory + "NO$GBA";
            if (!Directory.Exists(Returnable))
            {
                MessageBox.Show("The emulator NO$GBA was not found.");
            }
            //    Returnable = Constants.AppDirectory + "NO$GBA";
            return Returnable;
        }

        public static void NOGBAShizzle()
        {
            if ((int)Convert.ToByte(SettingsLib.GetSetting("USE_NOGBA")) == 1)
            {
                if (!Directory.Exists(FormNOGBAPath()))
                {
                    MsgError("NO$GBA was not found." + Constants.vbCrLf + Constants.vbCrLf + "Please reinstall " + Application.ProductName + ".");
                    return;
                }
                // MsgError(CompilePath + GetXDSLine("PROJECTNAME ").ToString.Substring(12) + ".nds")
                Process.Start(FormNOGBAPath() + "/NO$GBA.exe", SessionsLib.CompilePath + "DSGMTemp" + SessionsLib.Session + ".nds");
            }
            else
            {
                if (!File.Exists(SettingsLib.GetSetting("EMULATOR_PATH")))
                {
                    MsgError("The selected Custom Emulator does not exist.");
                    return;
                }
                Process.Start(SettingsLib.GetSetting("EMULATOR_PATH"), SessionsLib.CompilePath + "DSGMTemp" + SessionsLib.Session + ".nds");
            }
        }

        public static short HowManyChar(string TheText, string WhichChar)
        {
            if (TheText.Length == 0)
                return 0;
            short Returnable = 0;
            for (int X = 0, loopTo = TheText.Length - 1; X <= loopTo; X++)
            {
                if (TheText.Substring(X, 1) == WhichChar)
                    Returnable = (short)(Returnable + 1);
            }
            return Returnable;
        }

        public static bool IsNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            double argresult = 0d;
            return double.TryParse(val, NumberStyle, System.Globalization.CultureInfo.CurrentCulture, out argresult);
        }

        public static void MsgWarn(string msg)
        {
            MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void MsgError(string msg)
        {
            MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void MsgInfo(string msg, string title = "DS Game Maker")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static object CanDivide(short num1, short num2)
        {
            if (num1 == 0 | num2 == 0)
            {
                return false;
            }
            if (num1 % num2 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static short ImageCountColors(Bitmap TheImage)
        {
            short DOn = 0;
            if (TheImage.Palette.Entries.Length == 0)
            {
                var AllColors = new Dictionary<string, string>();
                for (short X = 0, loopTo = (short)(TheImage.Width - 1); X <= loopTo; X++)
                {
                    for (short Y = 0, loopTo1 = (short)(TheImage.Height - 1); Y <= loopTo1; Y++)
                    {
                        var TheColor = TheImage.GetPixel(X, Y);
                        string TheColorString = TheColor.R.ToString() + TheColor.G.ToString() + TheColor.B.ToString();
                        bool AlreadyThere = false;
                        foreach (var P in AllColors)
                        {
                            if (P.Key ==TheColorString)
                            {
                                AlreadyThere = true;
                                break;
                            }
                        }
                        if (!AlreadyThere)
                        {
                            AllColors.Add("", TheColorString);
                        }
                    }
                }
                return (short)AllColors.Count;
            }
            else
            {
                foreach (Color MyColor in TheImage.Palette.Entries)
                {
                    if (!(DOn == 0) & MyColor.R == 0 & MyColor.G == 0 & MyColor.B == 0)
                        break;
                    DOn = (short)(DOn + 1);
                }
                return DOn;
            }
        }

        public static bool EditImage(string FilePath, string ResourceName, bool CustomMessage)
        {
            string FinalEXE = SettingsLib.GetSetting("IMAGE_EDITOR_PATH");
            FinalEXE = File.Exists(FinalEXE) ? FinalEXE : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "mspaint.exe");

            ProcessStartInfo startInfo = new()
            {
                FileName = FinalEXE,
                Arguments = $"\"{FilePath}\"",
                WorkingDirectory = Path.GetDirectoryName(FilePath) ?? string.Empty,
                UseShellExecute = true
            };

            Process.Start(startInfo);

            //Process.Start(FinalEXE, "/"" + FilePath + ".png/"");

            string Message = "'" + ResourceName + "' has been opened. When you are finished, click 'OK'." + Constants.vbCrLf + Constants.vbCrLf + "To reverse any changes, please click 'Cancel'.";
            if (CustomMessage)
            {
                Message = "'" + ResourceName + "' has been opened. When you are finished, click 'OK'." + Constants.vbCrLf + Constants.vbCrLf + "You should then re-add the Frame to the Sprite.";
            }
            DialogResult Response = MessageBox.Show(Message, Application.ProductName, CustomMessage ? MessageBoxButtons.OK : MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (Response == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool EditSound(string FilePath, string ResourceName)
        {
            Process.Start(SettingsLib.GetSetting("SOUND_EDITOR_PATH"), "\"" + FilePath + "\"");
            
            string Message = "'" + ResourceName + "' has been opened. When you are finished, click 'OK'." + Constants.vbCrLf + Constants.vbCrLf + "To reverse any changes, please click 'Cancel'.";
            
            if (MessageBox.Show(Message, Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public enum ValidateLevel : byte
        {
            None = 0,
            NumberStart = 2,
            Loose = 4,
            Tight = 5
        }

        public enum CodeMode : byte
        {
            DBAS = 0,
            XDS = 1,
            C = 2
        }

        public static bool ValidateSomething(string ThingToValidate, byte VMode)
        {
            if (VMode == (byte)ValidateLevel.None)
                return true;
            if (ThingToValidate.Contains("!"))
                return false;
            if (ThingToValidate.Length == 0)
                return false;
            bool Returnable = true;
            if (VMode == (byte)ValidateLevel.NumberStart | VMode == (byte)ValidateLevel.Loose)
            {
                if (ThingToValidate.StartsWith(" "))
                    Returnable = false;
                foreach (string Number in Numbers)
                {
                    if (ThingToValidate.StartsWith(Number))
                        Returnable = false;
                }
            }
            if (VMode == (byte)ValidateLevel.Tight)
            {
                foreach (string BannedChar in BannedChars)
                {
                    if (ThingToValidate.Contains(BannedChar))
                        Returnable = false;
                }
            }
            return Returnable;
        }

        public static string OpenFile(string Directory, string Filter)
        {
            string Returnable;
            var OFD = new OpenFileDialog();
            OFD.InitialDirectory = Directory;
            OFD.FileName = string.Empty;
            OFD.Filter = Filter;
            OFD.Title = "Open File";
            if (!(OFD.ShowDialog() == DialogResult.OK))
                Returnable = string.Empty;
            else
                Returnable = OFD.FileName.Replace('\\', '/');
            OFD.Dispose();
            if (Returnable.Length > 0)
            {
                string PR = Returnable.Substring(0, Returnable.LastIndexOf("/"));
                LoadDefaultFolder = PR;
            }
            return Returnable;
        }

        public static void XDSChangeLine(string Input, string ChangeTo)
        {
            string FinalString = string.Empty;
            foreach (string XDSLine_ in StringToLines(CurrentXDS))
            {
                string XDSLine = XDSLine_;
                if (XDSLine == Input)
                    XDSLine = ChangeTo;
                FinalString += XDSLine + Constants.vbCrLf;
            }
            CurrentXDS = FinalString;
        }

        public static string GetInput(string Descriptor, string DefaultValue, byte VM)
        {
            var X = new InputBoxForm();
            X.Descriptor = Descriptor;
            X.TheInput = DefaultValue;
            X.Validation = VM;
            X.ShowDialog();
            string ToGive = X.TheInput;
            X.Dispose();
            return ToGive;
        }

        public static Color SelectColor(Color InputColor)
        {
            var CPicker = new ColorDialog();
            CPicker.AllowFullOpen = false;
            CPicker.AnyColor = true;
            CPicker.Color = InputColor;
            if (CPicker.ShowDialog() == DialogResult.OK)
                return CPicker.Color;
            return InputColor;
        }

        public static string PathToString(string path)
        {
            return System.Text.Encoding.ASCII.GetString(SafeGetFileData(path));
        }

        public static Image PathToImage(string path)
        {
            byte[] imgData = SafeGetFileImage(path);
            return new Bitmap(Image.FromStream(new MemoryStream(imgData)));
        }

        public static byte[] SafeGetFileImage(string filePath)
        {
            bool supported = false;

            string[] extensions = [".bmp",".gif",".png", ".jpg", ".jpeg"];

            foreach (string ext in extensions)
            {
                if (ext == Path.GetExtension(filePath))
                {
                    supported = true;
                }
            }

            if(supported == false)
            {
                return Array.Empty<byte>();
            }

            using (FileStream MyFileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader MyBinaryReader = new BinaryReader(MyFileStream))
                {
                    byte[] FinalData = MyBinaryReader.ReadBytes((int)MyFileStream.Length);
                    MyBinaryReader.Close();
                    MyFileStream.Close();

                    return FinalData;
                }
            }
        }

        public static byte[] SafeGetFileData(string filePath)
        {
            using (FileStream MyFileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader MyBinaryReader = new BinaryReader(MyFileStream))
                {
                    byte[] FinalData = MyBinaryReader.ReadBytes((int)MyFileStream.Length);
                    MyBinaryReader.Close();
                    MyFileStream.Close();

                    return FinalData;
                }
            }
        }

        public static Image MakeBMPTransparent(Image InputImage, Color InputColor)
        {
            using (Bitmap Returnable = (Bitmap)InputImage)
            {
                Returnable.MakeTransparent(InputColor);
                return Returnable;
            }
        }

        private static List<short> Positions = new List<short>();

        public enum ResourceIDs
        {
            Sprite = 0,
            DObject = 1,
            Background = 2,
            Sound = 3,
            Room = 4,
            Path = 5,
            Script = 6
        }

        public static void XDSAddLine(string NewLineContent)
        {
            CurrentXDS += Constants.vbCrLf + NewLineContent;
        }

        public static short XDSCountLines(string Filter)
        {
            short Returnable = 0;
            foreach (string XDSLine in StringToLines(CurrentXDS))
            {
                if (XDSLine.StartsWith(Filter))
                    Returnable = (short)(Returnable + 1);
            }
            return Returnable;
        }

        public static string MakeResourceName(string ResourcePrefix, string XDSPrefix)
        {
            for (short i = 1; i <= 1024; i++)
            {
                if (XDSCountLines(XDSPrefix + " " + ResourcePrefix + "_" + i.ToString()) == 0)
                    return ResourcePrefix + "_" + i.ToString();
            }
            return string.Empty;
        }

        public static void UpdateArrayActionsName(string ResourceTypeString, string OldName, string NewName, bool AppliesToAlso)
        {
            foreach (Form X in Program.Forms.main_Form.MdiChildren)
            {
                if (!(X.Name == "DObject"))
                    continue;
                DObject DForm = (DObject)X;
                for (short DOn = 0, loopTo = (short)(DForm.ActionArguments.Count - 1); DOn <= loopTo; DOn++)
                {
                    string Y = DForm.ActionArguments[(int)DOn];
                    // Dim NewString As String = String.Empty
                    string ActionName = DForm.Actions[(int)DOn];
                    string NewArgumentString = string.Empty;
                    string TypeString = GetActionTypes(ActionName);
                    for (byte P = 0, loopTo1 = (byte)HowManyChar(TypeString, ","); P <= loopTo1; P++)
                    {
                        string ToAdd = iGet(Y, P, ";");
                        string TType = iGet(TypeString, P, ",");
                        if ((TType == ResourceTypeString) && (ToAdd == OldName))
                            ToAdd = NewName;
                        NewArgumentString += ToAdd + ";";
                    }
                    NewArgumentString = NewArgumentString.Substring(0, NewArgumentString.Length - 1);
                    // MsgError("Y is " + Y)
                    // MsgError("NewString is " + NewArgumentString)
                    // Y = iget(Y, 0, ",") + "," + iget(Y, 1, ",") + "," + iget(Y, 2, ",") + "," + ActionName + "," + NewArgumentString + "," + iget(Y, 5, ",")
                    if (NewArgumentString != Y)
                    {
                        DForm.ActionArguments[(int)DOn] = NewArgumentString;
                        DForm.ActionDisplays[(int)DOn] = ActionsLib.ActionEquateDisplay(ActionName, NewArgumentString);
                    }
                    // Something here, James?
                    // Damn, shouldn't do more than one thing at once >.<
                }
                if (AppliesToAlso)
                {
                    for (short Don = 0, loopTo2 = (short)(DForm.ActionAppliesTos.Count - 1); Don <= loopTo2; Don++)
                    {
                        if (DForm.ActionAppliesTos[Don] == OldName)
                        {
                            DForm.ActionAppliesTos[(int)Don] = NewName;
                        }
                    }
                }
                DForm.ActionsList.Refresh();
            }
        }

        private static byte TempSpaces = 0;
        private static byte GAmount = 2;

        public static void IntelliSense(ref ScintillaNET.Scintilla TheControl)
        {
            string[] Starters = new string[] { "if", "while", "for", "with" };
            string[] Enders = new string[] { "end if", "end while", "next", "end with" };
            string[] CapsEnders = new string[] { "End If", "End While", "Next", "End With" };
            //string P = TheControl.Lines[TheControl.Caret.Position - 1].Text;
            string P = TheControl.Text;


            if ((P.Length <= 2) || (P.Length == 0))
            {
                return;
            }            
            
            P = P.Substring(0, P.Length - 2);

            sbyte SpaceCount = 0;
            for (byte i = 0, loopTo = (byte)(P.Length - 1); i <= loopTo; i++)
            {
                if (!P.Substring(i).StartsWith(" "))
                    break;
                SpaceCount = (sbyte)(SpaceCount + 1);
            }
            string F = P.Substring(SpaceCount).ToLower();
            byte Amount = 1;
            if (SpaceCount % 2 == 0)
                Amount = 2;
            byte FID = 100;
            for (byte DOn = 0, loopTo1 = (byte)(Starters.Count() - 1); DOn <= loopTo1; DOn++)
            {
                if (F.StartsWith(Starters[DOn] + " "))
                {
                    FID = DOn;
                    break;
                }
            }
            if (!(FID == 100))
            {
                TheControl.InsertText(0, (string)MakeSpaces((byte)(SpaceCount + Amount)));
                bool DoAdd = true;
                //for (byte i = (byte)TheControl.Caret.Position, loopTo2 = (byte)(TheControl.Lines.Count - 1); i <= loopTo2; i++)
                for (byte i = (byte)TheControl.CurrentPosition, loopTo2 = (byte)(TheControl.Lines.Count - 1); i <= loopTo2; i++)
                {
                    string L = TheControl.Lines[i].Text;
                    L = L.Substring(0, L.Length - 2).ToLower();
                    if (L.Length == 0 | L.Length <= SpaceCount)
                        continue;
                    L = L.Substring(SpaceCount);
                    if (L == Enders[FID].ToLower())
                        DoAdd = false;
                }
                if (DoAdd)
                {
                    //short BackupPos = (short)TheControl.Caret.Position;
                    //TheControl.InsertText(0, Conversions.ToString(Operators.AddObject(Operators.AddObject(Constants.vbCrLf, MakeSpaces((byte)SpaceCount)), CapsEnders[FID])));
                    //TheControl.Caret.Position = BackupPos;
                    // TheControl.SelectedText.Length = 0; Wat?
                }
                return;
            }
            bool IsOne = false;
            foreach (string D in Enders)
            {
                if (F == D)
                {
                    IsOne = true;
                }
            }
            if (F.StartsWith("next "))
                IsOne = true;
            if (IsOne & SpaceCount == TempSpaces)
            {
                SpaceCount = (sbyte)(SpaceCount - Amount);
                string FX = P;
                if (FX.StartsWith((char)MakeSpaces(Amount)))
                {
                    FX = FX.Substring(Amount);
                    //TheControl.Lines[TheControl.Caret.LineNumber - 1].Text = FX;
                }
            }
            if (SpaceCount < 0)
                SpaceCount = 0;
            TempSpaces = (byte)SpaceCount;
            TheControl.InsertText(0,(string)MakeSpaces((byte)SpaceCount));
        }

        public static void SilentMoveFile(string FromPath, string ToPath)
        {
            var BackupDate = File.GetLastWriteTime(FromPath);
            File.Move(FromPath, ToPath);
            File.SetLastWriteTime(ToPath, BackupDate);
        }

        public static string UpdateActionsName(string StringToChange, string ResourceTypeString, string OldName, string NewName, bool AppliesToAlso)
        {
            string FinalString = string.Empty;
            foreach (string Y_ in StringToLines(StringToChange))
            {
                string Y = Y_;
                if (Y.StartsWith("ACT "))
                {
                    string ActionName = iGet(Y, 3, ",");
                    string InputtedArgumentString = iGet(Y, 4, ",");
                    string NewArgumentString = string.Empty;
                    string TypeString = GetActionTypes(ActionName);
                    for (byte P = 0, loopTo = (byte)HowManyChar(TypeString, ","); P <= loopTo; P++)
                    {
                        string ToAdd = iGet(InputtedArgumentString, P, ";");
                        string TType = iGet(TypeString, P, ",");
                        if ((TType == ResourceTypeString) && (ToAdd == OldName))
                            ToAdd = NewName;
                        NewArgumentString += ToAdd + ";";
                    }
                    NewArgumentString = NewArgumentString.Substring(0, NewArgumentString.Length - 1);
                    string Thing = iGet(Y, 5, ",");
                    if (AppliesToAlso)
                    {
                        if (Thing == OldName)
                            Thing = NewName;
                    }
                    Y = iGet(Y, 0, ",") + "," + iGet(Y, 1, ",") + "," + iGet(Y, 2, ",") + "," + ActionName + "," + NewArgumentString + "," + Thing;
                }
                FinalString += Y + Constants.vbCrLf;
            }
            return FinalString;
        }

        public static void DeleteResource(string ResourceName, string Type)
        {
            short DOn = 0;
            if (Type == "Room" & GetXDSFilter("ROOM ").Length < 2)
            {
                MsgWarn("You must always have at least one room in a Project.");
                return;
            }

            if (MessageBox.Show("Are you sure you would like to delete '" + ResourceName + "'?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            if (Type == "Sprite")
            {
                DOn = 0;
                // For Each X As String In GraphicsToRedo
                // If X = ResourceName Then GraphicsToRedo.RemoveAt(DOn)
                // DOn += 1
                // Next
                if (Directory.Exists(SessionsLib.CompilePath + "build"))
                {
                    foreach (string X in Directory.GetFiles(SessionsLib.CompilePath + "build"))
                    {
                        if (X.EndsWith("/" + ResourceName + "_Sprite.h") || X.EndsWith("/" + ResourceName + "_Sprite.o"))
                        {
                            File.Delete(X);
                        }
                    }
                }

                if (File.Exists(SessionsLib.CompilePath + "gfx/" + ResourceName + ".png"))
                {
                    File.Delete(SessionsLib.CompilePath + "gfx/" + ResourceName + ".png");
                }

                if (File.Exists(SessionsLib.CompilePath + "gfx/bin/" + ResourceName + "_Sprite.bin"))
                {
                    File.Delete(SessionsLib.CompilePath + "gfx/bin/" + ResourceName + "_Sprite.bin");
                }

                XDSRemoveLine(GetXDSLine("SPRITE " + ResourceName + ","));

                foreach (string X in Directory.GetFiles(SessionsLib.SessionPath + "Sprites"))
                {
                    if (X.EndsWith("_" + ResourceName + ".png"))
                    {
                        File.Delete(X);
                    }
                }

                var AffectedObjects = new List<string>();
                foreach (string X in GetXDSFilter("OBJECT "))
                {
                    if (iGet(X, 1, ",") == ResourceName)
                    {
                        string ObjectName = iGet(X, 0, ",").Substring(7);
                        string NewLine = "OBJECT " + ObjectName + ",None,0";
                        XDSChangeLine(X, NewLine);
                        AffectedObjects.Add(ObjectName);
                    }
                }

                // Update rooms if they contain affected objects >.<
                foreach (Form X in Program.Forms.main_Form.MdiChildren)
                {
                    if (X.Name == "Room")
                    {
                        Room DForm = (Room)X;
                        byte TopAffected = 0;
                        byte BottomAffected = 0;
                        DOn = 0;
                        var loopTo = (short)(DForm.Objects.Count() - 1);
                        for (DOn = 0; DOn <= loopTo; DOn++)
                        {
                            if (AffectedObjects.Contains(DForm.Objects[(int)DOn].ObjectName))
                            {
                                if (DForm.Objects[(int)DOn].Screen)
                                {
                                    TopAffected = (byte)(TopAffected + 1);
                                }
                                else
                                {
                                    BottomAffected = (byte)(BottomAffected + 1);
                                }

                                DForm.Objects[(int)DOn].CacheImage = ObjectGetImage(DForm.Objects[(int)DOn].ObjectName);
                            }
                        }

                        if (TopAffected > 0)
                        {
                            DForm.RefreshRoom(true);
                        }

                        if (BottomAffected > 0)
                        {
                            DForm.RefreshRoom(false);
                        }
                    }
                    else if (X.Name == "DObject")
                    {
                        DObject DForm = (DObject)X;
                        string SpriteName = DForm.GetSpriteName();

                        if (SpriteName == ResourceName)
                        {
                            DForm.DeleteSprite();
                        }

                        DForm.MyXDSLines = UpdateActionsName(DForm.MyXDSLines, "Sprite", ResourceName, "<Unknown>", false);
                        UpdateArrayActionsName("Sprite", ResourceName, "<Unknown>", false);
                    }
                }

                CurrentXDS = UpdateActionsName(CurrentXDS, "Object", ResourceName, "<Unknown>", false);

                foreach (TreeNode X in Program.Forms.main_Form.ResourcesTreeView.Nodes[(int)ResourceIDs.Sprite].Nodes)
                {
                    if (X.Text == ResourceName)
                    {
                        X.Remove();
                        break;
                    }
                }
            }
            else if (Type == "DObject")
            {
                XDSRemoveLine(GetXDSLine("OBJECT " + ResourceName + ","));
                XDSRemoveFilter("EVENT " + ResourceName + ",");
                XDSRemoveFilter("ACT " + ResourceName + ",");
                XDSRemoveFilter("OBJECTPLOT " + ResourceName + ",");

                foreach (Form X in Program.Forms.main_Form.MdiChildren)
                {
                    if (X.Text == ResourceName)
                    {
                        continue;
                    }

                    if (X.Name == "Room")
                    {
                        Room DForm = (Room)X;
                        DForm.RemoveObjectFromDropper(ResourceName);
                        byte TopAffected = 0;
                        byte BottomAffected = 0;
                        DOn = 0;

                        foreach (Room.AnObject Y in DForm.Objects)
                        {
                            if (Y.InUse && (Y.ObjectName == ResourceName))
                            {
                                DForm.Objects[DOn].CacheImage = EmptyBitmap();
                                DForm.Objects[DOn].InUse = false;
                                DForm.Objects[DOn].ObjectName = string.Empty;
                                DForm.Objects[DOn].X = 0;
                                DForm.Objects[DOn].Y = 0;

                                if (DForm.Objects[DOn].Screen)
                                {
                                    TopAffected = (byte)(TopAffected + 1);
                                }
                                else
                                {
                                    BottomAffected = (byte)(BottomAffected + 1);
                                }
                            }
                            DOn = (short)(DOn + 1);
                        }

                        if (TopAffected > 0)
                        {
                            DForm.RefreshRoom(true);
                        }

                        if (BottomAffected > 0)
                        {
                            DForm.RefreshRoom(false);
                        }
                    }
                    else if (X.Name == "DObject")
                    {
                        DObject DForm = (DObject)X;
                        string FinalXDS = string.Empty;
                        foreach (string Y_ in DSGMlib.StringToLines(DForm.MyXDSLines))
                        {
                            string Y = Y_;

                            if (Y.Length == 0)
                            {
                                continue;
                            }

                            if (Y.StartsWith("EVENT "))
                            {
                                if ((iGet(Y, 1, ",") == "6") && (iGet(Y, 2, ",") == ResourceName))
                                {
                                    Y = iGet(Y, 0, ",") + ",6,<Unknown>";
                                }
                            }
                            // If Y.StartsWith("ACT ") And Y.EndsWith("," + ResourceName) Then
                            // Dim Z As String = Y
                            // Z = Z.Substring(0, Z.Length - iget(Z, 5, ",").ToString.Length) + "<Unknown>"
                            // Y = Z
                            // End If
                            FinalXDS += Y + Constants.vbCrLf;
                        }
                        FinalXDS = UpdateActionsName(FinalXDS, "Object", ResourceName, "<Unknown>", true);
                        DForm.MyXDSLines = FinalXDS;
                    }
                }

                foreach (TreeNode X in Program.Forms.main_Form.ResourcesTreeView.Nodes[(int)ResourceIDs.DObject].Nodes)
                {
                    //X.TreeView.Refresh();
                    //Program.Forms.main_Form.ResourcesTreeView.Refresh();

                    if (X.Text == ResourceName)
                    {
                        //Program.Forms.main_Form.ResourcesTreeView.Nodes[1].Nodes.Remove(X);
                        X.Remove();
                        //throw new PingException(ResourceName);
                        break;
                    }
                }
            }
            else if (Type == "Background")
            {
                XDSRemoveLine(GetXDSLine("BACKGROUND " + ResourceName));
                File.Delete(SessionsLib.SessionPath + "Backgrounds/" + ResourceName + ".png");
                if (File.Exists(SessionsLib.CompilePath + "gfx/" + ResourceName + ".png"))
                {
                    File.Delete(SessionsLib.CompilePath + "gfx/" + ResourceName + ".png");
                }
                if (File.Exists(SessionsLib.CompilePath + "gfx/bin/" + ResourceName + ".c"))
                {
                    File.Delete(SessionsLib.CompilePath + "gfx/bin/" + ResourceName + ".c");
                }
                if (File.Exists(SessionsLib.CompilePath + "gfx/bin/" + ResourceName + "_Map.bin"))
                {
                    File.Delete(SessionsLib.CompilePath + "gfx/bin/" + ResourceName + "_Map.bin");
                }
                if (File.Exists(SessionsLib.CompilePath + "gfx/bin/" + ResourceName + "_Tiles.bin"))
                {
                    File.Delete(SessionsLib.CompilePath + "gfx/bin/" + ResourceName + "_Tiles.bin");
                }
                if (File.Exists(SessionsLib.CompilePath + "gfx/bin/" + ResourceName + "_Pal.bin"))
                {
                    File.Delete(SessionsLib.CompilePath + "gfx/bin/" + ResourceName + "_Pal.bin");
                }
                if (File.Exists(SessionsLib.CompilePath + "nitrofiles/" + ResourceName + ".c"))
                {
                    File.Delete(SessionsLib.CompilePath + "nitrofiles/" + ResourceName + ".c");
                }
                if (File.Exists(SessionsLib.CompilePath + "nitrofiles/" + ResourceName + "_Map.bin"))
                {
                    File.Delete(SessionsLib.CompilePath + "nitrofiles/" + ResourceName + "_Map.bin");
                }
                if (File.Exists(SessionsLib.CompilePath + "nitrofiles/" + ResourceName + "_Tiles.bin"))
                {
                    File.Delete(SessionsLib.CompilePath + "nitrofiles/" + ResourceName + "_Tiles.bin");
                }
                if (File.Exists(SessionsLib.CompilePath + "nitrofiles/" + ResourceName + "_Pal.bin"))
                {
                    File.Delete(SessionsLib.CompilePath + "nitrofiles/" + ResourceName + "_Pal.bin");
                }
                if (Directory.Exists(SessionsLib.CompilePath + "build"))
                {
                    foreach (string X in Directory.GetFiles(SessionsLib.CompilePath + "build"))
                    {
                        string FName = X.Substring(X.LastIndexOf("/") + 1);
                        FName = FName.Substring(0, FName.LastIndexOf("."));
                        if (FName.Contains("_"))
                            FName = FName.Substring(0, FName.LastIndexOf("_"));
                        if (FName == ResourceName)
                            File.Delete(X);
                    }
                }
                if (BGsToRedo.Contains(ResourceName))
                    BGsToRedo.Remove(ResourceName);
                foreach (string X in StringToLines(CurrentXDS))
                {
                    if (!X.StartsWith("ROOM "))
                        continue;
                    string NewLine = X.Replace("," + ResourceName, ",");
                    if (!(NewLine.Length == X.Length))
                        XDSChangeLine(X, NewLine);
                }
                foreach (Form X in Program.Forms.main_Form.MdiChildren)
                {
                    if (X.Text == ResourceName)
                        continue;
                    if (X.Name == "Room")
                    {
                        ((Room)X).RemoveBackground(ResourceName);
                        continue;
                    }
                    if (X.Name == "DObject")
                    {
                        DObject DForm = (DObject)X;
                        DForm.MyXDSLines = DSGMlib.UpdateActionsName(DForm.MyXDSLines, "Background", ResourceName, "<Unknown>", false);
                    }
                }
                CurrentXDS = UpdateActionsName(CurrentXDS, "Background", ResourceName, "<Unknown>", false);
                UpdateArrayActionsName("Background", ResourceName, "<Unknown>", false);
                foreach (TreeNode X in Program.Forms.main_Form.ResourcesTreeView.Nodes[(int)ResourceIDs.Background].Nodes)
                {
                    if (X.Text == ResourceName)
                        X.Remove();
                }
            }
            else if (Type == "Sound")
            {
                string SoundLine = GetXDSLine("SOUND " + ResourceName + ",");
                string Extension = SoundLine.EndsWith(",0") ? "wav" : "mp3";
                XDSRemoveLine(SoundLine);
                File.Delete(SessionsLib.SessionPath + "Sounds/" + ResourceName + "." + Extension);
                CurrentXDS = UpdateActionsName(CurrentXDS, "Sound", ResourceName, "<Unknown>", false);
                UpdateArrayActionsName("Sound", ResourceName, "<Unknown>", false);
                foreach (TreeNode X in Program.Forms.main_Form.ResourcesTreeView.Nodes[(int)ResourceIDs.Sound].Nodes)
                {
                    if (X.Text == ResourceName)
                        X.Remove();
                }
                if (SoundsToRedo.Contains(ResourceName))
                    SoundsToRedo.Remove(ResourceName);
            }
            else if (Type == "Room")
            {
                XDSRemoveLine(GetXDSLine("ROOM " + ResourceName + ","));
                string NewString = string.Empty;
                foreach (string X in StringToLines(CurrentXDS))
                {
                    if (X.StartsWith("OBJECTPLOT "))
                    {
                        if (iGet(X, 1, ",") == ResourceName)
                            continue;
                    }
                    NewString += X + Constants.vbCrLf;
                }
                CurrentXDS = NewString;
                CurrentXDS = UpdateActionsName(CurrentXDS, "Room", ResourceName, "<Unknown>", false);
                UpdateArrayActionsName("Room", ResourceName, "<Unknown>", false);
                foreach (TreeNode X in Program.Forms.main_Form.ResourcesTreeView.Nodes[(int)ResourceIDs.Room].Nodes)
                {
                    if (X.Text == ResourceName)
                        X.Remove();
                }
                if (GetXDSLine("BOOTROOM ").Substring(9) == ResourceName)
                {
                    string NewRoomName = Program.Forms.main_Form.ResourcesTreeView.Nodes[(int)ResourceIDs.Room].Nodes[0].Text;
                    XDSChangeLine("BOOTROOM " + ResourceName, "BOOTROOM " + NewRoomName);
                }
            }
            else if (Type == "Path")
            {
                XDSRemoveLine("PATH " + ResourceName + ",");
                XDSRemoveFilter("PATHPOINT " + ResourceName + ",");
                CurrentXDS = UpdateActionsName(CurrentXDS, "Path", ResourceName, "<Unknown>", false);
                UpdateArrayActionsName("Path", ResourceName, "<Unknown>", false);
                foreach (TreeNode X in Program.Forms.main_Form.ResourcesTreeView.Nodes[(int)ResourceIDs.Path].Nodes)
                {
                    if (X.Text == ResourceName)
                        X.Remove();
                }
            }
            else if (Type == "Script")
            {
                XDSRemoveLine(GetXDSLine("SCRIPT " + ResourceName + ","));
                XDSRemoveFilter("SCRIPTARG " + ResourceName + ",");
                File.Delete(SessionsLib.SessionPath + "Scripts/" + ResourceName + ".dbas");
                CurrentXDS = UpdateActionsName(CurrentXDS, "Script", ResourceName, "<Unknown>", false);
                UpdateArrayActionsName("Script", ResourceName, "<Unknown>", false);
                foreach (TreeNode X in Program.Forms.main_Form.ResourcesTreeView.Nodes[(int)ResourceIDs.Script].Nodes)
                {
                    if (X.Text == ResourceName)
                        X.Remove();
                }
            }
            foreach (Form X in Program.Forms.main_Form.MdiChildren)
            {
                if (X.Text == ResourceName)
                    X.Close();
            }
        }

        public static Bitmap EmptyBitmap(int width = 16, int height = 16)
        {
            int _width = width;
            int _height = height;
            int _size = height/16;

            Graphics bitmap = Graphics.FromImage(new Bitmap(_width, height));

            int x = 0;
            int y = _size;

            while (y <= height)
            {
                bitmap.FillRectangle(Brushes.Red, 0, x, _width, _size); 
                x += _size;

                bitmap.FillRectangle(Brushes.Black, 0, y, _width, _size);    
                y += _size;
            }
            return new Bitmap(_width, _height, bitmap);
        }

        public static void CopyResource(string OldName, string NewName, byte ResourceType)
        {
            switch (ResourceType)
            {
                case (byte)ResourceIDs.Sprite:
                    {
                        string OldLine = GetXDSLine("SPRITE " + OldName + ",");
                        OldLine = OldLine.Substring(8 + OldName.Length);
                        XDSAddLine("SPRITE " + NewName + "," + OldLine);
                        foreach (string X_ in Directory.GetFiles(SessionsLib.SessionPath + "Sprites"))
                        {
                            string X = X_;
                            X = X.Substring(X.LastIndexOf("/") + 1);
                            short FrameNumber = Convert.ToInt16(X.Substring(0, X.IndexOf("_")));
                            string SpriteName = X.Substring(X.IndexOf("_") + 1);
                            SpriteName = SpriteName.Substring(0, SpriteName.Length - 4);
                            if (SpriteName == OldName)
                                File.Copy(X, SessionsLib.SessionPath + "Sprites/" + FrameNumber.ToString() + "_" + NewName + ".png");
                        }
                        AddResourceNode(ref ResourceType, NewName, "SpriteNode", true);
                        break;
                    }
                case (byte)ResourceIDs.Background:
                    {
                        File.Copy(SessionsLib.SessionPath + "Backgrounds/" + OldName + ".png", SessionsLib.SessionPath + "Backgrounds/" + NewName + ".png");
                        XDSAddLine("BACKGROUND " + NewName);
                        AddResourceNode(ref ResourceType, NewName, "BackgroundNode", true);
                        break;
                    }
                case (byte)ResourceIDs.Script:
                    {
                        string OldLine = GetXDSLine("SCRIPT " + OldName + ",");
                        OldLine = OldLine.Substring(OldLine.LastIndexOf(",") + 1);
                        XDSAddLine("SCRIPT " + NewName + "," + OldLine);
                        File.Copy(SessionsLib.SessionPath + "Scripts/" + OldName + ".dbas", SessionsLib.SessionPath + "Scripts/" + NewName + ".dbas");
                        foreach (string X_ in GetXDSFilter("SCRIPTARG " + OldName + ","))
                        {
                            string X = X_;
                            X = X.Substring(8 + OldName.Length);
                            XDSAddLine("SCRIPTARG " + NewName + "," + X);
                        }
                        AddResourceNode(ref ResourceType, NewName, "ScriptNode", true);
                        break;
                    }
                case (byte)ResourceIDs.Sound:
                    {
                        string OldLine = GetXDSLine("SOUND " + OldName + ",");
                        byte Type = Convert.ToByte(OldLine.Substring(7 + OldName.Length));
                        if (Type == 1)
                        {
                            File.Copy(SessionsLib.SessionPath + "Sounds/" + OldName + ".mp3", SessionsLib.SessionPath + "Sounds/" + NewName + ".mp3");
                        }
                        else
                        {
                            File.Copy(SessionsLib.SessionPath + "Sounds/" + OldName + ".wav", SessionsLib.SessionPath + "Sounds/" + NewName + ".wav");
                        }

                        break;
                    }
                case (byte)ResourceIDs.Path:
                    {
                        XDSAddLine("PATH " + NewName);
                        foreach (string X_ in GetXDSFilter("PATHPOINT " + OldName + ","))
                        {
                            string X = X_;
                            X = X.Substring(11 + OldName.Length);
                            string XP = iGet(X, 0, ",");
                            string YP = iGet(X, 1, ",");
                            XDSAddLine("PATHPOINT " + NewName + "," + XP + "," + YP);
                        }
                        AddResourceNode(ref ResourceType, NewName, "PathNode", true);
                        break;
                    }
                case (byte)ResourceIDs.Room:
                    {
                        string OldLine = GetXDSLine("ROOM " + OldName + ",");
                        OldLine = OldLine.Substring(6 + OldName.Length);
                        XDSAddLine("ROOM " + NewName + "," + OldLine);
                        foreach (string X in GetXDSFilter("OBJECTPLOT "))
                        {
                            if (iGet(X, 1, ",") != OldName)
                                continue;
                            string NewLine = iGet(X, 0, ",") + "," + NewName + "," + iGet(X, 2, ",") + "," + iGet(X, 3, ",") + "," + iGet(X, 4, ",");
                            XDSAddLine(NewLine);
                        }
                        AddResourceNode(ref ResourceType, NewName, "RoomNode", true);
                        break;
                    }
                case (byte)ResourceIDs.DObject:
                    {
                        string OldLine = GetXDSLine("OBJECT " + OldName + ",");
                        OldLine = OldLine.Substring(8 + OldName.Length);
                        XDSAddLine("OBJECT " + NewName + "," + OldLine);
                        foreach (string X in GetXDSFilter("EVENT " + OldName + ","))
                            XDSAddLine("EVENT " + NewName + X.Substring(("EVENT " + OldName).Length));
                        foreach (string X in GetXDSFilter("ACT " + OldName + ","))
                            XDSAddLine("ACT " + NewName + X.Substring(("ACT " + OldName).Length));
                        AddResourceNode(ref ResourceType, NewName, "ObjectNode", true);
                        break;
                    }
            }
        }

        public static string GenerateDuplicateResourceName(string OldName)
        {
            for (byte i = 2; i <= 100; i++)
            {
                string NewName = OldName + "_" + i.ToString();
                if (IsAlreadyResource(NewName).Length == 0)
                    return NewName;
            }
            return string.Empty; // Hack
        }

        public static byte ResourceToType(string ResourceName)
        {
            if (GetXDSFilter("SPRITE " + ResourceName + ",").Length > 0)
                return (byte)ResourceIDs.Sprite;
            if (GetXDSFilter("OBJECT " + ResourceName + ",").Length > 0)
                return (byte)ResourceIDs.DObject;
            if (DoesXDSLineExist("BACKGROUND " + ResourceName))
                return (byte)ResourceIDs.Background;
            if (GetXDSFilter("SOUND " + ResourceName + ",").Length > 0)
                return (byte)ResourceIDs.Sound;
            if (GetXDSFilter("ROOM " + ResourceName + ",").Length > 0)
                return (byte)ResourceIDs.Room;
            if (DoesXDSLineExist("PATH " + ResourceName))
                return (byte)ResourceIDs.Path;
            if (GetXDSFilter("SCRIPT " + ResourceName + ",").Length > 0)
                return (byte)ResourceIDs.Script;
            return 0;
        }

        public static string[] StringToLines(string InputString)
        {
            return System.Text.RegularExpressions.Regex.Split(InputString, Constants.vbNewLine);
        }

        public static void AddResourceNode(ref byte ResourceID, string ResourceName, string NodeType, bool DoShowWindow)
        {
            var MyNewNode = new TreeNode();

            MyNewNode.Name = NodeType;

            MyNewNode.ImageIndex = ResourceID;
            MyNewNode.SelectedImageIndex = ResourceID;

            MyNewNode.Text = ResourceName;

            Program.Forms.main_Form.ResourcesTreeView.Nodes[(int)ResourceID].Nodes.Add(MyNewNode);
            Program.Forms.main_Form.ResourcesTreeView.SelectedNode = MyNewNode;

            if (DoShowWindow)
            {
                OpenResource(ResourceName, ResourceID, true);
            }
        }

        public static string GetXDSLine(string FilterString)
        {
            foreach (string XDSLine in StringToLines(CurrentXDS))
            {
                if (XDSLine.StartsWith(FilterString))
                {
                    return XDSLine;
                }
            }
            return string.Empty;
        }

        public static void XDSRemoveLine(string line)
        {
            string FinalString = string.Empty;
            foreach (string XDSLine in StringToLines(CurrentXDS))
            {
                if ((XDSLine == line) == false)
                {
                    FinalString += XDSLine + Constants.vbCrLf;
                }
            }
            CurrentXDS = FinalString;
        }

        public static void XDSRemoveFilter(string Filter)
        {
            string FinalString = string.Empty;
            foreach (string XDSLine in StringToLines(CurrentXDS))
            {
                if (XDSLine.StartsWith(Filter) == false)
                    FinalString += XDSLine + Constants.vbCrLf;
            }
            CurrentXDS = FinalString;
        }

        public static string[] GetXDSFilter(string FilterString)
        {
            var Returnable = new List<string>();
            foreach (string XDSLine in StringToLines(CurrentXDS))
            {
                if (XDSLine.StartsWith(FilterString) == false | XDSLine.Length == 0)
                    continue;

                Returnable.Add(XDSLine);
            }
            return Returnable.ToArray();
        }

        // Function RemoveDeadLines(ByVal InputString As String) As String
        // Dim Returnable As String = String.Empty
        // For Each XDSLine As String In Split(InputString, vbcrlf, -1, CompareMethod.Text)
        // If Not XDSLine.Length = 0 Then Returnable += XDSLine + vbcrlf
        // Next
        // Return Returnable
        // End Function

        public static string iGet(string InputString, byte ReturnableItem, string SeperatorChar)
        {
            try
            {
                string[] TempArray = InputString.Split(Convert.ToChar(SeperatorChar));
                return TempArray[ReturnableItem];
            }
            catch
            {
                return string.Empty;
            }
        }

        public static void ClearResourcesTreeView()
        {
            for (byte NodeNo = 0, loopTo = (byte)(Program.Forms.main_Form.ResourcesTreeView.Nodes.Count - 1); NodeNo <= loopTo; NodeNo++)
                Program.Forms.main_Form.ResourcesTreeView.Nodes[(int)NodeNo].Nodes.Clear();
        }

        public static void CleanFresh(bool WishCloseNews)
        {
            try
            {
                if (SessionsLib.SessionPath.Length > 0 & Directory.Exists(Constants.AppDirectory + "ProjectTemp/" + SessionsLib.Session))
                {
                    Directory.Delete(SessionsLib.SessionPath, true);
                }
                if (SessionsLib.CompilePath.Length > 0 & Directory.Exists(SessionsLib.CompilePath))
                {
                    Directory.Delete(SessionsLib.CompilePath, true);
                }
            }
            catch
            {
            }
            bool AvoidNewsline = SettingsLib.GetSetting("CLOSE_NEWS") == "0" ? true : false;
            foreach (Form X in Program.Forms.main_Form.MdiChildren)
            {
                if (X.Name == "Newsline" & AvoidNewsline & WishCloseNews == false)
                    continue;
                X.Close();
            }
            ClearResourcesTreeView();
        }

        public static void OpenProject(string Result)
        {
            IsNewProject = false;
            ProjectPath = Result;
            BeingUsed = true;
            CleanFresh(false);
            string DisplayResult = Result.Substring(Result.LastIndexOf("/") + 1);
            DisplayResult = DisplayResult.Substring(0, DisplayResult.LastIndexOf("."));
            DisplayResult = DisplayResult.Replace(" ", string.Empty);
            string SessionName = string.Empty;
            for (byte Looper = 0; Looper <= 10; Looper++)
            {
                SessionName = DisplayResult + SessionsLib.MakeSessionName().ToString();
                if (!Directory.Exists(Constants.AppDirectory + "ProjectTemp/" + SessionName))
                    break;
            }
            SessionsLib.FormSession(SessionName);
            BeingUsed = true;
            ProjectPath = Result;
            SessionsLib.FormSessionFS();
            File.Copy(Result, SessionsLib.SessionPath + "Project.zip");
            string MyBAT = "zip.exe x Project.zip -y" + Constants.vbCrLf + "exit";
            DSGMlib.RunBatchString(MyBAT, SessionsLib.SessionPath, true);
            // File.Delete(SessionPath + "Project.zip")
            ClearResourcesTreeView();
            Program.Forms.main_Form.Text = TitleDataWorks();
            CurrentXDS = DSGMlib.PathToString(SessionsLib.SessionPath + "XDS.xds");
            // Project Upgrade
            bool HasMidPointLine = false;
            bool HasWiFiLine = false;
            string FinalString = string.Empty;
            bool Change = false;
            foreach (string X_ in StringToLines(CurrentXDS))
            {
                string X = X_;
                if (X.StartsWith("MIDPOINT_COLLISIONS "))
                    HasMidPointLine = true;
                if (X.StartsWith("INCLUDE_WIFI_LIB "))
                    HasWiFiLine = true;
                if (X.StartsWith("SCRIPT "))
                {
                    if (!X.Contains(","))
                    {
                        X += ",1";
                        Change = true;
                    }
                }
                FinalString += X + Constants.vbCrLf;
            }
            if (Change)
                CurrentXDS = FinalString;
            if (!HasMidPointLine)
                XDSAddLine("MIDPOINT_COLLISIONS 1");
            if (!HasWiFiLine)
                XDSAddLine("INCLUDE_WIFI_LIB 0");
            foreach (string XDSLine in GetXDSFilter("GLOBAL "))
            {
                string ChangeTo = string.Empty;
                if (iGet(XDSLine, 1, ",") == "1")
                {
                    ChangeTo = "Boolean";
                }
                else if (iGet(XDSLine, 1, ",") == "0")
                {
                    ChangeTo = "Integer";
                }
                if (ChangeTo.Length > 0)
                {
                    XDSChangeLine(XDSLine, iGet(XDSLine, 0, ",") + "," + ChangeTo + "," + iGet(XDSLine, 2, ","));
                }
            }
            foreach (string XDSLine_ in GetXDSFilter(string.Empty))
            {
                string XDSLine = XDSLine_;
                if (XDSLine.StartsWith("SPRITE "))
                {
                    XDSLine = XDSLine.Substring(7);
                    string SpriteName = iGet(XDSLine, 0, ",");
                    byte argResourceID = (byte)ResourceIDs.Sprite;
                    AddResourceNode(ref argResourceID, SpriteName, "SpriteNode", false);
                }
                else if (XDSLine.StartsWith("OBJECT "))
                {
                    XDSLine = XDSLine.Substring(7);
                    string ObjectName = iGet(XDSLine, 0, ",");
                    byte argResourceID1 = (byte)ResourceIDs.DObject;
                    AddResourceNode(ref argResourceID1, ObjectName, "ObjectNode", false);
                }
                else if (XDSLine.StartsWith("BACKGROUND "))
                {
                    XDSLine = XDSLine.Substring(11);
                    string BackgroundName = iGet(XDSLine, 0, ",");
                    byte argResourceID2 = (byte)ResourceIDs.Background;
                    AddResourceNode(ref argResourceID2, BackgroundName, "BackgroundNode", false);
                }
                else if (XDSLine.StartsWith("SOUND "))
                {
                    XDSLine = XDSLine.Substring(6);
                    string SoundName = iGet(XDSLine, 0, ",");
                    byte argResourceID3 = (byte)ResourceIDs.Sound;
                    AddResourceNode(ref argResourceID3, SoundName, "SoundNode", false);
                }
                else if (XDSLine.StartsWith("ROOM "))
                {
                    XDSLine = XDSLine.Substring(5);
                    string RoomName = iGet(XDSLine, 0, ",");
                    byte argResourceID4 = (byte)ResourceIDs.Room;
                    AddResourceNode(ref argResourceID4, RoomName, "RoomNode", false);
                }
                else if (XDSLine.StartsWith("PATH "))
                {
                    XDSLine = XDSLine.Substring(5);
                    string PathName = iGet(XDSLine, 0, ",");
                    byte argResourceID5 = (byte)ResourceIDs.Path;
                    AddResourceNode(ref argResourceID5, PathName, "PathNode", false);
                }
                else if (XDSLine.StartsWith("SCRIPT "))
                {
                    XDSLine = XDSLine.Substring(7);
                    string ScriptName = XDSLine.Substring(0, XDSLine.IndexOf(","));
                    byte argResourceID6 = (byte)ResourceIDs.Script;
                    AddResourceNode(ref argResourceID6, ScriptName, "ScriptNode", false);
                }
            }
            RedoAllGraphics = true;
            RedoSprites = true;
            BGsToRedo.Clear();
            FontsUsedLastTime.Clear();
            BuildSoundsRedoFromFile();
            SettingsLib.SetSetting("LAST_PROJECT", Result);
            SettingsLib.SaveSettings();
        }

        public static void BuildSoundsRedoFromFile()
        {
            SoundsToRedo.Clear();
            foreach (string X_ in GetXDSFilter("SOUND "))
            {
                string X = X_;
                X = X.Substring(6);
                X = X.Substring(0, X.Length - 2);
                SoundsToRedo.Add(X);
            }
        }

        public static void DebugSounds()
        {
            foreach (string Y in SoundsToRedo)
                MsgError("\"" + Y + "\"");
        }

        public static void AddSoundToRedo(string SoundName2)
        {
            if (!SoundsToRedo.Contains(SoundName2))
                SoundsToRedo.Add(SoundName2);
        }

        public static void ShowInternalForm(ref object Instance)
        {
            ((dynamic)Instance).TopLevel = false;
            ((dynamic)Instance).MdiParent = Program.Forms.main_Form;
            ((dynamic)Instance).Show();
        }

        public static void CleanInternalXDS()
        {
            string ToWrite = string.Empty;
            foreach (string X in StringToLines(CurrentXDS))
            {
                if (X.Length > 0)
                    ToWrite += X + Constants.vbCrLf;
            }
            CurrentXDS = ToWrite;
        }

        public static void URL(string URL)
        {
            Process.Start(URL);
        }

        public static string GetActionTypes(string ActionName)
        {
            string Returnable = string.Empty;
            foreach (string X_ in File.ReadAllLines(Constants.AppDirectory + "Actions/" + ActionName.Split('\\')[1] + ".action"))
            {
                string X = X_;
                if (!X.StartsWith("ARG "))
                    continue;
                X = X.Substring(4);
                string R = ScriptsLib.ArgumentTypeToString(Convert.ToByte(iGet(X, 1, ",")));
                Returnable += R + ",";
            }
            if (Returnable.Length > 0)
                Returnable = Returnable.Substring(0, Returnable.Length - 1);
            return Returnable;
        }

        public static string IsAlreadyResource(string TheName)
        {
            // Dim Returnable As String = String.Empty
            if (GetXDSFilter("OBJECT " + TheName + ",").Length > 0)
                return "an Object";
            if (GetXDSFilter("SPRITE " + TheName + ",").Length > 0)
                return "a Sprite";
            if (DoesXDSLineExist("BACKGROUND " + TheName))
                return "a Background";
            if (GetXDSFilter("SOUND " + TheName + ",").Length > 0)
                return "a Sound";
            if (GetXDSFilter("ROOM " + TheName + ",").Length > 0)
                return "a Room";
            if (DoesXDSLineExist("PATH " + TheName))
                return "a Path";
            if (GetXDSFilter("SCRIPT " + TheName + ",").Length > 0)
                return "a Script";
            return string.Empty;
        }

        public static bool GUIResNameChecker(string TheName)
        {
            string Reply = IsAlreadyResource(TheName);
            if (Reply.Length > 0)
            {
                MsgError("There is already " + Reply + " called '" + TheName + "'." + Constants.vbCrLf + Constants.vbCrLf + "You must choose another name.");
                return true;
            }
            else
            {
                if (!ValidateSomething(TheName, (byte)ValidateLevel.Tight))
                {
                    MsgError("The name may not contain a space or other unusual character.");
                    return true;
                }
                if (!ValidateSomething(TheName, (byte)ValidateLevel.NumberStart))
                {
                    MsgError("The name may not start with a number.");
                    return true;
                }
            }
            return false;
        }

        public static Bitmap ObjectGetImage(string ObjectName)
        {
            string XDSLine = GetXDSLine("OBJECT " + ObjectName + ",");
            // MsgError(XDSLine)
            string SpriteName = iGet(XDSLine, 1, ",");
            if (SpriteName == "None")
                return (Bitmap)MakeBMPTransparent(PathToImage(Constants.AppDirectory + "Resources/NoSprite.png"), Color.Magenta);
            short FrameNo = Convert.ToInt16(iGet(XDSLine, 2, ","));
            // MsgError(iget(XDSLine, 2, ","))
            return (Bitmap)MakeBMPTransparent(DSGMlib.PathToImage(SessionsLib.SessionPath + "Sprites/" + FrameNo.ToString() + "_" + SpriteName + ".png"), Color.Magenta);
        }

        public static void RunBatchString(string BatchString, string WorkingDirectory, bool is7Zip)
        {
            if (is7Zip)
                File.Copy(Constants.AppDirectory + "zip.exe", WorkingDirectory + "zip.exe", true);
            File.WriteAllText(WorkingDirectory + "/DSGMBatch.bat", BatchString);
            // MsgError(WorkingDirectory + "DSGMBatch.bat")
            var MyProcess = new Process();
            var MyStartInfo = new ProcessStartInfo(WorkingDirectory + "/DSGMBatch.bat");
            MyStartInfo.WorkingDirectory = WorkingDirectory;
            MyStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            MyProcess.StartInfo = MyStartInfo;
            MyProcess.Start();
            MyProcess.WaitForExit();
            MyProcess.Dispose();
            File.Delete(WorkingDirectory + "/DSGMBatch.bat");
            try
            {
                if (is7Zip)
                    File.Delete(WorkingDirectory + "zip.exe");
            }
            catch
            {
                // Nevermind then eigh
            }
        }

        public static string TitleDataWorks()
        {
            string Returnable = string.Empty;
            if (BeingUsed)
            {
                if (IsNewProject)
                {
                    Returnable = "<New Project>";
                }
                else
                {
                    Returnable = ProjectPath.Substring(ProjectPath.LastIndexOf("/") + 1);
                    Returnable = Returnable.Substring(0, Returnable.LastIndexOf("."));
                }
                CacheProjectName = Returnable;
                Returnable += " - ";
            }
            Returnable += Application.ProductName;
            return Returnable;
        }

        public static string SaveFile(string Directory, string Filter, string DefaultFileName)
        {
            if (Directory.Length == 0)
                Directory = SaveDefaultFolder;
            string Returnable;
            var SFD = new SaveFileDialog();
            SFD.InitialDirectory = Directory;
            SFD.FileName = DefaultFileName;
            SFD.Filter = Filter;
            SFD.Title = "Save File";
            if (!(SFD.ShowDialog() == DialogResult.OK))
                Returnable = string.Empty;
            else
                Returnable = SFD.FileName;
            SFD.Dispose();
            SaveDefaultFolder = Directory;
            return Returnable;
        }

        public static bool DoesXDSLineExist(string XDSLine)
        {
            foreach (string X in StringToLines(CurrentXDS))
            {
                if (X == XDSLine)
                    return true;
            }
            return false;
        }

        public static byte GetOSVersion()
        {
            return Convert.ToByte(Environment.OSVersion.Version.ToString().Substring(0, 1));
        }

    }
}