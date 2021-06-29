using System;

namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] e = {{1, 2}, {3, 4}};
            Matrix A = new Matrix(2, e);
            Console.WriteLine(A.Rows);
            Console.WriteLine(A.Columns);

            Matrix B = A * new Matrix(2, e);

            Console.WriteLine(A.ToString());
        }
    }
}
