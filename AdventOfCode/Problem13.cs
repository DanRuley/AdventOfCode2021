using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Problem13
    {

        static HashSet<Coord> coords;
        static List<Tuple<string, int>> folds;

        //public static void Main()
        //{
        //    readLines();
        //    solve();
        //    printCoords();
        //    Console.ReadLine();
        //}

        private static void solveFirstPart()
        {
            HashSet<Coord> newCoords = new HashSet<Coord>();
            foreach (Coord c in coords)
            {
                string foldOp = folds[0].Item1;
                int foldAmt = folds[0].Item2;
                switch (folds[0].Item1)
                {
                    case "x":
                        if (c.x > folds[0].Item2)
                            newCoords.Add(new Coord(foldAmt - (c.x - foldAmt), c.y));
                        else
                            newCoords.Add(c);
                        break;
                    case "y":
                        if (c.y > folds[0].Item2)
                            newCoords.Add(new Coord(c.x, foldAmt - (c.y - foldAmt)));
                        else
                            newCoords.Add(c);
                        break;
                }
            }
            coords = newCoords;
            Console.WriteLine(coords.Count);
        }

        private static void solve()
        {
            HashSet<Coord> newCoords;
            foreach (Tuple<string, int> f in folds)
            {
                newCoords = new HashSet<Coord>();
                foreach (Coord c in coords)
                {
                    string foldOp = f.Item1;
                    int foldAmt = f.Item2;
                    switch (foldOp)
                    {
                        case "x":
                            if (c.x > foldAmt)
                                newCoords.Add(new Coord(foldAmt - (c.x - foldAmt), c.y));
                            else
                                newCoords.Add(c);
                            break;
                        case "y":
                            if (c.y > foldAmt)
                                newCoords.Add(new Coord(c.x, foldAmt - (c.y - foldAmt)));
                            else
                                newCoords.Add(c);
                            break;
                    }
                }
                coords = newCoords;
            }
        }

        static void printCoords()
        {
            int maxX, maxY;
            maxX = maxY = 0;
            foreach (Coord c in coords)
            {
                maxX = c.x > maxX ? c.x : maxX;
                maxY = c.y > maxY ? c.y : maxY;
            }

            for (int i = 0; i <= maxY; i++)
            {
                for (int j = 0; j <= maxX; j++)
                {
                    Coord c = new Coord(j, i);
                    Console.Write((coords.Contains(c) ? "#" : "."));
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static void readLines()
        {
            coords = new HashSet<Coord>();
            folds = new List<Tuple<string, int>>();
            using (StreamReader sr = new StreamReader(@"c:/users/drslc/downloads/input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()).Length > 0)
                {
                    string[] pair = line.Split(',');
                    coords.Add(new Coord(int.Parse(pair[0]), int.Parse(pair[1])));
                }
                while ((line = sr.ReadLine()) != null)
                {
                    Match m = Regex.Match(line, @"[a-z\s]+(x|y)=(\d+)");
                    folds.Add(new Tuple<string, int>(m.Groups[1].Value, int.Parse(m.Groups[2].Value)));
                }
            }
        }

        class Coord
        {
            public int x;
            public int y;

            public Coord(int _x, int _y)
            {
                x = _x;
                y = _y;
            }


            public override int GetHashCode()
            {
                return ToString().GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (!(obj is Coord))
                    return false;

                return x == ((Coord)obj).x && y == ((Coord)obj).y;
            }
            public override string ToString()
            {
                return "(" + x + ", " + y + ")";
            }
        }
    }
}
