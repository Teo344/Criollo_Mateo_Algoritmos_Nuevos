using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criollo_Mateo_Algoritmos_Completos.Entidades
{
    public class Pixel
    {
        public Point2D Position { get; }
        public Color Color { get; }

        public Pixel(Point2D position, Color color)
        {
            Position = position;
            Color = color;
        }
    }
}
