using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Wizard
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void DTuploadbutton_Click(object sender, EventArgs e)
        {
            DesaTestUploadForm form4 = new DesaTestUploadForm();
            form4.Show();
        }

        private void QAuploadbutton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("WORK IN PROGRESS...");
        }

        private void DTdownbutton_Click(object sender, EventArgs e)
        {
            DesaTestDownloadForm form2 = new DesaTestDownloadForm();
            form2.Show();
        }

        private void QAdownbutton_Click(object sender, EventArgs e)
        {
            QaDownloadForm form3 = new QaDownloadForm();
            form3.Show();
        }
    }
}
