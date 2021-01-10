using MathNet.Numerics.LinearAlgebra.Double;

namespace Homework_3
{
    public class Interval
    {
        public double Left { get; set; }
        public double Right { get; set; }

        public override string ToString() => $"[{Left}, {Right}]";
    }
}