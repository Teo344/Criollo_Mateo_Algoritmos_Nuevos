using Criollo_Mateo_Algoritmos_Completos.Entidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criollo_Mateo_Algoritmos_Completos.Dominio.Interfaces
{
    public interface ILineAlgorithm
    {
        List<Pixel> DrawLine(Point2D start, Point2D end, Color color);
    }
}
