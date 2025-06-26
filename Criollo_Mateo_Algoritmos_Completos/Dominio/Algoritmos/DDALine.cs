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
    public class DDALine : ILineAlgorithm
    {
        public List<Pixel> DrawLine(Point2D start, Point2D end, Color color)
        {
            var pixels = new List<Pixel>();

            float dx = end.X - start.X;
            float dy = end.Y - start.Y;

            int steps = (int)Math.Max(Math.Abs(dx), Math.Abs(dy));

            float xIncrement = dx / (float)steps;
            float yIncrement = dy / (float)steps;

            float x = start.X;
            float y = start.Y;

            Point2D point;

            for (int i = 0; i <= steps; i++)
            {
                point = new Point2D(x, y);
                pixels.Add(new Pixel(point, color));
                x += xIncrement;
                y += yIncrement;
            }

            return pixels;
        }


    }
}
