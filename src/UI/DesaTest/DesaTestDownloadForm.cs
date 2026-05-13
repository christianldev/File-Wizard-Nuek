using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using File_Wizard.Infrastructure;

namespace File_Wizard.UI.DesaTest
{
    public partial class DesaTestDownloadForm : Form
    {
        private readonly SftpConnectionSettings connectionSettings;
        private bool cancelarDescarga = false;
        private bool descargaCorrecta = false;
        private string rutaLocal = @"C:";
        private readonly List<string> commandHistory = new List<string>();
        private int historyIndex = -1;

        public DesaTestDownloadForm(SftpConnectionSettings connectionSettings)
        {
            this.connectionSettings = connectionSettings ?? throw new ArgumentNullException(nameof(connectionSettings));
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog openD = new FolderBrowserDialog();
            openD.SelectedPath = rutaLocal;
            openD.Description = "Selecciona un directorio para guardar los componentes...";
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
                    button5.Enabled = true;
                    button5.BackColor = SystemColors.ControlLight;
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
                    button5.Enabled = false;
                    button5.BackColor = SystemColors.ActiveBorder;
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
                button5.Enabled = false;
                button5.BackColor = SystemColors.ActiveBorder;
                button6.Enabled = false;
                button6.BackColor = SystemColors.ActiveBorder;
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
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

            cancelarDescarga = false;
            DesactivarBotones();

            Thread descargaThread = new Thread(() =>
            {
                DescargarArchivos();
                Invoke((Action)(ActivarBotones));
            });
            descargaThread.Start();
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

        private void DescargarArchivos()
        {
            int numlineas = 0;
            int numlineasOK = 0;

            foreach (string linea in textBox1.Lines)
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

                if (radioButton1.Checked)
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

                prefijo = checkBox1.Checked ? prefijoPredict : string.Empty;

                if (!hubodirectorio)
                {
                    MessageBox.Show("Error - No se encontró directorio válido para el archivo: " + linea);
                    continue;
                }

                if (linea.Contains("*") || linea.Contains("?"))
                {
                    DescargarPorPatron(linea.Trim(), prefijo, ref numlineasOK);
                    continue;
                }

                string archivoSimple = linea.Trim();
                string rutaRemotaCompleta = directorio + "/" + archivoSimple;
                try
                {
                    if (!client.Exists(rutaRemotaCompleta))
                    {
                        MessageBox.Show("No existe el archivo: " + archivoSimple);
                        continue;
                    }

                    var atributos = client.GetAttributes(rutaRemotaCompleta);
                    localFilePath = Path.Combine(txtDir.Text, prefijo + archivoSimple);
                    using (Stream filestream = File.Create(localFilePath))
                    {
                        ulong totalBytesDownloaded = 0;
                        client.DownloadFile(rutaRemotaCompleta, filestream, bytesRead =>
                        {
                            if (cancelarDescarga)
                            {
                                return;
                            }

                            totalBytesDownloaded += bytesRead;
                            Invoke((Action)(() =>
                            {
                                progressBar1.Visible = true;
                                progressBar1.Style = ProgressBarStyle.Continuous;
                                progressBar1.Maximum = (int)Math.Max(1, (long)atributos.Size);
                                progressBar1.Value = (int)Math.Min(totalBytesDownloaded, (ulong)progressBar1.Maximum);
                                label5.Text = archivoSimple;
                            }));
                        });
                    }

                    numlineasOK++;
                }
                catch
                {
                    MessageBox.Show("Error al descargar el archivo: " + linea);
                }
                finally
                {
                    Invoke((Action)(() =>
                    {
                        progressBar1.Visible = false;
                        label5.Text = string.Empty;
                    }));
                }
            }

            Invoke((Action)(() =>
            {
                if (numlineasOK == numlineas)
                {
                    MessageBox.Show(numlineas == 1 ? "Archivo descargado correctmente" : "Todos los archivos fueron descargados correctamente");
                }
                else if (numlineas > 0)
                {
                    MessageBox.Show("Hubo error en la descarga de 1 o más componentes");
                }
            }));
        }

