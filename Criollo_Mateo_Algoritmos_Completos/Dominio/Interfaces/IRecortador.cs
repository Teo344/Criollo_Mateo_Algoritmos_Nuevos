using Criollo_Mateo_Algoritmos_Completos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criollo_Mateo_Algoritmos_Completos.Dominio.Interfaces
{
    public interface IRecortador
    {
        List<Point2D> RecortarLineas(List<Point2D> segmentos, List<Point2D> ventana);
        List<Point2D> RecortarPoligono(List<Point2D> poligono, List<Point2D> ventana);
    }
}
