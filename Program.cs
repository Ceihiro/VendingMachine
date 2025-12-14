// Starts the whole application

namespace VendingMachine
{
    internal static class Program
    {
        // Program starts
        [STAThread]
        static void Main()
        {
            // Prepare the app settings (for Windows Forms)
            ApplicationConfiguration.Initialize();

            // Open the main window of the app (MainForm)
            Application.Run(new MainForm());
        }
    }
}
