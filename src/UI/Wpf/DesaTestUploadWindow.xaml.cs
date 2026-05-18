using File_Wizard.Infrastructure;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Versioning;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Ookii.Dialogs.Wpf;

namespace File_Wizard.UI.Wpf
{
    [SupportedOSPlatform("windows")]
    public partial class DesaTestUploadWindow : Window
    {
        private readonly SftpConnectionSettings connectionSettings;
        private readonly DispatcherTimer connectionTimer = new DispatcherTimer();

        private SftpClient? client;
        private bool subidaCorrecta;
        private string rutaLocal = @"C:";
        private string directorio = "/sat/cdp/desa/cpy";

        public DesaTestUploadWindow(SftpConnectionSettings connectionSettings)
        {
            this.connectionSettings = connectionSettings ?? throw new ArgumentNullException(nameof(connectionSettings));
            InitializeComponent();

            ConnectToEnvironment();

            connectionTimer.Interval = TimeSpan.FromSeconds(5);
            connectionTimer.Tick += Timer_Tick;
        }

        private void Window_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            connectionTimer.Stop();

            if (client != null && client.IsConnected)
            {
                client.Disconnect();
            }
        }

        private string[]? archivosSeleccionadosParaSubir;

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new VistaOpenFileDialog
            {
                Title = "Selecciona el/los archivo(s) que deseas subir...",
                Filter = "Todos los archivos (*.*)|*.*",
                Multiselect = true // Permitir multiselect
            };

            if (Directory.Exists(rutaLocal))
            {
                openDialog.InitialDirectory = rutaLocal;
            }

