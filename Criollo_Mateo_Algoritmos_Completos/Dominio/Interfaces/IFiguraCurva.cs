using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criollo_Mateo_Algoritmos_Completos.Dominio.Interfaces
{
    public interface IFiguraCurva
    {
        int MinimoPuntos { get; }
        List<PointF> CalcularPuntos(List<PointF> puntos);
    }
}
