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



        public FrmCurvas()
        {
            InitializeComponent();
        }

        private void btnLineal_Click(object sender, EventArgs e)
        {
            figuraCurvaActual = new BezierLineal();
            lblTitulo.Text = "Curva Bézier Lineal";
        }

        private void btnCuadratica_Click(object sender, EventArgs e)
        {
            figuraCurvaActual = new BezierCuadratica();
            lblTitulo.Text = "Curva Bézier Cuadrática";
        }

        private void btnCubica_Click(object sender, EventArgs e)
        {
            figuraCurvaActual = new BezierCubica();
            lblTitulo.Text = "Curva Bézier Cúbica";
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

            // Marcar que ya dibujamos
            curvaDibujada = true;

            picCanvas.Invalidate();
        }

        private void btnSpline_Click(object sender, EventArgs e)
        {
            figuraCurvaActual = new Spline();
            lblTitulo.Text = "Curva B-Spline";
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            puntosControl.Clear();
            puntosCurva.Clear();
            curvaDibujada = false;
            picCanvas.Invalidate();
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

            // Si la curva ya está dibujada, no permitas agregar más puntos
            if (curvaDibujada)
                return;

            // Si no se hizo clic sobre un punto, agregar uno nuevo
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
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Dibuja los puntos de control claramente
            foreach (var p in puntosControl)
            {
                g.FillEllipse(Brushes.Red, p.X - radioSeleccion, p.Y - radioSeleccion, radioSeleccion * 2, radioSeleccion * 2);
                g.DrawEllipse(Pens.Black, p.X - radioSeleccion, p.Y - radioSeleccion, radioSeleccion * 2, radioSeleccion * 2);
            }

            // Dibuja la curva si existe
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
