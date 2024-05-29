using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;



namespace Project1
{
    class HopfieldNet
    {
        private int[,] weights;
        private int size;

        public HopfieldNet(int size)
        {
            this.size = size;
            this.weights = new int[size, size];
        }

        // Entrenamiento de la red
        public void Train(int[] patron)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i != j)
                    {
                        weights[i, j] += patron[i] * patron[j];
                    }
                }
            }
        }

        // Actualización de la red
        public int[] Update(int[] patron)
        {
            int[] nuevoPatron = new int[size];
            patron.CopyTo(nuevoPatron, 0);

            for (int i = 0; i < size; i++)
            {
                int sum = 0;
                for (int j = 0; j < size; j++)
                {
                    sum += weights[i, j] * nuevoPatron[j];
                }
                nuevoPatron[i] = sum >= 0 ? 1 : -1;
            }
            return nuevoPatron;
        }
    }
}
