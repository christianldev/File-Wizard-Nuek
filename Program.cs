using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Wizard
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicaci�n.
        /// </summary>
        [STAThread]
        static void Main()
        {
            WinFormsBootstrapper.Initialize();
            Application.Run(new MainMenuForm());
        }
    }
}