        private void DescargarPorPatron(string patron, string prefijo, ref int numlineasOK)
        {
            if (patron == "*" || patron == "?")
            {
                MessageBox.Show("Nombre incorrecto del archivo a descargar: * ?");
                return;
            }

            int numarchivosmatch = 0;
            string regexPattern = "^" + Regex.Escape(patron).Replace("\\*", ".*").Replace("\\?", ".") + "$";

            try
            {
                List<ISftpFile> archivos = ObtenerArchivosDelDirectorio(client, directorio);
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

                    string localFilePath = Path.Combine(txtDir.Text, prefijo + archivo.Name);
                    using (Stream filestream = File.Create(localFilePath))
                    {
                        ulong totalBytesDownloaded = 0;
                        client.DownloadFile(directorio + "/" + archivo.Name, filestream, bytesRead =>
                        {
                            if (cancelarDescarga)
                            {
                                return;
                            }

                            totalBytesDownloaded += bytesRead;
                            Invoke((Action)(() =>
                            {
                                progressBar1.Visible = true;
                                progressBar1.Style = ProgressBarStyle.Continuous;
                                progressBar1.Maximum = (int)Math.Max(1, (long)archivo.Length);
                                progressBar1.Value = (int)Math.Min(totalBytesDownloaded, (ulong)progressBar1.Maximum);
                                label5.Text = archivo.Name;
                            }));
                        });
                    }

                    Invoke((Action)(() =>
                    {
                        progressBar1.Visible = false;
                        label5.Text = string.Empty;
                    }));
                }

                if (numarchivosmatch == 0)
                {
                    MessageBox.Show("No se encontró el archivo: " + patron);
                }
                else
                {
                    numlineasOK++;
                }
            }
            catch
            {
                MessageBox.Show("Error al descargar el archivo: " + patron);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cancelarDescarga = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cacheDirectorios.Clear();
            MessageBox.Show("Cache de directorios limpiado");
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
                MessageBox.Show("No se han escrito los elementos para descargar");
                return;
            }

            if (txtDir.Text.Length == 0)
            {
                MessageBox.Show("Selecciona un directorio :)");
                return;
            }

            string rutaCompletaRemota = textBox3.Text.Trim();
            string rutaCompletaLocal = txtDir.Text.Trim();

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
                textBox3.Clear();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button6_Click(sender, e);
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Up)
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

                textBox3.Text = commandHistory[historyIndex];
                textBox3.SelectionStart = textBox3.Text.Length;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (commandHistory.Count == 0)
                {
                    return;
                }

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

        private void DescargaManual(string rutaEntradaRemota, string rutaSalidaLocal)
        {
            descargaCorrecta = false;
            string archivo = Path.GetFileName(rutaEntradaRemota);
            string localFilePath = Path.Combine(rutaSalidaLocal, archivo);

            try
            {
                if (!client.Exists(rutaEntradaRemota))
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

        public void DesactivarBotones()
        {
            if (client == null || !client.IsConnected)
            {
                cancelarDescarga = true;
                button2.Enabled = true;
                button2.BackColor = SystemColors.ControlLight;
            }
            else
            {
                cancelarDescarga = false;
                button2.Enabled = false;
                button2.BackColor = SystemColors.ActiveBorder;
            }

            button1.Enabled = false;
            button1.BackColor = SystemColors.ActiveBorder;
            button3.Enabled = false;
            button3.BackColor = SystemColors.ActiveBorder;
            button5.Enabled = false;
            button5.BackColor = SystemColors.ActiveBorder;
            button6.Enabled = false;
            button6.BackColor = SystemColors.ActiveBorder;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            checkBox1.Enabled = false;
            textBox3.Enabled = false;
        }

        public void ActivarBotones()
        {
            if (client == null || !client.IsConnected)
            {
                cancelarDescarga = true;
                button2.Enabled = true;
                button2.BackColor = SystemColors.ControlLight;
                button3.Enabled = false;
                button3.BackColor = SystemColors.ActiveBorder;
                button5.Enabled = false;
                button5.BackColor = SystemColors.ActiveBorder;
                button6.Enabled = false;
                button6.BackColor = SystemColors.ActiveBorder;
            }
            else
            {
                cancelarDescarga = false;
                button2.Enabled = false;
                button2.BackColor = SystemColors.ActiveBorder;
                button3.Enabled = true;
                button3.BackColor = SystemColors.ControlLight;
                button5.Enabled = true;
                button5.BackColor = SystemColors.ControlLight;
                button6.Enabled = true;
                button6.BackColor = SystemColors.ControlLight;
            }

            button1.Enabled = true;
            button1.BackColor = SystemColors.ControlLight;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            checkBox1.Enabled = true;
            textBox3.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (client == null || !client.IsConnected)
            {
                timer1.Stop();
                cancelarDescarga = true;
                label5.Text = string.Empty;
                label3.Text = "DESCONECTADO";
                label3.BackColor = Color.Tomato;
                button2.Enabled = true;
                button2.BackColor = SystemColors.ControlLight;
                button3.Enabled = false;
                button3.BackColor = SystemColors.ActiveBorder;
                button5.Enabled = false;
                button5.BackColor = SystemColors.ActiveBorder;
                button6.Enabled = false;
                button6.BackColor = SystemColors.ActiveBorder;
                MessageBox.Show("SE PERDIO LA CONEXION CON EL SERVIDOR");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

