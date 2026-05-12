using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace File_Wizard
{
    public partial class QaDownloadForm : Form
    {
        private bool cancelarDescarga = false;
        private bool descargaCorrecta = false;
        private string rutaLocal = @"C:";
        private List<string> commandHistory = new List<string>();
        private int historyIndex = -1;
        public QaDownloadForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openD = new FolderBrowserDialog();
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
                client = new SftpClient("172.21.200.16", "0satnexc", "0satnexc");
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

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
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
            if (radioButton4.Checked && string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Escribir por favor la ruta remota :)");
                return;
            }
            cancelarDescarga = false;
            DesactivarBotones();
            Thread descargaThread = new Thread(() =>
            {
                DescargarArchivos();
                this.Invoke((Action)(() =>
                {
                    ActivarBotones();
                }));
            });
            descargaThread.Start();
        }

        private List<ISftpFile> ObtenerArchivosDelDirectorio(
                SftpClient client,
                string remoteDirectory)
        {
            // 🔹 ¿Ya está en cache?
            if (cacheDirectorios.ContainsKey(remoteDirectory))
            {
                return cacheDirectorios[remoteDirectory];
            }

            // 🔹 No está → listar una sola vez
            var archivos = client
                .ListDirectory(remoteDirectory)
                .Where(f => f.IsRegularFile)
                .ToList();

            // 🔹 Guardar en cache
            cacheDirectorios.Add(remoteDirectory, archivos);

            return archivos;
        }

        private void DescargarArchivos()
        {
            int numlineas = 0;
            int numlineasOK = 0;
            foreach (string linea in textBox1.Lines)
            {
                if (!string.IsNullOrWhiteSpace(linea))
                {
                    numlineas++;
                    string extension = Path.GetExtension(linea.Trim());
                    string localFilePath;
                    string prefijoPredict = string.Empty;
                    string prefijo = string.Empty;
                    bool hubodirectorio = true;
                    if (radioButton1.Checked)
                    {
                        prefijoPredict = "QA-";

                        switch (extension)
                        {
                            case ".sh":
                                directorio = "/satqanexc/cad";
                                break;
                            case ".cbl":
                            case ".pco":
                                directorio = "/satqanexc/src";
                                break;
                            case ".scl":
                                directorio = "/satqanexc/cad/scl";
                                break;
                            case ".fact":
                                directorio = "/satqanexc/adm/fact";
                                break;
                            case ".sql":
                                directorio = "/satqanexc/adm/sql";
                                break;
                            case "":
                                directorio = "/satqanexc/cpy";
                                break;
                            default:
                                hubodirectorio = false;
                                break;
                        }
                    }
                    if (radioButton2.Checked)
                    {
                        prefijoPredict = "QA-COMUNIT-";

                        switch (extension)
                        {
                            case ".sh":
                                directorio = "/satqanexus/cad";
                                break;
                            case ".cbl":
                            case ".pco":
                                directorio = "/satqanexus/src";
                                break;
                            case ".scl":
                                directorio = "/satqanexus/cad/scl";
                                break;
                            case ".fact":
                                directorio = "/satqanexus/adm/fact";
                                break;
                            case ".sql":
                                directorio = "/satqanexus/adm/sql";
                                break;
                            case "":
                                directorio = "/satqanexus/cpy";
                                break;
                            default:
                                hubodirectorio = false;
                                break;
                        }
                    }
                    if (radioButton3.Checked)
                    {
                        prefijoPredict = "QA-BCHILE-";

                        switch (extension)
                        {
                            case ".sh":
                                directorio = "/satbcnxqa/cad";
                                break;
                            case ".cbl":
                            case ".pco":
                                directorio = "/satbcnxqa/src";
                                break;
                            case ".scl":
                                directorio = "/satbcnxqa/cad/scl";
                                break;
                            case ".fact":
                                directorio = "/satbcnxqa/adm/fact";
                                break;
                            case ".sql":
                                directorio = "/satbcnxqa/adm/sql";
                                break;
                            case "":
                                directorio = "/satbcnxqa/cpy";
                                break;
                            default:
                                hubodirectorio = false;
                                break;
                        }
                    }
                    if (radioButton4.Checked)
                    {
                        prefijoPredict = "QAX-";
                        switch (extension)
                        {
                            case ".sh":
                                directorio = "/" + textBox2.Text.Trim() + "/cad";
                                break;
                            case ".cbl":
                            case ".pco":
                                directorio = "/" + textBox2.Text.Trim() + "/src";
                                break;
                            case ".scl":
                                directorio = "/" + textBox2.Text.Trim() + "/cad/scl";
                                break;
                            case ".fact":
                                directorio = "/" + textBox2.Text.Trim() + "/adm/fact";
                                break;
                            case ".sql":
                                directorio = "/" + textBox2.Text.Trim() + "/adm/sql";
                                break;
                            case "":
                                directorio = "/" + textBox2.Text.Trim() + "/cpy";
                                break;
                            default:
                                hubodirectorio = false;
                                break;
                        }
                    }
                    if (checkBox1.Checked)
                    {
                        prefijo = prefijoPredict;
                    }
                    else
                    {
                        prefijo = string.Empty;
                    }
                    if (hubodirectorio)
                    {
                        Invoke((Action)(() =>
                        {
                            progressBar1.Style = ProgressBarStyle.Continuous;
                            progressBar1.Maximum = 100;
                            progressBar1.Value = 0;
                            progressBar1.Visible = true;
                            label5.Text = linea;
                        }));
                        if (!linea.Contains("*") && !linea.Contains("?"))
                        {
                            if (cancelarDescarga)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    MessageBox.Show(this, "Descarga cancelada.");
                                });
                                Invoke((Action)(() =>
                                {
                                    progressBar1.Visible = false;
                                    label5.Text = string.Empty;
                                }));
                                return;
                            }
                            string archivoSimple = linea.Trim();
                            string rutaRemotaCompleta = directorio + "/" + archivoSimple;
                            try
                            {
                                if (!client.Exists(rutaRemotaCompleta))
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        MessageBox.Show(this, "No existe el archivo: " + archivoSimple);
                                    });
                                    Invoke((Action)(() =>
                                    {
                                        progressBar1.Visible = false;
                                        label5.Text = string.Empty;
                                    }));
                                    return;
                                }
                                var atributos = client.GetAttributes(rutaRemotaCompleta);
                                localFilePath = Path.Combine(txtDir.Text, prefijo + archivoSimple);
                                Invoke((Action)(() =>
                                {
                                    progressBar1.Style = ProgressBarStyle.Continuous;
                                    progressBar1.Maximum = (int)atributos.Size;
                                    progressBar1.Value = 0;
                                    progressBar1.Visible = true;
                                    label5.Text = archivoSimple;
                                }));
                                using (Stream filestream = File.Create(localFilePath))
                                {
                                    ulong totalBytesDownloaded = 0;
                                    client.DownloadFile(rutaRemotaCompleta, filestream, (bytesRead) =>
                                    {
                                        if (cancelarDescarga)
                                        {
                                            this.Invoke((MethodInvoker)delegate
                                            {
                                                MessageBox.Show(this, "Descarga cancelada.");
                                            });
                                            Invoke((Action)(() =>
                                            {
                                                progressBar1.Visible = false;
                                                label5.Text = string.Empty;
                                            }));
                                            return;
                                        }

                                        totalBytesDownloaded += bytesRead;

                                        // Actualizar la barra de progreso en la UI
                                        Invoke((Action)(() =>
                                        {
                                            if (totalBytesDownloaded > (ulong)progressBar1.Maximum)
                                            {
                                                progressBar1.Value = progressBar1.Maximum;
                                            }
                                            else
                                            {
                                                progressBar1.Value = (int)totalBytesDownloaded;
                                            }
                                        }));
                                    });
                                }
                                Invoke((Action)(() =>
                                {
                                    progressBar1.Visible = false;
                                    label5.Text = string.Empty;
                                }));
                                numlineasOK++;
                            }
                            catch
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    MessageBox.Show(this, "Error al descargar el archivo: " + linea);
                                });
                                Invoke((Action)(() =>
                                {
                                    progressBar1.Visible = false;
                                    label5.Text = string.Empty;
                                }));
                            }
                        }
                        else
                        {
                            int numarchivosmatch = 0;
                            string patron = linea.Trim();
                            if (patron == "*" || patron == "?")
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    MessageBox.Show(this, "Nombre incorrecto del archivo a descargar: * ?");
                                });
                                Invoke((Action)(() =>
                                {
                                    progressBar1.Visible = false;
                                    label5.Text = string.Empty;
                                }));
                                return;
                            }
                            string regexPattern = "^" + Regex.Escape(patron).Replace("\\*", ".*").Replace("\\?", ".") + "$";
                            try
                            {
                                List<ISftpFile> archivos = ObtenerArchivosDelDirectorio(client, directorio);
                                foreach (var archivo in archivos)
                                {
                                    if (System.Text.RegularExpressions.Regex.IsMatch(archivo.Name, regexPattern))
                                    {
                                        numarchivosmatch++;
                                        if (cancelarDescarga)
                                        {
                                            this.Invoke((MethodInvoker)delegate
                                            {
                                                MessageBox.Show(this, "Descarga cancelada.");
                                            });
                                            Invoke((Action)(() =>
                                            {
                                                progressBar1.Visible = false;
                                                label5.Text = string.Empty;
                                            }));
                                            return;
                                        }
                                        localFilePath = Path.Combine(txtDir.Text, prefijo + archivo.Name);
                                        Invoke((Action)(() =>
                                        {
                                            progressBar1.Style = ProgressBarStyle.Continuous;
                                            progressBar1.Maximum = (int)archivo.Length;
                                            progressBar1.Value = 0;
                                            progressBar1.Visible = true;
                                            label5.Text = archivo.Name;
                                        }));
                                        using (Stream filestream = File.Create(localFilePath))
                                        {
                                            ulong totalBytesDownloaded = 0;
                                            client.DownloadFile(directorio + "/" + archivo.Name, filestream, (bytesRead) =>
                                            {
                                                if (cancelarDescarga)
                                                {
                                                    this.Invoke((MethodInvoker)delegate
                                                    {
                                                        MessageBox.Show(this, "Descarga cancelada.");
                                                    });
                                                    Invoke((Action)(() =>
                                                    {
                                                        progressBar1.Visible = false;
                                                        label5.Text = string.Empty;
                                                    }));
                                                    return;
                                                }

                                                totalBytesDownloaded += bytesRead;

                                                // Actualizar la barra de progreso en la UI
                                                Invoke((Action)(() =>
                                                {
                                                    if (totalBytesDownloaded > (ulong)progressBar1.Maximum)
                                                    {
                                                        progressBar1.Value = progressBar1.Maximum;
                                                    }
                                                    else
                                                    {
                                                        progressBar1.Value = (int)totalBytesDownloaded;
                                                    }
                                                }));
                                            });
                                        }
                                        Invoke((Action)(() =>
                                        {
                                            progressBar1.Visible = false;
                                            label5.Text = string.Empty;
                                        }));
                                    }
                                }
                                if (numarchivosmatch == 0)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        MessageBox.Show(this, "No se encontró el archivo: " + linea);
                                    });
                                    Invoke((Action)(() =>
                                    {
                                        progressBar1.Visible = false;
                                        label5.Text = string.Empty;
                                    }));
                                }
                                else
                                {
                                    numlineasOK++;
                                }
                            }
                            catch
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    MessageBox.Show(this, "Error al descargar el archivo: " + linea);
                                });
                                Invoke((Action)(() =>
                                {
                                    progressBar1.Visible = false;
                                    label5.Text = string.Empty;
                                }));
                            }
                        }
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            MessageBox.Show(this, "Error - No se encontró directorio válido para el archivo: " + linea);
                        });
                    }
                }
            }

            if (numlineasOK == numlineas)
            {
                if (numlineas == 1)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        MessageBox.Show(this, "Archivo descargado correctmente");
                    });
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        MessageBox.Show(this, "Todos los archivos fueron descargados correctamente");
                    });
                }
            }
            else
            {
                this.Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show(this, "Hubo error en la descarga de 1 o más componentes");
                });
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
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
                    MessageBox.Show("SE PERDIÓ LA CONEXION CON EL SERVIDOR");
                }
            }
            catch
            {
                timer1.Stop();
                MessageBox.Show("ERROR AL VERIFICAR LA CONEXION CON EL SERVIDOR");
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

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                textBox2.Text = "Ejem: satitanxqa (sin barras)";
                textBox2.Visible = true;
            }
            else
            {
                textBox2.Visible = false;
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

            if (rutaCompletaRemota.Contains("/") && rutaCompletaRemota.StartsWith("/") && !rutaCompletaRemota.EndsWith("/")
                && !rutaCompletaRemota.Contains("*") && !rutaCompletaRemota.Contains("?"))
            {
                DescargaManual(rutaCompletaRemota, rutaCompletaLocal);
                if (descargaCorrecta)
                {
                    commandHistory.Add(rutaCompletaRemota);
                    historyIndex = commandHistory.Count;
                    textBox3.Clear();
                }
            }
            else
            {
                MessageBox.Show("La ruta del fichero a descargar no es correcta");
                return;
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
                    MessageBox.Show("No se han escrito los elementos para descargar");
                    return;
                }
                if (txtDir.Text.Length == 0)
                {
                    MessageBox.Show("Selecciona un directorio :)");
                    return;
                }
                string rutaCompletaLocal = txtDir.Text.Trim();
                string rutaCompletaRemota = textBox3.Text.Trim();
                if (rutaCompletaRemota.Contains("/") && rutaCompletaRemota.StartsWith("/") && !rutaCompletaRemota.EndsWith("/")
                    && !rutaCompletaRemota.Contains("*") && !rutaCompletaRemota.Contains("?"))
                {
                    DescargaManual(rutaCompletaRemota, rutaCompletaLocal);
                    if (descargaCorrecta)
                    {
                        e.SuppressKeyPress = true;
                        commandHistory.Add(rutaCompletaRemota);
                        historyIndex = commandHistory.Count;
                        textBox3.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("La ruta del fichero a descargar no es correcta");
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

        private void DescargaManual(string rutaEntradaRemota, string rutaSalidaLocal)
        {
            string archivo = Path.GetFileName(rutaEntradaRemota);
            string localFilePath = Path.Combine(rutaSalidaLocal, archivo);
            try
            {
                if (!client.Exists(rutaEntradaRemota))
                {
                    MessageBox.Show("No existe el archivo: " + archivo);
                    descargaCorrecta = false;
                    return;
                }
                var attrs = client.GetAttributes(rutaEntradaRemota);
                if (attrs.IsDirectory)
                {
                    MessageBox.Show("Error - El texto ingresado es un directorio: " + rutaEntradaRemota);
                    descargaCorrecta = false;
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
                descargaCorrecta = false;
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
            radioButton3.Enabled = false;
            radioButton4.Enabled = false;
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
            radioButton3.Enabled = true;
            radioButton4.Enabled = true;
            textBox3.Enabled = true;
        }
    }
}
