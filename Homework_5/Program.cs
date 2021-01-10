using System;
using System.Collections.Generic;
using Homework_5.Helpers;
using MathNet.Numerics.LinearAlgebra;

namespace Homework_5
{
    class Program
    {
        private static readonly VectorBuilder<double> VectorBuilder = Vector<double>.Build;
        private static readonly MatrixBuilder<double> MatrixBuilder = Matrix<double>.Build;

        static void Main(string[] args)
        {
            Assignment1(correctionLoop: 2, verbose: false, saveToFile: false);
            // Assignment2(correctionLoop: 2, verbose: false, saveToFile: false);
            // Assignment3(correctionLoop: 2, verbose: false, saveToFile: false);
            // Assignment4(correctionLoop: 2, verbose: false, saveToFile: false);
            
        }

        private static void Assignment1(int correctionLoop, double T = 0.01, int tmax = 10, bool verbose = true,
            string assignmentPath = "C:/git/Computer-Aided-Analysis-and-Design/Homework_5/Files/Assignment1/", bool saveToFile = false)
        {
            Console.WriteLine("ASSIGNMENT 1");
            var start = VectorBuilder.Dense(new[] {1.0, 1.0});
            var A = MatrixBuilder.Dense(2, 2, new[] {0.0, -1.0, 1.0, 0.0});
            var B = MatrixBuilder.Dense(2, 2);
            Func<double, double> rt = _ => 0.0;

            var euler = Euler(assignmentPath + "euler.txt", tmax, T, start, A, B, rt, verbose, saveToFile);
            var reverseEuler = ReverseEuler(assignmentPath + "reverseEuler.txt", tmax, T, start, A, B, rt, verbose, saveToFile);
            var trapezoid = Trapezoid(assignmentPath + "trapezoid.txt", tmax, T, start, A, B, rt, verbose, saveToFile);
            var rungeKutta = RungeKutta(assignmentPath + "rungeKutta.txt", tmax, T, start, A, B, rt, verbose, saveToFile);
            // var pece = Pece(assignmentPath + "pece.txt", tmax, T, start, A, B, rt, verbose, correctionLoop, saveToFile);
            // var pecece = Pecece(assignmentPath + "pecece.txt", tmax, T, start, A, B, rt, verbose, correctionLoop, saveToFile);
        }

        private static void Assignment2(int correctionLoop, double T = 0.1, int tmax = 1, bool verbose = true,
            string assignmentPath = "C:/git/Computer-Aided-Analysis-and-Design/Homework_5/Files/Assignment2/", bool saveToFile = false)
        {
            Console.WriteLine("ASSIGNMENT 2");
            var start = VectorBuilder.Dense(new[] {1.0, -2.0});
            var A = MatrixBuilder.Dense(2, 2, new[] {0.0, -200.0, 1.0, -102.0});
            var B = MatrixBuilder.Dense(2, 2);
            Func<double, double> rt = _ => 0.0;

            var euler = Euler(assignmentPath + "euler.txt", tmax, T, start, A, B, rt, verbose, saveToFile);
            var reverseEuler = ReverseEuler(assignmentPath + "reverseEuler.txt", tmax, T, start, A, B, rt, verbose, saveToFile);
            var trapezoid = Trapezoid(assignmentPath + "trapezoid.txt", tmax, T, start, A, B, rt, verbose, saveToFile);
            var rungeKutta = RungeKutta(assignmentPath + "rungeKutta.txt", tmax, T, start, A, B, rt, verbose, saveToFile);
            // var pece = Pece(assignmentPath + "pece.txt", tmax, T, start, A, B, rt, verbose, correctionLoop, saveToFile);
            // var pecece = Pecece(assignmentPath + "pecece.txt", tmax, T, start, A, B, rt, verbose, correctionLoop, saveToFile);
        }

        private static void Assignment3(int correctionLoop, double T = 0.01, int tmax = 10, bool verbose = true,
            string assignmentPath = "C:/git/Computer-Aided-Analysis-and-Design/Homework_5/Files/Assignment3/", bool saveToFile = false)
        {
            Console.WriteLine("ASSIGNMENT 3");
            var start = VectorBuilder.Dense(new[] {1.0, 3.0});
            var A = MatrixBuilder.Dense(2, 2, new[] {0.0, 1.0, -2.0, -3.0});
            var B = MatrixBuilder.Dense(2, 2, new[] {2.0, 0.0, 0.0, 3.0});
            Func<double, double> rt = _ => 1.0;

            var euler = Euler(assignmentPath + "euler.txt", tmax, T, start, A, B, rt, verbose, saveToFile);
            var reverseEuler = ReverseEuler(assignmentPath + "reverseEuler.txt", tmax, T, start, A, B, rt, verbose, saveToFile);
            var trapezoid = Trapezoid(assignmentPath + "trapezoid.txt", tmax, T, start, A, B, rt, verbose, saveToFile);
            var rungeKutta = RungeKutta(assignmentPath + "rungeKutta.txt", tmax, T*100, start, A, B, rt, verbose, saveToFile);
            // var pece = Pece(assignmentPath + "pece.txt", tmax, T, start, A, B, rt, verbose, correctionLoop, saveToFile);
            // var pecece = Pecece(assignmentPath + "pecece.txt", tmax, T, start, A, B, rt, verbose, correctionLoop, saveToFile);
        }

