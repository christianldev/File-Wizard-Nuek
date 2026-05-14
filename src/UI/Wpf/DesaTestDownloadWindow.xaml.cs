using File_Wizard.Infrastructure;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Runtime.Versioning;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Ookii.Dialogs.Wpf;

namespace File_Wizard.UI.Wpf
{
    [SupportedOSPlatform("windows")]
    public partial class DesaTestDownloadWindow : Window
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
        private string directorio = "/sat/cdp/desa/cpy";

        public DesaTestDownloadWindow(SftpConnectionSettings connectionSettings)
        {
            this.connectionSettings = connectionSettings ?? throw new ArgumentNullException(nameof(connectionSettings));
            InitializeComponent();

            connectionTimer.Interval = TimeSpan.FromSeconds(1);
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
                    DownloadButton.IsEnabled = true;
                    ClearCacheButton.IsEnabled = true;
                    ManualDownloadButton.IsEnabled = true;
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

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() => ResultTextBlock.Text = "TRACE: DownloadButton_Click invoked");

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

            // Capture UI state on the UI thread before starting background work
            string filesText = string.Empty;
            string localDirectory = string.Empty;
            bool prefixChecked = false;
            bool desaChecked = false;

            Dispatcher.Invoke(() =>
            {
                filesText = FilesTextBox.Text;
                localDirectory = LocalDirectoryTextBox.Text;
                prefixChecked = PrefixCheckBox.IsChecked == true;
                desaChecked = DesaRadioButton.IsChecked == true;
            });

            cancelarDescarga = false;
            SetBusyState();

