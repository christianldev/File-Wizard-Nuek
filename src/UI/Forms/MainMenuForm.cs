using System;
using System.Windows.Forms;
using File_Wizard.Infrastructure;

namespace File_Wizard.UI.Forms
{
    public partial class MainMenuForm : Form
    {
        private readonly SftpConnectionSettings connectionSettings;

        public MainMenuForm(SftpConnectionSettings connectionSettings)
        {
            this.connectionSettings = connectionSettings ?? throw new ArgumentNullException(nameof(connectionSettings));
            InitializeComponent();
        }

        private void DTuploadbutton_Click(object sender, EventArgs e)
        {
            new File_Wizard.UI.DesaTest.DesaTestUploadForm(connectionSettings).Show();
        }

        private void QAuploadbutton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("WORK IN PROGRESS...");
        }

        private void DTdownbutton_Click(object sender, EventArgs e)
        {
            new File_Wizard.UI.DesaTest.DesaTestDownloadForm(connectionSettings).Show();
        }

        private void QAdownbutton_Click(object sender, EventArgs e)
        {
            new File_Wizard.UI.Qa.QaDownloadForm(connectionSettings).Show();
        }
    }
}

