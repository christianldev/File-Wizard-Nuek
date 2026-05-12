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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DesaTestDownloadForm));
            this.headerPanel = new System.Windows.Forms.Panel();
            this.sectionTitleLabel = new System.Windows.Forms.Label();
            this.sectionSubtitleLabel = new System.Windows.Forms.Label();
            this.connectionBadge = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.leftCard = new System.Windows.Forms.Panel();
            this.rightCard = new System.Windows.Forms.Panel();
            this.bottomCard = new System.Windows.Forms.Panel();
            this.leftCardHeader = new System.Windows.Forms.Label();
            this.remoteHintLabel = new System.Windows.Forms.Label();
            this.connectionStatusTitle = new System.Windows.Forms.Label();
            this.manualDownloadTitle = new System.Windows.Forms.Label();
            this.progressTitle = new System.Windows.Forms.Label();
            this.environmentTitle = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.txtDir = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.headerPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.leftCard.SuspendLayout();
            this.rightCard.SuspendLayout();
            this.bottomCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.headerPanel.Controls.Add(this.connectionBadge);
            this.headerPanel.Controls.Add(this.sectionSubtitleLabel);
            this.headerPanel.Controls.Add(this.sectionTitleLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(676, 98);
            this.headerPanel.TabIndex = 0;
            // 
            // sectionTitleLabel
            // 
            this.sectionTitleLabel.AutoSize = true;
            this.sectionTitleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 23.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sectionTitleLabel.ForeColor = System.Drawing.Color.White;
            this.sectionTitleLabel.Location = new System.Drawing.Point(22, 14);
            this.sectionTitleLabel.Name = "sectionTitleLabel";
            this.sectionTitleLabel.Size = new System.Drawing.Size(361, 42);
            this.sectionTitleLabel.TabIndex = 0;
            this.sectionTitleLabel.Text = "DESA-TEST Downloader";
            // 
            // sectionSubtitleLabel
            // 
            this.sectionSubtitleLabel.AutoSize = true;
            this.sectionSubtitleLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sectionSubtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(205)))), ((int)(((byte)(228)))));
            this.sectionSubtitleLabel.Location = new System.Drawing.Point(26, 58);
            this.sectionSubtitleLabel.Name = "sectionSubtitleLabel";
            this.sectionSubtitleLabel.Size = new System.Drawing.Size(408, 19);
            this.sectionSubtitleLabel.TabIndex = 1;
            this.sectionSubtitleLabel.Text = "Busca, descarga y administra componentes del entorno DESA-TEST";
            // 
            // connectionBadge
            // 
            this.connectionBadge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.connectionBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.connectionBadge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.connectionBadge.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectionBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(197)))), ((int)(((byte)(253)))));
            this.connectionBadge.Location = new System.Drawing.Point(515, 30);
            this.connectionBadge.Name = "connectionBadge";
            this.connectionBadge.Size = new System.Drawing.Size(136, 32);
            this.connectionBadge.TabIndex = 2;
            this.connectionBadge.Text = "DESA-TEST";
            this.connectionBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.mainPanel.Controls.Add(this.bottomCard);
            this.mainPanel.Controls.Add(this.rightCard);
            this.mainPanel.Controls.Add(this.leftCard);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 98);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(18, 18, 18, 18);
            this.mainPanel.Size = new System.Drawing.Size(860, 522);
            this.mainPanel.TabIndex = 1;
            // 
            // leftCard
            // 
            this.leftCard.BackColor = System.Drawing.Color.White;
            this.leftCard.Controls.Add(this.label1);
            this.leftCard.Controls.Add(this.button1);
            this.leftCard.Controls.Add(this.txtDir);
            this.leftCard.Controls.Add(this.label2);
            this.leftCard.Controls.Add(this.textBox1);
            this.leftCard.Controls.Add(this.label3);
            this.leftCard.Controls.Add(this.button2);
            this.leftCard.Controls.Add(this.groupBox1);
            this.leftCard.Location = new System.Drawing.Point(18, 18);
            this.leftCard.Name = "leftCard";
            this.leftCard.Padding = new System.Windows.Forms.Padding(16);
            this.leftCard.Size = new System.Drawing.Size(400, 230);
            this.leftCard.TabIndex = 0;
            // 
            // rightCard
            // 
            this.rightCard.BackColor = System.Drawing.Color.White;
            this.rightCard.Controls.Add(this.pictureBox1);
            this.rightCard.Controls.Add(this.label4);
            this.rightCard.Controls.Add(this.progressBar1);
            this.rightCard.Controls.Add(this.label5);
            this.rightCard.Controls.Add(this.button5);
            this.rightCard.Controls.Add(this.button3);
            this.rightCard.Controls.Add(this.button4);
            this.rightCard.Location = new System.Drawing.Point(432, 18);
            this.rightCard.Name = "rightCard";
            this.rightCard.Padding = new System.Windows.Forms.Padding(16);
            this.rightCard.Size = new System.Drawing.Size(410, 230);
            this.rightCard.TabIndex = 1;
            // 
            // bottomCard
            // 
            this.bottomCard.BackColor = System.Drawing.Color.White;
            this.bottomCard.Controls.Add(this.manualDownloadTitle);
            this.bottomCard.Controls.Add(this.progressTitle);
            this.bottomCard.Controls.Add(this.environmentTitle);
            this.bottomCard.Controls.Add(this.connectionStatusTitle);
            this.bottomCard.Controls.Add(this.checkBox1);
            this.bottomCard.Controls.Add(this.label6);
            this.bottomCard.Controls.Add(this.label7);
            this.bottomCard.Controls.Add(this.textBox3);
            this.bottomCard.Controls.Add(this.button6);
            this.bottomCard.Controls.Add(this.leftCardHeader);
            this.bottomCard.Controls.Add(this.remoteHintLabel);
            this.bottomCard.Location = new System.Drawing.Point(18, 260);
            this.bottomCard.Name = "bottomCard";
            this.bottomCard.Padding = new System.Windows.Forms.Padding(16);
            this.bottomCard.Size = new System.Drawing.Size(824, 240);
            this.bottomCard.TabIndex = 2;
            this.bottomCard.Visible = true;
            // 
            // leftCardHeader
            // 
            this.leftCardHeader.AutoSize = true;
            this.leftCardHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftCardHeader.Location = new System.Drawing.Point(16, 16);
            this.leftCardHeader.Name = "leftCardHeader";
            this.leftCardHeader.Size = new System.Drawing.Size(0, 19);
            this.leftCardHeader.TabIndex = 0;
            // 
            // remoteHintLabel
            // 
            this.remoteHintLabel.AutoSize = true;
            this.remoteHintLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remoteHintLabel.Location = new System.Drawing.Point(16, 39);
            this.remoteHintLabel.Name = "remoteHintLabel";
            this.remoteHintLabel.Size = new System.Drawing.Size(0, 15);
            this.remoteHintLabel.TabIndex = 1;
            // 
            // connectionStatusTitle
            // 
            this.connectionStatusTitle.AutoSize = true;
            this.connectionStatusTitle.Font = new System.Drawing.Font("Segoe UI", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectionStatusTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.connectionStatusTitle.Location = new System.Drawing.Point(16, 16);
            this.connectionStatusTitle.Name = "connectionStatusTitle";
            this.connectionStatusTitle.Size = new System.Drawing.Size(0, 15);
            this.connectionStatusTitle.TabIndex = 2;
            // 
            // manualDownloadTitle
            // 
            this.manualDownloadTitle.AutoSize = true;
            this.manualDownloadTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manualDownloadTitle.Location = new System.Drawing.Point(16, 16);
            this.manualDownloadTitle.Name = "manualDownloadTitle";
            this.manualDownloadTitle.Size = new System.Drawing.Size(0, 20);
            this.manualDownloadTitle.TabIndex = 3;
            // 
            // progressTitle
            // 
            this.progressTitle.AutoSize = true;
            this.progressTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressTitle.Location = new System.Drawing.Point(16, 16);
            this.progressTitle.Name = "progressTitle";
            this.progressTitle.Size = new System.Drawing.Size(0, 20);
            this.progressTitle.TabIndex = 4;
            // 
            // environmentTitle
            // 
            this.environmentTitle.AutoSize = true;
            this.environmentTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.environmentTitle.Location = new System.Drawing.Point(16, 16);
            this.environmentTitle.Name = "environmentTitle";
            this.environmentTitle.Size = new System.Drawing.Size(0, 17);
            this.environmentTitle.TabIndex = 5;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.textBox3.Location = new System.Drawing.Point(19, 56);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(650, 23);
            this.textBox3.TabIndex = 48;
            this.textBox3.Text = "/sat/cdp/desa/dat/MPJ01010.FLOG0064";
            this.textBox3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox3_KeyDown);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(111)))), ((int)(((byte)(235)))));
            this.button6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button6.Enabled = false;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(686, 50);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(120, 34);
            this.button6.TabIndex = 47;
            this.button6.Text = "DESCARGAR";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.label7.Location = new System.Drawing.Point(19, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(520, 24);            this.label7.TabIndex = 45;
            this.label7.Text = "(escribir ruta completa y nombre del archivo)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.label6.Location = new System.Drawing.Point(19, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(172, 24);
            this.label6.TabIndex = 46;
            this.label6.Text = "Descarga manual";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = global::File_Wizard.Properties.Resources.pug_sad_ezgif_com_effects;
            this.pictureBox1.Location = new System.Drawing.Point(256, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(138, 110);            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 44;
            this.pictureBox1.TabStop = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(16, 188);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(378, 23);            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 43;
            this.progressBar1.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(256, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 16);            this.label3.TabIndex = 42;
            this.label3.Text = "DESCONECTADO";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(16, 92);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(224, 120);            this.textBox1.TabIndex = 41;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.groupBox1.Location = new System.Drawing.Point(256, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(120, 100);            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "AMBIENTE";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(10, 58);            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(96, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "SATCDPTEST";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(10, 28);            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(97, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.Text = "SATCDPDESA";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // txtDir
            // 
            this.txtDir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDir.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDir.Location = new System.Drawing.Point(16, 26);
            this.txtDir.Multiline = true;
            this.txtDir.Name = "txtDir";
            this.txtDir.ReadOnly = true;
            this.txtDir.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDir.Size = new System.Drawing.Size(320, 28);            this.txtDir.TabIndex = 39;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.button5.Enabled = false;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Segoe UI Semibold", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(16, 147);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(120, 34);            this.button5.TabIndex = 35;
            this.button5.Text = "LIMPIAR CACHÉ";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(118)))), ((int)(((byte)(110)))));
            this.button3.Enabled = false;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe UI Semibold", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(142, 147);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(140, 34);            this.button3.TabIndex = 34;
            this.button3.Text = "DESCARGAR ARCHIVOS";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(288, 147);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(96, 34);            this.button4.TabIndex = 37;
            this.button4.Text = "CANCELAR";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(256, 160);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 28);            this.button2.TabIndex = 36;
            this.button2.Text = "CONECTAR";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.button1.Location = new System.Drawing.Point(344, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(38, 28);            this.button1.TabIndex = 38;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.label5.Location = new System.Drawing.Point(16, 160);            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 18);
            this.label5.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.label4.Location = new System.Drawing.Point(16, 110);            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 18);
            this.label4.TabIndex = 30;
            this.label4.Text = "Progreso>>";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.label2.Location = new System.Drawing.Point(16, 72);            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 24);
            this.label2.TabIndex = 32;
            this.label2.Text = "Archivos remotos:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.label1.Location = new System.Drawing.Point(16, 9);            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 24);
            this.label1.TabIndex = 33;
            this.label1.Text = "Directorio LOCAL:";
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Font = new System.Drawing.Font("Segoe UI", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(19, 130);            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(89, 20);
            this.checkBox1.TabIndex = 49;
            this.checkBox1.Text = "PREFIJO?";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // DesaTestDownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(676, 459);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.headerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DesaTestDownloadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DESA-TEST DOWNLOADER";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.leftCard.ResumeLayout(false);
            this.leftCard.PerformLayout();
            this.rightCard.ResumeLayout(false);
            this.rightCard.PerformLayout();
            this.bottomCard.ResumeLayout(false);
            this.bottomCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox1;
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