        private static void Assignment4(int correctionLoop, double T = 0.01, int tmax = 1, bool verbose = true,
            string assignmentPath = "C:/git/Computer-Aided-Analysis-and-Design/Homework_5/Files/Assignment4/", bool saveToFile = false)
        {
            Console.WriteLine("ASSIGNMENT 4");
            var start = VectorBuilder.Dense(new[] {-1.0, 3.0});
            var A = MatrixBuilder.Dense(2, 2, new[] {1.0, 1.0, -5.0, -7.0});
            var B = MatrixBuilder.Dense(2, 2, new[] {5.0, 0.0, 0.0, 3.0});
            Func<double, double> rt = t => t;

            var euler = Euler(assignmentPath + "euler.txt", tmax, T, start, A, B, rt, verbose, saveToFile);
            var reverseEuler = ReverseEuler(assignmentPath + "reverseEuler.txt", tmax, T, start, A, B, rt, verbose, saveToFile);
            var trapezoid = Trapezoid(assignmentPath + "trapezoid.txt", tmax, T, start, A, B, rt, verbose, saveToFile);
            var rungeKutta = RungeKutta(assignmentPath + "rungeKutta.txt", tmax, T, start, A, B, rt, verbose, saveToFile);
            // var pece = Pece(assignmentPath + "pece.txt", tmax, T, start, A, B, rt, verbose, correctionLoop, saveToFile);
            // var pecece = Pecece(assignmentPath + "pecece.txt", tmax, T, start, A, B, rt, verbose, correctionLoop, saveToFile);
        }

        private static Vector<double> Euler(string assignmentPath, double tmax, double T, Vector<double> start,
            Matrix<double> A,
            Matrix<double> B, Func<double, double> r, bool verbose = false, bool saveToFile = false)
        {
            Console.WriteLine($"\nEULER(tmax: {tmax}, T: {T}, start: [{string.Join(' ', start.ToArray())}])");
            var time = new List<double> {0.0};
            var data = new List<Vector<double>> {start};
            var pendulumData = new List<Vector<double>> {start};
            var x = VectorBuilder.DenseOfVector(start);
            var cumulativeError = VectorBuilder.Dense(A.RowCount);
            for (var t = T; t <= tmax; t += T)
            {
                var pendulum = Generator.GeneratePendulum(start, t);
                x += T * (A * x + B * VectorBuilder.Dense(new[] {r(t - T), r(t - T)}));

                cumulativeError += Vector<double>.Abs(x - pendulum);

                if (verbose)
                    Console.WriteLine(
                        $"t: {t:F2} -- x: [{string.Join(' ', x.ToArray())}] -- pendulum: {string.Join(' ', pendulum.ToArray())}]");

                data.Add(x);
                pendulumData.Add(pendulum);
                time.Add(t);
            }

            Console.WriteLine($"Cumulative error: [{string.Join(' ', cumulativeError.ToArray())}]");

            if (saveToFile)
                FileHelper.WriteToFile(time, data, assignmentPath, pendulumData);

            return x;
        }

