using File_Wizard.Infrastructure;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Threading;
using Ookii.Dialogs.Wpf;

namespace File_Wizard.UI.Wpf
{
    [SupportedOSPlatform("windows")]
    public partial class DesaTestUploadWindow : Window
    {
        private readonly SftpConnectionSettings connectionSettings;
        private readonly DispatcherTimer connectionTimer = new DispatcherTimer();
        private readonly ObservableCollection<UploadDestinationDecision> destinationDecisions = new ObservableCollection<UploadDestinationDecision>();
        private readonly Dictionary<string, string> destinationChoiceCache = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private SftpClient? client;
        private string rutaLocal = @"C:";
        private string directorio = "/sat/cdp/desa/cpy";

        public DesaTestUploadWindow(SftpConnectionSettings connectionSettings)
        {
            this.connectionSettings = connectionSettings ?? throw new ArgumentNullException(nameof(connectionSettings));
            InitializeComponent();

            DestinationDecisionsGrid.ItemsSource = destinationDecisions;
            DesaRadioButton.Checked += DestinationContext_Changed;
            TestRadioButton.Checked += DestinationContext_Changed;
            DatSecRoutingCheckBox.Checked += DestinationContext_Changed;
            DatSecRoutingCheckBox.Unchecked += DestinationContext_Changed;

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
                    SelectedFilesTextBox.Text = Path.GetFileName(openDialog.FileName);
                }
                else
                {
                    SelectedFilesTextBox.Text = string.Join(Environment.NewLine, archivosSeleccionadosParaSubir);
                }

