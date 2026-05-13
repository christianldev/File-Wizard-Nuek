using System;
using System.Runtime.Versioning;
using System.Windows;
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