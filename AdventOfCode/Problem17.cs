using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Problem17
    {
        static int[] bounds;
        static int xMin, xMax, yMin, yMax;

        //public static void Main()
        //{
        //    readInput();
        //    Console.WriteLine(partTwo());
        //    Console.ReadLine();
        //}

        private static int partOne()
        {
            int yMin = bounds[2];
            int vyi = Math.Abs(yMin) - 1;
            int yMax = sum(vyi);
            return yMax;
        }

        private static int partTwo()
        {
            int hits = 0;
            xMin = bounds[0];
            xMax = bounds[1];
            yMin = bounds[2];
            yMax = bounds[3];
            for (int vxi = 1; vxi <= xMax; vxi++)
            {
                for (int vyi = yMin; vyi <= Math.Abs(yMin) - 1; vyi++)
                {
                    int ypos, xpos;
                    ypos = xpos = 0;
                    int vy = vyi;
                    int vx = vxi;
                    for (int step = 1; ; step++)
                    {
                        xpos += vx;
                        ypos += vy;
                        vx = Math.Max(0, vx - 1);
                        vy--;

                        if (xpos > xMax || ypos < yMin)
                            break;
                        else if (xpos >= xMin && xpos <= xMax && ypos >= yMin && ypos <= yMax)
                        {
                            Console.WriteLine($"with initial velocity ({vxi}, {vyi}) has position ({xpos}, {ypos}) after {step} step(s)");
                            hits++;
                            break;
                        }
                    }
                }
            }
            return hits;
        }

        private static int sum(int n)
        {
            return n * (n + 1) / 2;
        }

        private static void readInput()
        {
            string line;
            using (StreamReader sr = new StreamReader(@"c:/users/drslc/downloads/input.txt"))
                line = sr.ReadLine();
            bounds = new int[] { 0, 0, 0, 0 };
            int i = 0;
            Match m = Regex.Match(line, @"(-?\d+)+");
            while (m.Success)
            {
                bounds[i++] = int.Parse(m.Groups[1].Value);
                m = m.NextMatch();
            }
        }
    }
}
