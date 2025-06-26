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
    public class FloodFill : IFillAlgorithm
    {
        public async Task FillAsync(Bitmap bitmap, Point2D startPoint, Color targetColor, Color replacementColor, Action<Point2D, Bitmap, List<Point2D>> progressCallback, int delay)
        {
            Stack<Point2D> stack = new Stack<Point2D>();
            HashSet<(int, int)> visited = new HashSet<(int, int)>();
            List<Point2D> filledPixels = new List<Point2D>();

            int width = bitmap.Width;
            int height = bitmap.Height;

            stack.Push(startPoint);

            while (stack.Count > 0)
            {
                Point2D p = stack.Pop();
                int x = (int)p.X;
                int y = (int)p.Y;

                if (x < 0 || x >= width || y < 0 || y >= height) continue;
                if (visited.Contains((x, y))) continue;
                if (bitmap.GetPixel(x, y).ToArgb() != targetColor.ToArgb()) continue;

                bitmap.SetPixel(x, y, replacementColor);
                filledPixels.Add(new Point2D(x, y));
                visited.Add((x, y));

                progressCallback?.Invoke(p, bitmap, filledPixels);
                await Task.Delay(delay);

                stack.Push(new Point2D(x - 1, y)); // Oeste
                stack.Push(new Point2D(x, y + 1)); // Sur
                stack.Push(new Point2D(x + 1, y)); // Este
                stack.Push(new Point2D(x, y - 1)); // Norte
            }
        }
    }



}
