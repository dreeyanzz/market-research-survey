namespace market_research_survey
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                ApplicationConfiguration.Initialize();
                Application.Run(new Window());
            }
            catch (Exception ex)
            {
                Console.WriteLine("CRASH DETECTED:");
                Console.WriteLine(ex.ToString());
                Console.ReadLine(); // Keeps the terminal open so you can read it
            }
        }
    }
}
