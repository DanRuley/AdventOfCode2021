using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    class Problem11
    {
        //public static void Main()
        //{
        //    int[,] board = readBoard();
        //    solve(board);

        //    Console.Read();
        //}

        private static void solve(int[,] board)
        {
            int flashes = 0;
            int itr = 0;
            while (flashes != board.GetLength(0) * board.GetLength(1))
            {
                printBoard(board);
                flashes = updateStep(board);
                itr++;
            }

            Console.WriteLine(itr);
            Console.ReadLine();
        }

        private static int updateStep(int[,] board)
        {
            Queue<Coord> q = new Queue<Coord>();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j]++;
                    if (board[i, j] > 9)
                        q.Enqueue(new Coord(i, j));
                }
            }

            HashSet<Coord> flashed = new HashSet<Coord>();
            int[] dxy = new int[] { -1, 0, 1 };

            while (q.Count > 0)
            {
                Coord curr = q.Dequeue();
                if (isIllegalCoordinate(curr.x, curr.y, board.GetLength(0), board.GetLength(1)) || flashed.Contains(curr))
                    continue;

                board[curr.x, curr.y]++;

                if (board[curr.x, curr.y] > 9)
                {
                    for (int i = 0; i < dxy.Length; i++)
                        for (int j = 0; j < dxy.Length; j++)
                            q.Enqueue(new Coord(curr.x + dxy[i], curr.y + dxy[j]));

                    flashed.Add(curr);
                    board[curr.x, curr.y] = 0;
                }
            }

            return flashed.Count;
        }

        private static bool isIllegalCoordinate(int x, int y, int xLen, int yLen)
        {
            return (x < 0 || x >= xLen || y < 0 || y >= yLen);
        }

        private static void printBoard(int[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(1); j++)
                    Console.Write(("" + board[i, j]).PadLeft(2) + (j == board.GetLength(1) - 1 ? "\n" : " "));
            Console.WriteLine();
        }

        private static int[,] readBoard()
        {
            int[,] board;
            List<string> lines = new List<string>();

            using (StreamReader sr = new StreamReader(@"c:/users/drslc/downloads/input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                    lines.Add(line);
            }

            board = new int[lines.Count, lines[0].Length];

            for (int i = 0; i < lines.Count; i++)
                for (int j = 0; j < lines[0].Length; j++)
                    board[i, j] = lines[i][j] - '0';

            return board;
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
