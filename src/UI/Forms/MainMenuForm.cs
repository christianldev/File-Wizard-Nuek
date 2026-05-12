using System;
using System.Windows.Forms;

namespace File_Wizard.UI.Forms
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void DTuploadbutton_Click(object sender, EventArgs e)
        {
            new File_Wizard.UI.DesaTest.DesaTestUploadForm().Show();
        }

        private void QAuploadbutton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("WORK IN PROGRESS...");
        }

        private void DTdownbutton_Click(object sender, EventArgs e)
        {
            new File_Wizard.UI.DesaTest.DesaTestDownloadForm().Show();
        }

        private void QAdownbutton_Click(object sender, EventArgs e)
        {
            new File_Wizard.UI.Qa.QaDownloadForm().Show();
        }
    }
}

