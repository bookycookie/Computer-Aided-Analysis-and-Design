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

            var start = VectorBuilder.Dense(new[] {1.0, 1.0});
            var A = MatrixBuilder.Dense(2, 2, new[] {0.0, -1.0, 1.0, 0.0});
            var B = MatrixBuilder.Dense(2, 2);
            Func<double, double> rt = _ => 0.0;

            // var eulerError = Euler(t, tmax, T, start, A, B, rt, verbose: true);
            // var reverseEulerError = ReverseEuler(t, tmax, T, start, A, B, rt, verbose: true);
            // var trapezoidError = Trapezoid(t, tmax, T, start, A, B, rt, verbose: true);
            // var rungeKuttaError = RungeKutta(t, tmax, T, start, A, B, rt, verbose: true);
        }

        private static double Euler(double time, double tmax, double T, Vector<double> start, Matrix<double> A,
            Matrix<double> B, Func<double, double> r, bool verbose = false)
        {
            var x = VectorBuilder.DenseOfVector(start);
            var cumulativeError = 0.0;
            for (var t = T; t <= tmax; t += T)
            {
                var pendulum = Generator.GeneratePendulum(start, t);
                x += T * A * x;

                cumulativeError += (x - pendulum).L2Norm();

                if (verbose)
                    Console.WriteLine(
                        $"t: {t:F2} -- x: [{x[0]:g4} {x[1]:g4}] -- pendulum: [{pendulum[0]:g4} {pendulum[1]:g4}]");
            }

            if (verbose)
                Console.WriteLine($"\nCumulative error: {cumulativeError}");
            return cumulativeError;
        }

        private static double ReverseEuler(double time, double tmax, double T, Vector<double> start, Matrix<double> A,
            Matrix<double> B, Func<double, double> r, bool verbose = false)
        {
            var U = MatrixBuilder.DenseIdentity(A.ColumnCount);
            var P = (U - A * T).Inverse();
            var Q = P * T * B;

            var x = VectorBuilder.DenseOfVector(start);
            var cumulativeError = 0.0;
            for (var t = T; t <= tmax; t += T)
            {
                var pendulum = Generator.GeneratePendulum(start, t);
                x = P * x + Q * VectorBuilder.Dense(new[] { r(t), r(t) });
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
            Matrix<double> B, Func<double, double> r, bool verbose = false)
        {
            var U = MatrixBuilder.DenseIdentity(A.ColumnCount);
            var R = (U - A * 0.5 * T).Inverse() * (U + A * 0.5 * T);

            var S = (U - A * 0.5 * T).Inverse() * (0.5 * T) * B;

            var x = VectorBuilder.DenseOfVector(start);
            var cumulativeError = 0.0;
            for (var t = T; t <= tmax; t += T)
            {
                var pendulum = Generator.GeneratePendulum(start, t);
                x = R * x + S * VectorBuilder.Dense(new[] {r(t - T) + r(t), r(t - T) + r(t)});
                cumulativeError += (x - pendulum).L2Norm();

                if (verbose)
                    Console.WriteLine(
                        $"t: {t:F2} -- x: [{x[0]:g4} {x[1]:g4}] -- pendulum: [{pendulum[0]:g4} {pendulum[1]:g4}]");
            }

            if (verbose)
                Console.WriteLine($"\nCumulative error: {cumulativeError}");
            return cumulativeError;
        }

        private static double RungeKutta(double time, double tmax, double T, Vector<double> start, Matrix<double> A,
            Matrix<double> B, Func<double, double> r, bool verbose = false)
        {
            var x = VectorBuilder.DenseOfVector(start);
            var cumulativeError = 0.0;
            for (var t = T; t <= tmax; t += T)
            {
                var pendulum = Generator.GeneratePendulum(start, t);
                var tk = t - T;
                var m1 = A * x + B * VectorBuilder.Dense(new[] {r(tk), r(tk)});
                var m2 = A * (x + (T / 2.0) * m1) + B * VectorBuilder.Dense(new[] { r(tk + (T / 2.0)), r(tk + (T / 2.0)) });
                var m3 = A * (x + (T / 2.0) * m2) + B * VectorBuilder.Dense(new[] { r(tk + (T / 2.0)), r(tk + (T / 2.0)) });
                var m4 = A * (x + T * m3) + B * VectorBuilder.Dense(new[] { r(tk + T), r(tk + T) });

                x += (T / 6.0) * (m1 + 2 * m2 + 2 * m3 + m4);

                cumulativeError += (x - pendulum).L2Norm();
                if (verbose)
                    Console.WriteLine(
                        $"t: {t:F2} -- x: [{x[0]:g4} {x[1]:g4}] -- pendulum: [{pendulum[0]:g4} {pendulum[1]:g4}]");
            }

            if (verbose)
                Console.WriteLine($"\nCumulative error: {cumulativeError}");
            return cumulativeError;
        }
    }
}