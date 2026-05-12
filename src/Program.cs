using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using File_Wizard.Infrastructure.Bootstrap;
using File_Wizard.UI.Auth;
using File_Wizard.UI.Forms;

namespace File_Wizard
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            WinFormsBootstrapper.Initialize();

            using (var loginForm = new SftpLoginForm())
            {
                if (loginForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                Application.Run(new MainMenuForm(loginForm.ConnectionSettings));
            }
        }
    }
}

