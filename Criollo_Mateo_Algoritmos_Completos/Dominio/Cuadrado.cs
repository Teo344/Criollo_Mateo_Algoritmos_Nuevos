using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Criollo_Mateo_Algoritmos_Completos.Entidades
{

    public class Cuadrado
    {
        public int Lado { get; set; }
        private float SF = 20; // escala
        public Point2D[] puntos;
        public Cuadrado(int lado)
        {
            Lado = lado;
        }

        public void PlotShape(Graphics g, PictureBox picCanvas)
        {
            float anchoCuadrado = Lado * SF;
            float altoCuadrado = Lado * SF;

            float x = (picCanvas.Width - anchoCuadrado) / 2;
            float y = (picCanvas.Height - altoCuadrado) / 2;

            puntos = new Point2D[]
            {
                new Point2D(x, y),
                new Point2D(x + anchoCuadrado, y),
                new Point2D(x + anchoCuadrado, y + altoCuadrado),
                new Point2D(x, y + altoCuadrado),
                new Point2D(x, y)
            };

            PointF[] puntosParaDibujar = puntos.Select(p => p.ToPointF()).ToArray();

            using (Pen pen = new Pen(Color.Green, 3))
            {
                g.DrawPolygon(pen, puntosParaDibujar);
            }
        }
    }


}