            var downloadThread = new Thread(() =>
            {
                DescargarArchivos(filesText, localDirectory, prefixChecked, desaChecked);
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

            List<ISftpFile> archivos = sftpClient
                .ListDirectory(remoteDirectory)
                .Where(f => f.IsRegularFile)
                .ToList();

            cacheDirectorios.Add(remoteDirectory, archivos);
            return archivos;
        }

        private void DescargarArchivos(string filesText, string localDirectory, bool prefixChecked, bool desaChecked)
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

                if (desaChecked)
                {
                    prefijoPredict = "DESA-";
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
                    prefijoPredict = "TEST-";
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

                prefijo = prefixChecked ? prefijoPredict : string.Empty;

                if (!hubodirectorio)
                {
                    MessageBox.Show("Error - No se encontró directorio válido para el archivo: " + linea);
                    continue;
                }

                if (linea.Contains("*") || linea.Contains("?"))
                {
                    DescargarPorPatron(linea.Trim(), prefijo, ref numlineasOK, localDirectory, directorio);
                    continue;
                }

                string archivoSimple = linea.Trim();
                string rutaRemotaCompleta = directorio + "/" + archivoSimple;

                try
                {
                    if (!client!.Exists(rutaRemotaCompleta))
                    {
                        Dispatcher.Invoke(() => MessageBox.Show("No existe el archivo: " + archivoSimple));
                        continue;
                    }

                    var atributos = client.GetAttributes(rutaRemotaCompleta);
                    localFilePath = Path.Combine(localDirectory, prefijo + archivoSimple);
                    using Stream filestream = File.Create(localFilePath);
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
                            DownloadProgressBar.Visibility = Visibility.Visible;
                            DownloadProgressBar.Maximum = Math.Max(1, (long)atributos.Size);
                            DownloadProgressBar.Value = Math.Min((double)totalBytesDownloaded, DownloadProgressBar.Maximum);
                            CurrentFileTextBlock.Text = archivoSimple;
                        });
                    });

                    numlineasOK++;
                }
                catch
                {
                    Dispatcher.Invoke(() => MessageBox.Show("Error al descargar el archivo: " + linea));
                }
                finally
                {
                    Dispatcher.Invoke(() =>
                    {
                        DownloadProgressBar.Visibility = Visibility.Collapsed;
                        CurrentFileTextBlock.Text = string.Empty;
                    });
                }
            }

            Dispatcher.Invoke(() =>
            {
                if (numlineasOK == numlineas)
                {
                    Dispatcher.Invoke(() => MessageBox.Show(numlineas == 1 ? "Archivo descargado correctmente" : "Todos los archivos fueron descargados correctamente"));
                    Dispatcher.Invoke(() => ResultTextBlock.Text = numlineas == 1 ? "Archivo descargado correctmente" : "Todos los archivos fueron descargados correctamente");
                }
                else if (numlineas > 0)
                {
                    Dispatcher.Invoke(() => MessageBox.Show("Hubo error en la descarga de 1 o más componentes"));
                    Dispatcher.Invoke(() => ResultTextBlock.Text = "Hubo error en la descarga de 1 o más componentes");
                }
            });
        }

        private void DescargarPorPatron(string patron, string prefijo, ref int numlineasOK, string localDirectory, string directorioLocal)
        {
            if (patron == "*" || patron == "?")
            {
                Dispatcher.Invoke(() => MessageBox.Show("Nombre incorrecto del archivo a descargar: * ?"));
                return;
            }

            int numarchivosmatch = 0;
            string regexPattern = "^" + Regex.Escape(patron).Replace("\\*", ".*").Replace("\\?", ".") + "$";

            try
            {
                List<ISftpFile> archivos = ObtenerArchivosDelDirectorio(client!, directorioLocal);

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
                    using Stream filestream = File.Create(localFilePath);
                    ulong totalBytesDownloaded = 0;

                    client!.DownloadFile(directorioLocal + "/" + archivo.Name, filestream, bytesRead =>
                    {
                        if (cancelarDescarga)
                        {
                            return;
                        }

                        totalBytesDownloaded += bytesRead;
                        Dispatcher.Invoke(() =>
                        {
                            DownloadProgressBar.Visibility = Visibility.Visible;
                            DownloadProgressBar.Maximum = Math.Max(1, (long)archivo.Length);
                            DownloadProgressBar.Value = Math.Min((double)totalBytesDownloaded, DownloadProgressBar.Maximum);
                            CurrentFileTextBlock.Text = archivo.Name;
                        });
                    });

                        Dispatcher.Invoke(() =>
                        {
                            DownloadProgressBar.Visibility = Visibility.Collapsed;
                            CurrentFileTextBlock.Text = string.Empty;
                        });
                }

                if (numarchivosmatch == 0)
                {
                    Dispatcher.Invoke(() => MessageBox.Show("No se encontró el archivo: " + patron));
                }
                else
                {
                    numlineasOK++;
                }
            }
            catch
            {
                Dispatcher.Invoke(() => MessageBox.Show("Error al descargar el archivo: " + patron));
            }
        }

        private void ManualDownloadButton_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() => ResultTextBlock.Text = "TRACE: ManualDownloadButton_Click invoked");

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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            cancelarDescarga = true;
        }

        private void ClearCacheButton_Click(object sender, RoutedEventArgs e)
        {
            cacheDirectorios.Clear();
            MessageBox.Show("Cache de directorios limpiado");
        }

        private void ManualPathTextBox_KeyDown(object sender, KeyEventArgs e)
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

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (client == null || !client.IsConnected)
            {
                connectionTimer.Stop();
                cancelarDescarga = true;
                CurrentFileTextBlock.Text = string.Empty;
                ConnectionStatusText.Text = "DESCONECTADO";
                ConnectionStatusText.Background = System.Windows.Media.Brushes.Tomato;
                SetDisconnectedState();
                MessageBox.Show("SE PERDIO LA CONEXION CON EL SERVIDOR");
            }
        }

        private void SetConnectedState()
        {
            DownloadButton.IsEnabled = true;
            ClearCacheButton.IsEnabled = true;
            ManualDownloadButton.IsEnabled = true;
            BrowseButton.IsEnabled = true;
            CancelButton.IsEnabled = true;
        }

        private void SetDisconnectedState()
        {
            ConnectionStatusText.Text = "DESCONECTADO";
            ConnectionStatusText.Background = System.Windows.Media.Brushes.Tomato;
            DownloadButton.IsEnabled = false;
            ClearCacheButton.IsEnabled = false;
            ManualDownloadButton.IsEnabled = false;
            BrowseButton.IsEnabled = true;
            CancelButton.IsEnabled = true;
            ResultTextBlock.Text = string.Empty;
        }

        private void SetBusyState()
        {
            DownloadButton.IsEnabled = false;
            ClearCacheButton.IsEnabled = false;
            ManualDownloadButton.IsEnabled = false;
            BrowseButton.IsEnabled = false;
            CancelButton.IsEnabled = false;
        }

        private void SetReadyState()
        {
            if (client == null || !client.IsConnected)
            {
                SetDisconnectedState();
                return;
            }

            SetConnectedState();
        }
    }
}