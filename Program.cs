namespace DS_Game_Maker
{
    public static class Program
    {
        public static GlobalFormsClass Forms;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            Forms = new GlobalFormsClass();
            Forms.Initialize(args);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(Forms.main_Form);
        }
    }
}