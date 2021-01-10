using System;
using System.Collections;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Homework_3.OptimizationFunctions
{
    public class Hessian
    {
        private List<List<Func<Vector, double>>> SecondDerivatives { get; set; }
        public Func<Vector, double> this[int i, int j]
        {
            get => SecondDerivatives[i][j];
            set => SecondDerivatives[i][j] = value;
        }

        public Hessian(List<List<Func<Vector, double>>> secondDerivatives) => SecondDerivatives = secondDerivatives;
    }
}