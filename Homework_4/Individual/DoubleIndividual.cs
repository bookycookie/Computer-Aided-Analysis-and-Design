namespace Homework_4.Individual
{
    public class DoubleIndividual : Individual<double[]>
    {
        public DoubleIndividual(int dimension) => Representation = new double[dimension];
        public override double[] ToDoubleArray() => Representation;
    }
}