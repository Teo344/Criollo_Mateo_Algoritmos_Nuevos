using Criollo_Mateo_Algoritmos_Completos.Entidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criollo_Mateo_Algoritmos_Completos.Dominio.Interfaces
{
    public interface IEllipseAlgorithm
    {
        List<Pixel> DrawEllipse(Point2D center, float rx, float ry, Color color);
    }
}
