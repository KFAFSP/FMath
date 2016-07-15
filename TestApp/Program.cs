using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FMath.Arithmetics;
using FMath.Linear.Generic;
using FMath.Linear.Generic.Immutable;
using FMath.Linear.Static;

namespace TestApp
{
    static class Program
    {
        static void Main(string[] AArguments)
        {
            IVector<int> ivTest = new ArrayVector<int>(new int[]{1, 3, 9});
            
            Console.WriteLine("Vector");
            Console.WriteLine(Vector.Format(ivTest));
            Console.WriteLine(Vector.Format(ivTest, "T"));
            Console.WriteLine(Vector.Format(ivTest, "a"));

            IMatrix<float> imTest = new ArrayMatrix<float>(new float[,]{{1.0f, 0.0f, 0.0f}, {0.0f, 1.0f, 0.0f}, {0.0f, 0.0f, 1.0f}});

            Console.WriteLine();
            Console.WriteLine("Matrix");
            Console.WriteLine(Matrix.Format(imTest));
            Console.WriteLine(Matrix.Format(imTest, "T"));
            Console.WriteLine(Matrix.Format(imTest, "e"));
            Console.WriteLine(Matrix.Format(imTest, "33"));

            Console.WriteLine();
            Console.WriteLine("Arithmetics");
            Console.WriteLine(GenericArithmetics.IsNumeric<float>());
            Console.WriteLine(GenericArithmetics.Add(1.0f, 2.0f));

            Console.ReadLine();
        }
    }
}
