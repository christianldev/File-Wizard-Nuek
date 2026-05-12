namespace File_Wizard.UI.Forms
{
    partial class MainMenuForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenuForm));
            this.headerPanel = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.DTdownbutton = new System.Windows.Forms.Button();
            this.DTuploadbutton = new System.Windows.Forms.Button();
            this.QAdownbutton = new System.Windows.Forms.Button();
            this.QAuploadbutton = new System.Windows.Forms.Button();
            this.footerLabel = new System.Windows.Forms.Label();
            this.headerPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(28)))), ((int)(((byte)(46)))));
            this.headerPanel.Controls.Add(this.subtitleLabel);
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(720, 110);
            this.headerPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(28, 20);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(152, 45);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "File Wizard";
            // 
            // subtitleLabel
            // 
            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(188)))), ((int)(((byte)(210)))));
            this.subtitleLabel.Location = new System.Drawing.Point(31, 66);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new System.Drawing.Size(323, 19);
            this.subtitleLabel.TabIndex = 1;
            this.subtitleLabel.Text = "Transferencias organizadas por ambiente y operación";
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.mainPanel.Controls.Add(this.footerLabel);
            this.mainPanel.Controls.Add(this.QAuploadbutton);
            this.mainPanel.Controls.Add(this.DTuploadbutton);
            this.mainPanel.Controls.Add(this.QAdownbutton);
            this.mainPanel.Controls.Add(this.DTdownbutton);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 110);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(24, 24, 24, 18);
            this.mainPanel.Size = new System.Drawing.Size(720, 370);
            this.mainPanel.TabIndex = 1;
            // 
            // DTdownbutton
            // 
            this.DTdownbutton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(111)))), ((int)(((byte)(235)))));
            this.DTdownbutton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DTdownbutton.FlatAppearance.BorderSize = 0;
            this.DTdownbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DTdownbutton.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTdownbutton.ForeColor = System.Drawing.Color.White;
            this.DTdownbutton.Location = new System.Drawing.Point(24, 24);
            this.DTdownbutton.Margin = new System.Windows.Forms.Padding(0, 0, 16, 16);
            this.DTdownbutton.Name = "DTdownbutton";
            this.DTdownbutton.Size = new System.Drawing.Size(316, 108);
            this.DTdownbutton.TabIndex = 0;
            this.DTdownbutton.Text = "DESA-TEST DOWNLOADER\r\nDescargar archivos del entorno DESA-TEST";
            this.DTdownbutton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DTdownbutton.UseVisualStyleBackColor = true;
            this.DTdownbutton.Click += new System.EventHandler(this.DTdownbutton_Click);
            // 
            // DTuploadbutton
            // 
            this.DTuploadbutton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(163)))), ((int)(((byte)(137)))));
            this.DTuploadbutton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DTuploadbutton.FlatAppearance.BorderSize = 0;
            this.DTuploadbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DTuploadbutton.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTuploadbutton.ForeColor = System.Drawing.Color.White;
            this.DTuploadbutton.Location = new System.Drawing.Point(356, 24);
            this.DTuploadbutton.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.DTuploadbutton.Name = "DTuploadbutton";
            this.DTuploadbutton.Size = new System.Drawing.Size(316, 108);
            this.DTuploadbutton.TabIndex = 1;
            this.DTuploadbutton.Text = "DESA-TEST UPLOADER\r\nSubir archivos al entorno DESA-TEST";
            this.DTuploadbutton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DTuploadbutton.UseVisualStyleBackColor = true;
            this.DTuploadbutton.Click += new System.EventHandler(this.DTuploadbutton_Click);
            // 
            // QAdownbutton
            // 
            this.QAdownbutton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.QAdownbutton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.QAdownbutton.FlatAppearance.BorderSize = 0;
            this.QAdownbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.QAdownbutton.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QAdownbutton.ForeColor = System.Drawing.Color.White;
            this.QAdownbutton.Location = new System.Drawing.Point(24, 148);
            this.QAdownbutton.Margin = new System.Windows.Forms.Padding(0, 0, 16, 16);
            this.QAdownbutton.Name = "QAdownbutton";
            this.QAdownbutton.Size = new System.Drawing.Size(316, 108);
            this.QAdownbutton.TabIndex = 2;
            this.QAdownbutton.Text = "QA DOWNLOADER\r\nDescargar archivos de ambientes QA";
            this.QAdownbutton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.QAdownbutton.UseVisualStyleBackColor = true;
            this.QAdownbutton.Click += new System.EventHandler(this.QAdownbutton_Click);
            // 
            // QAuploadbutton
            // 
            this.QAuploadbutton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.QAuploadbutton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.QAuploadbutton.FlatAppearance.BorderSize = 0;
            this.QAuploadbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.QAuploadbutton.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QAuploadbutton.ForeColor = System.Drawing.Color.White;
            this.QAuploadbutton.Location = new System.Drawing.Point(356, 148);
            this.QAuploadbutton.Margin = new System.Windows.Forms.Padding(0);
            this.QAuploadbutton.Name = "QAuploadbutton";
            this.QAuploadbutton.Size = new System.Drawing.Size(316, 108);
            this.QAuploadbutton.TabIndex = 3;
            this.QAuploadbutton.Text = "QA UPLOADER\r\nSubir archivos a entornos QA";
            this.QAuploadbutton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.QAuploadbutton.UseVisualStyleBackColor = true;
            this.QAuploadbutton.Click += new System.EventHandler(this.QAuploadbutton_Click);
            // 
            // footerLabel
            // 
            this.footerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.footerLabel.AutoSize = true;
            this.footerLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.footerLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(108)))), ((int)(((byte)(129)))));
            this.footerLabel.Location = new System.Drawing.Point(24, 329);
            this.footerLabel.Name = "footerLabel";
            this.footerLabel.Size = new System.Drawing.Size(326, 15);
            this.footerLabel.TabIndex = 4;
            this.footerLabel.Text = "Selecciona una operación para continuar con File Wizard";
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(720, 480);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximizeBox = false;
            this.Name = "MainMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Wizard";
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Panel mainPanel;

        private System.Windows.Forms.Button DTdownbutton;
        private System.Windows.Forms.Button DTuploadbutton;
        private System.Windows.Forms.Button QAdownbutton;
        private System.Windows.Forms.Button QAuploadbutton;
        private System.Windows.Forms.Label footerLabel;
    }
}


