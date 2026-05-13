using System;
using System.Globalization;
using System.Runtime.Versioning;
using System.Windows;
using Renci.SshNet;
using File_Wizard.Infrastructure;

namespace File_Wizard.UI.Wpf
{
    [SupportedOSPlatform("windows")]
    public partial class SftpLoginWindow : Window
    {
        public SftpConnectionSettings? ConnectionSettings { get; private set; }

        public SftpLoginWindow()
        {
            InitializeComponent();
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

                ConnectionSettings = new SftpConnectionSettings(
                    HostTextBox.Text.Trim(),
                    port,
                    UsernameTextBox.Text.Trim(),
                    PasswordBox.Password);

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar las credenciales SFTP: {ex.Message}");
            }
        }
    }
}