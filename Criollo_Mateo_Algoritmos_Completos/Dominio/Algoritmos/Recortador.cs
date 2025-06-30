using Criollo_Mateo_Algoritmos_Completos.Dominio.Interfaces;
using Criollo_Mateo_Algoritmos_Completos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criollo_Mateo_Algoritmos_Completos.Dominio.Algoritmos
{
    public class Recortador : IRecortador
    {
        const int INSIDE = 0;
        const int LEFT = 1;
        const int RIGHT = 2;
        const int BOTTOM = 4;
        const int TOP = 8;

        private int ComputeOutCode(double x, double y, double xmin, double xmax, double ymin, double ymax)
        {
            int code = INSIDE;
            if (x < xmin) code |= LEFT;
            else if (x > xmax) code |= RIGHT;
            if (y < ymin) code |= BOTTOM;
            else if (y > ymax) code |= TOP;
            return code;
        }

        public List<Point2D> RecortarLineas(List<Point2D> segmentos, List<Point2D> ventana)
        {
            List<Point2D> resultado = new List<Point2D>();
            double xmin = ventana[0].X, ymin = ventana[0].Y, xmax = ventana[2].X, ymax = ventana[2].Y;

            for (int i = 0; i < segmentos.Count; i += 2)
            {
                double x0 = segmentos[i].X, y0 = segmentos[i].Y;
                double x1 = segmentos[i + 1].X, y1 = segmentos[i + 1].Y;
                int outcode0 = ComputeOutCode(x0, y0, xmin, xmax, ymin, ymax);
                int outcode1 = ComputeOutCode(x1, y1, xmin, xmax, ymin, ymax);
                bool accept = false;

                while (true)
                {
                    if ((outcode0 | outcode1) == 0)
                    {
                        accept = true;
                        break;
                    }
                    else if ((outcode0 & outcode1) != 0)
                    {
                        break;
                    }
                    else
                    {
                        double x = 0, y = 0;
                        int outcodeOut = (outcode0 != 0) ? outcode0 : outcode1;

                        if ((outcodeOut & TOP) != 0) { x = x0 + (x1 - x0) * (ymax - y0) / (y1 - y0); y = ymax; }
                        else if ((outcodeOut & BOTTOM) != 0) { x = x0 + (x1 - x0) * (ymin - y0) / (y1 - y0); y = ymin; }
                        else if ((outcodeOut & RIGHT) != 0) { y = y0 + (y1 - y0) * (xmax - x0) / (x1 - x0); x = xmax; }
                        else if ((outcodeOut & LEFT) != 0) { y = y0 + (y1 - y0) * (xmin - x0) / (x1 - x0); x = xmin; }

                        if (outcodeOut == outcode0) { x0 = x; y0 = y; outcode0 = ComputeOutCode(x0, y0, xmin, xmax, ymin, ymax); }
                        else { x1 = x; y1 = y; outcode1 = ComputeOutCode(x1, y1, xmin, xmax, ymin, ymax); }
                    }
                }

                if (accept)
                {
                    resultado.Add(new Point2D((int)x0, (int)y0));
                    resultado.Add(new Point2D((int)x1, (int)y1));
                }
            }

            return resultado;
        }

        public List<Point2D> RecortarPoligono(List<Point2D> poligono, List<Point2D> ventana)
        {
            double xmin = ventana[0].X;
            double ymin = ventana[0].Y;
            double xmax = ventana[2].X;
            double ymax = ventana[2].Y;

            List<Point2D> output = new List<Point2D>(poligono);

            output = RecortarContraBorde(output, p => p.X >= xmin, (a, b) =>
            {
                if (a.X == b.X) return null;
                double t = (xmin - a.X) / (b.X - a.X);
                if (t < 0 || t > 1) return null;
                double y = a.Y + t * (b.Y - a.Y);
                return new Point2D((int)xmin, (int)y);
            });

            output = RecortarContraBorde(output, p => p.X <= xmax, (a, b) =>
            {
                if (a.X == b.X) return null;
                double t = (xmax - a.X) / (b.X - a.X);
                if (t < 0 || t > 1) return null;
                double y = a.Y + t * (b.Y - a.Y);
                return new Point2D((int)xmax, (int)y);
            });

            output = RecortarContraBorde(output, p => p.Y >= ymin, (a, b) =>
            {
                if (a.Y == b.Y) return null;
                double t = (ymin - a.Y) / (b.Y - a.Y);
                if (t < 0 || t > 1) return null;
                double x = a.X + t * (b.X - a.X);
                return new Point2D((int)x, (int)ymin);
            });

            output = RecortarContraBorde(output, p => p.Y <= ymax, (a, b) =>
            {
                if (a.Y == b.Y) return null;
                double t = (ymax - a.Y) / (b.Y - a.Y);
                if (t < 0 || t > 1) return null;
                double x = a.X + t * (b.X - a.X);
                return new Point2D((int)x, (int)ymax);
            });

            return output;
        }


        private List<Point2D> RecortarContraBorde(
            List<Point2D> input,
            Func<Point2D, bool> estaDentro,
            Func<Point2D, Point2D, Point2D?> calcularInterseccion)
        {
            List<Point2D> output = new List<Point2D>();
            if (input.Count == 0) return output;

            Point2D prev = input[input.Count - 1];
            foreach (Point2D actual in input)
            {
                bool inPrev = estaDentro(prev);
                bool inActual = estaDentro(actual);

                if (inActual)
                {
                    if (!inPrev)
                    {
                        var inter = calcularInterseccion(prev, actual);
                        if (inter.HasValue) output.Add(inter.Value);
                    }
                    output.Add(actual);
                }
                else if (inPrev)
                {
                    var inter = calcularInterseccion(prev, actual);
                    if (inter.HasValue) output.Add(inter.Value);
                }

                prev = actual;
            }
            return output;
        }

    }


}
