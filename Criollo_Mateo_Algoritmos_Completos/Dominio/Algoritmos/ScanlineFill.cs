using Criollo_Mateo_Algoritmos_Completos.Dominio.Interfaces;
using Criollo_Mateo_Algoritmos_Completos.Entidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criollo_Mateo_Algoritmos_Completos.Dominio.Algoritmos
{
    public class ScanlineFill : IFillAlgorithm
    {
        public async Task FillAsync(Bitmap bitmap, Point2D startPoint, Color targetColor, Color replacementColor,
            Action<Point2D, Bitmap, List<Point2D>> progressCallback, int delay)
        {
            int w = bitmap.Width, h = bitmap.Height;
            bool[,] visited = new bool[w, h];
            Stack<Point> stack = new Stack<Point>();
            List<Point2D> painted = new List<Point2D>();

            stack.Push(new Point((int)startPoint.X, (int)startPoint.Y));
            while (stack.Count > 0)
            {
                var pt = stack.Pop();
                int x = pt.X, y = pt.Y;
                if (x < 0 || x >= w || y < 0 || y >= h || visited[x, y]) continue;
                if (bitmap.GetPixel(x, y).ToArgb() != targetColor.ToArgb()) continue;

                int x1 = x, x2 = x;
                while (x1 - 1 >= 0 && bitmap.GetPixel(x1 - 1, y).ToArgb() == targetColor.ToArgb())
                    x1--;
                while (x2 + 1 < w && bitmap.GetPixel(x2 + 1, y).ToArgb() == targetColor.ToArgb())
                    x2++;

                for (int i = x1; i <= x2; i++)
                {
                    bitmap.SetPixel(i, y, replacementColor);
                    visited[i, y] = true;
                    painted.Add(new Point2D(i, y));
                }

                stack.Push(new Point(x1, y - 1));
                stack.Push(new Point(x2, y - 1));
                stack.Push(new Point(x1, y + 1));
                stack.Push(new Point(x2, y + 1));

                progressCallback?.Invoke(new Point2D(x, y), bitmap, new List<Point2D>(painted));
                if (delay > 0) await Task.Delay(delay);
            }
        }
    }

}
