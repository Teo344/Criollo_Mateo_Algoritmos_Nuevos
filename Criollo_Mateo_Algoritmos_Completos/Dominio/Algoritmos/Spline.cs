using Criollo_Mateo_Algoritmos_Completos.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criollo_Mateo_Algoritmos_Completos.Dominio.Algoritmos
{
    public class Spline : IFiguraCurva
    {
        public int MinimoPuntos => 5;
        public List<PointF> CalcularPuntos(List<PointF> puntos)
        {
            var resultado = new List<PointF>();
            int n = puntos.Count - 1;
            if (n < 3) return resultado;

            for (int i = 0; i < n - 2; i++)
            {
                for (float t = 0; t <= 1; t += 0.01f)
                {
                    float t2 = t * t;
                    float t3 = t2 * t;

                    float b0 = (1 - t) * (1 - t) * (1 - t) / 6f;
                    float b1 = (3 * t3 - 6 * t2 + 4) / 6f;
                    float b2 = (-3 * t3 + 3 * t2 + 3 * t + 1) / 6f;
                    float b3 = t3 / 6f;

                    float x = b0 * puntos[i].X + b1 * puntos[i + 1].X +
                              b2 * puntos[i + 2].X + b3 * puntos[i + 3].X;
                    float y = b0 * puntos[i].Y + b1 * puntos[i + 1].Y +
                              b2 * puntos[i + 2].Y + b3 * puntos[i + 3].Y;

                    resultado.Add(new PointF(x, y));
                }
            }
            return resultado;
        }
    }
}
