using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Problem5
    {
        public static void solve()
        {
            Dictionary<Point, int> points = new Dictionary<Point, int>();

            using (StreamReader sr = new StreamReader(@"c:\users\drslc\downloads\input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line = Regex.Replace(line, @",|\s+->\s+", " ");
                    string[] coords = line.Split(' ');

                    Point p1 = new Point(coords[0], coords[1]);
                    Point p2 = new Point(coords[2], coords[3]);

                    Point.countPoints(p1, p2, points);
                }
            }

            int count = 0;
            foreach (Point p in points.Keys)
            {
                if (points[p] >= 2)
                    count++;
            }

            Console.WriteLine(count);
            Console.ReadLine();
        }


        class Point
        {
            public int x;
            public int y;

            public Point(int _x, int _y)
            {
                x = _x;
                y = _y;
            }

            public Point(string _x, string _y)
            {
                x = int.Parse(_x);
                y = int.Parse(_y);
            }

            public override bool Equals(object o)
            {
                if (!(o is Point))
                    return false;

                Point p2 = (Point)o;

                return x == p2.x && y == p2.y;
            }

            public override int GetHashCode()
            {
                return (x + "," + y).GetHashCode();
            }

            public override string ToString()
            {
                return "(" + x + "," + y + ")";
            }

            private static void updateMap(Point p, Dictionary<Point, int> points)
            {
                if (points.ContainsKey(p))
                    points[p]++;
                else
                    points.Add(p, 1);
            }

            public static void countPoints(Point p1, Point p2, Dictionary<Point, int> points)
            {
                if (Math.Abs(p1.x - p2.x) == Math.Abs(p1.y - p2.y))
                    for (int i = p1.x, j = p1.y; ; i += p1.x < p2.x ? 1 : -1, j += p1.y < p2.y ? 1 : -1)
                    {
                        Point p = new Point(i, j);
                        updateMap(p, points);
                        if (p.Equals(p2))
                            break;
                    }
                else
                 if (p1.x == p2.x)
                    for (int i = p1.y < p2.y ? p1.y : p2.y; i <= (p1.y < p2.y ? p2.y : p1.y); i++)
                        updateMap(new Point(p1.x, i), points);

                else if (p1.y == p2.y)
                    for (int i = p1.x < p2.x ? p1.x : p2.x; i <= (p1.x < p2.x ? p2.x : p1.x); i++)
                        updateMap(new Point(i, p1.y), points);
            }
        }
    }
}
