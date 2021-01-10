using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Homework_5
{
    public static class Generator
    {
        public static Vector<double> GeneratePendulum(Vector<double> start, double t) =>
            new DenseVector(start.Count)
            {
                [0] = start[0] * Math.Cos(t) + start[1] * Math.Sin(t),
                [1] = start[1] * Math.Cos(t) - start[0] * Math.Sin(t)
            };
    }
}