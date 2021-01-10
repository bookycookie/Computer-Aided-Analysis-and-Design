using System;
using System.Collections;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Homework_3.OptimizationFunctions
{
    public interface IOptimizationFunction
    {
        public Func<Vector, double> Function { get; set; }
        public IList<Func<Vector, double>> Gradients { get; set; }
        public Hessian Hessian { get; set; }

        public double ComputeFunction(Vector x) => Function(x);
        public Vector ComputeGradient(Vector x)
        {
            var result = new DenseVector(x.Count);

            for (var i = 0; i < x.Count; i++)
                result[i] = Gradients[i].Invoke(x);

            return result;
        }

        public Matrix ComputeHessian(Vector x)
        {
            var result = new DenseMatrix(x.Count, x.Count);

            for (var i = 0; i < x.Count; i++)
            for (var j = 0; j < x.Count; j++)
                result[i, j] = Hessian[i, j].Invoke(x);

            return result;
        }
        
    }
}