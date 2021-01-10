using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework_2
{
    class Program
    {
        private const double K = 0.61803398874989484820458683436;
        static void Main(string[] args)
        {
            /* ZADATAK 1 */
            /*var xStartTask1 = new Vector(10.0);
            Func<double, double> funcTask1 = x => Math.Pow(x - 3, 2);
            Func<double[], double> func2Task1 = x => Math.Pow(x[0] - 3, 2);
            
            var goldenTask1 = GoldenRatio(xStartTask1.Point[0], 1, 0.000010, funcTask1);
            var hjTask1 = HookeJeeves(xStartTask1, func2Task1);
            var nmTask1 = NelderMead(xStartTask1, func2Task1);*/
            
            /* ZADATAK 2 */
            // Function 1
            /*var xStartTask2F1 = new Vector(-1.9, 2);
            var xMin1 = new Vector(1.0, 1.0);
            Func<double[], double> Rosenbrock = x => 100 * Math.Pow(x[1] - x[0] * x[0], 2) + Math.Pow(1 - x[0], 2);

            var hjZ2F1 = HookeJeeves(xStartTask2F1, Rosenbrock);
            var nmZ2F1 = NelderMead(xStartTask2F1, Rosenbrock);*/
            
            // Function 2
            /*var xStartTask2F2 = new Vector(0.1, 0.3);
            var xMin2 = new Vector(4.0, 2.0);
            Func<double[], double> F2 = x => Math.Pow(x[0] - 4, 2) + 4 * Math.Pow(x[1] - 2, 2);
            var hjZ2F2 = HookeJeeves(xStartTask2F2, F2);
            var nmZ2F2 = NelderMead(xStartTask2F2, F2);*/
            
            // Function 3
            /*var xStartTask2F3 = new Vector(0.1, 0.3, 0.5, 0.7, 0.9);
            var xMin3 = new Vector(4.0, 2.0);
            Func<double[], double> F3 = x => Math.Pow(x[0] - 3, 2);
            var hjZ2F3 = HookeJeeves(xStartTask2F3, F3);
            var nmZ2F3 = NelderMead(xStartTask2F3, F3);*/
            
            // Function 4
            /*var xStartTask2F4 = new Vector(5.1, 1.1);
            var xMin4 = new Vector(0.0, 0.0);
            Func<double[], double> Jakobovich = x =>
                Math.Abs((x[0] - x[1]) * (x[0] + x[1])) + Math.Sqrt(x[0] * x[0] + x[1] * x[1]);
            var hjZ2F4 = HookeJeeves(xStartTask2F4, Jakobovich);
            var nmZ2F4 = NelderMead(xStartTask2F4, Jakobovich);*/
            
            /* ZADATAK 3 */
            /*var xStartTask3F4 = new Vector(5.0, 5.0);
            Func<double[], double> Jakobovich2 = x =>
                Math.Abs((x[0] - x[1]) * (x[0] + x[1])) + Math.Sqrt(x[0] * x[0] + x[1] * x[1]);
            var hjZ3F4 = HookeJeeves(xStartTask3F4, Jakobovich2);
            var nmZ3F4 = NelderMead(xStartTask3F4, Jakobovich2);*/
            
            /* ZADATAK 4 */
            /*var x1StartTask4F1 = new Vector(0.5, 0.5);
            var offset1 = 13;
            Func<double[], double> Rosenbrock2 = x => 100 * Math.Pow(x[1] - x[0] * x[0], 2) + Math.Pow(1 - x[0], 2);
            var nmZ4Banana = NelderMead(x1StartTask4F1, Rosenbrock2, offset: offset1);
            
            var x2StartTask4F1 = new Vector(20.0, 20.0);
            var nmZ4Banana20 = NelderMead(x2StartTask4F1, Rosenbrock2, offset: offset1);*/
            
            /* ZADATAK 5 */
            /*Func<double[], double> Schaffer = x =>
            {
                var sum = x.Sum(t => Math.Pow(t, 2));

                var top = Math.Pow(Math.Sin(Math.Sqrt(sum)), 2) - 0.5;
                var bottom = Math.Pow(1 + 0.001 * sum, 2);

                return 0.5 + top / bottom;
            };
            const int attempts = 100;
            var success = 0;
            var r = new Random();
            for (var i = 0; i < attempts; i++)
            {
                var x1 = (double)r.Next(-50, 50);
                var x2 = (double)r.Next(-50, 50);
                var xStartSchaff = new Vector(x1, x2);
            
                var nmSchaff = NelderMead(xStartSchaff, Schaffer);

                if (Schaffer(nmSchaff.Point) < 10E-4) success++;
            }

            var probability = (success / (double) attempts) * 100;
            Console.WriteLine($"Probability of successfully finding global minima: {probability}%");*/



        }

        private static Interval? UnimodalInterval(double h, double point, Func<double, double> function)
        {
            var l = point - h;
            var r = point + h;
            var m = point;
            uint step = 1;
            var fm = function(m);
            var fl = function(l);
            var fr = function(r);

            if (fm < fr && fm < fl) return null;

            if (fm > fr)
                do
                {
                    l = m;
                    m = r;
                    fm = fr;
                    r = point + h * (step *= 2);
                    fr = function(r);

                } while (fm > fr);
            else
                do
                {
                    r = m;
                    m = l;
                    fm = fl;
                    l = point - h * (step *= 2);
                    fl = function(l);
                } while (fm > fl);
            
            var interval = new Interval { Left = l, Right = r };
            return interval;
        }

        private static Interval? GoldenRatio(double point, double step, double e, Func<double, double> function)
            => GoldenRatio(UnimodalInterval(step, point, function), e, function);

        private static Interval? GoldenRatio(Interval? ab, double e, Func<double, double> function)
        {
            Console.WriteLine("Golden Ratio started.");
            var a = ab!.Left;
            var b = ab!.Right;
            var c = b - K * (b - a);
            var d = a + K * (b - a);

            var fc = function(c);
            var fd = function(d);
            var i = 0;
            Console.WriteLine("\ta\tc\td\tb");
            //\tf(a)\tf(c)\tf(d)\tf(b)
            
            while (b - a > e) {
                Console.WriteLine($"i: {i}\t{a:0.000}\t{c:0.000}\t{d:0.000}\t{b:0.000}");
                //\t{function(a):0.000}\t{fc:0.000}\t{fd:0.000}\t{function(b):0.000}
                if (fc < fd)
                {
                    b = d;
                    d = c;
                    c = b - K * (b - a);
                    fd = fc;
                    fc = function(c);
                }
                else
                {
                    a = c;
                    c = d;
                    d = a + K * (b - a);
                    fc = fd;
                    fd = function(d);
                }

                i++;
            }
            var interval = new Interval { Left = a, Right = b };
            Console.WriteLine($"Golden Ratio concluded after {i} iterations.");
            Console.WriteLine($"Resulting interval: {interval}");
            Console.WriteLine();
            return interval;
        }

        private static Vector HookeJeeves(Vector x0, Func<double[], double> function, double deltaX = 1, double e = 0.000010)
        {
            Console.WriteLine("Hooke Jeeves started.");
            Vector xb = x0;
            Vector xp = x0;
            var i = 0;
            Console.WriteLine("\txb\t\txp\t\txn\t\tf(x)\t/\\x");
            do
            {
                var xn = Seek(xp, deltaX, function);
                
                Console.WriteLine($"i: {i}\t{xb}\t{xp}\t{xn}\t{function(xb.Point)}\t{deltaX}");
                i++;
                if (function(xn.Point) < function(xb.Point))
                {
                    // xp = new Vector(xn.Point.Zip(xb.Point, (xn1, xb1) => 2 * xn1 - xb1).ToArray());
                    xp = 2 * xn - xb;
                    xb = xn;
                }
                else
                {
                    deltaX *= 0.5;
                    xp = xb;
                }
            } while (!(deltaX <= e)); // do this until the deltaX is lower or equal to epsilon

            Console.WriteLine($"Hooke Jeeves concluded after {i} iterations.");
            Console.WriteLine($"Resulting vector: {xb}");
            Console.WriteLine();
            return xb;
        }

        private static Vector Seek(Vector xp, double deltaX, Func<double[], double> function)
        {
            // value copy of xp, not a reference copy
            var x = new Vector(xp.Point.Select(a => a).ToArray());

            for (var i = 0; i < x.Dimension; i++)
            {
                var p = function(x.Point);
                x[i] +=  deltaX;
                var n = function(x.Point);
                
                // if N < P, skip iteration of for loop
                if (!(n > p)) continue;
                x[i] -= 2 * deltaX; // positive offset is not optimal, subtract by 2dx to counteract the +dx, so the net is -dx
                n = function(x.Point);
                if (n > p) 
                    x[i] += deltaX; // negative offset is not optimal, backtrack to the beginning
            }
            return x;
        }

        private static Vector NelderMead(Vector x0, Func<double[], double> function, double alpha = 1,
            double beta = 0.5, double gamma = 2, double sigma = 0.5,
            double epsilon = 0.000010, double offset = 1)
        {
            Console.WriteLine("Nelder Mead Simplex started.");
            var x = x0.GenerateOffsetVectors(offset);
            Vector centroid;
            var iteration = 0;
            Console.WriteLine("\tcentroid\tf(Xc)");
            do
            {
                var h = MaximumSimplex(x, function);
                var l = MinimumSimplex(x, function);
                centroid = GenerateCentroid(x, l, h);
                Console.WriteLine($"i: {iteration}\t{centroid}\t\t{function(centroid.Point)}");
                iteration++;

                var xr = Reflection(centroid, x[h], alpha);

                if (function(xr.Point) < function(x[l].Point))
                {
                    Vector xe = Expansion(centroid, xr, gamma);

                    if (function(xe.Point) < function(x[l].Point))
                        x[h] = xe;
                    else
                        x[h] = xr;
                }
                else
                {
                    var isXrWorst = !x.Where((t, i) => i != h && function(xr.Point) <= function(x[i].Point)).Any();

                    if (isXrWorst)
                    {
                        if (function(xr.Point) < function(x[h].Point))
                            x[h] = xr;
                        Vector xk = Contraction(centroid, x[h], beta);

                        if (function(xk.Point) < function(x[h].Point))
                            x[h] = xk;
                        else
                            TranslateSimplexTowardsLowest(x, l, sigma);
                    }
                    else
                        x[h] = xr;
                }
            } while (CalculateStopCondition(x, centroid, function) > epsilon);

            Console.WriteLine($"Nelder Mead Simplex concluded in {iteration} iterations.");
            /*Console.WriteLine($"Centroid: {centroid}");
            Console.WriteLine($"f(Xc): {function(centroid.Point)}");*/
            Console.WriteLine();
            return centroid;
        }

        private static double CalculateStopCondition(IList<Vector> simplex, Vector centroid, Func<double[], double> function)
        {
            var n = simplex.Count;

            var quadraticSum = 0.0;

            for (var i = 0; i < n; i++)
                quadraticSum += Math.Pow(function(simplex[i].Point) - function(centroid.Point), 2);
            
            var result = Math.Sqrt(quadraticSum / n);
            return result;


        }
        private static Vector Reflection(Vector centroid, Vector xh, double alpha = 1)
        {
            // Console.WriteLine($"Reflection (alpha = {alpha})");
            var xr = new Vector(centroid.Dimension);

            xr = (1 + alpha) * centroid - alpha * xh;

            // Console.WriteLine(xr);
            return xr;
        }

        private static Vector Expansion(Vector centroid, Vector xr, double gamma = 2)
        {
            // Console.WriteLine($"Expansion (gamma = {gamma})");
            var xe = new Vector(centroid.Dimension);

            xe = (1 - gamma) * centroid + gamma * xr;

            // Console.WriteLine(xe);
            return xe;
        }

        private static Vector Contraction(Vector centroid, Vector xh, double beta = 0.5)
        {
            // Console.WriteLine($"Contraction (beta = {beta})");
            var xk = new Vector(centroid.Dimension);

            xk = (1 - beta) * centroid + beta * xh;

            // Console.WriteLine(xk);
            return xk;
        }

        private static void TranslateSimplexTowardsLowest(IList<Vector> simplex, int l, double sigma = 0.5)
        {
            // Console.WriteLine("Translating.");
            for (var i = 0; i < simplex.Count; i++) 
                simplex[i] = sigma * (simplex[i] + simplex[l]);
        }
        
        private static Vector GenerateCentroid(IList<Vector> simplex, int l, int h)
        {
            var n = simplex.First().Dimension;
            var point = new Vector(n);

            for (var i = 0; i < simplex.Count; i++)
            for (var j = 0; j < simplex[0].Dimension; j++)
            {
                // We skip the worst function evaluation
                // in this case it is the maximum simplex value
                // since we are finding the minimum.
                if (i == h) continue;
                point[j] += simplex[i].Point[j];
            }

            var centroid = new Vector(n);

            for (var j = 0; j < n; j++)
                centroid[j] = point[j] / n;

            //Console.WriteLine($"Centroid: {centroid}");

            return centroid;

        }

        private static int MaximumSimplex(IList<Vector> simplex, Func<double[], double> function)
        {
            var max = simplex.First();
            
            foreach (var vector in simplex)
                if (function(vector.Point) > function(max.Point))
                    max = vector;

            return simplex.IndexOf(max);

        }
        
        private static int MinimumSimplex(IList<Vector> simplex, Func<double[], double> function)
        {
            var min = simplex.First();
            
            foreach (var vector in simplex)
                if (function(vector.Point) < function(min.Point))
                    min = vector;

            return simplex.IndexOf(min);

        }
        
        /*private static double CoordinateSearch(double x0, double e, double n)
        {
            var x = x0;
            var xs = x;
            do
            {
                xs = x;
                for (int i = 1; i < n; i++)
                {
                    
                }
            } while (Math.Abs(x - xs) > e);
        }*/
    }
}