using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criollo_Mateo_Algoritmos_Completos.Entidades
{
    public class PolygonFigure
    {
        public List<Point2D> vertices { get; }
        public Point2D center { get; }
        public int sides { get; }

        public PolygonFigure(Point2D center, int sides, List<Point2D> vertices)
        {
            this.center = center;
            this.sides = sides;
            this.vertices = vertices;
        }

    }
}
