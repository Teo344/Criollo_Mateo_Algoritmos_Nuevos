using Criollo_Mateo_Algoritmos_Completos.Aplicacion;
using Criollo_Mateo_Algoritmos_Completos.Dominio.Algoritmos;
using Criollo_Mateo_Algoritmos_Completos.Dominio.Interfaces;
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
    public partial class FrmCurvas : Form
    {

        private List<PointF> puntosControl = new List<PointF>();
        private IFiguraCurva figuraCurvaActual;
        private bool arrastrando = false;
        private int indicePuntoSeleccionado = -1;
        private const int radioSeleccion = 6;
        private List<PointF> puntosCurva = new List<PointF>();
        private bool curvaDibujada = false;
        private DrawingManager drawingManager;



        public FrmCurvas()
        {
            InitializeComponent();
        }

        private void btnLineal_Click(object sender, EventArgs e)
        {
            figuraCurvaActual = new BezierLineal();
            lblTitulo.Text = "Curva Bézier Lineal";
            picCanvas.Enabled = true;
            btnDibujar.Enabled = true;

            activarBoton(btnLineal);
        }

        private void btnCuadratica_Click(object sender, EventArgs e)
        {
            figuraCurvaActual = new BezierCuadratica();
            lblTitulo.Text = "Curva Bézier Cuadrática";
            picCanvas.Enabled = true;
            btnDibujar.Enabled = true;
            activarBoton(btnCuadratica);
        }

        private void btnCubica_Click(object sender, EventArgs e)
        {
            figuraCurvaActual = new BezierCubica();
            lblTitulo.Text = "Curva Bézier Cúbica";
            picCanvas.Enabled = true;
            btnDibujar.Enabled = true;
            activarBoton(btnCubica);
        }

        private void activarBoton(Button botonActivo)
        {
            List<Button> botones = new List<Button> { btnLineal,btnCuadratica,btnCubica,btnSpline  };

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
            List<Button> botones = new List<Button> { btnLineal, btnCuadratica, btnCubica, btnSpline };

            foreach (var boton in botones)
            {
                boton.Enabled = true;
                boton.BackColor = SystemColors.Control;
                boton.ForeColor = SystemColors.ControlText;

            }
        }


        private void btnDibujar_Click(object sender, EventArgs e)
        {
            if (figuraCurvaActual == null)
            {
                MessageBox.Show("Seleccione el tipo de curva primero.");
                return;
            }

            if (puntosControl.Count < figuraCurvaActual.MinimoPuntos)
            {
                MessageBox.Show($"Se requieren al menos {figuraCurvaActual.MinimoPuntos} puntos.");
                return;
            }

            puntosCurva = figuraCurvaActual.CalcularPuntos(puntosControl);

            curvaDibujada = true;

            picCanvas.Invalidate();
            btnDibujar.Enabled = false;
            btnLineal.Enabled = false;
            btnCuadratica.Enabled = false;
            btnCubica.Enabled = false;
            btnSpline.Enabled = false;
            lblDescripcion.Text = "Curva dibujada. Puede arrastrar los puntos";
        }

        private void btnSpline_Click(object sender, EventArgs e)
        {
            figuraCurvaActual = new Spline();
            lblTitulo.Text = "Curva B-Spline";
            picCanvas.Enabled = true;
            btnDibujar.Enabled = true;
            activarBoton(btnSpline);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            puntosControl.Clear();
            puntosCurva.Clear();
            curvaDibujada = false;
            picCanvas.Invalidate();
            reiniciarBoton();
            picCanvas.Enabled = false;
            lblTitulo.Text = "Seleccione el tipo de curva";
            lblDescripcion.Text = "Seleccione la curva y de clic para los puntos";
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

        private void FrmCurvas_Load(object sender, EventArgs e)
        {
            lblTitulo.Text = "Seleccione el tipo de curva";
            picCanvas.MouseMove += picCanvas_MouseMove;
            picCanvas.MouseUp += picCanvas_MouseUp;
            picCanvas.MouseDown += picCanvas_MouseDown;
            lblDescripcion.Text = "Seleccione la curva y de clic para los puntos";
            picCanvas.Enabled = false;

        }

        private void picCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            // Primero detecta si hizo clic sobre un punto de control existente
            for (int i = 0; i < puntosControl.Count; i++)
            {
                float dx = e.X - puntosControl[i].X;
                float dy = e.Y - puntosControl[i].Y;
                float dist = (float)Math.Sqrt(dx * dx + dy * dy);

                if (dist <= radioSeleccion)
                {
                    indicePuntoSeleccionado = i;
                    arrastrando = true;
                    return;
                }
            }
            if (curvaDibujada)
                return;

            puntosControl.Add(new PointF(e.X, e.Y));
            picCanvas.Invalidate();
        }

        private void picCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (arrastrando && indicePuntoSeleccionado != -1)
            {
                puntosControl[indicePuntoSeleccionado] = new PointF(e.X, e.Y);

                // Si la curva ya estaba dibujada, actualizamos la curva en tiempo real
                if (curvaDibujada && figuraCurvaActual != null && puntosControl.Count >= figuraCurvaActual.MinimoPuntos)
                {
                    puntosCurva = figuraCurvaActual.CalcularPuntos(puntosControl);
                }

                picCanvas.Invalidate();
            }
        }

        private void picCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            arrastrando = false;
            indicePuntoSeleccionado = -1;
        }

        private void picCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            drawingManager = new DrawingManager(figuraCurvaActual);
            drawingManager.DibujarCurva(e.Graphics, puntosCurva, new Pen(Color.Blue, 2));

            foreach (var p in puntosControl)
            {
                g.FillEllipse(Brushes.Red, p.X - radioSeleccion, p.Y - radioSeleccion, radioSeleccion * 2, radioSeleccion * 2);
                g.DrawEllipse(Pens.Black, p.X - radioSeleccion, p.Y - radioSeleccion, radioSeleccion * 2, radioSeleccion * 2);
            }

            if (puntosCurva.Count > 1)
            {
                g.DrawLines(Pens.Blue, puntosCurva.ToArray());
            }
        }

        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < puntosControl.Count; i++)
            {
                float dx = e.X - puntosControl[i].X;
                float dy = e.Y - puntosControl[i].Y;
                float dist = (float)Math.Sqrt(dx * dx + dy * dy);

                if (dist <= radioSeleccion)
                {
                    indicePuntoSeleccionado = i;
                    arrastrando = true;
                    return;
                }
            }

            if (curvaDibujada)
                return;

            if (figuraCurvaActual != null && puntosControl.Count >= figuraCurvaActual.MinimoPuntos)
            {
                MessageBox.Show($"La curva seleccionada solo permite {figuraCurvaActual.MinimoPuntos} puntos de control.",
                                "Límite alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            puntosControl.Add(new PointF(e.X, e.Y));
            picCanvas.Invalidate();
        }
    }
}
