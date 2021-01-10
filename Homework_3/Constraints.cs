using System;
using System.Linq;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Homework_3
{
    public class Constraints
    {
        private Func<Vector, bool> InequalityOne = x => x[1] - x[0] >= 0;
        private Func<Vector, bool> InequalityTwo = x => 2 - x[0] >= 0;

        private Func<Vector, bool> Equality = x => x.All(val => val >= -100 && val <= 100);
    }
}