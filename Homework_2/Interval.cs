namespace Homework_2
{
    public class Interval
    {
        public double Left { get; set; }
        public double Right { get; set; }

        public override string ToString() => $"[{Left}, {Right}]";
    }
}