using MathNet.Numerics.LinearAlgebra;

namespace Homework_5
{
    public class Integrator
    {
        public Integrator(Vector<double> start, Vector<double> x, Matrix<double> xPrime)
        {
            Start = start;
            X = x;
            XPrime = xPrime;
        }

        private Vector<double> Start { get; set; }
        private Vector<double> X { get; set; }
        private Matrix<double> XPrime { get; set; }

        
    }
}