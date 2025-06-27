namespace Criollo_Mateo_Algoritmos_Completos.UI
{
    partial class FrmCurvas
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
            this.btnLineal = new System.Windows.Forms.Button();
            this.btnCuadratica = new System.Windows.Forms.Button();
            this.btnCubica = new System.Windows.Forms.Button();
            this.btnSpline = new System.Windows.Forms.Button();
            this.btnDibujar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.picCanvas = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLineal
            // 
            this.btnLineal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnLineal.Location = new System.Drawing.Point(21, 58);
            this.btnLineal.Name = "btnLineal";
            this.btnLineal.Size = new System.Drawing.Size(144, 56);
            this.btnLineal.TabIndex = 1;
            this.btnLineal.Text = "Lineal";
            this.btnLineal.UseVisualStyleBackColor = true;
            this.btnLineal.Click += new System.EventHandler(this.btnLineal_Click);
            // 
            // btnCuadratica
            // 
            this.btnCuadratica.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnCuadratica.Location = new System.Drawing.Point(21, 202);
            this.btnCuadratica.Name = "btnCuadratica";
            this.btnCuadratica.Size = new System.Drawing.Size(144, 56);
            this.btnCuadratica.TabIndex = 2;
            this.btnCuadratica.Text = "Cuadrática";
            this.btnCuadratica.UseVisualStyleBackColor = true;
            this.btnCuadratica.Click += new System.EventHandler(this.btnCuadratica_Click);
            // 
            // btnCubica
            // 
            this.btnCubica.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnCubica.Location = new System.Drawing.Point(682, 23);
            this.btnCubica.Name = "btnCubica";
            this.btnCubica.Size = new System.Drawing.Size(144, 56);
            this.btnCubica.TabIndex = 3;
            this.btnCubica.Text = "Cúbica";
            this.btnCubica.UseVisualStyleBackColor = true;
            this.btnCubica.Click += new System.EventHandler(this.btnCubica_Click);
            // 
            // btnSpline
            // 
            this.btnSpline.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnSpline.Location = new System.Drawing.Point(682, 202);
            this.btnSpline.Name = "btnSpline";
            this.btnSpline.Size = new System.Drawing.Size(144, 56);
            this.btnSpline.TabIndex = 4;
            this.btnSpline.Text = "Spline";
            this.btnSpline.UseVisualStyleBackColor = true;
            this.btnSpline.Click += new System.EventHandler(this.btnSpline_Click);
            // 
            // btnDibujar
            // 
            this.btnDibujar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnDibujar.Location = new System.Drawing.Point(12, 337);
            this.btnDibujar.Name = "btnDibujar";
            this.btnDibujar.Size = new System.Drawing.Size(144, 56);
            this.btnDibujar.TabIndex = 5;
            this.btnDibujar.Text = "Dibujar";
            this.btnDibujar.UseVisualStyleBackColor = true;
            this.btnDibujar.Click += new System.EventHandler(this.btnDibujar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnLimpiar.Location = new System.Drawing.Point(175, 337);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(144, 56);
            this.btnLimpiar.TabIndex = 6;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnRegresar
            // 
            this.btnRegresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnRegresar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnRegresar.Location = new System.Drawing.Point(335, 337);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(144, 56);
            this.btnRegresar.TabIndex = 7;
            this.btnRegresar.Text = "Regresar";
            this.btnRegresar.UseVisualStyleBackColor = false;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitulo.Location = new System.Drawing.Point(376, 302);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(70, 25);
            this.lblTitulo.TabIndex = 8;
            this.lblTitulo.Text = "label1";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.BackColor = System.Drawing.Color.Transparent;
            this.lblDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDescripcion.Location = new System.Drawing.Point(530, 337);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(50, 16);
            this.lblDescripcion.TabIndex = 12;
            this.lblDescripcion.Text = "label2";
            // 
            // picCanvas
            // 
            this.picCanvas.BackColor = System.Drawing.SystemColors.Info;
            this.picCanvas.Location = new System.Drawing.Point(190, 12);
            this.picCanvas.Name = "picCanvas";
            this.picCanvas.Size = new System.Drawing.Size(468, 287);
            this.picCanvas.TabIndex = 0;
            this.picCanvas.TabStop = false;
            this.picCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.picCanvas_Paint);
            this.picCanvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picCanvas_MouseClick);
            this.picCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picCanvas_MouseDown);
            this.picCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picCanvas_MouseMove);
            this.picCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picCanvas_MouseUp);
            // 
            // FrmCurvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(882, 413);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnDibujar);
            this.Controls.Add(this.btnSpline);
            this.Controls.Add(this.btnCubica);
            this.Controls.Add(this.btnCuadratica);
            this.Controls.Add(this.btnLineal);
            this.Controls.Add(this.picCanvas);
            this.Name = "FrmCurvas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCurvas";
            this.Load += new System.EventHandler(this.FrmCurvas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picCanvas;
        private System.Windows.Forms.Button btnLineal;
        private System.Windows.Forms.Button btnCuadratica;
        private System.Windows.Forms.Button btnCubica;
        private System.Windows.Forms.Button btnSpline;
        private System.Windows.Forms.Button btnDibujar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblDescripcion;
    }
}