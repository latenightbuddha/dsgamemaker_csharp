using Microsoft.Win32;

namespace DS_Game_Maker
{

    static class RegistryLib
    {

        public static void SetFileType(string @extension, string FileType)
        {
            var ext = Registry.ClassesRoot.CreateSubKey(@extension, RegistryKeyPermissionCheck.ReadWriteSubTree);
            ext.SetValue("", FileType);
        }

        public static void SetFileDescription(string FileType, string Description)
        {
            var ext = Registry.ClassesRoot.CreateSubKey(FileType, RegistryKeyPermissionCheck.ReadWriteSubTree);
            ext.SetValue("", Description);
        }

        public static void SetDefaultIcon(string FileType, string Icon)
        {
            var ext = Registry.ClassesRoot.OpenSubKey(FileType, true);
            ext.SetValue("DefaultIcon", Icon);
        }

        public static void AddAction(string FileType, string Verb, string ActionDescription)
        {
            var ext = Registry.ClassesRoot.OpenSubKey(FileType, true).CreateSubKey("Shell").CreateSubKey(Verb);
            ext.SetValue("", ActionDescription);
        }

        public static void SetExtensionCommandLine(string Command, string FileType, string CommandLine, string Name = "")
        {
            var rk = Registry.ClassesRoot;
            var ext = rk.OpenSubKey(FileType, true).OpenSubKey("Shell", true).OpenSubKey(Command, true).CreateSubKey("Command", RegistryKeyPermissionCheck.ReadWriteSubTree);
            ext.SetValue(Name, CommandLine);
        }

    }
}