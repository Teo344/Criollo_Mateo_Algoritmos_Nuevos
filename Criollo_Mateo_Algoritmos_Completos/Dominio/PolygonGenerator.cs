using Criollo_Mateo_Algoritmos_Completos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criollo_Mateo_Algoritmos_Completos.Dominio
{
    public class PolygonGenerator
    {
        public PolygonFigure GenerateRegularPolygon(Point2D center, float radius, int sides)
        {
            if (sides < 3)
                throw new ArgumentException("Un polígono debe tener al menos 3 lados.");

            List<Point2D> vertices = new List<Point2D>();

            double angleIncrement = 2 * Math.PI / sides;
            double startAngle = -Math.PI / 2 + (sides % 2 == 0 ? angleIncrement / 2 : 0);

            for (int i = 0; i < sides; i++)
            {
                double angle = startAngle + i * angleIncrement;

                float x = center.X + (int)Math.Round(radius * Math.Cos(angle));
                float y = center.Y + (int)Math.Round(radius * Math.Sin(angle));

                vertices.Add(new Point2D(x, y));
            }

            return new PolygonFigure(center, sides, vertices);
        }

    }


}
