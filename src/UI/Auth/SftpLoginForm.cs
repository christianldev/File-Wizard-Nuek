using File_Wizard.Infrastructure;
using Renci.SshNet;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace File_Wizard.UI.Auth
{
    public sealed class SftpLoginForm : Form
    {
        private readonly TextBox hostTextBox;
        private readonly NumericUpDown portNumericUpDown;
        private readonly TextBox usernameTextBox;
        private readonly TextBox passwordTextBox;
        private readonly Button loginButton;
        private readonly Button cancelButton;

        public SftpConnectionSettings ConnectionSettings { get; private set; }

        public SftpLoginForm()
        {
            Text = "Login SFTP";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(420, 280);
            BackColor = Color.FromArgb(245, 248, 252);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

            var titleLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(19, 28, 46),
                Location = new Point(24, 18),
                Text = "Acceso SFTP"
            };

            var subtitleLabel = new Label
            {
                AutoSize = true,
                ForeColor = Color.FromArgb(96, 108, 129),
                Location = new Point(24, 50),
                Text = "Ingresa las credenciales antes de abrir File Wizard"
            };

            var panel = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 4,
                Location = new Point(24, 86),
                Size = new Size(372, 134),
                AutoSize = false
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            panel.Controls.Add(new Label { Text = "Host", AutoSize = true, Anchor = AnchorStyles.Left, TextAlign = ContentAlignment.MiddleLeft }, 0, 0);
            panel.Controls.Add(new Label { Text = "Puerto", AutoSize = true, Anchor = AnchorStyles.Left, TextAlign = ContentAlignment.MiddleLeft }, 0, 1);
            panel.Controls.Add(new Label { Text = "Usuario", AutoSize = true, Anchor = AnchorStyles.Left, TextAlign = ContentAlignment.MiddleLeft }, 0, 2);
            panel.Controls.Add(new Label { Text = "Contraseña", AutoSize = true, Anchor = AnchorStyles.Left, TextAlign = ContentAlignment.MiddleLeft }, 0, 3);

            hostTextBox = new TextBox { Dock = DockStyle.Fill, Text = "10.22.98.131" };
            portNumericUpDown = new NumericUpDown { Dock = DockStyle.Fill, Minimum = 1, Maximum = 65535, Value = 22 };
            usernameTextBox = new TextBox { Dock = DockStyle.Fill, Text = "i584039" };
            passwordTextBox = new TextBox { Dock = DockStyle.Fill, UseSystemPasswordChar = true, Text = "I584039" };

            panel.Controls.Add(hostTextBox, 1, 0);
            panel.Controls.Add(portNumericUpDown, 1, 1);
            panel.Controls.Add(usernameTextBox, 1, 2);
            panel.Controls.Add(passwordTextBox, 1, 3);

            loginButton = new Button
            {
                Text = "Entrar",
                BackColor = Color.FromArgb(31, 111, 235),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(220, 234),
                Size = new Size(92, 30)
            };
            loginButton.FlatAppearance.BorderSize = 0;
            loginButton.Click += LoginButton_Click;

            cancelButton = new Button
            {
                Text = "Cancelar",
                DialogResult = DialogResult.Cancel,
                Location = new Point(314, 234),
                Size = new Size(82, 30)
            };

            AcceptButton = loginButton;
            CancelButton = cancelButton;

            Controls.Add(titleLabel);
            Controls.Add(subtitleLabel);
            Controls.Add(panel);
            Controls.Add(loginButton);
            Controls.Add(cancelButton);
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(hostTextBox.Text) || string.IsNullOrWhiteSpace(usernameTextBox.Text))
            {
                MessageBox.Show("Completa host y usuario.");
                return;
            }

            try
            {
                using (var client = new SftpClient(hostTextBox.Text.Trim(), (int)portNumericUpDown.Value, usernameTextBox.Text.Trim(), passwordTextBox.Text))
                {
                    client.ConnectionInfo.Timeout = TimeSpan.FromSeconds(8);
                    client.Connect();
                    if (!client.IsConnected)
                    {
                        MessageBox.Show("No fue posible validar la conexión SFTP.");
                        return;
                    }

                    client.Disconnect();
                }

                ConnectionSettings = new SftpConnectionSettings(
                    hostTextBox.Text.Trim(),
                    (int)portNumericUpDown.Value,
                    usernameTextBox.Text.Trim(),
                    passwordTextBox.Text);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar las credenciales SFTP: {ex.Message}");
            }
        }
    }
}