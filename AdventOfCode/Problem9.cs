using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    class Problem9
    {

        static HashSet<Coord> basinCoords = new HashSet<Coord>();
        //public static void Main()
        //{
        //    int[,] board = readBoard();
        //    int[,] visited = new int[board.GetLength(0), board.GetLength(1)];

        //    solve(board, visited);
        //    for (int i = 0; i < board.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < board.GetLength(1); j++)
        //        {
        //            Coord c = new Coord(i, j, -1);
        //            if (!basinCoords.Contains(c))
        //                Console.Write(" " + board[i, j] + "  ");
        //            else
        //                Console.Write("[" + board[i, j] + "] ");
        //        }
        //        Console.WriteLine();
        //    }
        //    Console.ReadLine();

        //}

        private static void solve(int[,] board, int[,] visited)
        {
            List<int> basins = new List<int>();

            List<Coord> sinks = getSinks(board);
            foreach (Coord sink in sinks)
                basins.Add(bfs(board, visited, sink));

            basins.Sort((i1, i2) => i2 - i1);
            int prod = 1;
            for (int i = 0; i < 3; i++)
                prod *= basins[i];

            Console.WriteLine(prod);
        }

        private static int bfs(int[,] board, int[,] visited, Coord sink)
        {
            Queue<Coord> q = new Queue<Coord>();
            int size = 0;
            q.Enqueue(sink);

            while (q.Count > 0)
            {
                Coord curr = q.Dequeue();

                //bounds check
                if (curr.x < 0 || curr.x >= board.GetLength(0) || curr.y < 0 || curr.y >= board.GetLength(1))
                    continue;

                if (visited[curr.x, curr.y] == 1)
                    continue;

                int v = board[curr.x, curr.y];

                if (v == 9 || curr.parentVal > v)
                    continue;

                visited[curr.x, curr.y] = 1;
                size++;
                basinCoords.Add(curr);

                q.Enqueue(new Coord(curr.x - 1, curr.y, v));
                q.Enqueue(new Coord(curr.x + 1, curr.y, v));
                q.Enqueue(new Coord(curr.x, curr.y - 1, v));
                q.Enqueue(new Coord(curr.x, curr.y + 1, v));
            }

            return size;
        }

        private static List<Coord> getSinks(int[,] board)
        {
            List<Coord> sinks = new List<Coord>();
            int[] dxy = new int[] { -1, 1 };
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    int n = board[i, j];
                    bool sink = true;
                    foreach (int dx in dxy)
                        if (i + dx >= 0 && i + dx < board.GetLength(0))
                            sink &= board[i + dx, j] > n;
                    foreach (int dy in dxy)
                        if (j + dy >= 0 && j + dy < board.GetLength(1))
                            sink &= board[i, j + dy] > n;

                    if (sink)
                        sinks.Add(new Coord(i, j, -1));
                }
            }
            return sinks;
        }
        private static int[,] readBoard()
        {
            List<string> input = new List<string>();

            using (StreamReader sr = new StreamReader(@"c:/users/drslc/downloads/input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                    input.Add(line);
            }

            int[,] board = new int[input.Count, input[0].Length];

            for (int i = 0; i < input.Count; i++)
                for (int j = 0; j < input[i].Length; j++)
                    board[i, j] = input[i][j] - '0';

            return board;
        }

        class Coord
        {
            public int x;
            public int y;
            public int parentVal;

            public Coord(int _x, int _y, int _v)
            {
                x = _x;
                y = _y;
                parentVal = _v;
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
