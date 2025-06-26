using Criollo_Mateo_Algoritmos_Completos.Dominio;
using Criollo_Mateo_Algoritmos_Completos.Dominio.Algoritmos;
using Criollo_Mateo_Algoritmos_Completos.Dominio.Interfaces;
using Criollo_Mateo_Algoritmos_Completos.Entidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Criollo_Mateo_Algoritmos_Completos.Aplicacion
{
    public class DrawingManager
    {
        private readonly ILineAlgorithm _lineAlgorithm;
        private readonly ICircleAlgorithm _circleAlgorithm;
        private readonly IFillAlgorithm _fillAlgorithm;
        private readonly PolygonGenerator _polygonGenerator;

        private Bitmap _buffer;

        public Bitmap GetBuffer()
        {
            return _buffer;
        }
        public DrawingManager(ILineAlgorithm lineAlgorithm)
        {
            _lineAlgorithm = lineAlgorithm;
        }

        public DrawingManager(ICircleAlgorithm circleAlgorithm)
        {
            _circleAlgorithm = circleAlgorithm; 
        }
        public DrawingManager(IFillAlgorithm fillAlgorithm, PictureBox canvas)
        {
            _polygonGenerator = new PolygonGenerator();
            _fillAlgorithm = fillAlgorithm;
            _buffer = new Bitmap(canvas.Width, canvas.Height); // Usa el tamaño real del canvas
        }



        public void drawPaint(Point2D punto, Color color, PictureBox canvas, int size)
        {
            if (canvas.Image == null)
            {
                canvas.Image = new Bitmap(canvas.Width, canvas.Height);
            }

            using (Graphics g = Graphics.FromImage(canvas.Image))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                int radio = size / 2;
                float x = punto.X - radio;
                float y = punto.Y - radio;

                // Círculo relleno
                using (SolidBrush brush = new SolidBrush(color))
                {
                    g.FillEllipse(brush, x, y, size, size);
                }   
 
                
                    using (Pen pen = new Pen(Color.Black, 1))
                    {
                        g.DrawEllipse(pen, x, y, size, size);
                    }
                
            }

            canvas.Invalidate();
        }



        public async Task DrawPixelsAsync(Point2D start, Point2D end, PictureBox canvas)
        {

            var pixels = _lineAlgorithm.DrawLine(start, end, Color.Blue);

            using (Graphics g = canvas.CreateGraphics())
            {

                for (int i = 0; i < pixels.Count; i++)
                {
                    var pixel = pixels[i];


                    float pixelX = (pixel.Position.X);
                    float pixelY = (pixel.Position.Y);


                    // Dibuja el pixel
                    g.FillRectangle(new SolidBrush(pixel.Color), pixelX, pixelY, 3, 3);
                    await Task.Delay(20);
                }
            }
        }


        private float calcularRadio(Point2D centro, Point2D borde)
        {
            float dx = borde.X - centro.X;
            float dy = borde.Y - centro.Y;

            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        public async Task DrawPixelsAsyncCircle(Point2D center, Point2D borde, PictureBox canvas)
        {

            float radius = calcularRadio(center, borde);


            var pixels = _circleAlgorithm.DrawCircle(center, radius, Color.Black);


            using (Graphics g = canvas.CreateGraphics())
            {
                for (int i = 0; i < pixels.Count; i++)
                {
                    var pixel = pixels[i];

                    float pixelX = pixel.Position.X;
                    float pixelY = pixel.Position.Y;

                    g.FillRectangle(new SolidBrush(pixel.Color), pixelX, pixelY, 3, 3);
                    await Task.Delay(10);
                }
            }

        }

        public void DrawPolygon(Point2D center, float radius, int sides, PictureBox picCanvas)
        {
            var polygon = _polygonGenerator.GenerateRegularPolygon(center, radius, sides);

            if (_buffer == null || _buffer.Width != picCanvas.Width || _buffer.Height != picCanvas.Height)
                _buffer = new Bitmap(picCanvas.Width, picCanvas.Height);

            using (Graphics g = Graphics.FromImage(_buffer))
            {
                Pen pen = new Pen(Color.Red, 2);
                Point[] points = polygon.vertices.Select(v => new Point((int)v.X, (int)v.Y)).ToArray();
                g.DrawPolygon(pen, points);
            }

            picCanvas.Image = _buffer;
        }

        public PolygonFigure devolverPolygono(Point2D center, float radius, int sides)
        {
            var polygon = _polygonGenerator.GenerateRegularPolygon(center, radius, sides);
            return polygon;
        }


        public async Task DrawFloodFill(Bitmap bitmap, Point2D startPoint, Color targetColor, Color fillColor, PictureBox canvas,int delay)
        {
            int count = 0;

            await _fillAlgorithm.FillAsync(bitmap, startPoint, targetColor, fillColor, (point, bmp, pixels) =>
            {
                // Actualiza imagen
                canvas.Image = (Bitmap)bmp.Clone();

                // Actualiza tabla con el último pixel pintado
                if (count < pixels.Count)
                {
                    var p = pixels[count];
                    count++;
                }
            }, delay);
        }



        public void dibujarLinea(Graphics g, List<Point2D> puntos, bool esRecorte)
        {
            if (puntos == null || puntos.Count < 2)
                return;

            Pen pen = esRecorte ? new Pen(Color.Red, 2) : new Pen(Color.Blue, 2);

            for (int i = 0; i < puntos.Count - 1; i++)
            {
                g.DrawLine(pen, (int)puntos[i].X, (int)puntos[i].Y, (int)puntos[i + 1].X, (int)puntos[i + 1].Y);
            }

            if (esRecorte)
            {
                g.DrawLine(pen, (int)puntos.Last().X, (int)puntos.Last().Y, (int)puntos[0].X, (int)puntos[0].Y);
            }
        }



        public void dibujarCompleto(Graphics g, List<Point2D> puntos)
        {
            using (Pen lapiz = new Pen(Color.Gray, 2))
            {
                for (int i = 0; i < puntos.Count - 1; i += 2)
                {
                    g.DrawLine(lapiz, puntos[i].X, puntos[i].Y, puntos[i + 1].X, puntos[i + 1].Y);
                }
            }
        }

        public void dibujarRecortado(Graphics g, List<Point2D> puntos)
        {
            using (Pen lapiz = new Pen(Color.Red, 2))
            {
                for (int i = 0; i < puntos.Count - 1; i += 2)
                {
                    g.DrawLine(lapiz, puntos[i].X, puntos[i].Y, puntos[i + 1].X, puntos[i + 1].Y);
                }
            }
        }

        public void ClearAll(PictureBox canvas)
        {
            if (_buffer != null)
            {
                _buffer.Dispose();
                _buffer = null;
            }
            Bitmap blank = new Bitmap(canvas.Width, canvas.Height);
            canvas.Image = blank;

        }



    }
}
