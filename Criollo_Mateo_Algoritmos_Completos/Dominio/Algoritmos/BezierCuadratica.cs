using Criollo_Mateo_Algoritmos_Completos.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criollo_Mateo_Algoritmos_Completos.Dominio.Algoritmos
{
    public class BezierCuadratica : IFiguraCurva
    {
        public int MinimoPuntos => 3;

        public List<PointF> CalcularPuntos(List<PointF> puntos)
        {
            var resultado = new List<PointF>();
            for (float t = 0; t <= 1; t += 0.01f)
            {
                float x = (float)(Math.Pow(1 - t, 2) * puntos[0].X +
                                  2 * (1 - t) * t * puntos[1].X +
                                  Math.Pow(t, 2) * puntos[2].X);
                float y = (float)(Math.Pow(1 - t, 2) * puntos[0].Y +
                                  2 * (1 - t) * t * puntos[1].Y +
                                  Math.Pow(t, 2) * puntos[2].Y);
                resultado.Add(new PointF(x, y));
            }
            return resultado;
        }
    }
}
