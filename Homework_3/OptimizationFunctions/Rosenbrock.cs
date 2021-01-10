using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Homework_3.OptimizationFunctions
{
    public class Rosenbrock : IOptimizationFunction
    {
        public Func<Vector, double> Function { get; set; }
        public IList<Func<Vector, double>> Gradients { get; set; }
        public Hessian Hessian { get; set; }

        public Rosenbrock()
        {
            Function = x => 100 * Math.Pow(x[1] - Math.Pow(x[0], 2), 2) + Math.Pow(1 - x[0], 2);
            Gradients = new List<Func<Vector, double>>
            {
                x => -400 * x[0] * (x[1] - Math.Pow(x[0], 2)) - 2 * (1 - x[0]),
                x => 200 * (x[1] - Math.Pow(x[0], 2)),
            };

            var hessianDerivativesRow1 = new List<Func<Vector, double>>
            {
                x => -400 * (x[1] - Math.Pow(x[0], 2)) + 800 * Math.Pow(x[0], 2) + 2,
                x => -400 * x[0],
            };
            var hessianDerivativesRow2 = new List<Func<Vector, double>>
            {
                x => -400 * x[0],
                x => 200
            };

            var hessianDerivatives = new List<List<Func<Vector, double>>>
            {
                hessianDerivativesRow1,
                hessianDerivativesRow2,
            };

            Hessian = new Hessian(hessianDerivatives);
        }

        public override string ToString() =>
            $"Function: Rosenbrock\n100 * Math.Pow(x[1] - Math.Pow(x[0], 2), 2) + Math.Pow(1 - x[0], 2);\n" +
            $"Gradient:\n-400 * x[0] * (x[1] - Math.Pow(x[0], 2)) - 2 * (1 - x[0]),\n" +
            $"200 * (x[1] - Math.Pow(x[0], 2))";
    }
}