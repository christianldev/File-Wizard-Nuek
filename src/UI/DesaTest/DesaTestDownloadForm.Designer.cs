using Renci.SshNet;
using Renci.SshNet.Sftp;
using System.Collections.Generic;

namespace File_Wizard.UI.DesaTest
{
    partial class DesaTestDownloadForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            headerPanel = new System.Windows.Forms.Panel();
            connectionBadge = new System.Windows.Forms.Label();
            sectionSubtitleLabel = new System.Windows.Forms.Label();
            sectionTitleLabel = new System.Windows.Forms.Label();
            mainPanel = new System.Windows.Forms.Panel();
            bottomCard = new System.Windows.Forms.Panel();
            manualDownloadTitle = new System.Windows.Forms.Label();
            progressTitle = new System.Windows.Forms.Label();
            environmentTitle = new System.Windows.Forms.Label();
            connectionStatusTitle = new System.Windows.Forms.Label();
            checkBox1 = new System.Windows.Forms.CheckBox();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            textBox3 = new System.Windows.Forms.TextBox();
            button6 = new System.Windows.Forms.Button();
            leftCardHeader = new System.Windows.Forms.Label();
            remoteHintLabel = new System.Windows.Forms.Label();
            rightCard = new System.Windows.Forms.Panel();
            label4 = new System.Windows.Forms.Label();
            progressBar1 = new System.Windows.Forms.ProgressBar();
            label5 = new System.Windows.Forms.Label();
            button5 = new System.Windows.Forms.Button();
            button3 = new System.Windows.Forms.Button();
            button4 = new System.Windows.Forms.Button();
            leftCard = new System.Windows.Forms.Panel();
            label1 = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            txtDir = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            textBox1 = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            button2 = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            radioButton2 = new System.Windows.Forms.RadioButton();
            radioButton1 = new System.Windows.Forms.RadioButton();
            timer1 = new System.Windows.Forms.Timer(components);
            headerPanel.SuspendLayout();
            mainPanel.SuspendLayout();
            bottomCard.SuspendLayout();
            rightCard.SuspendLayout();
            leftCard.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // headerPanel
            // 
            headerPanel.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            headerPanel.Controls.Add(connectionBadge);
            headerPanel.Controls.Add(sectionSubtitleLabel);
            headerPanel.Controls.Add(sectionTitleLabel);
            headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            headerPanel.Location = new System.Drawing.Point(0, 0);
            headerPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new System.Drawing.Size(1168, 151);
            headerPanel.TabIndex = 0;
            // 
            // connectionBadge
            // 
            connectionBadge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            connectionBadge.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            connectionBadge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            connectionBadge.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            connectionBadge.ForeColor = System.Drawing.Color.FromArgb(147, 197, 253);
            connectionBadge.Location = new System.Drawing.Point(954, 46);
            connectionBadge.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            connectionBadge.Name = "connectionBadge";
            connectionBadge.Size = new System.Drawing.Size(181, 49);
            connectionBadge.TabIndex = 2;
            connectionBadge.Text = "DESA-TEST";
            connectionBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sectionSubtitleLabel
            // 
            sectionSubtitleLabel.AutoSize = true;
            sectionSubtitleLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            sectionSubtitleLabel.ForeColor = System.Drawing.Color.FromArgb(191, 205, 228);
            sectionSubtitleLabel.Location = new System.Drawing.Point(35, 89);
            sectionSubtitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            sectionSubtitleLabel.Name = "sectionSubtitleLabel";
            sectionSubtitleLabel.Size = new System.Drawing.Size(521, 23);
            sectionSubtitleLabel.TabIndex = 1;
            sectionSubtitleLabel.Text = "Busca, descarga y administra componentes del entorno DESA-TEST";
            // 
            // sectionTitleLabel
            // 
            sectionTitleLabel.AutoSize = true;
            sectionTitleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 23.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            sectionTitleLabel.ForeColor = System.Drawing.Color.White;
            sectionTitleLabel.Location = new System.Drawing.Point(29, 22);
            sectionTitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            sectionTitleLabel.Name = "sectionTitleLabel";
            sectionTitleLabel.Size = new System.Drawing.Size(445, 52);
            sectionTitleLabel.TabIndex = 0;
            sectionTitleLabel.Text = "DESA-TEST Downloader";
            // 
            // mainPanel
            // 
            mainPanel.BackColor = System.Drawing.Color.FromArgb(245, 247, 252);
            mainPanel.Controls.Add(bottomCard);
            mainPanel.Controls.Add(rightCard);
            mainPanel.Controls.Add(leftCard);
            mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mainPanel.Location = new System.Drawing.Point(0, 151);
            mainPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            mainPanel.Name = "mainPanel";
            mainPanel.Padding = new System.Windows.Forms.Padding(21, 25, 21, 25);
            mainPanel.Size = new System.Drawing.Size(1168, 872);
            mainPanel.TabIndex = 1;
            // 
            // bottomCard
            // 
            bottomCard.BackColor = System.Drawing.Color.White;
            bottomCard.Controls.Add(manualDownloadTitle);
            bottomCard.Controls.Add(progressTitle);
            bottomCard.Controls.Add(environmentTitle);
            bottomCard.Controls.Add(connectionStatusTitle);
            bottomCard.Controls.Add(label6);
            bottomCard.Controls.Add(label7);
            bottomCard.Controls.Add(textBox3);
            bottomCard.Controls.Add(button6);
            bottomCard.Controls.Add(leftCardHeader);
            bottomCard.Controls.Add(remoteHintLabel);
            bottomCard.Location = new System.Drawing.Point(21, 400);
            bottomCard.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            bottomCard.Name = "bottomCard";
            bottomCard.Padding = new System.Windows.Forms.Padding(21, 25, 21, 25);
            bottomCard.Size = new System.Drawing.Size(1104, 400);
            bottomCard.TabIndex = 2;
            // 
            // manualDownloadTitle
            // 
            manualDownloadTitle.AutoSize = true;
            manualDownloadTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            manualDownloadTitle.Location = new System.Drawing.Point(21, 25);
            manualDownloadTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            manualDownloadTitle.Name = "manualDownloadTitle";
            manualDownloadTitle.Size = new System.Drawing.Size(0, 25);
            manualDownloadTitle.TabIndex = 3;
            // 
            // progressTitle
            // 
            progressTitle.AutoSize = true;
            progressTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            progressTitle.Location = new System.Drawing.Point(21, 25);
            progressTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            progressTitle.Name = "progressTitle";
            progressTitle.Size = new System.Drawing.Size(0, 25);
            progressTitle.TabIndex = 4;
            // 
            // environmentTitle
            // 
            environmentTitle.AutoSize = true;
            environmentTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            environmentTitle.Location = new System.Drawing.Point(21, 25);
            environmentTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            environmentTitle.Name = "environmentTitle";
            environmentTitle.Size = new System.Drawing.Size(0, 21);
            environmentTitle.TabIndex = 5;
            // 
            // connectionStatusTitle
            // 
            connectionStatusTitle.AutoSize = true;
            connectionStatusTitle.Font = new System.Drawing.Font("Segoe UI", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            connectionStatusTitle.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            connectionStatusTitle.Location = new System.Drawing.Point(21, 25);
            connectionStatusTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            connectionStatusTitle.Name = "connectionStatusTitle";
            connectionStatusTitle.Size = new System.Drawing.Size(0, 21);
            connectionStatusTitle.TabIndex = 2;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox1.Font = new System.Drawing.Font("Segoe UI", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            checkBox1.Location = new System.Drawing.Point(8, 17);
            checkBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(96, 25);
            checkBox1.TabIndex = 49;
            checkBox1.Text = "PREFIJO?";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = System.Drawing.Color.Transparent;
            label6.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label6.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            label6.Location = new System.Drawing.Point(25, 25);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(160, 25);
            label6.TabIndex = 46;
            label6.Text = "Descarga manual";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = System.Drawing.Color.Transparent;
            label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label7.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            label7.Location = new System.Drawing.Point(25, 122);
            label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(308, 20);
            label7.TabIndex = 45;
            label7.Text = "(escribir ruta completa y nombre del archivo)";
            // 
            // textBox3
            // 
            textBox3.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            textBox3.ForeColor = System.Drawing.Color.FromArgb(30, 64, 175);
            textBox3.Location = new System.Drawing.Point(25, 86);
            textBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            textBox3.Name = "textBox3";
            textBox3.Size = new System.Drawing.Size(866, 27);
            textBox3.TabIndex = 48;
            textBox3.Text = "/sat/cdp/desa/dat/MPJ01010.FLOG0064";
            textBox3.KeyDown += textBox3_KeyDown;
            // 
            // button6
            // 
            button6.BackColor = System.Drawing.Color.FromArgb(31, 111, 235);
            button6.Cursor = System.Windows.Forms.Cursors.Hand;
            button6.Enabled = false;
            button6.FlatAppearance.BorderSize = 0;
            button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button6.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            button6.ForeColor = System.Drawing.Color.White;
            button6.Location = new System.Drawing.Point(915, 77);
            button6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button6.Name = "button6";
            button6.Size = new System.Drawing.Size(160, 52);
            button6.TabIndex = 47;
            button6.Text = "DESCARGAR";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // leftCardHeader
            // 
            leftCardHeader.AutoSize = true;
            leftCardHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            leftCardHeader.Location = new System.Drawing.Point(21, 25);
            leftCardHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            leftCardHeader.Name = "leftCardHeader";
            leftCardHeader.Size = new System.Drawing.Size(0, 23);
            leftCardHeader.TabIndex = 0;
            // 
            // remoteHintLabel
            // 
            remoteHintLabel.AutoSize = true;
            remoteHintLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            remoteHintLabel.Location = new System.Drawing.Point(21, 60);
            remoteHintLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            remoteHintLabel.Name = "remoteHintLabel";
            remoteHintLabel.Size = new System.Drawing.Size(0, 20);
            remoteHintLabel.TabIndex = 1;
            // 
            // rightCard
            // 
            rightCard.BackColor = System.Drawing.Color.White;
            rightCard.Controls.Add(label4);
            rightCard.Controls.Add(progressBar1);
            rightCard.Controls.Add(label5);
            rightCard.Controls.Add(button5);
            rightCard.Controls.Add(checkBox1);
            rightCard.Controls.Add(button3);
            rightCard.Controls.Add(button4);
            rightCard.Location = new System.Drawing.Point(571, 25);
            rightCard.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            rightCard.Name = "rightCard";
            rightCard.Padding = new System.Windows.Forms.Padding(21, 25, 21, 25);
            rightCard.Size = new System.Drawing.Size(549, 354);
            rightCard.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = System.Drawing.Color.Transparent;
            label4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label4.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            label4.Location = new System.Drawing.Point(8, 111);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(175, 23);
            label4.TabIndex = 30;
            label4.Text = "Progreso de descarga";
            // 
            // progressBar1
            // 
            progressBar1.ForeColor = System.Drawing.Color.FromArgb(31, 111, 235);
            progressBar1.Location = new System.Drawing.Point(8, 146);
            progressBar1.Margin = new System.Windows.Forms.Padding(0);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new System.Drawing.Size(504, 40);
            progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            progressBar1.TabIndex = 43;
            progressBar1.Visible = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = System.Drawing.Color.Transparent;
            label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label5.ForeColor = System.Drawing.Color.FromArgb(30, 64, 175);
            label5.Location = new System.Drawing.Point(21, 246);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(0, 23);
            label5.TabIndex = 31;
            // 
            // button5
            // 
            button5.BackColor = System.Drawing.Color.FromArgb(148, 163, 184);
            button5.Enabled = false;
            button5.FlatAppearance.BorderSize = 0;
            button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button5.Font = new System.Drawing.Font("Segoe UI Semibold", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            button5.ForeColor = System.Drawing.Color.White;
            button5.Location = new System.Drawing.Point(203, 217);
            button5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(160, 52);
            button5.TabIndex = 35;
            button5.Text = "LIMPIAR CACHÉ";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // button3
            // 
            button3.BackColor = System.Drawing.Color.FromArgb(15, 118, 110);
            button3.Enabled = false;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button3.Font = new System.Drawing.Font("Segoe UI Semibold", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            button3.ForeColor = System.Drawing.Color.White;
            button3.Location = new System.Drawing.Point(8, 217);
            button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(187, 52);
            button3.TabIndex = 34;
            button3.Text = "DESCARGAR ARCHIVOS";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackColor = System.Drawing.Color.FromArgb(239, 68, 68);
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            button4.ForeColor = System.Drawing.Color.White;
            button4.Location = new System.Drawing.Point(371, 217);
            button4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(128, 52);
            button4.TabIndex = 37;
            button4.Text = "CANCELAR";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // leftCard
            // 
            leftCard.BackColor = System.Drawing.Color.White;
            leftCard.Controls.Add(label1);
            leftCard.Controls.Add(button1);
            leftCard.Controls.Add(txtDir);
            leftCard.Controls.Add(label2);
            leftCard.Controls.Add(textBox1);
            leftCard.Controls.Add(label3);
            leftCard.Controls.Add(button2);
            leftCard.Controls.Add(groupBox1);
            leftCard.Location = new System.Drawing.Point(21, 25);
            leftCard.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            leftCard.Name = "leftCard";
            leftCard.Padding = new System.Windows.Forms.Padding(21, 25, 21, 25);
            leftCard.Size = new System.Drawing.Size(533, 354);
            leftCard.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label1.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            label1.Location = new System.Drawing.Point(21, 14);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(152, 25);
            label1.TabIndex = 33;
            label1.Text = "Directorio Local:";
            label1.Click += label1_Click;
            // 
            // button1
            // 
            button1.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            button1.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            button1.Location = new System.Drawing.Point(459, 40);
            button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(51, 43);
            button1.TabIndex = 38;
            button1.Text = "...";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // txtDir
            // 
            txtDir.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtDir.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            txtDir.Location = new System.Drawing.Point(21, 40);
            txtDir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtDir.Multiline = true;
            txtDir.Name = "txtDir";
            txtDir.ReadOnly = true;
            txtDir.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtDir.Size = new System.Drawing.Size(426, 42);
            txtDir.TabIndex = 39;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label2.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            label2.Location = new System.Drawing.Point(21, 111);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(167, 25);
            label2.TabIndex = 32;
            label2.Text = "Archivos remotos:";
            // 
            // textBox1
            // 
            textBox1.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            textBox1.Location = new System.Drawing.Point(21, 142);
            textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            textBox1.Size = new System.Drawing.Size(298, 184);
            textBox1.TabIndex = 41;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = System.Drawing.Color.FromArgb(248, 113, 113);
            label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label3.Location = new System.Drawing.Point(341, 300);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(132, 21);
            label3.TabIndex = 42;
            label3.Text = "DESCONECTADO";
            // 
            // button2
            // 
            button2.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            button2.ForeColor = System.Drawing.Color.White;
            button2.Location = new System.Drawing.Point(341, 246);
            button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(160, 43);
            button2.TabIndex = 36;
            button2.Text = "CONECTAR";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = System.Drawing.Color.White;
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            groupBox1.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            groupBox1.Location = new System.Drawing.Point(341, 89);
            groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            groupBox1.Size = new System.Drawing.Size(160, 154);
            groupBox1.TabIndex = 40;
            groupBox1.TabStop = false;
            groupBox1.Text = "AMBIENTE";
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Checked = true;
            radioButton2.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            radioButton2.Location = new System.Drawing.Point(13, 89);
            radioButton2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new System.Drawing.Size(114, 24);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "SATCDPTEST";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            radioButton1.Location = new System.Drawing.Point(13, 43);
            radioButton1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new System.Drawing.Size(119, 24);
            radioButton1.TabIndex = 0;
            radioButton1.Text = "SATCDPDESA";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            timer1.Interval = 5000;
            timer1.Tick += timer1_Tick;
            // 
            // DesaTestDownloadForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 252);
            ClientSize = new System.Drawing.Size(1168, 1023);
            Controls.Add(mainPanel);
            Controls.Add(headerPanel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            MaximizeBox = false;
            Name = "DesaTestDownloadForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "DESA-TEST DOWNLOADER";
            FormClosing += Form2_FormClosing;
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            mainPanel.ResumeLayout(false);
            bottomCard.ResumeLayout(false);
            bottomCard.PerformLayout();
            rightCard.ResumeLayout(false);
            rightCard.PerformLayout();
            leftCard.ResumeLayout(false);
            leftCard.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        public System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public SftpClient client;
        public string directorio = "/sat/cdp/desa/cpy";
        Dictionary<string, List<ISftpFile>> cacheDirectorios = new Dictionary<string, List<ISftpFile>>();
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label sectionTitleLabel;
        private System.Windows.Forms.Label sectionSubtitleLabel;
        private System.Windows.Forms.Label connectionBadge;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel leftCard;
        private System.Windows.Forms.Panel rightCard;
        private System.Windows.Forms.Panel bottomCard;
        private System.Windows.Forms.Label leftCardHeader;
        private System.Windows.Forms.Label remoteHintLabel;
        private System.Windows.Forms.Label connectionStatusTitle;
        private System.Windows.Forms.Label manualDownloadTitle;
        private System.Windows.Forms.Label progressTitle;
        private System.Windows.Forms.Label environmentTitle;
    }
}
