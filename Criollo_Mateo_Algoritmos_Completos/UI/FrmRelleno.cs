using Criollo_Mateo_Algoritmos_Completos.Aplicacion;
using Criollo_Mateo_Algoritmos_Completos.Dominio.Algoritmos;
using Criollo_Mateo_Algoritmos_Completos.Dominio.Interfaces;
using Criollo_Mateo_Algoritmos_Completos.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Criollo_Mateo_Algoritmos_Completos.UI
{
    public partial class FrmRelleno : Form
    {
        DrawingManager drawingManager;
        IFillAlgorithm fillAlgorithm;
        private Point2D? fillStartPoint;

        PolygonFigure polygonFigure = null;



        public FrmRelleno()
        {
            InitializeComponent();
        }
        private void FrmRelleno_Load(object sender, EventArgs e)
        {
            fillAlgorithm = new FloodFill();
            drawingManager = new DrawingManager(fillAlgorithm, picCanvas);
            lblTitulo.Text = "Algoritmo No seleccionado";
            lblDescripcion.Text = "Crear la figura";
            btnFill.Enabled = false;
            btnScaline.Enabled = false;
            btnPintar.Enabled = false;
            picCanvas.Enabled = false;
        }

        private void btnDibujar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLade.Text))
            {
                MessageBox.Show("Ingrese el número de lados del polígono.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            float centerx = picCanvas.Width / 2f;
            float centery = picCanvas.Height / 2f;
            Point2D center = new Point2D(centerx, centery);
            int sides = int.Parse(txtLade.Text);


            drawingManager.DrawPolygon(center, 40, sides, picCanvas);
            polygonFigure = drawingManager.devolverPolygono(center, 40, sides);
            btnDibujar.Enabled = false;
            btnFill.Enabled = true;
            btnScaline.Enabled = true;
            lblDescripcion.Text = "Seleccione un algoritmo";

        }

        private void activarBoton(Button botonActivo)
        {
            List<Button> botones = new List<Button> { btnFill, btnScaline };

            foreach (var boton in botones)
            {
                if (boton == botonActivo)
                {
                    boton.BackColor = Color.RoyalBlue;
                    boton.ForeColor = Color.White;
                }
                else
                {
                    boton.BackColor = SystemColors.Control;
                    boton.ForeColor = SystemColors.ControlText;
                }
            }
        }

        private void reiniciarBoton()
        {
            List<Button> botones = new List<Button> { btnFill, btnScaline };

            foreach (var boton in botones)
            {
                boton.Enabled = false;
                boton.BackColor = SystemColors.Control;
                boton.ForeColor = SystemColors.ControlText;
             
            }
        }


        private void btnFill_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "Algoritmo Flood Fill";
            lblDescripcion.Text = "Haga click en el punto a rellenar";
            fillAlgorithm = new FloodFill();
            activarBoton(btnFill);
            picCanvas.Enabled = true;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            drawingManager.ClearAll(picCanvas);
            reiniciarBoton();
            btnPintar.Enabled = false;
            btnDibujar.Enabled = true;
            btnFill.Enabled = false;
            lblTitulo.Text = "Algoritmo No seleccionado";
            lblDescripcion.Text = "Crear la figura";
            fillStartPoint = null;
            picCanvas.Enabled = false;
        }

        private void picCanvas_MouseClick(object sender, MouseEventArgs e)
        {

            fillStartPoint = new Point2D(e.X, e.Y);
            lblDescripcion.Text = "El punto fue x= "+e.X + " y= " +e.Y;
            btnPintar.Enabled = true;
        }

        private void btnScaline_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "Algoritmo SkilineFill";
            lblDescripcion.Text = "Listo Para pintar";
            activarBoton(btnScaline);
            fillAlgorithm = new SkilineFill(polygonFigure.vertices);
            btnPintar.Enabled = true;
            picCanvas.Enabled = false;
            fillStartPoint = new Point2D(0,0);
        }

        private async void btnPintar_Click(object sender, EventArgs e)
        {
            Bitmap buffer = drawingManager.GetBuffer();
            Color targetColor = buffer.GetPixel((int)fillStartPoint.Value.X, (int)fillStartPoint.Value.Y);
            Color fillColor = Color.Blue;
            drawingManager = new DrawingManager(fillAlgorithm, picCanvas);
            btnFill.Enabled = false;
            btnScaline.Enabled = false;
            btnPintar.Enabled = false;

            await drawingManager.DrawFloodFill(buffer, fillStartPoint.Value, targetColor, fillColor, picCanvas, 2);

        }

        private void picCanvas_Click(object sender, EventArgs e)
        {
            // No utilizar
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (FrmHome frm = new FrmHome())
            {
                frm.ShowDialog();
            }

            this.Close();
        }
    }
}
