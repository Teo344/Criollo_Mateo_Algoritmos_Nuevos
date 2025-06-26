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
    public class BresenhamCircle : ICircleAlgorithm
    {
        public List<Pixel> DrawCircle(Point2D center, float radius, Color color)
        {
            List<Pixel> pixels = new List<Pixel>();
            HashSet<string> uniqueKeys = new HashSet<string>();

            float x = 0;
            float y = radius;
            float d = 3 - 2 * radius;

            while (x <= y)
            {
                List<Point2D> octantPoints = new List<Point2D>
    {
        new Point2D(center.X + x, center.Y + y),
        new Point2D(center.X - x, center.Y + y),
        new Point2D(center.X + x, center.Y - y),
        new Point2D(center.X - x, center.Y - y),
        new Point2D(center.X + y, center.Y + x),
        new Point2D(center.X - y, center.Y + x),
        new Point2D(center.X + y, center.Y - x),
        new Point2D(center.X - y, center.Y - x),
    };

                foreach (var p in octantPoints)
                {
                    string key = $"{p.X:0.00},{p.Y:0.00}";
                    if (!uniqueKeys.Contains(key))
                    {
                        pixels.Add(new Pixel(p, color));
                        uniqueKeys.Add(key);
                    }
                }

                if (d < 0)
                {
                    d += 4 * x + 6;
                }
                else
                {
                    d += 4 * (x - y) + 10;
                    y--;
                }

                x++;
            }

            return pixels;
        }

    }
}
