using System;

class Program
{
    static void Main(string[] args)
    {
        string imagePath = "D:\\Luis\\Documents\\Visual Studio 2022\\Projects\\ConsoleApp1\\images\\bloque_motor_2.png"; // ruta de imagen
        string outputPath = "D:\\Luis\\Documents\\Visual Studio 2022\\Projects\\ConsoleApp1\\images\\output.png"; // la ruta donde guarda la imagen procesada

        /*
                HoughTransform houghTransform = new HoughTransform(imagePath);

                // Aplicar detección de bordes
                houghTransform.ApplyEdgeDetection();

                // Aplicar la transformada de Hough para rectas
                houghTransform.HoughTransformRectas();

                // Aplicar la transformada de Hough para circunferencias con un rango de radios
                houghTransform.HoughTransformCircunferencias(20, 50);

                // Guardar el resultado
                houghTransform.SaveResult(outputPath);

                Console.WriteLine("Procesamiento completado. Resultado guardado en " + outputPath);
            }
        */


        HoughTransform houghTransform = new HoughTransform(imagePath);
        houghTransform.ApplyEdgeDetection();
        houghTransform.HoughTransformRectas();
        houghTransform.SaveResult(outputPath);

        Console.WriteLine("Transformada de Hough aplicada y resultado guardado.");
    }
}
