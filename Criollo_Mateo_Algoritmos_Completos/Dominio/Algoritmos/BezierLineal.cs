using Criollo_Mateo_Algoritmos_Completos.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criollo_Mateo_Algoritmos_Completos.Dominio.Algoritmos
{
    public class BezierLineal : IFiguraCurva
    {
        public int MinimoPuntos => 2;
        public List<PointF> CalcularPuntos(List<PointF> puntos)
        {
            var resultado = new List<PointF>();
            for (float t = 0; t <= 1; t += 0.01f)
            {
                float x = (1 - t) * puntos[0].X + t * puntos[1].X;
                float y = (1 - t) * puntos[0].Y + t * puntos[1].Y;
                resultado.Add(new PointF(x, y));
            }
            return resultado;
        }

    }
}
