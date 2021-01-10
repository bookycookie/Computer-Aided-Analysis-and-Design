using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Homework_2
{
    public class Vector
    {
        /// <summary>
        /// A single point. For example [x1, x2, x3, x4].
        /// </summary>
        public double[] Point { get; set; }
        
        /// <summary>
        /// Dimensionality of a vector is equal to the number of points. In the above example, the dimension is 4.
        /// </summary>
        public int Dimension { get; set; }

        public double this[int i]
        {
            get => Point[i];
            set => Point[i] = value;
        }

        public Vector(int n)
        {
            Point = new double[n];
            Dimension = n;
        }

        public Vector(params double[] point)
        {
            Point = point;
            Dimension = point.Length;
        }

        public IList<Vector> GenerateOffsetVectors(double offset)
        {
            var points = new List<Vector>();
            var n = Point.Length;

            points.Add(this);
            for (var i = 0; i < n; i++)
            {
                var point = new double[n];

                point[i] = Point[i] + offset;
                var newVector = new Vector(point);
                points.Add(newVector);
            }


            /*Console.WriteLine($"Generating simplex. Offset: {offset}");
            foreach (var viktor in points)
            {
                Console.WriteLine(viktor);
            }
            Console.WriteLine();*/

            return points;

        }

        public static Vector operator +(Vector a, Vector b)
        {
            Vector c = new Vector(a.Dimension);
            for (var i = 0; i < a.Dimension; i++)
                c[i] = a[i] + b[i];

            return c;
        }
        
        public static Vector operator -(Vector a, Vector b)
        {
            Vector c = new Vector(a.Dimension);
            for (var i = 0; i < a.Dimension; i++)
                c[i] = a[i] - b[i];

            return c;
        }

        public static Vector operator *(double scalar, Vector a)
        {
            Vector b = new Vector(a.Dimension);

            for (var i = 0; i < a.Dimension; i++) 
                b[i] = scalar * a[i];

            return b;
        }

        public override string ToString()
        {
            var result = "[";

            foreach (var x in Point)
            {
                result += $" {x:0.00000} ";
            }

            result += "]";

            return result;
        }
    }
}