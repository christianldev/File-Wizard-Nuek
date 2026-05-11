using System.Windows.Forms;

namespace File_Wizard
{
    internal static class WinFormsBootstrapper
    {
        public static void Initialize()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }
    }
}