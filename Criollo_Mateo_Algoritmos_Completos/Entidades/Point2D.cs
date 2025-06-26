using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criollo_Mateo_Algoritmos_Completos.Entidades
{
    public struct Point2D
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Point2D(float x, float y)
        {
            X = x;
            Y = y;
        }


        public PointF ToPointF()
        {
            return new PointF(X, Y);
        }

        public void leer(float x, float y)
        {
            X = x;
            Y = y;
        }
    }


}
