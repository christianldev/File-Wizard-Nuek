using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using File_Wizard.Infrastructure;

namespace File_Wizard.UI.DesaTest
{
    public partial class DesaTestUploadForm : Form
    {
        private readonly SftpConnectionSettings connectionSettings;
        private bool subidaCorrecta = false;
        private string rutaLocal = @"C:";
        private List<string> commandHistory = new List<string>();
        private int historyIndex = -1;

        public DesaTestUploadForm(SftpConnectionSettings connectionSettings)
        {
            this.connectionSettings = connectionSettings ?? throw new ArgumentNullException(nameof(connectionSettings));
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openD = new FolderBrowserDialog();
            openD.SelectedPath = rutaLocal;
            openD.Description = "Selecciona el directorio donde se encuentran los componentes...";
            openD.ShowNewFolderButton = true;
            if (openD.ShowDialog() == DialogResult.OK)
            {
                txtDir.Text = openD.SelectedPath;
                rutaLocal = openD.SelectedPath;
            }
            else
            {
                MessageBox.Show("Error al seleccionar el directorio, intente de nuevo...");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                client = new SftpClient(connectionSettings.Host, connectionSettings.Port, connectionSettings.Username, connectionSettings.Password);
                client.ConnectionInfo.Timeout = TimeSpan.FromSeconds(8);
                client.Connect();
                if (client.IsConnected)
                {
                    timer1.Start();
                    label3.Text = "CONECTADO";
                    label3.BackColor = Color.Lime;
                    button2.Enabled = false;
                    button2.BackColor = SystemColors.ActiveBorder;
                    button3.Enabled = true;
                    button3.BackColor = SystemColors.ControlLight;
                    button6.Enabled = true;
                    button6.BackColor = SystemColors.ControlLight;
                }
                else
                {
                    MessageBox.Show("CONEXION fallida");
                    label3.Text = "DESCONECTADO";
                    label3.BackColor = Color.Tomato;
                    button2.Enabled = true;
                    button2.BackColor = SystemColors.ControlLight;
                    button3.Enabled = false;
                    button3.BackColor = SystemColors.ActiveBorder;
                    button6.Enabled = false;
                    button6.BackColor = SystemColors.ActiveBorder;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error en la conexion");
                label3.Text = "DESCONECTADO";
                label3.BackColor = Color.Tomato;
                button2.Enabled = true;
                button2.BackColor = SystemColors.ControlLight;
                button3.Enabled = false;
                button3.BackColor = SystemColors.ActiveBorder;
                button6.Enabled = false;
                button6.BackColor = SystemColors.ActiveBorder;
            }
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null && client.IsConnected)
            {
                client.Disconnect();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (client == null || !client.IsConnected)
            {
                MessageBox.Show("NO HAY CONEXION CON EL SERVIDOR");
                return;
            }
            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                MessageBox.Show("Por favor, elegir un ambiente: DESA o TEST");
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("No se han escrito los elementos para descargar");
                return;
            }
            if (txtDir.Text.Length == 0)
            {
                MessageBox.Show("Selecciona un directorio :)");
                return;
            }

            string archivo = textBox1.Text.Trim();
            if (archivo.Contains("/") || archivo.Contains("?"))
            {
                MessageBox.Show("El nombre del archivo a subir tiene caracteres no permitidos: * ?");
                return;
            }

            string rutaCompleta = Path.Combine(txtDir.Text.Trim(), archivo);
            if (File.Exists(rutaCompleta))
            {
                SubirArchivo(rutaCompleta);
            }
            else
            {
                MessageBox.Show("El archivo local ingresado no existe :(");
            }
        }

