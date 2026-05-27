using System;
using System.Globalization;
using System.Runtime.Versioning;
using System.Windows;
using System.Windows.Media;
using Renci.SshNet;
using File_Wizard.Infrastructure;

namespace File_Wizard.UI.Wpf
{
    [SupportedOSPlatform("windows")]
    public partial class SftpLoginWindow : Window
    {
        public SftpConnectionSettings? ConnectionSettings { get; private set; }

        private const string DefaultEnvironmentName = "DESA";

        public SftpLoginWindow()
        {
            InitializeComponent();
            UpdateHostAndPortByEnvironment();
        }

        private void EnvironmentComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdateHostAndPortByEnvironment();
        }

        private void UpdateHostAndPortByEnvironment()
        {
            string environmentName = GetSelectedEnvironmentName();
            string host = GetEnvironmentSetting(environmentName, "HOST");
            string portValue = GetEnvironmentSetting(environmentName, "PORT");

            HostTextBox.Text = host;
            PortTextBox.Text = portValue;

            if (string.IsNullOrWhiteSpace(host) && string.IsNullOrWhiteSpace(portValue))
            {
                SetEnvironmentStatus(
                    $"No hay HOST ni PUERTO para {environmentName} en .env. Puedes ingresarlos manualmente.",
                    isWarning: true);
                return;
            }

            if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(portValue))
            {
                SetEnvironmentStatus(
                    $"Configuracion incompleta para {environmentName} en .env. Verifica HOST y PORT.",
                    isWarning: true);
                return;
            }

            if (!int.TryParse(portValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedPort) || parsedPort < 1 || parsedPort > 65535)
            {
                SetEnvironmentStatus(
                    $"El puerto configurado para {environmentName} no es valido. Debe estar entre 1 y 65535.",
                    isWarning: true);
                return;
            }

            SetEnvironmentStatus($"HOST y PUERTO cargados desde .env para {environmentName}.", isWarning: false);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(HostTextBox.Text) || string.IsNullOrWhiteSpace(UsernameTextBox.Text))
            {
                MessageBox.Show("Completa host y usuario.");
                return;
            }

            if (!int.TryParse(PortTextBox.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var port) || port < 1 || port > 65535)
            {
                MessageBox.Show("Ingresa un puerto válido.");
                return;
            }

            try
            {
                using var client = new SftpClient(HostTextBox.Text.Trim(), port, UsernameTextBox.Text.Trim(), PasswordBox.Password);
                client.ConnectionInfo.Timeout = TimeSpan.FromSeconds(8);
                client.Connect();

                if (!client.IsConnected)
                {
                    MessageBox.Show("No fue posible validar la conexión SFTP.");
                    return;
                }

                client.Disconnect();

                string environmentName = GetSelectedEnvironmentName();

                ConnectionSettings = new SftpConnectionSettings(
                    HostTextBox.Text.Trim(),
                    port,
                    UsernameTextBox.Text.Trim(),
                    PasswordBox.Password,
                    environmentName);

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar las credenciales SFTP: {ex.Message}");
            }
        }

        private string GetSelectedEnvironmentName()
        {
            string environmentName = ((System.Windows.Controls.ComboBoxItem)EnvironmentComboBox.SelectedItem)?.Content?.ToString() ?? DefaultEnvironmentName;

            return environmentName.Trim().ToUpperInvariant();
        }

        private static string GetEnvironmentSetting(string environmentName, string settingName)
        {
            return DotEnv.GetString($"SFTP_{environmentName}_{settingName}", string.Empty);
        }

        private void SetEnvironmentStatus(string message, bool isWarning)
        {
            if (EnvironmentStatusTextBlock is null)
            {
                return;
            }

            EnvironmentStatusTextBlock.Text = message;
            EnvironmentStatusTextBlock.Foreground = isWarning
                ? new SolidColorBrush(Color.FromRgb(153, 27, 27))
                : new SolidColorBrush(Color.FromRgb(6, 95, 70));
        }
    }
}