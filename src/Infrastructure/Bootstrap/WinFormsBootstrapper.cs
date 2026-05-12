using System.Windows.Forms;

namespace File_Wizard.Infrastructure.Bootstrap
{
    public static class WinFormsBootstrapper
    {
        public static void Initialize()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }
    }
}

