using System.Diagnostics;
using System.IO;

namespace DS_Game_Maker
{

    static class Toolchain
    {

        public static bool IsInstallerRunning()
        {
            byte EXECount = 0;
            Process[] ProcessesList = Process.GetProcesses();
            foreach (Process TheProcess in ProcessesList)
            {
                if (TheProcess.ProcessName.StartsWith("devkitProUpdater"))
                    EXECount = (byte)(EXECount + 1);
            }
            if (EXECount > 0)
                return true;
            else
                return false;
        }

        public static void MessageThing()
        {
            DS_Game_Maker.DSGMlib.MsgInfo("Please wait patiently while the installer is running. Press OK to continue.");
            if (IsInstallerRunning())
            {
                MessageThing();
            }
            else
            {
                ReinstallPAlib();
            }
        }

        public static void RundevkitProUpdater()
        {
            DS_Game_Maker.DSGMlib.MsgInfo("When presented with the option to choose components, select only 'devkitARM'.");
            Process.Start(DS_Game_Maker.DSGMlib.AppPath + "devkitProUpdater-1.5.0.exe");
            System.Threading.Thread.Sleep(1000);
            MessageThing();
        }

        public static void ReinstallPAlib()
        {
            File.Copy(DS_Game_Maker.DSGMlib.AppPath + "devkitPro32.zip", DS_Game_Maker.DSGMlib.CDrive + "devkitPro32.zip");
            File.Copy(DS_Game_Maker.DSGMlib.AppPath + "zip.exe", DS_Game_Maker.DSGMlib.CDrive + "zip.exe");
            string BatchText = "zip.exe x devkitPro32.zip";
            var BatchProcess = new Process();
            File.WriteAllText(DS_Game_Maker.DSGMlib.CDrive + "Generate.bat", BatchText);
            var BatchProcessInfo = new ProcessStartInfo(DS_Game_Maker.DSGMlib.CDrive + "Generate.bat");
            BatchProcessInfo.WorkingDirectory = DS_Game_Maker.DSGMlib.CDrive;
            BatchProcessInfo.WindowStyle = ProcessWindowStyle.Normal;
            BatchProcess.StartInfo = BatchProcessInfo;
            BatchProcess.Start();
            BatchProcess.WaitForExit();
            File.Delete(DS_Game_Maker.DSGMlib.CDrive + "Generate.bat");
            File.Delete(DS_Game_Maker.DSGMlib.CDrive + "devkitPro32.zip");
            File.Delete(DS_Game_Maker.DSGMlib.CDrive + "zip.exe");
            DS_Game_Maker.My.MyProject.Computer.FileSystem.MoveDirectory(DS_Game_Maker.DSGMlib.CDrive + "devkitPro", Path.GetTempPath() + "Toolchain");
            DS_Game_Maker.My.MyProject.Computer.FileSystem.RenameDirectory(DS_Game_Maker.DSGMlib.CDrive + "devkitPro32", "devkitPro");
            DS_Game_Maker.DSGMlib.MsgInfo("Thank you for installing devkitARM and PAlib!");
        }

        // Sub SetSystemVariable(ByVal name As String, ByVal value As String, ByVal UseElevatedPermissions As Boolean)
        // If UseElevatedPermissions Then
        // Environment.SetEnvironmentVariable(name, value, EnvironmentVariableTarget.Machine)
        // Else
        // Environment.SetEnvironmentVariable(name, value, EnvironmentVariableTarget.User)
        // End If
        // End Sub

    }
}