        private void SubirArchivo(string rutaLocalCompleta)
        {
            string archivo = Path.GetFileName(rutaLocalCompleta);
            string extension = Path.GetExtension(archivo);
            bool hubodirectorio = true;
            if (radioButton1.Checked)
            {
                switch (extension)
                {
                    case ".sh":
                        directorio = "/sat/cdp/desa/cad";
                        break;
                    case ".cbl":
                    case ".pco":
                        directorio = "/sat/cdp/desa/src";
                        break;
                    case ".scl":
                        directorio = "/sat/cdp/desa/cad/scl";
                        break;
                    case ".fact":
                        directorio = "/sat/cdp/desa/adm/fact";
                        break;
                    case ".sql":
                        directorio = "/sat/cdp/desa/adm/sql";
                        break;
                    case "":
                        directorio = "/sat/cdp/desa/cpy";
                        break;
                    default:
                        hubodirectorio = false;
                        break;
                }
            }
            else
            {
                switch (extension)
                {
                    case ".sh":
                        directorio = "/sat/cdp/test/cad";
                        break;
                    case ".cbl":
                    case ".pco":
                        directorio = "/sat/cdp/test/src";
                        break;
                    case ".scl":
                        directorio = "/sat/cdp/test/cad/scl";
                        break;
                    case ".fact":
                        directorio = "/sat/cdp/test/adm/fact";
                        break;
                    case ".sql":
                        directorio = "/sat/cdp/test/adm/sql";
                        break;
                    case "":
                        directorio = "/sat/cdp/test/cpy";
                        break;
                    default:
                        hubodirectorio = false;
                        break;
                }
            }

            if (hubodirectorio)
            {
                try
                {
                    string rutaCompletaRemota = directorio + "/" + archivo;
                    if (client.Exists(rutaCompletaRemota))
                    {
                        if (MessageBox.Show("\u00bfEst\u00e1s seguro de sobreescribir este archivo?", "CONFIRMACION", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            MessageBox.Show("SUBIDA CANCELADA POR EL USUARIO");
                            return;
                        }
                    }

                    using (FileStream fs = new FileStream(rutaLocalCompleta, FileMode.Open))
                    {
                        client.UploadFile(fs, rutaCompletaRemota);
                    }

                    MessageBox.Show("El archivo se subi\u00f3 correctamente :)");
                }
                catch
                {
                    MessageBox.Show("ERROR AL SUBIR EL ARCHIVO: " + archivo);
                }
            }
            else
            {
                MessageBox.Show("Error - No se encontr\u00f3 directorio v\u00e1lido para el archivo: " + archivo);
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (client == null || !client.IsConnected)
                {
                    MessageBox.Show("NO HAY CONEXION CON EL SERVIDOR");
                    return;
                }
                if (string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("No se han escrito los elementos para subir");
                    return;
                }
                if (txtDir.Text.Length == 0)
                {
                    MessageBox.Show("Selecciona un directorio :)");
                    return;
                }

                string rutaCompletaRemota = textBox3.Text.Trim();
                if (rutaCompletaRemota.Contains("/") && rutaCompletaRemota.StartsWith("/") && !rutaCompletaRemota.EndsWith("/")
                    && !rutaCompletaRemota.Contains("*") && !rutaCompletaRemota.Contains("?"))
                {
                    string archivo = Path.GetFileName(textBox3.Text.Trim());
                    string rutaCompletaLocal = Path.Combine(txtDir.Text.Trim(), archivo);
                    if (!File.Exists(rutaCompletaLocal))
                    {
                        MessageBox.Show("El archivo que se desea subir no existe en el directorio local :(");
                        return;
                    }

                    SubidaManual(rutaCompletaLocal, rutaCompletaRemota, archivo);
                    if (subidaCorrecta)
                    {
                        e.SuppressKeyPress = true;
                        commandHistory.Add(rutaCompletaRemota);
                        historyIndex = commandHistory.Count;
                        textBox3.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("La ruta del fichero a subir no es correcta");
                    return;
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (commandHistory.Count == 0) return;

                historyIndex--;
                if (historyIndex < 0)
                    historyIndex = 0;

                textBox3.Text = commandHistory[historyIndex];
                textBox3.SelectionStart = textBox3.Text.Length;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (commandHistory.Count == 0) return;

                historyIndex++;
                if (historyIndex >= commandHistory.Count)
                {
                    historyIndex = commandHistory.Count;
                    textBox3.Clear();
                }
                else
                {
                    textBox3.Text = commandHistory[historyIndex];
                    textBox3.SelectionStart = textBox3.Text.Length;
                }
                e.SuppressKeyPress = true;
            }
        }

        private void SubidaManual(string rutaEntradaLocalCompleta, string rutaSalidaRemotaCompleta, string archivoASubir)
        {
            try
            {
                if (client.Exists(rutaSalidaRemotaCompleta))
                {
                    if (MessageBox.Show("\u00bfEst\u00e1s seguro de sobreescribir este archivo?", "CONFIRMACION", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        subidaCorrecta = false;
                        MessageBox.Show("SUBIDA CANCELADA POR EL USUARIO");
                        return;
                    }
                }
                using (FileStream fs = new FileStream(rutaEntradaLocalCompleta, FileMode.Open))
                {
                    client.UploadFile(fs, rutaSalidaRemotaCompleta);
                }
                subidaCorrecta = true;
                MessageBox.Show("El archivo se subi\u00f3 correctamente :)");
            }
            catch
            {
                subidaCorrecta = false;
                MessageBox.Show("ERROR AL SUBIR EL ARCHIVO: " + archivoASubir);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (client == null || !client.IsConnected)
            {
                MessageBox.Show("NO HAY CONEXION CON EL SERVIDOR");
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("No se han escrito los elementos para subir");
                return;
            }
            if (txtDir.Text.Length == 0)
            {
                MessageBox.Show("Selecciona un directorio :)");
                return;
            }

            string rutaCompletaRemota = textBox3.Text.Trim();
            if (rutaCompletaRemota.Contains("/") && rutaCompletaRemota.StartsWith("/") && !rutaCompletaRemota.EndsWith("/")
                && !rutaCompletaRemota.Contains("*") && !rutaCompletaRemota.Contains("?"))
            {
                string archivo = Path.GetFileName(textBox3.Text.Trim());
                string rutaCompletaLocal = Path.Combine(txtDir.Text.Trim(), archivo);
                if (!File.Exists(rutaCompletaLocal))
                {
                    MessageBox.Show("El archivo que se desea subir no existe en el directorio local :(");
                    return;
                }
                SubidaManual(rutaCompletaLocal, rutaCompletaRemota, archivo);
                if (subidaCorrecta)
                {
                    commandHistory.Add(rutaCompletaRemota);
                    historyIndex = commandHistory.Count;
                    textBox3.Clear();
                }
            }
            else
            {
                MessageBox.Show("La ruta del fichero a subir no es correcta");
                return;
            }
        }
    }
}

