/*using AForge.Imaging;
using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

public class HoughTransform
{
    private Bitmap image;
    private Bitmap edges;
    private CannyEdgeDetector edgeDetector;

    public HoughTransform(string imagePath)
    {
        // Cargar la imagen
        image = new Bitmap(imagePath);

        // Verificar y convertir el formato de la imagen si es necesario
        if (image.PixelFormat != PixelFormat.Format24bppRgb)
        {
            Bitmap temp = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(temp))
            {
                g.DrawImage(image, 0, 0, image.Width, image.Height);
            }
            image.Dispose();
            image = temp;
        }

        // Inicializar el detector de bordes
        edgeDetector = new CannyEdgeDetector();
    }

    public void ApplyEdgeDetection()
    {
        // Aplicar el filtro de bordes (Canny)
        edges = edgeDetector.Apply(image);
    }

    public void HoughTransformRectas()
    {
        // Aplicar la transformada de Hough para rectas
        var lineTransform = new HoughLineTransformation();
        lineTransform.ProcessImage(edges);
        HoughLine[] lines = lineTransform.GetLinesByRelativeIntensity(0.5);

        // Dibujar las líneas detectadas
        using (Graphics g = Graphics.FromImage(image))
        {
            foreach (var line in lines)
            {
                // Calcular las coordenadas de las líneas
                int r = line.Radius;
                double t = line.Theta;
                double cosT = Math.Cos(t);
                double sinT = Math.Sin(t);
                int x0 = (int)(r * cosT);
                int y0 = (int)(r * sinT);
                int x1 = (int)(x0 + 1000 * (-sinT));
                int y1 = (int)(y0 + 1000 * (cosT));
                int x2 = (int)(x0 - 1000 * (-sinT));
                int y2 = (int)(y0 - 1000 * (cosT));

                // Dibujar la línea
                g.DrawLine(new Pen(Color.Red, 2), new Point(x1, y1), new Point(x2, y2));
            }
        }
    }

    public void HoughTransformCircunferencias(int minRadius, int maxRadius)
    {
        // Implementación de la transformada de Hough para circunferencias
        List<Circle> circles = new List<Circle>();
        int width = edges.Width;
        int height = edges.Height;

        // Bucle a través de todos los píxeles de la imagen de bordes
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (edges.GetPixel(x, y).R == 255)
                {
                    for (int r = minRadius; r <= maxRadius; r++)
                    {
                        for (int t = 0; t < 360; t++)
                        {
                            double theta = t * Math.PI / 180;
                            int a = (int)(x - r * Math.Cos(theta));
                            int b = (int)(y - r * Math.Sin(theta));

                            if (a >= 0 && a < width && b >= 0 && b < height)
                            {
                                circles.Add(new Circle { X = a, Y = b, Radius = r });
                            }
                        }
                    }
                }
            }
        }

        // Dibujar las circunferencias detectadas
        using (Graphics g = Graphics.FromImage(image))
        {
            foreach (var circle in circles)
            {
                g.DrawEllipse(new Pen(Color.Blue, 2), circle.X - circle.Radius, circle.Y - circle.Radius, circle.Radius * 2, circle.Radius * 2);
            }
        }
    }

    public void SaveResult(string outputPath)
    {
        image.Save(outputPath);
    }

    private class Circle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Radius { get; set; }
    }
}
*/

using System;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging;
using AForge.Imaging.Filters;

public class HoughTransform
{
    private Bitmap image;
    private Bitmap edges;
    private CannyEdgeDetector edgeDetector;

    public HoughTransform(string imagePath)
    {
        // Cargar la imagen
        try
        {
            image = new Bitmap(imagePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al cargar la imagen: " + ex.Message);
            return;
        }

        // Verificar y convertir el formato de la imagen si es necesario
        if (image.PixelFormat != PixelFormat.Format24bppRgb)
        {
            try
            {
                Bitmap temp = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
                using (Graphics g = Graphics.FromImage(temp))
                {
                    g.DrawImage(image, 0, 0, image.Width, image.Height);
                }
                image.Dispose();
                image = temp;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al convertir el formato de la imagen: " + ex.Message);
                return;
            }
        }

            // Inicializar el detector de bordes
            edgeDetector = new CannyEdgeDetector();
    }

    public void ApplyEdgeDetection()
    {
        // Aplicar el filtro de detección de bordes Canny
        try
        {
            edges = edgeDetector.Apply(image);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al aplicar la detección de bordes: " + ex.Message);
            return;
        }
    }

    public void HoughTransformRectas()
    {

        // Aplicar la transformada de Hough para líneas
        var lineTransform = new HoughLineTransformation();
        try
        {
            lineTransform.ProcessImage(edges);
            HoughLine[] lines = lineTransform.GetLinesByRelativeIntensity(0.5);

            // Dibujar las líneas detectadas
            using (Graphics g = Graphics.FromImage(image))
            {
                foreach (HoughLine line in lines)
                {
                    // Calcular las coordenadas de las líneas
                    int r = line.Radius;
                    double t = line.Theta;
                    double cosT = Math.Cos(t);
                    double sinT = Math.Sin(t);
                    int x0 = (int)(r * cosT);
                    int y0 = (int)(r * sinT);
                    int x1 = (int)(x0 + 1000 * (-sinT));
                    int y1 = (int)(y0 + 1000 * (cosT));
                    int x2 = (int)(x0 - 1000 * (-sinT));
                    int y2 = (int)(y0 - 1000 * (cosT));

                    // Dibujar la línea
                    g.DrawLine(new Pen(Color.Red, 2), new Point(x1, y1), new Point(x2, y2));
                }
            }
        } catch(Exception ex)
        {
            Console.WriteLine("Error al aplicar la transformada de Hough: " + ex.Message);
        }
    }

    public void SaveResult(string outputPath)
    {
        image.Save(outputPath);
    }
}
