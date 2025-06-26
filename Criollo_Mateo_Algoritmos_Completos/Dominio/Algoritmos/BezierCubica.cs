using Criollo_Mateo_Algoritmos_Completos.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criollo_Mateo_Algoritmos_Completos.Dominio.Algoritmos
{
    public class BezierCubica : IFiguraCurva
    {
        public int MinimoPuntos => 4;
        public List<PointF> CalcularPuntos(List<PointF> puntos)
        {
            var resultado = new List<PointF>();
            for (float t = 0; t <= 1; t += 0.01f)
            {
                float x = (float)(Math.Pow(1 - t, 3) * puntos[0].X +
                                  3 * Math.Pow(1 - t, 2) * t * puntos[1].X +
                                  3 * (1 - t) * Math.Pow(t, 2) * puntos[2].X +
                                  Math.Pow(t, 3) * puntos[3].X);
                float y = (float)(Math.Pow(1 - t, 3) * puntos[0].Y +
                                  3 * Math.Pow(1 - t, 2) * t * puntos[1].Y +
                                  3 * (1 - t) * Math.Pow(t, 2) * puntos[2].Y +
                                  Math.Pow(t, 3) * puntos[3].Y);
                resultado.Add(new PointF(x, y));
            }
            return resultado;
        }
    }
}
