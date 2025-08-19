using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Helper
{
    public static class J
    {
        // I/O 

        public static List<string> ReadAllLines(string from)
        {
            string dataIn = File.ReadAllText(from);
            return dataIn.Split('\n').Select(line => line.Trim()).ToList();
        }

        public static void O(object o) => Console.Write(o);

        public static void OL(object o)
        {
            if (o is IEnumerable && !(o is string))
            {
                foreach (var obj in (IEnumerable<object>)o)
                {
                    OL(obj);
                }
            }
            else
            {
                Console.WriteLine(o);
            }
        }

        // Conversion

        public static int I(string s)
        {
            if (!int.TryParse(s, out int result))
            {
                throw new FormatException($"Failed to parse '{s}' to int.");
            }
            return result;
        }

        public static int I(char c)
        {
            if (!int.TryParse(c.ToString(), out int result))
            {
                throw new FormatException($"Failed to parse '{c}' to int.");
            }
            return result;
        }

        public static long L(string s)
        {
            if (!long.TryParse(s, out long result))
            {
                throw new FormatException($"Failed to parse '{s}' to long.");
            }
            return result;
        }

        public static ulong UL(string s)
        {
            if (!ulong.TryParse(s, out ulong result))
            {
                throw new FormatException($"Failed to parse '{s}' to ulong.");
            }
            return result;
        }

        public static (long, long) Point(string s)
        {
            // Use a regular expression to find all numbers in the string
            var matches = Regex.Matches(s, @"\d+");

            // Check if we have exactly two numbers
            if (matches.Count != 2)
            {
                throw new ArgumentException("The input string does not contain exactly two numbers.");
            }

            // Parse the numbers and return them as a tuple
            long number1 = long.Parse(matches[0].Value);
            long number2 = long.Parse(matches[1].Value);

            return (number1, number2);
        }

        // Math

        public static long GCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public static long LCM(params long[] numbers)
        {
            if (numbers.Length == 0)
                throw new ArgumentException("At least two numbers are required.");

            long result = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                result = LCM(result, numbers[i]);
            }
            return result;
        }

        public static long CrossProduct((long, long) v1, (long, long) v2)
        {
            return v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1;
        }

        public static long CrossProduct((long, long, long) v1, (long, long, long) v2)
        {
            long xComponent = v1.Item2 * v2.Item3 - v1.Item3 * v2.Item2;
            long yComponent = v1.Item3 * v2.Item1 - v1.Item1 * v2.Item3;
            long zComponent = v1.Item1 * v2.Item2 - v1.Item2 * v2.Item1;

            return xComponent + yComponent + zComponent;
        }

        public static (double, double, double)? LineIntersection(
            Line3D line1, Line3D line2, bool infiniteLines, bool futureIntersection)
        {
            var r = (
                line1.Point2.X - line1.Point1.X,
                line1.Point2.Y - line1.Point1.Y,
                line1.Point2.Z - line1.Point1.Z);
            var s = (
                line2.Point2.X - line2.Point1.X,
                line2.Point2.Y - line2.Point1.Y,
                line2.Point2.Z - line2.Point1.Z);

            var rxs = CrossProduct(r, s);
            var q_p = (
                line2.Point1.X - line1.Point1.X,
                line2.Point1.Y - line1.Point1.Y,
                line2.Point1.Z - line1.Point1.Z);

            if (rxs == 0)
            {
                return null;
            }

            var t_numer = CrossProduct(q_p, s);
            var u_numer = CrossProduct(q_p, r);
            var denom = CrossProduct(r, s);

            double t = (double)t_numer / denom;
            double u = (double)u_numer / denom;

            double intersectX = line1.Point1.X + t * r.Item1;
            double intersectY = line1.Point1.Y + t * r.Item2;
            double intersectZ = line1.Point1.Z + t * r.Item3;

            if (infiniteLines)
            {
                if (futureIntersection && t >= 0 && u >= 0)
                {
                    return (intersectX, intersectY, intersectZ);
                }
                else if (!futureIntersection)
                {
                    return (intersectX, intersectY, intersectZ);
                }
            }
            else
            {
                if (t >= 0 && t <= 1 && u >= 0 && u <= 1)
                {
                    return (intersectX, intersectY, intersectZ);
                }
            }

            return null;
        }

        public class Line3D
        {
            public Vector3D Point1 { get; set; }
            public Vector3D Point2 { get; set; }

            public Line3D(Vector3D point1, Vector3D point2)
            {
                Point1 = point1;
                Point2 = point2;
            }
        }

        public class Vector3D
        {
            public long X, Y, Z;

            public Vector3D(long x, long y, long z)
            {
                X = x;
                Y = y;
                Z = z;
            }
        }
    }
}
