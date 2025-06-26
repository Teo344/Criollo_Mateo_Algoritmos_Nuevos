using Criollo_Mateo_Algoritmos_Completos.Dominio.Interfaces;
using Criollo_Mateo_Algoritmos_Completos.Entidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Criollo_Mateo_Algoritmos_Completos.Dominio.Algoritmos
{
    public class SkilineFill : IFillAlgorithm
    {
        private readonly List<Point2D> _vertices;

        public SkilineFill(List<Point2D> vertices)
        {
            _vertices = vertices;
        }

        public async Task FillAsync(Bitmap bitmap, Point2D startPoint, Color targetColor, Color replacementColor, Action<Point2D, Bitmap, List<Point2D>> progressCallback, int delay)
        {
            int minY = int.MaxValue, maxY = int.MinValue;

            foreach (var p in _vertices)
            {
                if (p.Y < minY) minY = (int)p.Y;
                if (p.Y > maxY) maxY = (int)p.Y;
            }

            List<Point2D> puntosRelleno = new List<Point2D>();

            for (int y = minY; y <= maxY; y++)
            {
                List<int> intersecciones = new List<int>();

                for (int i = 0; i < _vertices.Count; i++)
                {
                    Point2D p1 = _vertices[i];
                    Point2D p2 = _vertices[(i + 1) % _vertices.Count];

                    if (p1.Y == p2.Y)
                        continue;

                    if (y >= Math.Min(p1.Y, p2.Y) && y < Math.Max(p1.Y, p2.Y))
                    {
                        int x = (int)(p1.X + (y - p1.Y) * (p2.X - p1.X) / (p2.Y - p1.Y));
                        intersecciones.Add(x);
                    }
                }

                intersecciones.Sort();

                for (int i = 0; i < intersecciones.Count - 1; i += 2)
                {
                    for (int x = intersecciones[i]; x <= intersecciones[i + 1]; x++)
                    {
                        if (x >= 0 && y >= 0 && x < bitmap.Width && y < bitmap.Height)
                        {
                            bitmap.SetPixel(x, y, replacementColor);
                            var punto = new Point2D(x, y);
                            puntosRelleno.Add(punto);
                            progressCallback(punto, bitmap, puntosRelleno);
                            await Task.Delay(delay);
                        }
                    }
                }
            }
        }
    }


}
