using System;
using MathNet.Numerics.LinearAlgebra;

namespace Homework_5
{
    class Program
    {
        static void Main(string[] args)
        {
            Assignment1();
        }

        private static void Assignment1()
        {
            const double T = 0.01;
            const int tmax = 10;
            var t = 0.0;

            var V = Vector<double>.Build;
            var M = Matrix<double>.Build;
            
            var start = V.Dense(new[] { 1.0, 1.0 });
            var x = V.DenseOfVector(start);
            var A = M.Dense(2, 2, new[] { 0.0, -1.0, 1.0, 0.0 });
            var cumulativeError = 0.0;
            while (t < tmax)
            {
                var pendulum = Generator.GeneratePendulum(start, t);
                cumulativeError += (x - pendulum).L2Norm();
                x += T * A * x;
                t += T;
                
                Console.WriteLine($"t: {t:F2} -- x: [{x[0]:g4} {x[1]:g4}] -- pendulum: [{pendulum[0]:g4} {pendulum[1]:g4}]");
            }
            Console.WriteLine($"\nCumulative error: {cumulativeError}");
        }
        
    }
}