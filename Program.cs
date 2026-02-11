namespace DS_Game_Maker
{
    public static class Program
    {
        public static MainForm mainForm;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
<<<<<<< Updated upstream
        static void Main()
        {
            mainForm = new MainForm();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(mainForm);
=======
        public static void Main(string[] args)
        {   
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            Forms = new GlobalFormsClass();
            Forms.Initialize(args);


            Application.Run(Forms.main_Form);
>>>>>>> Stashed changes
        }
    }
}