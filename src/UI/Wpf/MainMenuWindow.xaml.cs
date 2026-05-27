using System;
using System.Runtime.Versioning;
using System.Windows;
using System.Windows.Controls;
using File_Wizard.Infrastructure;

namespace File_Wizard.UI.Wpf
{
    [SupportedOSPlatform("windows")]
    public partial class MainMenuWindow : Window
    {
        private readonly SftpConnectionSettings connectionSettings;

        public MainMenuWindow(SftpConnectionSettings connectionSettings)
        {
            this.connectionSettings = connectionSettings ?? throw new ArgumentNullException(nameof(connectionSettings));
            InitializeComponent();
            ApplyEnvironmentFilter();
        }

        private void ApplyEnvironmentFilter()
        {
            bool isQaEnvironment = string.Equals(
                connectionSettings.EnvironmentName,
                "QA",
                StringComparison.OrdinalIgnoreCase);

            // Reflow cards so the visible pair always occupies the first row.
            Grid.SetRow(QaDownloadButton, isQaEnvironment ? 0 : 1);
            Grid.SetRow(QaUploadButton, isQaEnvironment ? 0 : 1);

            DesaTestDownloadButton.Visibility = isQaEnvironment ? Visibility.Collapsed : Visibility.Visible;
            DesaTestUploadButton.Visibility = isQaEnvironment ? Visibility.Collapsed : Visibility.Visible;
            QaDownloadButton.Visibility = isQaEnvironment ? Visibility.Visible : Visibility.Collapsed;
            QaUploadButton.Visibility = isQaEnvironment ? Visibility.Visible : Visibility.Collapsed;

            DashboardHintTextBlock.Text = isQaEnvironment
                ? "Sesion QA: solo se muestran operaciones del entorno QA"
                : "Sesion DESA/TEST: solo se muestran operaciones del entorno DESA-TEST";
        }

        private void DTuploadbutton_Click(object sender, RoutedEventArgs e)
        {
            new DesaTestUploadWindow(connectionSettings).Show();
        }

        private void QAuploadbutton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("WORK IN PROGRESS...");
        }

        private void DTdownbutton_Click(object sender, RoutedEventArgs e)
        {
            new DesaTestDownloadWindow(connectionSettings).Show();
        }

        private void QAdownbutton_Click(object sender, RoutedEventArgs e)
        {
            new QaDownloadWindow(connectionSettings).Show();
        }
    }
}