using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpolationForZalik
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите первое число из размера изображения ");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите второе число из размера изображения ");
            int m = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите матрицу");
            string matrix = Console.ReadLine();

            string [] matrix1 = matrix.Split(';');
            List<List<int>> matrixAll = matrix1.Select(t => t.Split(' ')).Select(s => s.Select(t1 => Convert.ToInt32(t1)).ToList()).ToList();

            Console.ReadKey();
        }
    }
}
