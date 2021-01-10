using System;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Homework_1
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            Console.WriteLine("Zadatak 2 LUP:");
            var zadatak2 =  ReadMatrixFromFile(@"C:\Faks\APR\Homework_1\Files\Zadatak2.txt");
            var vector2 = new Matrix(new double[3, 1] {{12}, {12}, {1}});
            var permutationMatrix2 = Matrix.LUPDecomposition(zadatak2);
            var permutationVector2 = permutationMatrix2 * vector2;
            var fwd2 = Matrix.ForwardSubstitution(zadatak2, permutationVector2);
            var bck2 = Matrix.BackwardSubstitution(zadatak2, fwd2);
            var solution2 = bck2;
            Console.WriteLine(solution2);
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            
            Console.WriteLine("Zadatak 2 LU:");
            var zadatak2Lu =  ReadMatrixFromFile(@"C:\Faks\APR\Homework_1\Files\Zadatak2.txt");
            var vector2Lu = new Matrix(new double[3, 1] {{12}, {12}, {1}});
            var permutationMatrix2Lu = Matrix.LUPDecomposition(zadatak2Lu);
            var permutationVector2Lu = permutationMatrix2Lu * vector2Lu;
            var fwd2Lu = Matrix.ForwardSubstitution(zadatak2Lu, permutationVector2Lu);
            var bck2Lu = Matrix.BackwardSubstitution(zadatak2Lu, fwd2Lu);
            var solution2Lu = bck2Lu;
            Console.WriteLine(solution2Lu);
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX");


            Console.WriteLine("Zadatak 3 LUP:");
            var zadatak3 = ReadMatrixFromFile(@"C:\Faks\APR\Homework_1\Files\Zadatak3.txt");
            var vector3 = new Matrix(new double[3, 1] {{4}, {2}, {3}});
            var permutationMatrix3 = Matrix.LUPDecomposition(zadatak3);
            var permutationVector3 = permutationMatrix3 * vector3;
            var fwd3 = Matrix.ForwardSubstitution(zadatak3, permutationVector3);
            var bck3 = Matrix.BackwardSubstitution(zadatak3, fwd3);
            var solution3 = bck3;
            Console.WriteLine(solution3);
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            
            Console.WriteLine("Zadatak 3 LU:");
            var zadatak3Lu = ReadMatrixFromFile(@"C:\Faks\APR\Homework_1\Files\Zadatak3.txt");
            var vector3Lu = new Matrix(new double[3, 1] {{4}, {2}, {3}});
            var permutationMatrix3Lu = Matrix.LUPDecomposition(zadatak3Lu);
            var permutationVector3Lu = permutationMatrix3Lu * vector3Lu;
            var fwd3Lu = Matrix.ForwardSubstitution(zadatak3Lu, permutationVector3Lu);
            var bck3Lu = Matrix.BackwardSubstitution(zadatak3Lu, fwd3Lu);
            var solution3Lu = bck3Lu;
            Console.WriteLine(solution3Lu);
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX");

            Console.WriteLine("Zadatak 4 LUP:");
            var zadatak4 = ReadMatrixFromFile(@"C:\Faks\APR\Homework_1\Files\Zadatak4.txt");
            var vector4 = new Matrix(new double[3, 1] {{12000000.000001}, {14000000}, {10000000}});
            var permutationMatrix4 = Matrix.LUPDecomposition(zadatak4);
            var permutationVector4 = permutationMatrix4 * vector4;
            var fwd4 = Matrix.ForwardSubstitution(zadatak4, permutationVector4);
            var bck4 = Matrix.BackwardSubstitution(zadatak4, fwd4);
            var solution4 = bck4;
            Console.WriteLine(solution4);
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            
            Console.WriteLine("Zadatak 4 LU:");
            var zadatak4Lu = ReadMatrixFromFile(@"C:\Faks\APR\Homework_1\Files\Zadatak4.txt");
            var vector4Lu = new Matrix(new double[3, 1] {{12000000.000001}, {14000000}, {10000000}});
            var permutationMatrix4Lu = Matrix.LUPDecomposition(zadatak4Lu);
            var permutationVector4Lu = permutationMatrix4Lu * vector4Lu;
            var fwd4Lu = Matrix.ForwardSubstitution(zadatak4Lu, permutationVector4Lu);
            var bck4Lu = Matrix.BackwardSubstitution(zadatak4Lu, fwd4Lu);
            var solution4Lu = bck4Lu;
            Console.WriteLine(solution4Lu);
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX");

            Console.WriteLine("Zadatak 5 LUP:");
            var zadatak5 = ReadMatrixFromFile(@"C:\Faks\APR\Homework_1\Files\Zadatak5.txt");
            var vector5 = new Matrix(new double[3, 1] {{6}, {9}, {3}});
            var permutationMatrix5 = Matrix.LUPDecomposition(zadatak5);
            var permutationVector5 = permutationMatrix5 * vector5;
            var fwd5 = Matrix.ForwardSubstitution(zadatak5, permutationVector5);
            var bck5 = Matrix.BackwardSubstitution(zadatak5, fwd5);
            var solution5 = bck5;
            Console.WriteLine(solution5);
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            
            Console.WriteLine("Zadatak 5 LU:");
            var zadatak5Lu = ReadMatrixFromFile(@"C:\Faks\APR\Homework_1\Files\Zadatak5.txt");
            var vector5Lu = new Matrix(new double[3, 1] {{6}, {9}, {3}});
            var permutationMatrix5Lu = Matrix.LUPDecomposition(zadatak5Lu);
            var permutationVector5Lu = permutationMatrix5Lu * vector5Lu;
            var fwd5Lu = Matrix.ForwardSubstitution(zadatak5Lu, permutationVector5Lu);
            var bck5Lu = Matrix.BackwardSubstitution(zadatak5Lu, fwd5Lu);
            var solution5Lu = bck5Lu;
            Console.WriteLine(solution5Lu);
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX");

            Console.WriteLine("Zadatak 6 LUP:");
            var zadatak6 = ReadMatrixFromFile(@"C:\Faks\APR\Homework_1\Files\Zadatak6.txt");
            var vector6 = new Matrix(new double[3, 1] {{9000000000}, {15}, {0.0000000015}});
            var permutationMatrix6 = Matrix.LUPDecomposition(zadatak6);
            var permutationVector6 = permutationMatrix6 * vector6;
            var fwd6 = Matrix.ForwardSubstitution(zadatak6, permutationVector6);
            var bck6 = Matrix.BackwardSubstitution(zadatak6, fwd6);
            var solution6 = bck6;
            Console.WriteLine(solution6);
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            
            Console.WriteLine("Zadatak 6 LU:");
            var zadatak6Lu = ReadMatrixFromFile(@"C:\Faks\APR\Homework_1\Files\Zadatak6.txt");
            var vector6Lu = new Matrix(new double[3, 1] {{9000000000}, {15}, {0.0000000015}});
            var permutationMatrix6Lu = Matrix.LUDecomposition(zadatak6Lu);
            var permutationVector6Lu = permutationMatrix6Lu * vector6Lu;
            var fwd6Lu = Matrix.ForwardSubstitution(zadatak6Lu, permutationVector6Lu);
            var bck6Lu = Matrix.BackwardSubstitution(zadatak6Lu, fwd6Lu);
            var solution6Lu = bck6Lu;
            Console.WriteLine(solution6Lu);
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX");

            Console.WriteLine("Zadatak 7:");
            var zadatak7 = ReadMatrixFromFile(@"C:\Faks\APR\Homework_1\Files\Zadatak7.txt");
            var zad7Inverse = Matrix.Inverse(zadatak7);
            var result = zad7Inverse?.ToString();
            result = string.IsNullOrEmpty(result)
                ? "The result was null because the determinant is 0."
                : "The result wasn't null.";
            Console.WriteLine(result);
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX");

            Console.WriteLine("Zadatak 8:");
            var zadatak8 = ReadMatrixFromFile(@"C:\Faks\APR\Homework_1\Files\Zadatak8.txt");
            var zad8Inverse = Matrix.Inverse(zadatak8);
            Console.WriteLine(zad8Inverse);
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX");

            Console.WriteLine("Zadatak 9:");
            var zadatak9 = ReadMatrixFromFile(@"C:\Faks\APR\Homework_1\Files\Zadatak9.txt");
            var zad9Det = Matrix.Determinant(zadatak9);
            Console.WriteLine(zad9Det);
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX");

            Console.WriteLine("Zadatak 10:");
            var zadatak10 = ReadMatrixFromFile(@"C:\Faks\APR\Homework_1\Files\Zadatak10.txt");
            var zad10Det = Matrix.Determinant(zadatak10);
            Console.WriteLine(zad10Det);
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX");

        }

        private static void PrintMatrixToFile(string path, Matrix matrix, string fileName)
        {
            using var sw = new StreamWriter($"{path}\\{fileName}.txt");
            
            
            for (var i = 0; i < matrix.Elements.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.Elements.GetLength(1); j++)
                {
                    sw.Write($" {matrix.Elements[i, j]}");
                }
                sw.Write("\n");
            }
            sw.Flush();
            sw.Close();
        }

        private static Matrix ReadMatrixFromFile(string path)
        {
            var rows = File.ReadAllLines(path);
            var cols = rows[0].Trim().Split(' ');
            var result = new double[rows.Length, cols.Length];
            
            var i = 0;
            foreach (var row in rows)
            {
                var j = 0;
                foreach (var col in row.Trim().Split(' '))
                {
                    double.TryParse(col.Trim(), out result[i, j]);
                    j++;
                }
                i++;
            }
            return new Matrix(result);
        }
    }
}