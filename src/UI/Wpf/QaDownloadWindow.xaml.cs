using File_Wizard.Infrastructure;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Ookii.Dialogs.Wpf;

namespace File_Wizard.UI.Wpf
{
    [SupportedOSPlatform("windows")]
    public partial class QaDownloadWindow : Window
    {
        private readonly SftpConnectionSettings connectionSettings;
        private readonly Dictionary<string, List<ISftpFile>> cacheDirectorios = new Dictionary<string, List<ISftpFile>>();
        private readonly List<string> commandHistory = new List<string>();
        private readonly DispatcherTimer connectionTimer = new DispatcherTimer();

        private SftpClient? client;
        private bool cancelarDescarga;
        private bool descargaCorrecta;
        private string rutaLocal = @"C:";
        private int historyIndex = -1;
        private string directorio = "/satqanexc/cpy";

        public QaDownloadWindow(SftpConnectionSettings connectionSettings)
        {
            this.connectionSettings = connectionSettings ?? throw new ArgumentNullException(nameof(connectionSettings));
            InitializeComponent();

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

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new VistaFolderBrowserDialog
            {
                SelectedPath = rutaLocal,
                Description = "Selecciona un directorio para guardar los componentes...",
                UseDescriptionForTitle = true,
                ShowNewFolderButton = true
            };

            if (openDialog.ShowDialog(this) == true)
            {
                LocalDirectoryTextBox.Text = openDialog.SelectedPath;
                rutaLocal = openDialog.SelectedPath;
            }
            else
            {
                MessageBox.Show("Error al seleccionar el directorio, intente de nuevo...");
            }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                client = new SftpClient(connectionSettings.Host, connectionSettings.Port, connectionSettings.Username, connectionSettings.Password);
                client.ConnectionInfo.Timeout = TimeSpan.FromSeconds(8);
                client.Connect();

                if (client.IsConnected)
                {
                    connectionTimer.Start();
                    ConnectionStatusText.Text = "CONECTADO";
                    ConnectionStatusText.Background = System.Windows.Media.Brushes.LimeGreen;
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

        private void DownloadListButton_Click(object sender, RoutedEventArgs e)
        {
            if (client == null || !client.IsConnected)
            {
                MessageBox.Show("NO HAY CONEXION CON EL SERVIDOR");
                return;
            }

            if (string.IsNullOrWhiteSpace(FilesTextBox.Text))
            {
                MessageBox.Show("No se han escrito los elementos para descargar");
                return;
            }

            if (string.IsNullOrWhiteSpace(LocalDirectoryTextBox.Text))
            {
                MessageBox.Show("Selecciona un directorio :)");
                return;
            }

            if (OtherEnvironmentRadioButton.IsChecked == true && string.IsNullOrWhiteSpace(OtherEnvironmentTextBox.Text))
            {
                MessageBox.Show("Escribir por favor la ruta remota :)");
                return;
            }

            cancelarDescarga = false;
            SetBusyState();

            string filesText = FilesTextBox.Text;
            string localDirectory = LocalDirectoryTextBox.Text.Trim();
            bool environment1 = Environment1RadioButton.IsChecked == true;
            bool environment2 = Environment2RadioButton.IsChecked == true;
            bool environment3 = Environment3RadioButton.IsChecked == true;
            bool environment4 = OtherEnvironmentRadioButton.IsChecked == true;
            bool usePrefix = true;
            if (usePrefix)
            {
                usePrefix = true;
            }
            string otherEnvironmentValue = OtherEnvironmentTextBox.Text.Trim();

            Thread downloadThread = new Thread(() =>
            {
                DescargarArchivos(filesText, localDirectory, environment1, environment2, environment3, environment4, usePrefix, otherEnvironmentValue);
                Dispatcher.Invoke(SetReadyState);
            });
            downloadThread.Start();
        }

        private List<ISftpFile> ObtenerArchivosDelDirectorio(SftpClient sftpClient, string remoteDirectory)
        {
            if (cacheDirectorios.ContainsKey(remoteDirectory))
            {
                return cacheDirectorios[remoteDirectory];
            }

            var archivos = sftpClient.ListDirectory(remoteDirectory).Where(f => f.IsRegularFile).ToList();
            cacheDirectorios.Add(remoteDirectory, archivos);
            return archivos;
        }

        private void DescargarArchivos(string filesText, string localDirectory, bool environment1, bool environment2, bool environment3, bool environment4, bool usePrefix, string otherEnvironmentValue)
        {
            int numlineas = 0;
            int numlineasOK = 0;

            foreach (string linea in filesText.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            {
                if (string.IsNullOrWhiteSpace(linea))
                {
                    continue;
                }

                numlineas++;
                string extension = Path.GetExtension(linea.Trim());
                string localFilePath;
                string prefijoPredict = string.Empty;
                string prefijo = string.Empty;
                bool hubodirectorio = true;

                if (environment1)
                {
                    prefijoPredict = "QA-";
                    switch (extension)
                    {
                        case ".sh": directorio = "/satqanexc/cad"; break;
                        case ".cbl":
                        case ".pco": directorio = "/satqanexc/src"; break;
                        case ".scl": directorio = "/satqanexc/cad/scl"; break;
                        case ".fact": directorio = "/satqanexc/adm/fact"; break;
                        case ".sql": directorio = "/satqanexc/adm/sql"; break;
                        case "": directorio = "/satqanexc/cpy"; break;
                        default: hubodirectorio = false; break;
                    }
                }
                if (environment2)
                {
                    prefijoPredict = "QA-COMUNIT-";
                    switch (extension)
                    {
                        case ".sh": directorio = "/satqanexus/cad"; break;
                        case ".cbl":
                        case ".pco": directorio = "/satqanexus/src"; break;
                        case ".scl": directorio = "/satqanexus/cad/scl"; break;
                        case ".fact": directorio = "/satqanexus/adm/fact"; break;
                        case ".sql": directorio = "/satqanexus/adm/sql"; break;
                        case "": directorio = "/satqanexus/cpy"; break;
                        default: hubodirectorio = false; break;
                    }
                }
                if (environment3)
                {
                    prefijoPredict = "QA-BCHILE-";
                    switch (extension)
                    {
                        case ".sh": directorio = "/satbcnxqa/cad"; break;
                        case ".cbl":
                        case ".pco": directorio = "/satbcnxqa/src"; break;
                        case ".scl": directorio = "/satbcnxqa/cad/scl"; break;
                        case ".fact": directorio = "/satbcnxqa/adm/fact"; break;
                        case ".sql": directorio = "/satbcnxqa/adm/sql"; break;
                        case "": directorio = "/satbcnxqa/cpy"; break;
                        default: hubodirectorio = false; break;
                    }
                }
                if (environment4)
                {
                    prefijoPredict = "QAX-";
                    switch (extension)
                    {
                        case ".sh": directorio = "/" + otherEnvironmentValue + "/cad"; break;
                        case ".cbl":
                        case ".pco": directorio = "/" + otherEnvironmentValue + "/src"; break;
                        case ".scl": directorio = "/" + otherEnvironmentValue + "/cad/scl"; break;
                        case ".fact": directorio = "/" + otherEnvironmentValue + "/adm/fact"; break;
                        case ".sql": directorio = "/" + otherEnvironmentValue + "/adm/sql"; break;
                        case "": directorio = "/" + otherEnvironmentValue + "/cpy"; break;
                        default: hubodirectorio = false; break;
                    }
                }

                prefijo = usePrefix ? prefijoPredict : string.Empty;

                if (!hubodirectorio)
                {
                    MessageBox.Show("Error - No se encontró directorio válido para el archivo: " + linea);
                    continue;
                }

                if (linea.Contains("*") || linea.Contains("?"))
                {
                    DescargarPorPatron(linea.Trim(), localDirectory, prefijo, ref numlineasOK);
                    continue;
                }

                string archivoSimple = linea.Trim();
                string rutaRemotaCompleta = directorio + "/" + archivoSimple;

                try
                {
                    if (!client!.Exists(rutaRemotaCompleta))
                    {
                        MessageBox.Show("No existe el archivo: " + archivoSimple);
                        continue;
                    }

                    var atributos = client.GetAttributes(rutaRemotaCompleta);
                    localFilePath = Path.Combine(localDirectory, prefijo + archivoSimple);
                    Dispatcher.Invoke(() =>
                    {
                        DownloadProgressBar.Maximum = Math.Max(1, (long)atributos.Size);
                        DownloadProgressBar.Value = 0;
                        DownloadProgressBar.Visibility = Visibility.Visible;
                        CurrentItemTextBlock.Text = archivoSimple;
                    });

                    using (Stream filestream = File.Create(localFilePath))
                    {
                        ulong totalBytesDownloaded = 0;
                        client.DownloadFile(rutaRemotaCompleta, filestream, bytesRead =>
                        {
                            if (cancelarDescarga)
                            {
                                return;
                            }

                            totalBytesDownloaded += bytesRead;
                            Dispatcher.Invoke(() =>
                            {
                                DownloadProgressBar.Value = Math.Min((double)totalBytesDownloaded, DownloadProgressBar.Maximum);
                            });
                        });
                    }

                    Dispatcher.Invoke(() =>
                    {
                        DownloadProgressBar.Visibility = Visibility.Collapsed;
                        CurrentItemTextBlock.Text = string.Empty;
                    });
                    numlineasOK++;
                }
                catch
                {
                    MessageBox.Show("Error al descargar el archivo: " + linea);
                }
            }

            Dispatcher.Invoke(() =>
            {
                if (numlineasOK == numlineas)
                {
                    MessageBox.Show(numlineas == 1 ? "Archivo descargado correctmente" : "Todos los archivos fueron descargados correctamente");
                    ResultTextBlock.Text = numlineas == 1 ? "Archivo descargado correctmente" : "Todos los archivos fueron descargados correctamente";
                }
                else if (numlineas > 0)
                {
                    MessageBox.Show("Hubo error en la descarga de 1 o más componentes");
                    ResultTextBlock.Text = "Hubo error en la descarga de 1 o más componentes";
                }
            });
        }

        private void DescargarPorPatron(string patron, string localDirectory, string prefijo, ref int numlineasOK)
        {
            if (patron == "*" || patron == "?")
            {
                MessageBox.Show("Nombre incorrecto del archivo a descargar: * ?");
                return;
            }

            int numarchivosmatch = 0;
            string regexPattern = "^" + Regex.Escape(patron).Replace("\\*", ".*").Replace("\\?", ".") + "$";

            try
            {
                List<ISftpFile> archivos = ObtenerArchivosDelDirectorio(client!, directorio);
                foreach (ISftpFile archivo in archivos)
                {
                    if (!Regex.IsMatch(archivo.Name, regexPattern))
                    {
                        continue;
                    }

                    numarchivosmatch++;
                    if (cancelarDescarga)
                    {
                        MessageBox.Show("Descarga cancelada.");
                        return;
                    }

                    string localFilePath = Path.Combine(localDirectory, prefijo + archivo.Name);
                    Dispatcher.Invoke(() =>
                    {
                        DownloadProgressBar.Maximum = Math.Max(1, (long)archivo.Length);
                        DownloadProgressBar.Value = 0;
                        DownloadProgressBar.Visibility = Visibility.Visible;
                        CurrentItemTextBlock.Text = archivo.Name;
                    });

                    using (Stream filestream = File.Create(localFilePath))
                    {
                        ulong totalBytesDownloaded = 0;
                        client!.DownloadFile(directorio + "/" + archivo.Name, filestream, bytesRead =>
                        {
                            if (cancelarDescarga)
                            {
                                return;
                            }

                            totalBytesDownloaded += bytesRead;
                            Dispatcher.Invoke(() =>
                            {
                                DownloadProgressBar.Value = Math.Min((double)totalBytesDownloaded, DownloadProgressBar.Maximum);
                            });
                        });
                    }

                    Dispatcher.Invoke(() =>
                    {
                        DownloadProgressBar.Visibility = Visibility.Collapsed;
                        CurrentItemTextBlock.Text = string.Empty;
                    });
                }

                if (numarchivosmatch == 0)
                {
                    MessageBox.Show("No se encontró el archivo: " + patron);
                }
                else
                {
                    numlineasOK++;
                }
            }
            catch
            {
                MessageBox.Show("Error al descargar el archivo: " + patron);
            }
        }

        private void ManualDownloadButton_Click(object sender, RoutedEventArgs e)
        {
            if (client == null || !client.IsConnected)
            {
                MessageBox.Show("NO HAY CONEXION CON EL SERVIDOR");
                return;
            }

            if (string.IsNullOrWhiteSpace(ManualPathTextBox.Text))
            {
                MessageBox.Show("No se han escrito los elementos para descargar");
                return;
            }

            if (string.IsNullOrWhiteSpace(LocalDirectoryTextBox.Text))
            {
                MessageBox.Show("Selecciona un directorio :)");
                return;
            }

            string rutaCompletaRemota = ManualPathTextBox.Text.Trim();
            string rutaCompletaLocal = LocalDirectoryTextBox.Text.Trim();

            if (!rutaCompletaRemota.Contains("/") || !rutaCompletaRemota.StartsWith("/") || rutaCompletaRemota.EndsWith("/")
                || rutaCompletaRemota.Contains("*") || rutaCompletaRemota.Contains("?"))
            {
                MessageBox.Show("La ruta del fichero a descargar no es correcta");
                return;
            }

            DescargaManual(rutaCompletaRemota, rutaCompletaLocal);
            if (descargaCorrecta)
            {
                commandHistory.Add(rutaCompletaRemota);
                historyIndex = commandHistory.Count;
                ManualPathTextBox.Clear();
            }
        }

        private void DescargaManual(string rutaEntradaRemota, string rutaSalidaLocal)
        {
            descargaCorrecta = false;
            string archivo = Path.GetFileName(rutaEntradaRemota);
            string localFilePath = Path.Combine(rutaSalidaLocal, archivo);

            try
            {
                if (!client!.Exists(rutaEntradaRemota))
                {
                    MessageBox.Show("No existe el archivo: " + archivo);
                    return;
                }

                var attrs = client.GetAttributes(rutaEntradaRemota);
                if (attrs.IsDirectory)
                {
                    MessageBox.Show("Error - El texto ingresado es un directorio: " + rutaEntradaRemota);
                    return;
                }

                using (Stream filestream = File.Create(localFilePath))
                {
                    client.DownloadFile(rutaEntradaRemota, filestream);
                }

                descargaCorrecta = true;
                MessageBox.Show("Archivo descargado correctmente");
            }
            catch
            {
                MessageBox.Show("Error al descarrgar el archivo: " + archivo);
            }
        }

        private void ManualPathTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ManualDownloadButton_Click(sender, new RoutedEventArgs());
                e.Handled = true;
            }
            else if (e.Key == Key.Up)
            {
                if (commandHistory.Count == 0)
                {
                    return;
                }

                historyIndex--;
                if (historyIndex < 0)
                {
                    historyIndex = 0;
                }

                ManualPathTextBox.Text = commandHistory[historyIndex];
                ManualPathTextBox.CaretIndex = ManualPathTextBox.Text.Length;
                e.Handled = true;
            }
            else if (e.Key == Key.Down)
            {
                if (commandHistory.Count == 0)
                {
                    return;
                }

                historyIndex++;
                if (historyIndex >= commandHistory.Count)
                {
                    historyIndex = commandHistory.Count;
                    ManualPathTextBox.Clear();
                }
                else
                {
                    ManualPathTextBox.Text = commandHistory[historyIndex];
                    ManualPathTextBox.CaretIndex = ManualPathTextBox.Text.Length;
                }

                e.Handled = true;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            cancelarDescarga = true;
        }

        private void ClearCacheButton_Click(object sender, RoutedEventArgs e)
        {
            cacheDirectorios.Clear();
            MessageBox.Show("Cache de directorios limpiado");
        }

        private void OtherEnvironmentRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            OtherEnvironmentTextBox.Visibility = Visibility.Visible;
        }

        private void OtherEnvironmentRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            OtherEnvironmentTextBox.Visibility = Visibility.Collapsed;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (client == null || !client.IsConnected)
            {
                connectionTimer.Stop();
                cancelarDescarga = true;
                CurrentItemTextBlock.Text = string.Empty;
                ConnectionStatusText.Text = "DESCONECTADO";
                ConnectionStatusText.Background = System.Windows.Media.Brushes.Tomato;
                SetDisconnectedState();
                MessageBox.Show("SE PERDIÓ LA CONEXION CON EL SERVIDOR");
            }
        }

        private void SetConnectedState()
        {
            ConnectButton.IsEnabled = false;
            DownloadListButton.IsEnabled = true;
            ManualDownloadButton.IsEnabled = true;
            ClearCacheButton.IsEnabled = true;
        }

        private void SetDisconnectedState()
        {
            ConnectionStatusText.Text = "DESCONECTADO";
            ConnectionStatusText.Background = System.Windows.Media.Brushes.Tomato;
            ConnectButton.IsEnabled = true;
            DownloadListButton.IsEnabled = false;
            ManualDownloadButton.IsEnabled = false;
            ClearCacheButton.IsEnabled = false;
        }

        private void SetBusyState()
        {
            DownloadListButton.IsEnabled = false;
            ManualDownloadButton.IsEnabled = false;
            BrowseButton.IsEnabled = false;
            ConnectButton.IsEnabled = false;
            ClearCacheButton.IsEnabled = false;
            CancelButton.IsEnabled = true;
        }

        private void SetReadyState()
        {
            if (client == null || !client.IsConnected)
            {
                SetDisconnectedState();
                return;
            }

            SetConnectedState();
            BrowseButton.IsEnabled = true;
            CancelButton.IsEnabled = true;
        }
    }
}