                RefreshDestinationDecisions();
            }
        }

        private void DestinationContext_Changed(object sender, RoutedEventArgs e)
        {
            RefreshDestinationDecisions();
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
                    ClearCacheButton.IsEnabled = true;
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

        private void ClearCacheButton_Click(object sender, RoutedEventArgs e)
        {
            SaveCurrentDecisionsToCache();
            archivosSeleccionadosParaSubir = null;
            SelectedFilesTextBox.Text = "Aún no hay archivos seleccionados";
            destinationDecisions.Clear();
            destinationChoiceCache.Clear();
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

            if (archivosSeleccionadosParaSubir == null || archivosSeleccionadosParaSubir.Length == 0)
            {
                MessageBox.Show("Selecciona uno o más archivos para subir.");
                return;
            }

            SaveCurrentDecisionsToCache();
            RefreshDestinationDecisions();

            int correctos = 0;
            var fallidos = new List<string>();
            var noEncontrados = new List<string>();
            var erroresDetalle = new List<string>();

            foreach (string ruta in archivosSeleccionadosParaSubir)
            {
                if (!File.Exists(ruta))
                {
                    noEncontrados.Add(Path.GetFileName(ruta));
                    continue;
                }

                string? error;
                if (SubirArchivo(ruta, mostrarMsg: false, out error))
                {
                    correctos++;
                }
                else
                {
                    fallidos.Add(Path.GetFileName(ruta));
                    if (!string.IsNullOrWhiteSpace(error))
                    {
                        erroresDetalle.Add(error);
                    }
                }
            }

            if (correctos == archivosSeleccionadosParaSubir.Length)
            {
                MessageBox.Show(archivosSeleccionadosParaSubir.Length == 1
                    ? "Archivo subido correctamente."
                    : $"Proceso finalizado. Subidos correctamente: {correctos} de {archivosSeleccionadosParaSubir.Length}.");
            }
            else
            {
                string detalleFallidos = fallidos.Count > 0
                    ? "\nFallidos: " + string.Join(", ", fallidos)
                    : string.Empty;

                string detalleNoEncontrados = noEncontrados.Count > 0
                    ? "\nNo encontrados localmente: " + string.Join(", ", noEncontrados)
                    : string.Empty;

                string detalleErrores = erroresDetalle.Count > 0
                    ? "\nDetalles: " + string.Join(" | ", erroresDetalle)
                    : string.Empty;

                MessageBox.Show(
                    $"Proceso finalizado con errores. Subidos correctamente: {correctos} de {archivosSeleccionadosParaSubir.Length}.{detalleFallidos}{detalleNoEncontrados}{detalleErrores}");
            }
        }

        private bool SubirArchivo(string rutaLocalCompleta, bool mostrarMsg, out string? error)
        {
            error = null;

            string archivo = Path.GetFileName(rutaLocalCompleta);
            string extension = Path.GetExtension(archivo);

            string? directorioDestino = ResolveRemoteDirectory(rutaLocalCompleta, archivo, extension, DesaRadioButton.IsChecked == true);

            if (string.IsNullOrWhiteSpace(directorioDestino))
            {
                error = $"{archivo}: no se determinó destino remoto";
                if (mostrarMsg)
                {
                    MessageBox.Show("No se pudo determinar el destino remoto para: " + archivo);
                }

                return false;
            }

            directorio = directorioDestino;

            try
            {
                string rutaCompletaRemota = directorio + "/" + archivo;
                if (client!.Exists(rutaCompletaRemota))
                {
                    if (MessageBox.Show("¿Estás seguro de sobreescribir este archivo?", "CONFIRMACION", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    {
                        MessageBox.Show("SUBIDA CANCELADA POR EL USUARIO");
                        error = $"{archivo}: subida cancelada por el usuario";
                        return false;
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

                error = null;
                if (mostrarMsg) MessageBox.Show("El archivo se subió correctamente y se han dado todos los permisos (Chmod 777) :)");
                return true;
            }
            catch (Exception ex)
            {
                error = $"{archivo}: {ex.Message}";
                if (mostrarMsg) MessageBox.Show("ERROR AL SUBIR EL ARCHIVO: " + archivo);
                return false;
            }
        }

        private string? ResolveRemoteDirectory(string localPath, string archivo, string extension, bool isDesaEnvironment)
        {
            string environmentRoot = isDesaEnvironment ? "/sat/cdp/desa" : "/sat/cdp/test";
            bool useDatSecRouting = DatSecRoutingCheckBox.IsChecked == true;

            switch (extension.ToLowerInvariant())
            {
                case ".sh":
                    return environmentRoot + "/cad";
                case ".cbl":
                case ".pco":
                    return environmentRoot + "/src";
                case ".scl":
                    return environmentRoot + "/cad/scl";
                case ".fact":
                    return environmentRoot + "/adm/fact";
                case ".sql":
                    return environmentRoot + "/adm/sql";
                case "":
                    return ResolveCandidateDirectory(localPath, archivo, environmentRoot, useDatSecRouting);
                default:
                    return IsDatOrCpyCandidate(archivo)
                        ? ResolveCandidateDirectory(localPath, archivo, environmentRoot, useDatSecRouting)
                        : null;
            }
        }

        private static bool IsDatOrCpyCandidate(string archivo)
        {
            // Components such as NG..., MP..., GO... can exist in either /dat or /cpy,
            // even when they have a non-standard suffix after a dot.
            return Regex.IsMatch(archivo, @"^(NG|MP|GO)", RegexOptions.IgnoreCase);
        }

        private string? ResolveCandidateDirectory(string localPath, string archivo, string environmentRoot, bool useDatSecRouting)
        {
            List<string> targets = GetAllowedTargets(useDatSecRouting);

            UploadDestinationDecision? decision = destinationDecisions
                .FirstOrDefault(item => string.Equals(item.LocalPath, localPath, StringComparison.OrdinalIgnoreCase));

            if (decision == null || string.IsNullOrWhiteSpace(decision.SelectedTarget))
            {
                return null;
            }

            string normalized = decision.SelectedTarget.ToLowerInvariant();
            return targets.Contains(normalized)
                ? environmentRoot + "/" + normalized
                : null;
        }

        private static List<string> GetAllowedTargets(bool useDatSecRouting)
        {
            // Default mode: DAT/CPY.
            // DAT/SEC mode enabled: add SEC without removing CPY, so mixed batches are possible.
            return useDatSecRouting
                ? new List<string> { "dat", "cpy", "sec" }
                : new List<string> { "dat", "cpy" };
        }

        private void RefreshDestinationDecisions()
        {
            SaveCurrentDecisionsToCache();
            destinationDecisions.Clear();

            if (archivosSeleccionadosParaSubir == null || archivosSeleccionadosParaSubir.Length == 0)
            {
                return;
            }

            bool isDesaEnvironment = DesaRadioButton.IsChecked == true;
            string environmentRoot = isDesaEnvironment ? "/sat/cdp/desa" : "/sat/cdp/test";
            bool useDatSecRouting = DatSecRoutingCheckBox.IsChecked == true;
            List<string> allowedTargets = GetAllowedTargets(useDatSecRouting);

            foreach (string localPath in archivosSeleccionadosParaSubir)
            {
                string fileName = Path.GetFileName(localPath);
                string extension = Path.GetExtension(fileName);
                bool isCandidate = extension.Length == 0 || IsDatOrCpyCandidate(fileName);

                if (!isCandidate)
                {
                    continue;
                }

                string suggestedTarget = ResolveSuggestedTarget(localPath, fileName, environmentRoot, allowedTargets);

                destinationDecisions.Add(new UploadDestinationDecision
                {
                    LocalPath = localPath,
                    FileName = fileName,
                    AvailableTargets = new List<string>(allowedTargets),
                    SelectedTarget = suggestedTarget,
                    DetectionStatus = BuildDetectionStatus(fileName, environmentRoot, allowedTargets)
                });
            }
        }

        private void SaveCurrentDecisionsToCache()
        {
            foreach (UploadDestinationDecision decision in destinationDecisions)
            {
                if (!string.IsNullOrWhiteSpace(decision.SelectedTarget))
                {
                    destinationChoiceCache[decision.LocalPath] = decision.SelectedTarget.ToLowerInvariant();
                }
            }
        }

        private string ResolveSuggestedTarget(string localPath, string fileName, string environmentRoot, List<string> allowedTargets)
        {
            if (destinationChoiceCache.TryGetValue(localPath, out string? cachedValue) &&
                allowedTargets.Contains(cachedValue, StringComparer.OrdinalIgnoreCase))
            {
                return cachedValue;
            }

            if (client == null || !client.IsConnected)
            {
                return GetDefaultTargetWhenAmbiguous(fileName, allowedTargets);
            }

            var existingTargets = new List<string>();
            foreach (string target in allowedTargets)
            {
                if (client.Exists(environmentRoot + "/" + target + "/" + fileName))
                {
                    existingTargets.Add(target);
                }
            }

            if (existingTargets.Count == 1)
            {
                return existingTargets[0];
            }

            if (existingTargets.Count > 1)
            {
                return GetDefaultTargetWhenAmbiguous(fileName, allowedTargets);
            }

            return GetDefaultTargetWhenAmbiguous(fileName, allowedTargets);
        }

        private static string GetDefaultTargetWhenAmbiguous(string fileName, List<string> allowedTargets)
        {
            bool hasDat = allowedTargets.Contains("dat", StringComparer.OrdinalIgnoreCase);
            bool hasCpy = allowedTargets.Contains("cpy", StringComparer.OrdinalIgnoreCase);

            // If CPY is available and file does not look like a DAT payload, prefer CPY.
            if (hasCpy)
            {
                return LooksLikeDatFile(fileName) && hasDat ? "dat" : "cpy";
            }

            if (hasDat)
            {
                return "dat";
            }

            return allowedTargets.First();
        }

        private static bool LooksLikeDatFile(string fileName)
        {
            // Typical DAT patterns observed in the downloader logic.
            return Regex.IsMatch(fileName, @"^(NG|MP|GO)\d+\.F[A-Z0-9]+$", RegexOptions.IgnoreCase);
        }

        private string BuildDetectionStatus(string fileName, string environmentRoot, List<string> allowedTargets)
        {
            if (client == null || !client.IsConnected)
            {
                return "Sin conexión";
            }

            var existingTargets = new List<string>();
            foreach (string target in allowedTargets)
            {
                if (client.Exists(environmentRoot + "/" + target + "/" + fileName))
                {
                    existingTargets.Add(target);
                }
            }

            if (existingTargets.Count == 0)
            {
                return "No existe en servidor";
            }

            if (existingTargets.Count == 1)
            {
                return $"Solo {existingTargets[0]}";
            }

            return "Existe en: " + string.Join(", ", existingTargets);
        }

        private sealed class UploadDestinationDecision
        {
            public string LocalPath { get; set; } = string.Empty;
            public string FileName { get; set; } = string.Empty;
            public List<string> AvailableTargets { get; set; } = new List<string>();
            public string SelectedTarget { get; set; } = string.Empty;
            public string DetectionStatus { get; set; } = string.Empty;
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
            ClearCacheButton.IsEnabled = true;
            BrowseButton.IsEnabled = true;
        }

        private void SetDisconnectedState()
        {
            ConnectionStatusText.Text = "DESCONECTADO";
            ConnectionStatusText.Background = System.Windows.Media.Brushes.Tomato;
            UploadFileButton.IsEnabled = false;
            ClearCacheButton.IsEnabled = false;
            BrowseButton.IsEnabled = true;
        }
    }
}