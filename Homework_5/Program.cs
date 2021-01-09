using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Homework_5
{
    class Program
    {
        private static readonly VectorBuilder<double> VectorBuilder = Vector<double>.Build;
        private static readonly MatrixBuilder<double> MatrixBuilder = Matrix<double>.Build;

        static void Main(string[] args)
        {
            Assignment1();
        }

        private static void Assignment1()
        {
            const double T = 0.01;
            const int tmax = 10;
            const double t = 0.0;

            var start = VectorBuilder.Dense(new[] { 1.0, 1.0 });
            var A = MatrixBuilder.Dense(2, 2, new[] { 0.0, -1.0, 1.0, 0.0 });
            Func<double, double> rt = _ => 0.0;
            
            // var eulerError = Euler(t, tmax, T, start, A, verbose: true);
            // var reverseEulerError = ReverseEuler(t, tmax, T, start, A, r: rt, verbose: true);
            var trapezoidError = Trapezoid(t, tmax, T, start, A, verbose: true);
        }

        private static double Euler(double time, double tmax, double T, Vector<double> start, Matrix<double> A,
            Matrix<double> B = null, Func<double, double> r = null, bool verbose = false)
        {
            var x = VectorBuilder.DenseOfVector(start);
            var cumulativeError = 0.0;
            for (var t = T; t <= tmax; t += T)
            {
                var pendulum = Generator.GeneratePendulum(start, t);
                x += T * A * x;
                
                cumulativeError += (x - pendulum).L2Norm();

                if (verbose)
                    Console.WriteLine($"t: {t:F2} -- x: [{x[0]:g4} {x[1]:g4}] -- pendulum: [{pendulum[0]:g4} {pendulum[1]:g4}]");
            }

            if (verbose)
                Console.WriteLine($"\nCumulative error: {cumulativeError}");
            return cumulativeError;
        }

        private static double ReverseEuler(double time, double tmax, double T, Vector<double> start, Matrix<double> A,
            Matrix<double> B = null, Func<double, double> r = null, bool verbose = false)
        {
            var U = MatrixBuilder.DenseIdentity(A.ColumnCount);
            var P = (U - A * T).Inverse();
            var Q = P * T * (B ?? U);

            var x = VectorBuilder.DenseOfVector(start);
            var cumulativeError = 0.0;
            for (var t = T; t <= tmax; t += T)
            {
                var pendulum = Generator.GeneratePendulum(start, t);
                var rt = r?.Invoke(t) ?? 0;
                var rtVector = VectorBuilder.Dense(new []{rt, rt});
                x = P * x + Q * rtVector;
                cumulativeError += (x - pendulum).L2Norm();

                if (verbose)
                    Console.WriteLine(
                        $"t: {t:F2} -- x: [{x[0]:g4} {x[1]:g4}] -- pendulum: [{pendulum[0]:g4} {pendulum[1]:g4}]");
            }

            if (verbose)
                Console.WriteLine($"\nCumulative error: {cumulativeError}");
            return cumulativeError;
        }

        private static double Trapezoid(double time, double tmax, double T, Vector<double> start, Matrix<double> A,
            Matrix<double> B = null, Func<double, double> r = null, bool verbose = false)
        {
            var U = MatrixBuilder.DenseIdentity(A.ColumnCount);
            var R = (U - A * 0.5 * T).Inverse() * (U + A * 0.5 * T);

            var S = (U - A * 0.5 * T).Inverse() * (0.5 * T) * (B ?? U);
            
            var x = VectorBuilder.DenseOfVector(start);
            var cumulativeError = 0.0;
            for (var t = T; t <= tmax; t += T)
            {
                var pendulum = Generator.GeneratePendulum(start, t);
                var rt = r?.Invoke(t - T) ?? 0;
                var rtNext = r?.Invoke(t) ?? 0;
                var rtVector = VectorBuilder.Dense(new []{rt + rtNext, rt + rtNext});
                x = R * x + S * rtVector;
                cumulativeError += (x - pendulum).L2Norm();

                if (verbose)
                    Console.WriteLine($"t: {t:F2} -- x: [{x[0]:g4} {x[1]:g4}] -- pendulum: [{pendulum[0]:g4} {pendulum[1]:g4}]");
            }

            if (verbose)
                Console.WriteLine($"\nCumulative error: {cumulativeError}");
            return cumulativeError;
        }

    }
}