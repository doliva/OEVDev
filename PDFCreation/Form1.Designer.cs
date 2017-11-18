namespace PDFCreation
{
    partial class Form1
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
            this.generatePDFBtn = new System.Windows.Forms.Button();
            this.generarPDFVentas = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // generatePDFBtn
            // 
            this.generatePDFBtn.Location = new System.Drawing.Point(131, 168);
            this.generatePDFBtn.Name = "generatePDFBtn";
            this.generatePDFBtn.Size = new System.Drawing.Size(168, 23);
            this.generatePDFBtn.TabIndex = 0;
            this.generatePDFBtn.Text = "Generar PDF Voucher";
            this.generatePDFBtn.UseVisualStyleBackColor = true;
            this.generatePDFBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // generarPDFVentas
            // 
            this.generarPDFVentas.Location = new System.Drawing.Point(413, 168);
            this.generarPDFVentas.Name = "generarPDFVentas";
            this.generarPDFVentas.Size = new System.Drawing.Size(185, 23);
            this.generarPDFVentas.TabIndex = 1;
            this.generarPDFVentas.Text = "Generar PDF Ventas";
            this.generarPDFVentas.UseVisualStyleBackColor = true;
            this.generarPDFVentas.Click += new System.EventHandler(this.generarPDFVentas_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 391);
            this.Controls.Add(this.generarPDFVentas);
            this.Controls.Add(this.generatePDFBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button generatePDFBtn;
        private System.Windows.Forms.Button generarPDFVentas;
    }
}

