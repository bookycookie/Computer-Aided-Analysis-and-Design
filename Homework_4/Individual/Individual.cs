using System.Collections;
using System.Linq;

namespace Homework_4.Individual
{
    public abstract class Individual<T> where T : IEnumerable
    {
        public T Representation { get; set; }
        
        public double Fitness { get; set; } = double.PositiveInfinity;

        // Used only on a Binary individual so far.
        public int N { get; set; }

        public abstract double[] ToDoubleArray();
        public double LowerBound { get; set; }
        public double UpperBound { get; set; }
        public int Precision { get; set; }


        public override string ToString() => "Points: [" + string.Join(" ", ToDoubleArray()) + $"]\nFitness: {Fitness:E6}";
    }
}