using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Project1
{
    class Program
    {
        static void Main()
        {
            string filePath = "C:\\Users\\luis_\\source\\repos\\Project1\\Project1\\circulo.png"; // Ruta de imagen

            // Convertir la imagen a un vector binario
            int[] inputPattern = ConvertImageToPattern(filePath);
            int size = inputPattern.Length;
            HopfieldNet network = new HopfieldNet(size);

            network.Train(inputPattern);

            int[] outputPattern = inputPattern;
            for (int i = 0; i < 10; i++)
            {
                outputPattern = network.Update(outputPattern);
            }

            Console.WriteLine("Patrón de entrada:");
            PrintPattern(inputPattern);
            Console.WriteLine("Patrón recuperado:");
            PrintPattern(outputPattern);

            // Mantener la consola abierta hasta que se presione una tecla
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }

        static int[] ConvertImageToPattern(string filePath)
        {
            int newSize = 100; // Tamaño fijo de la imagen 10x10
            using (Bitmap bitmap = new Bitmap(filePath))
            {
                using (Bitmap resizedBitmap = new Bitmap(newSize, newSize))
                {
                    using (Graphics graphics = Graphics.FromImage(resizedBitmap))
                    {
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.DrawImage(bitmap, 0, 0, newSize, newSize);
                    }

                    int[] pattern = new int[newSize * newSize];
                    for (int y = 0; y < newSize; y++)
                    {
                        for (int x = 0; x < newSize; x++)
                        {
                            Color pixel = resizedBitmap.GetPixel(x, y);
                            int grayscale = (pixel.R + pixel.G + pixel.B) / 3;
                            pattern[y * newSize + x] = grayscale < 128 ? -1 : 1;
                        }
                    }
                    return pattern;
                }
            }
        }

        static void PrintPattern(int[] pattern)
        {
            int width = (int)Math.Sqrt(pattern.Length);
            for (int i = 0; i < pattern.Length; i++)
            {
                if (i % width == 0)
                {
                    Console.WriteLine();
                }
                Console.Write(pattern[i] == 1 ? "#" : " ");
            }
            Console.WriteLine();
        }
    }
}