        private static Vector<double> ReverseEuler(string assignmentPath, double tmax, double T, Vector<double> start,
            Matrix<double> A,
            Matrix<double> B, Func<double, double> r, bool verbose = false, bool saveToFile = false)
        {
            Console.WriteLine($"\nREVERSE EULER(tmax: {tmax}, T: {T}, start: [{string.Join(' ', start.ToArray())}])");
            var time = new List<double> {0.0};
            var data = new List<Vector<double>> {start};
            var pendulumData = new List<Vector<double>> {start};
            
            var U = MatrixBuilder.DenseIdentity(A.ColumnCount);
            var P = (U - A * T).Inverse();
            var Q = P * T * B;

            var x = VectorBuilder.DenseOfVector(start);
            var cumulativeError = VectorBuilder.Dense(A.RowCount);
            for (var t = T; t <= tmax; t += T)
            {
                var pendulum = Generator.GeneratePendulum(start, t);
                x = P * x + Q * VectorBuilder.Dense(new[] {r(t), r(t)});
                cumulativeError += Vector<double>.Abs(x - pendulum);

                if (verbose)
                    Console.WriteLine(
                        $"t: {t:F2} -- x: [{string.Join(' ', x.ToArray())}] -- pendulum: {string.Join(' ', pendulum.ToArray())}]");
                
                data.Add(x);
                pendulumData.Add(pendulum);
                time.Add(t);
            }

            Console.WriteLine($"Cumulative error: [{string.Join(' ', cumulativeError.ToArray())}]");
            
            if (saveToFile)
                FileHelper.WriteToFile(time, data, assignmentPath, pendulumData);

            return x;
        }

        private static Vector<double> ImplicitReverseEuler(double tmax, double T, Vector<double> start,
            Vector<double> prediction, Matrix<double> A,
            Matrix<double> B, Func<double, double> r, bool verbose = false)
        {
            Console.WriteLine(
                $"\nIMPLICIT REVERSE EULER(tmax: {tmax}, T: {T}, prediction: [{string.Join(' ', prediction.ToArray())}])");
            var x = VectorBuilder.Dense(prediction.Count);
            var cumulativeError = VectorBuilder.Dense(A.RowCount);
            for (var t = T; t <= tmax; t += T)
            {
                var pendulum = Generator.GeneratePendulum(start, t);
                x += T * (A * prediction + B * VectorBuilder.Dense(new[] {r(t), r(t)}));
                cumulativeError += Vector<double>.Abs(x - pendulum);

                if (verbose)
                    Console.WriteLine(
                        $"t: {t:F2} -- x: [{string.Join(' ', x.ToArray())}] -- pendulum: {string.Join(' ', pendulum.ToArray())}]");
            }

            Console.WriteLine($"Cumulative error: [{string.Join(' ', cumulativeError.ToArray())}]");
            
            return x;
        }

        private static Vector<double> ImplicitTrapezoid(double tmax, double T, Vector<double> start,
            Vector<double> prediction,
            Matrix<double> A,
            Matrix<double> B, Func<double, double> r, bool verbose = false)
        {
            Console.WriteLine(
                $"\nIMPLICIT TRAPEZOID(tmax: {tmax}, T: {T}, prediction: [{string.Join(' ', prediction.ToArray())}])");
            var x = VectorBuilder.Dense(prediction.Count);
            var cumulativeError = VectorBuilder.Dense(A.RowCount);
            for (var t = T; t <= tmax; t += T)
            {
                var pendulum = Generator.GeneratePendulum(start, t);
                x += 0.5 * T * (A * x + B * VectorBuilder.Dense(new[] {r(t - T), r(t - T)}) + A * prediction +
                                B * VectorBuilder.Dense(new[] {r(t), r(t)}));
                cumulativeError += Vector<double>.Abs(x - pendulum);

                if (verbose)
                    Console.WriteLine(
                        $"t: {t:F2} -- x: [{string.Join(' ', x.ToArray())}] -- pendulum: {string.Join(' ', pendulum.ToArray())}]");
            }

            Console.WriteLine($"Cumulative error: [{string.Join(' ', cumulativeError.ToArray())}]");
            return x;
        }

        private static Vector<double> Trapezoid(string assignmentPath, double tmax, double T, Vector<double> start,
            Matrix<double> A,
            Matrix<double> B, Func<double, double> r, bool verbose = false, bool saveToFile = false)
        {
            Console.WriteLine($"\nTRAPEZOID(tmax: {tmax}, T: {T}, start: [{string.Join(' ', start.ToArray())}])");
            var time = new List<double> {0.0};
            var data = new List<Vector<double>> {start};
            var pendulumData = new List<Vector<double>> {start};
            
            var U = MatrixBuilder.DenseIdentity(A.ColumnCount);
            var R = (U - A * 0.5 * T).Inverse() * (U + A * 0.5 * T);
            var S = (U - A * 0.5 * T).Inverse() * (0.5 * T) * B;

            var x = VectorBuilder.DenseOfVector(start);
            var cumulativeError = VectorBuilder.Dense(A.RowCount);
            for (var t = T; t <= tmax; t += T)
            {
                var pendulum = Generator.GeneratePendulum(start, t);
                x = R * x + S * VectorBuilder.Dense(new[] {r(t - T) + r(t), r(t - T) + r(t)});

                cumulativeError += Vector<double>.Abs(x - pendulum);

                if (verbose)
                    Console.WriteLine(
                        $"t: {t:F2} -- x: [{string.Join(' ', x.ToArray())}] -- pendulum: {string.Join(' ', pendulum.ToArray())}]");
                
                data.Add(x);
                pendulumData.Add(pendulum);
                time.Add(t);
            }

            Console.WriteLine($"Cumulative error: [{string.Join(' ', cumulativeError.ToArray())}]");
            if (saveToFile)
                FileHelper.WriteToFile(time, data, assignmentPath, pendulumData);

            return x;
        }