            if (openDialog.ShowDialog(this) == true)
            {
                archivosSeleccionadosParaSubir = openDialog.FileNames;

                // Mostrar solo el directorio
                LocalDirectoryTextBox.Text = Path.GetDirectoryName(openDialog.FileName);
                rutaLocal = LocalDirectoryTextBox.Text ?? "C:";

                if (archivosSeleccionadosParaSubir.Length == 1)
                {
                    ManualPathTextBox.Text = Path.GetFileName(openDialog.FileName);
                }
                else
                {
                    ManualPathTextBox.Text = $"<{archivosSeleccionadosParaSubir.Length} archivos seleccionados>";
                }
            }
        }

        private void ConnectToEnvironment()
        {
            try
            {
                client = new SftpClient(connectionSettings.Host, connectionSettings.Port, connectionSettings.Username, connectionSettings.Password);
                client.ConnectionInfo.Timeout = TimeSpan.FromSeconds(8);
                client.Connect();

                if (client.IsConnected)
                {
                    connectionTimer.Start();
                    ConnectionStatusText.Text = $"CONECTADO ({connectionSettings.EnvironmentName})";
                    ConnectionStatusText.Background = System.Windows.Media.Brushes.LimeGreen;
                    UploadFileButton.IsEnabled = true;
                    UploadButton.IsEnabled = true;
                    SetConnectedState();
                }
                else
                {
                    MessageBox.Show("CONEXION fallida");
                    SetDisconnectedState();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error en la conexion");
                SetDisconnectedState();
            }
        }

        private void UploadFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (client == null || !client.IsConnected)
            {
                MessageBox.Show("NO HAY CONEXION CON EL SERVIDOR");
                return;
            }

            if (!DesaRadioButton.IsChecked.GetValueOrDefault() && !TestRadioButton.IsChecked.GetValueOrDefault())
            {
                MessageBox.Show("Por favor, elegir un ambiente: DESA o TEST");
                return;
            }

            if (string.IsNullOrWhiteSpace(LocalDirectoryTextBox.Text))
            {
                MessageBox.Show("Selecciona un directorio :)");
                return;
            }

            // Subida múltiple (por Botón Browse)
            if (archivosSeleccionadosParaSubir != null && archivosSeleccionadosParaSubir.Length > 0 && 
                ManualPathTextBox.Text.StartsWith("<") && ManualPathTextBox.Text.EndsWith("archivos seleccionados>"))
            {
                int correctos = 0;
                foreach (string ruta in archivosSeleccionadosParaSubir)
                {
                    if (File.Exists(ruta))
                    {
                        SubirArchivo(ruta, mostrarMsg: false);
                        if (subidaCorrecta) correctos++;
                    }
                }
                MessageBox.Show($"Proceso finalizado. Subidos correctamente: {correctos} de {archivosSeleccionadosParaSubir.Length}.");
                return;
            }

            // Subida individual o manual
            if (string.IsNullOrWhiteSpace(ManualPathTextBox.Text))
            {
                MessageBox.Show("No se han escrito los elementos para descargar");
                return;
            }

            string archivo = ManualPathTextBox.Text.Trim();
            if (archivo.Contains("/") || archivo.Contains("?"))
            {
                MessageBox.Show("El nombre del archivo a subir tiene caracteres no permitidos: * ?");
                return;
            }

            string rutaCompleta = Path.Combine(LocalDirectoryTextBox.Text.Trim(), archivo);
            if (File.Exists(rutaCompleta))
            {
                SubirArchivo(rutaCompleta, mostrarMsg: true);
            }
            else
            {
                MessageBox.Show("El archivo local ingresado no existe :(");
            }
        }

        private void SubirArchivo(string rutaLocalCompleta, bool mostrarMsg = true)
        {
            string archivo = Path.GetFileName(rutaLocalCompleta);
            string extension = Path.GetExtension(archivo);
            bool hubodirectorio = true;

            if (DesaRadioButton.IsChecked == true)
            {
                switch (extension)
                {
                    case ".sh": directorio = "/sat/cdp/desa/cad"; break;
                    case ".cbl":
                    case ".pco": directorio = "/sat/cdp/desa/src"; break;
                    case ".scl": directorio = "/sat/cdp/desa/cad/scl"; break;
                    case ".fact": directorio = "/sat/cdp/desa/adm/fact"; break;
                    case ".sql": directorio = "/sat/cdp/desa/adm/sql"; break;
                    case "": directorio = "/sat/cdp/desa/cpy"; break;
                    default: hubodirectorio = false; break;
                }
            }
            else
            {
                switch (extension)
                {
                    case ".sh": directorio = "/sat/cdp/test/cad"; break;
                    case ".cbl":
                    case ".pco": directorio = "/sat/cdp/test/src"; break;
                    case ".scl": directorio = "/sat/cdp/test/cad/scl"; break;
                    case ".fact": directorio = "/sat/cdp/test/adm/fact"; break;
                    case ".sql": directorio = "/sat/cdp/test/adm/sql"; break;
                    case "": directorio = "/sat/cdp/test/cpy"; break;
                    default: hubodirectorio = false; break;
                }
            }

            if (!hubodirectorio)
            {
                MessageBox.Show("Error - No se encontró directorio válido para el archivo: " + archivo);
                return;
            }

            try
            {
                string rutaCompletaRemota = directorio + "/" + archivo;
                if (client!.Exists(rutaCompletaRemota))
                {
                    if (MessageBox.Show("¿Estás seguro de sobreescribir este archivo?", "CONFIRMACION", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    {
                        MessageBox.Show("SUBIDA CANCELADA POR EL USUARIO");
                        subidaCorrecta = false;
                        return;
                    }

                    CrearRespaldoRemoto(rutaCompletaRemota);
                }

                using FileStream fs = new FileStream(rutaLocalCompleta, FileMode.Open);
                client.UploadFile(fs, rutaCompletaRemota);

                // Cambiar los permisos a todos en Linux del archivo que acabamos de crear/sobreescribir. (chmod 777)
                try
                {
                    client.ChangePermissions(rutaCompletaRemota, 0777); // 777 as in read/write/execute for all
                }
                catch (Exception chmodEx)
                {
                    // Atrapamos unicamente si el cambio de permisos falla. Quizás el usuario no tiene de privilegios para hacer "chmod".
                    if (mostrarMsg) MessageBox.Show("El archivo se subió, pero hubo un problema otorgando todos los permisos (chmod 777):\n" + chmodEx.Message);
                }

                subidaCorrecta = true;
                if (mostrarMsg) MessageBox.Show("El archivo se subió correctamente y se han dado todos los permisos (Chmod 777) :)");
            }
            catch
            {
                subidaCorrecta = false;
                if (mostrarMsg) MessageBox.Show("ERROR AL SUBIR EL ARCHIVO: " + archivo);
            }
        }

        private void CrearRespaldoRemoto(string rutaCompletaRemota)
        {
            string directorioRemoto = Path.GetDirectoryName(rutaCompletaRemota)?.Replace('\\', '/') ?? string.Empty;
            string archivo = Path.GetFileName(rutaCompletaRemota);

            string respaldoBase = $"{rutaCompletaRemota}.{DateTime.Now:yyyy.MM.dd}";
            string respaldoFinal = respaldoBase;

            if (client == null || !client.IsConnected)
            {
                throw new InvalidOperationException("NO HAY CONEXION CON EL SERVIDOR");
            }

            if (client.Exists(respaldoFinal))
            {
                respaldoFinal = $"{rutaCompletaRemota}.{DateTime.Now:yyyy.MM.dd.HH.mm.ss}";
            }

            int intento = 0;
            while (client.Exists(respaldoFinal))
            {
                intento++;
                respaldoFinal = $"{rutaCompletaRemota}.{DateTime.Now:yyyy.MM.dd.HH.mm.ss}.{intento}";
            }

            client.RenameFile(rutaCompletaRemota, respaldoFinal);
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (client == null || !client.IsConnected)
            {
                connectionTimer.Stop();
                ConnectionStatusText.Text = "DESCONECTADO";
                ConnectionStatusText.Background = System.Windows.Media.Brushes.Tomato;
                SetDisconnectedState();
                MessageBox.Show("SE PERDIO LA CONEXION CON EL SERVIDOR");
            }
        }

        private void SetConnectedState()
        {
            UploadFileButton.IsEnabled = true;
            UploadButton.IsEnabled = true;
            BrowseButton.IsEnabled = true;
        }

        private void SetDisconnectedState()
        {
            ConnectionStatusText.Text = "DESCONECTADO";
            ConnectionStatusText.Background = System.Windows.Media.Brushes.Tomato;
            UploadFileButton.IsEnabled = false;
            UploadButton.IsEnabled = false;
            BrowseButton.IsEnabled = true;
        }
    }
}