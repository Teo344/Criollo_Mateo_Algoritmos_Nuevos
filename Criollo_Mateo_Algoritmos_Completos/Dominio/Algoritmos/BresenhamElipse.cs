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
    public class BresenhamElipse : IEllipseAlgorithm
    {
        public List<Pixel> DrawEllipse(Point2D center, float rx, float ry, Color color)
        {
            List<Pixel> pixels = new List<Pixel>();

            int x = 0;
            int y = (int)ry;

            float rx2 = rx * rx;
            float ry2 = ry * ry;

            float dx = 2 * ry2 * x;
            float dy = 2 * rx2 * y;

            float p1 = ry2 - rx2 * ry + 0.25f * rx2;

            void AddSymmetricPixels(int cx, int cy, int px, int py)
            {
                pixels.Add(new Pixel(new Point2D(cx + px, cy + py), color));
                pixels.Add(new Pixel(new Point2D(cx - px, cy + py), color));
                pixels.Add(new Pixel(new Point2D(cx + px, cy - py), color));
                pixels.Add(new Pixel(new Point2D(cx - px, cy - py), color));
            }

            while (dx < dy)
            {
                AddSymmetricPixels((int)center.X, (int)center.Y, x, y);
                x++;
                dx = 2 * ry2 * x;
                if (p1 < 0)
                {
                    p1 += dx + ry2;
                }
                else
                {
                    y--;
                    dy = 2 * rx2 * y;
                    p1 += dx - dy + ry2;
                }
            }

            float p2 = ry2 * (x + 0.5f) * (x + 0.5f) + rx2 * (y - 1) * (y - 1) - rx2 * ry2;

            while (y >= 0)
            {
                AddSymmetricPixels((int)center.X, (int)center.Y, x, y);
                y--;
                dy = 2 * rx2 * y;
                if (p2 > 0)
                {
                    p2 += rx2 - dy;
                }
                else
                {
                    x++;
                    dx = 2 * ry2 * x;
                    p2 += dx - dy + rx2;
                }
            }

            return pixels;
        }
    }
}
