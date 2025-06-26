using Criollo_Mateo_Algoritmos_Completos.Dominio.Interfaces;
using Criollo_Mateo_Algoritmos_Completos.Entidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criollo_Mateo_Algoritmos_Completos.Dominio.Algoritmos
{
    public class BresenhamLine : ILineAlgorithm
    {
        public List<Pixel> DrawLine(Point2D start, Point2D end, Color color)
        {
            List<Pixel> pixels = new List<Pixel>();

            float x0 = (int)Math.Round(start.X);
            float y0 = (int)Math.Round(start.Y);
            float x1 = (int)Math.Round(end.X);
            float y1 = (int)Math.Round(end.Y);

            float dx = Math.Abs(x1 - x0);
            float dy = Math.Abs(y1 - y0);
            float sx = x0 < x1 ? 1 : -1;
            float sy = y0 < y1 ? 1 : -1;

            float err = dx - dy;

            while (true)
            {
                pixels.Add(new Pixel(new Point2D(x0, y0), color));

                if (x0 == x1 && y0 == y1) break;

                float e2 = 2 * err;

                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }

                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }

            return pixels;

        }
    }
}