        private static Vector<double> RungeKutta(string assignmentPath, double tmax, double T, Vector<double> start,
            Matrix<double> A,
            Matrix<double> B, Func<double, double> r, bool verbose = false, bool saveToFile = false)
        {
            Console.WriteLine($"\nRUNGE KUTTA(tmax: {tmax}, T: {T}, start: [{string.Join(' ', start.ToArray())}])");
            var time = new List<double> {0.0};
            var data = new List<Vector<double>> {start};
            var pendulumData = new List<Vector<double>> {start};
            
            var x = VectorBuilder.DenseOfVector(start);
            var cumulativeError = VectorBuilder.Dense(A.RowCount);
            for (var t = T; t <= tmax; t += T)
            {
                var pendulum = Generator.GeneratePendulum(start, t);
                var tk = t - T;
                var m1 = A * x + B * VectorBuilder.Dense(new[] {r(tk), r(tk)});
                var m2 = A * (x + (T / 2.0) * m1) +
                         B * VectorBuilder.Dense(new[] {r(tk + (T / 2.0)), r(tk + (T / 2.0))});
                var m3 = A * (x + (T / 2.0) * m2) +
                         B * VectorBuilder.Dense(new[] {r(tk + (T / 2.0)), r(tk + (T / 2.0))});
                var m4 = A * (x + T * m3) + B * VectorBuilder.Dense(new[] {r(tk + T), r(tk + T)});

                x += (T / 6.0) * (m1 + 2 * m2 + 2 * m3 + m4);

                cumulativeError += Vector<double>.Abs(x - pendulum);
                if (verbose)
                    Console.WriteLine(
                        $"t: {t:F2} -- x: [{string.Join(' ', x.ToArray())}] -- pendulum: {string.Join(' ', pendulum.ToArray())}]");
                
                data.Add(x);
                pendulumData.Add(pendulum);
                time.Add(t);
            }

            Console.WriteLine($"Cumulative error: [{string.Join(' ', cumulativeError.ToArray())}]");
            if (saveToFile)
                FileHelper.WriteToFile(time, data, assignmentPath, pendulumData);
            return x;
        }
        private static Vector<double> Pece(string assignmentPath, double tmax, double T, Vector<double> start,
            Matrix<double> A,
            Matrix<double> B, Func<double, double> r, bool verbose = false, int correctionLoop = 1, bool saveToFile = false)
        {
            var time = new List<double> {0.0};
            var data = new List<Vector<double>> {start};
            var correction = VectorBuilder.DenseOfVector(start);
            for (var t = T; t <= tmax; t += T)
            {
                var prediction = Euler(assignmentPath, tmax: 0, T, correction, A, B, r, verbose);
                correction = prediction;
                for (var i = 0; i < correctionLoop; i++)
                    correction = ImplicitTrapezoid(tmax, T, start, prediction: correction, A, B, r, verbose);
                
                data.Add(correction);
                time.Add(t);
            }
            
            if (saveToFile)
                FileHelper.WriteToFile(time, data, assignmentPath, data);

            return correction;
        }
        
        private static Vector<double> Pecece(string assignmentPath, double tmax, double T, Vector<double> start,
            Matrix<double> A,
            Matrix<double> B, Func<double, double> r, bool verbose = false, int correctionLoop = 2, bool saveToFile = false)
        {
            var time = new List<double> {0.0};
            var data = new List<Vector<double>> {start};
            var correction = VectorBuilder.DenseOfVector(start);
            for (var t = T; t <= tmax; t += T)
            {
                var prediction = Euler(assignmentPath, tmax: T, T, correction, A, B, r, verbose);
                correction = prediction;
                for (var i = 0; i < correctionLoop; i++)
                    correction = ImplicitReverseEuler(tmax, T, start, prediction: correction, A, B, r, verbose);
                
                data.Add(correction);
                time.Add(t);
            }
            if (saveToFile)
                FileHelper.WriteToFile(time, data, assignmentPath, data);

            return correction;
        }

        
    }
}