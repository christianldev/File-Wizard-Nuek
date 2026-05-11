namespace File_Wizard
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
            this.DTdownbutton = new System.Windows.Forms.Button();
            this.DTuploadbutton = new System.Windows.Forms.Button();
            this.QAdownbutton = new System.Windows.Forms.Button();
            this.QAuploadbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DTdownbutton
            // 
            this.DTdownbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTdownbutton.Location = new System.Drawing.Point(22, 23);
            this.DTdownbutton.Name = "DTdownbutton";
            this.DTdownbutton.Size = new System.Drawing.Size(153, 57);
            this.DTdownbutton.TabIndex = 0;
            this.DTdownbutton.Text = "DESA-TEST DOWNLOADER";
            this.DTdownbutton.UseVisualStyleBackColor = true;
            this.DTdownbutton.Click += new System.EventHandler(this.DTdownbutton_Click);
            // 
            // DTuploadbutton
            // 
            this.DTuploadbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTuploadbutton.Location = new System.Drawing.Point(22, 107);
            this.DTuploadbutton.Name = "DTuploadbutton";
            this.DTuploadbutton.Size = new System.Drawing.Size(153, 57);
            this.DTuploadbutton.TabIndex = 0;
            this.DTuploadbutton.Text = "DESA-TEST UPLOADER";
            this.DTuploadbutton.UseVisualStyleBackColor = true;
            this.DTuploadbutton.Click += new System.EventHandler(this.DTuploadbutton_Click);
            // 
            // QAdownbutton
            // 
            this.QAdownbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QAdownbutton.Location = new System.Drawing.Point(196, 23);
            this.QAdownbutton.Name = "QAdownbutton";
            this.QAdownbutton.Size = new System.Drawing.Size(153, 57);
            this.QAdownbutton.TabIndex = 0;
            this.QAdownbutton.Text = "QASSUR DOWNLOADER";
            this.QAdownbutton.UseVisualStyleBackColor = true;
            this.QAdownbutton.Click += new System.EventHandler(this.QAdownbutton_Click);
            // 
            // QAuploadbutton
            // 
            this.QAuploadbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QAuploadbutton.Location = new System.Drawing.Point(196, 107);
            this.QAuploadbutton.Name = "QAuploadbutton";
            this.QAuploadbutton.Size = new System.Drawing.Size(153, 57);
            this.QAuploadbutton.TabIndex = 0;
            this.QAuploadbutton.Text = "QASSUR UPLOADER";
            this.QAuploadbutton.UseVisualStyleBackColor = true;
            this.QAuploadbutton.Click += new System.EventHandler(this.QAuploadbutton_Click);
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(378, 190);
            this.Controls.Add(this.QAuploadbutton);
            this.Controls.Add(this.DTuploadbutton);
            this.Controls.Add(this.QAdownbutton);
            this.Controls.Add(this.DTdownbutton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Wizard v1.0";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button DTdownbutton;
        private System.Windows.Forms.Button DTuploadbutton;
        private System.Windows.Forms.Button QAdownbutton;
        private System.Windows.Forms.Button QAuploadbutton;
    }
}

