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
    public partial class FrmRecortar : Form
    {

        Cuadrado cuadrado;
        Point2D inicio;
        Point2D final;
        Point2D punto;

        List<Point2D> puntosCompletos;
        List<Point2D> puntosCortados;
        bool dibujandoPoligono = false;
        bool recortado = false;

        DrawingManager drawingManager;
        IRecortador recorteAlgorithm;

        int contador;


        public FrmRecortar()
        {
            InitializeComponent();
        }

        private void FrmRecortar_Load(object sender, EventArgs e)
        {
            recorteAlgorithm = new Recortador();
            drawingManager = new DrawingManager(recorteAlgorithm);

            inicio = new Point2D();
            final = new Point2D();
            puntosCompletos = new List<Point2D>();
            puntosCortados = new List<Point2D>();
            contador = 0;
            punto = new Point2D();
            cuadrado = new Cuadrado(5);
            lblTitulo.Text = "Algoritmo de Recorte de Lineas";
            lblDescripcion.Text = "De dos clic para graficar las líneas";
            btnLinea.BackColor = Color.RoyalBlue;
            btnLinea.ForeColor = Color.White;
            btnCompletar.Enabled = false;
        }

        private void btnLinea_Click(object sender, EventArgs e)
        {
            drawingManager.ClearAll(picCanvas);
            puntosCompletos.Clear();
            activarBoton(btnLinea);
            dibujandoPoligono = false;

        }

        private void picCanvas_Paint(object sender, PaintEventArgs e)
        {
            cuadrado.PlotShape(e.Graphics, picCanvas);


            if (dibujandoPoligono == true)
            {
                drawingManager.dibujarLinea(e.Graphics, puntosCompletos, recortado);
                drawingManager.dibujarLinea(e.Graphics, puntosCortados, recortado);
                drawingManager.dibujarRecortado(e.Graphics, puntosCortados);
            }
            else {

                drawingManager.dibujarCompleto(e.Graphics, puntosCompletos);
                drawingManager.dibujarRecortado(e.Graphics, puntosCortados);

            }
        }

        private void picCanvas_MouseClick(object sender, MouseEventArgs e)
        {

            if (dibujandoPoligono == true)
            {
                punto.leer(e.X, e.Y);

                puntosCompletos.Add(new Point2D(punto.X, punto.Y));

                picCanvas.Invalidate();
                contador++;

            }
            else

            {
                if (contador == 0)
                {
                    inicio.leer(e.X, e.Y);
                    contador++;
                }
                else
                {
                    final.leer(e.X, e.Y);
                    puntosCompletos.Add(new Point2D(inicio.X, inicio.Y));
                    puntosCompletos.Add(new Point2D(final.X, final.Y));
                    contador = 0;
                    picCanvas.Invalidate();
                }
            }
        }


        private void activarBoton(Button botonActivo)
        {
            List<Button> botones = new List<Button> { btnLinea, btnPoligono };

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


        private void btnRecorte_Click(object sender, EventArgs e)
        {
            if (puntosCompletos.Count >= 2)
            {
                if(dibujandoPoligono == true)
                {
                    puntosCortados = recorteAlgorithm.RecortarPoligono(puntosCompletos, cuadrado.puntos.ToList());
                }
                else
                {
                    puntosCortados = recorteAlgorithm.RecortarLineas(puntosCompletos, cuadrado.puntos.ToList());
                }
                recortado = true;
                puntosCompletos.Clear();
                picCanvas.Invalidate();
                picCanvas.Enabled= false;
                btnRecorte.Enabled = false;
                btnLinea.Enabled = false;
                btnPoligono.Enabled = false;
            }

            else
            {
                MessageBox.Show("Debe hacer al menos dos clics para definir una línea completa.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            puntosCompletos.Clear();
            puntosCortados.Clear();
            picCanvas.Invalidate();
            recortado = false;
            contador = 0;
            btnLinea.Enabled = true;
            btnPoligono.Enabled = true;
            btnRecorte.Enabled = true;
            picCanvas.Enabled = true;
        }

        private void btnPoligono_Click(object sender, EventArgs e)
        {
            activarBoton(btnPoligono);

            puntosCompletos.Clear();
            puntosCortados.Clear();
            dibujandoPoligono = true;
            btnCompletar.Enabled = true;
            btnRecorte.Enabled = false;
            picCanvas.Invalidate();
            contador = 0;
        }

        private void btnCompletar_Click(object sender, EventArgs e)
        {
            recortado = true;
            btnLinea.Enabled = false;
            picCanvas.Invalidate();
            btnRecorte.Enabled = true;  
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
