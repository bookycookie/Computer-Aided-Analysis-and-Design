#nullable enable
using System.Collections.Generic;
using System.IO;
using MathNet.Numerics.LinearAlgebra;

namespace Homework_5.Helpers
{
    public static class FileHelper
    {
        public static void WriteToFile(List<double> time, List<Vector<double>> points, string path, List<Vector<double>>? pendulum)
        {
            using var file = new StreamWriter(path);
            for (var i = 0; i < points.Count; i++)
            {
                var vectorString = string.Join(" ", points[i][0].ToString("0.00000000", System.Globalization.CultureInfo.InvariantCulture), points[i][1].ToString("0.00000000", System.Globalization.CultureInfo.InvariantCulture));
                vectorString += ' ' + string.Join(" ", pendulum?[i][0].ToString("0.00000000", System.Globalization.CultureInfo.InvariantCulture), pendulum?[i][1].ToString("0.00000000", System.Globalization.CultureInfo.InvariantCulture));
                file.WriteLine(
                    $"{time[i].ToString("0.00000000", System.Globalization.CultureInfo.InvariantCulture)} " +
                    $"{vectorString.ToString(System.Globalization.CultureInfo.InvariantCulture)}");
            }
                
        }
    }
}