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
    public partial class FrmLinea : Form
    {
        DrawingManager drawingManager;
        ILineAlgorithm lineAlgorithm;
        ICircleAlgorithm circleAlgorithm;
        IEllipseAlgorithm elipseAlgorithm;
        List<Point2D> puntos;

        int countPoint;
        string algoritmoSeleccionado;

        public FrmLinea()
        {
            InitializeComponent();
        }

        private void FrmLinea_Load(object sender, EventArgs e)
        {
            lineAlgorithm = new DDALine();
            drawingManager = new DrawingManager(lineAlgorithm);
            lblTitulo.Text = "Algoritmo DDA";
            btnDDA.BackColor = Color.RoyalBlue;
            btnDDA.ForeColor = Color.White;
            countPoint = 0;
            puntos = new List<Point2D>();
            algoritmoSeleccionado = "DDA";
            lblDescripcion.Text = "De dos clic para graficar las líneas";
        }

        private void activarBoton(Button botonActivo)
        {
            List<Button> botones = new List<Button> { btnDDA, btnLinea, btnCirculo, btnElipse };

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


        private void btnDDA_Click(object sender, EventArgs e)
        {
            lineAlgorithm = new DDALine();
            drawingManager = new DrawingManager(lineAlgorithm);
            lblTitulo.Text = "Algoritmo DDA";
            lblDescripcion.Text = "De dos clic para graficar las líneas";
            algoritmoSeleccionado = "DDA";
            activarBoton(btnDDA);
        }

        private void btnLinea_Click(object sender, EventArgs e)
        {
            lineAlgorithm = new BresenhamLine();
            drawingManager = new DrawingManager(lineAlgorithm);
            lblTitulo.Text = "Algoritmo Bresenham Line";
            lblDescripcion.Text = "De dos clic para graficar las líneas";
            algoritmoSeleccionado = "BLine";
            activarBoton(btnLinea);
        }

        private void btnCirculo_Click(object sender, EventArgs e)
        {
            circleAlgorithm = new BresenhamCircle();
            drawingManager = new DrawingManager(circleAlgorithm);
            lblTitulo.Text = "Algoritmo Bresenham Circle";
            lblDescripcion.Text = "De dos clic para el centro y radio";
            algoritmoSeleccionado = "BCircle";
            activarBoton(btnCirculo);
        }

        private void btnElipse_Click(object sender, EventArgs e)
        {
            elipseAlgorithm = new BresenhamElipse();
            drawingManager = new DrawingManager(elipseAlgorithm);
            lblTitulo.Text = "Algoritmo Bresenham Elipse";
            lblDescripcion.Text = "De dos clic para el centro y radio";
            algoritmoSeleccionado = "BElipse";
            activarBoton(btnElipse);
        }


        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                if (countPoint >= 2)
                {
                    MessageBox.Show("Ya seleccionó los dos puntos. Reinicie si desea volver a dibujar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Point2D punto = new Point2D(e.X, e.Y);
                drawingManager.drawPaint(punto, Color.Red, picCanvas, 5);
                puntos.Add(punto);
                countPoint++;
            }
        }



        private void picCanvas_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            puntos.Clear();
            drawingManager.ClearAll(picCanvas);
            countPoint = 0;
            btnDDA.Enabled = true;
            btnLinea.Enabled = true;
            btnCirculo.Enabled = true;
            btnElipse.Enabled = true;
            btnDibujar.Enabled = true;
        }

        private async void btnDibujar_Click(object sender, EventArgs e)
        {
            if (puntos.Count < 2)
            {
                MessageBox.Show("Debe seleccionar dos puntos en el canvas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            btnDDA.Enabled = false;
            btnLinea.Enabled = false;
            btnCirculo.Enabled = false;
            btnElipse.Enabled = false;
            btnLimpiar.Enabled = false;

            if (algoritmoSeleccionado=="DDA" ||algoritmoSeleccionado=="BLine" )
            {
                await drawingManager.DrawPixelsAsync(puntos[0], puntos[1], picCanvas);
            }

            if (algoritmoSeleccionado == "BCircle")
            {
                await drawingManager.DrawPixelsAsyncCircle(puntos[0], puntos[1], picCanvas);
            }

            if (algoritmoSeleccionado == "BElipse")
            {
                await drawingManager.DrawPixelsAsyncEllipse(puntos[0], puntos[1], picCanvas);
            }


            btnLimpiar.Enabled = true;
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
