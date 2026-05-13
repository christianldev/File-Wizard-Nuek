using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Versioning;
using System.Windows;
using File_Wizard.UI.Wpf;

namespace File_Wizard
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [SupportedOSPlatform("windows")]
        [STAThread]
        static void Main()
        {
            var loginWindow = new SftpLoginWindow();
            if (loginWindow.ShowDialog() != true)
            {
                return;
            }

            var application = new System.Windows.Application();
            application.Run(new MainMenuWindow(loginWindow.ConnectionSettings!));
        }
    }
}

