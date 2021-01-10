using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Homework_3.OptimizationFunctions
{
    public class Function2 : IOptimizationFunction
    {
        public Func<Vector, double> Function { get; set; }
        public IList<Func<Vector, double>> Gradients { get; set; }
        public Hessian Hessian { get; set; }

        public Function2()
        {
            Function = x => Math.Pow(x[0] - 4, 2) + 4 * Math.Pow(x[1] - 2, 2);
            Gradients = new List<Func<Vector, double>>
            {
                x => 2 * (x[0] - 4),
                x => 8 * (x[1] - 2),
            };

            var hessianDerivativesRow1 = new List<Func<Vector, double>>
            {
                x => 2,
                x => 0,
                
            };
            var hessianDerivativesRow2 = new List<Func<Vector, double>>
            {
                x => 0,
                x => 8,
            };

            var hessianDerivatives = new List<List<Func<Vector, double>>>
            {
                hessianDerivativesRow1,
                hessianDerivativesRow2,
            };
            
            Hessian = new Hessian(hessianDerivatives);
        }
        
        public override string ToString() =>
            $"Function: F2\nMath.Pow(x[0] - 4, 2) + 4 * Math.Pow(x[1] - 2, 2)\n" +
            $"Gradient:\n2 * (x[0] - 4),\n" +
            $"8 * (x[1] - 2)\n";
    }
}